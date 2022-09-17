namespace Order.Core.Models 
{
    public class SearchParamItem
    {
        public SearchParamItem()
        {
            
        }

        public SearchParamItem(SearchType searchType, SearchValueType searchValueType, object searchValue)
        {
            SearchType = searchType;
            SearchValueType = searchValueType;
            SearchValue = searchValue;
        }

        public SearchType SearchType { get; set; }
        public SearchValueType SearchValueType { get; set; }
        public object SearchValue { get; set; }
    }

    public class SearchParamBase
    {
        public SearchParamItem Id { get; set; }
    }
}