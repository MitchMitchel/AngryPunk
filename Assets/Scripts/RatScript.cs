using UnityEngine;

public class RatScript : MonoBehaviour
{
    Animator ratAnim;
    public ParticleSystem punchEffect;
    float hitCount = 3f;

    public Transform targetPosition;
    public float speed = 3f;
    
    
    void Start()
    {
        ratAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitPunch"))
        {
            hitCount--;
            ratAnim.SetTrigger("HitRat");
            punchEffect.Play();
        }
        if (hitCount == 0f)
        {
            ratAnim.SetTrigger("DeathRat");
            this.enabled = false;
        }
    }
    void MoveOnScene()
    {
        
    }
    void Update()
    {
        MoveOnScene();

        

    }
}
