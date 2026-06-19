using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading.Tasks;



public class MedicScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] nodes = new Transform[10];
    public int nodeCount = 0;
    public int medicNumber = 0;
    public int profession = 0; //Matches the possible patient ailments.
    public float durationAmount = 10f;
    public float duration = 0f;
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
            }
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
            switch (destinations[taskToDo])
            {
                case 0: break;
                case 3:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 9; break;
                    }
                    break;
                case 4:
                    switch (nodeCount)
                    {
                        case 7: nodeCount = 10; break;
                    }
                    break;
            }
        } else if (nodeCount == 22)
        {
            task1Chosen = false;
            task2Chosen = false;
            task3Chosen = false;
            buttonText[0].text = "?";
            buttonText[1].text = "?";
            buttonText[2].text = "?";
        }

        



    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (nodeCount > nodes.Length-1)
        {
            nodeCount = 0;
        }

        if (clickedOn && awaitingSchedule)
        {
            //print("This should display.");

            uIInformation[4].gameObject.SetActive(true);
            uIInformation[5].gameObject.SetActive(true);
            uIInformation[6].gameObject.SetActive(true);

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

            //if (task1Chosen && task2Chosen && task3Chosen)
            //{
            //    //print("This should display.");
            //    uIInformation[0].gameObject.SetActive(true);
            //    uIInformation[1].gameObject.SetActive(true);
            //    uIInformation[2].gameObject.SetActive(true);
            //    uIInformation[3].gameObject.SetActive(true);
            //    uIInformation[4].gameObject.SetActive(true);
            //    uIInformation[5].gameObject.SetActive(true);
            //    uIInformation[6].gameObject.SetActive(false);
            //    //if (task1Chosen && task2Chosen && task3Chosen)
            //    //{
            //    //    uIInformation[6].gameObject.SetActive(false);
            //    //    awaitingSchedule = false;
            //    //    print("Not awaiting schedule now.");
            //    //}
            //}
            //else
            //{
            //    //print("This should display.");
            //    uIInformation[6].gameObject.SetActive(true);

            //    uIInformation[0].gameObject.SetActive(false);
            //    uIInformation[1].gameObject.SetActive(false);
            //    uIInformation[2].gameObject.SetActive(false);
            //    uIInformation[3].gameObject.SetActive(false);
            //    uIInformation[4].gameObject.SetActive(false);
            //    uIInformation[5].gameObject.SetActive(false);

            //    if (awaitingSchedule)
            //    {
            //        awaitingSchedule = false;
            //        print("Not awaiting schedule now.");
            //    }

            //    //Show schedule planner.
            //}

            if (doingWrongOperation)
            {
                doingWrongOperation = false;
            }

        }
        else if (clickedOn && !awaitingSchedule) {
            //uIInformation[4].gameObject.SetActive(true);

            //if (uIInformation[6].gameObject)
            //{
            //    uIInformation[6].gameObject.SetActive(false);
            //} else
            //{
            //    uIInformation[6].gameObject.SetActive(true);
            //}

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

        if (queueNodeScript != null)
        {
            if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.05f && !queueNodeScript.isOccupied)
            {
                if (rb.transform.position.x > nodes[nodeCount].transform.position.x + 0.5f)
                {
                    rb.transform.position += new Vector3(-travelSpeed, 0f, 0f);
                    //print("Moving along -x");
                }
                else if (rb.transform.position.x < nodes[nodeCount].transform.position.x - 0.5f)
                {
                    rb.transform.position += new Vector3(travelSpeed, 0f, 0f);
                    //print("Moving along x");
                }
                if (rb.transform.position.y > nodes[nodeCount].transform.position.y + 0.5f)
                {
                    rb.transform.position += new Vector3(0f, -travelSpeed, 0f);
                    //print("Moving along -y");
                }
                else if (rb.transform.position.y < nodes[nodeCount].transform.position.y - 0.5f)
                {
                    rb.transform.position += new Vector3(0f, travelSpeed, 0f);
                    //print("Moving along y");
                }
                //awaitingSchedule = false;
            } else if (!(task1Chosen && task2Chosen && task3Chosen) && nodeCount < 21)
            {
                awaitingSchedule = true;
            }

            if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 0.05f)
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
        else if (!awaitingSchedule && task1Chosen && task2Chosen && task3Chosen)
        {
            if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.05f)
            {
                print("Once all tasks are chosen, this should execute.");

                if (rb.transform.position.x > nodes[nodeCount].transform.position.x + 0.5f)
                {
                    rb.transform.position += new Vector3(-travelSpeed, 0f, 0f);
                    //print("Moving along -x");
                }
                else if (rb.transform.position.x < nodes[nodeCount].transform.position.x - 0.5f)
                {
                    rb.transform.position += new Vector3(travelSpeed, 0f, 0f);
                    //print("Moving along x");
                }
                if (rb.transform.position.y > nodes[nodeCount].transform.position.y + 0.5f)
                {
                    rb.transform.position += new Vector3(0f, -travelSpeed, 0f);
                    //print("Moving along -y");
                }
                else if (rb.transform.position.y < nodes[nodeCount].transform.position.y - 0.5f)
                {
                    rb.transform.position += new Vector3(0f, travelSpeed, 0f);
                    //print("Moving along y");
                }
                //awaitingSchedule = false;
            }

        }
        else
        {
            awaitingSchedule = true;
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
                nodeCount++;
                
                taskToDo++;
                patientScript.gameObject.SetActive(false);
                patientScript = null;

                if (duration <= 0)
                {
                    mainScript.score += 10;
                } else if (patientScript.time <= 0f)
                {
                    mainScript.score -= 10;
                    mainScript.health--;
                }

                duration = 10f; //CHANGE THIS TO CALL THE DURATION.
            }
        }
        if (isWorking && duration <= 0f)
        {
            duration = 500f;
        }

        if (isWorking && duration >= 0f)
        {
            if (patientScript.ailment != profession)
            {
                durationAmount = 1f / 60f;
            }
            else
            {
                durationAmount = 5f / 60f;
            }
            duration -= durationAmount;
        }
        //uIInformation[4].text = duration + " seconds";//Format this into mins.
        if (isWorking)
        {
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

    public void OnMouseUpMethod()
    {
        clickedOn = true;
        //print("Mouse clicked on Medic #" +medicNumber);
    }
    public void OnMouseOverMethod()
    {
        //Some kind of indication that you can click
        //print("Mouse over Medic #" +medicNumber);
    }
    public void OnMouseExitMethod()
    {
        clickedOn = false;
        //print("Mouse exited Medic #" + medicNumber);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Patient" && Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 1f && taskToDo<3)
        {
            isWorking = true;
            patientScript = collision.gameObject.GetComponent<PatientScript>();
            print("Medic is working!");
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Node" && Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 1f)
        {
            nodeCount++;
        }
    }

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
}
