using System;
using UnityEngine;

//[ExecuteInEditMode]
public class GridEntity : MonoBehaviour
{
	public event Action<GridEntity> OnMove = delegate {};
	public Vector3 velocity = new Vector3(0, 0, 0);
    public bool onGrid;
    Renderer _rend;

    private Structure _structure;
    public LightArea _defaultColor;
    private Material _defaultMaterial;
    
    [SerializeField] private Material _green;
    [SerializeField] private Material _purple;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _white;
    private void Awake()
    {
        _rend = GetComponent<Renderer>();
        _structure = GetComponent<Structure>();
    }

    void Update() {
	    if (onGrid)
	    {
		    //_rend.material.color = Color.red;
		    //_structure.Color(_defaultColor);
	    }
            
        else
            //_rend.material.color = Color.gray;
		//Optimization: Hacer esto solo cuando realmente se mueve y no en el update
		transform.position += velocity * Time.deltaTime;
	    //OnMove(this);
	}
    
    void GetType(Material d)
    {
	    if (d == _green)
	    {
		    _defaultColor.lightType = LightManager.MyLight.Green;
		    return;
	    }

	    if (d == _purple)
	    {
		    _defaultColor.lightType = LightManager.MyLight.Purple;
		    return;
	    }

	    if (d == _white)
	    {
		    _defaultColor.lightType = LightManager.MyLight.White;
		    return;
	    }

	    if (d == _yellow)
	    {
		    _defaultColor.lightType = LightManager.MyLight.Yellow;
		    // return;
	    }
	    //
	    // if (d == _grey)
	    // {
	    //     _defaultColor = LightManager.MyLight.Grey;
	    //     return;
	    // }
    }
}
