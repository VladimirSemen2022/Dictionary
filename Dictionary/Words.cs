using System;

namespace Dictionary
{
    class Words
    {
        public string English { get; set; }
        public string Translate { get; set; }

        public Words (string English="", string Translate = "")
        {
            this.English = English;
            this.Translate = Translate;
        }

        public override string ToString()
        {
            return $" English: {this.English}  translate: {this.Translate}";
        }
    }
}
