
using MLAPI;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : NetworkBehaviour
{
    CharacterController cc;
    [SerializeField]
    private Transform cameraTransform;
    public Transform player;
    public GameObject guns;
    [SerializeField]
    public float speed = 1f;
    float pitch = 0f;
    GunsScript gunsScript;
    public float gravityMultiplier = 1f;
    public float jumpHeight = 2.5f;
    private float height;
    private StanceHUD StanceHud;
    private PlayerHealthBar HealthHud;
    private BulletsHud BulletsHud;

    private PlayerShooting ps;

    public UnityAction<bool, bool> OnStanceChanged { get; internal set; }

    public bool IsCrouching { get; private set; }
    public bool IsSprinting { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;

        }
        else
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            cc = GetComponent<CharacterController>();
            gunsScript = (GunsScript)guns.GetComponent(typeof(GunsScript));
            StanceHud = FindObjectOfType<StanceHUD>();
            HealthHud = FindObjectOfType<PlayerHealthBar>();
            BulletsHud = FindObjectOfType<BulletsHud>();
            ps = GetComponent<PlayerShooting>();
            ps.initShoot(GetComponent<Player>(), gunsScript);
            HealthHud.InitHealthComponent(GetComponent<PlayerHealth>());
            BulletsHud.InitBulletsComponent(GetComponent<PlayerShooting>());
            StanceHud.SetCharacter(this);
        }
        height = Physics.gravity.y * gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            MovelPlayer();
            selectActiveWeapon();
            Look();
        }
    }

    private void selectActiveWeapon()
    {
        if (Input.GetButtonDown(InputConstants.PRIMARY_WEAPON))
        {
            gunsScript.ChangeActualWeapon(0);
        }
        if (Input.GetButtonDown(InputConstants.SECONDARY_WEAPON))
        {
            gunsScript.ChangeActualWeapon(1);
        }
        if (Input.GetButtonDown(InputConstants.OTHER_WEAPON))
        {
            gunsScript.ChangeActualWeapon(2);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            gunsScript.ChangeActualWeaponFoward();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            gunsScript.ChangeActualWeaponBackward();
        }
    }

    private void MovelPlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis(InputConstants.AXIS_HORIZONTAL), height, Input.GetAxis(InputConstants.AXIS_VERTICAL));
        // Jump
        if (Input.GetButtonDown(InputConstants.JUMP) && cc.isGrounded)
        {
            move.y = jumpHeight;
        }
        // end Jump
        // Sprint
        if (Input.GetButtonDown(InputConstants.SPRINT))
        {
            IsSprinting = true;
            speed *= 2;
        }
        if (Input.GetButtonUp(InputConstants.SPRINT))
        {
            IsSprinting = false; 
            speed /= 2;
        }
        // end Sprint
        // Crouch
        if (Input.GetButtonDown(InputConstants.CROUCH))
        {
            IsCrouching = true;
            speed /= 3;
            jumpHeight /= 1.5f;
            player.localScale *= 0.5f;
        }
        if (Input.GetButtonUp(InputConstants.CROUCH))
        {
            IsCrouching = false;
            speed *= 3;
            jumpHeight *= 1.5f;
            player.localScale /= 0.5f;
        }
        if (OnStanceChanged != null)
        {
            OnStanceChanged.Invoke(IsCrouching, IsSprinting);
        }
        // end Crouch
        // Gravity
        if (cc.isGrounded == false)
        {
            move.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
        // end Gravity
        height = move.y;
        move = transform.TransformDirection(move);
        cc.Move(speed * Time.deltaTime * move);
    }

    void Look()
    {
        float mousex = Input.GetAxis(InputConstants.MOUSE_X) * 3f;
        transform.Rotate(0, mousex, 0);
        pitch -= Input.GetAxis(InputConstants.MOUSE_Y) * 3f;
        pitch = Mathf.Clamp(pitch, -45, 45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}