using JpT.Entity;
using JpT.Logic;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JpT.DAO
{
    public class WordDAO
    {
        private ExcelUtils _excel = new ExcelUtils(Constant.FILE_DATA);
        private List<WordEntity> _wordOriginal = new List<WordEntity>();
        private List<LessonModel> _lessonOriginal = new List<LessonModel>();
        private List<GrammarEntity> _grammarOriginal = new List<GrammarEntity>();
        private ConfigEntity _config = new ConfigEntity();

        public WordDAO()
        {
            InitData();
            InitConfig();
        }

        public List<WordEntity> GetAllWordEntity()
        {
            List<WordEntity> clone = new List<WordEntity>();
            _wordOriginal.ForEach(x =>
            {
                clone.Add(x.Clone());
            });
            return clone;
        }

        public List<LessonModel> GetListLesson(LevelEnum level)
        {
            List<LessonModel> result = new List<LessonModel>();
            _lessonOriginal.FindAll(x => x.Level == level).ForEach(lesson =>
            {
                LessonHistoryEntity lessonHistoryEntity = _config.LessonHistoryList.FirstOrDefault(ls => ls.Name.Equals(lesson.LessonName));
                if (lessonHistoryEntity != null)
                {
                    lesson.LastLearning = lessonHistoryEntity.LastLearn;
                }
                result.Add(lesson);
            });
            return result;
        }

        public List<GrammarEntity> GetListGrammarByLesson(LessonModel lesson)
        {
            return _grammarOriginal.Where(x => x.Lesson.Equals(lesson.LessonName)).ToList();
        }

        public LevelEnum GetLevelConfig()
        {
            return _config.LevelSelected;
        }

        public List<WordEntity> GetDataByLesson(LessonModel lesson)
        {
            return _wordOriginal.Where(x => x.Lesson.Equals(lesson.LessonName) && x.Type.Equals(lesson.Type)).ToList();
        }

        public void UpdateWordHardAndLock(List<WordEntity> listDataEntity)
        {
            _excel.ws_GetBySheetIndex(0);

            foreach (WordEntity entity in listDataEntity)
            {
                _excel.cell_WriteByIndex(entity.Id, Constant.DATA_COL_IS_HARD, entity.IsHard);
                _excel.cell_WriteByIndex(entity.Id, Constant.DATA_COL_LOCK, entity.Lock);

                int index = _wordOriginal.IndexOf(entity);

                if (index != -1)
                {
                    _wordOriginal[index].IsHard = entity.IsHard;
                    _wordOriginal[index].Lock = entity.Lock;
                }
            }
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public void UpdateWordLastLearn(List<WordEntity> listDataEntity)
        {
            _excel.ws_GetBySheetIndex(0);

            foreach (WordEntity entity in listDataEntity)
            {
                _excel.cell_WriteByIndex(entity.Id, Constant.DATA_COL_LAST_LEARN, entity.LastLearn);

                int index = _wordOriginal.IndexOf(entity);

                if (index != -1)
                {
                    _wordOriginal[index].LastLearn = entity.LastLearn;
                }
            }
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public void UpdateLessonHistory(List<LessonHistoryEntity> listLessonHistoryEntity)
        {
            _excel.ws_GetBySheetIndex(2);

            foreach (LessonHistoryEntity entity in listLessonHistoryEntity)
            {
                int index = _config.LessonHistoryList.IndexOf(entity);
                int countNewLine = 1;
                if (index != -1)
                {
                    _config.LessonHistoryList[index].LastLearn = entity.LastLearn;
                    entity.Id = _config.LessonHistoryList[index].Id;
                }
                else
                {
                    entity.Id = _excel.ws_GetCountRow() + countNewLine;
                    countNewLine++;
                    _config.LessonHistoryList.Add(entity);
                }
                _excel.cell_WriteByIndex(entity.Id, Constant.CONFIG_COL_NAME, entity.Name);
                _excel.cell_WriteByIndex(entity.Id, Constant.CONFIG_COL_LAST_LEARN, entity.LastLearn);

                //_lessonOriginal.FirstOrDefault(x => x.LessonName.Equals(entity.Name)).LastLearning = entity.LastLearn;
            }
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public void UpdateWordType(List<WordEntity> listDataEntity)
        {
            _excel.ws_GetBySheetIndex(0);

            foreach (WordEntity entity in listDataEntity)
            {
                _excel.cell_WriteByIndex(entity.Id, Constant.DATA_COL_WORD_TYPE, entity.WordType);

                int index = _wordOriginal.IndexOf(entity);

                if (index != -1)
                {
                    _wordOriginal[index].LastLearn = entity.LastLearn;
                }
            }
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public void SaveLevelConfig(LevelEnum level)
        {
            _excel.ws_GetBySheetIndex(2);
            _excel.cell_WriteByIndex(3, Constant.CONFIG_COL_LEVEL, (int)level);
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public List<WordEntity> GetWordRemind()
        {
            List<WordEntity> listWordLearned = _wordOriginal.Where(x => !string.IsNullOrEmpty(x.LastLearn) && !string.IsNullOrEmpty(x.IsHard) && string.IsNullOrEmpty(x.Lock)).OrderBy(x => x.LastLearn).Take(Constant.MAX_COUNT_WORD_REMIND).ToList();
            return listWordLearned;
        }

        private bool InitConfig()
        {
            try
            {
                _config.LessonHistoryList = new List<LessonHistoryEntity>();
                _excel.ws_GetBySheetIndex(2);

                int countRows = _excel.ws_GetCountRow();
                for (int i = 3; i <= countRows; i++)
                {
                    LessonHistoryEntity entity = new LessonHistoryEntity();
                    entity.Id = i;
                    entity.Name = _excel.cell_GetValueByCell(i, Constant.CONFIG_COL_NAME);
                    entity.LastLearn = _excel.cell_GetValueByCell(i, Constant.CONFIG_COL_LAST_LEARN);
                    _config.LessonHistoryList.Add(entity);
                }
                string levelStr = _excel.cell_GetValueByCell(3, Constant.CONFIG_COL_LEVEL);
                switch (levelStr)
                {
                    case "4":
                        _config.LevelSelected = LevelEnum.N1;
                        break;
                    case "3":
                        _config.LevelSelected = LevelEnum.N2;
                        break;
                    case "2":
                        _config.LevelSelected = LevelEnum.N3;
                        break;
                    case "1":
                        _config.LevelSelected = LevelEnum.N4;
                        break;
                    default:
                        _config.LevelSelected = LevelEnum.N5;
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InitData()
        {
            try
            {
                string listKotobaStr = string.Empty;
                _excel.ws_GetBySheetIndex(0);
                int countRows = _excel.ws_GetCountRow();
                for (int i = 2; i <= countRows; i++)
                {
                    WordEntity entity = new WordEntity();
                    entity.Id = i;
                    entity.Type = _excel.cell_GetValueByCell(i, Constant.DATA_COL_TYPE);
                    entity.Level = _excel.cell_GetValueByCell(i, Constant.DATA_COL_LEVEL);
                    entity.Lesson = _excel.cell_GetValueByCell(i, Constant.DATA_COL_LESSON);
                    entity.Kanji = _excel.cell_GetValueByCell(i, Constant.DATA_COL_KANJI);
                    entity.Hiragana = _excel.cell_GetValueByCell(i, Constant.DATA_COL_HIRAGANA);
                    entity.CnVi = _excel.cell_GetValueByCell(i, Constant.DATA_COL_CNVI);
                    entity.Mean = _excel.cell_GetValueByCell(i, Constant.DATA_COL_MEANING);
                    entity.IsHard = _excel.cell_GetValueByCell(i, Constant.DATA_COL_IS_HARD);
                    entity.Lock = _excel.cell_GetValueByCell(i, Constant.DATA_COL_LOCK);
                    entity.LastLearn = _excel.cell_GetValueByCell(i, Constant.DATA_COL_LAST_LEARN);
                    entity.WordType = _excel.cell_GetValueByCell(i, Constant.DATA_COL_WORD_TYPE);
                    if (entity.IsEmpty() || !string.IsNullOrEmpty(entity.Lock))
                    {
                        continue;
                    }
                    _wordOriginal.Add(entity);
                }


                _excel.ws_GetBySheetIndex(1);
                countRows = _excel.ws_GetCountRow();
                for (int i = 2; i <= countRows; i++)
                {
                    GrammarEntity entity = new GrammarEntity();
                    entity.Id = "GM_" + i;
                    entity.Level = _excel.cell_GetValueByCell(i, 2);
                    entity.Lesson = _excel.cell_GetValueByCell(i, 3);

                    entity.Label = _excel.cell_GetValueByCell(i, 4);
                    entity.Grammar = _excel.cell_GetValueByCell(i, 5);
                    entity.Mean = _excel.cell_GetValueByCell(i, 6);
                    entity.Example = _excel.cell_GetValueByCell(i, 7);

                    FormatGrammar(i, entity);

                    if (entity.IsEmpty())
                    {
                        continue;
                    }
                    _grammarOriginal.Add(entity);
                }

                _wordOriginal.Select(x => x.Lesson).Distinct().ToList().ForEach(x =>
                {
                    WordEntity wordFirst = _wordOriginal.FirstOrDefault(wf => wf.Lesson.Equals(x));
                    LessonModel ls = new LessonModel() { LessonName = wordFirst.Lesson, Type = wordFirst.Type, Level = CommonUtils.ConvertLevelEnum(wordFirst.Level) };
                    _lessonOriginal.Add(ls);
                });

                _grammarOriginal.Select(x => x.Lesson).Distinct().ToList().ForEach(x =>
                {
                    GrammarEntity grammarFirst = _grammarOriginal.FirstOrDefault(wf => wf.Lesson.Equals(x));
                    LessonModel ls = new LessonModel() { LessonName = grammarFirst.Lesson, Level = CommonUtils.ConvertLevelEnum(grammarFirst.Level) };
                    _lessonOriginal.Add(ls);
                });

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void FormatGrammar(int row, GrammarEntity entity)
        {
            if (entity.Label == null || entity.Grammar == null || entity.Example == null) { return; }

            string speChar = "ー・（）＆．＋？＝";
            string norChar = "-/()&.+?=";
            bool needFormat = false;

            for (int i = 0; i < speChar.Length; i++)
            {
                if (entity.Label.Contains(speChar[i]))
                {
                    entity.Label= entity.Label.Replace(speChar[i], norChar[i]);
                    needFormat = true;
                }
                if (entity.Grammar.Contains(speChar[i]))
                {
                    entity.Grammar = entity.Grammar.Replace(speChar[i], norChar[i]);
                    needFormat = true;
                }
                if (entity.Example.Contains(speChar[i]))
                {
                    entity.Example = entity.Example.Replace(speChar[i], norChar[i]);
                    needFormat = true;
                }
            }

            if (needFormat)
            {
                _excel.cell_WriteByIndex(row, 4, entity.Label);
                _excel.cell_WriteByIndex(row, 5, entity.Grammar);
                _excel.cell_WriteByIndex(row, 7, entity.Example);
            }

        }
    }
}
