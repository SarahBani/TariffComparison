using System.ComponentModel;

namespace Test.Common
{
    public enum TariffType
    {
        [Description(Constants.BasicTariff)]
        Basic,
        [Description(Constants.PackagedTariff)]
        Packaged
    }
}
