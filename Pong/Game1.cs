﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D ballTexture;
        private Texture2D paddleTexture;
        private Ball ball;
        private ScoreScreen score;
        private PlayerPaddle player;
        private ComputerPaddle computer;

        private SpriteFont font;

        private const int gameWidth = 800;
        private const int gameHeight = 600;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = gameWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = gameHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = TextureManager.CreateTexture(20, 20);
            paddleTexture = TextureManager.CreateTexture(20, 100);
            font = Content.Load<SpriteFont>("testfont");

            score = new ScoreScreen(font, new Vector2(gameWidth * 0.25F, gameHeight * 0.45F), new Vector2(gameWidth * 0.75F, gameHeight * 0.45F));
            ball = new Ball(ballTexture, new Vector2(390, 290));
            player = new PlayerPaddle(paddleTexture, new Vector2(10, gameHeight / 2 - 50));
            computer = new ComputerPaddle(paddleTexture, new Vector2(gameWidth - 30, gameHeight / 2 - 50));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            ballTexture.Dispose();
            font = null;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*
            player.HandleInput();
            computer.Move();
             */
            throw new NotImplementedException("input handling for player");

            ball.Update(gameTime);
            player.Update(gameTime);
            computer.Update(gameTime);

            checkCollisions();

            score.Update(playerLeftScore.ToString(), playerRightScore.ToString());

            base.Update(gameTime);
        }

        private void checkCollisions()
        {
            if (ball.Position.X > gameWidth) 
            {
                computer.Score++;
                ball.Reset();
            }
            else if (ball.Position.X < 0) 
            {
                player.Score++;
                ball.Reset();
            }
            else if (ball.Position.Y > gameHeight - 20 || ball.Position.Y < 0)
            {
                ball.ReverseY();
            }
            
            throw new NotImplementedException("Paddle collision with ball");
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            score.Draw(spriteBatch);
            player.Draw(spriteBatch);
            computer.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}