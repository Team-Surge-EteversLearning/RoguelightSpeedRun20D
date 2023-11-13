using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseCompletePanelController : MonoBehaviour
{
    [SerializeField] float waitTime;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    public void ActiveAndDisable()
    {
        gameObject.SetActive(true);
        StartCoroutine(WaitDisable());
    }
    public IEnumerator WaitDisable()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
