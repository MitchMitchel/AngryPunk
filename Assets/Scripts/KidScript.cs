using UnityEngine;

public class KidScript : MonoBehaviour
{
    Animator kidAnim;
    public ParticleSystem punchEffect;
    float hitCount = 3f;
    void Start()
    {
        kidAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
