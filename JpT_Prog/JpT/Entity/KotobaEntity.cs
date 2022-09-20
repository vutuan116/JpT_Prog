namespace JpT.Entity
{
    public class KotobaEntity
    {
        public int Id { get; set; }
        public string Lesson { get; set; }
        public string Content { get; set; }
        public string Hiragana { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Hard { get; set; }
        public string Lock { get; set; }
        public string HistoryAnswer { get; set; }
        public string MeaningOther { get; set; }
        public string LastLearning { get; set; }
        
        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Lesson + Content + Hiragana + Meaning);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            return this.Id ==((KotobaEntity) obj).Id;
        }
    }
}
