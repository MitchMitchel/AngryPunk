using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public Image healthFill;
    public Image gameOver;

    public Image digit1;
    public Image digit2;
    public Image digit3;
    
    
    public Sprite[] digitsScore;


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

    public void UpdateScore(float currentScore)
    {
        int score = (int)currentScore;

        int unitsIndex = score % 10;

        digit3.sprite = digitsScore[unitsIndex];

        score /= 10;

        int tensIndex = score % 10;

        digit2.sprite = digitsScore[tensIndex];

        score /= 10;

        int hundrIndex = score % 10;

        digit1.sprite = digitsScore[hundrIndex];

        score /= 10;
    }
}
