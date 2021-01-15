using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestDeviceChecker 
{
    private const string KEY_TESTER = "urmobitester";
    public static bool IsTester;

    public static void CheckIfTesterFileExists(out bool isTester)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
        var files = directoryInfo.GetFiles();
        foreach (var item in files)
        {
            if (item.Name.ToLower().Contains(KEY_TESTER))
            {
                IsTester = true;
                break;
            }
        }
        isTester = IsTester;
    }

    public static string[] AllTestDevices = new string[]
    {
            "B3AE7AE0C27AE19F1110D38604E6054C",
            "D3A36D4648C624DC5282C5662FB3C6DC",
            "771E022FCC40B3C60B271B72D21B3D90",
            "17B3841373A8F4095C2BEFEEE4F60915",
            "E8DC8AA20C453D3E206DA077F436418E",
            "8AE934DC2BD35735230D6CCE12AD7DC4",
            "4F6E13AD55C31607D58C20BD7761E357",
            "7AFDF8FEF2E219C6FC7BD9C309C9A52E",
            "80809F7D117AE0C4C20772382374CEB9",
            "33E2647D47F33E1791997BAA98D1D5F1",
            "F137422A3B7240EFADC1661E2DFFB12E",
            "378BB2FD04022F9B7912B29B7F5A43ED",
            "5B4635FA6519095A43AE62ADFA32F5B6",
            "19ED617DAABDE492CA14C8F40F1F58E8",
            "8EAA611D4A31262AEB14DE1D74984D7D",
            "47C5B897143C4A59801182188A5E9BC1",
            "58CB2A5DEF0C80725A1B505263D8DE1A",
            "E2B5E7F24096468652E64E369FD02C0D",
            "BFC048692A6D881B4C26A46CF41D1B2A",
            "716C54DFB6453E5345E45853A441B8FE",
            "B0F938138888ED619761CE1F03A66FB6",
            "EE3E1B3C6CD0928EC658568BCB2EAD3A",
            "1B8C2DCCF9FCE31B56DF06329A16BC09",
            "7F20EA55F201E842AF86F110929CBA51",
            "025F8F90FE02A930813EB30DDE48A94B",
            "5116C1F9516601A4380D9EA4A721B79B",
            "D19C34D9177518F533E9BEE37FBC1935",
            "D69021CA6059C61A6461E87DA74B8A85",
            "D6024C3D1176DB9E3BC92E656FC25998",
            "666ffae8db98f080bc5994d183473633".ToUpper(),
            "27FD0599F8813879810781ADC29CD365"

    };
}
