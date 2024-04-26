using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    private Animator animator;
    private Enemy enemy;
    private float attackRange;
    private BoxCollider rightHandCollider;
    private BoxCollider leftHandCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        attackRange = enemy.GetAttackRange();
        animator = GetComponentInChildren<Animator>();
        rightHandCollider = rightHand.GetComponent<BoxCollider>();
        leftHandCollider = leftHand.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsAttacking())
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.27f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.63f)
            {
                leftHandCollider.enabled = true;
                rightHandCollider.enabled = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.63f)
            {
                rightHandCollider.enabled = true;
                leftHandCollider.enabled = false;
            }
            else
            {
                rightHandCollider.enabled = false;
                leftHandCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
        }
    }
}
