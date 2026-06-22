using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public GameObject[] medicStaff = new GameObject[8], patientCast = new GameObject[12];
    public MedicScript medicScript, medicSciptInitial;
    public PatientScript patientScriptInitial;
    public int health = 6, score = 0, medicScriptTaskNumber = 0, gameState = 0;
    
    public AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[16];
    public int medicineSuppliesA = 0;

    public InputActionAsset inputAsset;
    public int taskChosen = 0, numberLastClicked = 0;
    public TMP_Text scoreNum, hPNum;
    public float time;

    public TMP_Text[] textBoxes = new TMP_Text[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //    uIInformation[0].setactive(false);
        //    uIInformation[1].setactive(false);
        //    uIInformation[2].setactive(false);
        //    uIInformation[3].setactive(false);


        //inputAsset = GetComponent<InputActionAsset>();

        medicStaff[0].SetActive(true);
        medicStaff[1].SetActive(true);
        patientCast[0].SetActive(true);
        patientCast[1].SetActive(true);
        for (int i = 2; i < 12; i++) {
            if (i < 8)
            {
                medicStaff[i].SetActive(false);
            }
            patientCast[i].SetActive(false);
        }
        

        textBoxes[0].gameObject.SetActive(false);
        health = 2;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scoreNum.text = score+"";
        hPNum.text = health + "";

        print("Time: " + time);
        switch (time)
        {
            case >20 when time<21:
                patientCast[2].SetActive(true);
                patientScriptInitial = patientCast[2].GetComponent<PatientScript>();
                patientScriptInitial.time = 90f;
                break;
            case >50 when time < 51:
                textBoxes[0].gameObject.SetActive(true);
                textBoxes[0].text = "You survived! There are no more patients left, so you have reached the end. See your high score. Either close or reset.";
                Time.timeScale = 0;
                break;


        }

        if (health <= 0)
        {
            textBoxes[0].gameObject.SetActive(true);
            textBoxes[0].text = "GAME OVER. Your HP is depleted. Your clinic has ''gone under'' financially. Either close or reset.";
            Time.timeScale = 0;
        }
        if (inputAsset.FindAction("Pause").triggered)
        {
            Application.Quit();//This will be made into a proper pause menu at some point.

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

        if (inputAsset.FindAction("Reset").triggered)
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }

    private void FixedUpdate()
    {
        

        

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
                    medicScript.task1Chosen = true;
                    medicScript.buttonText[0].text = numberLastClicked+"";
                    medicScript.duration = 40f;
                    break;
                case 2: medicScript.task2Chosen = true; medicScript.buttonText[1].text = numberLastClicked + ""; medicScript.duration = 30f; break;                    
                case 3: medicScript.task3Chosen = true; medicScript.buttonText[2].text = numberLastClicked + ""; medicScript.duration = 10f; break;                    
            }
            taskChosen = 0;
            numberLastClicked = 0;
        }

    }

    public void LoseScore(int amount) {//Need to incorporate this.
        score -= amount;
        health--;
    }

    

    public void PlaySoundEffect(int num)
    {
        audioSource.PlayOneShot(audioClips[num]);
    }
}
