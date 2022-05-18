var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

//MVP, Jāuztaisa iespēja ievadīt jaunus uzdevumus
//1. Uztaisīt objektus/klases lai pievienotu uzdevumus
//2. Uztaisīt klasi/objektus lietotāja izveidei
//3. Datubāze
// -> lietājiem >> Username, password
// -> Numurs, Uzdevums, Datums, Lietotājs, Nosaukums


//Bāze
//2 tabulas -> Imants parole
//Numurs, Uzdevums, Datums, Lietotājs, Nosaukums -> Imants