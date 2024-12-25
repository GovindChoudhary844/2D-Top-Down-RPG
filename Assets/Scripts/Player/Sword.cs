using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnpoint;
    //[SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;


    private Animator animator;
    private GameObject slashAnim;
    private Camera mainCamera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        slashAnimSpawnpoint = GameObject.Find("SlashSpawnPoint").transform;
    }


    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        // Trigger attack animation
        animator.SetTrigger("Attack");

        // Instantiate slash animation
        if (slashAnimPrefab != null && slashAnimSpawnpoint != null)
        {
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnpoint.position, Quaternion.identity);
            slashAnim.transform.parent = transform.parent;
        }
        else
        {
            Debug.LogError("SlashAnimPrefab or SlashAnimSpawnpoint is not set.");
        }
    }


    public void SwingUpFlipAnimEvent()
    {
        FlipSlashAnimation(-180);
    }

    public void SwingDownFlipAnimEvent()
    {
        FlipSlashAnimation(0);
    }

    private void FlipSlashAnimation(float angle)
    {
        if (slashAnim == null)
        {
            Debug.LogError("Slash animation is missing.");
            return;
        }

        slashAnim.transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Flip the slash animation if the player is facing left
        if (PlayerController.Instance != null && PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // Calculate angle between player and mouse
        Vector3 direction = mousePos - playerScreenPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Restrict the rotation angle to 30% of the full range
        float minAngle = -25f; 
        float maxAngle = 25f;  

        // Adjust rotation based on player's facing direction
        if (PlayerController.Instance != null && PlayerController.Instance.FacingLeft)
        {
            // Flip angle for left-facing direction and apply clamping
            float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 180, clampedAngle);
        }
        else
        {
            // Clamp angle for right-facing direction
            float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
        }
    }




}