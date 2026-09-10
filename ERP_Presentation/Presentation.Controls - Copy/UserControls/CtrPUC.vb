Imports System.ComponentModel

Public Class CtrPUC
    Inherits DevExpress.XtraEditors.SearchLookUpEdit
    Implements ISupportInitialize

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.INDGcId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcName = New DevExpress.XtraGrid.Columns.GridColumn()
        INDGcHandlesThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        INDGcHandlesCostCenter = New DevExpress.XtraGrid.Columns.GridColumn()
    End Sub

    Friend WithEvents INDGcId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcHandlesThirdParty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcHandlesCostCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrPUCView As DevExpress.XtraGrid.Views.Grid.GridView

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit

        Me.CtrPUCView = Me.Properties.View
        'Me.Properties.NullText = String.Empty
        Me.Properties.ValueMember = "Id"
        Me.Properties.DisplayMember = "NumberName"
        Me.Properties.PopupFormMinSize = New Size(800, 0)
        Me.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.CtrPUCView.Columns.Clear()
        Me.CtrPUCView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcId, Me.INDGcCode, Me.INDGcName, INDGcHandlesThirdParty, Me.INDGcHandlesCostCenter})
        Me.CtrPUCView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrPUCView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrPUCView.OptionsView.ShowDetailButtons = False
        Me.CtrPUCView.OptionsView.ShowGroupPanel = False
        Me.CtrPUCView.OptionsFind.FindFilterColumns = "Number"
        Me.Properties.View = CtrPUCView

        Me.INDGcId.Caption = "Id"
        Me.INDGcId.FieldName = "Id"
        '
        'INDGcCode
        '
        Me.INDGcCode.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGcCode.Caption = "Código"
        Me.INDGcCode.FieldName = "Number"
        Me.INDGcCode.Visible = True
        Me.INDGcCode.VisibleIndex = 0
        Me.INDGcCode.Width = 300
        '
        'INDGcName
        '
        Me.INDGcName.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGcName.Caption = "Nombre"
        Me.INDGcName.FieldName = "Name"
        Me.INDGcName.Visible = True
        Me.INDGcName.VisibleIndex = 1
        Me.INDGcName.Width = 350
        '
        'INDGcHandlesThirdParty
        '
        Me.INDGcHandlesThirdParty.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGcHandlesThirdParty.Caption = "Maneja Tercero"
        Me.INDGcHandlesThirdParty.FieldName = "HandlesThirdParty"
        Me.INDGcHandlesThirdParty.Visible = True
        Me.INDGcHandlesThirdParty.VisibleIndex = 2
        Me.INDGcHandlesThirdParty.Width = 350
        '
        'INDGcHandlesCostCenter
        '
        Me.INDGcHandlesCostCenter.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGcHandlesCostCenter.Caption = "Maneja Centro de Costo"
        Me.INDGcHandlesCostCenter.FieldName = "HandlesCostCenter"
        Me.INDGcHandlesCostCenter.Visible = True
        Me.INDGcHandlesCostCenter.VisibleIndex = 3
        Me.INDGcHandlesCostCenter.Width = 350
    End Sub

End Class