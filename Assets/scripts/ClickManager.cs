using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Fire1")) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Casts a ray starting at mousePos2D, in the direction of Vector2.zero.
            // The default magnitude is infinte. 
            // This will catch objects only directly under the clicked point.
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // If there is a hit, hit will be the GameObject with hit.collider being its collider.
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);

                hit.collider.gameObject.SendMessage("HandleClick", mousePos2D);
            }
        }
    }
}
