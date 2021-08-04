using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace InterviewCRUD.Tools
{
    public static class ErrorAnalyze
    {
        public static string GetModelStateError(ModelStateDictionary modelState)
        {
            return string.Join(",", modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
        }
    }
}