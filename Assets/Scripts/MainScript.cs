using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public GameObject[] medicStaff = new GameObject[8];
    public MedicScript medicScript;
    public int health = 6, score = 0, medicScriptTaskNumber = 0;
    
    public AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[16];
    public int medicineSuppliesA = 0;

    public InputActionAsset inputAsset;
    public int taskChosen = 0, numberLastClicked = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //    uIInformation[0].setactive(false);
        //    uIInformation[1].setactive(false);
        //    uIInformation[2].setactive(false);
        //    uIInformation[3].setactive(false);


        //inputAsset = GetComponent<InputActionAsset>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (inputAsset.FindAction("Pause").triggered)
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                //pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                //pauseMenu.SetActive(true);
            }
        }

        //if (inputAsset.FindAction("Click").triggered) {
        //    print("Player clicked.");
        //}
        if (medicScript != null)
        {

        }

        //if (taskChosen != 0)
        //{
        //    print("Task Chosen: " + taskChosen);
        //}
        //if (numberLastClicked != 0)
        //{
        //    print("Number Chosen: " + numberLastClicked);
        //}

        if (taskChosen != 0 && numberLastClicked != 0)
        {
            medicScript.destinations[medicScriptTaskNumber-1] = numberLastClicked;
            switch (taskChosen)
            {
                case 0: break;
                    case 1:
                    medicScript.task1Chosen = true; break;
                case 2: medicScript.task2Chosen = true; break;
                case 3: medicScript.task3Chosen = true; break;
            }
            taskChosen = 0;
            numberLastClicked = 0;
        }

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
