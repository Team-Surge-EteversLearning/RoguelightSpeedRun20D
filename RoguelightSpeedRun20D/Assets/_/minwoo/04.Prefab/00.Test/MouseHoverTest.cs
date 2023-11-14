using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스가 오브젝트 위에 들어왔을 때 호출됩니다.
        Debug.Log("Mouse entered over: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 오브젝트를 빠져나갔을 때 호출됩니다.
        Debug.Log("Mouse exited from: " + gameObject.name);
    }
}
