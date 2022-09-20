using System.Collections.Generic;

namespace JpT.Utilities
{
    public class Constant
    {
        // Common
        public static readonly string FILE_DATA = System.AppDomain.CurrentDomain.BaseDirectory + @"Data\Data.xlsx";

        public static readonly string FILE_CONFIG = System.AppDomain.CurrentDomain.BaseDirectory + @"Config.txt";
        public static readonly string FILE_CONFIG_REGEX_GET_DATA = @"^NewKotoba=([0-9\/]*)\|AgainKotoba=([0-9\/]*)\|NewKanji=([0-9\/]*)\|AgainKanji=([0-9\/]*)$";
        public static readonly string FILE_CONFIG_FORMAT_SAVE = "NewKotoba={0}|AgainKotoba={1}|NewKanji={2}|AgainKanji={3}";

        public static readonly string DF_DATETIME_FORMAT = "yyyy/MM/dd";

        public static string DF_LEVEL_KOTOBA = "2";
        public static string DF_LEVEL_KANJI = "2";

        public static string DF_ANSWER_TRUE = "O";
        public static string DF_ANSWER_FALSE = "X";

        public static string NOTI_NO_DATA = "Không có dữ liệu";

        // View Kotoba again
        public static readonly List<int[]> LEARNING_AGAIN_RULE_IN = new List<int[]>() { new int[] {8, 68, 2, 10}, new int[] {10, 72, 2, 10 },
                                                                                      new int[] {12, 76, 3, 8 }, new int[] {16, 80, 3, 8 }, new int[] {20, 84, 3, 6 } };
        public static readonly List<int> LEARNING_AGAIN_RULE_OUT = new List<int>() { 1, 1, 2, 2, 2 };
        public static readonly List<int> RULE_LOCK = new List<int>() { 20, 18, 16, 14, 12 };

        // View Kotoba new
        public static readonly int KOTOBA_NEW_COUNT_ANSWER_DF = 2;


        // View Kanji new 
        public static readonly int KANJI_NEW_COUNT_ANSWER_DF = 2;

        // GUI
        public static readonly int DF_WIDTH_COLUMN = 0;
        public static readonly int WIDTH_COLUMN_NORMAL = 220;
        public static readonly int WIDTH_COLUMN_NORMAL_KANJI = 160;
        public static readonly int WIDTH_COLUMN_SMALL = 100;
        public static readonly string BTN_CONTENT_TEST = "Luyện tập";
        public static readonly string BTN_CONTENT_CHECK = "Kiểm tra";
        public static readonly string BTN_CONTENT_AGAIN = "Luyện tập lại";
        public static readonly string BTN_CONTENT_NEXT = "Tiếp tục";
        public static readonly string BTN_CONTENT_FINISH = "Kết thúc";

        // DAO
        public static readonly int KOTOBA_COLUMN_LESSON = 2;
        public static readonly int KOTOBA_COLUMN_CONTENT = 3;
        public static readonly int KOTOBA_COLUMN_HIRAGANA = 4;
        public static readonly int KOTOBA_COLUMN_MEANING = 5;
        public static readonly int KOTOBA_COLUMN_TYPE = 6;
        public static readonly int KOTOBA_COLUMN_IS_HARD = 7;
        public static readonly int KOTOBA_COLUMN_LOCK = 8;
        public static readonly int KOTOBA_COLUMN_HISTORY = 9;
        public static readonly int KOTOBA_COLUMN_OTHER_MEANING = 10;
        public static readonly int KOTOBA_COLUMN_LAST_LEARNING = 11;

        public static readonly int KANJI_COLUMN_LESSON = 2;
        public static readonly int KANJI_COLUMN_KANJI = 3;
        public static readonly int KANJI_COLUMN_HIRAGANA = 4;
        public static readonly int KANJI_COLUMN_HANVIET = 5;
        public static readonly int KANJI_COLUMN_MEANING = 6;
        public static readonly int KANJI_COLUMN_IS_HARD = 7;
        public static readonly int KANJI_COLUMN_LOCK = 8;
        public static readonly int KANJI_COLUMN_HISTORY = 9;
        public static readonly int KANJI_COLUMN_OTHER_MEANING = 10;
        public static readonly int KANJI_COLUMN_LAST_LEARNING = 11;
    }
}
