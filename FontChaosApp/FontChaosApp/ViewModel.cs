using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FontChaosApp {
    public class ViewModel : Notify {
        List<FontFamily> fonts = new List<FontFamily>(Fonts.SystemFontFamilies);
        Random r = new Random();
        FlowDocument flowDocument;

        private string _input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }


        public ViewModel(RichTextBox RichText) {
            flowDocument = new FlowDocument();
            flowDocument.Blocks.Add(BuildLetter(Input));
            RichText.Document = flowDocument;
        }

        public Section BuildLetter(string input) {
            Section section = new Section();
            Paragraph paragraph = new Paragraph();
            foreach(char c in input) {
                Run run = new Run(c.ToString());
                run.FontFamily = fonts[r.Next(0, fonts.Count)];
                paragraph.Inlines.Add(run);
            }
            section.Blocks.Add(paragraph);
            return section;
        }

        public void OnClick(object sender, EventArgs e) {
            flowDocument.Blocks.Clear();
            flowDocument.Blocks.Add(BuildLetter(Input));
        }
    }

    public class Notify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
