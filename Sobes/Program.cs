using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Sobes.Command;
using Sobes.Repository;
using Sobes.Service;
using MediatR;

namespace Sobes
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���������� ������������ � ����������� ����������� FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>(); // register validators
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            // ���������� ��������� ���� ������
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ����������� MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // ����������� �������� � ������������
            builder.Services.AddScoped<NoteService>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();

            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            builder.Services.AddScoped<ReminderService>();
            builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

            // Pipeline ��� ���������
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
