using System.Threading.Tasks;

namespace Surprenant.Grunt.Core;

/// <summary>
/// 
/// </summary>
public interface IHaloInfiniteClientFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IHaloInfiniteClient> CreateAsync();
}
