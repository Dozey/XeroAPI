
namespace XeroAPI.Linq
{
    interface IXeroObjectPath
    {
        bool IsValid { get; }
        string Path { get; }
    }
}
