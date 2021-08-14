using UnityEngine; 
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System.Collections;

public class PlayerShooting : NetworkBehaviour
{
    public ParticleSystem bulletParticleSystem;
    private ParticleSystem.EmissionModule em;
    float fireRate = 10f;
    float shootTimer = 0f;
    int bullets = 30;
    public int Bullets { get => bullets; }
    private bool isRecharging = false;

    public AudioClip ShottingAudioSource;
    public AudioClip RechargeAudioSource;
    public AudioSource AudioSource;

    Player pp;
    GunsScript gs;

    NetworkVariableBool shooting = 
        new NetworkVariableBool(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly}, false);

    //bool shooting = false;

    void Start()
    {
        em = bulletParticleSystem.emission;     
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer && !isRecharging)
        {
            if (gs.getActiveWeaponIndex() == 0)
            {
                shooting.Value = Input.GetButton(InputConstants.FIRE);
                shootTimer += Time.deltaTime;
                if (Input.GetButtonDown(InputConstants.RECHARGE_WEAPON))
                {
                    AudioSource.PlayOneShot(RechargeAudioSource);
                    isRecharging = true;
                    StartCoroutine(recharge());
                }
                if (shooting.Value && (shootTimer >= 1f / fireRate) && bullets > 0)
                {
                    shootTimer = 0f;
                    bullets--;
                    ShootServerRpc();
                }

            }
            
        }
        em.rateOverTime = shooting.Value && !isRecharging && bullets > 0 ? fireRate : 0f;
    }

    public void initShoot(Player p, GunsScript g)
    {
        gs = g;
        pp = p;
    }

    private bool plantBomb()
    {
        //se adicionar mais arma isso não funciona
        if (gs.getActiveWeaponIndex() == 1 && 2 != (int)pp.team && pp.getPlayerLocation() == "bombSite")
        {
            //planta a bomba
            return true;
        }
        return false;
    }

    private bool defuseBomb()
    {
        if (1 != (int)pp.team && pp.getPlayerLocation() == "bombSite")
        {
            //defusa a bomba
            return true;
        }
        return false;
    }

    private IEnumerator recharge()
    {
        yield return new WaitForSeconds(2.0f);
        bullets = 30;
        isRecharging = false;
    }

    [ServerRpc]
    void ShootServerRpc()
    {
        AudioSource.PlayOneShot(ShottingAudioSource);
        Ray ray = new Ray(bulletParticleSystem.transform.position, bulletParticleSystem.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //acertamos algo
            var player = hit.collider.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.TakeDamage(10f);
            }
        }
    }
}
