Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPacTransfer
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPacTransfer))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcPacTransfer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResidue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCharacter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbNature = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDRptIcbMonth = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDsbAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonthPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcPACControlPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcPACControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgPacTransfer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgAnnualizedCashFlow = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciAddCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcPacTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcPacTransfer.SuspendLayout()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgPacTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgAnnualizedCashFlow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAddCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcPacTransfer)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcPacTransfer
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcPacTransfer
        '
        Me.INDlcPacTransfer.Controls.Add(Me.INDgcDetail)
        Me.INDlcPacTransfer.Controls.Add(Me.INDsbAddCategory)
        Me.INDlcPacTransfer.Controls.Add(Me.INDmeObservations)
        Me.INDlcPacTransfer.Controls.Add(Me.INDtxtDocument)
        Me.INDlcPacTransfer.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlcPacTransfer.Controls.Add(Me.INDbtnCode)
        Me.INDlcPacTransfer.Controls.Add(Me.INDpccChangeEntity)
        Me.INDlcPacTransfer.Controls.Add(Me.INDsleValidity)
        Me.INDlcPacTransfer.Controls.Add(Me.INDsleEntity)
        Me.INDlcPacTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcPacTransfer.Location = New System.Drawing.Point(202, 7)
        Me.INDlcPacTransfer.Name = "INDlcPacTransfer"
        Me.INDlcPacTransfer.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(459, 288, 250, 350)
        Me.INDlcPacTransfer.Root = Me.INDlcgPacTransfer
        Me.INDlcPacTransfer.Size = New System.Drawing.Size(804, 598)
        Me.INDlcPacTransfer.TabIndex = 1
        Me.INDlcPacTransfer.Text = "LayoutControl1"
        '
        'INDgcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetail, Nothing)
        Me.INDgcDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcDetail.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetail, False)
        Me.INDgcDetail.Location = New System.Drawing.Point(852, 95)
        Me.INDgcDetail.MainView = Me.INDgvDetail
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptIcbNature, Me.INDRptIcbMonth, Me.INDRptTxtValue})
        Me.INDgcDetail.Size = New System.Drawing.Size(824, 462)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 13
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetail})
        '
        'INDgvDetail
        '
        Me.INDgvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColResource, Me.INDColMonth, Me.INDColResidue, Me.INDColCharacter, Me.INDColValue})
        Me.INDgvDetail.GridControl = Me.INDgcDetail
        Me.INDgvDetail.Name = "INDgvDetail"
        Me.INDgvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvDetail.OptionsDetail.SmartDetailExpand = False
        Me.INDgvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvDetail.OptionsView.ShowFooter = True
        Me.INDgvDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetail, False)
        Me.INDgvDetail.ViewCaption = "Identificación del Rubro"
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Rubro"
        Me.INDColCode.FieldName = "CodeNameCategory"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.OptionsColumn.ReadOnly = True
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        Me.INDColCode.Width = 232
        '
        'INDColResource
        '
        Me.INDColResource.Caption = "Recurso"
        Me.INDColResource.FieldName = "CodeNameFinancialSource"
        Me.INDColResource.Name = "INDColResource"
        Me.INDColResource.OptionsColumn.AllowEdit = False
        Me.INDColResource.OptionsColumn.AllowFocus = False
        Me.INDColResource.OptionsColumn.ReadOnly = True
        Me.INDColResource.Visible = True
        Me.INDColResource.VisibleIndex = 1
        Me.INDColResource.Width = 166
        '
        'INDColMonth
        '
        Me.INDColMonth.Caption = "Mes"
        Me.INDColMonth.FieldName = "Month"
        Me.INDColMonth.Name = "INDColMonth"
        Me.INDColMonth.OptionsColumn.AllowEdit = False
        Me.INDColMonth.OptionsColumn.AllowFocus = False
        Me.INDColMonth.OptionsColumn.ReadOnly = True
        Me.INDColMonth.Visible = True
        Me.INDColMonth.VisibleIndex = 2
        Me.INDColMonth.Width = 170
        '
        'INDColResidue
        '
        Me.INDColResidue.Caption = "Saldo"
        Me.INDColResidue.DisplayFormat.FormatString = "c0"
        Me.INDColResidue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColResidue.FieldName = "Balance"
        Me.INDColResidue.Name = "INDColResidue"
        Me.INDColResidue.OptionsColumn.AllowEdit = False
        Me.INDColResidue.OptionsColumn.AllowFocus = False
        Me.INDColResidue.OptionsColumn.ReadOnly = True
        Me.INDColResidue.Visible = True
        Me.INDColResidue.VisibleIndex = 3
        Me.INDColResidue.Width = 161
        '
        'INDColCharacter
        '
        Me.INDColCharacter.Caption = "Naturaleza"
        Me.INDColCharacter.ColumnEdit = Me.INDRptIcbNature
        Me.INDColCharacter.FieldName = "Nature"
        Me.INDColCharacter.Name = "INDColCharacter"
        Me.INDColCharacter.Visible = True
        Me.INDColCharacter.VisibleIndex = 4
        Me.INDColCharacter.Width = 129
        '
        'INDRptIcbNature
        '
        Me.INDRptIcbNature.AutoHeight = False
        Me.INDRptIcbNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptIcbNature.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Débito", CType(1, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Crédito", CType(2, Byte), 1)})
        Me.INDRptIcbNature.LargeImages = Me.ImageCollection1
        Me.INDRptIcbNature.Name = "INDRptIcbNature"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "-.png")
        Me.ImageCollection1.Images.SetKeyName(1, "+.png")
        '
        'INDColValue
        '
        Me.INDColValue.Caption = "Valor"
        Me.INDColValue.ColumnEdit = Me.INDRptTxtValue
        Me.INDColValue.DisplayFormat.FormatString = "c0"
        Me.INDColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColValue.FieldName = "Value"
        Me.INDColValue.Name = "INDColValue"
        Me.INDColValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c0}")})
        Me.INDColValue.Visible = True
        Me.INDColValue.VisibleIndex = 5
        Me.INDColValue.Width = 202
        '
        'INDRptTxtValue
        '
        Me.INDRptTxtValue.AutoHeight = False
        Me.INDRptTxtValue.Mask.EditMask = "c0"
        Me.INDRptTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDRptTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptTxtValue.MaxLength = 16
        Me.INDRptTxtValue.Name = "INDRptTxtValue"
        '
        'INDRptIcbMonth
        '
        Me.INDRptIcbMonth.AutoHeight = False
        Me.INDRptIcbMonth.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enero", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Febrero", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Marzo", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Abril", CType(4, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mayo", CType(5, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Junio", CType(6, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Julio", CType(7, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Agosto", CType(8, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Septiembre", CType(9, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Octubre", CType(10, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Noviembre", CType(11, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Diciembre", CType(12, Byte), -1)})
        Me.INDRptIcbMonth.Name = "INDRptIcbMonth"
        '
        'INDsbAddCategory
        '
        Me.INDsbAddCategory.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAddCategory.Appearance.Options.UseFont = True
        Me.INDsbAddCategory.Location = New System.Drawing.Point(852, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddCategory, True)
        Me.INDsbAddCategory.Name = "INDsbAddCategory"
        Me.INDsbAddCategory.Size = New System.Drawing.Size(824, 32)
        Me.INDsbAddCategory.StyleController = Me.INDlcPacTransfer
        Me.INDsbAddCategory.TabIndex = 12
        Me.INDsbAddCategory.Text = "Agregar"
        '
        'INDmeObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObservations, True)
        Me.INDmeObservations.EnterMoveNextControl = True
        Me.INDmeObservations.Location = New System.Drawing.Point(438, 276)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeObservations.Name = "INDmeObservations"
        Me.INDmeObservations.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmeObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeObservations.Properties.MaxLength = 500
        Me.INDmeObservations.Size = New System.Drawing.Size(386, 71)
        Me.INDmeObservations.StyleController = Me.INDlcPacTransfer
        Me.INDmeObservations.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObservations, 0)
        Me.INDmeObservations.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDocument, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDocument, True)
        Me.INDtxtDocument.EditValue = ""
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(438, 212)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDocument.Name = "INDtxtDocument"
        Me.INDtxtDocument.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDocument.Properties.MaxLength = 100
        Me.INDtxtDocument.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDocument.StyleController = Me.INDlcPacTransfer
        Me.INDtxtDocument.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        Me.INDtxtDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(438, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlcPacTransfer
        Me.INDdeDocumentDate.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(438, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlcPacTransfer
        Me.INDbtnCode.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(64, 280)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 11
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleEntityPopUp)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Location = New System.Drawing.Point(12, 96)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidityPopUp.Name = "INDsleValidityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidityPopUp.Properties.DisplayMember = "Year"
        Me.INDsleValidityPopUp.Properties.NullText = ""
        Me.INDsleValidityPopUp.Properties.PopupSizeable = False
        Me.INDsleValidityPopUp.Properties.PopupView = Me.INDGvValidityPopUp
        Me.INDsleValidityPopUp.Properties.ShowFooter = False
        Me.INDsleValidityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidityPopUp, True)
        Me.INDsleValidityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleValidityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleValidityPopUp.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
        '
        'INDGvValidityPopUp
        '
        Me.INDGvValidityPopUp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvValidityPopUp.Appearance.Row.Options.UseFont = True
        Me.INDGvValidityPopUp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.INDgcStatusValidityPopUp, Me.GridColumn7, Me.GridColumn8, Me.INDgcIncomeMonthPopUp, Me.INDgcPACControlPopUp})
        Me.INDGvValidityPopUp.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvValidityPopUp.Name = "INDGvValidityPopUp"
        Me.INDGvValidityPopUp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowAutoFilterRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvValidityPopUp, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Año"
        Me.GridColumn4.FieldName = "Year"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'INDgcStatusValidityPopUp
        '
        Me.INDgcStatusValidityPopUp.Caption = "Estado"
        Me.INDgcStatusValidityPopUp.FieldName = "StatusText"
        Me.INDgcStatusValidityPopUp.Name = "INDgcStatusValidityPopUp"
        Me.INDgcStatusValidityPopUp.Visible = True
        Me.INDgcStatusValidityPopUp.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Resolución"
        Me.GridColumn7.FieldName = "ResolutionNumber"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor"
        Me.GridColumn8.DisplayFormat.FormatString = "c0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ResolutionValue"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        '
        'INDgcIncomeMonthPopUp
        '
        Me.INDgcIncomeMonthPopUp.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonthPopUp.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonthPopUp.Name = "INDgcIncomeMonthPopUp"
        '
        'INDgcPACControlPopUp
        '
        Me.INDgcPACControlPopUp.Caption = "P A C"
        Me.INDgcPACControlPopUp.FieldName = "PACControl"
        Me.INDgcPACControlPopUp.Name = "INDgcPACControlPopUp"
        '
        'INDsleEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityPopUp, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityPopUp, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityPopUp.Name = "INDsleEntityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityPopUp.Properties.DisplayMember = "NameCode"
        Me.INDsleEntityPopUp.Properties.NullText = ""
        Me.INDsleEntityPopUp.Properties.PopupSizeable = False
        Me.INDsleEntityPopUp.Properties.PopupView = Me.GridView2
        Me.INDsleEntityPopUp.Properties.ShowFooter = False
        Me.INDsleEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityPopUp, True)
        Me.INDsleEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleEntityPopUp.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityPopUp, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsDetail.ShowDetailTabs = False
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Nombre"
        Me.GridColumn10.FieldName = "Name"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleEntityPopUp
        Me.INDlciEntityPopUp.CustomizationFormText = "LayoutControlItem3"
        Me.INDlciEntityPopUp.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.Name = "INDlciEntityPopUp"
        Me.INDlciEntityPopUp.Size = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntityPopUp.Text = "Entidad Presupuestal"
        Me.INDlciEntityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDlciValidityPopUp
        '
        Me.INDlciValidityPopUp.Control = Me.INDsleValidityPopUp
        Me.INDlciValidityPopUp.CustomizationFormText = "LayoutControlItem4"
        Me.INDlciValidityPopUp.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.Name = "INDlciValidityPopUp"
        Me.INDlciValidityPopUp.Size = New System.Drawing.Size(279, 66)
        Me.INDlciValidityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidityPopUp.Text = "Vigencia"
        Me.INDlciValidityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = ""
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDlcPacTransfer
        Me.INDsleValidity.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, "")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValidity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.INDgcStatusValidity, Me.GridColumn5, Me.GridColumn6, Me.INDgcIncomeMonth, Me.INDgcPACControl})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsCustomization.AllowGroup = False
        Me.INDgvValidity.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvValidity.OptionsDetail.ShowDetailTabs = False
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowDetailButtons = False
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Año"
        Me.GridColumn3.FieldName = "Year"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'INDgcStatusValidity
        '
        Me.INDgcStatusValidity.Caption = "Estado"
        Me.INDgcStatusValidity.FieldName = "StatusText"
        Me.INDgcStatusValidity.Name = "INDgcStatusValidity"
        Me.INDgcStatusValidity.Visible = True
        Me.INDgcStatusValidity.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Resolución"
        Me.GridColumn5.FieldName = "ResolutionNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Valor"
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "ResolutionValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        '
        'INDgcIncomeMonth
        '
        Me.INDgcIncomeMonth.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonth.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonth.Name = "INDgcIncomeMonth"
        '
        'INDgcPACControl
        '
        Me.INDgcPACControl.Caption = "P A C"
        Me.INDgcPACControl.FieldName = "PACControl"
        Me.INDgcPACControl.Name = "INDgcPACControl"
        '
        'INDsleEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntity, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntity, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Location = New System.Drawing.Point(24, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = ""
        Me.INDsleEntity.Properties.PopupSizeable = False
        Me.INDsleEntity.Properties.PopupView = Me.GridView1
        Me.INDsleEntity.Properties.ShowFooter = False
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntity, True)
        Me.INDsleEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntity.StyleController = Me.INDlcPacTransfer
        Me.INDsleEntity.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntity, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDlcgPacTransfer
        '
        Me.INDlcgPacTransfer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPacTransfer.AppearanceGroup.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPacTransfer.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPacTransfer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPacTransfer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgPacTransfer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPacTransfer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgPacTransfer, False)
        Me.INDlcgPacTransfer.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgPacTransfer.GroupBordersVisible = False
        Me.INDlcgPacTransfer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgAnnualizedCashFlow, Me.INDlcgMainData, Me.INDlcgDetail})
        Me.INDlcgPacTransfer.Name = "Root"
        Me.INDlcgPacTransfer.Size = New System.Drawing.Size(1700, 581)
        Me.INDlcgPacTransfer.TextVisible = False
        '
        'INDlcgAnnualizedCashFlow
        '
        Me.INDlcgAnnualizedCashFlow.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAnnualizedCashFlow.AppearanceGroup.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAnnualizedCashFlow.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAnnualizedCashFlow.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgAnnualizedCashFlow, False)
        Me.INDlcgAnnualizedCashFlow.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciValidity, Me.INDlciEntity})
        Me.INDlcgAnnualizedCashFlow.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgAnnualizedCashFlow.Name = "INDlcgAnnualizedCashFlow"
        Me.INDlcgAnnualizedCashFlow.Size = New System.Drawing.Size(414, 561)
        Me.INDlcgAnnualizedCashFlow.Text = "Traslado Plan Anualizado de Caja"
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 438)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciValidity.TextToControlDistance = 5
        '
        'INDlciEntity
        '
        Me.INDlciEntity.Control = Me.INDsleEntity
        Me.INDlciEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciEntity.Name = "INDlciEntity"
        Me.INDlciEntity.Size = New System.Drawing.Size(390, 64)
        Me.INDlciEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntity.Text = "Entidad Presupuestal"
        Me.INDlciEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntity.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciEntity.TextToControlDistance = 5
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCode, Me.INDlciDocumentDate, Me.INDlciDocument, Me.INDlciObservations})
        Me.INDlcgMainData.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 561)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciDocumentDate
        '
        Me.INDlciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDlciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDlciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.Name = "INDlciDocumentDate"
        Me.INDlciDocumentDate.ShowInCustomizationForm = False
        Me.INDlciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentDate.Text = "Fecha del Documento"
        Me.INDlciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentDate.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocumentDate.TextToControlDistance = 5
        '
        'INDlciDocument
        '
        Me.INDlciDocument.Control = Me.INDtxtDocument
        Me.INDlciDocument.Location = New System.Drawing.Point(0, 128)
        Me.INDlciDocument.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.Name = "INDlciDocument"
        Me.INDlciDocument.ShowInCustomizationForm = False
        Me.INDlciDocument.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocument.Text = "Documento"
        Me.INDlciDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocument.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocument.TextToControlDistance = 5
        '
        'INDlciObservations
        '
        Me.INDlciObservations.Control = Me.INDmeObservations
        Me.INDlciObservations.Location = New System.Drawing.Point(0, 192)
        Me.INDlciObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.Name = "INDlciObservations"
        Me.INDlciObservations.ShowInCustomizationForm = False
        Me.INDlciObservations.Size = New System.Drawing.Size(390, 310)
        Me.INDlciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciObservations.Text = "Observaciones"
        Me.INDlciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciObservations.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciObservations.TextToControlDistance = 5
        '
        'INDlcgDetail
        '
        Me.INDlcgDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDetail.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgDetail, False)
        Me.INDlcgDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciAddCategory, Me.LayoutControlItem1})
        Me.INDlcgDetail.Location = New System.Drawing.Point(828, 0)
        Me.INDlcgDetail.Name = "INDlcgDetail"
        Me.INDlcgDetail.Size = New System.Drawing.Size(852, 561)
        Me.INDlcgDetail.Text = "Listado de Rubros"
        '
        'INDlciAddCategory
        '
        Me.INDlciAddCategory.Control = Me.INDsbAddCategory
        Me.INDlciAddCategory.CustomizationFormText = "Agregar Rubro"
        Me.INDlciAddCategory.Location = New System.Drawing.Point(0, 0)
        Me.INDlciAddCategory.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlciAddCategory.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlciAddCategory.Name = "INDlciAddCategory"
        Me.INDlciAddCategory.Size = New System.Drawing.Size(828, 36)
        Me.INDlciAddCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAddCategory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAddCategory.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcDetail
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Rubros"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 466)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPacTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmPacTransfer"
        Me.Opacity = 1.0R
        Me.Tag = "212"
        Me.Text = "Traslado De Plan Anualizado de Caja - Ingresos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcPacTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcPacTransfer.ResumeLayout(False)
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgPacTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgAnnualizedCashFlow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAddCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlcPacTransfer As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgPacTransfer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPACControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgAnnualizedCashFlow As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonthPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPACControlPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDlcgDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciAddCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptIcbMonth As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDColResidue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCharacter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptIcbNature As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDColValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
End Class
