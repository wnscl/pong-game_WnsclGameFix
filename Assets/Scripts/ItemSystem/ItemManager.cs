using System.Collections;
using UnityEngine;


public enum ItemType
{
    SpeedItem = 0,
    ScoreItem,
    SizeItem,
    Length
}

public class ItemManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Ball ball;
    [Header("Item Spawn")]
    [SerializeField] private float itemSpawnChance = 0.35f;
    [SerializeField] private float itemSpawnIntervalTime = 2.5f;

    private void Start()
    {
        StartCoroutine(ItemSpawnRoutine());
    }
    private IEnumerator ItemSpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            float spawnChance = Random.Range(0, 1.0f);
            bool isSpawned = spawnChance <= itemSpawnChance ? true : false;
            if (isSpawned)
                ball.TakeItem(GetRandomItemType());
            else
                ball.RemoveItem();
            yield return new WaitForSeconds(itemSpawnIntervalTime);
        }
    }
    private ItemType GetRandomItemType()
    {
        int itemNum = Random.Range(0, (int)ItemType.Length);
        return (ItemType)itemNum;
    }

}
