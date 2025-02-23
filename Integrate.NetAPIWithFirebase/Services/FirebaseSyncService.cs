using APIWithFireBase.Models;
using Firebase.Database;
using Integrate.NetAPIWithFirebase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class FirebaseSyncService
{

    // private readonly ApplicationDbContext context;
    private readonly IOptions<FirebaseSettings> settings;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly FirebaseClient _firebaseClient;

    public FirebaseSyncService(IOptions<FirebaseSettings> settings, IServiceScopeFactory scopeFactory)
    {
        // this.context = context;
        this.settings = settings;
        this.scopeFactory = scopeFactory;
        _firebaseClient = new FirebaseClient(settings.Value.BaseUrl);
    }
    public async Task StartListeningAsync()
    {


         _firebaseClient
            .Child("BabyTemp") // عقدة البيانات في Firebase
            .AsObservable<BabyTemperature>()
            .Subscribe(async update =>
            {
                if (update.Object != null)
                {
                    try
                    {
                        // إنشاء Scope جديد لضمان أن DbContext يتم إنشاؤه بشكل صحيح
                        using var scope = scopeFactory.CreateScope();
                        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();

                        //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        // إنشاء DbContext جديد لكل تحديث من Firebase
                        await using var context = contextFactory.CreateDbContext();

                        context.BabyTemperatures.Add(update.Object);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // تسجيل الخطأ لمنع كسر التطبيق
                        Console.WriteLine($"Error saving data to SQL Server: {ex.Message}");
                    }
                }
            });
    }


    //public async Task StartListeningAsync()
    //{
    //    // متابعة التغييرات في العقدة المحددة في Firebase
    //    var observable = _firebaseClient
    //        .Child("BabyTemp") // عقدة البيانات
    //        .AsObservable<BabyTemperature>() // تأكد أن النموذج مطابق لبياناتك
    //        .Subscribe(async update =>
    //        {
    //            if (update.Object != null)
    //            {
    //                // حفظ البيانات في SQL Server عند تحديث Firebase
    //                //context.BabyTemperatures.Add(update.Object);
    //                using var scope = scopeFactory.CreateScope();
    //                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    //                context.BabyTemperatures.Add(update.Object);
    //                await context.SaveChangesAsync();
    //            }
    //        });
    //}

}
