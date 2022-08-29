using Suf.Factory;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewMyTestFactory", menuName = "Factory/MyTest Factory")]
public class MyTestFactorySO: FactorySO<GameObject>
{
    public GameObject itemPrefab;

    public override GameObject Create()
    {
        // return Instantiate(itemPrefab);
        var obj = new GameObject("My Test");
        obj.AddComponent<Image>();
        return obj;
    }
}