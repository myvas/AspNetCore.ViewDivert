namespace Myvas.AspNetCore.ViewDivert
{
	public interface IDeviceFactory
    {
        IDevice Normal();
        IDevice Mobile();
        IDevice Tablet();
        IDevice Other(string code);
    }
}
