using System;
using UnityEngine;
using UnityEngine.Jobs;

public class GameplayBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _truckSpawnPoint;
    [SerializeField] private Transform _cargoSpawnPoint;

    [SerializeField] private TruckFactory _truckFactory;
    [SerializeField] private CargoFactory _cargoFactory;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private void Awake()
    {
        InitializeData();
        Spawn();
    }

    private void Spawn()
    {
        Truck truck = _truckFactory.Get(_persistentData.PlayerData.SelectedTruckSkin, _truckSpawnPoint.position);
        Cargo cargo = _cargoFactory.Get(_persistentData.PlayerData.SelectedCargoSkin, _cargoSpawnPoint.position);
        
        Debug.Log("Bootstrap spawn");
    }

    private void InitializeData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);

        LoadDataOrInit();
    }

    private void LoadDataOrInit()
    {
        if (!_dataProvider.TryLoad())
            _persistentData.PlayerData = new PlayerData();
    }
}
