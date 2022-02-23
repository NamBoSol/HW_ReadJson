using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class Main : MonoBehaviour
{
    [SerializeField]
    List<Vector2> mapTiles = new List<Vector2>();
    public GameObject tileTemplate;
    public GameObject objectTemplate;
    public Transform mapContainer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mapTiles.Add(position);
            var tileObject = Instantiate(tileTemplate, mapContainer);
            tileObject.transform.position = position;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var obj = Instantiate(objectTemplate, mapContainer);
            obj.transform.position = position;
        }
    }

    public void LoadMap()
    {
        ClearMap();
        string mapJsonString = PlayerPrefs.GetString("map", "");

        mapTiles = JsonConvert.DeserializeObject<List<Vector2>>( mapJsonString);
        if (mapTiles == null)
        {
            mapTiles = new List<Vector2>();
        }
        else
        {
            mapTiles.ForEach((tile) =>
            {
                var tileObject = Instantiate(tileTemplate, mapContainer);
                tileObject.transform.position = tile;
            });
        }

    }

    public void SaveMap()
    {
        mapTiles.RemoveAt(mapTiles.Count - 1);
        PlayerPrefs.SetString("map", JsonConvert.SerializeObject(mapTiles, Formatting.None,
new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
}));
    }

    public void ClearMap()
    {
        foreach (Transform child in mapContainer)
        {
            Destroy(child.gameObject);
        }
    }

}
