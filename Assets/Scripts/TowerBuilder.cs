using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    public Color AllowColor;
    public Color DenyColor;
    private TowerData currentTowerData;
    private void Awake()
    {
        Events.OnTowerSelected += TowerSelected; // kuulab
        gameObject.SetActive(false);
    }

   

    private void OnDestroy()
    {
        Events.OnTowerSelected -= TowerSelected;
    }

    private void TowerSelected(TowerData data)
    {
        currentTowerData = data;
        print("data : "  + currentTowerData);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Reposition the gameobject to mouse coordinates. 
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Round the coordinates to make it snap to a grid.
        var pos = new Vector3(
            Mathf.Round(mousepos.x - 0.5f) + 0.5f ,
            Mathf.Round(mousepos.y - 0.5f) + 0.5f, 
            0);
        transform.position = pos;
        //Verify that building area is free of other towers.
        bool free = IsFree(pos);
        //By using a static overlap method from Physics2D class we can make this work without collider and a 2d rigidbody.
        //Tint the sprite to green or red accordingly.
        TintSprite(free ? AllowColor : DenyColor);
        //Call the build method when the player presses left mouse button
        if (Input.GetMouseButton(0))
        {
            Build(pos);
        }
        if (Input.GetMouseButton(1))
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsFree(Vector3 pos)
    {
        var overlaps = Physics2D.OverlapCircleAll(pos, 0.2f);
        foreach (var overlap in overlaps)
        {
            if (!overlap.isTrigger)
                return false;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }
        return true;

    }

    void TintSprite(Color col)
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in renderers)
        {
            spriteRenderer.color = col;
        }
    }

    private void Build(Vector3 pos)
    {
        bool free = IsFree(pos);

        if (!free || !HasEnoughGold())
            return;

        GameObject.Instantiate<Tower>(currentTowerData.TowerPrefab,pos,Quaternion.identity,null);
        Events.SetGold(Events.GetGold() - currentTowerData.Cost);
        gameObject.SetActive(false);
    }

    private bool HasEnoughGold()
    {
        int currentGold = Events.GetGold();
        return currentGold >= currentTowerData.Cost;
    }
        
}











