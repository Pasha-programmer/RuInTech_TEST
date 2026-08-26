namespace RuInTech_TEST.UI
{
    partial class AssetsForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView gridAssets;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuBanks;
        private System.Windows.Forms.ToolStripMenuItem menuRawMaterialKinds;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridAssets = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();

            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBanks = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRawMaterialKinds = new System.Windows.Forms.ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)(this.gridAssets)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // gridAssets
            this.gridAssets.AllowUserToAddRows = false;
            this.gridAssets.AllowUserToDeleteRows = false;
            this.gridAssets.AllowUserToResizeRows = false;
            this.gridAssets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAssets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAssets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAssets.Location = new System.Drawing.Point(0, 0);
            this.gridAssets.MultiSelect = false;
            this.gridAssets.Name = "gridAssets";
            this.gridAssets.ReadOnly = true;
            this.gridAssets.RowHeadersVisible = false;
            this.gridAssets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAssets.Size = new System.Drawing.Size(884, 383);
            this.gridAssets.TabIndex = 0;
            this.gridAssets.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAssets_CellDoubleClick);

            // pnlButtons
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 411);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Size = new System.Drawing.Size(884, 50);
            this.pnlButtons.TabIndex = 1;

            // btnRefresh
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Location = new System.Drawing.Point(769, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(220, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(100, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(10, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(884, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";

            // menuFile
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] 
            {
                this.menuBanks,
                this.menuRawMaterialKinds,
            });
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(65, 24);
            this.menuFile.Text = "Меню";

            // menuBanks
            this.menuBanks.Name = "menuBanks";
            this.menuBanks.Size = new System.Drawing.Size(168, 26);
            this.menuBanks.Text = "Банки";
            this.menuBanks.Click += new System.EventHandler(this.menuBanks_Click);

            this.menuRawMaterialKinds.Name = "menuRawMaterialKinds";
            this.menuRawMaterialKinds.Size = new System.Drawing.Size(168, 26);
            this.menuRawMaterialKinds.Text = "Виды сырья";
            this.menuRawMaterialKinds.Click += new System.EventHandler(this.menuRawMaterialKinds_Click);

            // AssetsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.gridAssets);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.menuStrip);
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "AssetsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Активы предприятия";

            ((System.ComponentModel.ISupportInitialize)(this.gridAssets)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

