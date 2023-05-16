using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance { get; private set; }

    public User user1;
    public User user2;
    public Camera mainCamera;
    public Camera user1ViewCamera;
    public Camera user2ViewCamera;
    public GameObject canvas;
    public GameObject waterUser1;
    public GameObject waterUser2;
    public User winUser;

    public GameObject startScreen;
    public TMP_Text lostInfo;

    private List<KeyCode> keyCodes = new List<KeyCode>();
    private int keyCodesCount
    {
        get
        {
            return keyCodes.Count;
        }
    }


    // TODO: - ganjiaqi. temp use, delete later
    //public TMP_Text winInfo;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // here to add all the keys(letter and number)
        for (int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
        {
            keyCodes.Add((KeyCode)i);
        }
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            keyCodes.Add((KeyCode)i);
        }

        //EnterNextRound();
        //NewStartNewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if(startScreen.activeSelf && user1.poem != null && user1.poem.Length > 0 && user2.poem != null && user2.poem.Length > 0)
        {
            startScreen.SetActive(false);
            NewStartNewRound();
        }
    }

    public void UserWinThisLevel(User user)
    {
        if(winUser != null)
        {
            return;
        }

        EndCurRound();

        winUser = user;

        Debug.Log($"user {winUser.id} finally won!!!!!");
        //winInfo.text = $"user {winUser.id} won!!!";
        

        // TODO: - ganjiaqi. maybe try to split screen
        if (winUser == user1)
        {
            SetToUser1View();
            waterUser1.SetActive(true);

            lostInfo.text = $"{user2.name} lost";
            user2.gameObject.GetComponent<Animator>().SetTrigger("lose");
        }
        else
        {
            SetToUser2View();
            waterUser2.SetActive(true);

            lostInfo.text = $"{user1.name} lost";
            user1.gameObject.GetComponent<Animator>().SetTrigger("lose");
        }

        disableUIThings();
    }

    public void NewStartNewRound()
    {
        user1.EnterNextLetter();
        user2.EnterNextLetter();
    }

    public void EndGameToShake()
    {
        user1.isOnContest = false;
        user2.isOnContest = false;
    }

    // both users enter next round
    //public void EnterNextRound()
    //{
    //    Debug.Log("both users enter next round");
    //    EndCurRound();

    //    int totalKeyNums = keyCodes.Count;
    //    int indexForUser1 = Random.Range(0, totalKeyNums);
    //    int indexForUser2 = Random.Range(0, totalKeyNums);

    //    // two users need to press different keycode
    //    while (indexForUser2 == indexForUser1)
    //    {
    //        indexForUser2 = Random.Range(0, totalKeyNums);
    //    }

    //    user1.StartRound(keyCodes[indexForUser1]);
    //    user2.StartRound(keyCodes[indexForUser2]);
    //}

    // only one user enter next round
    //public void EnterNextRound(User goUser)
    //{
    //    goUser.CleanThisRound();

    //    Debug.Log($"user {goUser.id} enter next round");

    //    User stayUser = goUser == user1 ? user2 : user1;
    //    int indexForUser = Random.Range(0, keyCodesCount);

    //    // two users need to press different keycode
    //    while (keyCodes[indexForUser] == stayUser.curKeyCode)
    //    {
    //        indexForUser = Random.Range(0, keyCodesCount);
    //    }

    //    goUser.StartRound(keyCodes[indexForUser]);

    //}

    private void EndCurRound()
    {
        user1.CleanThisRound();
        user2.CleanThisRound();
    }

    private void SetToUser1View()
    {
        user1ViewCamera.enabled = true;
        user2ViewCamera.enabled = false;
        mainCamera.enabled = false;
    }

    private void SetToUser2View()
    {
        user1ViewCamera.enabled = false;
        user2ViewCamera.enabled = true;
        mainCamera.enabled = false;
    }

    private void SetToMainCamera()
    {
        user1ViewCamera.enabled = false;
        user2ViewCamera.enabled = false;
        mainCamera.enabled = true;
    }

    private void disableUIThings()
    {
        canvas.SetActive(false);
    }

    private void eableUIThings()
    {
        canvas.SetActive(true);
    }
}
