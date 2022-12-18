using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace Folk_CourseProject_Part1
{
    public partial class MainForm : Form
    {
        // class level references
        string[] titleArray = new string[5];
        string[] artistArray = new string[5];
        string[] genreArray = new string[5];
        int[] yearArray = new int[5];
        string[] urlArray = new string[5];
        int songCount = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        public bool ValidInput() 
        {
            // Return True if all fields are non-empty
            bool isValid = true;

            if (string.IsNullOrEmpty(titleText.Text))
            {
                //Title is Empty
                MessageBox.Show("What Is The Name Of The Song?");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(artistText.Text))
            {
                //Artist is Empty
                MessageBox.Show("Who Wrote The Song?");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(genreText.Text))
            {
                //Genre is Empty
                MessageBox.Show("The Song Must Have A Genre!");
                isValid=false;
            }
            else if (string.IsNullOrEmpty(yearText.Text))
            {
                //Year is Empty
                MessageBox.Show("When Was The Song Released?");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(urlText.Text))
            {
                //URL is Empty
                MessageBox.Show("Please Provide A Link To The Song!");
                isValid = false;
            }
            return isValid;
        }

        private void artistText_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(outputText.Text);
            string nl = "\r\n";
       
            if (ValidInput())
            {
                // Add Title to list box and song list
                songList.Items.Add(titleText.Text);
                titleArray[songCount] = titleText.Text;
                artistArray[songCount] = artistText.Text;
                genreArray[songCount] = genreText.Text;
                yearArray[songCount] = int.Parse(yearText.Text);
                urlArray[songCount] = urlText.Text;

                // increment song counter
                songCount++;

                //Build the output text
                sb.Append(titleText.Text);
                sb.Append(nl);
                sb.Append(artistText.Text);
                sb.Append(nl);
                sb.Append(genreText.Text);
                sb.Append(nl);
                sb.Append(yearText.Text);
                sb.Append(nl);
                sb.Append(urlText.Text);
                sb.Append(nl);
                sb.Append(nl);          // blank line

                outputText.Text = sb.ToString();

                MessageBox.Show("Song Successfully Added!");
            }
        }

        private bool SongInList(string songTitle)
        {
            bool found = false;

            foreach (var item in songList.Items)
            {
                string currentSong = item as string;
                // lowercase comparison not case sensitive
                if(songTitle.ToLower() == currentSong.ToLower())
                {
                    found = true;
                }
            }

            return found;
            
        }

        private int GetSongIndex (string songTitle)
        {
            int songIndex = songList.Items.IndexOf(songTitle);
            return songIndex;
        }
        private void songList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int songIndex = songList.SelectedIndex;

            //if song is selected (index > -1), show details
            if (songIndex >-1 )
            {
                StringBuilder sb = new StringBuilder(string.Empty);
                string nl = "\r\n";

                sb.Append(titleArray[songIndex]);
                sb.Append(nl);
                sb.Append(artistArray[songIndex]);
                sb.Append(nl);
                sb.Append(genreArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex]);
                sb.Append(nl);
                sb.Append(urlArray[songIndex]);
                sb.Append(nl);

                outputText.Text = sb.ToString();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void allSongsButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            string nl = "\r\n";

            //Build the output text
            foreach (var item in songList.Items)
            {
                sb.Append(item.ToString());
                sb.Append(nl);

            }

            // Put output text into output textbox
            outputText.Text = sb.ToString();

        }

        private void findButton_Click(object sender, EventArgs e)
        {
            if(SongInList(titleText.Text))
            {
                StringBuilder sb = new StringBuilder(string.Empty);
                string nl = "\r\n";

                int songIndex = GetSongIndex(titleText.Text);

                //Build output text
                sb.Append(titleArray[songIndex]);
                sb.Append(nl);
                sb.Append(artistArray[songIndex]);
                sb.Append(nl);
                sb.Append(genreArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex]);
                sb.Append(nl);
                sb.Append(urlArray[songIndex]);
                sb.Append(nl);

                outputText.Text = sb.ToString();

            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            int songIndex = songList.SelectedIndex;
            string url = urlArray[songIndex];
            webViewDisplay.CoreWebView2.Navigate(url);
        }

        private void clearButton_Click_1(object sender, EventArgs e)
        {
            titleText.Text = ""; //one to clear textbox
            artistText.Text = String.Empty;
            genreText.Clear();
            yearText.Clear();
            urlText.Clear();
        }
    }
}
