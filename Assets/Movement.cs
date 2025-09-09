using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject scrap;
    public float moveSpeed = 3f;
    float rotationSpeed = 90f;
    void Start()
    {
        PlaceGameObjectAtRandomPosition(scrap);
    }

    // Update is called once per frame
    void Update()
    {

        float move = 0f;
        float rotate = 0f;

        if (Keyboard.current.wKey.isPressed)
            move = 1f;
        else if (Keyboard.current.sKey.isPressed)
            move = -1f;

        if (Keyboard.current.aKey.isPressed)
            rotate = -1f;
        else if (Keyboard.current.dKey.isPressed)
            rotate = 1f;

        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotate * rotationSpeed * Time.deltaTime);

        if (Keyboard.current.oKey.isPressed)
        {
            PlaceGameObjectAtRandomPosition(scrap);
        }
    }

    public void PlaceGameObjectAtRandomPosition(GameObject obj, float range = 5f)
    {
        if (obj == null) return;
        Vector3 randomPosition = new Vector3(
            Random.Range(-range, range),
            obj.transform.position.y,
            Random.Range(-range, range)
        );
        obj.transform.position = randomPosition;
    }
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "walls")
        {
            Debug.Log("fail");
        }
        else if (collision.gameObject.tag == "scrap")
        {
            Debug.Log("succes");
            PlaceGameObjectAtRandomPosition(scrap);
        }

    }
}
