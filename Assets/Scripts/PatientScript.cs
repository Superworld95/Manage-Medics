using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PatientScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int patientNumber, scoreValue = 10;
    public float time = 0f;
    public int ailment = 0; //0 is poiso, 1 is CPR, 2 is surgery.
    public TMP_Text[] uIInformation = new TMP_Text[4];
    public MainScript mainScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uIInformation[0].gameObject.SetActive(false);
        uIInformation[1].gameObject.SetActive(false);
        uIInformation[2].gameObject.SetActive(false);
        uIInformation[3].gameObject.SetActive(false);
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
        uIInformation[0].gameObject.SetActive(true);
        uIInformation[1].gameObject.SetActive(true);
        uIInformation[2].gameObject.SetActive(true);
        uIInformation[3].gameObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        uIInformation[0].gameObject.SetActive(false);
        uIInformation[1].gameObject.SetActive(false);
        uIInformation[2].gameObject.SetActive(false);
        uIInformation[3].gameObject.SetActive(false);
    }
}
