using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CreateTool : BuildingTool
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _editor.CurrentHolder != null)
        {
            Execute();
        }
    }
    public override void Execute()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.gameObject.layer == 6 && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            if (_editor.SpaceController.TryAllocateSpace(_editor.CurrentHolder._entity))
            {
                var entity = Instantiate(_editor.CurrentHolder._entity, new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y)), Quaternion.identity, _editor.Parent.transform);
                entity.OnConstruct();
                SoundController.Instance.PlayPlaceEntity();
                _editor.SpaceController.UpdateLevelAllocateStatus();
            }
        }
    }
}
