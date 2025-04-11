using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackFactory", menuName = "Gameplay/TrackFactory")]
public class TruckFactory : ScriptableObject
{
    [SerializeField] private Truck _truck1;
    [SerializeField] private Truck _truck2;

    public Truck Get(TruckSkins skinType, Vector3 spawnPoint)
    {
        Truck instance = Instantiate(GetPrefab(skinType), spawnPoint, Quaternion.identity, null);
        instance.Initialize();
        return instance;
    }

    private Truck GetPrefab(TruckSkins skinType)
    {
        switch (skinType)
        {
            case TruckSkins.Track1:
                return _truck1;
            case TruckSkins.Track2:
                return _truck2;
            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
