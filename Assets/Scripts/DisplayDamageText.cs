using UnityEngine;

public class DisplayDamageText : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    private IDamagable DamagableObject;

    private void Awake()
    {
        DamagableObject = GetComponentInParent<IDamagable>();
    }
    private void Start()
    {
        DamagableObject.OnHealthChanged += DisplayFloatingText_OnHealthChanged;
    }

    private void DisplayFloatingText_OnHealthChanged(object sender, float e)
    {
        //Instantiate floating text prefab
        GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        floatingText.GetComponentInChildren<TextMesh>().text = Mathf.Abs(e).ToString();

        Destroy(floatingText, 1.0f);
    }
}
