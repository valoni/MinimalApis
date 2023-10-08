using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication4;
using System.Data;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();


builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("BasicAuthentication", null);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/**/

DataAccess.ConStr = app.Configuration.GetConnectionString("DB")??"";
Console.WriteLine(DataAccess.ConStr);

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("Any");

app.UseHttpsRedirection();


app.MapGet("api/pro/Get", () =>
{
    return "Hello World from Anonumous Bit Web Apis!";
}).AllowAnonymous()
  .WithOpenApi(operation => new(operation)
  {
      Description = "Just to test return values from non authorized client for test "
  });


app.MapGet("api/pro/Get2", () =>
{
    return "Hello World from Authorized Bit Web Apis!";
}).RequireAuthorization()
  .WithOpenApi(operation => new(operation)
  {
       Description = "Just to test return values from an authorized client for test "
  });



/* select * from table */
app.MapGet("api/pro/Table/{table}", (string table) =>
{
    DataTable dt = DataAccess.dtTable(table);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization()
  .WithOpenApi(operation => new(operation)
  {
      Description = " Api that return result from SELECT * FROM TABLE => {table} is tablename "
  });


/* select * from table where wh=vl */
app.MapGet("api/pro/TableW/{table}/{wh}/{vl}", (string table,string wh,string vl) =>
{
    DataTable dt = DataAccess.dtTable2(table, wh, vl);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization()
  .WithOpenApi(operation => new(operation)
  {
      Description = " Api that return result from SELECT * FROM {table} WHERE {wh}={vl} => {table} is tablename , {wh} are fields for where condition and {vl} for value to check for the field on where condition "
  });



/* select f1,f2,f3,f4 from table */
app.MapGet("api/pro/TableF/{table}/{fields}", (string table, string fields) =>
{
    DataTable dt = DataAccess.dtTable3(table, fields);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization();


/* select f1,f2,f3,f4 from table where wh=vl  .. */
app.MapGet("api/pro/TableFW/{table}/{f}/{wh}/{vl}", (string table,string f,string wh,string vl) =>
{
    DataTable dt = DataAccess.dtTable3(table, f, wh, vl);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization();

/* select f1,f2,f3,f4 from table where wh1=vl1 and wh2=vl2 and wh3=vl3 .. */
app.MapGet("api/pro/TableLWF/{table}/{f}/{wh}/{vl}", (string table, string f, string wh, string vl) =>
{
    DataTable dt = DataAccess.dtTable4(table, f, wh, vl);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization();

app.MapGet("api/pro/Procedure/{spName}/{spparameters}", (string spName, string spparameters) =>
{
    DataTable dt = DataAccess.dtTable5(spName, spparameters);
    var JSONresult = JsonConvert.SerializeObject(dt);

    return JSONresult;

}).RequireAuthorization();


/*

app.MapPost("/Post", ([FromBody] JsonObject json) =>
{
    var info = json["info"]?.ToString();

    return "Posted From " + info;
});




app.MapPost("/FromHeaders", ([FromHeader] JsonObject json) =>
{
    var info = json["info"]?.ToString();

    return "From Headers" + info;
});




app.MapPost("/FromQueries", ([FromQuery] JsonObject json) =>
{
    var info = json["info"]?.ToString();

    return "From FromQuery" + info;
});
*/


app.Run();



