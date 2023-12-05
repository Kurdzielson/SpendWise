using Microsoft.Extensions.Logging;
using SpendWise.Modules.Expenses.Application.Tags.Exceptions;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Update;

internal class UpdateTagHandler(ITagRepository tagRepository, IContext context, ILogger<UpdateTagHandler> logger)
    : ICommandHandler<UpdateTagCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(UpdateTagCommand command,
        CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var tag = await tagRepository.GetAsync(command.TagId, customerId, cancellationToken)
                  ?? throw new TagNotFound(command.TagId);

        tag.Update(command.Name, command.ColorHex);

        var result = await tagRepository.UpdateAsync(tag, cancellationToken);
        logger.LogInformation($"Tag with Id: '{result}' has been updated by customer with Id: '{customerId}'.");

        return new UpdateResponse(result);
    }
}