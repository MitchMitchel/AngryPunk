using UnityEngine;

public class OficerScript : MonoBehaviour
{
    Animator officerAnim;
    public ParticleSystem punchEffect;
    float hitCount = 3f;

    void Start()
    {
        officerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitPunch"))
        {
            hitCount --;
            officerAnim.SetTrigger("Hit");
            punchEffect.Play();
        }
        if (hitCount == 0f)
        {
            officerAnim.SetTrigger("DeathOfficer");
            this.enabled = false;
        }
    }
}
