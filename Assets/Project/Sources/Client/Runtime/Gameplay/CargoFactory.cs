
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CargoFactory", menuName = "Gameplay/CargoFactory")]
public class CargoFactory : ScriptableObject
{
    [SerializeField] private Cargo _cargo1;
    [SerializeField] private Cargo _cargo2;
    [SerializeField] private Cargo _cargo3;
    [SerializeField] private Cargo _cargo4;

    public Cargo Get(CargoSkins skinType, Vector3 spawnPoint)
    {
        Cargo instance = Instantiate(GetPrefab(skinType), spawnPoint, Quaternion.identity, null);
        instance.Initialize();
        return instance;
    }

    private Cargo GetPrefab(CargoSkins skinType)
    {
        switch (skinType)
        {
            case CargoSkins.Cargo1:
                return _cargo1;
            case CargoSkins.Cargo2:
                return _cargo2;
            case CargoSkins.Cargo3:
                return _cargo3;
            case CargoSkins.Cargo4:
                return _cargo4;
            
            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
