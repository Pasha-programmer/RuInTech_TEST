using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Common.Extensions;
using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Contract.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuInTech_TEST.UI
{
    public partial class AssetsForm : Form
    {
        private readonly IAssetsInfoGetter _assetsInfoGetter;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Активы, отображаемые в гриде в текущий момент (индекс строки == индекс в списке).
        /// </summary>
        private IReadOnlyList<Asset> _assets = Array.Empty<Asset>();

        public AssetsForm(
            IAssetsInfoGetter assetsInfoGetter,
            IServiceProvider serviceProvider)
        {
            _assetsInfoGetter = assetsInfoGetter ?? throw new ArgumentNullException(nameof(assetsInfoGetter));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            InitializeComponent();
            InitializeGridColumns();

            Load += async (_, __) => await ReloadAssets();
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

        private async Task ReloadAssets()
        {
            _assets = (await _assetsInfoGetter.GetAssets(null)).ToList();

            var rows = _assets.Select(a => new AssetGridRow
            {
                Id = a.Id ?? 0,
                TypeName = a.AssetKind.GetDescription(),
                Name = a.Name,
                Summary = a.Summary,
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

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = _serviceProvider.GetRequiredService<AssetEditForm>())
            {
                editForm.Initialize(null);
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultAsset != null)
                {
                    var editor = GetEditor(editForm.ResultAsset);
                    if (editor == null)
                    {
                        return;
                    }

                    var result = await editor.AddAsset(editForm.ResultAsset);
                    if (result.HasValue)
                    {
                        await ReloadAssets();
                    }
                    else
                    {
                        MessageBox.Show(this, "Не удалось добавить актив.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            await EditSelectedAsset();
        }

        private async void gridAssets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            await EditSelectedAsset();
        }

        private async Task EditSelectedAsset()
        {
            var selected = GetSelectedAsset();
            if (selected == null)
            {
                MessageBox.Show(this, "Выберите актив для редактирования.", "Активы",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var editForm = _serviceProvider.GetRequiredService<AssetEditForm>())
            {
                editForm.Initialize(selected);
                if (editForm.ShowDialog(this) == DialogResult.OK && editForm.ResultAsset != null)
                {
                    var editor = GetEditor(editForm.ResultAsset);
                    if (editor != null)
                    {
                        var result = await editor.UpdateAsset(editForm.ResultAsset);
                        if (result)
                        {
                            await ReloadAssets();
                        }
                        else
                        {
                            MessageBox.Show(this, "Не удалось обновить актив.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
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
                var editor = GetEditor(selected);
                if (editor == null)
                {
                    return;
                }

                var result = await editor.DeleteAsset(selected.Id.Value);
                if (result)
                {
                    await ReloadAssets();
                }
                else
                {
                    MessageBox.Show(this, "Не удалось удалить актив.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await ReloadAssets();
        }

        /// <summary>
        /// Получить редактор для конкретного типа актива.
        /// </summary>
        private IAssetsInfoEditor GetEditor(Asset asset)
        {
            if (asset == null)
                return null;

            // Получаем редактор по типу актива
            try
            {
                var assetType = asset.GetType();
                var editorType = typeof(IAssetsInfoEditorGeneric<>).MakeGenericType(assetType);
                var editor = _serviceProvider.GetRequiredService(editorType);
                return editor as IAssetsInfoEditor;

                //switch (asset.AssetKind)
                //{
                //    case AssetKind.Cash:
                //        return (IAssetsInfoEditorGeneric<Asset>)_serviceProvider.GetRequiredService<IAssetsInfoEditorGeneric<CashAsset>>();
                //    case AssetKind.PaymentAccount:
                //        return _serviceProvider.GetRequiredService<IAssetsInfoEditorGeneric<PaymentAccount>>() as IAssetsInfoEditorGeneric<Asset>;
                //    case AssetKind.Coupon:
                //        return _serviceProvider.GetRequiredService<IAssetsInfoEditorGeneric<Сoupon>>() as IAssetsInfoEditorGeneric<Asset>;
                //    case AssetKind.RawMaterial:
                //        return _serviceProvider.GetRequiredService<IAssetsInfoEditorGeneric<RawMaterial>>() as IAssetsInfoEditorGeneric<Asset>;
                //    case AssetKind.Realty:
                //        return _serviceProvider.GetRequiredService<IAssetsInfoEditorGeneric<Realty>>() as IAssetsInfoEditorGeneric<Asset>;
                //    default:
                //        throw new NotSupportedException($"Тип актива {asset.AssetKind} не поддерживается.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не найден редактор для типа актива: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
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
