using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaseCompletePanelController : MonoBehaviour
{
    [SerializeField] float waitTime;
    TMP_Text text;
    private void Start()
    {
        gameObject.SetActive(false);
        text = GetComponentInChildren<TMP_Text>();
    }
    // Start is called before the first frame update
    public void ActiveAndDisable(string name)
    {
        switch (name)
        {
            case "0":
                name = "HpPotion";
                break;
            case "1":
                name = "ManaPotion";
                break;
            case "2":
                name = "Bomb";
                break;
            case "3":
                name = "Barrier";
                break;
        }
        gameObject.SetActive(true);
        text.text = $"You successfully purchased the {name}.";
        StartCoroutine(WaitDisable());
    }
    public IEnumerator WaitDisable()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
