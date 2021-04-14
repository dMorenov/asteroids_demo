using UnityEngine;

namespace Map
{
    public class Bounds
    {
        public float HorizontalSize => _horizontalSize;
        public float VerticalSize => _verticalSize;
        public Vector2 Origin => _origin;

        private float _horizontalSize;
        private float _verticalSize;
        private Vector2 _origin;

        private float _minX;
        private float _minY;
        private float _maxX;
        private float _maxY;

        public Bounds(float verticalSize, float horizontalSize, Vector2 origin, float displacement = 0f)
        {
            _horizontalSize = horizontalSize;
            _verticalSize = verticalSize;
            _origin = origin;

            _minX = origin.x - _horizontalSize - displacement;
            _minY = origin.y - _verticalSize - displacement;
            _maxX = origin.x + _horizontalSize + displacement;
            _maxY = origin.y + _verticalSize + displacement;
        }

        public Vector2 BottomRight => new Vector2(_maxX, _minY);
        public Vector2 BottomLeft => new Vector2(_minX, _minY);
        public Vector2 TopLeft => new Vector2(_minX, _maxY);
        public Vector2 TopRight => new Vector2(_maxX, _maxY);
    }
}