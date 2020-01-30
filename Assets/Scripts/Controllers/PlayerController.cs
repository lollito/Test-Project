using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{



    public delegate void OnFocusChanged(Interactable newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public Interactable focus;	// Our current focus: Item, Enemy etc.

    public float rangeAttack = 15f;

    public GameObject markerPrefab;
    private GameObject marker;
    private bool aiming;

    private PlayerMotor motor;
    private CharacterAnimator characterAnimator;



    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        characterAnimator = GetComponent<CharacterAnimator>();
        marker = Instantiate(markerPrefab) as GameObject;
        marker.SetActive(false);
    }

    void Update()
    {
        //scommentare quanto si aggiunge UI
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            aiming = true;
            marker.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            aiming = false;
            marker.SetActive(false);
        }
        if (aiming) {
            if (Physics.Raycast(ray, out hit, 100))
            {

                marker.SetActive(true);
                marker.transform.position = hit.point;

            }
            else {
                marker.SetActive(false);
            }
        }
        if (Input.GetMouseButton(1))
        {
            //MOVE
           // if (!characterAnimator.isOnAttack()) {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (!(hit.transform.gameObject.tag == "Player"))
                    {
                        SetFocus(null);
                        characterAnimator.disableAttack();
                        motor.MoveToPoint(hit.point);
                       
                    }
                   
                }
          //  }
           
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //ATTACK
            if (Physics.Raycast(ray, out hit, 100))
            {
                float distance = Vector3.Distance(hit.transform.position, transform.position);
               // if (hit.transform.gameObject.tag == "Enemy")
                 if (true)
                    {
                    
                    if (distance <= rangeAttack)
                    {
                        SetFocus(hit.collider.GetComponent<Interactable>());

                    }
                    
                }
                


            }
        }

        
    }


    // Set our focus to a new focus
    void SetFocus(Interactable newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
        }

        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(transform);
        }

    }
}
