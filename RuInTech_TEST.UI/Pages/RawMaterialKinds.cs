using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Contract.Interfaces.RawMaterialKinds;
using RuInTech_TEST.Contract.Models.RawMaterial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuInTech_TEST.UI.Pages
{
    public partial class RawMaterialKindsForm : Form
    {
        private readonly IRawMaterialKindGetterService _rawMaterialKindGetterService;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Активы, отображаемые в гриде в текущий момент (индекс строки == индекс в списке).
        /// </summary>
        private IReadOnlyList<RawMaterialKind> _rawMaterialKinds = Array.Empty<RawMaterialKind>();

        public RawMaterialKindsForm(
            IRawMaterialKindGetterService bankInfoGetterService,
            IServiceProvider serviceProvider)
        {
            _rawMaterialKindGetterService = bankInfoGetterService ?? throw new ArgumentNullException(nameof(bankInfoGetterService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            InitializeComponent();
            InitializeGridColumns();

            Load += async (_, __) => await ReloadRawMaterialKinds();
        }

        private void InitializeGridColumns()
        {
            gridRawMaterialKinds.AutoGenerateColumns = false;
            gridRawMaterialKinds.Columns.Clear();

            gridRawMaterialKinds.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(RawMaterialKindGridRow.Id),
                HeaderText = "№",
                DataPropertyName = nameof(RawMaterialKindGridRow.Id),
                FillWeight = 8,
            });
            gridRawMaterialKinds.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(RawMaterialKindGridRow.Name),
                HeaderText = "Наименование",
                DataPropertyName = nameof(RawMaterialKindGridRow.Name),
                FillWeight = 24,
            });
            gridRawMaterialKinds.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(RawMaterialKindGridRow.Description),
                HeaderText = "Описание",
                DataPropertyName = nameof(RawMaterialKindGridRow.Description),
                FillWeight = 24,
            });
        }

        private async Task ReloadRawMaterialKinds()
        {
            _rawMaterialKinds = (await _rawMaterialKindGetterService.GetRawMaterialKinds()).ToList();

            var rows = _rawMaterialKinds.Select(a => new RawMaterialKindGridRow
            {
                Id = a.Id.Value,
                Name = a.Name,
                Description = a.Description,
            }).ToList();

            gridRawMaterialKinds.DataSource = rows;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = _serviceProvider.GetRequiredService<RawMaterialKindEditForm>())
            {
                editForm.Initialize();
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultRawMaterialKind != null)
                {
                    var editor = _serviceProvider.GetRequiredService(typeof(IRawMaterialKindEditorService)) as IRawMaterialKindEditorService;

                    if (editor == null)
                    {
                        return;
                    }

                    var result = await editor.AddRawMaterialKind(editForm.ResultRawMaterialKind);
                    if (result.HasValue)
                    {
                        await ReloadRawMaterialKinds();
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
            var selected = GetSelectedRawMaterialKind();
            if (selected == null)
            {
                MessageBox.Show(this, "Выберите сырье для удаления.", "Активы",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Удалить сырье \"{selected.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes && selected.Id.HasValue)
            {
                var editor = _serviceProvider.GetRequiredService(typeof(IRawMaterialKindEditorService)) as IRawMaterialKindEditorService;
                if (editor == null)
                {
                    return;
                }

                var result = await editor.DeleteRawMaterialKind(selected.Id.Value);
                if (result)
                {
                    await ReloadRawMaterialKinds();
                }
                else
                {
                    MessageBox.Show(this, "Не удалось удалить сырье.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private RawMaterialKind GetSelectedRawMaterialKind()
        {
            if (gridRawMaterialKinds.CurrentRow == null)
            {
                return null;
            }

            var index = gridRawMaterialKinds.CurrentRow.Index;
            if (index < 0 || index >= _rawMaterialKinds.Count)
            {
                return null;
            }

            return _rawMaterialKinds[index];
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await ReloadRawMaterialKinds();
        }

        /// <summary>
        /// Строка отображения банка в гриде.
        /// </summary>
        private sealed class RawMaterialKindGridRow
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }
    }
}
