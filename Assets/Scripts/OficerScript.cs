using UnityEngine;

public class OficerScript : MonoBehaviour
{
    Animator officerAnim;
    public ParticleSystem punchEffect;

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
            officerAnim.SetTrigger("Hit");
            punchEffect.Play();
        }
    }
}
