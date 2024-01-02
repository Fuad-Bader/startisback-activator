﻿/*
    @app        : StartIsBack Activator
    @repo       : https://github.com/Aetherinox/startisback-activator
    @author     : Aetherinox
*/

using SIBActivator;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SIBActivator.Forms;
using System.Runtime.ConstrainedExecution;
using Lng = SIBActivator.Properties.Resources;
using Cfg = SIBActivator.Properties.Settings;

namespace SIBActivator
{
    public partial class FormParent : Form, IReceiver
    {

        /*
            Define > Classes
        */

        private Patch Patch         = new Patch( );
        private Helpers Helpers     = new Helpers( );

        /*
            Define > Internal > Helper
        */

        internal Helpers Helper
        {
            set     { Helpers = value;  }
            get     { return Helpers;   }
        }

        /*
            Define > Mouse
        */

        private bool mouseDown;
        private Point lastLocation;

        /*
            Frame > Parent
        */

        public FormParent( )
        {
            InitializeComponent( );
            StatusBar.InitializeReceiver( this );

            this.statusStrip.Renderer   = new StatusBar_Renderer( );

            string ver                  = AppInfo.ProductVersionCore.ToString( );
            string product              = AppInfo.Title;
            string tm                   = AppInfo.Trademark;

            btn_Close.Parent            = imgHeader;
            btn_Close.BackColor         = Color.Transparent;

            btn_Minimize.Parent         = imgHeader;
            btn_Minimize.BackColor      = Color.Transparent;

            lbl_Product.Parent          = imgHeader;
            lbl_Product.BackColor       = Color.Transparent;

            lbl_Version.Parent          = imgHeader;
            lbl_Version.BackColor       = Color.Transparent;
            lbl_Version.Text            = "v" + ver + " by " + tm;
            lbl_Product.Text            = product;

            lbl_intro.Text              = string.Format( Lng.txt_intro, Environment.NewLine );
            btnPatch.Text               = Lng.btn_patch;

            string l1                   = Lng.txt_intro1;
            string l2                   = Lng.txt_intro2;

            rtxt_Intro.Text             = "";

            rtxt_Intro.AppendText       ( l1 );
            rtxt_Intro.Select           ( 0, l1.Length );
            rtxt_Intro.SelectionColor   = Color.White;

            rtxt_Intro.AppendText       ( "\n\n" );

            rtxt_Intro.AppendText       ( l2 );
            rtxt_Intro.Select           ( l1.Length + 1, l2.Length );
            rtxt_Intro.SelectionColor   = Color.FromArgb( 31, 133, 222 );

            rtxt_Intro.AppendText       ( "\n" );

        }

        /*
            Frame > Parent > Load
        */

        private void FormParent_Load( object sender, EventArgs e )
        {
            mnuTop.Renderer             = new ToolStripProfessionalRenderer( new mnu_ColorTable( ) );
            status_Label.Text           = string.Format( Lng.status_generate );
            statusStrip.Refresh( );
        }

        #region "Main Window: Control Buttons"

            /*
                Control Buttons
                    ->  Minimize
                    ->  Maximize
                    ->  Close

                Icons:  http://modernicons.io/segoe-mdl2/cheatsheet/
                Font:   Segoe MDL2 Assets
            */

            /*
                Window > Button > Minimize > Click
            */

            private void btn_Window_Minimize_Click( object sender, EventArgs e )
            {
                this.WindowState = FormWindowState.Minimized;
            }

            /*
                Window > Button > Minimize > Mouse Enter
            */

            private void btn_Window_Minimize_MouseEnter( object sender, EventArgs e )
            {
                btn_Minimize.ForeColor = Color.FromArgb( 243, 41, 101 );
            }

            /*
                Window > Button > Minimize > Mouse Leave
            */

            private void btn_Window_Minimize_MouseLeave( object sender, EventArgs e )
            {
                btn_Minimize.ForeColor = Color.FromArgb( 255, 255, 255 );
            }

            /*
                Window > Button > Close > Click
            */

            private void btn_Window_Close_Click( object sender, EventArgs e )
            {
                Application.Exit( );
            }

            /*
                Window > Button > Close > Mouse Enter
            */

            private void btn_Window_Close_MouseEnter( object sender, EventArgs e )
            {
                btn_Close.ForeColor = Color.FromArgb( 243, 41, 101 );
            }

            /*
                Window > Button > Close > Mouse Leave
            */

            private void btn_Window_Close_MouseLeave( object sender, EventArgs e )
            {
                btn_Close.ForeColor = Color.FromArgb( 255, 255, 255 );
            }

        #endregion

        #region "Main Window: Dragging"

            /*
                Main Form > Mouse Down
                deals with moving form around on screen
            */

