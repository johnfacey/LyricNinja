using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using GameStateManagement;
using Microsoft.Xna.Framework.Media;

namespace LyricNinja
{
    class Global
    {
        public static SoundEffectInstance soundInstance;
        public static List<LyricObject> m_LyricObject = new List<LyricObject>();
        public static Random random = new Random();
        public static int num;
        public static List<int> m_Answers = new List<int>();
        public PlayerIndex controllingPlayer = PlayerIndex.One;
        public static int currentAnswerPos = 0;
        public static SoundEffect gong1;
        public static SoundEffect punch1;
        public static Song song1;
        public static ArtistModeScreen artistModeScreen;

        public static void ReadXMLFile(string filename)
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

        public static void SelectArtistQuestion()
        {
            Global.soundInstance = null;
            Global.num = Global.random.Next(Global.m_LyricObject.Count);
            //randomize array here -- hard code 4
            Global.m_Answers.Clear();
            Global.m_Answers.Add(Global.num);



            int randPos = Global.random.Next(Global.m_LyricObject.Count);
            String currentArtist = Global.m_LyricObject[randPos].Artist;

            while (Global.m_Answers.Count < 4)
            {
                /*while (m_Answers.Contains(randPos)) {
                    randPos = random.Next(m_LyricObject.Count);
                } m_Answers.Add(randPos);
                randPos = random.Next(m_LyricObject.Count);
                */

                while (Global.m_Answers.Contains(randPos))
                {
                    randPos = Global.random.Next(Global.m_LyricObject.Count);
                } Global.m_Answers.Add(randPos);
                randPos = Global.random.Next(Global.m_LyricObject.Count);

            }

            for (int k = Global.m_Answers.Count - 1; k > 1; --k)
            {
                int randIndx = Global.random.Next(k); //
                int temp = Global.m_Answers[k];
                Global.m_Answers[k] = Global.m_Answers[randIndx]; // move random num to

                Global.m_Answers[randIndx] = temp;
            }
            //gameState = StateManager.ASK;
        }

        public static void AnswerArtistQuestion() {

            if (m_Answers[currentAnswerPos] == num)
            {
                if (soundInstance == null)
                    soundInstance = gong1.Play(1.0f, 0.0f, 0.0f, false);


                //MediaPlayer.Stop(
                //spriteBatch.DrawString(Font1, "You got it right", FontPos2, Color.AntiqueWhite,
                //0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            }
            else
            {
                if (soundInstance == null)
                    soundInstance = punch1.Play(1.0f, 0.0f, 0.0f, false);
                //spriteBatch.DrawString(Font1, "You got it wrong", FontPos2, Color.AntiqueWhite,
                //0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }

        }
    }


}
