namespace PurrSoft.Api.Bootstrap;

public static class MiddlewareExtensions
{
    public static WebApplication UseAppMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("_myAllowSpecificOrigins");

        // Temporarily skipping authentication & authorization middleware
         //app.UseAuthentication(); // Not needed in this iteration
         //app.UseAuthorization(); // Not needed in this iteration

         app.MapControllers();

        return app;
    }
}
