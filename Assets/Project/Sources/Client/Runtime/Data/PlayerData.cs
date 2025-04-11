
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    private CargoSkins _selectedCargoSkin;
    private TruckSkins _selectedTruckSkin;

    private List<CargoSkins> _openedCargoSkins;
    private List<TruckSkins> _openedTrackSkins;

    private int _money;

    public PlayerData()
    {
        _money = 15000;
        
        _selectedCargoSkin = CargoSkins.Cargo1;
        _selectedTruckSkin = TruckSkins.Track1;

        _openedCargoSkins = new List<CargoSkins>() { _selectedCargoSkin };
        _openedTrackSkins = new List<TruckSkins>() { _selectedTruckSkin };
    }

    [JsonConstructor]
    public PlayerData(int money, CargoSkins selectedCargoSkin, TruckSkins selectedTruckSkin, 
        List<CargoSkins> openedCargoSkins, List<TruckSkins> openedTrackSkins)
    {
        Money = money;
        _selectedCargoSkin = selectedCargoSkin;
        _selectedTruckSkin = selectedTruckSkin;

        _openedCargoSkins = new List<CargoSkins>(openedCargoSkins);
        _openedTrackSkins = new List<TruckSkins>(openedTrackSkins);
    }

    public int Money
    {
        get => _money;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public CargoSkins SelectedCargoSkin
    {
        get => _selectedCargoSkin;
        set
        {
            if (!_openedCargoSkins.Contains(value))
                throw new ArgumentException(nameof(value));

            _selectedCargoSkin = value;
        }
    }
    
    public TruckSkins SelectedTruckSkin
    {
        get => _selectedTruckSkin;
        set
        {
            if (!_openedTrackSkins.Contains(value))
                throw new ArgumentException(nameof(value));

            _selectedTruckSkin = value;
        }
    }

    public IEnumerable<CargoSkins> OpenedCargoSkins => _openedCargoSkins;
    public IEnumerable<TruckSkins> OpenedTrackSkins => _openedTrackSkins;

    public void OpenCargoSkin(CargoSkins skin)
    {
        if (_openedCargoSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));
        
        _openedCargoSkins.Add(skin);
    }
    
    public void OpenTrackSkin(TruckSkins skin)
    {
        if (_openedTrackSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));
        
        _openedTrackSkins.Add(skin);
    }
}
