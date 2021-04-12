using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //private variables
    private Rigidbody rB;
    bool isRaining;
    bool isDry;
    bool isSnowing;
    bool haveSeeds;
    bool seedsPlanted;
    bool plantGrown;
    bool havePlant;
    bool generatorOn;
    bool leverPulled;
    bool windmillOn;
    bool hasFuze;
    bool fuzeCharged;
    bool plugIn;
    bool bridgeUp;

    //public variables
    //[SerializeField] Camera playerCam;
    [SerializeField] Collider riverCollider;
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject plank;
    [SerializeField] GameObject plant;
    public static CursorLockMode lockState;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        isDry = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //called last every frame
    private void FixedUpdate()
    {
        //detect mouse input
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse clicked!");

            //send out a raycast to detect collisions
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                //check the object interacted with
                GameObject interactedObject = interactionInfo.collider.gameObject;

                //rune interactions
                if (interactedObject.tag == "RuneRain")
                {
                    isRaining = true;
                    isDry = false;
                    isSnowing = false;
                    plank.gameObject.SetActive(true);
                    riverCollider.gameObject.SetActive(false);
                    Debug.Log("It's raining!");
                }
                else if (interactedObject.tag == "RuneDry")
                {
                    isRaining = false;
                    isDry = true;
                    isSnowing = false;
                    plank.gameObject.SetActive(false);
                    riverCollider.gameObject.SetActive(false);
                    Debug.Log("It's dry!");
                }
                else if (interactedObject.tag == "RuneSnow")
                {
                    isRaining = false;
                    isDry = false;
                    isSnowing = true;
                    plank.gameObject.SetActive(false);
                    riverCollider.gameObject.SetActive(true);
                    Debug.Log("It's snowing!");
                }
                else
                {
                    //audioSource.PlayOneShot(pop, 0.2F);
                    Debug.Log("No object!");
                }

                //windmill sequence
                if(interactedObject.tag == "Plug" && isSnowing)
                {
                    plugIn = true;
                    Debug.Log(plugIn);
                }
                else if(plugIn == true && isDry == true)
                {
                    fuzeCharged = true;
                }
                else if(interactedObject.tag == "Plug" && fuzeCharged == true)
                {
                    hasFuze = true;
                    Debug.Log(hasFuze);
                }

                //plant sequence of events
                if (interactedObject.tag == "Seeds")
                {
                    haveSeeds = true;
                    Debug.Log(haveSeeds);
                }
                else if(interactedObject.tag == "Soil" && haveSeeds == true)
                {
                    seedsPlanted = true;
                    Debug.Log(seedsPlanted);
                }
                else if (seedsPlanted == true && isRaining == true)
                {
                    plantGrown = true;
                    plant.gameObject.SetActive(true);
                    Debug.Log(plantGrown);
                }
                else if (interactedObject.tag == "Plant" && plantGrown == true)
                {
                    havePlant = true;
                    Debug.Log(havePlant);
                }
                else if (havePlant == true && interactedObject.tag == "Generator")
                {
                    generatorOn = true;
                    Debug.Log(generatorOn);
                }

                //puddle sequence
                if(interactedObject.tag == "Lever" && generatorOn == true && isDry)
                {
                    bridgeUp = true;
                    Debug.Log(bridgeUp);
                    bridge.gameObject.SetActive(true);
                }
            }
        }
    }
}
