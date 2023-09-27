using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissors : MonoBehaviour
{
    public GameObject m_severPlayerPrefab; //������ �÷��̾�
    public GameObject m_clientPlayerPrefab; // Ŭ���̾�Ʈ �� ĳ����

    public GameObject m_RPSseclectorPrefab;//���������� ����
    public GameObject m_shootCallPrefab; //����������! ��ȣ ������
    public GameObject m_battleSelectPrefab; //���� ����
    public GameObject m_actionControllerPrefab; // ��������
    public GameObject m_resultScenePrefab; //��� ǥ��

    public GameObject m_finalResultWinPrefab; //���� ��� �¸�
    public GameObject m_finalResultLosePrefab; //���� ��� �й�

    const int PLAYER_NUM = 2;
    const int PLAY_MAX = 3;
    GameObject m_serverPlayer; //���ֻ���ؼ� Ȯ��
    GameObject m_clientPlayer; //���ֻ���ؼ� Ȯ��

    GameState m_gameState = GameState.None;
    InputData[] m_inputData = new InputData[PLAYER_NUM];
    NetworkManager m_networkManager = null;
    string m_serverAddress;

    int m_playerId = 0;
    int[] m_score = new int[PLAYER_NUM];
    Winner m_actionWinner = Winner.None;

    bool m_isGameOver = false;

    //������ �ۼ��� ����
    float m_timer;
    bool m_isSendAction;
    bool m_isReceiveAction;

    enum GameState
    {
        None = 0,
        Ready,          //���� ����� �α��� ���
        SelectRPS,      //���������� ����
        WaitRPS,        //���Ŵ��
        Shoot,          //���������� ����
        Action,         //������ ���ϱ� ����. ���Ŵ��
        EndAction,      //������ ���ϱ� ����
        Result,         //��� ��ǥ
        EndGame,        //��
        Disconnect,     //����
    };

    private void Start()
    {
        m_serverPlayer = null;
        m_clientPlayer = null;

        m_timer = 0;
        m_isSendAction = false;
        m_isReceiveAction = false;

        //�ʱ�ȭ
        for (int i = 0; i < m_inputData.Length; i++)
        {
            m_inputData[i].rpskind = RPSKind.None;
            m_inputData[i].attckInfo.actionKind = ActionKind.None;
            m_inputData[i].attckInfo.actionTime = 0.0f;
        }

        //���� ���� ��Ű�� ����
        m_gameState = GameState.None;

        for (int i = 0; i < m_score.Length; i++)
        {
            m_score[i] = 0;
        }

        //��� ��� �ۼ�
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



    //�̺�Ʈ �߻��� �ݹ� �Լ�
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