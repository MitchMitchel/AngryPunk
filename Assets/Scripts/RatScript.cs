using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class RatScript : MonoBehaviour
{
    Animator ratAnim;
    Rigidbody2D rb;
    public Slider health;
    public GameObject healthBar;
    public ParticleSystem punchEffect;
    public Transform targetPosition;
    public Transform punkPos;
    public UIScripts ui;
    public PunkScript punk;
    AudioSource audio;
    public AudioClip hitClip;
    float maxHealth = 100f;
    float currentHealth;
    public float amount = 30f;
    public float hitCount = 3f;
    public float stopDistance = 0.5f; 
    public float speed = 3f;
    public float attackCoolDown = 1.5f;
    private float lastAttackTime;
    bool isUsed = false;

    

    void Start()
    {
        currentHealth = maxHealth;
        health.maxValue = maxHealth; 
        health.value = currentHealth;
        ratAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        healthBar.SetActive(false);
        rb.freezeRotation = true;
        this.gameObject.SetActive(false);
    }

    void FixedUpdate() 
    {
        MoveOnScene();
        
    }
    private void Update()
    {
        PunchRat();
    }
    public void HealthRat(ref float currentHealth, float maxHealth, float ammount)
    {
        
        if (currentHealth <= 0 && ammount < 0) return;
        currentHealth += ammount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        health.value = currentHealth;
    }

    void MoveOnScene()
    {
        if (targetPosition == null) return;

        float distance = Vector2.Distance(rb.position, targetPosition.position);

        if (distance > stopDistance)
        {    
            Vector2 direction = ((Vector2)targetPosition.position - rb.position).normalized;     
            Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;         
            rb.MovePosition(newPos);
            
        }

        
        if (distance <= stopDistance)
        {
            ratAnim.SetBool("IsIdle", true);
            ratAnim.SetBool("IsRunning", false);
            if (!healthBar.activeSelf) healthBar.SetActive(true);
        }
        else
        {
            ratAnim.SetBool("IsIdle", false);
            ratAnim.SetBool("IsRunning", true);
        }
        
    }
    void EndPunch()
    {
        ratAnim.SetBool("IsIdle", true);
    }
    void PunchRat()
    {
        float dist = Vector3.Distance(punkPos.position, transform.position);

        if (dist < 2f)
        {
            if (Time.time >= lastAttackTime + attackCoolDown)
            {
                ratAnim.SetTrigger("Punch");
                lastAttackTime = Time.time;
            }        
        }       
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("HitPunch") && !isUsed)
        {
            isUsed = true;
            HealthRat(ref currentHealth,100f,- 30f);
            ui.UpdateScore(10);
            audio.PlayOneShot(hitClip);
            ratAnim.SetTrigger("HitRat");
            
            if (punchEffect != null) punchEffect.Play();
            Invoke("ResetHit", 0.5f);
        }

        if (currentHealth <= 0f)
        {
            ratAnim.SetTrigger("DeathRat");
            rb.simulated = false; 
            this.enabled = false;
        }

        
    }
    void ResetHit()
    {
        isUsed = false;
    }
}

