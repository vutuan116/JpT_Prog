using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace JpT.Logic
{
    public class ViewKotobaAgainLogic
    {
        #region method support
        private bool IsNeedLearn(KotobaEntity entity)
        {
            int level = int.TryParse(entity.Hard, out level) ? level : 5;
            return checkLearning(entity, Constant.LEARNING_AGAIN_RULE_IN[level - 1][0], Constant.LEARNING_AGAIN_RULE_IN[level - 1][1], Constant.LEARNING_AGAIN_RULE_IN[level - 1][2], Constant.LEARNING_AGAIN_RULE_IN[level - 1][3]);
        }

        private bool checkLearning(KotobaEntity entity, int count, int ratioTrue, int countLastHistory, int lastLearning)
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

        private bool checkLearnDone(KotobaModel kotoba)
        {
            int countTrue = kotoba.HistoryAnser.Replace(Constant.DF_ANSWER_FALSE, string.Empty).Length;
            int level = int.TryParse(kotoba.Level, out level) ? level : 5;
            return countTrue == Constant.LEARNING_AGAIN_RULE_OUT[level - 1];
        }
        #endregion

        private List<KotobaModel> _listKotobaModel = new List<KotobaModel>();
        private List<KotobaEntity> _listKotobaEntity = null;
        public int CountKotoba { get; set; }
        public int CountKotobaLearned { get; set; }

        public bool IsFinish { get; set; }

        public ViewKotobaAgainLogic()
        {
            _listKotobaEntity = CommonDAO.KotobaGetWasLearned();

            foreach (KotobaEntity entity in _listKotobaEntity)
            {
                if (IsNeedLearn(entity))
                {
                    KotobaModel model = CommonUtils.MappingData<KotobaModel>(entity);
                    model.VisibleRow = "Visible";
                    model.HistoryAnser = "";
                    _listKotobaModel.Add(model);
                }
            }
            CountKotoba = _listKotobaEntity.Count;
            IsFinish = false;
        }

        public List<KotobaModel> GetKotobaToInitGUI()
        {
            List<KotobaModel> result = new List<KotobaModel>();
            Random random = new Random();
            while (_listKotobaModel.Count > 0 && result.Count < 10)
            {
                int index = random.Next(0, _listKotobaModel.Count);
                result.Add(_listKotobaModel[index]);
                _listKotobaModel.RemoveAt(index);
            }

            CountKotobaLearned = CountKotoba - _listKotobaModel.Count() - result.Count;

            while (result.Count < 10)
            {
                result.Add(new KotobaModel());
            }

            return result;
        }

        //public void updateData(List<KotobaModel> listModel)
        //{
        //    for (int i = 0; i < listModel.Count; i++)
        //    {
        //        if (listModel[i].Id == 0) continue;
        //        if (checkLearnDone(listModel[i]))
        //        {
        //            KotobaEntity entity = _listKotobaEntity.Where(x=> x.Id == listModel[i].Id).FirstOrDefault();
        //            entity.MeaningOther = listModel[i].MeaningOther;
        //            entity.HistoryAnswer = entity.HistoryAnswer + listModel[i].HistoryAnser;
        //            entity.LastLearning = DateTime.Now.ToString(Constant.DF_DATETIME_FORMAT);
        //        }
        //        else
        //        {
        //            listModel[i].Answer = string.Empty;
        //            listModel[i].IsRightAnswer = false;
        //            _listKotobaModel.Add(listModel[i]);
        //        }
        //    }

        //    IsFinish = _listKotobaModel.Count == 0;

        //    if (IsFinish)
        //    {
        //        CountKotoba = 0;
        //        CommonDAO.KotobaUpdateData(_listKotobaEntity);
        //    }
        //}
    }
}
