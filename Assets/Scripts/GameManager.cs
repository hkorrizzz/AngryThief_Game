using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxNumberOfShots = 3;
    [SerializeField] private float _secondsToWaitBeforeDeathCheck = 3f;
    [SerializeField] private GameObject _restartScreenObject;
    [SerializeField] private SlingShotHandler _slingShotHandler;

    private int _usedNumberOfShots;

    private IcinHandler _iconHandler;

    private List<Baddie> _baddies = new List<Baddie>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _iconHandler = GameManager.FindAnyObjectByType<IcinHandler>();

        Baddie[] baddies = FindObjectsOfType<Baddie>();
        for (int i = 0; i < baddies.Length; i++)
        {
            _baddies.Add(baddies[i]);
        }

            _restartScreenObject.SetActive(false);
    }

    public void UseShot()
    {

        _usedNumberOfShots++;
        _iconHandler.UseShot(_usedNumberOfShots);



        CheckForLastShot();
    }

    public bool HasEnoughShots()
    {

        if (_usedNumberOfShots < MaxNumberOfShots)
        {
            return true;
        }
        else { return false; }
    }

    public void CheckForLastShot()
    {


        if (_usedNumberOfShots==MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }
    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(_secondsToWaitBeforeDeathCheck);


        if (_baddies.Count == 0)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void RemoveBaddie(Baddie baddie)
    {
        if (_baddies.Contains(baddie))
        {
            _baddies.Remove(baddie);
        }
        CheckForAllDeadBaddies();
    }

    private void CheckForAllDeadBaddies ()
    {
        if (_baddies.Count == 0)
        {
            
            WinGame();
        }
    }
    #region win/lose
    private void WinGame()
    {
        _restartScreenObject.SetActive(true);
        _slingShotHandler.enabled = false;
    }

    public void LoseGame()
    {
        DOTween.Clear(true);
        //   SceneManager.LoadScene(0);//////////////////

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    #endregion


    public void ExitToMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

