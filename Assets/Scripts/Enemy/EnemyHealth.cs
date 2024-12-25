using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 10f;

    private int currentHealth;
    private Knockback knockback; 
    private Flash flash;

    private void Awake()
    {
        // Ensure the Knockback and Flash scripts are attached to the same GameObject
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();

        // If Knockback or Flash is missing, log a warning
        if (knockback == null)
        {
            Debug.LogWarning("Knockback script not found on " + gameObject.name);
        }

        if (flash == null)
        {
            Debug.LogWarning("Flash script not found on " + gameObject.name);
        }
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Only apply knockback if the Knockback script exists
        if (knockback != null && PlayerController.Instance != null)
        {
            knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        }
        else
        {
            if (PlayerController.Instance == null)
            {
                Debug.LogWarning("PlayerController.Instance is null");
            }
        }

        // Flash effect after taking damage
        if (flash != null)
        {
            StartCoroutine(flash.FlashRoutine());
        }

        // Check for death after the flash routine is done
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            // Instantiate the death VFX and destroy the enemy
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
