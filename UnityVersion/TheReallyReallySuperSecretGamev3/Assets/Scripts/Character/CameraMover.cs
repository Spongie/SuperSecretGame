using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CameraMover : MonoBehaviour
    {
        public Transform Player;
        public float CameraSpeed;
        private Rect ivLeftArea;
        private Rect ivRightArea;
        private Rect ivTopArea;
        private Rect ivBottomArea;

        void Start()
        {
            Vector2 size = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

            ivLeftArea = new Rect(0, 0, size.x / 8, size.y);
            ivRightArea = new Rect(size.x - (size.x / 8), 0, size.x / 8, size.y);
            ivTopArea = new Rect(0, 0, size.x, size.y / 8);
            ivBottomArea = new Rect(0, size.y - (size.y / 8), size.x, size.y / 8);
        }

        void Update()
        {
            Vector2 playerPos = Camera.main.WorldToScreenPoint(Player.position);

            if (ivLeftArea.Contains(playerPos))
                transform.Translate(new Vector2(-CameraSpeed, 0));

            if (ivRightArea.Contains(playerPos))
                transform.Translate(new Vector2(CameraSpeed, 0));

            if (ivTopArea.Contains(playerPos))
                transform.Translate(new Vector2(0, -CameraSpeed));

            if(ivBottomArea.Contains(playerPos))
                transform.Translate(new Vector2(0, CameraSpeed));
        }
    }
}
