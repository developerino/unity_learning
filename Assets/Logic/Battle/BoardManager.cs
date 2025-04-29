using System;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<TileProperty> _tilePropsList;
    [SerializeField] private GameObject _piece;

    private void OnEnable()
    {
        InputManager.OnWorldClick += HandleWorldClick;
    }

    private void OnDisable()
    {
        InputManager.OnWorldClick -= HandleWorldClick;
    }

    public void InitializeBoard()
    {
        Debug.Log("BoardManager: Initialized by GameManager.");

        _tilePropsList = new List<TileProperty>();
        int id = 1;
        foreach (Transform tileChild in transform)
        {
            TileProperty tempRef = tileChild.GetComponent<TileProperty>();
            tempRef.SetId(id);
            tempRef.SetOwner(OWNER.NONE);

            _tilePropsList.Add(tempRef);
            id++;
        }
    }

    private void HandleWorldClick(Vector3 clickPosition)
    {
        // Responds to clicks cleanly
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"BoardManager: Raycasted object is: {hit.collider.gameObject.name}");
            // TODO: Add board-specific reactions here
        }
    }
}
