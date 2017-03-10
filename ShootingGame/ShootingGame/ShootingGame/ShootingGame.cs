using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using ShootingGame.Def;
using ShootingGame.Scene;
using ShootingGame.GameObjects;

namespace ShootingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ShootingGame : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private static GameDevice gameDevice;
        private InputState inputState;
        private SceneManager sceneManager;

        public ShootingGame()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "ShootingGame";

            gameDevice = new GameDevice(Content, GraphicsDevice);       //GameDevice�C���X�^���X�𐶐�
            inputState = gameDevice.GetInputState();

            //Scene�Ǘ��̐���
            sceneManager = new SceneManager();

            //�eScene�̐����ƒǉ�
            IScene gamePlay = new GamePlay();
            sceneManager.Add(SceneType.Title, new Title());
            sceneManager.Add(SceneType.GamePlay, gamePlay);
            sceneManager.Add(SceneType.Ending, new Ending(gamePlay));
            sceneManager.Add(SceneType.Load, new Load());

            //�ŏ���Scene�Ɉڍs
            sceneManager.Change(SceneType.Load);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            gameDevice.LoadContent();
            Score.LoadHighScore();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            gameDevice.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (inputState.GetKeyState(Buttons.Back) && inputState.GetKeyState(Buttons.Start) || inputState.GetKeyState(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            //gameDevice�̍X�V
            gameDevice.Update();

            //Scene�Ǘ��̍X�V
            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //�`����
            gameDevice.GetRenderer().Begin();
            sceneManager.Draw(gameTime);
            gameDevice.GetRenderer().End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// GameDevice�C���X�^���X���擾
        /// </summary>
        /// <returns></returns>
        public static GameDevice GetGameDevice()
        {
            return gameDevice;
        }
    }
}
