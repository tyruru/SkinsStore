
using System;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;
    
    [SerializeField] private ShopCategoryButton _trackSkinButton;
    [SerializeField] private ShopCategoryButton _cargoSkinButton;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Image _selectedText;
    
    [SerializeField] private ShopPanel _shopPanel;

    [SerializeField] private SkinPlacement _skinPlacement;
    
    private IDataProvider _dataProvider;

    private ShopItemView _previewedItem;

    private Wallet _wallet;
    
    private SkinSelector _skinSelector;
    private SelectedSkinChecker _selectedSkinChecker;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;
    
    private void OnEnable()
    {
        _trackSkinButton.Click += OnTrackSkinsButtonClick;
        _cargoSkinButton.Click += OnCargoSkinsButtonClick;
        _buyButton.Click += OnBuyButtonClicked;
        _selectionButton.onClick.AddListener(OnSelectionButtonClicked);
    }

    private void OnDisable()
    {
        _trackSkinButton.Click -= OnTrackSkinsButtonClick;
        _cargoSkinButton.Click -= OnCargoSkinsButtonClick;
        _shopPanel.ItemViewClicked -= OnItemViewClicked;
        _buyButton.Click -= OnBuyButtonClicked;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClicked);
    }

    public void Initialize(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, 
        SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _dataProvider = dataProvider;

        _wallet = wallet;
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
        _skinUnlocker = skinUnlocker;
        _skinSelector = skinSelector;
        
        _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);
        
        _shopPanel.ItemViewClicked += OnItemViewClicked;
        
        OnCargoSkinsButtonClick();
    }
    private void OnCargoSkinsButtonClick()
    {
       _cargoSkinButton.Select();
       _trackSkinButton.Unselect();
       _shopPanel.Show(_contentItems.CargoSkinItems.Cast<ShopItem>());
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        _previewedItem = item;
        _skinPlacement.InstantiateModel(_previewedItem.Model);
        
        _openSkinsChecker.Visit(_previewedItem.Item);

        if (_openSkinsChecker.IsOpened)
        {
            _selectedSkinChecker.Visit(_previewedItem.Item);

            if (_selectedSkinChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }
            
            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewedItem.Price);
        }
    }

    private void OnBuyButtonClicked()
    {
        if (_wallet.IsEnough(_previewedItem.Price))
        {
            _wallet.Spend(_previewedItem.Price);
            
            _skinUnlocker.Visit(_previewedItem.Item);
            
            SelectSkin();
            
            _previewedItem.Unlock();
            
            _dataProvider.Save();
        }
    }

    private void OnSelectionButtonClicked()
    {
        SelectSkin();
        
        _dataProvider.Save();
    }
    
    private void OnTrackSkinsButtonClick()
    {
        _trackSkinButton.Select();
        _cargoSkinButton.Unselect();
        _shopPanel.Show(_contentItems.TrackSkinItems.Cast<ShopItem>());
    }

    private void SelectSkin()
    {
        _skinSelector.Visit(_previewedItem.Item);
        _shopPanel.Select(_previewedItem);
        ShowSelectedText();
    }
    
    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }
    
    private void ShowSelectedText()
    {
        _selectedText.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectionButton();
    }
    
    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price);
        
        if(_wallet.IsEnough(price))
            _buyButton.UnLock();
        else
            _buyButton.Lock();
        
        HideSelectedText();
        HideSelectionButton();
    }
    
    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
}
