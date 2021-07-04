# ElmahCoreExample
[Library Github Address](https://github.com/ElmahCore/ElmahCore)

**ElmahCore kütüphanesi ile uygulama içerisinde çıkan hataları loglama.**

[![Build Status](https://travis-ci.org/grandchamp/Identity.Dapper.svg?branch=master)](https://www.nuget.org/packages/ElmahCore/)

Nuget package (Eg: ElmahCore).

Startup.cs ConfigureServices **ElmahCore**
```
services.AddElmah<SqlErrorLog>(options =>
{
    options.Path = "log";
    // options.OnPermissionCheck = context => context.User.IsInRole("admin");
    options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
    // options.Notifiers.Add(new ErrorMailNotifier("Email",new EmailOptions()));
    options.Filters.Add(new CmsErrorLogFilter());
});
```

Startup.cs Configure Middleware **ElmahCore**
```
app.UseElmah();
```
