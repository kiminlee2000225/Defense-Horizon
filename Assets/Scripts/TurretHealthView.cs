using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealthView : MonoBehaviour
{
    public int distToWall = 3;
    bool change;
    private Color startColor;
    private GameObject currWall;
    // Start is called before the first frame update
    void Start()
    {
        change = false;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distToWall) && !PauseMenuBehaviour.isGamePaused)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (currWall !=null && currWall != hit.transform.gameObject)
                {
                    currWall.gameObject.GetComponent<WallBehavior>().change = false;
                }
                currWall = hit.transform.gameObject;
                currWall.gameObject.GetComponent<WallBehavior>().change = true;
            } else
            {
                if (currWall != null)
                {
                    currWall.gameObject.GetComponent<WallBehavior>().change = false;
                }

            }
        }
    }

}
