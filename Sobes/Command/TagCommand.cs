using DatabaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sobes.Service;
using System;


namespace Sobes.Command
{
    public record GetAllTagsQuery() : IRequest<List<Tag>>;

    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<Tag>>
    {
        private readonly TagService _TagService;

        public GetAllTagsQueryHandler(TagService TagService)
        {
            _TagService = TagService;
        }

        public async Task<List<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            return await _TagService.GetAllTagsAsync();
        }
    }
    public record CreateTagCommand(string Name) : IRequest<int>;

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly TagService _TagService;
        private readonly AppDbContext _context;

        public CreateTagCommandHandler(TagService TagService, AppDbContext context)
        {
            _TagService = TagService;
            _context = context;
        }

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var Tag = new Tag { Name = request.Name };
            await _TagService.CreateTagAsync(Tag);
            return Tag.Id;
        }
    }

    public record DeleteTagCommand(int Id) : IRequest;

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly TagService _TagService;

        public DeleteTagCommandHandler(TagService TagService)
        {
            _TagService = TagService;
        }

        public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            await _TagService.DeleteTagAsync(request.Id);
        }
    }

    public record UpdateTagCommand(int Id, string Name) : IRequest
    {
        public int Id { get; set; }
    }


    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly TagService _TagService;
        private readonly AppDbContext _context;


        public UpdateTagCommandHandler(TagService TagService, AppDbContext context)
        {
            _TagService = TagService;
            _context = context;
        }

        public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var Tag = _context.Tags.Find(request.Id);
            if (Tag == null)
            {
                throw new Exception("Тег не найден");
            }
            Tag.Name = request.Name;

            await _TagService.UpdateTagAsync(Tag);
        }

    }


    public record GetTag(int Id) : IRequest<Tag>;

    public class GetTagCommandHander : IRequestHandler<GetTag, Tag>
    {
        private readonly TagService _TagService;

        public GetTagCommandHander(TagService TagService)
        {
            _TagService = TagService;
        }

        public async Task<Tag?> Handle(GetTag request, CancellationToken cancellationToken)
        {

            return await _TagService.GetTagAsync(request.Id);

        }
    }
}
