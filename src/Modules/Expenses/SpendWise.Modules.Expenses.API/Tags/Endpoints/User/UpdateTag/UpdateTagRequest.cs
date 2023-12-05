using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Expenses.Application.Tags.Commands.Update;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.UpdateTag;

internal class UpdateTagRequest
{
    [FromRoute(Name = "tagId")] public Guid TagId { get; set; }
    [FromBody] public UpdateTagCommand Command { get; set; }
}