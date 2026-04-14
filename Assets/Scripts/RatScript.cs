using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class RatScript : MonoBehaviour
{
    Animator ratAnim;
    Rigidbody2D rb;
    public Image health;
    
    public ParticleSystem punchEffect;
    public Transform targetPosition;
    public Transform punkPos;
    float maxHealth = 100f;
    public float hitCount = 3f;
    public float stopDistance = 0.5f; 
    public float speed = 3f;
    public float attackCoolDown = 1.5f;
    private float lastAttackTime;
    
    

    void Start()
    {
        ratAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        
       
        rb.freezeRotation = true;
    }

    void FixedUpdate() 
    {
        MoveOnScene();
        
    }
    private void Update()
    {
        PunchRat();
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
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitPunch"))
        {
            hitCount--;
            
            ratAnim.SetTrigger("HitRat");
            if (punchEffect != null) punchEffect.Play();
        }

        if (hitCount <= 0f)
        {
            ratAnim.SetTrigger("DeathRat");
            rb.simulated = false; 
            this.enabled = false;
        }
    }
}

