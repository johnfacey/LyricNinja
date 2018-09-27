using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml;
using System.Threading;

namespace LyricNinja
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<LyricObject> m_LyricObject = new List<LyricObject>();
        Random random = new Random();
        int num;
        SpriteFont Font1;
        Vector2 FontPos, FontPos1, FontPos2, FontPos3, FontPos4, FontPos5;
        public int gameState = StateManager.PRELOAD;
        private Rectangle TitleSafe;
        KeyboardState oldState = Keyboard.GetState();
        StateManager stateManager = new StateManager();
        GameInput gameInput = new GameInput();
        Texture2D NinjaLyric100, ninjafront, leftstance;
        Texture2D background,YinYang;
        Texture2D achievement;
        Texture2D enemy;
        Texture2D left_still;
        List<int> m_Answers = new List<int>();
        public PlayerIndex controllingPlayer = PlayerIndex.One;
        public int currentAnswerPos = 0;
        public GamePadState mPreviousGamePadState = GamePad.GetState(PlayerIndex.One); 
        SoundEffect gong1;
        SoundEffect punch1;
        SoundEffectInstance soundInstance;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
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
            Components.Add(new GamerServicesComponent(this));
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

            // TODO: use this.Content to load your game content here
            // Create a new SpriteBatch, which can be used to draw textures.

            Font1 = Content.Load<SpriteFont>("Kootenay");

            // TODO: Load your game content here            
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);

            FontPos1 = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 20);

            FontPos2 = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 40);

            FontPos3 = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 60);

            FontPos4 = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 80);
            
            FontPos5 = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 100);
            // Read setup information from the xml file..
            //Stream stream = File.OpenRead("Content/Lyrics.xml");

            NinjaLyric100 = Content.Load<Texture2D>("NinjaLyric100");
            background = Content.Load<Texture2D>("background");
            enemy = Content.Load<Texture2D>("enemy");

            gong1 = Content.Load<SoundEffect>("gong1");
            punch1 = Content.Load<SoundEffect>("punch1");

            achievement = Content.Load<Texture2D>("achievement");
            left_still = Content.Load<Texture2D>("left_still");
            ninjafront = Content.Load<Texture2D>("ninjafront");
            leftstance = Content.Load<Texture2D>("leftstance");
            YinYang = Content.Load<Texture2D>("starry");

            ReadXMLFile(StorageContainer.TitleLocation + "\\" + "LyricData.xml");
            gameState = StateManager.TITLE;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();//TODO: Quit Game

            // TODO: Add your update logic here
            UpdateInput();

            base.Update(gameTime);
        }

        public void SelectQuestion()
        {
            soundInstance = null;
            num = random.Next(m_LyricObject.Count);
            //randomize array here -- hard code 4
            m_Answers.Clear();
            m_Answers.Add(num);


            
            int randPos = random.Next(m_LyricObject.Count);
            String currentArtist = m_LyricObject.ElementAt(randPos).Artist;

            while (m_Answers.Count < 4) {
                /*while (m_Answers.Contains(randPos)) {
                    randPos = random.Next(m_LyricObject.Count);
                } m_Answers.Add(randPos);
                randPos = random.Next(m_LyricObject.Count);
                */

                while (m_Answers.Contains(randPos)) {
                   randPos = random.Next(m_LyricObject.Count);
               } m_Answers.Add(randPos);
               randPos = random.Next(m_LyricObject.Count);
               
            }
           
            for (int k = m_Answers.Count - 1; k > 1; --k)
            {
            int randIndx = random.Next(k); //
            int temp = m_Answers[k];
            m_Answers[k] = m_Answers[randIndx]; // move random num to

            m_Answers[randIndx] = temp;
            }
            gameState = StateManager.ASK;
        }

        public void UpdateInput()
        {
            // Get the current gamepad state.
            gameInput.UpdateInput(this);
        }

        void LoadLyricContent()
        {
            try
            {
                StreamReader streamReader = new StreamReader(StorageContainer.TitleLocation + "\\" + "LyricData.txt");
                String line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    String[] data = line.Split('|');

                    LyricObject myLyric = new LyricObject();
                    myLyric.Artist = data[0];
                    myLyric.Title = data[1];
                    myLyric.S1 = data[2];
                    myLyric.S2 = data[3];
                    myLyric.S3 = data[4];
                    myLyric.S4 = data[5];
                    m_LyricObject.Add(myLyric);

                }

                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            // Draw Hello World

            stateManager.HandleState(this, gameState);//handles changing the game state each re-draw
          
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AskQuestion()
        {
           
            string output = "Who Sang the Song: " + m_LyricObject.ElementAt(num).Title;
            Vector2 pos = new Vector2(TitleSafe.Left, TitleSafe.Top);
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;

            spriteBatch.Draw(NinjaLyric100, new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (NinjaLyric100.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (NinjaLyric100.Height / 2) - 100), Color.White);
            
            spriteBatch.DrawString(Font1, output, FontPos, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);


            spriteBatch.DrawString(Font1, "A: " + m_LyricObject.ElementAt(m_Answers.ElementAt(0)).Artist, FontPos1, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(Font1, "B: " + m_LyricObject.ElementAt(m_Answers.ElementAt(1)).Artist, FontPos2, Color.AntiqueWhite,
            0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(Font1, "X: " + m_LyricObject.ElementAt(m_Answers.ElementAt(2)).Artist, FontPos3, Color.AntiqueWhite,
            0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(Font1, "Y: " + m_LyricObject.ElementAt(m_Answers.ElementAt(3)).Artist, FontPos4, Color.AntiqueWhite,
            0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(background, new Vector2(0, graphics.GraphicsDevice.Viewport.Height - background.Height), Color.White);

            UpdateInput();
        }

        public void AnswerQuestion()
        {

            string output = "Correct Answer: " + m_LyricObject.ElementAt(num).Artist;
            Vector2 pos = new Vector2(TitleSafe.Left, TitleSafe.Top);
            
            spriteBatch.Draw(ninjafront, new Vector2(0,0), Color.White);
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            spriteBatch.DrawString(Font1, output, FontPos, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            if (m_Answers[currentAnswerPos] == num)
            {
                if (soundInstance == null) 
                    soundInstance = gong1.Play(1.0f, 0.0f, 0.0f, false);
               
                
                //MediaPlayer.Stop(
                spriteBatch.DrawString(Font1, "You got it right", FontPos2, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            }
            else {
                if (soundInstance == null)
                    soundInstance = punch1.Play(1.0f, 0.0f, 0.0f, false);
                spriteBatch.DrawString(Font1, "You got it wrong", FontPos2, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }

            spriteBatch.DrawString(Font1, "Press Left /shoulder buttton continue", FontPos3, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            
            UpdateInput();
            
        }

        public void DrawTitle()
        {
            string output = "Lyric Ninja";
            soundInstance = null;

            // Find the center of the string
            Vector2 FontOrigin = new Vector2();
            FontOrigin = Font1.MeasureString(output) / 2;
            // Draw the string

            Vector2 pos = new Vector2(TitleSafe.Left, TitleSafe.Top);
            //spriteBatch.Draw(NinjaLyric100, new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (NinjaLyric100.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (NinjaLyric100.Height / 2)), Color.White);
            spriteBatch.Draw(YinYang, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(Font1, output, FontPos3, Color.AntiqueWhite,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.9f);

            spriteBatch.DrawString(Font1, "Press Start/Space", FontPos4, Color.AntiqueWhite,
            0, FontOrigin, 1.0f, SpriteEffects.None, 0.9f);

            //winOverlay = Content.Load<Texture2D>("Overlays/you_win");
            spriteBatch.Draw(leftstance, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(NinjaLyric100, new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (NinjaLyric100.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (NinjaLyric100.Height / 2) - 100), Color.White);
            
            
        }
        /**
         * Returns the Title Safe Area
         * 
         */
        protected Rectangle GetTitleSafeArea(float percent)
        {
            Rectangle retval = new Rectangle(graphics.GraphicsDevice.Viewport.X,
                graphics.GraphicsDevice.Viewport.Y,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);
                #if XBOX
                        // Find Title Safe area of Xbox 360.
                        float border = (1 - percent) / 2;
                        retval.X = (int)(border * retval.Width);
                        retval.Y = (int)(border * retval.Height);
                        retval.Width = (int)(percent * retval.Width);
                        retval.Height = (int)(percent * retval.Height);
                        return retval;            
                #else
                    return retval;
                #endif
        }

        void ReadXMLFile(string filename)
        {
            //load the xml file into the XmlTextReader object. 
            XmlTextReader XmlRdr = new System.Xml.XmlTextReader(filename);

            //while moving through the xml document.
            bool ListIsReady = false;

            LyricObject myLyric = new LyricObject();

            while (XmlRdr.Read())
            {
                ListIsReady = true; // guard to make sure we have font objects to fill with data..


                if (ListIsReady)
                {
                    //check the node type and look for the elements desired.
                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "Artist")
                    {
                        myLyric.Artist = XmlRdr.ReadElementContentAsString();
                    }

                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "Title")
                    {
                        myLyric.Title = XmlRdr.ReadElementContentAsString();
                    }

                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "S1")
                    {
                        myLyric.S1 = XmlRdr.ReadElementContentAsString();
                    }

                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "S2")
                    {
                        myLyric.S2 = XmlRdr.ReadElementContentAsString();
                    }

                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "S3")
                    {
                        myLyric.S3 = XmlRdr.ReadElementContentAsString();
                    }

                    if (XmlRdr.NodeType == XmlNodeType.Element && XmlRdr.Name == "S4")
                    {
                        myLyric.S4 = XmlRdr.ReadElementContentAsString();
                        
                        m_LyricObject.Add(myLyric);
                        myLyric = new LyricObject();
                    }

                }

            } //endwhile
        }
    }
 
 
}
    