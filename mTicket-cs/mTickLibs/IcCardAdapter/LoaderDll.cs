using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace mTickLibs.IcCardAdapter
{
    public class LoaderDll
    {
        public const Byte RequestModeIdle = 0;
        public const Byte RequestModeAll = 1;
        public const Byte LoadKeyModeKeyA = 0;
        public const Byte LoadKeyModeKeyB = 4;

        [DllImport("umf.DLL", EntryPoint = "fw_init")]
        public static extern Int32 fw_init(Int16 port, Int32 baud);
        [DllImport("umf.DLL", EntryPoint = "fw_exit")]
        public static extern Int32 fw_exit(Int32 icdev);
        [DllImport("umf.Dll", EntryPoint = "fw_request")]
        public static extern Int32 fw_request(Int32 icdev, Byte _Mode, ref UInt32 TagType);
        [DllImport("umf.DLL", EntryPoint = "fw_anticoll")]
        public static extern Int32 fw_anticoll(Int32 icdev, Byte _Bcnt, ref ulong _Snr);
        [DllImport("umf.DLL", EntryPoint = "fw_select")]
        public static extern Int32 fw_select(Int32 icdev, UInt32 _Snr, ref Byte _Size);
        [DllImport("umf.DLL", EntryPoint = "fw_card")]
        public static extern Int32 fw_card(Int32 icdev, Byte _Mode, ref ulong _Snr);
        [DllImport("umf.DLL", EntryPoint = "fw_card_hex")]
        public static extern Int32 fw_card_hex(Int32 icdev, Byte _Mode, StringBuilder _Data);
        [DllImport("umf.DLL", EntryPoint = "fw_load_key")]
        public static extern Int32 fw_load_key(Int32 icdev, Byte _Mode, Byte _SecNr, Byte[] _NKey);
        [DllImport("umf.DLL", EntryPoint = "fw_authentication")]
        public static extern Int32 fw_authentication(Int32 icdev, Byte _Mode, Byte _SecNr);
        [DllImport("umf.DLL", EntryPoint = "fw_read")]
        public static extern Int32 fw_read(Int32 icdev, Byte _Adr, Byte[] _Data);

        [DllImport("umf.dll", EntryPoint = "fw_read_hex")]
        public static extern Int16 fw_read_hex(Int32 icdev, Byte _Adr, StringBuilder _Data);

        [DllImport("umf.DLL", EntryPoint = "fw_write")]
        public static extern Int32 fw_write(Int32 icdev, Byte _Adr, Byte[] _Data);

        [DllImport("umf.dll", EntryPoint = "fw_write_hex")]
        public static extern Int16 fw_write_hex(Int32 icdev, Byte _Adr, string _Data);

        [DllImport("umf.DLL", EntryPoint = "fw_halt")]
        public static extern Int32 fw_halt(Int32 icdev);
        [DllImport("umf.DLL", EntryPoint = "fw_changeb3")]
        public static extern Int32 fw_changeb3(Int32 icdev, Byte _SecNr, Byte[] _KeyA, Byte[] _CtrlW, Byte _Bk, Byte[] _KeyB);
        [DllImport("umf.DLL", EntryPoint = "fw_initval")]
        public static extern Int32 fw_initval(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("umf.DLL", EntryPoint = "fw_increment")]
        public static extern Int32 fw_increment(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("umf.DLL", EntryPoint = "fw_readval")]
        public static extern Int32 fw_readval(Int32 icdev, Byte _Adr, ref UInt32 _Value);
        [DllImport("umf.DLL", EntryPoint = "fw_decrement")]
        public static extern Int32 fw_decrement(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("umf.DLL", EntryPoint = "fw_restore")]
        public static extern Int32 fw_restore(Int32 icdev, Byte _Adr);
        [DllImport("umf.DLL", EntryPoint = "fw_transfer")]
        public static extern Int32 fw_transfer(Int32 icdev, Byte _Adr);
        [DllImport("umf.DLL", EntryPoint = "fw_beep")]
        public static extern Int32 fw_beep(Int32 icdev, UInt32 _Msec);
        [DllImport("umf.DLL", EntryPoint = "fw_getver")]
        public static extern Int32 fw_getver(Int32 icdev, ref Byte buff);
        [DllImport("umf.DLL", EntryPoint = "fw_reset")]
        public static extern Int16 fw_reset(Int32 icdev, UInt16 _Msec);
        [DllImport("umf.DLL", EntryPoint = "hex_a")]
        public static extern void hex_a(ref Byte hex, ref Byte a, Int16 len);

        //Ultralight卡函数
        [DllImport("umf.dll", EntryPoint = "fw_request_ultralt")]
        public static extern Int32 fw_request_ultralt(Int32 icdev, Byte _Mode);
        [DllImport("umf.dll", EntryPoint = "fw_anticall_ultralt")]
        public static extern Int32 fw_anticall_ultralt(Int32 icdev, ref ulong _Snr);
        [DllImport("umf.dll", EntryPoint = "fw_select_ultralt")]
        public static extern Int32 fw_select_ultralt(Int32 icdev, ulong _Snr);
        [DllImport("umf.dll", EntryPoint = "fw_write_ultralt")]
        public static extern Int32 fw_write_ultralt(Int32 icdev, Byte iPage, Byte[] wdata);
        [DllImport("umf.dll", EntryPoint = "fw_read_ultralt")]
        public static extern Int32 fw_read_ultralt(Int32 icdev, Byte iPage, Byte[] rdata);
    }
}
