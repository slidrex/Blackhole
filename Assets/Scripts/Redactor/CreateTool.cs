using UnityEngine;

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
        if (hit.collider == null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Instantiate(_editor?.CurrentHolder?._entity.gameObject, new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y)), Quaternion.identity, _editor.Parent.transform);
        }
    }
}
