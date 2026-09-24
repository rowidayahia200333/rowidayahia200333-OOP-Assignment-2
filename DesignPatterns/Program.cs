
namespace DesignPatterns;

public class Program
{
    public static void Main(string[] args)
    {
        //var db = new DatabaseService();
        //var ui = new UiService();

        //db.Config.Theme = "jj";
        //db.Connect();
        //ui.Render();
        //Console.WriteLine(ui.Config.Theme);

        //bool isSameRef = ReferenceEquals(ui.Config,db.Config);
        //Console.WriteLine(isSameRef);


    }
}

public class AppConfig
{
    public static int LoadCount;

    public string DbConnection { get; set; }
    public string Theme { get; set; }

    private static Lazy<AppConfig> _instance = new Lazy<AppConfig>(() => new AppConfig());
    public static AppConfig Instance => _instance.Value;

    private AppConfig()
    {
        LoadCount++;
        Console.WriteLine($"[AppConfig] Loading settings from disk... (load #{LoadCount})");
        Thread.Sleep(300);
        DbConnection = "Server=localhost;Db=School";
        Theme = "Light";

        

    }

}

public class DatabaseService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Connect() => Console.WriteLine($"Connecting to {Config.DbConnection}");
}

public class UiService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Render() => Console.WriteLine($"UI is using theme: {Config.Theme}");
}


// proto

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }

    public Weapon Clone()
    {
        return new Weapon { Name =this.Name ,Damage=this.Damage}
    }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy source)
    {
        this._modelData = source._modelData;

        this.Name = source.Name;
        this.Health = source.Health;
        this.Weapon = source.Weapon?.Clone();
        this.Abilities = new List<string>(source.Abilities);
    }
    

    public abstract Enemy clone();
    
}

public class Orc : Enemy
{
    public Orc()
        :base()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc source) : base(source) { }

    public override Enemy clone()
    {
        return new Orc(this);
    }
   
}

public class Elf : Enemy
{
    public Elf() : base()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    private Elf(Elf source) : base(source) { }

    public override Enemy Clone() => new Elf(this);
}

public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new();

    public void Register(string key, Enemy prototype)
    {
        _prototypes[key] = prototype;
    }

    public Enemy Get(string key)
    {
        return _prototypes[key].Clone();
    }
}

//Builder

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(courseCode)) throw new ArgumentException("course required");

        if (accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}

public class RegestrationBuilder
{
    private string StudentEmail;
    private string CourseCode;
    private string AccessMode;
    private string? GroupCode;
    private string? DiscountCode;
    private bool _SendWhatsApp;
    private bool _SendEmailWelcome;
    private string? MentorNote;
    private DateOnly? PreferredStart;

    public RegistrationBuilder(string studentEmail, string courseCode)
    {
        _studentEmail = studentEmail;
        _courseCode = courseCode;
    }

    public RegestrationBuilder WithAccessMode(string mode)
    {
        AccessMode = mode;
        return this;
    }

    public RegestrationBuilder InGroup(string? groupCode)
    {
        GroupCode = groupCode;
        return this;
    }

    public RegestrationBuilder WithDiscount(string? discountCode)
    {
        DiscountCode = discountCode;
        return this;
    }

    public RegestrationBuilder SendWhatsApp(bool enable = true)
    {
        _SendWhatsApp = enable;
        return this;
    }

    public RegestrationBuilder SendEmailWelcome(bool enable = true)
    {
        _SendEmailWelcome = enable;
        return this;
    }

    public RegestrationBuilder WithMentorNote(string? note)
    {
        MentorNote = note;
        return this;
    }

    public RegestrationBuilder StartingOn(DateOnly? start)
    {
        PreferredStart = start;
        return this;
    }

    public CourseRegistration Build() => 
        new CourseRegistration(StudentEmail, CourseCode,AccessMode, GroupCode,discountCode,
            _SendWhatsApp,_SendEmailWelcome, MentorNote,PreferredStart);
}
public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudentUgly()
    {
        return new RegestrationBuilder("sara@mail.com", "SEF-101")
            .WithAccessMode("LiveGroup").InGroup("G1").WithDiscount("EARLY10")
            .SendWhatsApp(true).SendEmailWelcome(true).WithMentorNote("Needs evening slot")
            .StartingOn(new DateOnly(2026, 10, 1)).Build();

      
    }

    public static CourseRegistration CreateVideosOnlyUgly()
    {
        return new RegestrationBuilder("ali@mail.com", "SEF-101")
        .WithAccessMode("VideosOnly")
        .SendEmailWelcome(true)
        .Build();
    }

}
