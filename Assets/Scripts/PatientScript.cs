using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PatientScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int patientNumber = 0, scoreValue = 10;
    public float time = 10f;
    public int ailment = 0; //0 is poiso, 1 is CPR, 2 is surgery.
    public TMP_Text[] uIInformation = new TMP_Text[4];
    public MainScript mainScript;
    public InputActionAsset inputAsset;

    //public Vector2 screenPos;
    public Vector2 worldPos;
    public bool isSupplyBox = false;
    public bool selectedAlready = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uIInformation[0].gameObject.SetActive(false);
        uIInformation[1].gameObject.SetActive(false);
        uIInformation[2].gameObject.SetActive(false);
        //screenPos = Input.mousePosition;
        //screenPos = Mouse.current.position;
        uIInformation[3].gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint((inputAsset.FindAction("Point").ReadValue<Vector2>()));
        if (Vector2.Distance(worldPos, rb.transform.position) <= 1f && inputAsset.FindAction("Click").WasReleasedThisFrame())
        {
            OnMouseUpMethod();
        }
        if (isSupplyBox)
        {
            uIInformation[3].text = "Have:";
        } else
        {
            uIInformation[3].text = "Need:";
        }

        if (time <= 1f)
        {
            mainScript.LoseScore(scoreValue);
            mainScript.PlaySoundEffect(3);
            rb.transform.position = new Vector2(0f, -20f);
            rb.gameObject.SetActive(false);
        }
        if (time > 0f)
        {
            //print(Time.deltaTime);
            time -= 1f/(60f*4f);
            //time--;
        }
        if (Mathf.Floor(time % 60) < 10)
        {
            uIInformation[0].text = Mathf.Floor((time / 60f)) + ":0" + Mathf.Floor(time % 60);
        } else
        {
            uIInformation[0].text = Mathf.Floor((time / 60f)) + ":" + Mathf.Floor(time % 60);
        }

        switch(ailment)
        {
            case 1: uIInformation[1].text = "Medication A"; break;
            case 2: uIInformation[1].text = "Medication B"; break;
            case 3: uIInformation[1].text = "Medication AB"; break;
            case 4: uIInformation[1].text = "Surgery"; break;
            case 5: uIInformation[1].text = "Prosthetic"; break;
            case 0:
            default: uIInformation[1].text = "CPR"; break;
        }
            
        //print(inputAsset.FindAction("Point").ReadValue<Vector2>());
        //print(Vector2.Distance(worldPos, rb.transform.position));

        if (Vector2.Distance(worldPos, rb.transform.position) <= 1f)
        {
            OnMouseOverMethod();
            //if (inputAsset.FindAction("Click").triggered)
            //{
            //    OnMouseDownMethod();
            //}
        }
        else
        {
            if (uIInformation[0].gameObject.active)
            {
                OnMouseExitMethod();
            }
        }
        if (isSupplyBox)
        {
            rb.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        } else
        {
            rb.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    void FixedUpdate()
    {
        
    }

    public void OnMouseUpMethod()
    {
        if (mainScript != null)
        {
            if (!isSupplyBox && (mainScript.medicalSupplies[ailment] <= 0)) {//This needs to be fixed.
                //Not enough supplies.
                //print("SupplyBox should be false " + isSupplyBox);
                mainScript.textBoxes[11].gameObject.SetActive(true);

                
            } else
            {
                //print("SupplyBox should be true " + isSupplyBox);
                mainScript.numberLastClicked = patientNumber;
                mainScript.textBoxes[11].gameObject.SetActive(false);
            }
                
        }
        //mainScript.numberLastClicked = patientNumber;
    }
    public void OnMouseOverMethod()
    {
        //print("Mouse over Patient #"+patientNumber);
        uIInformation[0].gameObject.SetActive(true);
        uIInformation[1].gameObject.SetActive(true);
        uIInformation[2].gameObject.SetActive(true);
        uIInformation[3].gameObject.SetActive(true);
    }
    public void OnMouseExitMethod()
    {
        //print("Mouse exited Patient #" + patientNumber);
        uIInformation[0].gameObject.SetActive(false);
        uIInformation[1].gameObject.SetActive(false);
        uIInformation[2].gameObject.SetActive(false);
        uIInformation[3].gameObject.SetActive(false);
    }
}
