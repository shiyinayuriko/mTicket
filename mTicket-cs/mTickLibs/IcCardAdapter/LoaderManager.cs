using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using mTickLibs.NfcAdapter;

namespace mTickLibs.IcCardAdapter
{
    public class LoaderManager
    {
        private Int32 _hdev = -1;
        public void SetupDevice()
        {
            if (_hdev > 0) LoaderDll.fw_exit(_hdev);

            var tmphdev = LoaderDll.fw_init(100, 0);
            if (tmphdev < 0)
            {
                throw new IcCardException(String.Format("fw_init({0},{1}):{2}", 100, 0, tmphdev));
            }
            else
            {
                _hdev = tmphdev;
            }
        }

        public AbstractIcCard GetCard()
        {
            UInt32 type = 0;
            var ret = LoaderDll.fw_request(_hdev, LoaderDll.RequestModeIdle,ref type);
            if (ret == 65) { return null;}
            if (ret != 0)
            {
                throw new IcCardException(String.Format("fw_request({0},{1},{2}):{3}", _hdev, LoaderDll.RequestModeIdle, type, ret));
            }
            AbstractIcCard icCard;
            switch (type)
            {
                case 4:icCard = MifareOneIcCard.Get();break;
                case 68: icCard = MifareUltraLightIcCard.Get(_hdev); break;
                default: icCard = DefaultIcCard.Get(); break;
            }

            return icCard;
        }

        public delegate byte[][] OnDealwithCard(AbstractIcCard card);

        public OnDealwithCard DealwithCard;
        public void AutoLoad()
        {
            SetupDevice();
            while (true)
            {
                try
                {
                    Thread.Sleep(500);
                    var card = GetCard();
                    if(card == null) continue;
                    byte[][] bytes =  DealwithCard(card);
                    if(bytes!=null) card.Save(_hdev,bytes);
                }
                catch (IcCardException e)
                {
                    SetupDevice();
                }
            }
        }

        public void StartAutoLoad()
        {
            new Thread(AutoLoad).Start();
        }
    }
}
