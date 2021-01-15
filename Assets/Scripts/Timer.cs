using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Timer
{
    private static MonoBehaviour behaviour;
    public delegate void Task();
    public delegate bool RepeatCondition();
    static MonoBehaviour _behaviour;

    //static Timer()
    //{
    //    behaviour = GameProgressHolder.Instance.GetComponent<MonoBehaviour>();
    //}

    //public static Coroutine StartCorouitine(IEnumerator task)
    //{
    //    return behaviour.StartCoroutine(task);
    //}


    //public static void Schedule(float delay, Task task)
    //{
    //    behaviour.StartCoroutine(DoTask(task, delay));
    //}

    public static void Schedule(MonoBehaviour _behaviour, float delay, Task task)
    {
        if (_behaviour.gameObject.activeInHierarchy == false) return;
        _behaviour.StartCoroutine(DoTask(task, delay));
    }

    public static void DoRepeating(MonoBehaviour behaviour, Task task, float periodRepeating, RepeatCondition repeatCondition)
    {
        _behaviour.StartCoroutine(RepeatTask(task, periodRepeating, repeatCondition));
    }

    private static IEnumerator DoTask(Task task, float delay)
    {
        yield return new WaitForSeconds(delay);
        task();
    }

    private static IEnumerator RepeatTask(Task task, float periodRepeating, RepeatCondition repeatCondition)
    {
        while (repeatCondition())
        {
            yield return new WaitForSeconds(periodRepeating);
            task();
        }       
    }
}
