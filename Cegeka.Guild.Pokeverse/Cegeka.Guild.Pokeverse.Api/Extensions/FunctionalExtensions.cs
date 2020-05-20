using System;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api.Extensions
{
    public static class FunctionalExtensions
    {
        public static IActionResult ToActionResult(this Result result, Func<IActionResult> onOk)
        {
            if (result.IsFailure)
            {
                return new BadRequestObjectResult(new ApiResult(result));
            }

            return onOk();
        }

        private sealed class ApiResult
        {
            public ApiResult(Result result)
            {
                Error = result.IsFailure ? result.Error : null;
                IsSuccess = result.IsSuccess;
            }

            public string Error { get; private set; }

            public bool IsSuccess { get; private set; }

            public bool IsFailure => !IsSuccess;
        }
    }
}