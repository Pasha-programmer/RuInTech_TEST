using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RuInTech_TEST
{
    public partial class AssetsForm : Form
    {
        private readonly IAssetsInfoGetter _assetsInfoGetter;
        private readonly IAssetsInfoEditor _assetsInfoEditor;

        /// <summary>
        /// Активы, отображаемые в гриде в текущий момент (индекс строки == индекс в списке).
        /// </summary>
        private IReadOnlyList<Asset> _assets = Array.Empty<Asset>();

        public AssetsForm(IAssetsInfoGetter assetsInfoGetter, IAssetsInfoEditor assetsInfoEditor)
        {
            _assetsInfoGetter = assetsInfoGetter ?? throw new ArgumentNullException(nameof(assetsInfoGetter));
            _assetsInfoEditor = assetsInfoEditor ?? throw new ArgumentNullException(nameof(assetsInfoEditor));

            InitializeComponent();
            InitializeGridColumns();

            Load += (_, __) => ReloadAssets();
        }

        private void InitializeGridColumns()
        {
            gridAssets.AutoGenerateColumns = false;
            gridAssets.Columns.Clear();

            gridAssets.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(AssetGridRow.Id),
                HeaderText = "№",
                DataPropertyName = nameof(AssetGridRow.Id),
                FillWeight = 8,
            });
            gridAssets.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(AssetGridRow.TypeName),
                HeaderText = "Тип",
                DataPropertyName = nameof(AssetGridRow.TypeName),
                FillWeight = 18,
            });
            gridAssets.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(AssetGridRow.Name),
                HeaderText = "Наименование",
                DataPropertyName = nameof(AssetGridRow.Name),
                FillWeight = 24,
            });
            gridAssets.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(AssetGridRow.Summary),
                HeaderText = "Сведения",
                DataPropertyName = nameof(AssetGridRow.Summary),
                FillWeight = 50,
            });
        }

        private void ReloadAssets()
        {
            _assets = _assetsInfoGetter.GetAssets();

            var rows = _assets.Select(a => new AssetGridRow
            {
                Id = a.Id ?? 0,
                TypeName = AssetPresenter.GetTypeName(a),
                Name = a.Name,
                Summary = AssetPresenter.GetSummary(a),
            }).ToList();

            gridAssets.DataSource = rows;
        }

        private Asset GetSelectedAsset()
        {
            if (gridAssets.CurrentRow == null)
            {
                return null;
            }

            var index = gridAssets.CurrentRow.Index;
            if (index < 0 || index >= _assets.Count)
            {
                return null;
            }

            return _assets[index];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new AssetEditForm(existingAsset: null))
            {
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultAsset != null)
                {
                    _assetsInfoEditor.AddAsset(editForm.ResultAsset);
                    ReloadAssets();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedAsset();
        }

        private void gridAssets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            EditSelectedAsset();
        }

        private void EditSelectedAsset()
        {
            var selected = GetSelectedAsset();
            if (selected == null)
            {
                MessageBox.Show(this, "Выберите актив для редактирования.", "Активы",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var editForm = new AssetEditForm(existingAsset: selected))
            {
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultAsset != null)
                {
                    _assetsInfoEditor.UpdateAsset(editForm.ResultAsset);
                    ReloadAssets();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAsset();
            if (selected == null)
            {
                MessageBox.Show(this, "Выберите актив для удаления.", "Активы",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Удалить актив \"{selected.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes && selected.Id.HasValue)
            {
                _assetsInfoEditor.DeleteAsset(selected.Id.Value);
                ReloadAssets();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadAssets();
        }

        /// <summary>
        /// Строка отображения актива в гриде.
        /// </summary>
        private sealed class AssetGridRow
        {
            public long Id { get; set; }
            public string TypeName { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Summary { get; set; } = string.Empty;
        }
    }
}
