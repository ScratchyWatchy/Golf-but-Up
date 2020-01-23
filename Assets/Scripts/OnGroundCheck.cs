using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public int colCounter = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(colCounter == 0)
        {
            gameObject.GetComponentInParent<TouchLaunch>().isGrounded = false;
        }
        else
        {
            gameObject.GetComponentInParent<TouchLaunch>().isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colCounter++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        colCounter--;
    }
}
