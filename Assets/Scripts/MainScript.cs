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
    public float time, finTime = 220;

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
        health = 8;

        textBoxes[2].gameObject.SetActive(false);
        textBoxes[3].gameObject.SetActive(false);
        textBoxes[4].gameObject.SetActive(false);
        textBoxes[5].gameObject.SetActive(false);
        textBoxes[10].gameObject.SetActive(false);
        textBoxes[11].gameObject.SetActive(false);
        textBoxes[12].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scoreNum.text = score+"";
        hPNum.text = health + "";

        if (Mathf.Floor((finTime - time) % 60) < 10)
        {
            textBoxes[14].text = Mathf.Floor((finTime - time)/60) + ":0" + Mathf.Floor(finTime - time) % 60;
        }
        else
        {
            textBoxes[14].text = Mathf.Floor((finTime - time) / 60) + ":" + Mathf.Floor(finTime - time) % 60;
        }
        //textBoxes[14].text = 180f - time +"";

        //print("Time: " + time);
        switch (time)
        {
            case > 0 when time < 1:
                SetPatient(0, 0, 55f, false);
                SetPatient(1, 0, 55f, false);
                break;
            /*
            case > 1 when time < 2: //THIS WILL BE REMOVED.
                SetPatient(0, 0, 900f, false);
                SetPatient(1, 0, 900f, false);
                SetPatient(2, 0, 900f, false);
                SetPatient(3, 0, 900f, false);
                SetPatient(4, 0, 900f, true);
                SetPatient(5, 0, 900f, false);
                SetPatient(6, 0, 900f, false);
                SetPatient(7, 0, 900f, true);
                SetPatient(8, 0, 900f, false);
                SetPatient(9, 0, 900f, false);
                SetPatient(10, 0, 900f, false);
                SetPatient(11, 0, 900f, false);
                break;
                */

            //RESTORE THIS LATER
            case >20 when time<21:
                SetPatient(2, 0, 55f, false);
                break;
            case >30 when time < 31:
                SetPatient(3, 0, 55f, false);
                break;
            case > 50 when time < 51:
                SetPatient(4, 0, 55f, false);
                break;
            case > 70 when time < 71:
                SetPatient(5, 0, 55f, false);
                SetPatient(6, 1, 55f, true);

                textBoxes[2].gameObject.SetActive(true);
                textBoxes[10].gameObject.SetActive(true);
                break;
            case > 80 when time < 81:
                SetMedic(2, 1);
                break;
            case > 90 when time < 91:
                SetPatient(0, 0, 60f, false);
                break;
            case > 100 when time < 101:
                SetPatient(1, 0, 60f, false);
                SetPatient(2, 1, 60f, true);
                SetMedic(3, 1);
                //SetMedic(4, 1);
                break;
            case > 120 when time < 121:
                SetPatient(9, 1, 60f, true);
                SetPatient(10, 1, 60f, true);
                SetPatient(11, 1, 60f, false);
                break;
            case > 130 when time < 131:
                SetPatient(3, 0, 60f, false);
                SetPatient(4, 0, 60f, false);
                break;
            case > 160 when time < 151:
                SetPatient(5, 0, 60f, false);
                SetPatient(6, 0, 60f, false);
                SetPatient(7, 1, 60f, true);
                SetPatient(8, 0, 60f, false);
                break;
            case > 190 when time < 171:
                SetPatient(9, 0, 60f, false);
                SetPatient(10, 1, 60f, false);
                SetPatient(11, 0, 60f, false);
                SetPatient(11, 1, 60f, true);
                break;

        }
        if (time > finTime)
        {
            textBoxes[0].gameObject.SetActive(true);
            textBoxes[0].text = "You survived! No more waiting patients. See your score. Either close or reset.";
            Time.timeScale = 0;
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
        textBoxes[6].text = medicalSupplies[1] + "";
        textBoxes[7].text = medicalSupplies[2] + "";
        textBoxes[8].text = medicalSupplies[3] + "";
        textBoxes[9].text = medicalSupplies[4] + "";

    }

    private void FixedUpdate()
    {
        

        

        //if (inputAsset.FindAction("Click").triggered) {
        //    print("Player clicked.");
        //}
        //if (medicScript != null)
        //{

        //}

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
                    if (!patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready)
                    //if (!medicScript.destinations[medicScriptTaskNumber - 1].patientScript.selectedAlready)
                    {
                        patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task1Chosen = true;
                        medicScript.buttonText[0].text = numberLastClicked + "";
                        medicScript.durationC = 60f;
                        textBoxes[12].gameObject.SetActive(false);
                    } else
                    {
                        print("This one's already selected! 1/3");
                        textBoxes[12].gameObject.SetActive(true);
                        if(!medicScript.awaitingSchedule)
                        {
                            patientCast[numberLastClicked - 1].GetComponent<PatientScript>().selectedAlready = false;
                            textBoxes[12].gameObject.SetActive(false);
                        }
                    }
                                           
                    break;
                case 2:
                    if (!patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready)
                    {
                        patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task2Chosen = true;
                        medicScript.buttonText[1].text = numberLastClicked + "";
                        medicScript.durationC = 60f;
                        textBoxes[12].gameObject.SetActive(false);
                    }
                    else
                    {
                        print("This one's already selected! 2/3");
                        textBoxes[12].gameObject.SetActive(true);
                        if (!medicScript.awaitingSchedule)
                        {
                            patientCast[numberLastClicked - 1].GetComponent<PatientScript>().selectedAlready = false;
                            textBoxes[12].gameObject.SetActive(false);
                        }
                    }
                    break;
                case 3:
                    if (!patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready)
                    {
                        patientCast[numberLastClicked-1].GetComponent<PatientScript>().selectedAlready = true;
                        medicScript.task3Chosen = true;
                        medicScript.buttonText[2].text = numberLastClicked + "";
                        medicScript.durationC = 60f;
                        textBoxes[12].gameObject.SetActive(false);
                        if (!medicScript.awaitingSchedule)
                        {
                            patientCast[numberLastClicked - 1].GetComponent<PatientScript>().selectedAlready = false;
                            textBoxes[12].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        print("This one's already selected! 3/3");
                        textBoxes[12].gameObject.SetActive(true);
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
        //print("Play sound " + num);

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
        patientScriptInitial.timeC = patientScriptInitial.time;
        patientScriptInitial.isSupplyBox = isSupplyBox;
    }

    public void SetMedic(int num, int profession)
    {
        medicStaff[num].SetActive(true);
        medicSciptInitial = medicStaff[num].GetComponent<MedicScript>();
        medicSciptInitial.profession = profession;
    }
}
