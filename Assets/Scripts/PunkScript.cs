using UnityEditor.Tilemaps;
using UnityEngine;

public class PunkScript : MonoBehaviour
{
    Rigidbody2D rigidPunk;
    Animator animPunk;
    SpriteRenderer spritePunk;
    Collider2D hitCollider;
    //public ParticleSystem punchEffect;

    bool IsPoisoned = false;
    bool IsEat = false;



    [SerializeField] float speed = 3f;
    void Start()
    {
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
        
        if (directionX > 0.1f)
        {
            spritePunk.flipX = false;
            
        }
        if (directionX < -0.1f)
        {
            spritePunk.flipX = true;
            
        }

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
