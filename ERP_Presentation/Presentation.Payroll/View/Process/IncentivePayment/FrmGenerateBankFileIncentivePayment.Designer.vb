Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGenerateBankFileIncentivePayment
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgleLastLiquidationDate = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDateIncentivePayment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDButtonFileGenerate = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleBank = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvSleBank = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleCompany = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNitCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCodeCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBank = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLastLiquidationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvSleBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(734, 177)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(734, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(734, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDgleLastLiquidationDate)
        Me.LayoutControl1.Controls.Add(Me.INDButtonFileGenerate)
        Me.LayoutControl1.Controls.Add(Me.INDsleBank)
        Me.LayoutControl1.Controls.Add(Me.INDgleCompany)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsView.UseDefaultDragAndDropRendering = False
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(730, 168)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgleLastLiquidationDate
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleLastLiquidationDate, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleLastLiquidationDate, False)
        Me.INDgleLastLiquidationDate.Location = New System.Drawing.Point(189, 84)
        Me.INDgleLastLiquidationDate.Name = "INDgleLastLiquidationDate"
        Me.INDgleLastLiquidationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleLastLiquidationDate.Properties.Appearance.Options.UseFont = True
        Me.INDgleLastLiquidationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleLastLiquidationDate.Properties.DisplayMember = "Date.Date"
        Me.INDgleLastLiquidationDate.Properties.ImmediatePopup = True
        Me.INDgleLastLiquidationDate.Properties.NullText = ""
        Me.INDgleLastLiquidationDate.Properties.ValueMember = "Date"
        Me.INDgleLastLiquidationDate.Properties.View = Me.GridLookUpEdit2View
        Me.INDgleLastLiquidationDate.Size = New System.Drawing.Size(269, 28)
        Me.INDgleLastLiquidationDate.StyleController = Me.LayoutControl1
        Me.INDgleLastLiquidationDate.TabIndex = 7
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleLastLiquidationDate, Nothing)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDateIncentivePayment})
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'INDColDateIncentivePayment
        '
        Me.INDColDateIncentivePayment.Caption = "Fecha"
        Me.INDColDateIncentivePayment.FieldName = "Date"
        Me.INDColDateIncentivePayment.Name = "INDColDateIncentivePayment"
        Me.INDColDateIncentivePayment.Visible = True
        Me.INDColDateIncentivePayment.VisibleIndex = 0
        '
        'INDButtonFileGenerate
        '
        Me.INDButtonFileGenerate.Location = New System.Drawing.Point(192, 120)
        Me.INDButtonFileGenerate.MaximumSize = New System.Drawing.Size(269, 0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDButtonFileGenerate, False)
        Me.INDButtonFileGenerate.Name = "INDButtonFileGenerate"
        Me.INDButtonFileGenerate.Size = New System.Drawing.Size(269, 32)
        Me.INDButtonFileGenerate.StyleController = Me.LayoutControl1
        Me.INDButtonFileGenerate.TabIndex = 6
        Me.INDButtonFileGenerate.Text = "Generar Archivo"
        '
        'INDsleBank
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBank, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBank, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBank, False)
        Me.INDsleBank.Location = New System.Drawing.Point(189, 48)
        Me.INDsleBank.Name = "INDsleBank"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBank, False)
        Me.INDsleBank.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBank.Properties.Appearance.Options.UseFont = True
        Me.INDsleBank.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleBank.Properties.DisplayMember = "Number"
        Me.INDsleBank.Properties.NullText = ""
        Me.INDsleBank.Properties.PopupSizeable = False
        Me.INDsleBank.Properties.ShowFooter = False
        Me.INDsleBank.Properties.ValueMember = "Number"
        Me.INDsleBank.Properties.View = Me.INDgvSleBank
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBank, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBank, True)
        Me.INDsleBank.Size = New System.Drawing.Size(269, 28)
        Me.INDsleBank.StyleController = Me.LayoutControl1
        Me.INDsleBank.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBank, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBank, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBank, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBank, False)
        '
        'INDgvSleBank
        '
        Me.INDgvSleBank.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvSleBank.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvSleBank.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvSleBank.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvSleBank.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvSleBank.Appearance.Row.Options.UseFont = True
        Me.INDgvSleBank.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColBank, Me.INDColAccount})
        Me.INDgvSleBank.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvSleBank.Name = "INDgvSleBank"
        Me.INDgvSleBank.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvSleBank.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvSleBank.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvSleBank.OptionsView.ShowAutoFilterRow = True
        Me.INDgvSleBank.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvSleBank, False)
        '
        'INDColBank
        '
        Me.INDColBank.Caption = "Banco"
        Me.INDColBank.FieldName = "IdBank.Name"
        Me.INDColBank.Name = "INDColBank"
        Me.INDColBank.Visible = True
        Me.INDColBank.VisibleIndex = 0
        '
        'INDColAccount
        '
        Me.INDColAccount.Caption = "Cuenta Bancaria"
        Me.INDColAccount.FieldName = "Number"
        Me.INDColAccount.Name = "INDColAccount"
        Me.INDColAccount.Visible = True
        Me.INDColAccount.VisibleIndex = 1
        '
        'INDgleCompany
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleCompany, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleCompany, False)
        Me.INDgleCompany.Location = New System.Drawing.Point(189, 12)
        Me.INDgleCompany.Name = "INDgleCompany"
        Me.INDgleCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleCompany.Properties.Appearance.Options.UseFont = True
        Me.INDgleCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleCompany.Properties.DisplayMember = "Name"
        Me.INDgleCompany.Properties.ImmediatePopup = True
        Me.INDgleCompany.Properties.NullText = ""
        Me.INDgleCompany.Properties.ValueMember = "Id"
        Me.INDgleCompany.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleCompany.Size = New System.Drawing.Size(269, 28)
        Me.INDgleCompany.StyleController = Me.LayoutControl1
        Me.INDgleCompany.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleCompany, Nothing)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNitCompany, Me.INDColNameCompany})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDColNitCompany
        '
        Me.INDColNitCompany.Caption = "Nit"
        Me.INDColNitCompany.FieldName = "Nit"
        Me.INDColNitCompany.Name = "INDColNitCompany"
        Me.INDColNitCompany.Visible = True
        Me.INDColNitCompany.VisibleIndex = 0
        '
        'INDColNameCompany
        '
        Me.INDColNameCompany.Caption = "Nombre"
        Me.INDColNameCompany.FieldName = "Name"
        Me.INDColNameCompany.Name = "INDColNameCompany"
        Me.INDColNameCompany.Visible = True
        Me.INDColNameCompany.VisibleIndex = 1
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodeCompany, Me.INDlyItemBank, Me.LayoutControlItem3, Me.INDlyItemLastLiquidationDate, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(730, 168)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemCodeCompany
        '
        Me.INDlyItemCodeCompany.Control = Me.INDgleCompany
        Me.INDlyItemCodeCompany.CustomizationFormText = "INDlyItemCodeCompany"
        Me.INDlyItemCodeCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodeCompany.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemCodeCompany.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemCodeCompany.Name = "INDlyItemCodeCompany"
        Me.INDlyItemCodeCompany.Size = New System.Drawing.Size(710, 36)
        Me.INDlyItemCodeCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeCompany.Text = "Empresa"
        Me.INDlyItemCodeCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeCompany.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCodeCompany.TextToControlDistance = 12
        '
        'INDlyItemBank
        '
        Me.INDlyItemBank.Control = Me.INDsleBank
        Me.INDlyItemBank.CustomizationFormText = "INDlyItemBank"
        Me.INDlyItemBank.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemBank.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemBank.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemBank.Name = "INDlyItemBank"
        Me.INDlyItemBank.Size = New System.Drawing.Size(710, 36)
        Me.INDlyItemBank.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBank.Text = "Banco"
        Me.INDlyItemBank.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBank.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemBank.TextToControlDistance = 12
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDButtonFileGenerate
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(180, 108)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(450, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(450, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(530, 40)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDlyItemLastLiquidationDate
        '
        Me.INDlyItemLastLiquidationDate.Control = Me.INDgleLastLiquidationDate
        Me.INDlyItemLastLiquidationDate.CustomizationFormText = "INDlyItemLastLiquidationDate"
        Me.INDlyItemLastLiquidationDate.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemLastLiquidationDate.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.Name = "INDlyItemLastLiquidationDate"
        Me.INDlyItemLastLiquidationDate.Size = New System.Drawing.Size(710, 36)
        Me.INDlyItemLastLiquidationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLastLiquidationDate.Text = "Periodo Prima"
        Me.INDlyItemLastLiquidationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLastLiquidationDate.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemLastLiquidationDate.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 108)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(180, 21)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(180, 21)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(180, 40)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150514"

        '
        'FrmGenerateBankFileIncentivePayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 294)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGenerateBankFileIncentivePayment"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generar Archivo Plano"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvSleBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDButtonFileGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDsleBank As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDgvSleBank As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgleCompany As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCodeCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemBank As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgleLastLiquidationDate As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemLastLiquidationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColNitCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAccount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDateIncentivePayment As DevExpress.XtraGrid.Columns.GridColumn
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
