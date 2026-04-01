using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

using UnityEngine;

public class RatScript : MonoBehaviour
{
    Animator ratAnim;
    Rigidbody2D rb; // Добавляем ссылку на компонент

    public ParticleSystem punchEffect;
    public Transform targetPosition;

    public float hitCount = 3f;
    public float stopDistance = 0.5f; // Уменьшил для точности
    public float speed = 3f;

    void Start()
    {
        ratAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Инициализируем Rigidbody

        // Чтобы крыса не вращалась как бешеная при столкновениях:
        rb.freezeRotation = true;
    }

    void FixedUpdate() // Для физики лучше использовать FixedUpdate
    {
        MoveOnScene();
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
            ratAnim.SetBool("IsRunning", false); // Не забудь выключить бег!
        }
        else
        {
            ratAnim.SetBool("IsIdle", false);
            ratAnim.SetBool("IsRunning", true);
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
            rb.simulated = false; // Выключаем физику после смерти
            this.enabled = false;
        }
    }
}

