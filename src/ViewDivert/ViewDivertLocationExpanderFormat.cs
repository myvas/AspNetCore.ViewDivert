namespace Myvas.AspNetCore.ViewDivert
{
	/// <summary>
	/// View扩展方式
	/// </summary>
	public enum ViewDivertLocationExpanderFormat
    {
        /// <summary>
        /// 在文件名中加标识，例如./Index.wx.cshtml
        /// </summary>
        Suffix,

        /// <summary>
        /// 创建子文件夹，例如./wx/Index.cshtml
        /// </summary>
        SubFolder
    }
}
