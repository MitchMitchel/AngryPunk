using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PunkScript : MonoBehaviour
{
    Rigidbody2D rigidPunk;
    public Animator animPunk;
    SpriteRenderer spritePunk;
    Collider2D hitCollider;
    public AudioSource audio;
    public AudioClip eatAudio;
    float maxHealth = 100f;
    float currentHealth;
    public UIScripts ui;
    public bool hasHit = false;
    public float amount = 30f;
    public int scHit = 0;
    private bool isDead = false;
    //public ParticleSystem punchEffect;

    bool IsPoisoned = false;
    bool IsEat = false;


    [SerializeField] float speed = 3f;
    [SerializeField] float minX, maxX, minY, maxY;
    void Start()
    {
        currentHealth = maxHealth;
        ui.DamageHealth(currentHealth, maxHealth);
        rigidPunk = GetComponent<Rigidbody2D>();
        animPunk = GetComponent<Animator>();
        spritePunk = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPoisoned && !IsEat)
        {
            Run();
            Punch();
        }
        else
        {
            rigidPunk.linearVelocity = Vector2.zero;
            animPunk.SetFloat("Run", 0);
        }
        
    }
    void Run()
    {
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");
        Vector2 mover = new Vector2(directionX, directionY);
        rigidPunk.linearVelocity = mover * speed ;
        animPunk.SetFloat("Run", Mathf.Abs(directionX));

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        if (directionX > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        if (directionX < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
    }
    public void HealthPlayer(float amount)
    {
        if (currentHealth <= 0) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ui.DamageHealth(currentHealth, maxHealth);
        
    }
    public void DamageEat(float damage)
    {
        currentHealth -= 30f;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ui.DamageHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            animPunk.SetBool("IsDeath", true);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.simulated = false;
            }
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            enabled = false;
        }
    }
    public void DamageFight(float damage)
    {
        if (isDead) return;        
        currentHealth -= 10f;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ui.DamageHealth(currentHealth, maxHealth);
        if (currentHealth <= 0f)
        {
            isDead = true;
            animPunk.SetBool("IsDead",true);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.simulated = false;

            }
            enabled = false;
        }
    }
    void Punch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animPunk.SetTrigger("Punch");
            
            
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHit"))
        {
            animPunk.SetTrigger("Damage");
            DamageFight(30f);                    
        }    
    }
    
    
    void EndPunch()
    {    
        animPunk.SetBool("Idle", true);
    }
    void StartPoison()
    {
        IsPoisoned = true;
    }
    void StopPoison()
    {
        IsPoisoned = false;
    }
    void StartEat()
    {
        IsEat = true;
    }
    void StopEat()
    {
        IsEat = false;
    }
    
}
