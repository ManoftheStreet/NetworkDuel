using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//가위바위보에 낼 손 종류
public enum RPSKind
{
    None = -1,
    Rock = 0,
    Paper,
    Scissor,
}

//공격 방어 설정
public enum ActionKind
{
    None = 0,    //미결정
    Attack,     //공격
    Block,      //방어
}

public struct AttackInfo
{
    public ActionKind actionKind;
    public float actionTime; //경과 시간

    public AttackInfo(ActionKind kind, float time)
    {
        actionKind = kind;
        actionTime = time;
    }
}

public struct InputData
{
    public RPSKind rpskind; //가위바위보 선택
    public AttackInfo attckInfo; //공방 정보
}

//승자식별
public enum Winner
{
    None = 0,       //미결정
    ServerPlayer,   //서버쪽 1P 승리
    ClientPlayer,   //클라이언트 쪽 2P 승리
    Draw,           //무승부
}
