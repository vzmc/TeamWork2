//======================================
// Playerのコントローラー
// 作成者：張ユービン
//======================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class PlayerController : IController
    {
        private Player player;
        private InputState input;
        private int number;
        private Timer fireTimer;

        private Vector2 fireDirection;

        public PlayerController(int num)
        {
            number = num;
            input = ShootingGame.GetGameDevice().GetInputState();
            fireTimer = new Timer(0.1f);
            fireDirection = new Vector2(0, -1);
        }

        public void SetTarget(GameObject obj)
        {
            player = (Player)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            fireTimer.Update(gameTime, Parameters.GameSpeed);

            velocity = input.Velocity() * player.MoveSpeed;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;

            position = Vector2.Clamp(position, Vector2.Zero, new Vector2(Screen.GameWidth, Screen.GameHeight));

            if (input.CheckDownKey(Keys.Space, Buttons.RightTrigger) && fireTimer.IsTime())
            {
                bf.Fire(fireDirection * 1000);
                fireTimer.Initialize();
                ShootingGame.GetGameDevice().GetSound().PlaySE("shot");
            }
        }

        public object Clone()
        {
            return null;
        }

        public int GetPoint()
        {
            return 0;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
