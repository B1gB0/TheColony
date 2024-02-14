using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Base _baseTemplate;

    private Base _newBase;
    private Unit _unit;

    private bool _isSelected = false;
    private bool _isBuild = false;
    private bool _isReady = false;

    public bool IsBuild => _isBuild;

    private void OnTriggerEnter(Collider other)
    {
        if(_unit != null && other.gameObject == _unit.gameObject)
        {
            _newBase = Instantiate(_baseTemplate, new Vector3( transform.position.x, 0.03f, transform.position.z),
            Quaternion.identity);

            _unit.IsFree = true;
            _unit.IsGet = false;
            _isReady = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isReady)
        {
            EquipBase();
        }

        if (_isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.TryGetComponent<Level>(out Level level))
                    {
                        transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        _isSelected = false;
                        _isBuild = true;
                    }
                }
            }
        }
    }

    private void EquipBase()
    {
        _newBase.GetUnit(_unit);
        _newBase.GetFlag(this);
        gameObject.SetActive(false);
    }

    public void SelectBase()
    {
        _isSelected = true;
    }

    public void GetUnit(Unit unit)
    {
        _unit = unit;
    }
}
