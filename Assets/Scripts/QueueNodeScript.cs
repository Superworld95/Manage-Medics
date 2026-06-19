using UnityEngine;

public class QueueNodeScript : MonoBehaviour
{
    //public Rigidbody2D body;
    public Transform nodeTransform;
    BoxCollider2D boxCollider;
    public bool isOccupied = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        nodeTransform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Medic" /*&& Vector2.Distance(nodeTransform.position, collision.gameObject.transform.position) <= 0.01f*/)
        {
            isOccupied = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOccupied = false;
    }
}
