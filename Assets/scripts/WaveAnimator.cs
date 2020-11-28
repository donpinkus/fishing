using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnimator : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public Rigidbody2D rb;

    public float lessM;
    public float waveForce;

    private SpriteRenderer m_SpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = wave1.GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        m_SpriteRenderer.color = new Color(1f,1f,1f,.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        // Make one of the waves rock back and forth
        if (rb.velocity.magnitude < lessM) {
            if (rb.velocity.x < 0) {
                rb.AddForce(new Vector2(waveForce, 0));
            } else {
                rb.AddForce(new Vector2(-waveForce, 0));
            }
            
        }
    }
}
