using MediatR;
using Persistence;

namespace Application.Activities;
public class Edit
{
    public class Command : IRequest
    {
        public ActivityDto Activity { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext context;

        public Handler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync(request.Activity.Id);
            if (activity == null)
            {
                throw new InvalidOperationException();
            }

            activity.Title = request.Activity.Title ?? activity.Title;
            activity.Description = request.Activity.Description ?? activity.Description;
            activity.Category = request.Activity.Category ?? activity.Category;
            activity.Date = request.Activity.Date ?? activity.Date;
            activity.City = request.Activity.City ?? activity.City;
            activity.Venue = request.Activity.Venue ?? activity.Venue;
            await context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}