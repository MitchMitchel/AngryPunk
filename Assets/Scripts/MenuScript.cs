using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button play;
    public Button exit;
    public Canvas canvas;
    public TimerScripts timer;
    public SpawnerScript spawner;
    public RatScript ratS;
    public KidScript kid;
    void Start()
    {
        play.onClick.AddListener(Play);
    }

    
    void Update()
    {
        
    }
    
    public void Play()
    {
        canvas.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        timer.digit1.gameObject.SetActive(true);
        timer.digit2.gameObject.SetActive(true);
        gameObject.SetActive(false);
        ratS.gameObject.SetActive(true);
        kid.gameObject.SetActive(true);
    }

}
