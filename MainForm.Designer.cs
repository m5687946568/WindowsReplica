
namespace WindowsReplica
{
    partial class WindowsReplica
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsReplica));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_OnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ClickThrough = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Minimized = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.FormMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconMenu.SuspendLayout();
            this.FormMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "WindowsReplica";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // NotifyIconMenu
            // 
            this.NotifyIconMenu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.NotifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_OnTop,
            this.ToolStripMenuItem_ClickThrough,
            this.toolStripSeparator3,
            this.ToolStripMenuItem_Reset,
            this.ToolStripSeparator2,
            this.ToolStripMenuItem_Show,
            this.ToolStripMenuItem_Minimized,
            this.ToolStripSeparator1,
            this.ToolStripMenuItem_Exit});
            this.NotifyIconMenu.Name = "notifyIconmenu";
            this.NotifyIconMenu.ShowCheckMargin = true;
            this.NotifyIconMenu.ShowImageMargin = false;
            this.NotifyIconMenu.Size = new System.Drawing.Size(152, 154);
            this.NotifyIconMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NotifyIconMenu_ItemClicked);
            // 
            // ToolStripMenuItem_OnTop
            // 
            this.ToolStripMenuItem_OnTop.Checked = true;
            this.ToolStripMenuItem_OnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_OnTop.Name = "ToolStripMenuItem_OnTop";
            this.ToolStripMenuItem_OnTop.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_OnTop.Text = "OnTop";
            // 
            // ToolStripMenuItem_ClickThrough
            // 
            this.ToolStripMenuItem_ClickThrough.Name = "ToolStripMenuItem_ClickThrough";
            this.ToolStripMenuItem_ClickThrough.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_ClickThrough.Text = "Click Through";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
            // 
            // ToolStripMenuItem_Reset
            // 
            this.ToolStripMenuItem_Reset.Name = "ToolStripMenuItem_Reset";
            this.ToolStripMenuItem_Reset.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_Reset.Text = "Reset";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // ToolStripMenuItem_Show
            // 
            this.ToolStripMenuItem_Show.Name = "ToolStripMenuItem_Show";
            this.ToolStripMenuItem_Show.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_Show.Text = "Show";
            // 
            // ToolStripMenuItem_Minimized
            // 
            this.ToolStripMenuItem_Minimized.Name = "ToolStripMenuItem_Minimized";
            this.ToolStripMenuItem_Minimized.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_Minimized.Text = "Minimized";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(151, 22);
            this.ToolStripMenuItem_Exit.Text = "Exit";
            // 
            // FormMenu
            // 
            this.FormMenu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.FormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.FormMenu.Name = "formmenu";
            this.FormMenu.Size = new System.Drawing.Size(95, 26);
            this.FormMenu.TabStop = true;
            this.FormMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FormMenu_Opening);
            this.FormMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FormMenu_ItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // WindowsReplica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(732, 410);
            this.ContextMenuStrip = this.FormMenu;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsReplica";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowsReplica_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowsReplica_MouseMove);
            this.Resize += new System.EventHandler(this.WindowsReplica_Resize);
            this.NotifyIconMenu.ResumeLayout(false);
            this.FormMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_OnTop;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip NotifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Minimized;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Show;
        private System.Windows.Forms.ContextMenuStrip FormMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Reset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ClickThrough;
    }
}

