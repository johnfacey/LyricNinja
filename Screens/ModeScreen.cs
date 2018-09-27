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
using LyricNinja;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class ModeScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public ModeScreen()
            : base("Select Mode")
        {
            // Create our menu entries.
            MenuEntry artistMenuEntry = new MenuEntry("Artist Mode");
            MenuEntry lyricMenuEntry = new MenuEntry("Lyric Mode");
            MenuEntry backMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            artistMenuEntry.Selected += ArtistMenuEntrySelected;
            lyricMenuEntry.Selected += LyircMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(artistMenuEntry);
            MenuEntries.Add(lyricMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void ArtistMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
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
