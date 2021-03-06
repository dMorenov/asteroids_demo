using UnityEngine;

namespace Map
{
    public class MapBounds : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float boundsDisplacement;

        public Bounds Bounds => _bounds;
        public bool ShowGizmos = false;


        private Bounds _bounds;

        private void Start()
        {
            var verticalSize = cam.orthographicSize;
            var horizontalSize = verticalSize * Screen.width / Screen.height;

            _bounds = new Bounds(verticalSize, horizontalSize, cam.transform.position, boundsDisplacement);
        }

        public void ResetOrigin()
        {
            transform.position = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!ShowGizmos || !UnityEditor.EditorApplication.isPlaying) return;
#endif

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_bounds.BottomLeft, _bounds.BottomRight);
            Gizmos.DrawLine(_bounds.BottomRight, _bounds.TopRight);
            Gizmos.DrawLine(_bounds.TopRight, _bounds.TopLeft);
            Gizmos.DrawLine(_bounds.TopLeft, _bounds.BottomLeft);
        }
    }
}