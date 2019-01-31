using Instructions;
using UnityEngine;

namespace Service
{
    public class PlayerInputService: MonoBehaviour
    {
        public delegate void PlayerInput(MovementInstruction instruction);
        public event PlayerInput OnInput;
        
        private void Update()
        {            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CreateInstruction(KeyCode.DownArrow);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CreateInstruction(KeyCode.UpArrow);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                CreateInstruction(KeyCode.RightArrow);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                CreateInstruction(KeyCode.LeftArrow);
            }
        }

        private void CreateInstruction(KeyCode key)
        {
            if (OnInput == null)
                return;

            switch (key)
            {
                case KeyCode.DownArrow:
                    OnInput(new MovementInstruction(Vector2Int.down));
                    break;
                
                case KeyCode.RightArrow:
                    OnInput(new MovementInstruction(Vector2Int.right));
                    break;
                
                case KeyCode.UpArrow:
                    OnInput(new MovementInstruction(Vector2Int.up));
                    break;
                
                case KeyCode.LeftArrow:
                    OnInput(new MovementInstruction(Vector2Int.left));
                    break;
            }
        }
    }
}