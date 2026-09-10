Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfirmationDocumentsExpenses
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcConfirmationDocumentsExpenses = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcDocuments = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDocuments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleDocumentType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciDocumentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcConfirmationDocumentsExpenses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcConfirmationDocumentsExpenses.SuspendLayout()
        CType(Me.INDGcDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleDocumentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcConfirmationDocumentsExpenses)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 611)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcConfirmationDocumentsExpenses
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 602)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcConfirmationDocumentsExpenses
        '
        Me.INDlcConfirmationDocumentsExpenses.Controls.Add(Me.INDGcDocuments)
        Me.INDlcConfirmationDocumentsExpenses.Controls.Add(Me.INDGleDocumentType)
        Me.INDlcConfirmationDocumentsExpenses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcConfirmationDocumentsExpenses.Location = New System.Drawing.Point(202, 7)
        Me.INDlcConfirmationDocumentsExpenses.Name = "INDlcConfirmationDocumentsExpenses"
        Me.INDlcConfirmationDocumentsExpenses.Root = Me.LayoutControlGroup1
        Me.INDlcConfirmationDocumentsExpenses.Size = New System.Drawing.Size(804, 602)
        Me.INDlcConfirmationDocumentsExpenses.TabIndex = 1
        Me.INDlcConfirmationDocumentsExpenses.Text = "LayoutControl1"
        '
        'INDGcDocuments
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDocuments, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDocuments, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDocuments, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDocuments, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDocuments, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDocuments, False)
        Me.INDGcDocuments.Location = New System.Drawing.Point(24, 123)
        Me.INDGcDocuments.MainView = Me.INDGvDocuments
        Me.INDGcDocuments.Name = "INDGcDocuments"
        Me.INDGcDocuments.Size = New System.Drawing.Size(824, 438)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDocuments, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcDocuments, New System.Drawing.Size(828, 0))
        Me.INDGcDocuments.TabIndex = 4
        Me.INDGcDocuments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDocuments})
        '
        'INDGvDocuments
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvDocuments.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvDocuments.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvDocuments.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvDocuments.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvDocuments.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvDocuments.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDocuments.Appearance.Row.Options.UseFont = True
        Me.INDGvDocuments.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDocuments.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDocuments.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolDate, Me.INDColCode, Me.INDColDocument, Me.INDColStatus})
        Me.INDGvDocuments.GridControl = Me.INDGcDocuments
        Me.INDGvDocuments.Name = "INDGvDocuments"
        Me.INDGvDocuments.OptionsCustomization.AllowGroup = False
        Me.INDGvDocuments.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvDocuments.OptionsDetail.ShowDetailTabs = False
        Me.INDGvDocuments.OptionsSelection.MultiSelect = True
        Me.INDGvDocuments.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvDocuments.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDocuments.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDocuments.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDocuments.OptionsView.ShowDetailButtons = False
        Me.INDGvDocuments.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDocuments, False)
        '
        'INDcolDate
        '
        Me.INDcolDate.Caption = "Fecha"
        Me.INDcolDate.FieldName = "DocumentDate"
        Me.INDcolDate.Name = "INDcolDate"
        Me.INDcolDate.OptionsColumn.AllowEdit = False
        Me.INDcolDate.OptionsColumn.AllowFocus = False
        Me.INDcolDate.Visible = True
        Me.INDcolDate.VisibleIndex = 1
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Consecutivo"
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 2
        '
        'INDColDocument
        '
        Me.INDColDocument.Caption = "Documento"
        Me.INDColDocument.FieldName = "Document"
        Me.INDColDocument.Name = "INDColDocument"
        Me.INDColDocument.OptionsColumn.AllowEdit = False
        Me.INDColDocument.OptionsColumn.AllowFocus = False
        Me.INDColDocument.Visible = True
        Me.INDColDocument.VisibleIndex = 3
        '
        'INDColStatus
        '
        Me.INDColStatus.Caption = "Estado"
        Me.INDColStatus.FieldName = "StatusName"
        Me.INDColStatus.Name = "INDColStatus"
        Me.INDColStatus.OptionsColumn.AllowEdit = False
        Me.INDColStatus.OptionsColumn.AllowFocus = False
        Me.INDColStatus.Visible = True
        Me.INDColStatus.VisibleIndex = 4
        '
        'INDGleDocumentType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleDocumentType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleDocumentType, False)
        Me.INDGleDocumentType.Location = New System.Drawing.Point(24, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleDocumentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleDocumentType.Name = "INDGleDocumentType"
        Me.INDGleDocumentType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGleDocumentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDocumentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleDocumentType.Properties.Appearance.Options.UseFont = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleDocumentType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleDocumentType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleDocumentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleDocumentType.Properties.DisplayMember = "Item2"
        Me.INDGleDocumentType.Properties.NullText = ""
        Me.INDGleDocumentType.Properties.ValueMember = "Item1"
        Me.INDGleDocumentType.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleDocumentType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleDocumentType.StyleController = Me.INDlcConfirmationDocumentsExpenses
        Me.INDGleDocumentType.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleDocumentType, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.GridLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.GridLookUpEdit1View.OptionsDetail.ShowDetailTabs = False
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowDetailButtons = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo De Documento"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(872, 585)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciDocumentType, Me.LayoutControlItem1})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(852, 565)
        Me.INDlcgMainData.Text = "Confirmación de Documentos"
        '
        'INDlciDocumentType
        '
        Me.INDlciDocumentType.Control = Me.INDGleDocumentType
        Me.INDlciDocumentType.Location = New System.Drawing.Point(0, 0)
        Me.INDlciDocumentType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentType.Name = "INDlciDocumentType"
        Me.INDlciDocumentType.Size = New System.Drawing.Size(828, 64)
        Me.INDlciDocumentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentType.Text = "Tipo de Documento"
        Me.INDlciDocumentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentType.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocumentType.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDocuments
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 442)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmConfirmationDocumentsExpenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmConfirmationDocumentsExpenses"
        Me.Opacity = 1.0R
        Me.Tag = "239"
        Me.Text = "Confirmación de Documentos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcConfirmationDocumentsExpenses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcConfirmationDocumentsExpenses.ResumeLayout(False)
        CType(Me.INDGcDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleDocumentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcConfirmationDocumentsExpenses As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGleDocumentType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciDocumentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcDocuments As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDocuments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColStatus As DevExpress.XtraGrid.Columns.GridColumn
End Class
