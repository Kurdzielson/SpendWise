using Microsoft.Extensions.Logging;
using SpendWise.Modules.Expenses.Application.Tags.Exceptions;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Contexts;

namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Delete;

internal class DeleteTagHandler(ITagRepository tagRepository, IContext context, ILogger<DeleteTagHandler> logger)
    : ICommandHandler<DeleteTagCommand>
{
    public async Task HandleAsync(DeleteTagCommand command, CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var tag = await tagRepository.GetAsync(command.TagId, customerId, cancellationToken)
                  ?? throw new TagNotFound(command.TagId);

        await tagRepository.RemoveAsync(tag, cancellationToken);
        logger.LogInformation($"tag with Id: '{tag.Id}' has been removed by customer with Id: '{customerId}'");
    }
}