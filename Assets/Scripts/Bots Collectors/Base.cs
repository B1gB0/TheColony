using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _numberUnits;
    [SerializeField] private Scanner _scanner;

    private List<Unit> _units = new List<Unit>();
    private List<Resources> _resources;
    private Coroutine _coroutine;
    private Vector3 _positionUnit = new Vector3(5f, 1f, 6f);
    private float _stepSpawnPosition = 1f;
    private int _numberResources = 0;
    private int _counterSpawn = 0;

    public event UnityAction<int> ResourcesChanged;

    public int Resources { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Resources>(out Resources resources))
        {
            resources.Unit._isFree = true;
            resources.Unit._isGet = false;
            resources.gameObject.SetActive(false);
            Resources++;
            ResourcesChanged?.Invoke(Resources);
            Debug.Log(Resources);
        }
    }

    private void Start()
    {
        _resources = _scanner.Resources;
    }

    private void Update()
    {
        SendUnits();

        if (_coroutine == null && _counterSpawn < _numberUnits)
        {
            _coroutine = StartCoroutine(SpawnUnit(_unit));
        }
        else
        {
            StopCoroutine(_coroutine);
        }
    }

    private void SendUnits()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i]._isFree && _resources[_numberResources]._isTaken == false && _numberResources < _resources.Count - 1)
            {
                _units[i].GetResources(_resources[_numberResources]);
                _numberResources++;
            }
            else if (_units[i]._isGet == true)
            {
                _units[i].GiveResources(transform);
            }
        }
    }

    private IEnumerator SpawnUnit(Unit unit)
    {
        while (_counterSpawn < _numberUnits)
        {
            Vector3 nextPositionUnit = _positionUnit;
            _counterSpawn++;
            _stepSpawnPosition++;

            nextPositionUnit.x += _stepSpawnPosition;

            Unit spawned = Instantiate(unit, nextPositionUnit, Quaternion.identity);
            _units.Add(spawned);
        }

        yield return null;
    }
}
