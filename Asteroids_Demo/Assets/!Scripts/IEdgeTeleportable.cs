using UnityEngine;

public interface IEdgeTeleportable
{
    public Vector2 GetPosition();

    public void TeleportHorizontalyTo(float x);

    public void TeleportVerticalyTo(float y);
}
