using PaymentApi.Business.Abstract;
using PaymentApi.Business.Concrete;
using PaymentApi.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<PaymentDbContext>();

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
