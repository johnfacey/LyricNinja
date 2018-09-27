#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System;
using LyricNinja;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class ArtistAnswerModeScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public ArtistAnswerModeScreen()
            : base("Correct Answer: " + Global.m_LyricObject[Global.num].Artist)
        {


            if (Global.m_Answers[Global.currentAnswerPos] == Global.num)
            {
                Global.soundInstance = Global.gong1.Play(1.0f, 0.0f, 0.0f, false);
            }
            else {
                Global.soundInstance = Global.punch1.Play(1.0f, 0.0f, 0.0f, false);
            }
            // Create our menu entries.
            MenuEntry MenuEntry0 = new MenuEntry("Continue");
            

            // Hook up menu event handlers.
            MenuEntry0.Selected += ArtistMenuEntrySelected;
           

            // Add entries to the menu.
            MenuEntries.Add(MenuEntry0);
           
        }


        #endregion



        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void ArtistMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(Global.artistModeScreen);

            Global.SelectArtistQuestion();

            Global.artistModeScreen = new ArtistModeScreen();
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               Global.artistModeScreen);

          
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void LyircMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
