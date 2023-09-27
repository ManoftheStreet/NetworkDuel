using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������������ �� �� ����
public enum RPSKind
{
    None = -1,
    Rock = 0,
    Paper,
    Scissor,
}

//���� ��� ����
public enum ActionKind
{
    None = 0,    //�̰���
    Attack,     //����
    Block,      //���
}

public struct AttackInfo
{
    public ActionKind actionKind;
    public float actionTime; //��� �ð�

    public AttackInfo(ActionKind kind, float time)
    {
        actionKind = kind;
        actionTime = time;
    }
}

public struct InputData
{
    public RPSKind rpskind; //���������� ����
    public AttackInfo attckInfo; //���� ����
}

//���ڽĺ�
public enum Winner
{
    None = 0,       //�̰���
    ServerPlayer,   //������ 1P �¸�
    ClientPlayer,   //Ŭ���̾�Ʈ �� 2P �¸�
    Draw,           //���º�
}
