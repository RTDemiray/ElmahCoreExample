using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Mvc.Notifiers;
using ElmahCore.Sql;
using ElmahCoreExample.ElmahCustom;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElmahCoreExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //@TODO: Elmah servisini ekliyoruz.
            services.AddElmah<SqlErrorLog>(options =>
            {
                //@TODO: Elmah tarafından oluşan log sayfasını izlemek için belirttiğiniz url. Default'ta /elmah olarak belirlenmiştir.
                options.Path = "log";
                //@TODO: Log kayıtlarına ulaşabilmek için authentication veya authorization kontrolü yapabilirsiniz.
                // options.OnPermissionCheck = context => context.User.IsInRole("admin");
                //@TODO: Elmah logları database'e yazabilmesi için ilgili connection'ı belirtiyoruz. Kendisi ilgili database'de ELMAH_Error adında bir tablo oluşturup içine logları basıyor.
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                //@TODO: Bir bildirim servisi eklemek istiyorsak kullanabiliriz. Elmah kütüphanesinin içerisinde gelen ErrorMailNotifier sınıfı ile uygulama içerisinde bir hata oluştuğunda otomatik mail gönderme işlemini konfigüre edebiliyoruz.
                // options.Notifiers.Add(new ErrorMailNotifier("Email",new EmailOptions()));
                //@TODO: Oluşan hataları filtreleyebileceğimiz bir servis ekleyip oluşturabiliriz.
                options.Filters.Add(new CmsErrorLogFilter());
            });
            
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            //@TODO: Elmah middleware'ini ekliyoruz.
            app.UseElmah();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}