using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace ERPFramework.Controls
{

    public interface ISnapLineControl
    {
        Control textBoxValue { get; }
        Control labelValue { get; }
    }

    public class MySnapLinesDesigner : ControlDesigner
    {
        public override IList SnapLines
        {
            get
            {
                /* Code from above */
                IList snapLines = base.SnapLines;

                // *** This will need to be modified to match your user control
                ISnapLineControl control = Control as ISnapLineControl;
                if (control == null) { return snapLines; }

                // *** This will need to be modified to match the item in your user control
                // This is the control in your UC that you want SnapLines for the entire UC
                IDesigner designer = TypeDescriptor.CreateDesigner(
                    control.textBoxValue, typeof(IDesigner));
                if (designer == null) { return snapLines; }

                // *** This will need to be modified to match the item in your user control
                designer.Initialize(control.textBoxValue);

                using (designer)
                {
                    ControlDesigner boxDesigner = designer as ControlDesigner;
                    if (boxDesigner == null) { return snapLines; }

                    foreach (SnapLine line in boxDesigner.SnapLines)
                    {
                        if (line.SnapLineType == SnapLineType.Baseline)
                        {
                            if (control.textBoxValue != null)
                                snapLines.Add(new SnapLine(SnapLineType.Baseline,
                                      line.Offset + control.textBoxValue.Top,
                                      line.Filter, line.Priority));
                            break;
                        }
                    }
                }

                if (control.labelValue == null || control.labelValue.Tag.ToString() == bool.FalseString)
                    return snapLines;

                // *** This will need to be modified to match the item in your user control
                // This is the control in your UC that you want SnapLines for the entire UC
                designer = TypeDescriptor.CreateDesigner(
                    control.labelValue, typeof(IDesigner));
                if (designer == null) { return snapLines; }

                // *** This will need to be modified to match the item in your user control
                designer.Initialize(control.labelValue);

                using (designer)
                {
                    ControlDesigner boxDesigner = designer as ControlDesigner;
                    if (boxDesigner == null) { return snapLines; }

                    foreach (SnapLine line in boxDesigner.SnapLines)
                    {
                        if (line.SnapLineType == SnapLineType.Baseline)
                        {
                            if (control.labelValue != null)
                                snapLines.Add(new SnapLine(SnapLineType.Baseline,
                                      line.Offset + control.labelValue.Top,
                                      line.Filter, line.Priority));
                            break;
                        }
                    }
                }
                return snapLines;
            }

        }
    }
}
