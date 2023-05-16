using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    public int id;
    public string name;
    
    public ScoreBar scoreBar;

    public KeyCode curKeyCode;
    public bool isOnContest = false;
    public AudioSource winAudio;
    public AudioSource shakeAudio;
    public AudioSource loseAudio;
    public GameObject sodaCan;
    public TMP_Text userName;

    // TODO: - ganjiaqi remove punctuation
    public string poem = null;
    public PoemText poemText;

    private int curWinTimes = 0; // win time & current character index in the poem
    private Animator animator;

    public int winThreshhold
    {
        get
        {
            return poem.Length;
        }
    }
    public float progress
    {
        get
        {
            return (float)curWinTimes / (float)winThreshhold;
        }
    }

    public TMP_Text infoText;
    public TMP_Text poem_text;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnContest)
        {
            return;
        }

        if (Input.GetKeyDown(curKeyCode))
        {
            Debug.Log($"user {id} got the key this round");
            infoText.text = "got the key!";

            AddOneTimeWin();

            // if this user win!
            if (curWinTimes == winThreshhold)
            {
                //isOnContest = false;
                Debug.Log("plan to win!!");
                StartCoroutine(UserShakeAnimationProcess(true));
            }
            else
            {
                Debug.Log("enter process false");
                StartCoroutine(UserShakeAnimationProcess(false));
                //GameManagement.instance.EnterNextRound(this);
                EnterNextLetter();
            }
        }
    }

    public void EnterNextLetter()
    {
        CleanThisRound();

        Debug.Log($"user {id} enter next letter round");

        isOnContest = true;

        if(curWinTimes >= winThreshhold)
        {
            return;
        }

        // TODO: - ganjiaqi. handle with the punctation!!!!
        char curChar = poem[curWinTimes];
        curChar = char.ToLower(curChar);

        if (curChar < 'a' || curChar > 'z')
        {
            AddOneTimeWin();

            // if win
            if (curWinTimes >= winThreshhold)
            {
                Debug.Log("wrong way to win!");
                StartCoroutine(UserShakeAnimationProcess(true));
                return;
            }

            EnterNextLetter();
            return;
        }

        curKeyCode = KeyCode.A + (curChar - 'a');

        Debug.Log($"user {id} kecode is {curKeyCode}");
        infoText.text = $"press {curKeyCode}";
    }

    //public void StartRound(KeyCode keyCode)
    //{
    //    isOnContest = true;
    //    curKeyCode = keyCode;
    //    string numberStr = keyCode.ToString().Replace("Alpha", "").TrimStart();

    //    Debug.Log($"user {id} kecode is {keyCode}");
    //    infoText.text = $"press {numberStr}";
    //}

    public void CleanThisRound()
    {
        isOnContest = false;
        curKeyCode = KeyCode.None;
    }

    public void PlayShakeSound()
    {
        shakeAudio.Play();
    }

    public void PlayLoseSound()
    {
        loseAudio.Play();
    }

    public void ThrowSodaCan()
    {
        sodaCan.SetActive(false);
    }

    public void UpdateUserPoem(string newPoem)
    {
        Debug.Log("update poem!");
        userName.text = name;
        poem = newPoem;

        poem_text.text = poem;
        scoreBar.slider.maxValue = winThreshhold;
        poemText.originPoem = poem;

        isOnContest = true;
    }

    //public void StartNewLevel(int totalRoundTime)
    //{
    //    winThreshhold = totalRoundTime;
    //    scoreBar.SetScoreThreshhold(totalRoundTime);
    //}

    private void AddOneTimeWin()
    {
        curWinTimes += 1;
        scoreBar.AddScore();
        poemText.UpdatePoemText(curWinTimes);
    }

    IEnumerator UserShakeAnimationProcess(bool isWin = false)
    {
        // someone win, stop contest!!!
        if(isWin)
        {
            // TODO: - ganjiaqi here maybe add some win audio indicate???
            // TODO: - ganjiaqi check this logic sequence for multi users!!
            GameManagement.instance.EndGameToShake();
        }

        Debug.Log("......");
        animator.SetTrigger("shake");
        yield return new WaitForSeconds(0.8f);

        if (isWin)
        {
            if(GameManagement.instance.winUser != null)
            {
                yield return null;
            } else
            {
                winAudio.Play();
                // TODO: - ganjiaqi timing for animation
                animator.SetTrigger("win");
                //animator.SetBool("winBool", true);
                Debug.Log("trigger win");

                yield return new WaitForSeconds(1.667f);

                GameManagement.instance.UserWinThisLevel(this);
            }
        } else
        {
            Debug.Log("trigger idle");
            animator.SetTrigger("idle");
        }
    }

}