            private void MainForm_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown       = true;
                lastLocation    = e.Location;
            }

            /*
                Main Form > Mouse Up
                deals with moving form around on screen
            */

            private void MainForm_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
            }

            /*
                Main Form > Mouse Move
                deals with moving form around on screen
            */

            private void MainForm_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        #endregion

        #region "Top Menu"

            /*
                Top Menu > Color Overrides
            */

            public class mnu_ColorTable : ProfessionalColorTable
            {
                /*
                    Gets the starting color of the gradient used when
                    a top-level System.Windows.Forms.ToolStripMenuItem is pressed.
                */

                public override Color MenuItemPressedGradientBegin => Color.FromArgb( 255, 55, 55, 55 );

                /*
                    Gets the end color of the gradient used when a top-level
                    System.Windows.Forms.ToolStripMenuItem is pressed.
                */

                public override Color MenuItemPressedGradientEnd => Color.FromArgb( 255, 55, 55, 55 );

                /*
                    Gets the border color to use with a
                    System.Windows.Forms.ToolStripMenuItem.
                */

                public override Color MenuItemBorder => Color.FromArgb( 0, 45, 45, 45 );

                /*
                    Gets the starting color of the gradient used when the
                    System.Windows.Forms.ToolStripMenuItem is selected.
                */

                public override Color MenuItemSelectedGradientBegin => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Gets the end color of the gradient used when the
                    System.Windows.Forms.ToolStripMenuItem is selected.
                */

                public override Color MenuItemSelectedGradientEnd => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Gets the solid background color of the
                    System.Windows.Forms.ToolStripDropDown.
                */

                public override Color ToolStripDropDownBackground => Color.FromArgb( 255, 40, 40, 40 );

                /*
                    Top Menu > Image > Start Gradient Color
                */

                public override Color ImageMarginGradientBegin => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Top Menu > Image > Middle Gradient Color
                */

                public override Color ImageMarginGradientMiddle => Color.FromArgb( 0, 222, 31, 103 );

                /*
                    Top Menu > Image > End Gradient Color
                */

                public override Color ImageMarginGradientEnd => Color.FromArgb( 0, 222, 31, 103 );

                /*
                    Top Menu > Shadow Effect
                */

                public override Color SeparatorDark => Color.FromArgb( 0, 255, 255, 255 );

                /*
                    Top Menu > Border Color
                */

                public override Color MenuBorder => Color.FromArgb( 0, 45, 45, 45 );

                /*
                     Top Menu > Item Hover BG Color
                 */

                public override Color MenuItemSelected => Color.FromArgb( 255, 222, 31, 103 );
            }

            /*
                Top Menu > Paint
            */

            private void mnu_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                Color backColor             = Color.FromArgb( 35, 255, 255, 255 );
                var imgSize                 = mnuTop.ClientSize;
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, 1, imgSize.Width - 2, 1 );
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, imgSize.Height - 2, imgSize.Width - 2, 1 );
            }

            /*
                Top Menu > Click Item
            */

            private void mnu_Main_ItemClicked( object sender, ToolStripItemClickedEventArgs e ) { }

            /*
                Top Menu > Help > About
            */

            private void mnu_Item_About_Click( object sender, EventArgs e )
            {
                FormAbout to    = new FormAbout( );
                to.TopMost      = true;
                to.Show( );
            }

            /*
                Top Menu > Help > Contribute
            */

            private void mnu_Item_Contribute_Click( object sender, EventArgs e )
            {
                FormContribute to   = new FormContribute( );
                to.TopMost      = true;
                to.Show( );
            }

            /*
                Top Menu > Help > Github Updates
            */

            private void mnu_Item_GithubUpdates_Click( object sender, EventArgs e )
            {
                System.Diagnostics.Process.Start( Cfg.Default.app_url_github );
            }

            /*
                Top Menu > Help > x509 Certificate Validation
            */

            private void mnu_Item_Validate_Click( object sender, EventArgs e )
            {

                string exe_target = System.AppDomain.CurrentDomain.FriendlyName;
                if ( !File.Exists( exe_target ) )
                {

                    MessageBox.Show( string.Format( "Could not find executable's location. Aborting validation\n\nFilename: \n{0}", exe_target ),
                        "Integrity Check: Aborted",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    return;
                }

                string x509_cert    = Helpers.x509_Thumbprint( exe_target );

                /*
                    x509 certificate

                    Add integrity validation. Ensure the resource DLL has been signed by the developer,
                    otherwise cancel the patching step.
                */

                if ( x509_cert != "0" )
                {

                    /* certificate: resource file  signed */

                    if ( x509_cert.ToLower( ) == Cfg.Default.app_dev_piv_thumbprint.ToLower( ) )
                    {

                        /* certificate: resource file signed and authentic */

                        MessageBox.Show( string.Format( "Successfully validated that this patch is authentic, continuing...\n\nCertificate Thumbprint: \n{0}", x509_cert ),
                            "Integrity Check Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        /* certificate: resource file signed but not by developer */

                        MessageBox.Show( string.Format( "The fails associated to this patch have a signature, however, it is not by the developer who wrote the patch, aborting...\n\nCertificate Thumbprint: \n{0}", x509_cert ),
                            "Integrity Check Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    /* certificate: resource file not signed at all */

                    MessageBox.Show( string.Format( "The files for this activator are not signed and may be fake from another source. Files from this activator's developer will ALWAYS be signed.\n\nEnsure you downloaded this patch from the developer.\n\nFailed File(s):\n     {0}", exe_target ),
                        "Integrity Check Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
            }

            /*
                Top Menu > Separator
                Separates "Exit" from the other items in "About" dropdown.
            */

            private void mnu_Sep_Exit_Paint( object sender, PaintEventArgs e )
            {
                ToolStripSeparator toolStripSeparator = (ToolStripSeparator)sender;

                int width           = toolStripSeparator.Width;
                int height          = toolStripSeparator.Height;
                Color backColor     = Color.FromArgb( 255, 222, 31, 103 );

                // Fill the background.
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 0, 0, width, height );
            }

            /*
                Top Menu > File > Exit
            */

            private void mnu_Item_Exit_Click( object sender, EventArgs e )
            {
                Application.Exit( );
            }

        #endregion

        #region "Header"

            private void imgHeader_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void imgHeader_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
            }

            private void imgHeader_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        #endregion

        #region "Label: Title"

            private void lbl_Title_Click( object sender, EventArgs e ) { }

            private void lbl_Title_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_Title_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_Title_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        #endregion

        #region "Body: Intro"

            private void lbl_intro_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_intro_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
            }

            private void lbl_intro_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

            private void rtxt_Intro_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void rtxt_Intro_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
            }

            private void rtxt_Intro_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        #endregion

        #region "Button: Patch"

            /*
                Button > Patch > Click
            */

            private void btn_Patch_Click( object sender, EventArgs e )
            {
                Patch.Start( );
            }

        #endregion

        #region "Button: Open Folder"

            /*
                Auto-detects which folder StartIsBack is installed in and opens that folder
                in the user's File Explorer.
            */

            private void btn_OpenFolder_Click( object sender, EventArgs e )
            {
                string src_file_path        = Helper.FindApp( );

                if ( String.IsNullOrEmpty( src_file_path ) )
                {
                    MessageBox.Show(
                        string.Format( Lng.msgbox_nolocopen_msg ),
                        Lng.msgbox_nolocopen_title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                /*
                    target directory
                */

                if ( Directory.Exists( src_file_path ) )
                    Process.Start( "explorer.exe", src_file_path );

                /*
                    cannot locate mobaxterm program. Open dialog in Program Files(86)
                */

                else
                {
                    string path_progfiles = Helpers.ProgramFiles( );
                    Process.Start( "explorer.exe", path_progfiles );

                    MessageBox.Show(
                        string.Format( Lng.msgbox_nolocopen_msg ),
                        Lng.msgbox_nolocopen_title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

            }
        #endregion

        #region "Status Bar"

            /*
                status bar in lower part of the main interface.
                updated when certain actions are completed to inform the user.
            */

            private void status_Strip_ItemClicked( object sender, ToolStripItemClickedEventArgs e ) { }

            /*
                Statusbar > Mouse Actions
            */

            private void status_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void status_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void status_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

            /*
                Statusbar > Paint
            */

            private void statusStrip_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                Color backColor             = Color.FromArgb( 35, 255, 255, 255 );
                var imgSize                 = statusStrip.ClientSize;
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, 1, imgSize.Width - 2, 2 );
            }

            /*
                Receiver > Update Status
            */

            public void Status( string message )
            {
                status_Label.Text = message;
            }

            /*
                Status Bar > Color Table (Override)
            */

            public class Status_ClrTable : ProfessionalColorTable
            {
                public override Color StatusStripGradientBegin => Color.FromArgb( 35, 35, 35 );
                public override Color StatusStripGradientEnd => Color.FromArgb( 35, 35, 35 );
            }

            /*
                Status Bar > Renderer
                Override colors
            */

            public class StatusBar_Renderer : ToolStripProfessionalRenderer
            {
                public StatusBar_Renderer( )
                    : base( new Status_ClrTable( ) ) { }

                protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
                {
                    if ( !( e.ToolStrip is StatusStrip ) )
                        base.OnRenderToolStripBorder( e );
                }
            }

        #endregion

    }
}
