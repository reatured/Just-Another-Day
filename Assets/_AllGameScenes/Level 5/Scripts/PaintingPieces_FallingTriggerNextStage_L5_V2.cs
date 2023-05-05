using UnityEngine;

public class PaintingPieces_FallingTriggerNextStage_L5_V2 : MonoBehaviour
{
    public GameObject dragCollisionSurface; //disable this when the painting is teared apart.
    public LevelManager levelManager;
    public Transform parentNextStage, paintingPiece1, paintingPiece2;//child painting piece 1 and 2 to parent when they start to fall

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Table" && this.enabled == true) //go to next stage when the painting collide with the table.
        {
            print("collision with table");

            paintingPiece1.parent = parentNextStage;
            paintingPiece2.parent = parentNextStage; 
            levelManager.nextStage();
            Destroy(this);
        }
    }


    private void OnEnable()
    {
        print("enabled");

        dragCollisionSurface.SetActive(false);
    }
}
