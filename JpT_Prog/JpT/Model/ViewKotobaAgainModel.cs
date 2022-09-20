using System.Collections.Generic;

namespace JpT.Model
{
    public class ViewKotobaAgainModel : BaseModel
    {
        private List<KotobaModel> _kotoba;
        public List<KotobaModel> Kotoba
        {
            get
            {
                if (_kotoba == null) _kotoba = new List<KotobaModel>();
                return _kotoba;
            }
            set
            {
                if (_kotoba == value) return;
                _kotoba = value;
                OnPropertyChanged("Kotoba");
            }
        }

        private string _lesson;
        public string Lesson
        {
            get { return _lesson; }
            set
            {
                if (_lesson == value) return;
                _lesson = value;
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

        private int _widthColumnAnswer;
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

        private int _widthColumnMeaning;
        public int WidthColumnMeaning
        {
            get { return _widthColumnMeaning; }
            set
            {
                if (_widthColumnMeaning == value) return;
                _widthColumnMeaning = value;
                OnPropertyChanged("WidthColumnMeaning");
            }
        }

        private int _widthColumnStatus;
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

        private string _btnProcessContent;
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

        private string _loading;
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

        private bool _isEnableTextboxAnswer;
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

        public List<KotobaModel> CloneKotoba()
        {
            List<KotobaModel> result = new List<KotobaModel>();
            foreach (KotobaModel temp in Kotoba)
            {
                result.Add(temp);
            }
            return result;
        }
    }
}
