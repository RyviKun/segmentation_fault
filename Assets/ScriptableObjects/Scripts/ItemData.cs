using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private bool isUsable = false;
    [SerializeField] private bool isInUse = false;
    [SerializeField] private int effectTimer;
    public void SetItemInitial()
    {
        isUsable = false;
        isInUse = false;
        effectTimer = 0;
    }
    public bool GetItemStatus()
    {
        return isInUse;
    }
    public bool GetItemAvailability()
    {
        return isUsable;
    }
    public int GetEffectTimer()
    {
        return effectTimer;
    }
    public void CountDownTimer()
    {
        effectTimer--;
    }
    public void ItemUsed()
    {
        isUsable = false;
        isInUse = true;
        effectTimer = 5;
    }
    public void ItemObtained()
    {
        isUsable = true;
    }
    public void ItemEffectOff()
    {
        isInUse = false;
    }
}
