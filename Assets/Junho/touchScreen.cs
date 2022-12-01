using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class touchScreen : touchInput, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region variable
    public float ScreenRate = 0.5f;

    public RectTransform TouchRect;

    public GameObject StickBack;
    public GameObject StickHandle;

    public Toggle toggle;

    private float max_dist = 20f;
    #endregion

    private Vector3 inPos;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(width);
        TouchRect.sizeDelta = new Vector2(Screen.width * ScreenRate, TouchRect.sizeDelta.y);

        StickBack.SetActive(false);
        StickHandle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!Ontouch)
        {
            if (Onvalue)
            {
                Vector3 nPos = new Vector2(valX, valY);
                Vector3 next_pos = Vector3.Lerp(nPos, Vector3.zero, Time.deltaTime * 5f);

                valX = next_pos.x;
                valY = next_pos.y;

                StickHandle.transform.position = StickBack.transform.position + next_pos * max_dist;

                if (next_pos.magnitude < 0.1f && next_pos.magnitude > -0.1f)
                {
                    StickBack.SetActive(false);
                    StickHandle.SetActive(false);

                    Onvalue = false;
                }
            }
            else
            {
                valX = 0;
                valY = 0;
            }
        }
    }

    void Touch(PointerEventData eventData)
    {
        //��ġ ������ �ޱ�
        Vector3 m_pos = eventData.position;
        Vector3 mouse_dir = m_pos - StickBack.transform.position;
        Vector3 result_dir = mouse_dir;

        float chect_dist = max_dist;

        //magnitude(�Ÿ�)�� max_dist(40)�̻� �̸� 
        if (mouse_dir.magnitude > max_dist)
        {
            chect_dist = max_dist;
            //dir�� ��ֶ�����(�Ÿ�(ũ��)�� 1�� ���ͷ� ����)�ϰ� 
            //max_dist(40)�� ������
            result_dir = mouse_dir.normalized * max_dist;
            Debug.Log("m:" + result_dir);
        }

        StickBack.transform.position = inPos;
        StickHandle.transform.position = StickBack.transform.position + result_dir;

        //�Է� ��� Ȯ��
        Vector3 f_dir = result_dir / chect_dist;

        valX = f_dir.x;
        valY = f_dir.y;

        if (valX != 0 || valY != 0)
            Onvalue = true;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        inPos = eventData.position;
        if (toggle.isOn)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(TouchRect, eventData.position))
            {
                StickBack.SetActive(true);
                StickHandle.SetActive(true);

                Ontouch = true;

                Touch(eventData);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Ontouch)
        {
            Touch(eventData);
        }
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (Ontouch)
        {
            Ontouch = false;
        }
    }
}