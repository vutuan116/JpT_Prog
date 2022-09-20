namespace JpT.Entity
{
    public class KanjiEntity
    {
        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Lesson + Kanji + Hiragana + Meaning);
        }

        public int Id { get; set; }
        public string Lesson { get; set; }
        public string Kanji { get; set; }
        public string Hiragana { get; set; }
        public string HanViet { get; set; }
        public string Meaning { get; set; }
        public string Hard { get; set; }
        public string Lock { get; set; }
        public string HistoryAnswer { get; set; }
        public string MeaningOther { get; set; }
        public string LastLearning { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            return this.Id == ((KanjiEntity)obj).Id;
        }
    }
}
