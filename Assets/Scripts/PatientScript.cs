using UnityEngine;
using TMPro;

public RigidBody rb;
public int patientNumber, scoreValue = 10;
public float time = 0f;
public int ailment = 0; //0 is poiso, 1 is CPR, 2 is surgery.
public TMP_Text[] uIInformation = new TMP_Text[4];
public MainScript mainScript;



public class PatientScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uIInformation[0].setactive(false);
        uIInformation[1].setactive(false);
        uIInformation[2].setactive(false);
        uIInformation[3].setactive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (time <= 0f)
        {
            mainScript.LoseScore(scoreValue);
        }
    }

    public void OnMouseOver()
    {
        uIInformation[0].setactive(true);
        uIInformation[1].setactive(true);
        uIInformation[2].setactive(true);
        uIInformation[3].setactive(true);
    }
    public void OnMouseExit()
    {
        uIInformation[0].setactive(false);
        uIInformation[1].setactive(false);
        uIInformation[2].setactive(false);
        uIInformation[3].setactive(false);
    }
}
