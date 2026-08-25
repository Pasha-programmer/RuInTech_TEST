using RuInTech_TEST.Common.Extensions;
using RuInTech_TEST.Contract.Models;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Contract.Models.RawMaterial;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RuInTech_TEST.UI
{
    /// <summary>
    /// Форма добавления / редактирования актива.
    /// Набор видимых полей зависит от выбранного типа актива.
    /// </summary>
    internal class AssetEditForm : Form
    {
        private Asset _existingAsset;

        private const int ContentWidth = 580;
        private const int LabelColumnWidth = 210;
        private const int RowHeight = 32;

        // Общие поля
        private readonly TextBox _txtName = new TextBox { Width = 260 };
        private readonly ComboBox _cmbType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 260 };

        // Денежный актив - общие поля (сумма/валюта)
        private readonly GroupBox _grpMonetary = new GroupBox { Text = "Денежная сумма" };
        private readonly NumericUpDown _numCost = CreateMoneyNumericUpDown();
        private readonly ComboBox _cmbCurrency = CreateCurrencyComboBox();

        // Платёжный счёт
        private readonly GroupBox _grpPaymentAccount = new GroupBox { Text = "Банковский счёт" };
        private readonly TextBox _txtBankName = new TextBox { Width = 260 };
        private readonly TextBox _txtAccountNumber = new TextBox { Width = 260 };
        // Талон/купон
        private readonly GroupBox _grpCoupon = new GroupBox { Text = "Талон / купон" };
        private readonly TextBox _txtCouponType = new TextBox { Width = 260 };

        // Неденежный актив - общие поля (три вида стоимости)
        private readonly GroupBox _grpNonMonetary = new GroupBox { Text = "Стоимость" };
        private readonly NumericUpDown _numInitialCost = CreateMoneyNumericUpDown();
        private readonly ComboBox _cmbInitialCurrency = CreateCurrencyComboBox();
        private readonly NumericUpDown _numResidualCost = CreateMoneyNumericUpDown();
        private readonly ComboBox _cmbResidualCurrency = CreateCurrencyComboBox();
        private readonly NumericUpDown _numEstimatedCost = CreateMoneyNumericUpDown();
        private readonly ComboBox _cmbEstimatedCurrency = CreateCurrencyComboBox();

        // Недвижимость
        private readonly GroupBox _grpRealty = new GroupBox { Text = "Недвижимость" };
        private readonly TextBox _txtInventoryNumber = new TextBox { Width = 260 };
        private readonly TextBox _txtRealtyAdditionalInfo = new TextBox { Width = 260 };

        // Сырьё / материалы
        private readonly GroupBox _grpRawMaterial = new GroupBox { Text = "Сырьё / материалы" };
        private readonly TextBox _txtRawType = new TextBox { Width = 260 };
        private readonly ComboBox _txtUnitOfMeasure = CreateUnitOfMeasureComboBox();
        private readonly NumericUpDown _numQuantity = new NumericUpDown
        {
            DecimalPlaces = 3,
            Maximum = 1_000_000_000,
            Minimum = 0,
            Width = 120,
        };
        private readonly CheckBox _chkHasProductionDate = new CheckBox { Text = "Дата производства указана", AutoSize = true };
        private readonly DateTimePicker _dtpProductionDate = new DateTimePicker { Width = 260, Enabled = false };
        private readonly TextBox _txtRawAdditionalInfo = new TextBox { Width = 260 };

        private readonly Button _btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Width = 100, Height = 32 };
        private readonly Button _btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Width = 100, Height = 32 };

        // Прокручиваемая область со всеми полями формы (на случай, если все группы не влезают по высоте).
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

        /// <summary>
        /// Результат редактирования - актив с заполненными полями (используется если DialogResult == OK).
        /// </summary>
        public Asset ResultAsset { get; private set; }

        public void Initialize(Asset asset)
        {
            BuildLayout();
            WireEvents();

            if (asset != null)
            {
                InitializeForEdit(asset);
            }
            else
            {
                InitializeForCreate();
            }

            UpdateVisiblePanels();
        }

        private void InitializeForEdit(Asset asset)
        {
            _existingAsset = asset ?? throw new ArgumentNullException(nameof(asset));
            Text = "Редактирование актива";
            PopulateFromExistingAsset(_existingAsset);
            _cmbType.Enabled = false; // тип существующего актива не меняется
        }

        private void InitializeForCreate()
        {
            _existingAsset = null;
            Text = "Новый актив";
            _cmbType.SelectedIndex = (int)AssetKind.Cash;
        }

        private static NumericUpDown CreateMoneyNumericUpDown() => new NumericUpDown
        {
            DecimalPlaces = 2,
            Maximum = 1_000_000_000,
            Minimum = 0,
            Width = 140,
            ThousandsSeparator = true,
        };

        private static ComboBox CreateCurrencyComboBox()
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 90 };
            combo.DataSource = Enum.GetValues(typeof(CurrencyType)).Cast<CurrencyType>().Select(ct => ct.GetDescription()).ToList();
            combo.SelectedValue = CurrencyType.RUB;
            return combo;
        }

        private static ComboBox CreateUnitOfMeasureComboBox()
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 90 };
            combo.DataSource = Enum.GetValues(typeof(UnitOfMeasure)).Cast<UnitOfMeasure>().Select(ct => ct.GetDescription()).ToList();
            combo.SelectedValue = UnitOfMeasure.Kilogram;
            return combo;
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

            _cmbType.Items.AddRange(
                Enum.GetValues(typeof(AssetKind)).Cast<AssetKind>().Select(ct => ct.GetDescription()).ToArray()
            );

            _contentPanel.Controls.Add(LabeledRow("Наименование актива:", _txtName));
            _contentPanel.Controls.Add(LabeledRow("Тип актива:", _cmbType));

            // --- Денежный актив (общее) ---
            var monetaryFlow = CreateGroupContent();
            monetaryFlow.Controls.Add(LabeledRow("Сумма:", _numCost, _cmbCurrency));
            _grpMonetary.Controls.Add(monetaryFlow);
            SetGroupSize(_grpMonetary, monetaryFlow, rowCount: 1);
            _contentPanel.Controls.Add(_grpMonetary);

            // --- Банковский счёт ---
            var payFlow = CreateGroupContent();
            payFlow.Controls.Add(LabeledRow("Банк:", _txtBankName));
            payFlow.Controls.Add(LabeledRow("Номер счёта:", _txtAccountNumber));
            _grpPaymentAccount.Controls.Add(payFlow);
            SetGroupSize(_grpPaymentAccount, payFlow, rowCount: 2);
            _contentPanel.Controls.Add(_grpPaymentAccount);

            // --- Талон / купон ---
            var couponFlow = CreateGroupContent();
            couponFlow.Controls.Add(LabeledRow("Вид талона:", _txtCouponType));
            _grpCoupon.Controls.Add(couponFlow);
            SetGroupSize(_grpCoupon, couponFlow, rowCount: 1);
            _contentPanel.Controls.Add(_grpCoupon);

            // --- Неденежный актив (общее) ---
            var nonMonetaryFlow = CreateGroupContent();
            nonMonetaryFlow.Controls.Add(LabeledRow("Начальная балансовая стоимость:", _numInitialCost, _cmbInitialCurrency));
            nonMonetaryFlow.Controls.Add(LabeledRow("Остаточная балансовая стоимость:", _numResidualCost, _cmbResidualCurrency));
            nonMonetaryFlow.Controls.Add(LabeledRow("Оценочная стоимость:", _numEstimatedCost, _cmbEstimatedCurrency));
            _grpNonMonetary.Controls.Add(nonMonetaryFlow);
            SetGroupSize(_grpNonMonetary, nonMonetaryFlow, rowCount: 3);
            _contentPanel.Controls.Add(_grpNonMonetary);

            // --- Недвижимость ---
            var realtyFlow = CreateGroupContent();
            realtyFlow.Controls.Add(LabeledRow("Инвентарный номер:", _txtInventoryNumber));
            realtyFlow.Controls.Add(LabeledRow("Доп. сведения (адрес, год постройки):", _txtRealtyAdditionalInfo));
            _grpRealty.Controls.Add(realtyFlow);
            SetGroupSize(_grpRealty, realtyFlow, rowCount: 2);
            _contentPanel.Controls.Add(_grpRealty);

            // --- Сырьё / материалы ---
            var rawFlow = CreateGroupContent();
            rawFlow.Controls.Add(LabeledRow("Вид сырья:", _txtRawType));
            rawFlow.Controls.Add(LabeledRow("Единица измерения:", _txtUnitOfMeasure));
            rawFlow.Controls.Add(LabeledRow("Количество:", _numQuantity));
            rawFlow.Controls.Add(_chkHasProductionDate);
            rawFlow.Controls.Add(LabeledRow("Дата производства:", _dtpProductionDate));
            rawFlow.Controls.Add(LabeledRow("Доп. сведения:", _txtRawAdditionalInfo));
            _grpRawMaterial.Controls.Add(rawFlow);
            SetGroupSize(_grpRawMaterial, rawFlow, rowCount: 6);
            _contentPanel.Controls.Add(_grpRawMaterial);

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

        /// <summary>
        /// Создать внутреннюю панель группы с отступом под заголовок GroupBox.
        /// Размер выставляется явно (см. <see cref="SetGroupSize"/>), чтобы избежать
        /// непредсказуемого поведения AutoSize у вложенных панелей.
        /// </summary>
        private static FlowLayoutPanel CreateGroupContent() => new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = false,
            Padding = new Padding(10, 22, 10, 8),
            Location = new Point(0, 0),
        };

        /// <summary>
        /// Высота одной строки формы (подпись + поле) вместе с отступами.
        /// </summary>
        private const int RowUnitHeight = 42;

        /// <summary>
        /// Выставить фиксированный размер GroupBox и его содержимого по числу строк.
        /// Не полагаемся на AutoSize: во вложенных FlowLayoutPanel/GroupBox он часто
        /// даёт нулевую или заниженную высоту, из-за чего часть полей обрезается.
        /// </summary>
        private void SetGroupSize(GroupBox group, FlowLayoutPanel content, int rowCount)
        {
            group.AutoSize = false;
            group.Width = ContentWidth - 30;

            content.Width = group.Width - 20;
            content.Height = rowCount * RowUnitHeight + 10;

            group.Height = content.Height + 30;
        }

        /// <summary>
        /// Строка "подпись + одно или два поля ввода" с выровненными колонками.
        /// </summary>
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
            _cmbType.SelectedIndexChanged += (_, __) => UpdateVisiblePanels();
            _chkHasProductionDate.CheckedChanged += (_, __) => _dtpProductionDate.Enabled = _chkHasProductionDate.Checked;
            _btnOk.Click += BtnOk_Click;
        }

        private void UpdateVisiblePanels()
        {
            var kind = GetSelectedKindByIndex(_cmbType.SelectedIndex);

            SuspendLayout();

            _scrollPanel.SuspendLayout();
            _contentPanel.SuspendLayout();

            _grpMonetary.Visible = new[] { AssetKind.Cash, AssetKind.PaymentAccount, AssetKind.Coupon }.Contains(kind);
            _grpPaymentAccount.Visible = kind == AssetKind.PaymentAccount;
            _grpCoupon.Visible = kind == AssetKind.Coupon;

            _grpNonMonetary.Visible = new[] { AssetKind.Realty, AssetKind.RawMaterial }.Contains(kind);
            _grpRealty.Visible = kind == AssetKind.Realty;
            _grpRawMaterial.Visible = kind == AssetKind.RawMaterial;

            _contentPanel.ResumeLayout(true);
            _scrollPanel.ResumeLayout(true);

            ResumeLayout(true);
        }

        private void PopulateFromExistingAsset(Asset asset)
        {
            _txtName.Text = asset.Name;

            switch (asset)
            {
                case PaymentAccount pa:
                    _cmbType.SelectedIndex = GetSelectedIndexByKind(AssetKind.PaymentAccount);
                    _numCost.Value = pa.MonetaryValue.Cost;
                    _cmbCurrency.SelectedItem = pa.MonetaryValue.Currency;
                    _txtBankName.Text = pa.BankAccount.Bank.Name;
                    _txtAccountNumber.Text = pa.BankAccount.PersonalAccount;
                    break;

                case Сoupon coupon:
                    _cmbType.SelectedIndex = GetSelectedIndexByKind(AssetKind.Coupon);
                    _numCost.Value = coupon.MonetaryValue.Cost;
                    _cmbCurrency.SelectedItem = coupon.MonetaryValue.Currency;
                    _txtCouponType.Text = coupon.Type;
                    break;

                case CashAsset cash:
                    _cmbType.SelectedIndex = GetSelectedIndexByKind(AssetKind.Cash);
                    _numCost.Value = cash.MonetaryValue.Cost;
                    _cmbCurrency.SelectedItem = cash.MonetaryValue.Currency;
                    break;

                case Realty realty:
                    _cmbType.SelectedIndex = GetSelectedIndexByKind(AssetKind.Realty);
                    _numInitialCost.Value = realty.InitialBalanceCost.Cost;
                    _cmbInitialCurrency.SelectedItem = realty.InitialBalanceCost.Currency;
                    _numResidualCost.Value = realty.ResidualBalanceCost.Cost;
                    _cmbResidualCurrency.SelectedItem = realty.ResidualBalanceCost.Currency;
                    _numEstimatedCost.Value = realty.EstimatedCost.Cost;
                    _cmbEstimatedCurrency.SelectedItem = realty.EstimatedCost.Currency;
                    _txtInventoryNumber.Text = realty.InventoryNumber;
                    _txtRealtyAdditionalInfo.Text = realty.AdditionalInfo;
                    break;

                case RawMaterial raw:
                    _cmbType.SelectedIndex = GetSelectedIndexByKind(AssetKind.RawMaterial);
                    _numInitialCost.Value = raw.InitialBalanceCost.Cost;
                    _cmbInitialCurrency.SelectedItem = raw.InitialBalanceCost.Currency;
                    _numResidualCost.Value = raw.ResidualBalanceCost.Cost;
                    _cmbResidualCurrency.SelectedItem = raw.ResidualBalanceCost.Currency;
                    _numEstimatedCost.Value = raw.EstimatedCost.Cost;
                    _cmbEstimatedCurrency.SelectedItem = raw.EstimatedCost.Currency;
                    _txtRawType.Text = raw.RawMaterialKind.Name;
                    _txtUnitOfMeasure.SelectedItem = raw.UnitOfMeasure;
                    _numQuantity.Value = (decimal)raw.Quantity;
                    _chkHasProductionDate.Checked = raw.ProductionDate.HasValue;
                    _dtpProductionDate.Enabled = raw.ProductionDate.HasValue;
                    if (raw.ProductionDate.HasValue)
                    {
                        _dtpProductionDate.Value = raw.ProductionDate.Value.DateTime;
                    }
                    _txtRawAdditionalInfo.Text = raw.AdditionalInfo ?? string.Empty;
                    break;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show(this, "Укажите наименование актива.", "Проверка данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var id = _existingAsset?.Id;
            var name = _txtName.Text.Trim();

            Asset asset;
            switch (GetSelectedKindByIndex(_cmbType.SelectedIndex))
            {
                case AssetKind.Cash:
                    asset = new CashAsset
                    {
                        Id = id,
                        Name = name,
                        MonetaryValue = new MonetaryValue(_numCost.Value, GetSelectedCurrencyTypeByIndex(_cmbCurrency.SelectedIndex)),
                    };
                    break;

                case AssetKind.PaymentAccount:
                    asset = new PaymentAccount
                    {
                        Id = id,
                        Name = name,
                        MonetaryValue = new MonetaryValue(_numCost.Value, GetSelectedCurrencyTypeByIndex(_cmbCurrency.SelectedIndex)),
                        BankAccount = new BankAccount
                        {
                            PersonalAccount = _txtAccountNumber.Text.Trim(),
                            Bank = new Bank
                            {
                                Id = (_existingAsset as PaymentAccount)?.BankAccount.Bank.Id ?? 0,
                                Name = _txtBankName.Text.Trim(),
                            },
                        },
                    };
                    break;

                case AssetKind.Coupon:
                    asset = new Сoupon
                    {
                        Id = id,
                        Name = name,
                        MonetaryValue = new MonetaryValue(_numCost.Value, GetSelectedCurrencyTypeByIndex(_cmbCurrency.SelectedIndex)),
                        Type = _txtCouponType.Text.Trim(),
                    };
                    break;

                case AssetKind.Realty:
                    asset = new Realty
                    {
                        Id = id,
                        Name = name,
                        InitialBalanceCost = new MonetaryValue(_numInitialCost.Value, GetSelectedCurrencyTypeByIndex(_cmbInitialCurrency.SelectedIndex)),
                        ResidualBalanceCost = new MonetaryValue(_numResidualCost.Value, GetSelectedCurrencyTypeByIndex(_cmbResidualCurrency.SelectedIndex)),
                        EstimatedCost = new MonetaryValue(_numEstimatedCost.Value, GetSelectedCurrencyTypeByIndex(_cmbEstimatedCurrency.SelectedIndex)),
                        InventoryNumber = _txtInventoryNumber.Text.Trim(),
                        AdditionalInfo = _txtRealtyAdditionalInfo.Text.Trim(),
                    };
                    break;

                case AssetKind.RawMaterial:
                    asset = new RawMaterial
                    {
                        Id = id,
                        Name = name,
                        InitialBalanceCost = new MonetaryValue(_numInitialCost.Value, GetSelectedCurrencyTypeByIndex(_cmbInitialCurrency.SelectedIndex)),
                        ResidualBalanceCost = new MonetaryValue(_numResidualCost.Value, GetSelectedCurrencyTypeByIndex(_cmbResidualCurrency.SelectedIndex)),
                        EstimatedCost = new MonetaryValue(_numEstimatedCost.Value, GetSelectedCurrencyTypeByIndex(_cmbEstimatedCurrency.SelectedIndex)),
                        RawMaterialKind = new RawMaterialKind
                        {
                            Name = _txtRawType.Text.Trim(),
                        },
                        UnitOfMeasure = GetSelectedUnitOfMeasureByIndex(_txtUnitOfMeasure.SelectedIndex),
                        Quantity = (double)_numQuantity.Value,
                        ProductionDate = _chkHasProductionDate.Checked ? new DateTimeOffset(_dtpProductionDate.Value) : (DateTimeOffset?)null,
                        AdditionalInfo = string.IsNullOrWhiteSpace(_txtRawAdditionalInfo.Text) ? null : _txtRawAdditionalInfo.Text.Trim(),
                    };
                    break;

                default:
                    throw new InvalidOperationException("Неизвестный тип актива.");
            }

            ResultAsset = asset;
            DialogResult = DialogResult.OK;
        }


        private AssetKind GetSelectedKindByIndex(int index) => (AssetKind)(index + 1);

        private int GetSelectedIndexByKind(AssetKind assetKind) => (int)assetKind - 1;

        private CurrencyType GetSelectedCurrencyTypeByIndex(int index) => (CurrencyType)(index + 1);

        private UnitOfMeasure GetSelectedUnitOfMeasureByIndex(int index) => (UnitOfMeasure)(index + 1);
    }
}
