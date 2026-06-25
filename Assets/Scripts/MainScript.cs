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
    public int[] medicalSupplies = new int[8];
    //public int medicineSuppliesA = 0;

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
        health = 6;

        textBoxes[2].gameObject.SetActive(false);
        textBoxes[3].gameObject.SetActive(false);
        textBoxes[4].gameObject.SetActive(false);
        textBoxes[5].gameObject.SetActive(false);
        textBoxes[10].gameObject.SetActive(false);
        textBoxes[11].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scoreNum.text = score+"";
        hPNum.text = health + "";

        //print("Time: " + time);
        switch (time)
        {
            case >20 when time<21:
                SetPatient(2, 0, 90f, false);
                break;
            case >30 when time < 31:
                SetPatient(3, 0, 90f, false);
                break;
            case > 50 when time < 51:
                SetPatient(4, 0, 90f, false);
                break;
            case > 70 when time < 71:
                SetPatient(5, 0, 90f, false);
                SetPatient(6, 1, 90f, true);

                textBoxes[2].gameObject.SetActive(true);
                textBoxes[10].gameObject.SetActive(true);
                break;
            case > 80 when time < 81:
                SetMedic(2, 1);
                break;
            case > 90 when time < 91:
                SetPatient(0, 0, 90f, true);
                break;
            case > 100 when time < 101:
                SetPatient(1, 0, 90f, false);
                SetPatient(2, 1, 90f, true);
                break;
            case > 120 when time < 121:
                SetPatient(9, 1, 90f, true);
                SetPatient(10, 1, 90f, true);
                SetPatient(11, 1, 90f, false);
                patientCast[9].SetActive(true);
                break;
            case > 130 when time < 131:
                SetPatient(3, 0, 90f, false);
                SetPatient(4, 0, 90f, false);
                break;
            case > 150 when time < 151:
                SetPatient(5, 0, 90f, false);
                SetPatient(6, 0, 90f, false);
                SetPatient(7, 1, 90f, true);
                SetPatient(8, 0, 90f, false);
                break;
            case > 300 when time < 301:
                textBoxes[0].gameObject.SetActive(true);
                textBoxes[0].text = "You survived! There are no more patients left, so you have reached the end. See your high score. Either close or reset.";
                Time.timeScale = 0;
                break;
                //The UI for different supplies appears at some point.

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

        //medical supplies displayed
        textBoxes[6].text = medicalSupplies[0] + "";
        textBoxes[7].text = medicalSupplies[1] + "";
        textBoxes[8].text = medicalSupplies[2] + "";
        textBoxes[9].text = medicalSupplies[3] + "";

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

        if (taskChosen > 0 && numberLastClicked > 0)
        {
            
            medicScript.destinations[medicScriptTaskNumber-1] = numberLastClicked;
            switch (taskChosen)
            {
                case 0: break;
                    case 1:
                    if (!patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready)
                    //if (!medicScript.destinations[medicScriptTaskNumber - 1].patientScript.selectedAlready)
                    {
                        patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task1Chosen = true;
                        medicScript.buttonText[0].text = numberLastClicked + "";
                        medicScript.durationC = 40f;
                        textBoxes[1].gameObject.SetActive(false);
                    } else
                    {
                        print("This one's already selected! 1/3");
                        textBoxes[1].gameObject.SetActive(true);
                    }
                                           
                    break;
                case 2:
                    if (!patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready)
                    {
                        patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task2Chosen = true;
                        medicScript.buttonText[1].text = numberLastClicked + "";
                        medicScript.durationC = 30f; 
                    }
                    else
                    {
                        print("This one's already selected! 2/3");
                    }
                    break;
                case 3:
                    if (!patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready)
                    {
                        patientCast[numberLastClicked].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task3Chosen = true;
                        medicScript.buttonText[2].text = numberLastClicked + "";
                        medicScript.durationC = 10f;
                    }
                    else
                    {
                        print("This one's already selected! 3/3");
                    }
                    break;                    
            }
            medicScript.duration = medicScript.durationC;
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
        print("Play sound " + num);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClips[num]);
        }
    }

    public void SetPatient(int num, int ailment, float time, bool isSupplyBox)
    {
        patientCast[num].SetActive(true);
        patientScriptInitial = patientCast[num].GetComponent<PatientScript>();
        patientScriptInitial.ailment = ailment;
        patientScriptInitial.time = time;
        patientScriptInitial.isSupplyBox = isSupplyBox;
    }

    public void SetMedic(int num, int profession)
    {
        medicStaff[num].SetActive(true);
        medicSciptInitial = medicStaff[num].GetComponent<MedicScript>();
        medicSciptInitial.profession = profession;
    }
}
