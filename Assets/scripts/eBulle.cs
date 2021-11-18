using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBulle : MonoBehaviour
{
    Rigidbody2D leBody;

    // Start is called before the first frame update
    void Start()
    {
        leBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        leBody.velocity = Vector2.zero;
        Destroy(this.gameObject);
    }
}
