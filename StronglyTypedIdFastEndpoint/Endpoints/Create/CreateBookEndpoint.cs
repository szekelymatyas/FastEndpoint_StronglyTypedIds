using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StronglyTypedIdFastEndpoint;

namespace StronglyTypedIdFastEndpoint.Endpoints.Create;

[HttpPost("book/create")]
[PrimaryConstructor]
[AllowAnonymous]
public sealed partial class Endpoint : Endpoint<Request, Book>
{
    private readonly BookDbContext dbContext;
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var res = await dbContext.Books.AddAsync(new Book { Title = req.Title});
        await dbContext.SaveChangesAsync();
        await SendCreatedAtAsync<GetById.Endpoint>(new GetById.Request{Id = res.Entity.Id }, res.Entity);
    }
}