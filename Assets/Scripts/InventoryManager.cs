using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject noUseTxt;
    public string[] summaries;

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
        noUseTxt.GetComponent<TextMeshProUGUI>().text = "";
        noUseTxt.SetActive(true);
        // Get the item name from the button
        string itemName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if(itemName == "VR_Testimony")
        {
            if (PlayerPrefs.GetString("val1") == "true")
            {
                noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[0];
                if (PlayerPrefs.GetString("val2") == "true")
                    noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[1];
            }
            else
                StartCoroutine(NoUseFlash());
        }
        else if(itemName == "TD_Testimony")
        {
            if (PlayerPrefs.GetString("terra1") == "true")
            {
                noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[2];
                if (PlayerPrefs.GetString("terra2") == "true")
                    noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[3];
            }
            else
                StartCoroutine(NoUseFlash());
        }
        else if (itemName == "LP_Testimony")
        {
            if (PlayerPrefs.GetString("leo1") == "true")
            {
                noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[4];
                if (PlayerPrefs.GetString("leo2") == "true")
                    noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[5];
            }
            else
                StartCoroutine(NoUseFlash());
        }
        else if (itemName == "TR_Testimony")
        {
            if (PlayerPrefs.GetString("rex1") == "true")
            {
                noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[6];
                if (PlayerPrefs.GetString("rex2") == "true")
                    noUseTxt.GetComponent<TextMeshProUGUI>().text = summaries[7];
            }
            else
                StartCoroutine(NoUseFlash());
        }
    }

    IEnumerator NoUseFlash()
    {
        noUseTxt.GetComponent<TextMeshProUGUI>().text = "You haven't learned anything from this person yet...";
        noUseTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noUseTxt.gameObject.SetActive(false);
        noUseTxt.GetComponent<TextMeshProUGUI>().text = "";
    }
}
