using JpT.Entity;
using JpT.Logic;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JpT
{
    /// <summary>
    /// Interação lógica para SubWindowKanji.xam
    /// </summary>
    public partial class View_Flashcard : UserControl
    {
        private ViewFlashcardLogic _logic = new ViewFlashcardLogic();
        private ViewFlashcardModel _model = new ViewFlashcardModel();
        private List<int> listWordIdShowed;
        private List<WordModel> listWordTemp = new List<WordModel>();
        private int wordIndexInListShowed = 0;

        public View_Flashcard(MainWindow mainWindow)
        {
            InitializeComponent();
            InitData();
            _model.Level = _logic.GetLevelConfig();
            cbbxLevel.SelectedIndex = (int)_model.Level;
            this.DataContext = _model;
        }

        #region Menu Screen
        private void InitData()
        {
            _model.ListKanjiLesson = new ObservableCollection<LessonModel>();
            _model.ListVocabularyLesson = new ObservableCollection<LessonModel>();
            tbxVocabularySum.Text = string.Empty;
            tbxKanjiSum.Text = string.Empty;
            int lastLsVocabulary = 0;
            int lastLsKanji = 0;

            List<LessonModel> listLesson = _logic.GetListLesson(_model.Level);
            listLesson.ForEach(x =>
            {
                if (x.Type.Equals("TV"))
                {
                    _model.ListVocabularyLesson.Add(x);
                    if (!string.IsNullOrEmpty(x.LastLearning)) lastLsVocabulary++;
                }
                else
                {
                    _model.ListKanjiLesson.Add(x);
                    if (!string.IsNullOrEmpty(x.LastLearning)) lastLsKanji++;
                }
            });

            _model.StartMode = StartModeEnum.LearnNormal;
            _model.SubStartMode = SubStartModeEnum.Sequentially;

            if (_model.SubStartMode == SubStartModeEnum.Sequentially)
            {
                rdbxSequentially.IsChecked = true;
            }
            else
            {
                rdbxRandom.IsChecked = true;
            }
            cbbxStartMode.SelectedIndex = (int)StartModeEnum.LearnNormal;

            if (_model.ListVocabularyLesson.Count != 0)
            {
                _model.ListVocabularyLesson[lastLsVocabulary].IsSelected = true;
                lastLsVocabulary = (lastLsVocabulary + 3) >= _model.ListVocabularyLesson.Count ? _model.ListVocabularyLesson.Count - 1 : (lastLsVocabulary + 3);
                dgv_Lesson_TV.ScrollIntoView(_model.ListVocabularyLesson[lastLsVocabulary]);
            }

            if (_model.ListKanjiLesson.Count != 0)
            {
                _model.ListKanjiLesson[lastLsKanji].IsSelected = true;
                lastLsKanji = (lastLsKanji + 3) >= _model.ListKanjiLesson.Count ? _model.ListKanjiLesson.Count - 1 : (lastLsKanji + 3);
                dgv_Lesson_KJ.ScrollIntoView(_model.ListKanjiLesson[lastLsKanji]);
            }

            tabFlashCard.Visibility = Visibility.Hidden;
            tabShowAll.Visibility = Visibility.Hidden;
            tabSelectLesson.Visibility = Visibility.Visible;
        }

        private void SubStartMode_Checked(object sender, RoutedEventArgs e)
        {
            _model.SubStartMode = SubStartModeEnum.Sequentially;
        }

        private void SubStartMode_UnChecked(object sender, RoutedEventArgs e)
        {
            _model.SubStartMode = SubStartModeEnum.Random;
        }

        private void cbbxStartMode_Change(object sender, SelectionChangedEventArgs e)
        {
            ComboBox temp = (ComboBox)sender;
            switch (temp.SelectedIndex)
            {
                case 0:
                    _model.StartMode = StartModeEnum.ViewListWord;
                    break;
                case 1:
                    _model.StartMode = StartModeEnum.LearnNormal;
                    _model.IsCheckRepeat = true;
                    break;
                case 2:
                    _model.StartMode = StartModeEnum.OnlyHard;
                    _model.IsCheckRepeat = false;
                    break;
                case 3:
                    _model.StartMode = StartModeEnum.Remind;
                    _model.IsCheckRepeat = false;
                    break;
                default:
                    _model.StartMode = StartModeEnum.LearnNormal;
                    _model.IsCheckRepeat = true;
                    break;
            }
            subStartModeDiv.Visibility = _model.StartMode == StartModeEnum.ViewListWord ? Visibility.Hidden : Visibility.Visible;
        }

        private void cbbxLevel_Change(object sender, SelectionChangedEventArgs e)
        {
            ComboBox temp = (ComboBox)sender;
            _model.Level = CommonUtils.ConvertLevelEnum(temp.SelectedIndex.ToString());
            _logic.SaveLevelConfig(_model.Level);
            InitData();
        }

        private void cbxVocabulary_SelectChange(object sender, RoutedEventArgs e)
        {
            int count = _model.ListVocabularyLesson.Count;
            string str = string.Empty;
            for (int i = 0; i < count; i++)
            {
                str = str + (_model.ListVocabularyLesson[i].IsSelected ? _model.ListVocabularyLesson[i].LessonName + Environment.NewLine : string.Empty);
            }
            tbxVocabularySum.Text = str;
        }

        private void cbxKanji_SelectChange(object sender, RoutedEventArgs e)
        {
            int count = _model.ListKanjiLesson.Count;
            string str = string.Empty;
            for (int i = 0; i < count; i++)
            {
                str = str + (_model.ListKanjiLesson[i].IsSelected ? _model.ListKanjiLesson[i].LessonName + Environment.NewLine : string.Empty);
            }
            tbxKanjiSum.Text = str;
        }

        private void btnUnSelectAllVocabulary_Click(object sender, RoutedEventArgs e)
        {
            int count = _model.ListVocabularyLesson.Count;
            for (int i = 0; i < count; i++)
            {
                _model.ListVocabularyLesson[i].IsSelected = false;
            }
        }

        private void btnUnSelectAllKanji_Click(object sender, RoutedEventArgs e)
        {
            int count = _model.ListKanjiLesson.Count;
            for (int i = 0; i < count; i++)
            {
                _model.ListKanjiLesson[i].IsSelected = false;
            }
        }

        private void btnStartLearn_Click(object sender, RoutedEventArgs e)
        {
            List<LessonModel> listLesson = new List<LessonModel>();
            for (int i = 0; i < _model.ListVocabularyLesson.Count; i++)
            {
                if (_model.ListVocabularyLesson[i].IsSelected)
                {
                    listLesson.Add(_model.ListVocabularyLesson[i]);
                }
            }
            for (int i = 0; i < _model.ListKanjiLesson.Count; i++)
            {
                if (_model.ListKanjiLesson[i].IsSelected)
                {
                    listLesson.Add(_model.ListKanjiLesson[i]);
                }
            }
            List<WordModel> wordList = _logic.GetListWordByLesson(listLesson, _model.StartMode, _model.IsCheckRepeat);
            if (_model.SubStartMode == SubStartModeEnum.Random && _model.StartMode != StartModeEnum.ViewListWord)
            {
                _model.CurrentListWord = new ObservableCollection<WordModel>(_logic.RandomWordInList(wordList));
            }
            else
            {
                _model.CurrentListWord = new ObservableCollection<WordModel>(wordList);
            }

            if (_model.CurrentListWord.Count == 0)
            {
                MessageBox.Show("Không có từ vựng nào!");
                return;
            }

            if (_model.StartMode == StartModeEnum.ViewListWord)
            {
                tabSelectLesson.Visibility = Visibility.Hidden;
                listWordTemp = new List<WordModel>();
                foreach (WordModel x in _model.CurrentListWord)
                {
                    listWordTemp.Add(x.Clone());
                }
                tabShowAll.Visibility = Visibility.Visible;

                KanjiCbx.IsChecked = true;
                HiraCbx.IsChecked = true;
                MeanCbx.IsChecked = true;
            }
            else
            {
                tabSelectLesson.Visibility = Visibility.Hidden;
                tabFlashCard.Visibility = Visibility.Visible;

                _model.CurrentWord = _model.CurrentListWord[0];
                listWordIdShowed = new List<int>();
                listWordIdShowed.Add(_model.CurrentWord.Id);
                wordIndexInListShowed = listWordIdShowed.Count;

                processbar.Maximum = _model.CurrentListWord.Count;
                processbar.Value = 0;
                tbxHiragana.Focus();
            }
        }
        #endregion

        #region Learning Screen
        private void tabLearningKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabFlashCard.Visibility == Visibility.Hidden) return;
            if (e.Key == Key.Escape)
            {
                this.btnBackToMenu_Click();
            }
            else if (e.Key == Key.Right || e.Key == Key.N)
            {
                if (_model.CurrentWord.IsDisplayed)
                {
                    WordModel temp = null;
                    if (wordIndexInListShowed == listWordIdShowed.Count)
                    {
                        tbxMean.Visibility = Visibility.Hidden;
                        _model.CurrentWord.LastLearn = DateTime.Now;
                        temp = getNextWord();
                        if (temp == null)
                        {
                            MessageBox.Show("Đã học xong!");
                            SaveDataLastLearn();
                            tabSelectLesson.Visibility = Visibility.Visible;
                            tabFlashCard.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            _model.CurrentWord = temp;
                            listWordIdShowed.Add(_model.CurrentWord.Id);
                            wordIndexInListShowed = listWordIdShowed.Count;
                        }
                        processbar.Value = _model.CurrentListWord.Count(x => x.IsDisplayed);
                    }
                    else
                    {
                        temp = _model.CurrentListWord.FirstOrDefault(x => x.Id == listWordIdShowed.Last());
                        _model.CurrentWord = temp;
                        _model.CurrentWord.IsDisplayed = false;
                        wordIndexInListShowed = listWordIdShowed.Count;
                        tbxMean.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    _model.CurrentWord.IsDisplayed = true;
                    tbxMean.Visibility = Visibility.Visible;
                }
            }
            else if (e.Key == Key.Left || e.Key == Key.B)
            {
                if (wordIndexInListShowed > 0)
                {
                    wordIndexInListShowed = wordIndexInListShowed == listWordIdShowed.Count && wordIndexInListShowed != 1 ? (wordIndexInListShowed - 2) : (wordIndexInListShowed - 1);
                    _model.CurrentWord = _model.CurrentListWord.FirstOrDefault(x => x.Id == listWordIdShowed[wordIndexInListShowed]).Clone();
                    _model.CurrentWord.IsDisplayed = true;
                    tbxMean.Visibility = Visibility.Visible;
                }
            }
        }

        private void SaveDataLastLearn()
        {
            _logic.UpdateWordLastLearn(_model.CurrentListWord);

            List<LessonModel> listModelSelected = new List<LessonModel>();
            foreach (LessonModel vocabularyLesson in _model.ListVocabularyLesson)
            {
                if (vocabularyLesson.IsSelected)
                {
                    vocabularyLesson.IsSelected = false;
                    vocabularyLesson.LastLearning = DateTime.Now.ToString(Constant.DATETIME_FORMAT);
                    listModelSelected.Add(vocabularyLesson);
                }
            }

            foreach (LessonModel kanjiLesson in _model.ListKanjiLesson)
            {
                if (kanjiLesson.IsSelected)
                {
                    kanjiLesson.IsSelected = false;
                    kanjiLesson.LastLearning = DateTime.Now.ToString(Constant.DATETIME_FORMAT);
                    listModelSelected.Add(kanjiLesson);
                }
            }

            _logic.UpdateLessonLastLearn(listModelSelected);
        }

        private WordModel getNextWord()
        {
            WordModel result = null;
            int index = _model.CurrentListWord.IndexOf(_model.CurrentWord);
            _model.CurrentWord.IsDisplayed = !_model.CurrentWord.IsRepeat;
            result = _model.CurrentListWord.FirstOrDefault(x => !x.IsDisplayed && _model.CurrentListWord.IndexOf(x) > index);


            if (result == null)
            {
                result = _model.CurrentListWord.FirstOrDefault(x => x.IsRepeat);
            }

            return result;
        }
        #endregion

        #region Show All Screen
        private void btnBackToMenu_Click(object sender = null, RoutedEventArgs e = null)
        {
            tabSelectLesson.Visibility = Visibility.Visible;
            tabFlashCard.Visibility = Visibility.Hidden;
            tabShowAll.Visibility = Visibility.Hidden;
        }

        private void btnSaveListWord_Click(object sender, RoutedEventArgs e)
        {
            _logic.UpdateWordHardAndLock(_model.CurrentListWord);
            btnBackToMenu_Click();
        }

        private void tabShowAllCbxChange(object sender, RoutedEventArgs e)
        {
            if (KanjiCbx.IsChecked == true)
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    if (string.IsNullOrEmpty(x.Kanji))
                    {
                        x.Kanji = listWordTemp.First(y => y.Id == x.Id).Kanji;
                    }
                }
            }
            else
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    x.Kanji = string.Empty;
                }
            }

            if (HiraCbx.IsChecked == true)
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    if (string.IsNullOrEmpty(x.Hiragana))
                    {
                        x.Hiragana = listWordTemp.First(y => y.Id == x.Id).Hiragana;
                    }
                }
            }
            else
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    x.Hiragana = string.Empty;
                }
            }

            if (MeanCbx.IsChecked == true)
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    if (string.IsNullOrEmpty(x.Mean))
                    {
                        x.Mean = listWordTemp.First(y => y.Id == x.Id).Mean;
                    }
                }
            }
            else
            {
                foreach (WordModel x in _model.CurrentListWord)
                {
                    x.Mean = string.Empty;
                }
            }

        }
        #endregion

        private void btnGenJson_Click(object sender, RoutedEventArgs e)
        {
            _logic.UpdateWordTypeOfWordBook();

            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            List<TuVungJson> tuVungData = new List<TuVungJson>();
            List<KanjiJson> kanjiData = new List<KanjiJson>();
            List<GrammarJson> grammarData = new List<GrammarJson>();

            Enum.GetValues(typeof(LevelEnum)).Cast<LevelEnum>().ToList().ForEach(level =>
            {
                List<LessonModel> lessonList = _logic.GetListLesson(level);
                lessonList.ForEach(lesson =>
                {
                    if (lesson.Type != null && lesson.Type.Equals("TV"))
                    {
                        TuVungJson tuVung = new TuVungJson() { Lesson = lesson.LessonName, Level = lesson.Level.ToString(), Data = new List<TuVungItem>() };
                        List<WordModel> wordList = _logic.GetListWordByLesson(new List<LessonModel>() { lesson }, StartModeEnum.ViewListWord, false);
                        wordList.ForEach(word =>
                        {
                            tuVung.Data.Add(new TuVungItem() { Id = word.Id, Type = word.WordType, Hira = word.Hiragana, Kanji = word.Kanji, Mean = word.Mean });
                        });
                        tuVungData.Add(tuVung);
                    }
                    else if (lesson.Type != null && lesson.Type.Equals("KJ"))
                    {
                        KanjiJson kanji = new KanjiJson() { Lesson = lesson.LessonName, Level = lesson.Level.ToString(), Data = new List<KanjiItem>() };
                        List<WordModel> wordList = _logic.GetListWordByLesson(new List<LessonModel>() { lesson }, StartModeEnum.ViewListWord, false);
                        wordList.ForEach(word =>
                        {
                            kanji.Data.Add(new KanjiItem() { Id = word.Id, Hira = word.Hiragana, Kanji = word.Kanji, CnVi = word.CnVi, Mean = word.Mean });
                        });
                        kanjiData.Add(kanji);
                    }
                    else
                    {
                        GrammarJson grammar = new GrammarJson() { Lesson = lesson.LessonName, Level = lesson.Level.ToString(), Data = new List<GrammarItem>() };
                        List<GrammarEntity> grammarList = _logic.GetListGrammarByLesson(new List<LessonModel>() { lesson });
                        grammarList.ForEach(gm =>
                        {
                            grammar.Data.Add(new GrammarItem() { Id = gm.Id, Label = gm.Label, Grammar = gm.Grammar, Mean = gm.Mean, Example = gm.Example });
                        });
                        grammarData.Add(grammar);
                    }
                });

            });

            string tuVungText = "var tuVung = " + Environment.NewLine + jsSer.Serialize(tuVungData);
            string kanjiText = "var kanji = " + Environment.NewLine + jsSer.Serialize(kanjiData);
            string grammarText = "var grammar = " + Environment.NewLine + jsSer.Serialize(grammarData);

            string pathTV = Path.Combine(@"D:\Github\JpT\js\data", "tuvung.js");
            File.WriteAllText(pathTV, tuVungText);
            string pathKJ = Path.Combine(@"D:\Github\JpT\js\data", "kanji.js");
            File.WriteAllText(pathKJ, kanjiText);
            string pathGM = Path.Combine(@"D:\Github\JpT\js\data", "grammar.js");
            File.WriteAllText(pathGM, grammarText);

            MessageBox.Show("Done!" + Environment.NewLine + pathTV + Environment.NewLine + pathKJ + Environment.NewLine + pathGM);

            Application.Current.Shutdown();
        }
    }

    public class TuVungJson
    {
        public string Lesson { get; set; }
        public string Level { get; set; }
        public List<TuVungItem> Data { get; set; }
    }

    public class TuVungItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Hira { get; set; }
        public string Kanji { get; set; }
        public string Mean { get; set; }
    }

    public class KanjiJson
    {
        public string Lesson { get; set; }
        public string Level { get; set; }
        public List<KanjiItem> Data { get; set; }
    }

    public class KanjiItem
    {
        public int Id { get; set; }
        public string Hira { get; set; }
        public string Kanji { get; set; }
        public string CnVi { get; set; }
        public string Mean { get; set; }
    }

    public class GrammarJson
    {
        public string Lesson { get; set; }
        public string Level { get; set; }
        public List<GrammarItem> Data { get; set; }

    }
    public class GrammarItem
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Grammar { get; set; }
        public string Mean { get; set; }
        public string Example { get; set; }
    }
}
