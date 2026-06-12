using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainScript : MonoBehaviour
{
    public GameObject[] medicStaff = new GameObject[8];
    public MedicScript medicScript;
    public int health = 6, score = 0;
    
    public AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[16];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //    uIInformation[0].setactive(false);
    //    uIInformation[1].setactive(false);
    //    uIInformation[2].setactive(false);
    //    uIInformation[3].setactive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseScore(int amount) {
        score -= amount;
        health--;
    }

    public void PlaySoundEffect(int num)
    {
        audioSource.PlayOneShot(audioClips[num]);
    }
}
