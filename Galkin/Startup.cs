using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "����������� �������������",
                    Description = "����������� �������������"
                });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v2",
                    Title = "���������",
                    Description = "����������, ��������, ��������� � ��������� ������ � �������"
                });
                c.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v3",
                    Title = "������ � ����������",
                    Description = "����������, ��������, ��������� � ��������� ������ � �����������"
                });
                string filePath = Path.Combine(AppContext.BaseDirectory, "Galkin.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvcWithDefaultRoute();
            app.UseStatusCodePages();
            app.UseSwagger();
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�����������");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "����������");
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "������");
            });
        }
    }
}
