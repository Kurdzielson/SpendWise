using SpendWise.Modules.Expenses.Core.Tags.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;

namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Create;

internal class CreateTagHandler(ITagRepository tagRepository, IContext context, ILogger<CreateTagHandler> logger)
    : ICommandHandler<CreateTagCommand, CreateResponse>
{
    public async Task<CreateResponse> HandleAsync(CreateTagCommand command,
        CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var tag = Tag.Create(customerId, command.Name, command.ColorHex);

        var result = await tagRepository.AddAsync(tag, cancellationToken);
        logger.LogInformation($"Tag with Id: '{result}' has been created by customer with Id: {customerId}.");

        return new CreateResponse(result);
    }
}