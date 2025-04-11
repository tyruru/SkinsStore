
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;
    
    private List<ShopItemView> _shopItems = new();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }
    
    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach (var item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;
            
            spawnedItem.UnSelect();
            spawnedItem.UnHighLight();
            
            _openSkinsChecker.Visit(spawnedItem.Item);
            if (_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItem.Item);

                if (_selectedSkinChecker.IsSelected)
                {
                    spawnedItem.Select();
                    spawnedItem.HighLight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }
                
                spawnedItem.Unlock();
            }
            else
            {
                spawnedItem.Lock();
            }
            
            _shopItems.Add(spawnedItem);
        }
        
        Sort();
    }

    public void Select(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
            item.UnSelect();
        
        itemView.Select();
    }

    private void Sort()
    {
        _shopItems = _shopItems
            .OrderBy(item => item.IsLock)
            .ThenByDescending(item => item.Price)
            .ToList();

        for (int i = 0; i < _shopItems.Count(); i++)
        {
            _shopItems[i].transform.SetSiblingIndex(i);
        }
    }
    
    private void OnItemViewClick(ShopItemView itemView)
    {
        Highlight(itemView);
        ItemViewClicked?.Invoke(itemView);
    }

    private void Highlight(ShopItemView shopItemView)
    {
        foreach (var item in _shopItems)
            item.UnHighLight();
        
        shopItemView.HighLight();
    }

    private void Clear()
    {
        foreach (var item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }
        
        _shopItems.Clear();
    }
}
