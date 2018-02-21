using System;
using System.ComponentModel;
using System.Globalization;
using MetroFramework.Controls;

namespace ERPFramework.Controls
{
    public partial class MetroInputDateRange : MetroUserControl
    {
        public enum RangeTypes
        {
            E_LastWeek,
            E_LastMonth,
            E_LastTwoMonths,
            E_LastThreeMonths,
            E_LastFourMonths,
            E_LastSixMonths,
            E_LastNineMonths,
            E_LastYear,
            E_CurrentWeek,
            E_CurrentMonth,
            E_CurrentTwoMonth,
            E_CurrentQuarter,
            E_CurrentTrimester,
            E_CurrentSemester,
            E_CurrentYear,
            E_PreviousWeek,
            E_PreviousMonth,
            E_PreviousTwoMonth,
            E_PreviousQuarter,
            E_PreviousTrimester,
            E_PreviousSemester,
            E_PreviousYear,
            E_AllDate,
            E_Custom
        }

        #region Field
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Browsable(false)]
        [DefaultValue(typeof(DateTime), "1753/01/01")]
        public DateTime DateFrom { get { return dtbFrom.Today; } }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Browsable(false)]
        [DefaultValue(typeof(DateTime), "1753/01/01")]
        public DateTime DateTo { get { return dtbTo.Today; } }

        private DateTime currentDate;
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(typeof(DateTime), "1753/01/01")]
        public DateTime CurrentDate { set { currentDate = value; SetDate(); } }

        private RangeTypes rangeType = RangeTypes.E_AllDate;
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(typeof(RangeTypes), "23")]
        public RangeTypes RangeType { get { return rangeType; } set { rangeType = value; cbbRangeType.SelectedValue = rangeType.Int();  SetDate(); } }

        EnumsManager<RangeTypes> eRangeTypeManager = new EnumsManager<RangeTypes>(Properties.Resources.ResourceManager);
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        #endregion

        public MetroInputDateRange()
        {
            InitializeComponent();
        }

        #region Override methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            eRangeTypeManager.AttachTo(cbbRangeType);
        }
        #endregion

        #region private methods
        private void SetDate()
        {
            dtbFrom.Enabled = rangeType != RangeTypes.E_AllDate;
            dtbTo.Enabled = rangeType != RangeTypes.E_AllDate;

            switch (rangeType)
            {
                case RangeTypes.E_AllDate:
                    break;
                case RangeTypes.E_LastWeek:
                    dtbFrom.Today = currentDate.AddDays(-6);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastMonth:
                    dtbFrom.Today = currentDate.AddMonths(-1);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastTwoMonths:
                    dtbFrom.Today = currentDate.AddMonths(-2);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastThreeMonths:
                    dtbFrom.Today = currentDate.AddMonths(-3);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastFourMonths:
                    dtbFrom.Today = currentDate.AddMonths(-4);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastSixMonths:
                    dtbFrom.Today = currentDate.AddMonths(-6);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastNineMonths:
                    dtbFrom.Today = currentDate.AddMonths(-9);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_LastYear:
                    dtbFrom.Today = currentDate.AddMonths(-12);
                    dtbTo.Today = currentDate;
                    break;
                case RangeTypes.E_CurrentWeek:
                    dtbFrom.Today = currentDate.AddDays(DayOfWeek.Monday - currentDate.DayOfWeek);
                    dtbTo.Today = dtbFrom.Today.AddDays(6);
                    break;
                case RangeTypes.E_CurrentMonth:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var days = dfi.Calendar.GetDaysInMonth(dtbFrom.Today.Year, dtbFrom.Today.Month) - 1;
                        dtbTo.Today = dtbFrom.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_CurrentTwoMonth:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 1) % 2;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta);
                        dtbTo.Today = dtbFrom.Today.AddMonths(1);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) -1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_CurrentQuarter:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 2) % 3;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta);
                        dtbTo.Today = dtbFrom.Today.AddMonths(2);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) -1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_CurrentTrimester:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 3) % 4;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta);
                        dtbTo.Today = dtbFrom.Today.AddMonths(3);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_CurrentSemester:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 5) % 6;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta);
                        dtbTo.Today = dtbFrom.Today.AddMonths(5);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_CurrentYear:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 11) % 12;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta);
                        dtbTo.Today = dtbFrom.Today.AddMonths(11);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousWeek:
                    dtbFrom.Today = currentDate.AddDays(DayOfWeek.Monday - currentDate.DayOfWeek - 7);
                    dtbTo.Today = dtbFrom.Today.AddDays(6);
                    break;
                case RangeTypes.E_PreviousMonth:
                    {
                        dtbFrom.Today = currentDate.AddMonths(-1);
                        dtbFrom.Today = new DateTime(dtbFrom.Today.Year, dtbFrom.Today.Month, 1);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbFrom.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousTwoMonth:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 1) % 2;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta - 2);
                        dtbTo.Today = dtbFrom.Today.AddMonths(1);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousQuarter:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 2) % 3;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta-3);
                        dtbTo.Today = dtbFrom.Today.AddMonths(2);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousTrimester:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 3) % 4;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta-4);
                        dtbTo.Today = dtbFrom.Today.AddMonths(3);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousSemester:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 5) % 6;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta-6);
                        dtbTo.Today = dtbFrom.Today.AddMonths(5);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;
                case RangeTypes.E_PreviousYear:
                    {
                        dtbFrom.Today = currentDate.AddDays(-(currentDate.Day - 1));
                        var delta = (dtbFrom.Today.Month + 11) % 12;
                        dtbFrom.Today = dtbFrom.Today.AddMonths(-delta-12);
                        dtbTo.Today = dtbFrom.Today.AddMonths(11);
                        var days = dfi.Calendar.GetDaysInMonth(dtbTo.Today.Year, dtbTo.Today.Month) - 1;
                        dtbTo.Today = dtbTo.Today.AddDays(days);
                    }
                    break;

            }
        }
        #endregion

        private void cbbRangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || currentDate == DateTime.MinValue)
                return;

            rangeType = eRangeTypeManager.GetValue();
            SetDate();
        }
    }
}
