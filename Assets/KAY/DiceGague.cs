using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGague : MonoBehaviour
{
    private readonly int GRADE = 12;
    [SerializeField] private Transform center;   // ���� �߽�

    [SerializeField] private float speed = 3f;   // �ȵ� ���ǵ�
    [SerializeField] private Transform visual;   // �ȵ��Ÿ��� ������Ʈ

    [SerializeField] private GameObject graduation;   // ���� ������

    private bool isLeftMove;    // ���� �������� �����ִ��� �ƴ���
    private float curAngle;     // ���� ����
    public float radius;        // ���� ������
    private float angle;        // �ȵ��Ÿ� �ִ� ����

    private bool isPlaying = true;
    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            if (value)
            {
                // �ʱ�ȭ�ϸ� ����
                curAngle = -angle;
            }
            else
            {
                CaculateGrade();
            }

            isPlaying = value;
        }
    }

    private void Start()
    {
        radius = Vector2.Distance(transform.position, center.position);

        Vector3 direction = transform.position - center.position;
        angle = 90f - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ���� ����
        for (int i = 1; i < GRADE; i++)
        {
            GameObject newObj = Instantiate(graduation, transform);
            newObj.transform.position = GetPos(-angle + (angle * 2 / GRADE) * i);
            newObj.transform.up = GetDirection(-angle + (angle * 2 / GRADE) * i);
            newObj.SetActive(true);
        }

        IsPlaying = true;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            curAngle += Time.deltaTime * speed;

            // �� ���� �������� �� ������ �ٲ۴�
            if (curAngle > angle || curAngle < -angle)
            {
                ChangeDirection(!isLeftMove);
            }

            visual.position = GetPos(curAngle);
            visual.up = GetDirection(curAngle);
        }
    }

    // ���� ������ �ٲٴ� �Լ�
    private void ChangeDirection(bool isLeftMove)
    {
        this.isLeftMove = isLeftMove;

        if (isLeftMove)
            curAngle = angle;
        else
            curAngle = -angle;

        speed *= -1;
    }

    // ���� angle�� ����
    private Vector3 GetDirection(float angle)
    {
        Vector3 direction = Vector3.zero;
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        direction.y = Mathf.Cos(angle * Mathf.Deg2Rad);

        return direction;
    }

    // ���� angle�� ��ġ
    private Vector3 GetPos(float angle)
    {
        return center.position + GetDirection(angle) * radius;
    }

    // ���(�ܰ�) �Ǵ� �Լ�
    private void CaculateGrade()
    {
        float rate = (curAngle + angle) / (angle * 2f);
        // -angle���� angle���� curAngle�� ����

        // 1~GRADE���� �ܰ谡 ���´�
        // 0���� �����ϹǷ� 1�� ������
        int grade = (int)(rate / (1 / (float)GRADE)) + 1;
        Debug.Log($"{grade}�ܰ�");
    }
}