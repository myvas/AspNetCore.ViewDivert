﻿namespace Myvas.AspNetCore.ViewDivert
{
	public class LiteDevice : IDevice
    {
        private readonly DeviceType _deviceType;

        public LiteDevice(DeviceType deviceType, string code = "")
        {
            _deviceType = deviceType;
            DeviceCode = code;
        }

        public bool IsMobile => _deviceType == DeviceType.Mobile;
        public bool IsTablet => _deviceType == DeviceType.Tablet;
        public bool IsNormal => _deviceType == DeviceType.Normal;
        public string DeviceCode { get; }
        public override string ToString()
        {
            return $"{_deviceType}";
        }
    }
}
