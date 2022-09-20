using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JpT.Logic
{
   public class ViewKanjiFlashcard
   {
      public ObservableCollection<LessonModel> GetListLesson(TabName tabname)
      {
         ObservableCollection<LessonModel> result = null;
         if (tabname == TabName.Kanji)
         {
            result = new ObservableCollection<LessonModel>(CommonDAO.KanjiGetListLesson());
         }
         else
         {
            result = new ObservableCollection<LessonModel>(CommonDAO.KotobaGetListLesson());
         }
         foreach (LessonModel lesson in result)
         {
            if (string.IsNullOrEmpty(lesson.LastLearning))
            {
               lesson.IsSelected = true;
               break;
            }
         }
         return result;
      }

      public List<KanjiModel> GetListObjByLesson(TabName tabname, LessonModel lesson, bool isOnlyHard = false, bool isGetAll = false)
      {
         List<KanjiModel> result = new List<KanjiModel>();

         if (tabname == TabName.Kanji)
         {
            List<KanjiEntity> listEntity = CommonDAO.KanjiGetByLesson(lesson.LessonName);

            foreach (KanjiEntity entity in listEntity)
            {
               KanjiModel model = CommonUtils.MappingData<KanjiModel>(entity);
               model.IsRepeat = string.IsNullOrEmpty(lesson.LastLearning);
               model.IsHard = !string.IsNullOrEmpty(entity.Hard);
               model.IsLock = !string.IsNullOrEmpty(entity.Lock);

               if (isGetAll)
               {
                  result.Add(model);
               }
               else if (isOnlyHard)
               {
                  if (model.IsHard) result.Add(model);
               }
               else if (!model.IsHard && !model.IsLock)
               {
                  result.Add(model);
               }
            }
         }
         else
         {
            List<KotobaEntity> listEntity = CommonDAO.KotobaGetByLesson(lesson.LessonName);

            foreach (KotobaEntity entity in listEntity)
            {
               KanjiModel model = CommonUtils.MappingData<KanjiModel>(entity);
               model.IsRepeat = string.IsNullOrEmpty(lesson.LastLearning);
               model.IsHard = !string.IsNullOrEmpty(entity.Hard);
               model.IsLock = !string.IsNullOrEmpty(entity.Lock);

               if (isGetAll)
               {
                  result.Add(model);
               }
               else if (isOnlyHard)
               {
                  if (model.IsHard) result.Add(model);
               }
               else if (!model.IsHard && !model.IsLock)
               {
                  result.Add(model);
               }
            }
         }

         return result;
      }

      public void UpdateLastLearning(TabName tabName, ObservableCollection<LessonModel> lessonList)
      {
         List<LessonModel> data = new List<LessonModel>(lessonList);
         if (tabName == TabName.Kanji)
         {
            CommonDAO.KanjiUpdateData(data);
         }
         else
         {
            CommonDAO.KotobaUpdateData(data);
         }
      }

      public void SaveListHard(TabName tabName, ObservableCollection<KanjiModel> objList)
      {
         if (tabName == TabName.Kanji)
         {
            List<KanjiEntity> data = new List<KanjiEntity>();
            foreach (KanjiModel model in objList)
            {
               KanjiEntity entity = new KanjiEntity() { Id = model.Id, Hard = model.IsHard ? "1" : string.Empty, Lock = model.IsLock ? "1" : string.Empty };
               data.Add(entity);
            }
            CommonDAO.KanjiUpdateDataIsHardAndLock(data);
         }
         else
         {
            List<KotobaEntity> data = new List<KotobaEntity>();
            foreach (KanjiModel model in objList)
            {
               KotobaEntity entity = new KotobaEntity() { Id = model.Id, Hard = model.IsHard ? "1" : string.Empty, Lock = model.IsLock ? "1" : string.Empty };
               data.Add(entity);
            }
            CommonDAO.KotobaUpdateDataIsHardAndLock(data);
         }
      }
   }
}
