using UnityEngine;

public class SkinPlacement : MonoBehaviour
{
    public const string RendererLayer = "SkinRenderer";

    [SerializeField] private Rotator _rotator;
    
    private GameObject _currentModel;

    public void InstantiateModel(GameObject model)
    {
        if(_currentModel != null)
            Destroy(_currentModel.gameObject);
        
        _rotator.ResetRotation();

        _currentModel = Instantiate(model, transform);

        Transform[] children = _currentModel.GetComponentsInChildren<Transform>();

        foreach (var item in children)
            item.gameObject.layer = LayerMask.NameToLayer(RendererLayer);
        
    }
}
