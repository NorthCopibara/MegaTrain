using UnityEngine;

namespace Qbik.Static.Controller
{
    public static class SController
    {
        #region Attack
        public static void AttackCollision(GameObject coolision, bool state)
        {
            coolision.SetActive(state); //Активируем объект коллизии, который сам себя выключит
        }
        #endregion

        #region Movement
        public static void Move(float move, float Speed, Rigidbody2D rb) 
        {
            rb.velocity = new Vector2(move * Speed, rb.velocity.y);
        }

        public static void Move(Vector2 move, Rigidbody2D rb)
        {
            rb.AddForce(move);
        }

        public static void Flip(Rigidbody2D rb, Transform enemyGFX)
        {
            if (rb.velocity.x >= 0.01f)
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            else
            if (rb.velocity.x <= -0.01f)
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        #endregion

        #region Jump
        public static void Jump(int extraJump, float jumpForce, Rigidbody2D rb)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        public static bool IsGround(Transform groundChek, float checkRadius, LayerMask whatIsGround)
        {
            bool _isGround = Physics2D.OverlapCircle(groundChek.position, checkRadius, whatIsGround);
            return _isGround;
        }
        #endregion
    }
}
