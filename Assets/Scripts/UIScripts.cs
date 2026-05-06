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
    public Image win;
    public PunkScript punk;    
    public Sprite[] digitsScore;
    private float totalScore = 0f;
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
        totalScore += currentScore;
        int score = (int)totalScore;
        int tempScore = score;

        digit3.sprite = digitsScore[tempScore % 10];
        tempScore /= 10;

        digit2.sprite = digitsScore[tempScore % 10];
        tempScore /= 10;

     
        digit1.sprite = digitsScore[tempScore % 10];

        if (totalScore >= 100)
        {
            
            punk.animPunk.SetBool("Dance",true);
            win.GetComponent<Animator>().SetTrigger("Win");
        }
    }
}
