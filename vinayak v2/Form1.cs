using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace vinayak_v2
{
    public partial class Form1 : Form
    {   
        SpeechRecognitionEngine recEngine= new SpeechRecognitionEngine();
        SpeechSynthesizer speech= new SpeechSynthesizer();
        System.Media.SoundPlayer music= new System.Media.SoundPlayer();
        public Form1()
        {
            InitializeComponent();

            Choices choices= new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngne_SpeechRecognized);

            speech.SelectVoiceByHints(VoiceGender.Male);
        }


        private void recEngne_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;

            if(result == "Hello")
            {
                result = "Hello, my name is Vinayak how can I help you?";

            }
            if(result == "What time it is")
            {
                result = "it is currently " + DateTime.Now.ToLongTimeString();
            }
            if(result == "Open Google")
            {
                System.Diagnostics.Process.Start("https://www.google.com/");
                result = "Opening google";
            }
            if (result == "Open Youtube")
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/");
                result = "Opening Youtube";
            }
            if (result == "Close Google chrome")
            {
                System.Diagnostics.Process[] close= System.Diagnostics.Process.GetProcessesByName("chrome");
                foreach(System.Diagnostics.Process p in close)
                    p.Kill();
                result = "Closing google chrome";

            }
            if (result == "Shut down")
            {
                Application.Exit();
            }
            if (result == "Unholy")
            {
                music.SoundLocation = "Unholy.mp3";
                music.Play();
                result = "";
            }
            if(result == "stop")
            {
                speech.SpeakAsyncCancelAll();
                music.Stop();
                result = "";
            }
            speech.SpeakAsync(result);
            label2.Text = result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
