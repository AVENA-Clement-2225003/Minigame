using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    Coroutine m_MoovingCoroutine;
    public void Move(InputAction.CallbackContext context)
    {
        if (m_MoovingCoroutine == null )
        {
            m_MoovingCoroutine = StartCoroutine(Mooving(context));
        }
    }

    IEnumerator Mooving(InputAction.CallbackContext context)
    {
        while (context.started){
            Vector2 value = context.ReadValue<Vector2>();
            Debug.Log("Mooving" + value);
            yield return null;
        }
        m_MoovingCoroutine = null;
    }
}
