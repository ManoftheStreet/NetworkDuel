using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissors : MonoBehaviour
{
    public GameObject m_severPlayerPrefab; //서버측 플레이어
    public GameObject m_clientPlayerPrefab; // 클라이언트 측 캐릭터

    public GameObject m_RPSseclectorPrefab;//가위바위보 선택
    public GameObject m_shootCallPrefab; //가위바위보! 구호 연출음
    public GameObject m_battleSelectPrefab; //공수 선택
    public GameObject m_actionControllerPrefab; // 전투연출
    public GameObject m_resultScenePrefab; //결과 표시

    public GameObject m_finalResultWinPrefab; //최종 결과 승리
    public GameObject m_finalResultLosePrefab; //최종 결과 패배

    const int PLAYER_NUM = 2;
    const int PLAY_MAX = 3;
    GameObject m_serverPlayer; //자주사용해서 확보
    GameObject m_clientPlayer; //자주사용해서 확보

    GameState m_gameState = GameState.None;
    InputData[] m_inputData = new InputData[PLAYER_NUM];
    NetworkManager m_networkManager = null;
    string m_serverAddress;

    int m_playerId = 0;
    int[] m_score = new int[PLAYER_NUM];
    Winner m_actionWinner = Winner.None;

    bool m_isGameOver = false;

    //공수의 송수신 대기용
    float m_timer;
    bool m_isSendAction;
    bool m_isReceiveAction;

    enum GameState
    {
        None = 0,
        Ready,          //게임 상대의 로그인 대기
        SelectRPS,      //가위바위보 선택
        WaitRPS,        //수신대기
        Shoot,          //가위바위보 연출
        Action,         //때리기 피하기 선택. 수신대기
        EndAction,      //때리기 피하기 연출
        Result,         //결과 발표
        EndGame,        //끝
        Disconnect,     //오류
    };

    private void Start()
    {
        m_serverPlayer = null;
        m_clientPlayer = null;

        m_timer = 0;
        m_isSendAction = false;
        m_isReceiveAction = false;

        //초기화
        for (int i = 0; i < m_inputData.Length; i++)
        {
            m_inputData[i].rpskind = RPSKind.None;
            m_inputData[i].attckInfo.actionKind = ActionKind.None;
            m_inputData[i].attckInfo.actionTime = 0.0f;
        }

        //아직 동작 시키지 않음
        m_gameState = GameState.None;

        for (int i = 0; i < m_score.Length; i++)
        {
            m_score[i] = 0;
        }

        //통신 모듈 작성
        GameObject go = new GameObject("NetWork");
        if (go != null)
        {
            TransportTCP transport = go.AddComponent<TransportTCP>();
            if (transport != null)
            {
                transport.RegisterEventHandler(EventCallback);
            }
        }
    }



    //이벤트 발생시 콜백 함수
    public void EventCallback(NetEventState state)
    {
        switch (state.type)
        {
            case NetEventType.Disconnect:
                if(m_gameState < GameState.EndGame && m_isGameOver == false)
                {
                    m_gameState=GameState.Disconnect;
                }
                break;
        }
    }
}