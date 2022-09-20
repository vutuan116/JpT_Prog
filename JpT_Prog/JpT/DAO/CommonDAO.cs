using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JpT.DAO
{
   public class CommonDAO
   {
      private static ExcelUtils _excel = new ExcelUtils(Constant.FILE_DATA);
      private static List<KotobaEntity> _listKotoba = new List<KotobaEntity>();
      private static List<LessonModel> _listLessonKotoba = new List<LessonModel>();
      private static List<string> _listLastLearningKotoba = new List<string>();
      private static List<KanjiEntity> _listKanji = new List<KanjiEntity>();
      private static List<LessonModel> _listLessonKanji = new List<LessonModel>();
      private static List<string> _listLastLearningKanji = new List<string>();

      public static List<KotobaEntity> KotobaGetAllEntity()
      {
         return _listKotoba;
      }

      public static List<string> KotobaGetListLastLearning()
      {
         return _listLastLearningKotoba;
      }

      public static List<LessonModel> KotobaGetListLesson()
      {
         return _listLessonKotoba;
      }

      public static List<KotobaEntity> KotobaGetByLesson(string lesson = "")
      {
         return _listKotoba.Where(x => x.Lesson.Equals(lesson)).ToList();
      }

      public static void KotobaUpdateDataIsHardAndLock(List<KotobaEntity> listKotobaEntity)
      {
         _excel.ws_GetBySheetIndex(0);

         foreach (KotobaEntity entity in listKotobaEntity)
         {
            _excel.cell_WriteByIndex(entity.Id, Constant.KOTOBA_COLUMN_IS_HARD, entity.Hard);
            _excel.cell_WriteByIndex(entity.Id, Constant.KOTOBA_COLUMN_LOCK, entity.Lock);

            int index = _listKotoba.IndexOf(entity);

            if (index != -1)
            {
               _listKotoba[index].Hard = entity.Hard;
               _listKotoba[index].Lock = entity.Lock;
            }
         }
         _excel.Workbook_Save(Constant.FILE_DATA);
      }

      public static void KotobaUpdateData(List<LessonModel> lessonList)
      {
         _excel.ws_GetBySheetIndex(0);
         string now = DateTime.Now.ToString("yyyy/MM/dd");
         foreach (LessonModel lesson in lessonList)
         {
            if (lesson.IsSelected)
            {
               List<KotobaEntity> kotobaList = KotobaGetByLesson(lesson.LessonName);
               foreach (KotobaEntity entity in kotobaList)
               {
                  _excel.cell_WriteByIndex(entity.Id, Constant.KOTOBA_COLUMN_LAST_LEARNING, now);
               }
               foreach (LessonModel lessonModel in _listLessonKotoba)
               {
                  if (lessonModel.LessonName.Equals(lesson.LessonName))
                  {
                     lessonModel.LastLearning = now;
                     break;
                  }
               }
            }
         }
         _excel.Workbook_Save(Constant.FILE_DATA);
      }
      public static List<KotobaEntity> KotobaGetWasLearned()
      {
         return _listKotoba.Where(x => (!string.IsNullOrEmpty(x.LastLearning))).ToList();
      }

      public static List<KanjiEntity> KanjiGetAllEntity()
      {
         return _listKanji;
      }

      public static List<string> KanjiGetListLastLearning()
      {
         return _listLastLearningKanji;
      }

      public static List<LessonModel> KanjiGetListLesson()
      {
         return _listLessonKanji;
      }

      public static List<KanjiEntity> KanjiGetByLesson(string lesson = "")
      {
         return _listKanji.Where(x => x.Lesson.Equals(lesson)).ToList();
      }

      public static void KanjiUpdateDataIsHardAndLock(List<KanjiEntity> listKanjiEntity)
      {
         _excel.ws_GetBySheetIndex(1);

         foreach (KanjiEntity entity in listKanjiEntity)
         {
            _excel.cell_WriteByIndex(entity.Id, Constant.KANJI_COLUMN_IS_HARD, entity.Hard);
            _excel.cell_WriteByIndex(entity.Id, Constant.KANJI_COLUMN_LOCK, entity.Lock);

            int index = _listKanji.IndexOf(entity);

            if (index != -1)
            {
               _listKanji[index].Hard = entity.Hard;
               _listKanji[index].Lock = entity.Lock;
            }
         }
         _excel.Workbook_Save(Constant.FILE_DATA);
      }

      public static void KanjiUpdateData(List<LessonModel> lessonList)
      {
         _excel.ws_GetBySheetIndex(1);
         string now = DateTime.Now.ToString("yyyy/MM/dd");
         foreach (LessonModel lesson in lessonList)
         {
            if (lesson.IsSelected)
            {
               List<KanjiEntity> kanjiList = KanjiGetByLesson(lesson.LessonName);
               foreach (KanjiEntity entity in kanjiList)
               {
                  _excel.cell_WriteByIndex(entity.Id, Constant.KANJI_COLUMN_LAST_LEARNING, now);
               }
               foreach (LessonModel lessonModel in _listLessonKanji)
               {
                  if (lessonModel.LessonName.Equals(lesson.LessonName))
                  {
                     lessonModel.LastLearning = now;
                     lessonModel.IsSelected = false;
                     break;
                  }
               }
            }
         }
         _excel.Workbook_Save(Constant.FILE_DATA);
      }

      public static List<KanjiEntity> KanjiGetWasLearned()
      {
         return _listKanji.Where(x => (!string.IsNullOrEmpty(x.LastLearning))).ToList();
      }

      public static bool InitData()
      {
         try
         {
            #region get data kotoba
            string listKotobaStr = string.Empty;
            _excel.ws_GetBySheetIndex(0);
            int countRows = _excel.ws_GetCountRow();
            for (int i = 3; i < countRows; i++)
            {
               KotobaEntity entity = new KotobaEntity();
               entity.Id = i;
               entity.Lesson = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_LESSON);
               entity.Content = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_CONTENT);
               entity.Hiragana = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_HIRAGANA);
               if (string.IsNullOrEmpty(entity.Content))
               {
                  entity.Content = entity.Hiragana;
                  entity.Hiragana = string.Empty;
               }
               entity.Meaning = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_MEANING).ToLower();
               entity.Type = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_TYPE);
               entity.Hard = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_IS_HARD);
               entity.Lock = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_LOCK);
               entity.HistoryAnswer = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_HISTORY);
               entity.MeaningOther = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_OTHER_MEANING).ToLower();
               entity.LastLearning = _excel.cell_GetValueByCell(i, Constant.KOTOBA_COLUMN_LAST_LEARNING);

               if (entity.isEmpty())
               {
                  continue;
               }

               _listKotoba.Add(entity);

               if (!listKotobaStr.Contains(entity.Lesson))
               {
                  listKotobaStr += entity.Lesson;
                  _listLessonKotoba.Add(new LessonModel() { LessonName = entity.Lesson, LastLearning = entity.LastLearning, IsSelected = false });
                  _listLastLearningKotoba.Add(entity.LastLearning);
               }

            }
            #endregion

            #region get data kanji
            string listKanjiStr = string.Empty;
            _excel.ws_GetBySheetIndex(1);
            countRows = _excel.ws_GetCountRow();
            for (int i = 3; i <= countRows; i++)
            {
               KanjiEntity entity = new KanjiEntity();
               entity.Id = i;
               entity.Lesson = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_LESSON);
               entity.Kanji = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_KANJI);
               entity.Hiragana = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_HIRAGANA);
               entity.HanViet = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_HANVIET);
               entity.Meaning = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_MEANING).ToLower();
               entity.Hard = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_IS_HARD);
               entity.Lock = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_LOCK);
               entity.HistoryAnswer = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_HISTORY);
               entity.MeaningOther = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_OTHER_MEANING).ToLower();
               entity.LastLearning = _excel.cell_GetValueByCell(i, Constant.KANJI_COLUMN_LAST_LEARNING);

               if (entity.isEmpty())
               {
                  continue;
               }

               _listKanji.Add(entity);

               if (!listKanjiStr.Contains(entity.Lesson))
               {
                  listKanjiStr += entity.Lesson;
                  _listLessonKanji.Add(new LessonModel() { LessonName = entity.Lesson, LastLearning = entity.LastLearning, IsSelected = false });
                  _listLastLearningKanji.Add(entity.LastLearning);
               }
            }
            #endregion

            return true;
         }
         catch
         {
            return false;
         }
      }

      private static bool IsLock(KotobaEntity kotoba, KanjiEntity kanji = null)
      {
         if (kotoba == null && kanji == null || kotoba != null && kanji != null)
            return false;

         int level = int.TryParse(kotoba != null ? kotoba.Hard : kanji.Hard, out level) ? level : 5;
         string historyAnswer = kotoba != null ? kotoba.HistoryAnswer : kanji.HistoryAnswer;

         if (historyAnswer.Contains(Constant.DF_ANSWER_FALSE) || historyAnswer.Length < Constant.RULE_LOCK[level - 1])
            return false;
         return true;
      }
   }
}
