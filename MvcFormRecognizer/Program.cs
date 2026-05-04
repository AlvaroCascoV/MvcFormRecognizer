using Azure.AI.FormRecognizer.DocumentAnalysis;
using MvcFormRecognizer.Services;

var builder = WebApplication.CreateBuilder(args);

string endPoint = builder.Configuration.GetValue<string>("AzureDocuments:Endpoint");
string apiKey = builder.Configuration.GetValue<string>("AzureDocuments:ApiKey");
DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endPoint), new Azure.AzureKeyCredential(apiKey));
builder.Services.AddTransient<DocumentAnalysisClient>(x => client);
builder.Services.AddTransient<ServiceRecognizer>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
