using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace LyricNinja
{
    class GameInput
    {
        public void UpdateInput(Game1 gameObject)
        {
            // Get the current gamepad state.
          
            KeyboardState newState = Keyboard.GetState();
            GamePadState currentState = GamePad.GetState(gameObject.controllingPlayer);


            if (currentState.Buttons.Back == ButtonState.Pressed ||
               newState.IsKeyDown(Keys.Escape))
            {
                gameObject.gameState = StateManager.TITLE;
            }

            if (gameObject.gameState == StateManager.TITLE)
            {
#if XBOX
                gameObject.controllingPlayer = PlayerIndex.One;
                for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
                {
                    if (currentState.Buttons.Start == ButtonState.Pressed)
                    {
                        gameObject.controllingPlayer = index;

                        SignedInGamer gamer = Gamer.SignedInGamers[gameObject.controllingPlayer];
                        if (gamer != null)
                        {
                            String playerName = gamer.Gamertag;
                        }
                        else
                        {

                            Guide.ShowSignIn(1, false);
                        }
                        gameObject.gameState = StateManager.SELECT;
                        break;
                    }
                }
                    /*if (!Guide.IsVisible && !StorageDeviceSelectorShowed) {
                        try { 
                            Guide.BeginShowStorageDeviceSelector(StorageSelectorCallback, null);
                            StorageDeviceSelectorShowed = true;
                        } catch {}*/
#else
                if (GamePad.GetState(gameObject.controllingPlayer).Buttons.Start == ButtonState.Pressed ||
                        newState.IsKeyDown(Keys.Space))
                {
                    Console.WriteLine("you pressed Start");
                    gameObject.gameState = StateManager.SELECT;
                }
#endif


                }//end title state

                if (gameObject.gameState == StateManager.ASK)
                {
                    if (currentState.Buttons.A == ButtonState.Pressed ||
                        newState.IsKeyDown(Keys.F1))
                    {
                        Console.WriteLine("you pressed A/1");
                        gameObject.currentAnswerPos = 0;
                        gameObject.gameState = StateManager.ANSWER;
                    }

                    if (currentState.Buttons.B == ButtonState.Pressed ||
                        newState.IsKeyDown(Keys.F2))
                    {
                        Console.WriteLine("you pressed B/2");
                        gameObject.currentAnswerPos = 1;
                        gameObject.gameState = StateManager.ANSWER;
                    }

                    if (currentState.Buttons.X == ButtonState.Pressed ||
                        newState.IsKeyDown(Keys.F3))
                    {
                        Console.WriteLine("you pressed X/3");
                        gameObject.currentAnswerPos = 2;
                        gameObject.gameState = StateManager.ANSWER;
                    }

                    if (currentState.Buttons.Y == ButtonState.Pressed ||
                        newState.IsKeyDown(Keys.F4))
                    {
                        Console.WriteLine("you pressed Y/4");
                        gameObject.currentAnswerPos = 3;
                        gameObject.gameState = StateManager.ANSWER;
                    }
                }
                if (gameObject.gameState == StateManager.ANSWER)
                {
                    
                    gameObject.gameState = StateManager.ANSWER;
                    //
                    gameObject.UpdateInput();
                    if (currentState.Buttons.A == ButtonState.Pressed  ||
                        newState.IsKeyDown(Keys.Space))
                    {
                        Console.WriteLine("you pressed space");
                        gameObject.gameState = StateManager.SELECT;
                    }


                }
                gameObject.mPreviousGamePadState = currentState;
            }
        }
    }
