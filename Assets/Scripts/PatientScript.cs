using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PatientScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int patientNumber, scoreValue = 10;
    public float time = 10f;
    public int ailment = 0; //0 is poiso, 1 is CPR, 2 is surgery.
    public TMP_Text[] uIInformation = new TMP_Text[4];
    public MainScript mainScript;
    public InputActionAsset inputAsset;

    //public Vector2 screenPos;
    public Vector2 worldPos;

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

        if (time <= 1f)
        {
            mainScript.LoseScore(scoreValue);
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
    }
    void FixedUpdate()
    {
        
    }

    public void OnMouseUpMethod()
    {
        if (mainScript != null)
        {
            mainScript.numberLastClicked = patientNumber;
        }
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
