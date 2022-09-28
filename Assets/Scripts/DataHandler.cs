using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataHandler : MonoBehaviour
{
    [SerializeField]private GameObject furniture;
    
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> _items;
    [SerializeField] private String label;
    
    private int id = 0;
    
    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
        
    }
    
    private async void Start()
    {
        _items = new List<Item>();
        //LoadItems();
        await Get(label); // 從AddressableAsset載入
        CreateButtons();
    }

    // void LoadItems()
    // {
    //     var items_obj =Resources.LoadAll("Items",typeof(Item));
    //     foreach (var item in items_obj)
    //     {
    //         _items.Add(item as Item);
    //     }
    //     
    // }

    // 在選單每個圖片上加按鈕
    void CreateButtons()
    {
        foreach (Item i in _items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = id;
            b.ButtonTexture = i.itemImage;
            id++;
        }
        // 縮放
        buttonContainer.GetComponent<UIContentFitter>().Fit();
    }

    // 設置家具
    public void SetFurinute(int id)
    {
        furniture = _items[id].itemPrefab;
    }

    // 獲得家具
    public GameObject GetFurniture()
    {
        return furniture;
    }

    // 從AddressableAsset載入
    public async Task Get(String label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach (var location in locations)
        {
            var obj = await Addressables.LoadAssetAsync<Item>(location).Task;
            _items.Add(obj);
        }
    }
}
