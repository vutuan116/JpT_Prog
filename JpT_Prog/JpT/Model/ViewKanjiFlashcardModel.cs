using System.Collections.ObjectModel;

namespace JpT.Model
{
   public class ViewKanjiFlashcardModel : BaseModel
   {
      private string _contentSelected;
      private string _kanji;
      private string _kotoba;
      private string _lesson = string.Empty;
      private bool _isRepeat = false;
      private string _hiragana;
      private string _hanViet;
      private string _meaning;
      private ObservableCollection<LessonModel> _lessonList = new ObservableCollection<LessonModel>();
      private ObservableCollection<KanjiModel> _kanjiList = new ObservableCollection<KanjiModel>();

      public string ContentSelected
      {
         get
         {
            return _contentSelected;
         }
         set
         {
            if (_contentSelected == value) return;
            _contentSelected = value;
            OnPropertyChanged("ContentSelected");
         }
      }

      public string Kanji
      {
         get
         {
            return _kanji;
         }
         set
         {
            if (_kanji == value) return;
            _kanji = value;
            OnPropertyChanged("Kanji");
         }
      }

      public string Kotoba
      {
         get
         {
            return _kotoba;
         }
         set
         {
            if (_kotoba == value) return;
            _kotoba = value;
            OnPropertyChanged("Kotoba");
         }
      }

      public string Lesson
      {
         get
         {
            return _lesson;
         }
         set
         {
            if (_lesson == value) return;
            _lesson = value;
            OnPropertyChanged("Lesson");
         }
      }

      public bool IsRepeat
      {
         get
         {
            return _isRepeat;
         }
         set
         {
            if (_isRepeat == value) return;
            _isRepeat = value;
            OnPropertyChanged("IsRepeat");
         }
      }

      public string Hiragana
      {
         get
         {
            return _hiragana;
         }
         set
         {
            if (_hiragana == value) return;
            _hiragana = value;
            OnPropertyChanged("Hiragana");
         }
      }

      public string HanViet
      {
         get
         {
            return _hanViet;
         }
         set
         {
            if (_hanViet == value) return;
            _hanViet = value;
            OnPropertyChanged("HanViet");
         }
      }

      public string Meaning
      {
         get
         {
            return _meaning;
         }
         set
         {
            if (_meaning == value) return;
            _meaning = value;
            OnPropertyChanged("Meaning");
         }
      }

      public ObservableCollection<LessonModel> LessonList
      {
         get { return _lessonList; }
         set
         {
            if (_lessonList == value) return;
            _lessonList = value;
            OnPropertyChanged("LessonList");
         }
      }

      public ObservableCollection<KanjiModel> KanjiList
      {
         get { return _kanjiList; }
         set
         {
            if (_kanjiList == value) return;
            _kanjiList = value;
            OnPropertyChanged("KanjiList");
         }
      }
   }
}
