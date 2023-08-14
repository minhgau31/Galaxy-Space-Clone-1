using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();
   public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }
  public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObserver(EventID eventID)
    {
        _observers.ForEach((_observers) => { _observers.OnNotify(eventID);});
    }
}

