using RuInTech_TEST.Contract.Models.Organization;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RuInTech_TEST.UI
{
    /// <summary>
    /// Форма добавления / редактирования актива.
    /// Набор видимых полей зависит от выбранного типа актива.
    /// </summary>
    internal class BankEditForm : Form
    {
        private const int ContentWidth = 580;
        private const int LabelColumnWidth = 210;
        private const int RowHeight = 32;

        private readonly TextBox _txtName = new TextBox { Width = 260 };

        private readonly Button _btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Width = 100, Height = 32 };
        private readonly Button _btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Width = 100, Height = 32 };

        private readonly Panel _scrollPanel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
        };

        private readonly FlowLayoutPanel _contentPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Padding = new Padding(10),
        };

        public Bank ResultAsset { get; private set; }

        public void Initialize()
        {
            BuildLayout();
            WireEvents();

            InitializeForCreate();
        }

        private void InitializeForCreate()
        {
            Text = "Новый банк";
        }

        private void BuildLayout()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = true;
            ShowInTaskbar = false;
            ClientSize = new Size(ContentWidth + 40, 640);
            MinimumSize = new Size(ContentWidth + 80, 420);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 9F);

            // --- корневой layout: прокручиваемая область сверху, кнопки снизу ---
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
            };
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));

            _contentPanel.Controls.Add(LabeledRow("Наименование актива:", _txtName));

            _scrollPanel.Controls.Add(_contentPanel);

            var buttonsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
            };
            buttonsPanel.Controls.Add(_btnCancel);
            buttonsPanel.Controls.Add(_btnOk);

            root.Controls.Add(_scrollPanel, 0, 0);
            root.Controls.Add(buttonsPanel, 0, 1);

            Controls.Add(root);

            AcceptButton = _btnOk;
            CancelButton = _btnCancel;
        }

        private static Control LabeledRow(string labelText, Control input, Control secondInput = null)
        {
            var row = new TableLayoutPanel
            {
                ColumnCount = secondInput == null ? 2 : 3,
                RowCount = 1,
                AutoSize = false,
                Margin = new Padding(0, 4, 0, 4),
                Width = ContentWidth - 60,
                Height = RowHeight,
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, LabelColumnWidth));
            row.ColumnStyles.Add(secondInput == null
                ? new ColumnStyle(SizeType.Percent, 100F)
                : new ColumnStyle(SizeType.AutoSize));
            if (secondInput != null)
            {
                row.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            var label = new Label
            {
                Text = labelText,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            input.Anchor = AnchorStyles.Left;
            input.Margin = new Padding(0, 2, 6, 2);

            row.Controls.Add(label, 0, 0);
            row.Controls.Add(input, 1, 0);

            if (secondInput != null)
            {
                secondInput.Anchor = AnchorStyles.Left;
                secondInput.Margin = new Padding(0, 2, 0, 2);
                row.Controls.Add(secondInput, 2, 0);
            }

            return row;
        }

        private void WireEvents()
        {
            _btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show(this, "Укажите наименование банка.", "Проверка данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var name = _txtName.Text.Trim();

            ResultAsset = new Bank
            {
                Name = name,
            };

            DialogResult = DialogResult.OK;
        }
    }
}
