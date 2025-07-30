using BVGF.Mapper;
using BVGFRepository.Interfaces.MstCategary;
using BVGFRepository.Repository.MstCategary;
using BVGFServices.Interfaces.CategoryMember;
using BVGFServices.Interfaces.MstCategary;
using BVGFServices.Interfaces.MstMember;
using BVGFServices.Services.CategoryMember;
using BVGFServices.Services.MstCategary;
using BVGFServices.Services.MstMember;

namespace BVGF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //register our repository
            builder.Services.AddScoped(typeof(IMstCategary<>),typeof(MstCategary<>));

            //register services
            builder.Services.AddScoped<IMstCategary, MstCategary>();

            builder.Services.AddScoped<IMstMember, MstMember>();

            builder.Services.AddScoped<ICategoryMember, CategoryMember>();
            //register our automapper
            builder.Services.AddAutoMapper(typeof(Automapperr));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 
            app.UseCors("AllowAll");



            app.MapControllers();

            app.Run();
        }
    }
}
