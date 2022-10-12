using UnityEngine;

namespace Entities
{
    public static class PositionProcessor
    {
        private const float MAXRayDistance = 6;
        private const int LayerWallIndex = 6;
        private const float YRayCastPosition = 0.05f;
        private static float _sphereRadius = 5;
        private static float _ySpawnPosition = 0.05f;

        public static Vector3 GetNewPosition()
        {
            Vector3 position = Random.insideUnitSphere * _sphereRadius;
            position.y = _ySpawnPosition;

            Vector3 raycastPosition = position;
            raycastPosition.y += YRayCastPosition;

            Ray ray = new Ray(raycastPosition, position - raycastPosition);

            if (Physics.Raycast(ray, MAXRayDistance, 1 << LayerWallIndex))
            {
                return GetNewPosition();
            }

            return position;
        }
    }
}