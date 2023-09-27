using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetEventType
{
    Connect = 0, //���� �̺�Ʈ
    Disconnect,  //���� �̺�Ʈ
    SendError,   //�۽� ����
    ReceiveError,//���� ����
}

public enum NetEventResult
{
    Failure = -1, //����
    Success = 0,  //����
}

public class NetEventState
{
    public NetEventType type;
    public NetEventResult result;
}