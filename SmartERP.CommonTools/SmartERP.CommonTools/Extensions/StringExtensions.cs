using System.Linq.Dynamic.Core;

namespace SmartERP.CommonTools.Extensions
{
    public static class StringExtensions
    {
        public static Predicate<T> ToPredicate<T>(this string expression, bool isTrue = true)
        {
            var exp = DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig(), isTrue, expression).Compile();
            var predicate = new Predicate<T>(exp);
            return predicate;
        }
    }
}
