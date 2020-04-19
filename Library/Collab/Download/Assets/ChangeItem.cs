using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItem : MonoBehaviour
{
    public GameObject[] items;
    public GameObject itemPanel;
    public static int selectedItem = 0;
    GameObject currentProjectile;
    Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        selectedItem = PlayerPrefs.GetInt("selectedSpell", 0);

        buttons = itemPanel.GetComponentsInChildren<Button>();
        UpdateItemUI();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSpell = selectedItem;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedItem >= items.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                ++selectedItem;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedItem <= 0)
            {
                selectedItem = items.Length - 1;
            }
            else
            {
                --selectedItem;
            }
        }

        currentProjectile = items[selectedItem];
        ShootProjectiles.currentItem = currentProjectile;

        if (previousSpell != selectedItem)
        {
            UpdateItemUI();
            PlayerPrefs.SetInt("selectedSpell", selectedItem);
        }
    }

    void UpdateItemUI()
    {
        int i = 0;

        foreach (Button itemIcon in buttons)
        {
            if (i == selectedItem)
            {
                itemIcon.transform.localScale *= 1.25f;
            }
            else
            {
                itemIcon.transform.localScale = new Vector3(1, 1, 1);
            }
            ++i;
        }
    }
}
