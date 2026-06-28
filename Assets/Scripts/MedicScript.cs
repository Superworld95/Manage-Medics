using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;



public class MedicScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] nodes = new Transform[10];
    public int nodeCount = 0;
    public int medicNumber = 0;
    public int profession = 0; //Matches the possible patient ailments.
    public float durationAmount = 10f;
    public float duration = 0f, durationC = 20f;
    public int[] destinations = new int[3]; //1 to 8.
    public bool task1Chosen = false, task2Chosen = false, task3Chosen = false;
    public int taskToDo = 0;
    public bool awaitingSchedule = false;
    public bool clickedOn = false, doingWrongOperation = false, isWorking = false;
    public TMP_Text[] uIInformation = new TMP_Text[4], buttonText = new TMP_Text[3];
    float travelSpeed = 0.05f;
    public PatientScript patientScript;
    public QueueNodeScript queueNodeScript, queueNodeScript2;
    public MainScript mainScript;

    public BoxCollider2D boxCollider;
    public InputActionAsset inputAsset;

    public Vector2 worldPos;
    public GameObject particleEffect;
    public ParticleSystem particleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        uIInformation[0].gameObject.SetActive(false);
        uIInformation[1].gameObject.SetActive(false);
        uIInformation[2].gameObject.SetActive(false);
        uIInformation[3].gameObject.SetActive(false);
        uIInformation[4].gameObject.SetActive(false);
        uIInformation[5].gameObject.SetActive(false);
        uIInformation[6].gameObject.SetActive(false);
        particleSystem = particleEffect.GetComponent<ParticleSystem>();
        //uIInformation[7].gameObject.SetActive(false);
        particleEffect.SetActive(false);
    }

    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint((inputAsset.FindAction("Point").ReadValue<Vector2>()));
        if (Vector2.Distance(worldPos, rb.transform.position) <= 1f && !uIInformation[6].gameObject.active)
        {
            //print("Able to click on the medic.");
            OnMouseOverMethod();
            if (inputAsset.FindAction("Click").WasReleasedThisFrame())
            {
                //print("Clicked on the medic.");
                OnMouseUpMethod();
            } else { OnMouseOverMethod(); }
        }
        else if (uIInformation[6].gameObject.active /*&& Vector2.Distance(worldPos, uIInformation[6].gameObject.transform.position) <= 3f */)
        {
            if (inputAsset.FindAction("Click").WasReleasedThisFrame() && Vector2.Distance(worldPos, rb.transform.position) <= 1f)
            {
                //print("Close the tasks UI.");
                OnMouseExitMethod();
            }
        }
        else
        {
            if (uIInformation[0].gameObject)//active
            {
                OnMouseExitMethod();
            }

        }
        if (taskToDo < 3)
        {
            if (destinations[taskToDo] == 0 && (task1Chosen && task2Chosen && task3Chosen))
            {
                taskToDo++;
                print("Onto task #" + taskToDo);
            }
        }
        

        //Manual pathfinding.

        //Try fix this.

        //I think the idea for this doesn't work because it needs to be case-by-case.
        //if (nodeCount > destinations[taskToDo] + 6 && destinations[taskToDo] != 0)
        //{
        //    nodeCount = destinations[taskToDo] + 6;
        //}

        if (nodeCount == 1)
        {
            task1Chosen = false;
            task2Chosen = false;
            task3Chosen = false;
            buttonText[0].text = "?";
            buttonText[1].text = "?";
            buttonText[2].text = "?";
        } else if (taskToDo < 3 /*&& nodeCount > destinations[taskToDo] + 6*/)
        {
            switch (destinations[taskToDo])
            {
                case 0: break;
                case 2: break;
                case 3:
                case 4:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 9; break;
                            
                    }
                    break;
                //case 3:
                //    switch (nodeCount)
                //    {
                //        case 7: nodeCount = 9; break;
                //    }
                //    break;
                //case 4:
                //    switch (nodeCount)
                //    {
                //        case 7: nodeCount = 10; break;
                //    }
                //break;
                case 5:
                case 6:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 11; break;
                    }
                    break;
                case 7:
                case 8:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 11; break;
                    }
                    break;
                case 9:
                case 10:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 11; break;
                        case 12: nodeCount = 17; break;
                    }
                    break;
                case 11:
                case 12:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 11; break;
                        case 12: nodeCount = 19; break;
                    }
                    break;

            }
        }
        else if ((taskToDo == 2 && destinations[taskToDo] == 0) || taskToDo >= 3)
        {//Not working?
            print("Should be done and go for the end.");
            nodeCount = 21;
            taskToDo = 0;
        }     

        switch (profession)
        {
            case 1: uIInformation[5].text = "Medication A"; break;
            case 2: uIInformation[5].text = "Medication B"; break;
            case 3: uIInformation[5].text = "Medication AB"; break;
            case 4: uIInformation[5].text = "Surgery"; break;
            case 5: uIInformation[5].text = "Prosthetic"; break;
            case 0:
                default:
                uIInformation[5].text = "CPR";
                break;
        }



    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (nodeCount > nodes.Length - 1)
        {
            nodeCount = 0;
        }

        if (clickedOn && awaitingSchedule)
        {
            //print("This should display.");

            uIInformation[4].gameObject.SetActive(true);
            uIInformation[5].gameObject.SetActive(true);
            uIInformation[6].gameObject.SetActive(true);
            //uIInformation[7].gameObject.SetActive(true);

            uIInformation[0].gameObject.SetActive(false);
            uIInformation[1].gameObject.SetActive(false);
            uIInformation[2].gameObject.SetActive(false);
            uIInformation[3].gameObject.SetActive(false);
            //uIInformation[4].gameObject.SetActive(false);
            //uIInformation[5].gameObject.SetActive(false);

            if (task1Chosen && task2Chosen && task3Chosen)
            {
                if (awaitingSchedule)
                {
                    awaitingSchedule = false;
                    print("Not awaiting schedule now.");
                }
            }

            if (doingWrongOperation)
            {
                print("Doing wrong op! But not anymore!");
                doingWrongOperation = false;
                particleSystem.startColor = Color.green;
            }

        }
        else if (clickedOn && !awaitingSchedule) {

            uIInformation[6].gameObject.SetActive(false);

            uIInformation[0].gameObject.SetActive(true);
            uIInformation[1].gameObject.SetActive(true);
            uIInformation[2].gameObject.SetActive(true);
            uIInformation[3].gameObject.SetActive(true);
            uIInformation[4].gameObject.SetActive(true);
            uIInformation[5].gameObject.SetActive(true);



        } else
        {
            uIInformation[0].gameObject.SetActive(false);
            uIInformation[1].gameObject.SetActive(false);
            uIInformation[2].gameObject.SetActive(false);
            uIInformation[3].gameObject.SetActive(false);
            uIInformation[4].gameObject.SetActive(false);
            uIInformation[5].gameObject.SetActive(false);
            uIInformation[6].gameObject.SetActive(false);
            //Close other information
        }

        //queueNodeScript = null; queueNodeScript2 = null;

        if (!isWorking)
        {



            if (nodes[nodeCount].GetComponent<QueueNodeScript>() != null)
            {
                queueNodeScript = nodes[nodeCount].GetComponent<QueueNodeScript>();
            } else
            {
                queueNodeScript = null;
            }
            if (nodeCount < nodes.Length - 1)
            {
                if (nodes[nodeCount + 1].GetComponent<QueueNodeScript>() != null)
                {
                    queueNodeScript2 = nodes[nodeCount + 1].GetComponent<QueueNodeScript>();
                }
                else
                {
                    queueNodeScript2 = null;
                }
            }
            else
            {
                queueNodeScript2 = null;
            }

            //print(!awaitingSchedule && (task1Chosen || task2Chosen || task3Chosen));    
            //print(!(task1Chosen || task2Chosen || task3Chosen));
            //print("Task 1 chosen " + task1Chosen);
            //print("Task 2 chosen " + task2Chosen);
            //print("Task 3 chosen " + task3Chosen);

            if (queueNodeScript2 != null)
            {
                if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.05f && queueNodeScript2 != null && !queueNodeScript2.isOccupied)//THIS PART IS THE PROBLEM SO FAR.
                {
                    if (rb.transform.position.x > nodes[nodeCount].transform.position.x + 0.05f)
                    {
                        rb.transform.position += new Vector3(-travelSpeed, 0f, 0f);
                        //print("Moving along -x");
                    }
                    else if (rb.transform.position.x < nodes[nodeCount].transform.position.x - 0.05f)
                    {
                        rb.transform.position += new Vector3(travelSpeed, 0f, 0f);
                        //print("Moving along x");
                    }
                    if (rb.transform.position.y > nodes[nodeCount].transform.position.y + 0.05f)
                    {
                        rb.transform.position += new Vector3(0f, -travelSpeed, 0f);
                        //print("Moving along -y");
                    }
                    else if (rb.transform.position.y < nodes[nodeCount].transform.position.y - 0.05f)
                    {
                        rb.transform.position += new Vector3(0f, travelSpeed, 0f);
                        //print("Moving along y");
                    }
                    //awaitingSchedule = false;
                }
                if (!(task1Chosen || task2Chosen || task3Chosen) && nodeCount <= 4)
                {
                    //print("This is activating?");
                    //print(!awaitingSchedule && !(task1Chosen || task2Chosen || task3Chosen));
                    //print(!awaitingSchedule);
                    //print((task1Chosen || task2Chosen || task3Chosen));
                    //print("Task 1 chosen2 " + task1Chosen);
                    //print("Task 2 chosen2 " + task2Chosen);
                    //print("Task 3 chosen2 " + task3Chosen);
                    awaitingSchedule = true;
                }

                if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 0.5f)
                {
                    nodeCount++;
                }

                //if (queueNodeScript != null)
                //{
                //    if (!queueNodeScript2.isOccupied && !queueNodeScript.isOccupied && Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 0.05f)
                //    {
                //        nodeCount++;
                //    }
                //}
            }
            else if (!awaitingSchedule && (task1Chosen || task2Chosen || task3Chosen))
            {
                if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.05f)
                {
                    //print("Once all (or some) tasks are chosen, this should execute.");

                    if (rb.transform.position.x > nodes[nodeCount].transform.position.x + 0.05f)
                    {
                        rb.transform.position += new Vector3(-travelSpeed, 0f, 0f);
                        //print("Moving along -x");
                    }
                    else if (rb.transform.position.x < nodes[nodeCount].transform.position.x - 0.05f)
                    {
                        rb.transform.position += new Vector3(travelSpeed, 0f, 0f);
                        //print("Moving along x");
                    }
                    if (rb.transform.position.y > nodes[nodeCount].transform.position.y + 0.05f)
                    {
                        rb.transform.position += new Vector3(0f, -travelSpeed, 0f);
                        //print("Moving along -y");
                    }
                    else if (rb.transform.position.y < nodes[nodeCount].transform.position.y - 0.05f)
                    {
                        rb.transform.position += new Vector3(0f, travelSpeed, 0f);
                        //print("Moving along y");
                    }
                    //awaitingSchedule = false;
                }

            }
            else //if(!(task1Chosen && task2Chosen && task3Chosen) && nodeCount <= 4)
            {
                //awaitingSchedule = true;
            }


            //switch (Random.Range(0, 8))
            //{
            //    case 0: doingWrongOperation = true;
            //        break;
            //        default: break;
            //}


        }
        if (patientScript != null)
        {
            if (duration <= 0f || patientScript.time <= 0f)
            {
                //print("Duration: " + duration+" patientTime: "+ patientScript.time);
                isWorking = false;
                particleEffect.SetActive(false);
                uIInformation[0].text = (taskToDo + 1) + "/" + 3 + " Done!";
                if (destinations[1] != 0 || destinations[2] != 0)
                {
                    nodeCount = 6;
                }
                



                if (patientScript != null)
                {
                    if (patientScript.time <= 0)
                    {
                        if (!patientScript.isSupplyBox)
                        {
                            mainScript.score -= 10;
                            mainScript.health--;
                            mainScript.PlaySoundEffect(3);                            
                        }
                        nodeCount++;
                    }
                    else
                    {

                        if (patientScript.isSupplyBox)
                        {
                            mainScript.medicalSupplies[patientScript.ailment]++;
                        }
                        else
                        {
                            mainScript.score += 10;
                            mainScript.PlaySoundEffect(4);
                            switch (patientScript.ailment)
                            {
                                case 0: break;
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    print("Supply reduced!");
                                    mainScript.medicalSupplies[patientScript.ailment]--;
                                    break;
                                    //(switch (patientScript.ailment))
                                    //{
                                    //    mainScript.medicalSupplies[patientScript.ailment]--;
                                    //}

                            }
                        }
                        patientScript.selectedAlready = false;
                        if (durationC > 0)
                        {
                            durationC -= 5f;
                        }
                        duration = durationC;
                    }
                    taskToDo++;
                    print("Onto task #"+ taskToDo);
                    patientScript.gameObject.SetActive(false);
                    patientScript = null;
                }
            }
            if (isWorking && duration <= 0f)
            {
                duration = 60f;
            }

            if (isWorking && duration >= 0f)
            {
                if (patientScript.ailment != profession)
                {
                    if (doingWrongOperation)
                    {
                        durationAmount = 1f / 60f;
                    } else
                    {
                        durationAmount = 3f / 60f;
                    }
                        
                }
                else
                {
                    if (doingWrongOperation)
                    {
                        durationAmount = 4f / 60f;
                    }
                    else
                    {
                        durationAmount = 5f / 60f;
                    }
                }
                duration -= durationAmount;
            }
            //uIInformation[4].text = duration + " seconds";//Format this into mins.
            if (isWorking)
            {
                //print("duration: " + duration + ", duration rounded down mod 10: " + Mathf.Floor(duration) % 10);

                if (Mathf.Floor(duration) % 10 == 0)
                {
                    //print("Sound should be playing");
                    switch (patientScript.ailment)
                    {
                        case 0: mainScript.PlaySoundEffect(0); break;
                        case 1: case 2: case 3: mainScript.PlaySoundEffect(1); break;
                        case 4: mainScript.PlaySoundEffect(2); break;
                    }
                }


                if (Mathf.Floor(duration % 60) < 10)
                {
                    uIInformation[0].text = Mathf.Floor((duration / 60f)) + ":0" + Mathf.Floor(duration % 60);
                }
                else
                {
                    uIInformation[0].text = Mathf.Floor((duration / 60f)) + ":" + Mathf.Floor(duration % 60);
                }
            }


        }
    }

    public void OnMouseUpMethod()
    {
        clickedOn = true;
        //print("Mouse clicked on Medic #" +medicNumber);
    }
    public void OnMouseOverMethod()
    {
        //Some kind of indication that you can click
        //print("Mouse over Medic #" +medicNumber);
        if (!awaitingSchedule)
        {
            uIInformation[6].gameObject.SetActive(false);

            uIInformation[0].gameObject.SetActive(true);
            uIInformation[1].gameObject.SetActive(true);
            uIInformation[2].gameObject.SetActive(true);
            uIInformation[3].gameObject.SetActive(true);
            uIInformation[4].gameObject.SetActive(true);
            uIInformation[5].gameObject.SetActive(true);
        }
    }
    public void OnMouseExitMethod()
    {
        clickedOn = false;
        //print("Mouse exited Medic #" + medicNumber);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node" && Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 0.65f)
        {
            nodeCount++;
        }

        //print("The position of the destination node is: "+nodes[destinations[taskToDo] + 7].transform.position);//This doesn't work since the queue is not linear.

        if (collision.gameObject.tag == "Patient" && taskToDo < 3)//This code is wrong. It shouldn't be searching for nodes[nodeCount] at all.
        {
            patientScript = collision.gameObject.GetComponent<PatientScript>();
            if (patientScript.patientNumber == destinations[taskToDo])
            {
                //if (Vector2.Distance(rb.transform.position, patientScript.transform.position) <= 0.65f)

                //print(Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position));
                isWorking = true;
                particleEffect.SetActive(true);
                print("Medic is working! " + patientScript.patientNumber);
                //Set the particle effects HERE.
                if (Random.Range(0, 7) == 0)
                {
                    doingWrongOperation = true;
                    particleSystem.startColor = Color.red;
                } else
                {
                    doingWrongOperation = false;
                    particleSystem.startColor = Color.green;
                }

            }
            
        }

    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{

        
    //}

    public void Button1Pressed()
    {
        mainScript.taskChosen = 1;
        mainScript.medicScript = this.GetComponent<MedicScript>();
        mainScript.medicScriptTaskNumber = 1;
        //gameManager.numberLastClicked = 1;
    }
    public void Button2Pressed()
    {
        mainScript.taskChosen = 2;
        mainScript.medicScript = this.GetComponent<MedicScript>();
        mainScript.medicScriptTaskNumber = 2;
    }
    public void Button3Pressed()
    {
        mainScript.taskChosen = 3;
        mainScript.medicScript = this.GetComponent<MedicScript>();
        mainScript.medicScriptTaskNumber = 3;
    }

    public void ButtonDonePressed()
    {
        if (task1Chosen || task2Chosen || task3Chosen)
        {
            task1Chosen = true;
            task2Chosen = true;
            task3Chosen = true;
        }
    }
}
