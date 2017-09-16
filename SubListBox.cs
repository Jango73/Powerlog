
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PowerLog
{
    public class SubListBox : ListBox
    {
        public delegate void WheelHandlerDelegate(object sender, KeyEventArgs args);

        public event WheelHandlerDelegate WheelDelegate;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message msg)
        {
            // const int WM_VSCROLL = 0x0115;
            const int WM_MOUSEWHEEL = 0x020A;

            base.WndProc(ref msg);

            switch (msg.Msg)
            {
                case WM_MOUSEWHEEL:
                    if (WheelDelegate != null)
                    {
                        try
                        {
                            UInt32 Distance = ((((UInt32)msg.WParam) & 0xFFFF0000) >> 16);

                            if (((UInt32)msg.WParam & 0x80000000) == 0x80000000)
                            {
                                WheelDelegate(this, new KeyEventArgs(Keys.PageDown));
                            }
                            else
                            {
                                WheelDelegate(this, new KeyEventArgs(Keys.PageUp));
                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex.ToString());
                        }
                    }
                    break;
            }
        }
    }
}
