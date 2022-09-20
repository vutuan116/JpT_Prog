namespace JpT.Model
{
   public class KanjiModel : BaseModel
   {
      private int _id;
      private string _kanji;
      private string _content;
      private string _hiragana;
      private string _hiraganaAnswer = string.Empty;
      private string _hanViet;
      private string _meaningAnswer = string.Empty;
      private string _meaningOther;
      private string _meaning;
      private string _historyAnswer = string.Empty;
      private bool _isRightAnswer;
      private bool _isRepeat = false;
      private string _visibleRow = "Hidden";
      private bool _isHard = false;
      private bool _isLock = false;
      

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

      public string Lesson { get; set; }

      public string Kanji
      {
         get { return _kanji; }
         set
         {
            if (_kanji == value) return;
            _kanji = value;
            OnPropertyChanged("Kanji");
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

      public string HiraganaAnswer
      {
         get { return _hiraganaAnswer; }
         set
         {
            if (_hiraganaAnswer == value) return;
            _hiraganaAnswer = value;
            OnPropertyChanged("HiraganaAnswer");
         }
      }

      public string HanViet
      {
         get { return _hanViet; }
         set
         {
            if (_hanViet == value) return;
            _hanViet = value;
            OnPropertyChanged("HanViet");
         }
      }

      public string MeaningAnswer
      {
         get { return _meaningAnswer; }
         set
         {
            if (_meaningAnswer == value) return;
            _meaningAnswer = value;
            OnPropertyChanged("MeaningAnswer");
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

      public bool IsRepeat
      {
         get { return _isRepeat; }
         set
         {
            if (_isRepeat == value) return;
            _isRepeat = value;
            OnPropertyChanged("IsRepeat");
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

      public bool IsHard
      {
         get { return _isHard; }
         set
         {
            if (_isHard == value) return;
            _isHard = value;
            OnPropertyChanged("IsHard");
         }
      }

      public bool IsLock
      {
         get { return _isLock; }
         set
         {
            if (_isLock == value) return;
            _isLock = value;
            OnPropertyChanged("IsLock");
         }
      }

      public KanjiModel Clone()
      {
         return (KanjiModel)this.MemberwiseClone();
      }

      public bool Equals(KanjiModel other)
      {
         if (other == null) return false;
         if (this == other) return true;
         return this.Id == other.Id;
      }
   }
}
