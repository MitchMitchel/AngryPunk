using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public Image healthFill;
    public Image gameOver;

    Animator gameAnim;


    
    public void DamageHealth(float currentHealth,float maxHealth)
    {
        gameAnim = GetComponentInChildren<Animator>();
        healthFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0f)
        {
            gameAnim.SetTrigger("GameOver");
            //gameAnim.SetTrigger("Death");
        }
    }
}
