using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private GameObject playerPunk;
    private Animator animPunk;

    public enum Food { Banana, Apple, Stub, JunkFood, Song }
    public Food type;

    private void Start()
    {
        playerPunk = GameObject.FindWithTag("Player");

        if (playerPunk != null)
        {
            animPunk = playerPunk.GetComponent<Animator>();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case Food.Banana:
                    animPunk.SetTrigger("EatFood");
                    Destroy(gameObject);
                    break;
                case Food.Apple:
                    animPunk.SetTrigger("EatFood");
                    Destroy(gameObject);
                    break;
                case Food.Stub:
                    animPunk.SetTrigger("Intoxication");
                    Destroy(gameObject);
                    break;
                case Food.JunkFood:
                    animPunk.SetTrigger("Intoxication");
                    Destroy(gameObject);
                    break;
               
            }
        }
    }
}
