using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContractWithWireMock.Messages
{
    public class ErrorResponse
    {
        public List<string> Errors;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorResponse" /> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        public ErrorResponse(ModelStateDictionary modelState)
        {
            Errors = new List<string>();
            foreach (var errorList in modelState.Values.Where(m => m.Errors != null && m.Errors.Count > 0))
                foreach (var error in errorList.Errors)
                {
                    var errorText = error.ErrorMessage;
                    //TODO: move the message to ErrorMessages file
                    if (error.Exception != null && !Errors.Contains("Unable to process request"))
                        errorText += "Unable to process request";
                    if (!string.IsNullOrWhiteSpace(errorText))
                        Errors.Add(errorText);
                }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorResponse" /> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public ErrorResponse(Exception ex)
        {
            Errors = new List<string> {ex.Message};
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorResponse" /> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public ErrorResponse(string error)
        {
            Errors = new List<string> {error};
        }
    }
}
