// ... existing code ...

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Search Engine API", 
        Version = "v1",
        Description = "API for Search Engine Application"
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Engine API V1");
    });
}

// ... existing code ...