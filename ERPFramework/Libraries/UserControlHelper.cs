using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPFramework.Libraries
{
    public sealed class OpenControlHelper : Component, ICloneable, ISupportInitialize
    {
        public enum ControlPosition {Center, Manual, DockLeft, DockRight, DockTop, DockDown, Owner}

        Dictionary<string, Control> _listControl = new Dictionary<string, Control>();
        MetroForm _owner = null;

        public MetroFramework.Forms.MetroForm Owner { get { return _owner; } set { _owner = value; } }

        public void AddControl(Control userCtrl)
        {
            System.Diagnostics.Debug.Assert(!_listControl.ContainsKey(userCtrl.Name), "Control Duplicated");
            _listControl.Add(userCtrl.Name, userCtrl);
        }

        #region Constructor
        public OpenControlHelper(MetroFramework.Forms.MetroForm owner)
        {
            _owner = owner;
        }

        public OpenControlHelper()
        {
        } 
        #endregion

        #region ISupportInitialize


        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            OpenControlHelper newUserControlHelper = new OpenControlHelper(_owner);
            return newUserControlHelper;
        }

        public object Clone(ContainerControl owner)
        {
            OpenControlHelper clonedManager = Clone() as OpenControlHelper;


            return clonedManager;
        }

        #endregion

        public Control ShowControl(Control userCtrl, bool modal = false, ControlPosition position = ControlPosition.Center, System.Windows.Forms.Control owner = null, Point? location = null)
        {
            System.Diagnostics.Debug.Assert(!_listControl.ContainsKey(userCtrl.Name), "Control Duplicated");

            if (modal && !(userCtrl is Form))
                LockAllControl();

            Point ctrlLocation = CalculateLocation(userCtrl, position, location, owner);

            userCtrl.Location = ctrlLocation;

            Show(userCtrl, modal);

            return userCtrl;
        }

        private void Show(Control userCtrl, bool modal)
        {

            if (userCtrl is MetroForm)
            {
                MetroForm metroForm = userCtrl as MetroForm;
                metroForm.StartPosition = FormStartPosition.Manual;
                ERPFramework.GlobalInfo.StyleManager.Clone(metroForm);
                metroForm.FormClosed += Metroform_FormClosed;
                metroForm.FormClosing += MetroForm_FormClosing;

                if (modal)
                    LockAllControl();

                _listControl.Add(userCtrl.Name, userCtrl);
                Size sz = metroForm.Size;
                metroForm.Size = Size.Empty;
                metroForm.StyleManager.Update();
                metroForm.Show(_owner);
                MetroFramework.Animation.ExpandAnimation exp = new MetroFramework.Animation.ExpandAnimation();
                exp.Start(metroForm, sz, MetroFramework.Animation.TransitionType.EaseInExpo, 4);
            }
            //else if (userCtrl is MetroUserControl)
            //{
            //    MetroUserControl metroCtrl = userCtrl as MetroUserControl;
            //    metroCtrl.StyleManager = styleManager;
            //    metroCtrl.Clo += Metroform_FormClosed;

            //    if (modal)
            //        metroform.ShowDialog();
            //    else
            //        metroform.Show();
            //}
            else
                System.Diagnostics.Debug.Assert(false, "Unknow control type");
        }

        private void MetroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Metroform_FormClosed(object sender, FormClosedEventArgs e)
        {
            MetroForm metroform = sender as MetroForm;
            _listControl.Remove(metroform.Name);
            UnlockAllControl();

            _owner.Focus();

        }

        private Point CalculateLocation(Control userCtrl, ControlPosition position, Point? location, Control owner)
        {
            Point pt = Point.Empty;
            Point ownerPos = Point.Empty;
            switch (position)
            {
                case ControlPosition.Owner:
                    System.Diagnostics.Debug.Assert(owner !=null, "Missing Owner");
                    if (userCtrl is Form)
                        pt = owner.PointToScreen(Point.Empty);
                    else
                        pt = owner.Location;

                    pt.Y += owner.Height;
                    ownerPos = _owner.PointToScreen(Point.Empty);
                    if (pt.X + userCtrl.Width > ownerPos.X + _owner.Width)
                        pt.X = ownerPos.X + _owner.Width - userCtrl.Width;
                    return pt;
                case ControlPosition.Center:
                    ownerPos = _owner.PointToScreen(Point.Empty);
                    ownerPos.X += (owner.Width - userCtrl.Width) / 2;
                    ownerPos.Y += (owner.Height - userCtrl.Height) / 2;
                    if (ownerPos.X < 0) ownerPos.X = 0;
                    if (ownerPos.Y < 0) ownerPos.Y = 0;
                    return ownerPos;
                case ControlPosition.Manual:
                    System.Diagnostics.Debug.Assert(location != null, "Location is null");
                    return (Point)location;
            }

            return Point.Empty;
        }

        private void LockAllControl()
        {
            foreach(KeyValuePair<string, Control> ctrl in _listControl)
                ctrl.Value.Enabled = false;
        }

        private void UnlockAllControl()
        {
            foreach (KeyValuePair<string, Control> ctrl in _listControl)
                ctrl.Value.Enabled = true;
        }
    }
}
