using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{

	public int damagePerShot = 20;
	public float fireRate = 0f;
	public float range = 100f;
	public int animWeaponType;
	public int currentAmmoInMagazine;
    public Transform gunTip;
    public int maxAmmoInMagazine = 10;
	public float reloadTime = 1f;
	public int currentAmmo = 20;


    ParticleSystem gunParticles;
    AudioSource gunAudio;

    //   public GameObject bullet;
    //   public float bulletForce = 100;
    private bool isReloading;
    
	[SerializeField]
	//Animator anim;

	float timer;
	int shootableMask1;
    Ray shootRay;
    RaycastHit shootHit;
    //int shootableMask2;
    //int shootableMask3;
    LineRenderer gunLine;
	float effectsDisplayTime = 0.02f;

    bool haveShoot = false;


	void Start ()
	{
		currentAmmoInMagazine = maxAmmoInMagazine;
	}

	void Awake ()
	{
		shootableMask1 = LayerMask.GetMask ("Shootable");
		//shootableMask2 = LayerMask.GetMask ("Wall");
		//shootableMask3 = LayerMask.GetMask ("Doors");

		// Set up the references.
		gunParticles = gunTip.gameObject.GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
        
     
        gunAudio = GetComponent<AudioSource> ();
        //gunLight = GetComponent<Light> ();

    }

	void OnEnable ()
	{
		//anim.SetInteger ("WeaponType", animWeaponType);
		//anim.SetTrigger ("WeaponChange");
		isReloading = false;
		//anim.SetBool ("Reloading", false);

        gunLine.enabled = false;

	}



	void Update ()
	{
        
        haveShoot = false;
		timer += Time.deltaTime;

		if (timer >= effectsDisplayTime) {
			DisableEffects ();
		}

        if (isReloading)
            return;


        if (!gunLine.enabled)
        {
            gunLine.enabled = true;
        }

        if (currentAmmoInMagazine <= 0 && currentAmmo > 0) {
			StartCoroutine (Reload ());
			return;
		}
			
		if (currentAmmoInMagazine > 0) {
			if (Input.GetKeyDown (KeyCode.R) && currentAmmo > 0 && currentAmmoInMagazine < maxAmmoInMagazine)
				StartCoroutine (Reload ());


			if (Input.GetButton ("Fire1") && timer >= fireRate && fireRate > 0) {
                haveShoot = true;
				Shoot (true);
			}

			if (Input.GetButtonDown ("Fire1") && fireRate == 0) {
                haveShoot = true;
				Shoot (true);
			}
		}
		
		if(haveShoot == false)
        {
            Shoot(false);
        }
		
			
	}

	public void DisableEffects ()
	{
        //gunLine.enabled = false;
       // gunFlah.enabled = false;
		//gunLight.enabled = false;
	}

	IEnumerator Reload ()
	{

		isReloading = true;
        gunLine.enabled = false;

		yield return new WaitForSeconds (reloadTime);

		if ((currentAmmoInMagazine + currentAmmo) >= maxAmmoInMagazine) {
			currentAmmo -= maxAmmoInMagazine - currentAmmoInMagazine;
			currentAmmoInMagazine = maxAmmoInMagazine;
		} else {
			currentAmmoInMagazine = currentAmmoInMagazine + currentAmmo;
			currentAmmo = 0;
		}

        isReloading = false;
	}

    void Shoot(bool reallyShoot)
    {
        if (reallyShoot)
        {
            timer = 0f;
            currentAmmoInMagazine--;

            //GameObject temporaryBulletHandler;
            //temporaryBulletHandler = Instantiate(bullet, gunTip.transform.position, gunTip.transform.rotation) as GameObject;
            //temporaryBulletHandler.transform.Rotate(Vector3.left * 90);

            //Rigidbody temporaryRigidbody;
            //temporaryRigidbody = temporaryBulletHandler.GetComponent<Rigidbody>();

            //temporaryRigidbody.AddForce(transform.forward * bulletForce);

            //Destroy(temporaryBulletHandler, 10f);


            //gunFlah.enabled = true;
            // anim.SetTrigger("Attack");
            gunAudio.Play();
            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();
        }
 

        // Enable the light.
        //cRadLight.radius = rangeOfFireFlash;
        //gunLight.enabled = true;

       

        // Enable the line renderer and set it's first position to be the end of the gun.
        //gunLine.enabled = true;
        gunLine.SetPosition(0, gunTip.position);
        shootRay.origin = gunTip.position;
        shootRay.direction = gunTip.forward;
        //Debug.DrawRay (firePos, transform.right * range);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask1)){
            if (reallyShoot)
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot);
                }
            }
			gunLine.SetPosition (1, shootHit.point);
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}