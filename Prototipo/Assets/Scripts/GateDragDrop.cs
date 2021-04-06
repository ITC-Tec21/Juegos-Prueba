using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDragDrop : MonoBehaviour
{
    public Collider2D inputCollider, outputCollider;

    private bool on;
    // Empty object that groups all the slots as children. Must be asigned in Unity
    public GameObject slotGroup;

    // Slot this gate is currently in (null if it isn't in any slot)
    public GameObject currentSlot;

    // Object that hold the initial position of the gate. Must be asigned in Unity
    private Vector3 defaultPosition;

    // True while the gate is moving
    private bool moving;
    private float startPosX;
    private float startPosY;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            inputCollider.enabled = false;
            outputCollider.enabled = false;
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
        
    }

    private void OnCollisionStay2D(Collision2D other){
        
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("mousedown");
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            moving = true;
            
            // Sets currentSlot to null everytime the gate gets picked up
            if (currentSlot != null) {
                inputCollider.enabled = true;
                outputCollider.enabled = true;
                currentSlot.GetComponent<SlotProperties>().setGate(null);
                currentSlot = null;
            }

            // Makes the tiles appear
            slotGroup.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    private void OnMouseUp() {
        // Makes the tiles disapear
        slotGroup.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // Movement stops
        moving = false;

        // Variables for checking if the gate is in a slot
        GameObject newSlot;
        SlotProperties newSlotScript;

        // Iterates children of slotGroup (all slots)
        foreach (Transform child in slotGroup.transform)
        {

            // If the gate position is close enough to a slot
            if (Vector3.Distance(child.position, this.transform.position) <= 0.8) {
                newSlot = child.gameObject;
                newSlotScript = newSlot.GetComponent<SlotProperties>();

                if(newSlotScript.editable == true){
                    // Moves the gate to the exact same position as the slot
                    this.transform.localPosition = newSlot.transform.localPosition;

                    // If the slot is holding another gate, the other gate's position is reset
                    if (newSlotScript.getGate() != null){
                        Debug.Log("reset slot found gate");
                        newSlotScript.getGate().GetComponent<GateDragDrop>().resetPos();
                    }

                    // The slot information is updated
                    newSlotScript.setGate(this.gameObject);

                    // currentSlot is updated
                    currentSlot = newSlot; 

                    break;
                }
            }            
        }

        // Resets gate position if it couldn't be matched to any slot
        if (currentSlot == null) {
            Debug.Log("slot not found");
            resetPos();
        }
    }

    public void resetPos() {
        // Initial position is restored
        this.transform.position = defaultPosition;

        // currentSlot is updated (set to null)
        if (currentSlot != null){
            Debug.Log("currentslot null");
            currentSlot.GetComponent<SlotProperties>().setGate(null);
            currentSlot = null;
        }
    }
}