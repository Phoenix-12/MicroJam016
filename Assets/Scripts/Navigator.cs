using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab; 
    [SerializeField] private List<GameObject> _arrows = new List<GameObject>();
    [SerializeField] private List<Transform> _targets; 

    private void Start()
    {    
         CreateArrow();
    }

    private void FixedUpdate()
    {
        var i = 0;
        foreach(var arrow in _arrows)
        {
            arrow.transform.position = transform.position + (_targets[i].position - transform.position).normalized;
            arrow.transform.up = (_targets[i].position - transform.position).normalized;
            i++;
        }
    }

    private void CreateArrow()
    {
        for(int i = 0; i < _targets.Count; i++)
        {
            Vector3 g = transform.position +(_targets[i].position - transform.position).normalized;
            var arrow = Instantiate(_arrowPrefab, g, Quaternion.identity);
            _arrows.Add(arrow);
        }
    }

}
