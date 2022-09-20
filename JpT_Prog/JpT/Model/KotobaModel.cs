namespace JpT.Model
{
    public class KotobaModel : BaseModel
    {
        private int _id;
        private string _content;
        private string _hiragana;
        private string _answer = string.Empty;
        private string _meaningOther = string.Empty;
        private string _meaning;
        private string _historyAnswer = string.Empty;
        private bool _isRightAnswer;
        private string _visibleRow = "Hidden";

        public string Level { get; set; }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (_content == value) return;
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public string Hiragana
        {
            get { return _hiragana; }
            set
            {
                if (_hiragana == value) return;
                _hiragana = value;
                OnPropertyChanged("Hiragana");
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                if (_answer == value) return;
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public string Meaning
        {
            get { return _meaning; }
            set
            {
                if (_meaning == value) return;
                _meaning = value;
                OnPropertyChanged("Meaning");
            }
        }

        public string MeaningOther
        {
            get { return _meaningOther; }
            set
            {
                if (_meaningOther == value) return;
                _meaningOther = value;
                OnPropertyChanged("MeaningOther");
            }
        }

        public string HistoryAnser
        {
            get { return _historyAnswer; }
            set
            {
                if (_historyAnswer == value) return;
                _historyAnswer = value;
                OnPropertyChanged("HistoryAnser");
            }
        }

        public bool IsRightAnswer
        {
            get { return _isRightAnswer; }
            set
            {
                if (_isRightAnswer == value) return;
                _isRightAnswer = value;
                OnPropertyChanged("IsRightAnswer");
            }
        }

        public string VisibleRow
        {
            get { return _visibleRow; }
            set
            {
                if (_visibleRow == value) return;
                _visibleRow = value;
                OnPropertyChanged("VisibleRow");
            }
        }

        public KotobaModel Clone()
        {
            return (KotobaModel)this.MemberwiseClone();
        }

        public bool Equals(KotobaModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return this.Id == other.Id;
        }
    }
}
