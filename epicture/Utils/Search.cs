using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;

namespace epicture
{
    class Search
    {
        public List<String> picList = new List<string>();

        public Search(Filter img)
        {
            var client = new ImageSearchAPI(new ApiKeyServiceClientCredentials("API KEY Bing search v7"));
            var imageResults = client.Images.SearchAsync(
                query: img.search,
                imageType: img.type,
                size: img.size,
                aspect: img.aspect,
                license: img.isFree,
                count: img.nbResult
                ).Result;

            foreach(var val in imageResults.Value)
            {
                picList.Add(val.ContentUrl);
            }

            //if (String.IsNullOrEmpty(picList.First<String>))
            //{
            //    if (imageResults.PivotSuggestions.Count > 0)
            //        var Suggestion = imageResults.PivotSuggestions.First().Suggestions.First().Text;
            //    if (imageResults.QueryExpansions.Count > 0)
            //        var QueryExpansion = imageResults.QueryExpansions.First().Text;
            //}
        }
    }
}
