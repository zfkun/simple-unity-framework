using Suf.Factory;
using Suf.Pool;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMyTestPool", menuName = "Pool/MyTest Pool")]
public class MyTestPoolSO: GameObjectPoolSO<MyTestPoolSO>
{
    [SerializeField]
    private MyTestFactorySO _factory;

    public override IFactory<GameObject> Factory
    {
        get => _factory;
        set => _factory = value as MyTestFactorySO;
    }
}