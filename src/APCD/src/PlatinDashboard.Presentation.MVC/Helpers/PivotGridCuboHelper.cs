using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PlatinDashboard.Presentation.MVC.Helpers
{
    public class PivotGridCuboHelper
    {
        static PivotGridSettings exportPivotGridSettings;
        static PivotGridSettings dataAwarePivotGridSettings;

        public static PivotGridSettings ExportPivotGridSettings
        {
            get
            {
                if (exportPivotGridSettings == null)
                    exportPivotGridSettings = CreatePivotGridSettings();
                return exportPivotGridSettings;
            }
        }
        public static PivotGridSettings DataAwarePivotGridSettings
        {
            get
            {
                if (dataAwarePivotGridSettings == null)
                    dataAwarePivotGridSettings = CreateDataAwarePivotGridSettings();
                return dataAwarePivotGridSettings;
            }
        }

        static PivotGridSettings CreatePivotGridSettingsBase()
        {
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = "pivotGrid";
            settings.CallbackRouteValues = new { Controller = "Cubo", Action = "ExportPartial" };
            settings.Width = Unit.Percentage(100);
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto;
            settings.OptionsData.DataProcessingEngine = PivotDataProcessingEngine.LegacyOptimized;
            return settings;
        }
        static PivotGridSettings CreatePivotGridSettings()
        {
            PivotGridSettings settings = CreatePivotGridSettingsBase();

            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Data";
                field.Caption = "Data";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Loja";
                field.Caption = "Loja";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Bruto";
                field.Caption = "Bruto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Desconto";
                field.Caption = "Desconto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Devolucao";
                field.Caption = "Devolução";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Liquida";
                field.Caption = "Líquido";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.DataArea;
                field.FieldName = "Lucro";
                field.Caption = "Lucro";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "PercentualMargem";
                field.Caption = "Margem";
                field.Visible = false;
                field.ValueFormat.FormatString = "{0} %";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TicketMedio";
                field.Caption = "Ticket Médio";
                field.ValueFormat.FormatString = "0.##";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "QtMediaClientes";
                field.Caption = "Quantidade Média por Cliente";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "ClientesAtendidos";
                field.Caption = "Clientes Atendidos";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TotalLojas";
                field.Caption = "Total Lojas";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Custo";
                field.Caption = "Custo";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
                field.Visible = false;
            });

            return settings;
        }
        static PivotGridSettings CreateDataAwarePivotGridSettings()
        {
            PivotGridSettings settings = CreatePivotGridSettingsBase();

            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Data";
                field.Caption = "Data";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Loja";
                field.Caption = "Loja";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Bruto";
                field.Caption = "Bruto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Desconto";
                field.Caption = "Desconto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Devolucao";
                field.Caption = "Devolução";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Liquida";
                field.Caption = "Líquido";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.DataArea;
                field.FieldName = "Lucro";
                field.Caption = "Lucro";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "PercentualMargem";
                field.Caption = "Margem";
                field.Visible = false;
                field.ValueFormat.FormatString = "{0} %";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TicketMedio";
                field.Caption = "Ticket Médio";
                field.ValueFormat.FormatString = "0.##";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "QtMediaClientes";
                field.Caption = "Quantidade Média por Cliente";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "ClientesAtendidos";
                field.Caption = "Clientes Atendidos";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TotalLojas";
                field.Caption = "Total Lojas";
            });
            settings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Custo";
                field.Caption = "Custo";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
                field.Visible = false;
            });

            return settings;
        }

        public delegate ActionResult PivotGridExportMethod(PivotGridSettings settings, object dataObject);
        public delegate ActionResult PivotGridDataAwareExportMethod(PivotGridSettings settings, object dataObject, XlsxExportOptions exportOptions);
        public class PivotGridExportType
        {
            public string Title { get; set; }
            public PivotGridExportMethod Method { get; set; }
            public PivotGridDataAwareExportMethod ExcelMethod { get; set; }
        }

        static PivotGridSettings CreatePivotChartIntegrationSettings()
        {
            PivotGridSettings pivotGridSettings = new PivotGridSettings();
            pivotGridSettings.Name = "pivotGrid";
            pivotGridSettings.CallbackRouteValues = new { Controller = "Cubo", Action = "PivotGrid" };
            pivotGridSettings.OptionsChartDataSource.DataProvideMode = PivotChartDataProvideMode.UseCustomSettings;
            pivotGridSettings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto;
            pivotGridSettings.OptionsData.DataProcessingEngine = PivotDataProcessingEngine.LegacyOptimized;
            pivotGridSettings.Width = Unit.Percentage(100);
            pivotGridSettings.Theme = "Material";
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Data";
                field.Caption = "Data";
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Loja";
                field.Caption = "Loja";
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Bruto";
                field.Caption = "Bruto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Desconto";
                field.Caption = "Desconto";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Devolucao";
                field.Caption = "Devolução";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.RowArea;
                field.FieldName = "Liquida";
                field.Caption = "Líquido";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.DataArea;
                field.FieldName = "Lucro";
                field.Caption = "Lucro";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "PercentualMargem";
                field.Caption = "Margem";
                field.Visible = false;
                field.ValueFormat.FormatString = "{0} %";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TicketMedio";
                field.Caption = "Ticket Médio";
                field.ValueFormat.FormatString = "0.##";
                field.ValueFormat.FormatType = FormatType.Numeric;
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "QtMediaClientes";
                field.Caption = "Quantidade Média por Cliente";
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "ClientesAtendidos";
                field.Caption = "Clientes Atendidos";
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "TotalLojas";
                field.Caption = "Total Lojas";
            });
            pivotGridSettings.Fields.Add(field =>
            {
                field.Area = PivotArea.FilterArea;
                field.FieldName = "Custo";
                field.Caption = "Custo";
                field.ValueFormat.FormatString = "c2";
                field.ValueFormat.FormatType = FormatType.Numeric;
                field.Visible = false;
            });
            //pivotGridSettings.PreRender = (sender, e) => {
            //    int selectNumber = 4;
            //    var field = ((MVCxPivotGrid)sender).Fields["Category Name"];
            //    object[] values = field.GetUniqueValues();
            //    List<object> includedValues = new List<object>(values.Length / selectNumber);
            //    for (int i = 0; i < values.Length; i++)
            //    {
            //        if (i % selectNumber == 0)
            //            includedValues.Add(values[i]);
            //    }
            //    field.FilterValues.ValuesIncluded = includedValues.ToArray();
            //};
            pivotGridSettings.ClientSideEvents.EndCallback = "UpdateChart";
            return pivotGridSettings;
        }
        public static IEnumerable<DevExpress.XtraCharts.ViewType> GetSupportedChartTypes()
        {
            IEnumerable<DevExpress.XtraCharts.ViewType> restictedChartTypes = new List<DevExpress.XtraCharts.ViewType> {
                DevExpress.XtraCharts.ViewType.PolarArea,
                DevExpress.XtraCharts.ViewType.PolarRangeArea,
                DevExpress.XtraCharts.ViewType.PolarLine,
                DevExpress.XtraCharts.ViewType.ScatterPolarLine,
                DevExpress.XtraCharts.ViewType.SideBySideGantt,
                DevExpress.XtraCharts.ViewType.SideBySideRangeBar,
                DevExpress.XtraCharts.ViewType.RangeBar,
                DevExpress.XtraCharts.ViewType.Gantt,
                DevExpress.XtraCharts.ViewType.PolarPoint,
                DevExpress.XtraCharts.ViewType.Stock,
                DevExpress.XtraCharts.ViewType.CandleStick,
                DevExpress.XtraCharts.ViewType.Bubble
            };
            return Enum.GetValues(typeof(DevExpress.XtraCharts.ViewType)).Cast<DevExpress.XtraCharts.ViewType>().Except(restictedChartTypes).ToList();
        }
    }
}