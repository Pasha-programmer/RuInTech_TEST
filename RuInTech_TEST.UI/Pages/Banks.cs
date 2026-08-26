using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Contract.Interfaces.Organization;
using RuInTech_TEST.Contract.Models.Organization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuInTech_TEST.UI.Pages
{
    public partial class BanksForm : Form
    {
        private readonly IBankInfoGetterService _bankInfoGetterService;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Активы, отображаемые в гриде в текущий момент (индекс строки == индекс в списке).
        /// </summary>
        private IReadOnlyList<Bank> _banks = Array.Empty<Bank>();

        public BanksForm(
            IBankInfoGetterService bankInfoGetterService,
            IServiceProvider serviceProvider)
        {
            _bankInfoGetterService = bankInfoGetterService ?? throw new ArgumentNullException(nameof(bankInfoGetterService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            InitializeComponent();
            InitializeGridColumns();

            Load += async (_, __) => await ReloadBanks();
        }

        private void InitializeGridColumns()
        {
            gridBanks.AutoGenerateColumns = false;
            gridBanks.Columns.Clear();

            gridBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(BankGridRow.Id),
                HeaderText = "№",
                DataPropertyName = nameof(BankGridRow.Id),
                FillWeight = 8,
            });
            gridBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(BankGridRow.Name),
                HeaderText = "Наименование",
                DataPropertyName = nameof(BankGridRow.Name),
                FillWeight = 24,
            });
        }

        private async Task ReloadBanks()
        {
            _banks = (await _bankInfoGetterService.GetBankFullInfo()).ToList();

            var rows = _banks.Select(a => new BankGridRow
            {
                Id = a.Id.Value,
                Name = a.Name,
            }).ToList();

            gridBanks.DataSource = rows;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = _serviceProvider.GetRequiredService<BankEditForm>())
            {
                editForm.Initialize();
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultAsset != null)
                {
                    var editor = _serviceProvider.GetRequiredService(typeof(IBankInfoEditorService)) as IBankInfoEditorService;

                    if (editor == null)
                    {
                        return;
                    }

                    var result = await editor.AddBank(editForm.ResultAsset);
                    if (result.HasValue)
                    {
                        await ReloadBanks();
                    }
                    else
                    {
                        MessageBox.Show(this, "Не удалось добавить актив.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedBank();
            if (selected == null)
            {
                MessageBox.Show(this, "Выберите банк для удаления.", "Активы",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Удалить банк \"{selected.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes && selected.Id.HasValue)
            {
                var editor = _serviceProvider.GetRequiredService(typeof(IBankInfoEditorService)) as IBankInfoEditorService;
                if (editor == null)
                {
                    return;
                }

                var result = await editor.DeleteBank(selected.Id.Value);
                if (result)
                {
                    await ReloadBanks();
                }
                else
                {
                    MessageBox.Show(this, "Не удалось удалить актив.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Bank GetSelectedBank()
        {
            if (gridBanks.CurrentRow == null)
            {
                return null;
            }

            var index = gridBanks.CurrentRow.Index;
            if (index < 0 || index >= _banks.Count)
            {
                return null;
            }

            return _banks[index];
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await ReloadBanks();
        }

        /// <summary>
        /// Строка отображения банка в гриде.
        /// </summary>
        private sealed class BankGridRow
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}
