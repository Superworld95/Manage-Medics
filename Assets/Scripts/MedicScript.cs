using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;



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
    public TMP_Text[] uIInformation = new TMP_Text[4];
    public float travelSpeed = 0.01f;
    public PatientScript patientScript;
    public QueueNodeScript queueNodeScript, queueNodeScript2;

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
            OnMouseOverMethod();
            if (inputAsset.FindAction("Click").WasReleasedThisFrame())
            {
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clickedOn && awaitingSchedule)
        {
            if (task3Chosen)
            {
                uIInformation[0].gameObject.SetActive(true);
                uIInformation[1].gameObject.SetActive(true);
                uIInformation[2].gameObject.SetActive(true);
                uIInformation[3].gameObject.SetActive(true);
                uIInformation[4].gameObject.SetActive(true);
                uIInformation[5].gameObject.SetActive(true);
            }
            else
            {
                uIInformation[6].gameObject.SetActive(true);
                //Show schedule planner.
            }
                        
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

        


        if (!awaitingSchedule)
        {
            if(nodes[nodeCount].GetComponent<QueueNodeScript>() != null)
            {
                queueNodeScript = nodes[nodeCount].GetComponent<QueueNodeScript>();
            }            
            if (queueNodeScript != null)
            {
                if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.1f && !queueNodeScript.isOccupied)
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

                }

                if (nodes[nodeCount+1].GetComponent<QueueNodeScript>() != null)
                {
                    queueNodeScript2 = nodes[nodeCount+1].GetComponent<QueueNodeScript>();
                }
                if (queueNodeScript != null && queueNodeScript2 != null)
                {
                    if (!queueNodeScript2.isOccupied && !queueNodeScript.isOccupied && Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) <= 1f)
                    {
                        nodeCount++;
                    }
                }
            }
            else
            {
                if (!task3Chosen)
                {

                } else if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.1f)
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

                }

            }


            //switch (Random.Range(0, 8))
            //{
            //    case 0: doingWrongOperation = true;
            //        break;
            //        default: break;
            //}

            if (patientScript != null) {
                if (duration <= 0f || patientScript.time <= 0f)
                {
                    isWorking = false;
                    nodeCount++;
                    taskToDo++;
                }
            }

            

            if (isWorking)
            {
                duration = 500f;
                if (patientScript.ailment != profession)
                {
                    durationAmount = 5f;
                }
                else
                {
                    durationAmount = 15f;
                }
                duration -= durationAmount;
            }
            uIInformation[4].text = duration + " seconds";//Format this into mins.
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Patient")
        {
            isWorking = true;
            patientScript = collision.gameObject.GetComponent<PatientScript>();
        }
        if (collision.gameObject.tag == "Node")
        {
            nodeCount++;
        }
    }
}
