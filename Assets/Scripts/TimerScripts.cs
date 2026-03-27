using UnityEngine;
using UnityEngine.UI;

public class TimerScripts : MonoBehaviour
{
    public Image digit1;
    public Image digit2;

    public Sprite[] digits;

    private float timeRemaining = 60f;
    private int lastSecond = -1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            int currentSecond = Mathf.FloorToInt(timeRemaining);

            if (currentSecond != lastSecond)
            {
                TimerSprites(currentSecond);
                lastSecond = currentSecond;
            }
        }
    }
    void TimerSprites(int seconds)
    {
        int dig1 = seconds / 10;
        int dig2 = seconds % 10;

        if (digits.Length >= 10)
        {
            digit1.sprite = digits[dig1];
            digit2.sprite = digits[dig2];
        }
    }
}
