using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetEventType
{
    Connect = 0, //접속 이벤트
    Disconnect,  //끊김 이벤트
    SendError,   //송신 오류
    ReceiveError,//수신 오류
}

public enum NetEventResult
{
    Failure = -1, //실패
    Success = 0,  //성공
}

public class NetEventState
{
    public NetEventType type;
    public NetEventResult result;
}