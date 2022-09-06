using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UniRx;
using UnityEngine;
using UnityEngine.XR;

public class TestCode : MonoBehaviour
{
    public event Action onEvent;
    public ReactiveProperty<int> intValue = new ReactiveProperty<int>();
    // Start is called before the first frame update
    void Start()
    {
       var ob = Observable.Create<int>(observer =>
        {
            Debug.Log("Start");

            for (int i = 0; i <= 100; i += 10)
            {
                observer.OnNext(i);
            }
            observer.OnError(new Exception());

            Debug.Log("Finished");
            observer.OnCompleted();


            return Disposable.Create(() =>
            {
                        // 종료시 처리
                Debug.Log("Dispose");
            });
        });


        ob.Subscribe( value =>{
            Debug.Log(value); 
        }, e=>{
            Debug.Log($"Exception {e.Message}");

        }, ()=> {
            Debug.Log("OnCompelte");

        });

        Observable.Timer(TimeSpan.FromMilliseconds(3000)).AsObservable().Subscribe( value => Debug.Log(value ));

        



        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
