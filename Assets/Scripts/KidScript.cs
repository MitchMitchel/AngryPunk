using TMPro;
using UnityEngine;

public class KidScript : MonoBehaviour
{
    Animator kidAnim;
    public ParticleSystem punchEffect;
    float hitCount = 3f;
    Rigidbody2D kidRb;
    public Transform targetPosition;

    public float stopDistance = 0.5f; 
    public float speed = 3f;
    void Start()
    {
        kidAnim = GetComponent<Animator>();
        kidRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveOnScene();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitPunch"))
        {
            hitCount--;
            kidAnim.SetTrigger("HitKid");
            punchEffect.Play();
        }
        if (hitCount == 0f)
        {
            kidAnim.SetTrigger("DeathKid");
            this.enabled = false;
        }
    }
    void MoveOnScene()
    {
        if (targetPosition == null) return;

        float distance = Vector2.Distance(kidRb.position, targetPosition.position);

        if (distance > stopDistance)
        {

            Vector2 direction = ((Vector2)targetPosition.position - kidRb.position).normalized;


            Vector2 newPos = kidRb.position + direction * speed * Time.fixedDeltaTime;


            kidRb.MovePosition(newPos);

        }


        if (distance <= stopDistance)
        {
            kidAnim.SetBool("IsIdle", true);
            kidAnim.SetBool("IsGo", false); 
        }
        else
        {
            kidAnim.SetBool("IsIdle", false);
            kidAnim.SetBool("IsGo", true);
        }

    }
}
