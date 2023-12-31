using UnityEngine;

public class DeleteTool : BuildingTool
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Execute();
        }
    }
    public override void Execute()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (hit.collider != null && hit.collider.gameObject.layer != 6)
        {
            DestroyImmediate(hit.collider.gameObject);
            SoundController.Instance.PlayDeconstructEntity();
            _editor.SpaceController.UpdateLevelAllocateStatus();
        }
    }
}
