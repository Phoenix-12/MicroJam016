using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public List<GameObject> Targets = new List<GameObject>();

    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _bigArrow;
    [SerializeField] private GameObject _gate;
    private List<GameObject> _bigArrows = new List<GameObject>();
    private List<GameObject> _minArrows = new List<GameObject>();

    public void HideArrows()
    {
        foreach (var arrow in _bigArrows)
        {
            arrow.SetActive(false);
        }
        foreach (var arrow in _minArrows)
        {
            arrow.SetActive(false);
        }
    }

    private void Start()
    {
        CreateArrow();    
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        foreach (var bigArrow in _bigArrows)
        {
            bigArrow.transform.position = transform.position + (_gate.transform.position - transform.position).normalized;
            bigArrow.transform.up = (_gate.transform.position - transform.position).normalized;
        }

        var i = 0;
        foreach (var arrow in _minArrows)
        {
            if (Targets[i].active)
            {
                arrow.transform.position = transform.position + (Targets[i].transform.position - transform.position).normalized;
                arrow.transform.up = (Targets[i].transform.position - transform.position).normalized;
                
            }
            else
            {
                arrow.SetActive(false);
            }
            i++;
        }
    }

    private void CreateArrow()
    {
        Vector3 bigArrowPosition = transform.position + (_gate.transform.position - transform.position).normalized;
        var bigArrow = Instantiate(_bigArrow, bigArrowPosition, Quaternion.identity);
        _bigArrows.Add(bigArrow);

        for(int i = 0; i < Targets.Count; i++)
        {
            Vector3 minArrowPosition = transform.position +(Targets[i].transform.position - transform.position).normalized;
            var minArrow = Instantiate(_arrowPrefab, minArrowPosition, Quaternion.identity);
            _minArrows.Add(minArrow);
        }
    }
}