using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;

namespace JpT.Logic
{
    public class ViewKanjiNewLogic
    {
        private List<KanjiModel> _listKanjiModel = new List<KanjiModel>();
        private List<KanjiEntity> _listKanjiEntity = null;

        public bool IsFinish { get; set; }
        public string Lesson { get; set; }
        public int CountPage { get; set; }

        public ViewKanjiNewLogic()
        {
            List<string> listLastLearning = CommonDAO.KanjiGetListLastLearning();
            List<LessonModel> listLesson = CommonDAO.KanjiGetListLesson();

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
            _listKanjiEntity = CommonDAO.KanjiGetByLesson(Lesson);

            foreach (KanjiEntity entity in _listKanjiEntity)
            {
                KanjiModel model = CommonUtils.MappingData<KanjiModel>(entity);
                model.VisibleRow = "Visible";

                _listKanjiModel.Add(model);
            }

            CountPage = _listKanjiEntity.Count / 8;
            CountPage = _listKanjiEntity.Count % 8 == 0 ? CountPage : CountPage + 1;

            IsFinish = false;
        }

        public List<KanjiModel> GetKanjiByPage(int page)
        {
            page = page * 8;
            int length = _listKanjiModel.Count - page;

            if (length > 8)
            {
                length = 8;
            }
            else
            {
                IsFinish = true;
            }

            List<KanjiModel> result = _listKanjiModel.GetRange(page, length);

            while (result.Count < 8)
            {
                result.Add(new KanjiModel());
            }

            return result;
        }

        //internal void updateData()
        //{
        //    for (int i = 0; i < _listKanjiEntity.Count; i++)
        //    {
        //        _listKanjiEntity[i].MeaningOther = _listKanjiModel[i].MeaningOther;
        //        _listKanjiEntity[i].HistoryAnswer = _listKanjiModel[i].HistoryAnser;
        //        _listKanjiEntity[i].LastLearning = DateTime.Now.ToString(Constant.DF_DATETIME_FORMAT);
        //    }
        //    CommonDAO.KanjiUpdateData(_listKanjiEntity);
        //}
    }
}
