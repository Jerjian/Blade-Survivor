
using UnityEngine;

[CreateAssetMenu(fileName = "SwordSO", menuName = "ScriptableObjects/SwordSO")]
public class SwordSO : ScriptableObject
{
    public Transform swordPrefab;
    public float damage;
    public float attackSpeed;
    public float attackRange;
    public float attackDuration;
    public float attackCooldown;
    public string attackName;
    public Sprite swordSprite;
}
