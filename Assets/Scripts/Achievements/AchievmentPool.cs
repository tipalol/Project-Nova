using UnityEngine;
using System.Collections.Generic;

public class AchievmentPool : MonoBehaviour
{
    private List<Achievement> _pool;

    private void Start()
    {
        Debug.Log("Привет я креветка");
        
        _pool = new List<Achievement>();
        Init();
    }

    

    private void Init()
    {
        _pool.Add(new Pacifist());
    }
}