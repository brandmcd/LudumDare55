using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject noUseTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to check when an item button is clicked
    public void ItemButtonClicked()
    {
        // Get the item name from the button
        string itemName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Item Name: " + itemName);

        //Check if the item is currently usable

        // Find the child GameObject with the name "NoUseTxt"
        StartCoroutine(NoUseFlash());
    }

    IEnumerator NoUseFlash()
    {
        noUseTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noUseTxt.gameObject.SetActive(false);
    }
}
