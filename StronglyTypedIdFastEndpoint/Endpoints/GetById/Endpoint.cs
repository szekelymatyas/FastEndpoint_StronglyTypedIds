using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StronglyTypedIdFastEndpoint;

namespace StronglyTypedIdFastEndpoint.Endpoints.GetById;

[HttpGet("book/{Id}")]
[PrimaryConstructor]
[AllowAnonymous]
public sealed partial class Endpoint: Endpoint<Request, Book>
{
    private readonly BookDbContext dbContext;
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var res = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == req.Id);
        if(res is null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendOkAsync(res);

    }
}

