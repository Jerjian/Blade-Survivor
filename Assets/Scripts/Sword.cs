using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private SwordSO swordData;
    public SwordSO GetSwordData() => swordData;
}
