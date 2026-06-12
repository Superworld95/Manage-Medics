using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MedicScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] nodes = new Transform[10];
    public int nodeCount = 0;
    public int profession = 0; //Matches the possible patient ailments.
    public float durationAmount = 10f;
    public float duration = 0f;
    public int[] destinations = new int[3]; //1 to 8.
    public bool task1Chosen = false, task2Chosen = false, task3Chosen = false;
    public int taskToDo = 0;
    public bool awaitingSchedule = true;
    public bool clickedOn = false, doingWrongOperation = false, isWorking = false;
    public TMP_Text[] uIInformation = new TMP_Text[4];
    public float travelSpeed = 0.1f;
    public PatientScript patientScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clickedOn && awaitingSchedule)
        {
            uIInformation[0].gameObject.SetActive(true);
            uIInformation[1].gameObject.SetActive(true);
            uIInformation[2].gameObject.SetActive(true);
            uIInformation[3].gameObject.SetActive(true);
            if (doingWrongOperation)
            {
                doingWrongOperation = false;
            }

        }
        else if (clickedOn && !awaitingSchedule) {
            uIInformation[4].gameObject.SetActive(true);
        } else
        {
            uIInformation[0].gameObject.SetActive(false);
            uIInformation[1].gameObject.SetActive(false);
            uIInformation[2].gameObject.SetActive(false);
            uIInformation[3].gameObject.SetActive(true);
            uIInformation[4].gameObject.SetActive(true);
        }

        if (!awaitingSchedule)
        {
            if (Vector2.Distance(rb.transform.position, nodes[nodeCount].transform.position) >= 0.1f)
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

        //switch (Random.Range(0, 8))
        //{
        //    case 0: doingWrongOperation = true;
        //        break;
        //        default: break;
        //}


        if (duration <= 0f || patientScript.time <= 0f)
            {
                isWorking = false;
                nodeCount++;
                taskToDo++;
            }

        if (isWorking)
            {
                duration = 500f;
                if (patientScript.ailment != profession)
                {
                    durationAmount = 5f;
                } else
                {
                    durationAmount = 15f;
                }
                duration -= durationAmount;
            }
        uIInformation[4].text = duration + " seconds";//Format this into mins.
    }

    void OnMouseDown()
    {
        clickedOn = true;
    }
    void OnMouseEnter()
    {
        //Some kind of indication that you can click.
    }
    void OnMouseExit()
    {
        clickedOn = false;
    }

    void OnTriggerEnter2D(Collider collision)
    {
        if (collision.gameObject.tag == "Patient")
        {
            isWorking = true;
            patientScript = collision.gameObject.GetComponent<PatientScript>();
        }
    }
}}
