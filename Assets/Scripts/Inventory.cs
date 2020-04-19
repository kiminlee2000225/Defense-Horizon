using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text moneyTextInGame;
    public Text moneyTextShop;

    public int gold;

    public static int glue;
    public static int bomb;

    public Text glueCount;
    public Text bombCount;

    public Sprite[] wallShopImages;
    public Image wallShopImage;

    public Button wallUpgradeButton;
    public Button arrowExtraDamageButton;
    public Button arrowPoisonButton;
    public Button arrowIceButton;

    public GameObject arrowPrefab;

    public static int wallLevel;

    GameObject[] walls;

    public AudioClip purchaseSFX;
    Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
        glue = 0;
        bomb = 0;
        wallLevel = 0;
        wallUpgradeButton.interactable = true;
        updateMoneyText();
        glueCount.text = "x 0";
        bombCount.text = "x 0";
        setWallImage();
        walls = GameObject.FindGameObjectsWithTag("Wall");

        arrowPrefab.GetComponent<ArrowStats>().damage = 25;
        arrowPrefab.GetComponent<ArrowStats>().ice = false;
        arrowPrefab.GetComponent<ArrowStats>().poison = false;
    }

    private void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        glueCount.text = "x " + glue;
        bombCount.text = "x " + bomb;
    }
    public void addGold(int amount) {
        gold += amount;
        updateMoneyText();
    }

    public bool spendGold(int amount) {
        if (gold >= amount) {
            gold -= amount;
            AudioSource.PlayClipAtPoint(purchaseSFX, cameraPosition);
            updateMoneyText();
            return true;
        }
        return false;
    }

    public void buyGlue() {
        if (spendGold(20)) {
            glue += 2;
        }
    }

    public void buyBomb() {
        if (spendGold(10)) {
            bomb += 2;
        }
    }

    public void buyWallUpgrade() {
        if (spendGold(50)) {
            wallLevel++;
            for (int i = 0; i < walls.Length; i++) {
                walls[i].GetComponent<WallBehavior>().UpgradeWalls();
            }
            setWallImage();
        }
    }

    public void buyArrowExtraDamageUpgrade() {
        if (spendGold(50)) {
            arrowPrefab.GetComponent<ArrowStats>().damage = 50;
            arrowExtraDamageButton.interactable = false;
        }
    }

    public void buyArrowIceUpgrade() {
        if (spendGold(50)) {
            arrowPrefab.GetComponent<ArrowStats>().ice = true;
            arrowIceButton.interactable = false;
        }
    }

    public void buyArrowPoisonUpgrade() {
        if (spendGold(50)) {
            arrowPrefab.GetComponent<ArrowStats>().poison = true;
            arrowPoisonButton.interactable = false;
        }
    }

    void updateMoneyText() {
        string moneyText = gold.ToString() + "G";
        moneyTextShop.text = moneyText;
        moneyTextInGame.text = moneyText;
    }

    void setWallImage() {
        wallShopImage.GetComponent<Image>().sprite = wallShopImages[wallLevel];
        if (wallLevel >= wallShopImages.Length - 1) {
            wallUpgradeButton.interactable = false;
        }
    }
}
