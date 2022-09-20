using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;

namespace JpT.Logic
{
    public class ViewKotobaNewLogic
    {
        private List<KotobaModel> _listKotobaModel = new List<KotobaModel>();
        private List<KotobaEntity> _listKotobaEntity = null;

        public bool IsFinish { get; set; }
        public string Lesson { get; set; }
        public int CountPage { get; set; }

        public ViewKotobaNewLogic()
        {
            List<string> listLastLearning = CommonDAO.KotobaGetListLastLearning();
            List<LessonModel> listLesson = CommonDAO.KotobaGetListLesson();

            int index = 0;
            for (index = 0; index < listLastLearning.Count; index++)
            {
                if (string.IsNullOrEmpty(listLastLearning[index]))
                {
                    break;
                }
            }
            if (listLastLearning.Count == index)
            {
                return;
            }
            Lesson = listLesson[index].LessonName;
            _listKotobaEntity = CommonDAO.KotobaGetByLesson(Lesson);

            foreach (KotobaEntity entity in _listKotobaEntity)
            {
                KotobaModel model = CommonUtils.MappingData<KotobaModel>(entity);
                model.VisibleRow = "Visible";

                _listKotobaModel.Add(model);
            }

            CountPage = _listKotobaEntity.Count / 10;
            CountPage = _listKotobaEntity.Count % 10 == 0 ? CountPage : CountPage + 1;

            IsFinish = false;
        }

        public List<KotobaModel> GetKotobaByPage(int page)
        {
            page = page * 10;
            int length = _listKotobaModel.Count - page;

            if (length > 10)
            {
                length = 10;
            }
            else
            {
                IsFinish = true;
            }

            List<KotobaModel> result = _listKotobaModel.GetRange(page, length);

            while (result.Count < 10)
            {
                result.Add(new KotobaModel());
            }

            return result;
        }

        //public void updateData()
        //{
        //    for (int i = 0; i < _listKotobaEntity.Count; i++)
        //    {
        //        _listKotobaEntity[i].MeaningOther = _listKotobaModel[i].MeaningOther;
        //        _listKotobaEntity[i].HistoryAnswer = _listKotobaModel[i].HistoryAnser;
        //        _listKotobaEntity[i].LastLearning = DateTime.Now.ToString(Constant.DF_DATETIME_FORMAT);
        //    }
        //    CommonDAO.KotobaUpdateDataIsHard(_listKotobaEntity);
        //}
    }
}
