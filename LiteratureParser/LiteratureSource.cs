using System.Collections.Generic;

namespace LiteratureParser
{
    public class LiteratureSource
    {
        public int numberInList;
        public int id;
        public string shadowText;
        public string frontText;
        public List<int> linkedIds;
        public int linkedToId;
        public bool isHidden;

        public LiteratureSource(int number, string text)
        {
            numberInList = number;
            id = number;
            shadowText = text;
            frontText = text;
            linkedIds = new List<int>();
            isHidden = false;
            linkedToId = 0;
        }
    }
}
