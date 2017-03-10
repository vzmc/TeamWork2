//==================================================
// 移動する背景
// 作成者：葉梨
// 修正者：張ユービン
//=================================================

using Microsoft.Xna.Framework;
using MyLib.Device;
using ShootingGame.Def;

namespace ShootingGame.Maps
{
    class BackGround
    {
        private string name;
        private float moveSpeed;
        private Vector2 drawOrigin;
        private Vector2 position;
        private Vector2 position2;

        public BackGround(string name)
        {
            this.name = name;
            drawOrigin = new Vector2(0, 1);
            moveSpeed = 100;
            position = Vector2.Zero;
            position2 = new Vector2(0, -ShootingGame.GetGameDevice().GetRenderer().Textures[name].Height);
        }

        public void Init()
        {
            position = new Vector2(0, 0);
            position2 = new Vector2(0, -ShootingGame.GetGameDevice().GetRenderer().Textures[name].Height);
        }

        public void Update(GameTime gameTime)
        {
            position += drawOrigin * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
            position2 += drawOrigin * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;

            if (position.Y > Screen.Height)
            {
                position.Y  = position2.Y - ShootingGame.GetGameDevice().GetRenderer().Textures[name].Height;
            }
            if (position2.Y > Screen.Height)
            {
                position2.Y  = position.Y - ShootingGame.GetGameDevice().GetRenderer().Textures[name].Height;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("main", position);
            renderer.DrawTexture("main", position2);
        }
    }
}
