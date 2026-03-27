using Unity.Mathematics.Geometry;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PunkScript : MonoBehaviour
{
    Rigidbody2D rigidPunk;
    Animator animPunk;
    SpriteRenderer spritePunk;
    Collider2D hitCollider;
    float maxHealth = 100f;
    float currentHealth;
    public UIScripts ui;     
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
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ui.DamageHealth(currentHealth, maxHealth);
        if (currentHealth <= 0f)
        {
            animPunk.SetTrigger("Death");

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; 
                rb.isKinematic = true;      
            }

            this.enabled = false;
        }
    }
    public void Damage(float damage)
    {
        currentHealth -= 30f;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        ui.DamageHealth(currentHealth, maxHealth);

    }
    void Punch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animPunk.SetTrigger("Punch");
            //punchEffect.Play();
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
