using UnityEngine;
using TMPro;

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


public class MedicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clickedOn && awaitingSchedule)
        {
            uIInformation[0].setactive(true);
            uIInformation[1].setactive(true);
            uIInformation[2].setactive(true);
            uIInformation[3].setactive(true);
            if (doingWrongOperation)
            {
                doingWrongOperation = false;
            }

        }
        else if (clickedOn && !awaitingSchedule) {
            uIInformation[4].setactive(true);
        } else
        {
            uIInformation[0].setactive(false);
            uIInformation[1].setactive(false);
            uIInformation[2].setactive(false);
            uIInformation[3].setactive(false);
            uIInformation[4].setactive(true);
        }

        if (!awaitingSchedule)
        {
            if (Vector2.Distance(rb.transform.position, node[nodeCount].transform.position) >= 0.1f)
            {
                if (rb.transform.position.x > node[nodeCount].transform.position.x + 0.5f)
                {
                    rb.transform.position += new Vector3(-travelSpeed, 0f, 0f);
                    //print("Moving along -x");
                }
                else if (rb.transform.position.x < node[nodeCount].transform.position.x - 0.5f)
                {
                    rb.transform.position += new Vector3(travelSpeed, 0f, 0f);
                    //print("Moving along x");
                }
                if (rb.transform.position.y > node[nodeCount].transform.position.y + 0.5f)
                {
                    rb.transform.position += new Vector3(0f, -travelSpeed, 0f);
                    //print("Moving along -y");
                }
                else if (rb.transform.position.y < node[nodeCount].transform.position.y - 0.5f)
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
                if (paitentScript.ailment != profession)
                {
                    durationAmount = 5f;
                } else
                {
                    durationAmount = 15f;
                }
                duration -= durationAmount;
            }
        textBoxes[4].text = duration + " seconds";//Format this into mins.
    }

    public void OnMouseDown()
    {
        clickedOn = true;
    }
    public void OnMouseEnter()
    {
        //Some kind of indication that you can click.
    }
    public void OnMouseExit()
    {
        clickedOn = false;
    }

    public void onTriggerEnter2D(Collision collision)
    {
        if (collision.gameObject.tag = "Patient")
        {
            isWorking = true;
            patientScript = collision.gameObject.PatientScript;
        }
    }
}
