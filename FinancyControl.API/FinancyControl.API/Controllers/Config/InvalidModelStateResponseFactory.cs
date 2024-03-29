﻿using FinancyControl.API.Resources;
using Microsoft.AspNetCore.Mvc;
using FinancyControl.API.Extension;

namespace FinancyControl.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
