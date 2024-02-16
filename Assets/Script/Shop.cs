using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public MessageBox messageBox;
    public enum ItemType
    {
        Land,
        LandForRent,
        Collection
    }

    [System.Serializable]
    public class ShopItem
    {
        public Sprite image;
        public int price;
        public string displayedPrice;
        public string drp;
        public GameObject percentageBarPrefab;
        public float distancePercentage;
        public bool isPurchased = false;
        public ItemType itemType;
    }

    public List<ShopItem> ShopItemList;
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject ShopPanel;
    Button buyBtn;

    void Start()
    {
        int len = ShopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = ShopItemList[i].displayedPrice;
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            g.transform.GetChild(3).GetComponent<TMP_Text>().text = ShopItemList[i].drp.ToString();

            // Set the item type based on its position in the list
            if (i < 6)
            {
                ShopItemList[i].itemType = ItemType.Land;
            }
            else if (i < 12)
            {
                ShopItemList[i].itemType = ItemType.LandForRent;
            }
            else
            {
                ShopItemList[i].itemType = ItemType.Collection;
            }

            // Set the displayed price based on the item type
            if (ShopItemList[i].itemType == ItemType.Land)
            {
                ShopItemList[i].displayedPrice = (ShopItemList[i].price / 1000) + "K";
            }
            else if (ShopItemList[i].itemType == ItemType.LandForRent)
            {
                ShopItemList[i].displayedPrice = ShopItemList[i].price.ToString() + "/D";
                buyBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = "RENT";
               //Debug.Log($"Item {i} Type: {ShopItemList[i].itemType}, Displayed Price: {ShopItemList[i].displayedPrice}");
            }
            else
            {
                ShopItemList[i].displayedPrice = ShopItemList[i].price.ToString();
            }


           
            // Check if the item requires a percentage bar
            if (ShopItemList[i].itemType == ItemType.Land || ShopItemList[i].itemType == ItemType.LandForRent)
            {
                if (ShopItemList[i].percentageBarPrefab != null)
                {
                    // Instantiate percentage bar prefab as a child of the item
                    GameObject percentageBar = Instantiate(ShopItemList[i].percentageBarPrefab, g.transform);

                    // Set the position of the percentage bar relative to the item
                    percentageBar.transform.localPosition = new Vector3(12f, -28f, 0f); // Adjust the values as needed

                    // Find the "Percentage" and "Percentage Distance BG" objects within the percentageBar
                    Transform percentageTransform = percentageBar.transform.Find("Percentage");
                    Transform percentageDistanceBGTransform = percentageBar.transform.Find("Percentage Distance BG");

                    // Set the fill amount based on the distance percentage
                    if (percentageTransform != null)
                    {
                        Image percentageImage = percentageTransform.GetComponent<Image>();
                        if (percentageImage != null)
                        {
                            percentageImage.fillAmount = ShopItemList[i].distancePercentage;
                        }
                        else
                        {
                            Debug.LogError("Image component not found on 'Percentage' within the 'Percentage Bar Prefab'.");
                        }
                    }
                }
            }

            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
            if (ShopItemList[i].isPurchased)
            {
                DisableBuyButton(buyBtn);
            }

            // Adjust the position of the "K" based on the length of the displayed value
            AdjustKPosition(g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>(), ShopItemList[i].price);
        }
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        Button clickedButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();

        if (GameManager.instance.HasEnoughCoins(ShopItemList[itemIndex].price))
        {
            GameManager.instance.UseCoins(ShopItemList[itemIndex].price);

            ShopItemList[itemIndex].isPurchased = true;

            // Handle item type-specific logic
            switch (ShopItemList[itemIndex].itemType)
            {
                case ItemType.Land:
                    AssetManager.instance.PurchaseLand(ShopItemList[itemIndex].price);
                    break;
                case ItemType.LandForRent:
                    AssetManager.instance.PurchaseRentedLand(ShopItemList[itemIndex].price);
                    break;
                case ItemType.Collection:
                    AssetManager.instance.PurchaseCollection(ShopItemList[itemIndex].price);
                    break;
            }

            DisableBuyButton(clickedButton);

            GameManager.instance.UpdateAllCoinsUIText();
        }
        else
        {
            messageBox.ShowMessageBox("You don't have enough coins!");
            Debug.Log("You don't have enough coins!");
        }
    }

    void DisableBuyButton(Button clickedButton)
    {
        // Disable the clicked button
        clickedButton.interactable = false;
        clickedButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "PURCHASED";
    }

    void AdjustKPosition(TMP_Text textComponent, int value)
    {
        // Check if "K" is present in the text
        int kIndex = textComponent.text.LastIndexOf("K");

        if (kIndex >= 0)
        {
            // Calculate the offset based on the length of the value
            float offset = (value.ToString().Length - 1) * 0.1f; // Adjust this value as needed

            // Adjust the position of the "K" by updating the RectTransform of the textComponent itself
            RectTransform textRectTransform = textComponent.GetComponent<RectTransform>();
            textRectTransform.localPosition = new Vector3(textRectTransform.localPosition.x + offset, textRectTransform.localPosition.y, textRectTransform.localPosition.z);
        }
        //else
        //{
            //Debug.LogError("Unable to find 'K' in the text.");
        //}
    }
}
