namespace Myvas.AspNetCore.ViewDivert
{

	public interface IDeviceAccessor
    {
        string Device { get; }
        string Preference { get; }
    }
}
