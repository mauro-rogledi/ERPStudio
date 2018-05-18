using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Forms
{

    public interface IDocumentBase
    {
        string Title { get; set; }
        event EventHandler Exit;
        DBMode DocumentMode { get; }
        string Name { get; set; }
        SqlProxyConnection Connection { get; }
        SqlProxyTransaction Transaction { get; }
    }

    public interface IDocument : IDocumentBase
    {
        DialogResult MyMessageBox(string text);
        DialogResult MyMessageBox(string text, string caption);
        DialogResult MyMessageBox(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
        DialogResult MyMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

        void Close();

        List<Addon> AddonList { get; }


    }
}
