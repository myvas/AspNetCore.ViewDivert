using System;

namespace Microsoft.AspNetCore.Mvc
{
	/// <summary>
	/// 放在Controller/Action方法上，以决定是否根据浏览器来选择不同的View
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class ViewDivertAttribute : Attribute
	{
	}
}
