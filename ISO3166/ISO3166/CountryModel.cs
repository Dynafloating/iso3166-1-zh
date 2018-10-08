namespace ISO3166
{
    public class CountryModel
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public string NumericCode { get; set; }
        public string TraditionalChineseName { get; set; }
        public string SimplifiedChineseName { get; set; }
        public bool Independent { get; set; }

        public CountryModel(
            string name, string twoLetterCode, string threeLetterCode, string numericCode, 
            string traditionalChineseName, string simplifiedChineseName, bool independent)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            NumericCode = numericCode;
            TraditionalChineseName = traditionalChineseName;
            SimplifiedChineseName = simplifiedChineseName;
            Independent = independent;
        }
    }
}
