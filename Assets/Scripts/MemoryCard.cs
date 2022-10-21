using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    public int identifier;

    public float targetHeight = 0.01f;
    public float targetRotation = -90;

    public void OnMouseDown()                                                                                   // OnMouseDown braucht einen Collider!
    {
        FindObjectOfType<GameManager>().CardClicked(this);                                                      // this = Objekt ist eine Karte auf der ein Collider und über das OnMouseDown registriert wird
    }

    private void Update()
    {
        // Move up/down
        float heightValue = Mathf.MoveTowards(transform.position.y, targetHeight, 0.5f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, heightValue, transform.position.z);

        // Rotate X
        Quaternion rotationValue = Quaternion.Euler(targetRotation, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationValue, 10 * Time.deltaTime);
    }
}
