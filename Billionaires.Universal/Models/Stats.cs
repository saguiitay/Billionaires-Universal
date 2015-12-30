using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Stats : ViewModelBase
    {
        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);

        private int _ytdRel;
        private int _ytd;
        private int _lastRel;
        private int _last;
        private List<int> _hold;
        private float _net;
        private int _rank;
        private readonly NumberFormatInfo _myNfi;

        public Stats()
        {
            _myNfi = new NumberFormatInfo
                {
                    CurrencyNegativePattern = 1,
                    CurrencySymbol = "$"
                };
        }

        public int Rank
        {
            get { return _rank; }
            set { Set(ref _rank, value); }
        }

        public float Net
        {
            get { return _net; }
            set
            {
                Set(ref _net, value);
                RaisePropertyChanged(nameof(NetValue));
            }
        }

        public string NetValue => string.Format("${0:0.0#}B", _net / 100f);

        public List<int> Hold
        {
            get { return _hold; }
            set { Set(ref _hold, value); }
        }

        public int Last
        {
            get { return _last; }
            set
            {
                Set(ref _last, value);
                RaisePropertyChanged(nameof(LastText));
            }
        }

        public string LastText
        {
            get
            {
                var unit = "M";
                var val = _last/100f;
                if (Math.Abs(val) > 1000)
                {
                    val = val/1000f;
                    unit = "B";
                }
                
                return val.ToString("C", _myNfi) + unit;
            }
        }

        public int Last_rel
        {
            get { return _lastRel; }
            set
            {
                Set(ref _lastRel, value);
                RaisePropertyChanged(nameof(LastRelText));
            }
        }

        public string LastRelText
        {
            get { return string.Format("{0:0.0#}", _lastRel / 100f); }
        }

        public Brush LastColor
        {
            get { return _last < 0 ? RedBrush : GreenBrush; }
        }



        public int Ytd
        {
            get { return _ytd; }
            set
            {
                Set(ref _ytd, value);
                RaisePropertyChanged(nameof(YtdText));
            }
        }

        public string YtdText
        {
            get
            {
                var unit = "M";
                var val = _ytd / 100f;
                if (Math.Abs(val) > 1000)
                {
                    val = val / 1000f;
                    unit = "B";
                }
                return val.ToString("C", _myNfi) + unit;
            }
        }

        public int Ytd_rel
        {
            get { return _ytdRel; }
            set
            {
                Set(ref _ytdRel, value);
                RaisePropertyChanged(nameof(YtdRelText));
            }
        }

        public string YtdRelText
        {
            get { return string.Format("{0:0.0#}", _ytdRel / 100f); }
        }

        public Brush YtdColor
        {
            get { return _ytd < 0 ? RedBrush : GreenBrush; }
        }

    }
}