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
    class ArtistModeScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public ArtistModeScreen()
            : base("Who Sang: " + Global.m_LyricObject[Global.num].Title + " ?" )
        {

            Global.soundInstance = null;

            
            // Create our menu entries.
            MenuEntry MenuEntry0 = new MenuEntry(Global.m_LyricObject[Global.m_Answers[0]].Artist);
            MenuEntry MenuEntry1 = new MenuEntry(Global.m_LyricObject[Global.m_Answers[1]].Artist);
            MenuEntry MenuEntry2 = new MenuEntry(Global.m_LyricObject[Global.m_Answers[2]].Artist);
            MenuEntry MenuEntry3 = new MenuEntry(Global.m_LyricObject[Global.m_Answers[3]].Artist);
            

            // Hook up menu event handlers.
            MenuEntry0.Selected += MenuEntry0Selected;
            MenuEntry1.Selected += MenuEntry1Selected;
            MenuEntry2.Selected += MenuEntry2Selected;
            MenuEntry3.Selected += MenuEntry3Selected;

            // Add entries to the menu.
            MenuEntries.Add(MenuEntry0);
            MenuEntries.Add(MenuEntry1);
            MenuEntries.Add(MenuEntry2);
            MenuEntries.Add(MenuEntry3);
        }


        #endregion



        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void MenuEntry0Selected(object sender, PlayerIndexEventArgs e)
        {
           // LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
             //                  new ArtistAnswerModeScreen());

            Global.currentAnswerPos = 0;
            ScreenManager.AddScreen(new ArtistAnswerModeScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void MenuEntry1Selected(object sender, PlayerIndexEventArgs e)
        {
            Global.currentAnswerPos = 1;
            ScreenManager.AddScreen(new ArtistAnswerModeScreen(), e.PlayerIndex);
        }

        void MenuEntry2Selected(object sender, PlayerIndexEventArgs e)
        {
            Global.currentAnswerPos = 2;
            ScreenManager.AddScreen(new ArtistAnswerModeScreen(), e.PlayerIndex);
        }

        void MenuEntry3Selected(object sender, PlayerIndexEventArgs e)
        {
            Global.currentAnswerPos = 3;
            ScreenManager.AddScreen(new ArtistAnswerModeScreen(), e.PlayerIndex);
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
