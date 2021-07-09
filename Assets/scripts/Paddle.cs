using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 2f; // in units
    [SerializeField] float maxX = 14f;  // in units

    // cached references
    // cached references
    GameSession theGameSession;
    Ball theBall;   // cache kar rahe hain taaki update mei baar baar FindObjectOftype call na ho because that is stressful for the game

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        // float getmousePos  = Input.mousePosition.x / Screen.width * screenWidthInUnits; // yeh tab use kar rahe the jab auto play add nahi kiya tha
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        // paddlePos.x = Mathf.Clamp(getmousePos, minX, maxX); // yeh bhi tab  use kar rahe the jab autoplay vaali setting nahi daali thi
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }
    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())      // agar autoplay on hai to paddle ki x pos will be equal to ball ki x pos therefore jahaan ball vahaan paddle hence game apne aap chalegi
        {                                                             // used for game testing
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;   // agar autoplay off hai toh mouse position return hogi
        }

    }
}
