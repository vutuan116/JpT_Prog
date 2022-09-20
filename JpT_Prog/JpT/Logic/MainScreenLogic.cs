using JpT.DAO;
using JpT.Model;
using JpT.Utilities;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace JpT.Logic
{
    public class MainScreenLogic
    {
        public void InitData(MainModel model)
        {
            CommonDAO.InitData();

            try
            {
                string configStr = File.ReadAllText(Constant.FILE_CONFIG);
                Match match = Regex.Match(configStr, Constant.FILE_CONFIG_REGEX_GET_DATA);
                model.KotobaNewLastLearning = string.IsNullOrEmpty(match.Groups[1].Value) ? Constant.NOTI_NO_DATA : match.Groups[1].Value;
                model.KotobaAgainLastLearning = string.IsNullOrEmpty(match.Groups[2].Value) ? Constant.NOTI_NO_DATA : match.Groups[2].Value;
                model.KanjiNewLastLearning = string.IsNullOrEmpty(match.Groups[3].Value) ? Constant.NOTI_NO_DATA : match.Groups[3].Value;
                model.KanjiAgainLastLearning = string.IsNullOrEmpty(match.Groups[4].Value) ? Constant.NOTI_NO_DATA : match.Groups[4].Value;
            }
            catch
            {
                model.KotobaNewLastLearning = model.KotobaAgainLastLearning = model.KanjiNewLastLearning = model.KanjiAgainLastLearning = Constant.NOTI_NO_DATA;
            }
        }

        public void SaveData(MainModel model)
        {
            try
            {
                DateTime temp;
                if (!DateTime.TryParse(model.KotobaNewLastLearning, out temp)) model.KotobaNewLastLearning = string.Empty;
                if (!DateTime.TryParse(model.KotobaAgainLastLearning, out temp)) model.KotobaAgainLastLearning = string.Empty;
                if (!DateTime.TryParse(model.KanjiNewLastLearning, out temp)) model.KanjiNewLastLearning = string.Empty;
                if (!DateTime.TryParse(model.KanjiAgainLastLearning, out temp)) model.KanjiAgainLastLearning = string.Empty;

                string content = string.Format(Constant.FILE_CONFIG_FORMAT_SAVE, model.KotobaNewLastLearning, model.KotobaAgainLastLearning, model.KanjiNewLastLearning, model.KanjiAgainLastLearning);

                File.WriteAllText(Constant.FILE_CONFIG, content);
            }
            catch{}
        }
    }
}
