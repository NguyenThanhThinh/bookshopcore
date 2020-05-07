namespace BookShop.Extensions
{
    public static class MoneyExtensions
    {
        public static string ToMoneyFormatted(this decimal money, string postFix = "")
            => money.ToString("N0") + (string.IsNullOrEmpty(postFix) ? string.Empty : " " + postFix);

        public static decimal ToMoney(this string money, int defaultValue = 0)
        {
            decimal val;
            if (decimal.TryParse(money, out val))
                return val;

            return defaultValue;
        }
    }
}
