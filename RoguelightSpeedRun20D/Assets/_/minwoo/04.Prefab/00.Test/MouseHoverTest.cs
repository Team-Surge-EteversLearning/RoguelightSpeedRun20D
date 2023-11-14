using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺�� ������Ʈ ���� ������ �� ȣ��˴ϴ�.
        Debug.Log("Mouse entered over: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺�� ������Ʈ�� ���������� �� ȣ��˴ϴ�.
        Debug.Log("Mouse exited from: " + gameObject.name);
    }
}
