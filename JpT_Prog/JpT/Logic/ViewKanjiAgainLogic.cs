using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace JpT.Logic
{
    public class ViewKanjiAgainLogic
    {
        #region method support
        private bool IsNeedLearn(KanjiEntity entity)
        {
            int level = int.TryParse(entity.Hard, out level) ? level : 5;
            return checkLearning(entity, Constant.LEARNING_AGAIN_RULE_IN[level - 1][0], Constant.LEARNING_AGAIN_RULE_IN[level - 1][1], Constant.LEARNING_AGAIN_RULE_IN[level - 1][2], Constant.LEARNING_AGAIN_RULE_IN[level - 1][3]);
        }

        private bool checkLearning(KanjiEntity entity, int count, int ratioTrue, int countLastHistory, int lastLearning)
        {
            if (entity.HistoryAnswer.Length < count)
                return true;

            int ratio = (Regex.Matches(entity.HistoryAnswer, Constant.DF_ANSWER_TRUE).Count * 100) / entity.HistoryAnswer.Length;
            if (ratio < ratioTrue)
                return true;

            string lastHistory = entity.HistoryAnswer.Substring(entity.HistoryAnswer.Length - countLastHistory);
            if (lastHistory.Contains(Constant.DF_ANSWER_FALSE))
                return true;

            int lastDateLearn = (DateTime.Now - DateTime.ParseExact(entity.LastLearning, Constant.DF_DATETIME_FORMAT, new CultureInfo("en-EN"))).Days;
            if (lastDateLearn > lastLearning)
                return true;

            return false;
        }

        private bool checkLearnDone(KanjiModel kanji)
        {
            int countTrue = kanji.HistoryAnser.Replace(Constant.DF_ANSWER_FALSE, string.Empty).Length;
            int level = int.TryParse(kanji.Level, out level) ? level : 5;

            return countTrue == Constant.LEARNING_AGAIN_RULE_OUT[level - 1];
        }
        #endregion

        private List<KanjiModel> _listKanjiModel = new List<KanjiModel>();
        private List<KanjiEntity> _listKanjiEntity = null;

        public bool IsFinish { get; set; }

        public ViewKanjiAgainLogic()
        {
            _listKanjiEntity = CommonDAO.KanjiGetWasLearned();

            foreach (KanjiEntity entity in _listKanjiEntity)
            {
                if (IsNeedLearn(entity))
                {
                    KanjiModel model = CommonUtils.MappingData<KanjiModel>(entity);
                    model.VisibleRow = "Visible";
                    model.HistoryAnser = "";
                    _listKanjiModel.Add(model);
                }
            }
            IsFinish = false;
        }

        public List<KanjiModel> GetKanjiToInitGUI()
        {
            List<KanjiModel> result = new List<KanjiModel>();
            Random random = new Random();
            while (_listKanjiModel.Count > 0 && result.Count < 8)
            {
                int index = random.Next(0, _listKanjiModel.Count);
                result.Add(_listKanjiModel[index]);
                _listKanjiModel.RemoveAt(index);
            }

            while (result.Count < 8)
            {
                result.Add(new KanjiModel());
            }

            return result;
        }

        //public void updateData(List<KanjiModel> listModel)
        //{
        //    for (int i = 0; i < listModel.Count; i++)
        //    {
        //        if (listModel[i].Id == 0) continue;
        //        if (checkLearnDone(listModel[i]))
        //        {
        //            int index = _listKanjiEntity.IndexOf(new KanjiEntity(listModel[i].Id));
        //            _listKanjiEntity[index].MeaningOther = listModel[i].MeaningOther;
        //            _listKanjiEntity[index].HistoryAnswer = _listKanjiEntity[index].HistoryAnswer + listModel[i].HistoryAnser;
        //            _listKanjiEntity[index].LastLearning = DateTime.Now.ToString(Constant.DF_DATETIME_FORMAT);
        //        }
        //        else
        //        {
        //            _listKanjiModel.Add(listModel[i]);
        //        }
        //    }

        //    IsFinish = _listKanjiModel.Count == 0;

        //    if (IsFinish)
        //    {
        //        CommonDAO.KanjiUpdateData(_listKanjiEntity);
        //    }
        //}
    }
}
