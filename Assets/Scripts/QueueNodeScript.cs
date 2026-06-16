using UnityEngine;

public class QueueNodeScript : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public bool isOccupied = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Medic")
        {
            isOccupied = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOccupied = false;
    }
}
