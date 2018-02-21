#region Copyright ©2005, Cristi Potlog - All Rights Reserved

/* ------------------------------------------------------------------- *
*                            Cristi Potlog                             *
*                  Copyright ©2005 - All Rights reserved               *
*                                                                      *
* THIS SOURCE CODE IS PROVIDED "AS IS" WITH NO WARRANTIES OF ANY KIND, *
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE        *
* WARRANTIES OF DESIGN, MERCHANTIBILITY AND FITNESS FOR A PARTICULAR   *
* PURPOSE, NONINFRINGEMENT, OR ARISING FROM A COURSE OF DEALING,       *
* USAGE OR TRADE PRACTICE.                                             *
*                                                                      *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.             *
* ------------------------------------------------------------------- */

#endregion Copyright ©2005, Cristi Potlog - All Rights Reserved

#region References

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace ERPFramework.Controls
{
    #region Enums

    /// <summary>
    /// Represents possible styles of a wizard page.
    /// </summary>
    public enum WizardPageStyle
    {
        /// <summary>
        /// Represents a OK wizard page with white background,
        /// a large logo on the left and OK button.
        /// </summary>
        OK,

        /// <summary>
        /// Represents a finish wizard page with white background,
        /// a large logo on the left and OK button.
        /// </summary>
        Finish,

        /// <summary>
        /// Represents a blank wizard page.
        /// </summary>
        Custom
    }

    #endregion

    /// <summary>
    /// Represents a wizard page control with basic layout functionality.
    /// </summary>
    [DefaultEvent("Click")]
    [Designer(typeof(WizardPage.WizardPageDesigner))]
    [Localizable(true)]
    public class WizardPage : Panel
    {
        #region Consts

        #endregion

        #region Fields

        private string title = String.Empty;
        private string description = String.Empty;
        private WizardPageStyle style = WizardPageStyle.Custom;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="WizardPage"/> class.
        /// </summary>
        public WizardPage()
        {
            // reset control style to improove rendering (reduce flicker)
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the style of the wizard page.
        /// </summary>
        [DefaultValue(WizardPageStyle.Custom)]
        [Category("Wizard")]
        [Description("Gets or sets the style of the wizard page.")]
        public WizardPageStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        /// <summary>
        /// Gets or sets the title of the wizard page.
        /// </summary>
        [DefaultValue("")]
        [Category("Wizard")]
        [Description("Gets or sets the title of the wizard page.")]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }
                if (this.title != value)
                {
                    this.title = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the description of the wizard page.
        /// </summary>
        [DefaultValue("")]
        [Category("Wizard")]
        [Description("Gets or sets the description of the wizard page.")]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }
                if (this.description != value)
                {
                    this.description = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Inner classes

        /// <summary>
        /// This is a designer for the Banner.
        /// This designer locks the control vertical sizing.
        /// </summary>
        internal class WizardPageDesigner : ParentControlDesigner
        {
            /// <summary>
            /// Gets the selection rules that indicate the movement capabilities of a component.
            /// </summary>
            public override SelectionRules SelectionRules
            {
                get
                {
                    // lock the control
                    return SelectionRules.Visible | SelectionRules.Locked;
                }
            }
        }

        #endregion
    }
}