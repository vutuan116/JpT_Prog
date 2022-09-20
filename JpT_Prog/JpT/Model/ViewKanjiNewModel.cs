using System.Collections.Generic;

namespace JpT.Model
{
    public class ViewKanjiNewModel : BaseModel
    {

        private List<KanjiModel> _kanji;
        private string _lesson;
        private string _loading;
        private int _widthColumnData;
        private int _widthColumnAnswer;
        private int _widthColumnStatus;
        private string _btnProcessContent;
        private bool _isEnableTextboxAnswer;

        public List<KanjiModel> Kanji
        {
            get
            {
                if (_kanji == null) _kanji = new List<KanjiModel>();
                return _kanji;
            }
            set
            {
                if (_kanji == value) return;
                _kanji = value;
                OnPropertyChanged("Kanji");
            }
        }

        public string Lesson
        {
            get { return _lesson; }
            set
            {
                if (_lesson == value) return;
                _lesson = value;
                OnPropertyChanged("Lesson");
            }
        }

        public string Loading
        {
            get { return _loading; }
            set
            {
                if (_loading == value) return;
                _loading = value;
                OnPropertyChanged("Loading");
            }
        }

        private bool _isEnableStatus = false;
        public bool IsEnableStatus
        {
            get { return _isEnableStatus; }
            set
            {
                if (_isEnableStatus == value) return;
                _isEnableStatus = value;
                OnPropertyChanged("IsEnableStatus");
            }
        }

        public int WidthColumnData
        {
            get { return _widthColumnData; }
            set
            {
                if (_widthColumnData == value) return;
                _widthColumnData = value;
                OnPropertyChanged("WidthColumnData");
            }
        }

        public int WidthColumnAnswer
        {
            get { return _widthColumnAnswer; }
            set
            {
                if (_widthColumnAnswer == value) return;
                _widthColumnAnswer = value;
                OnPropertyChanged("WidthColumnAnswer");
            }
        }

        public int WidthColumnStatus
        {
            get { return _widthColumnStatus; }
            set
            {
                if (_widthColumnStatus == value) return;
                _widthColumnStatus = value;
                OnPropertyChanged("WidthColumnStatus");
            }
        }

        public string BtnProcessContent
        {
            get { return _btnProcessContent; }
            set
            {
                if (_btnProcessContent == value) return;
                _btnProcessContent = value;
                OnPropertyChanged("BtnProcessContent");
            }
        }

        public bool IsEnableTextboxAnswer
        {
            get { return _isEnableTextboxAnswer; }
            set
            {
                if (_isEnableTextboxAnswer == value) return;
                _isEnableTextboxAnswer = value;
                OnPropertyChanged("IsEnableTextboxAnswer");
            }
        }

        public List<KanjiModel> CloneKanji()
        {
            List<KanjiModel> result = new List<KanjiModel>();
            foreach (KanjiModel temp in Kanji)
            {
                result.Add(temp);
            }
            return result;
        }
    }
}
