using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EvolveGames;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool inInventory = false; //if player is in their inventory
    GameObject item1BlankSpace;
    GameObject item2BlankSpace;
    GameObject item3BlankSpace;

    GameObject item1Image;
    GameObject item2Image;
    GameObject item3Image;

    GameObject player;
    ItemInteractController ItemScript;

    TextMeshProUGUI inventoryEnterTooltip;

    GameObject inventoryMainBody;
    bool inventoryLoaded = false;

    OptoinsMenuController optionsScript;
    StartMenuController startMenuScript;
    DeathSceneEffects deathSceneScript;

    void Start()
    {
        inventoryMainBody = GameObject.Find("Inventory Main Body");


        item1BlankSpace = GameObject.Find("Item 1 not gained");
        item2BlankSpace = GameObject.Find("Item 2 not gained");
        item3BlankSpace = GameObject.Find("Item 3 not gained");

        item1Image = GameObject.Find("Page Item 1 Inventory Version");
        item2Image = GameObject.Find("Page Item 2 Inventory Version");
        item3Image = GameObject.Find("Page Item 3 Inventory Version");

        //intiiallising the UI elements as off, bc player does not start in inventory, but I wanna be able to see them in play.
        item1BlankSpace.SetActive(false);
        item2BlankSpace.SetActive(false);
        item3BlankSpace.SetActive(false);

        item1Image.SetActive(false);
        item2Image.SetActive(false);
        item3Image.SetActive(false);

        ItemScript = GetComponent<ItemInteractController>();

        inventoryEnterTooltip = GameObject.Find("Inventory Enter Tooltip").GetComponent<TextMeshProUGUI>();

        inventoryMainBody.SetActive(false);

        optionsScript = GameObject.Find("Options Menu").GetComponent<OptoinsMenuController>();
        startMenuScript = GameObject.Find("Start Menu").GetComponent<StartMenuController>();
        deathSceneScript = GameObject.Find("Death Scene Effects Object").GetComponent<DeathSceneEffects>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inInventory && !deathSceneScript.deathScenePlay && !optionsScript.inOptions && !startMenuScript.inStartMenu) //if the player is not in their inventory, or another screen where we don't want the inventory, show their inventory
            {
                inInventory = true;
            }
            else
            { //if the player is in their inventory, stop showing their inventory
                inInventory = false;
            }
        } // end of if I pressed

        if (inInventory && !inventoryLoaded)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponentInChildren<HeadBob>().enabled = false;
            inventoryEnterTooltip.enabled = false;
            inventoryMainBody.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            flipInventoryItems();

            inventoryLoaded = true;
        }
        if (!inInventory && inventoryLoaded)
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponentInChildren<HeadBob>().enabled = true;
            inventoryEnterTooltip.enabled = true;
            inventoryMainBody.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            flipInventoryItems();

            inventoryLoaded = false;
        }
    }

    void flipInventoryItems()
    {
        if (ItemScript.Item1Gained)
        {
            item1Image.SetActive(!item1Image.activeSelf);
        }
        else
        {
            item1BlankSpace.SetActive(!item1BlankSpace.activeSelf);
        }

        if (ItemScript.Item2Gained)
        {
            item2Image.SetActive(!item2Image.activeSelf);
        }
        else
        {
            item2BlankSpace.SetActive(!item2BlankSpace.activeSelf);
        }

        if (ItemScript.Item3Gained)
        {
            item3Image.SetActive(!item3Image.activeSelf);
        }
        else
        {
            item3BlankSpace.SetActive(!item3BlankSpace.activeSelf);
        }
    }
}
