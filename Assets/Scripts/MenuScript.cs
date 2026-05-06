using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button play;
    public Button exit;
    public GameObject logo;
    public Canvas canvas;
    public TimerScripts timer;
    public SpawnerScript spawner;
    public RatScript ratS;
    public KidScript kid;
    AudioSource audio;
    public GameObject level;
    public AudioClip playClip;
    public AudioClip startClip;
    
    public UIScripts ui;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = startClip;
        audio.loop = true;
        audio.Play();
        play.onClick.AddListener(Play);
        exit.onClick.AddListener(Exit);
        
        spawner.gameObject.SetActive(false);
        timer.digit1.gameObject.SetActive(false);
        timer.digit2.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        
    }
   
    public void Exit()
    {
        AudioSource.PlayClipAtPoint(playClip, Camera.main.transform.position);
        Application.Quit();

    }
    
    public void Play()
    {
        AudioSource.PlayClipAtPoint(playClip, Camera.main.transform.position);
        play.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);

        level.gameObject.GetComponentInChildren<Animator>().SetTrigger("Level");
        
        StartCoroutine(AllPlaying());
        
    }
    public IEnumerator AllPlaying()
    {
        
        yield return new WaitForSeconds(3);

        audio.Stop();
        audio.loop = false;
        
        canvas.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        timer.digit1.gameObject.SetActive(true);
        timer.digit2.gameObject.SetActive(true);
        gameObject.SetActive(false);
        ratS.gameObject.SetActive(true);
        kid.gameObject.SetActive(true);
    }
}
