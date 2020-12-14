namespace Archiwum
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiwumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naMakulatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grid = new System.Windows.Forms.DataGridView();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.edytujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.odświeżToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kopiujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eksportujDoExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delAllDiv = new System.Windows.Forms.ToolStripSeparator();
            this.delAll = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.Menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiwumToolStripMenuItem,
            this.naMakulatureToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiwumToolStripMenuItem
            // 
            this.archiwumToolStripMenuItem.Name = "archiwumToolStripMenuItem";
            this.archiwumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.archiwumToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.archiwumToolStripMenuItem.Text = "Archiwum";
            this.archiwumToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // naMakulatureToolStripMenuItem
            // 
            this.naMakulatureToolStripMenuItem.Name = "naMakulatureToolStripMenuItem";
            this.naMakulatureToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.naMakulatureToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.naMakulatureToolStripMenuItem.Text = "Na makulaturę";
            this.naMakulatureToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.pomocToolStripMenuItem.Text = "&Pomoc...";
            this.pomocToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.ContextMenuStrip = this.Menu;
            this.grid.Location = new System.Drawing.Point(0, 24);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(800, 401);
            this.grid.TabIndex = 1;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
            // 
            // Menu
            // 
            this.Menu.AccessibleName = "";
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSearch,
            this.toolStripMenuItem1,
            this.edytujToolStripMenuItem,
            this.dodajToolStripMenuItem,
            this.usuńToolStripMenuItem,
            this.toolStripMenuItem2,
            this.odświeżToolStripMenuItem,
            this.kopiujToolStripMenuItem,
            this.eksportujDoExcelToolStripMenuItem,
            this.delAllDiv,
            this.delAll});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(242, 201);
            this.Menu.Text = "Menu";
            this.Menu.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Opening);
            // 
            // tbSearch
            // 
            this.tbSearch.AutoSize = false;
            this.tbSearch.CausesValidation = false;
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.ShortcutsEnabled = false;
            this.tbSearch.Size = new System.Drawing.Size(130, 23);
            this.tbSearch.ToolTipText = "Po naciśnięciu Enter zostaną wyświetlone pola z podaną wartością\r\n(Program nieste" +
    "ty nie pozwala na pisanie polskich liter)";
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSearch_KeyPress);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
            // 
            // edytujToolStripMenuItem
            // 
            this.edytujToolStripMenuItem.Name = "edytujToolStripMenuItem";
            this.edytujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.edytujToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.edytujToolStripMenuItem.Text = "&Edytuj...";
            this.edytujToolStripMenuItem.Click += new System.EventHandler(this.EdytujToolStripMenuItem_Click);
            // 
            // dodajToolStripMenuItem
            // 
            this.dodajToolStripMenuItem.Name = "dodajToolStripMenuItem";
            this.dodajToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.dodajToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.dodajToolStripMenuItem.Text = "&Dodaj...";
            this.dodajToolStripMenuItem.Click += new System.EventHandler(this.DodajToolStripMenuItem_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.usuńToolStripMenuItem.Text = "&Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.UsuńToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(238, 6);
            // 
            // odświeżToolStripMenuItem
            // 
            this.odświeżToolStripMenuItem.Name = "odświeżToolStripMenuItem";
            this.odświeżToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.odświeżToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.odświeżToolStripMenuItem.Text = "&Odśwież";
            this.odświeżToolStripMenuItem.Click += new System.EventHandler(this.OdświeżToolStripMenuItem_Click);
            // 
            // kopiujToolStripMenuItem
            // 
            this.kopiujToolStripMenuItem.Name = "kopiujToolStripMenuItem";
            this.kopiujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.kopiujToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.kopiujToolStripMenuItem.Text = "&Kopiuj";
            this.kopiujToolStripMenuItem.Click += new System.EventHandler(this.KopiujToolStripMenuItem_Click);
            // 
            // eksportujDoExcelToolStripMenuItem
            // 
            this.eksportujDoExcelToolStripMenuItem.Name = "eksportujDoExcelToolStripMenuItem";
            this.eksportujDoExcelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.eksportujDoExcelToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.eksportujDoExcelToolStripMenuItem.Text = "&Eksportuj do Excel";
            this.eksportujDoExcelToolStripMenuItem.Click += new System.EventHandler(this.EksportujDoExcelToolStripMenuItem_Click);
            // 
            // delAllDiv
            // 
            this.delAllDiv.Name = "delAllDiv";
            this.delAllDiv.Size = new System.Drawing.Size(238, 6);
            // 
            // delAll
            // 
            this.delAll.Name = "delAll";
            this.delAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.delAll.Size = new System.Drawing.Size(241, 22);
            this.delAll.Text = "&Usuń wszystkie";
            this.delAll.ToolTipText = "Usunięcię wszystkich pozycji przeznaczonych na makulaturę";
            this.delAll.Click += new System.EventHandler(this.DelAll_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(21, 17);
            this.lblStatus.Text = "[  ]";
            this.lblStatus.MouseHover += new System.EventHandler(this.LblStatus_MouseHover);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(816, 488);
            this.Name = "FormMain";
            this.Text = "Archiwum";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archiwumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naMakulatureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripTextBox tbSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem edytujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem odświeżToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eksportujDoExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delAll;
        private System.Windows.Forms.ToolStripSeparator delAllDiv;
        private System.Windows.Forms.ToolStripMenuItem kopiujToolStripMenuItem;
    }
}

