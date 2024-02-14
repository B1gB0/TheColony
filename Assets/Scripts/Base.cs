using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _numberUnits;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _waitingPosition;
    [SerializeField] private ResourcesCollector _resourcesCollector;
    [SerializeField] private Flag _flag;

    private List<Unit> _units = new List<Unit>();
    private List<Resources> _scanningResources;
    private Coroutine _coroutine;
    private Vector3 _positionUnit = new Vector3(5f, 1f, 6f);
    private float _stepPositionUnit = 0f;
    private int _numberResources = 0;
    private int _counterSpawn = 0;
    private int _priceUnit = 3;
    private int _priceBuldingBase = 5;
    private bool _isSelect = false;

    public event UnityAction<int> ResourcesChanged;

    public int Resources { get; private set; }

    private void Start()
    {
        _scanningResources = _scanner.Resources;

        _coroutine = StartCoroutine(SpawnUnit(_unit));
    }

    private void Update()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SpawnUnit(_unit));

        SendUnits();
        GetResources();

        if (_isSelect == false)
        {
            ChooseBase();
        }
    }

    public void CreateUnit()
    {
        if(Resources >= _priceUnit)
        {
            _numberUnits++;
            Resources -= _priceUnit;
            ResourcesChanged?.Invoke(Resources);
            _coroutine = StartCoroutine(SpawnUnit(_unit));
        }
    }

    public void GetFlag(Flag flag)
    {
        _flag = flag;
    }

    public void GetUnit(Unit unit)
    {
        _units.Add(unit);
    }

    private void GetResources()
    {
        if (_resourcesCollector.Resources != null && _resourcesCollector.Resources.IsTaken)
        {
            _resourcesCollector.Resources.Unit.IsFree = true;
            _resourcesCollector.Resources.Unit.IsGet = false;
            DisableResources();
        }
        else if (_resourcesCollector.Resources != null)
        {
            DisableResources();
        }
    }

    private void SendUnits()
    {
        for (int i = 0; i < _units.Count && _numberResources < _scanningResources.Count; i++)
        {
            if (_units[i].IsFree && _flag.IsBuild && Resources >= _priceBuldingBase)
            {
                _units[i].IsFree = false;
                _units[i].IsGet = true;

                Resources -= _priceBuldingBase;
                ResourcesChanged?.Invoke(Resources);

                _units[i].GetPositionBase(_flag.transform);
                _flag.GetUnit(_units[i]);
                _units.Remove(_units[i]);
            }
            else if (_units[i].IsFree && _scanningResources[_numberResources].IsTaken == false)
            {
                _units[i].GetResources(_scanningResources[_numberResources]);
                _units[i].GetPositionBase(transform);
                _numberResources++;
            }
            else if (_units[i].IsFree)
            {
                WaitForOrders(_units[i]);
            }
        }
    }

    private void WaitForOrders(Unit unit)
    {
        _stepPositionUnit = 0f;

        unit.IsFree = true;
        unit.GetWaitingPosition(_waitingPosition, _stepPositionUnit);
        _stepPositionUnit++;
    }

    private void DisableResources()
    {
        _resourcesCollector.Resources.IsTaken = true;
        _resourcesCollector.Resources.gameObject.SetActive(false);
        _resourcesCollector.GiveResourcesToBase();
        Resources++;
        ResourcesChanged?.Invoke(Resources);
        Debug.Log(Resources);
    }

    private void ChooseBase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Base>(out Base baseOfBots))
                {
                    baseOfBots.GetComponent<MeshRenderer>().material.color = Color.red;
                    _flag.SelectBase();
                }
            }
        }
    }

    private IEnumerator SpawnUnit(Unit unit)
    {
        while (_counterSpawn < _numberUnits)
        {
            Vector3 nextPositionUnit = _positionUnit;
            _counterSpawn++;
            _stepPositionUnit++;

            nextPositionUnit.x += _stepPositionUnit;

            Unit spawned = Instantiate(unit, nextPositionUnit, Quaternion.identity);
            _units.Add(spawned);
        }

        yield return null;
    }
}
