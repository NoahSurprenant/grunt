
namespace Surprenant.Grunt.Models;

public class User
{
    public string xuid { get; set; }
    public string gamertag { get; set; }
    public Gamerpic gamerpic { get; set; }
}

public class Gamerpic
{
    public string small { get; set; }
    public string medium { get; set; }
    public string large { get; set; }
    public string xlarge { get; set; }
}
