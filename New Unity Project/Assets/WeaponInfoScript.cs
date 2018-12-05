using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoScript : MonoBehaviour
{

    [SerializeField, Tooltip("Insert Tooltip")]
    private GameObject _weapon;
    public GameObject Weapon { get { return _weapon; } set { _weapon = value; } }
    [SerializeField, Tooltip("Insert Tooltip")]
    private GameObject _infoBox;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Image _icon;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Text _prize;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Text _description;

    private int _goldCost;

    private void Start()
    {
        _infoBox.transform.forward = -Camera.main.transform.forward;
    }

    public void SetCost(int pGold)
    {
        _goldCost = pGold;
        if (_goldCost > 0)
        {
            _prize.text = "PRIZE: " + _goldCost;
        }
        else
        {
            _prize.text = null;
        }
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<Weapon>().GoldValue);
    }

    void RotateInfoBox()
    {
        _infoBox.transform.rotation.SetLookRotation(-Camera.main.transform.forward);
    }

    void LateUpdate()
    {
       RotateInfoBox();
    }
}
