Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEconomicIndicator
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSeArrearsInterestInvoiced = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeFinancingInterestSale = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeInterestOnArrears = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeFinancialInterests = New DevExpress.XtraEditors.SpinEdit()
        Me.INDDateYearMonth = New Presentation.Controls.CtrDateNavigator()
        Me.INDBeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciYearMonth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDFinancialInterests = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInterestOnArrears = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinancingInterestSale = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciArrearsInterestInvoiced = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDSeArrearsInterestInvoiced.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeFinancingInterestSale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeInterestOnArrears.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeFinancialInterests.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDFinancialInterests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInterestOnArrears, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinancingInterestSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciArrearsInterestInvoiced, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcBase
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSeArrearsInterestInvoiced)
        Me.INDLcBase.Controls.Add(Me.INDSeFinancingInterestSale)
        Me.INDLcBase.Controls.Add(Me.INDSeInterestOnArrears)
        Me.INDLcBase.Controls.Add(Me.INDSeFinancialInterests)
        Me.INDLcBase.Controls.Add(Me.INDDateYearMonth)
        Me.INDLcBase.Controls.Add(Me.INDBeId)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSeArrearsInterestInvoiced
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeArrearsInterestInvoiced, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeArrearsInterestInvoiced, False)
        Me.INDSeArrearsInterestInvoiced.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeArrearsInterestInvoiced.EnterMoveNextControl = True
        Me.INDSeArrearsInterestInvoiced.Location = New System.Drawing.Point(24, 403)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeArrearsInterestInvoiced, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeArrearsInterestInvoiced.Name = "INDSeArrearsInterestInvoiced"
        Me.INDSeArrearsInterestInvoiced.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeArrearsInterestInvoiced.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeArrearsInterestInvoiced.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeArrearsInterestInvoiced.Properties.Appearance.Options.UseFont = True
        Me.INDSeArrearsInterestInvoiced.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeArrearsInterestInvoiced.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeArrearsInterestInvoiced.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeArrearsInterestInvoiced.Size = New System.Drawing.Size(386, 28)
        Me.INDSeArrearsInterestInvoiced.StyleController = Me.INDLcBase
        Me.INDSeArrearsInterestInvoiced.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeArrearsInterestInvoiced, 0)
        '
        'INDSeFinancingInterestSale
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeFinancingInterestSale, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeFinancingInterestSale, False)
        Me.INDSeFinancingInterestSale.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeFinancingInterestSale.EnterMoveNextControl = True
        Me.INDSeFinancingInterestSale.Location = New System.Drawing.Point(24, 339)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeFinancingInterestSale, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeFinancingInterestSale.Name = "INDSeFinancingInterestSale"
        Me.INDSeFinancingInterestSale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeFinancingInterestSale.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeFinancingInterestSale.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeFinancingInterestSale.Properties.Appearance.Options.UseFont = True
        Me.INDSeFinancingInterestSale.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeFinancingInterestSale.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeFinancingInterestSale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeFinancingInterestSale.Size = New System.Drawing.Size(386, 28)
        Me.INDSeFinancingInterestSale.StyleController = Me.INDLcBase
        Me.INDSeFinancingInterestSale.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeFinancingInterestSale, 0)
        '
        'INDSeInterestOnArrears
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeInterestOnArrears, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeInterestOnArrears, False)
        Me.INDSeInterestOnArrears.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeInterestOnArrears.EnterMoveNextControl = True
        Me.INDSeInterestOnArrears.Location = New System.Drawing.Point(24, 275)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeInterestOnArrears, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeInterestOnArrears.Name = "INDSeInterestOnArrears"
        Me.INDSeInterestOnArrears.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeInterestOnArrears.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeInterestOnArrears.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeInterestOnArrears.Properties.Appearance.Options.UseFont = True
        Me.INDSeInterestOnArrears.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeInterestOnArrears.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeInterestOnArrears.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeInterestOnArrears.Size = New System.Drawing.Size(386, 28)
        Me.INDSeInterestOnArrears.StyleController = Me.INDLcBase
        Me.INDSeInterestOnArrears.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeInterestOnArrears, 0)
        '
        'INDSeFinancialInterests
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeFinancialInterests, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeFinancialInterests, False)
        Me.INDSeFinancialInterests.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeFinancialInterests.EnterMoveNextControl = True
        Me.INDSeFinancialInterests.Location = New System.Drawing.Point(24, 211)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeFinancialInterests, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeFinancialInterests.Name = "INDSeFinancialInterests"
        Me.INDSeFinancialInterests.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeFinancialInterests.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeFinancialInterests.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeFinancialInterests.Properties.Appearance.Options.UseFont = True
        Me.INDSeFinancialInterests.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeFinancialInterests.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeFinancialInterests.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeFinancialInterests.Size = New System.Drawing.Size(386, 28)
        Me.INDSeFinancialInterests.StyleController = Me.INDLcBase
        Me.INDSeFinancialInterests.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeFinancialInterests, 0)
        '
        'INDDateYearMonth
        '
        Me.INDDateYearMonth.CtrCalendar = Nothing
        Me.INDDateYearMonth.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDDateYearMonth.Location = New System.Drawing.Point(24, 123)
        Me.INDDateYearMonth.Name = "INDDateYearMonth"
        Me.INDDateYearMonth.Size = New System.Drawing.Size(386, 60)
        Me.INDDateYearMonth.TabIndex = 2
        Me.INDDateYearMonth.WithEvent = True
        '
        'INDBeId
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBeId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBeId, False)
        Me.INDBeId.EnterMoveNextControl = True
        Me.INDBeId.Location = New System.Drawing.Point(24, 83)
        Me.IndigoTextEdit1.SetMascara(Me.INDBeId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBeId.Name = "INDBeId"
        Me.INDBeId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBeId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeId.Properties.Appearance.Options.UseBackColor = True
        Me.INDBeId.Properties.Appearance.Options.UseFont = True
        Me.INDBeId.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Taxes.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDBeId.Size = New System.Drawing.Size(386, 28)
        Me.INDBeId.StyleController = Me.INDLcBase
        Me.INDBeId.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBeId, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGeneralData})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcgBase.TextVisible = False
        '
        'INDGeneralData
        '
        Me.INDGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDGeneralData, False)
        Me.INDGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciId, Me.INDLciYearMonth, Me.INDFinancialInterests, Me.INDLciInterestOnArrears, Me.INDLciFinancingInterestSale, Me.INDLciArrearsInterestInvoiced})
        Me.INDGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDGeneralData.Name = "INDGeneralData"
        Me.INDGeneralData.Size = New System.Drawing.Size(1241, 554)
        Me.INDGeneralData.Text = "Datos Generales"
        '
        'INDLciId
        '
        Me.INDLciId.Control = Me.INDBeId
        Me.INDLciId.Location = New System.Drawing.Point(0, 0)
        Me.INDLciId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciId.Name = "INDLciId"
        Me.INDLciId.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciId.Text = "Id"
        Me.INDLciId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciId.TextSize = New System.Drawing.Size(196, 21)
        '
        'INDLciYearMonth
        '
        Me.INDLciYearMonth.Control = Me.INDDateYearMonth
        Me.INDLciYearMonth.Location = New System.Drawing.Point(0, 64)
        Me.INDLciYearMonth.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciYearMonth.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciYearMonth.Name = "INDLciYearMonth"
        Me.INDLciYearMonth.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciYearMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYearMonth.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciYearMonth.TextVisible = False
        '
        'INDFinancialInterests
        '
        Me.INDFinancialInterests.Control = Me.INDSeFinancialInterests
        Me.INDFinancialInterests.Location = New System.Drawing.Point(0, 128)
        Me.INDFinancialInterests.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDFinancialInterests.MinSize = New System.Drawing.Size(390, 64)
        Me.INDFinancialInterests.Name = "INDFinancialInterests"
        Me.INDFinancialInterests.Size = New System.Drawing.Size(1217, 64)
        Me.INDFinancialInterests.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDFinancialInterests.Text = "Intereses financieros"
        Me.INDFinancialInterests.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDFinancialInterests.TextSize = New System.Drawing.Size(196, 21)
        '
        'INDLciInterestOnArrears
        '
        Me.INDLciInterestOnArrears.Control = Me.INDSeInterestOnArrears
        Me.INDLciInterestOnArrears.Location = New System.Drawing.Point(0, 192)
        Me.INDLciInterestOnArrears.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciInterestOnArrears.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciInterestOnArrears.Name = "INDLciInterestOnArrears"
        Me.INDLciInterestOnArrears.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciInterestOnArrears.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInterestOnArrears.Text = "Interés de Mora"
        Me.INDLciInterestOnArrears.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInterestOnArrears.TextSize = New System.Drawing.Size(196, 21)
        '
        'INDLciFinancingInterestSale
        '
        Me.INDLciFinancingInterestSale.Control = Me.INDSeFinancingInterestSale
        Me.INDLciFinancingInterestSale.Location = New System.Drawing.Point(0, 256)
        Me.INDLciFinancingInterestSale.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciFinancingInterestSale.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciFinancingInterestSale.Name = "INDLciFinancingInterestSale"
        Me.INDLciFinancingInterestSale.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciFinancingInterestSale.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFinancingInterestSale.Text = "Interés de Financiación Venta"
        Me.INDLciFinancingInterestSale.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFinancingInterestSale.TextSize = New System.Drawing.Size(196, 21)
        '
        'INDLciArrearsInterestInvoiced
        '
        Me.INDLciArrearsInterestInvoiced.Control = Me.INDSeArrearsInterestInvoiced
        Me.INDLciArrearsInterestInvoiced.Location = New System.Drawing.Point(0, 320)
        Me.INDLciArrearsInterestInvoiced.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciArrearsInterestInvoiced.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciArrearsInterestInvoiced.Name = "INDLciArrearsInterestInvoiced"
        Me.INDLciArrearsInterestInvoiced.Size = New System.Drawing.Size(1217, 175)
        Me.INDLciArrearsInterestInvoiced.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciArrearsInterestInvoiced.Text = "Interés Mora A.E. Facturado"
        Me.INDLciArrearsInterestInvoiced.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciArrearsInterestInvoiced.TextSize = New System.Drawing.Size(196, 21)
        '
        'FrmEconomicIndicator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmEconomicIndicator"
        Me.Opacity = 1.0R
        Me.Tag = "1842"
        Me.Text = "Indicador Económico"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDSeArrearsInterestInvoiced.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeFinancingInterestSale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeInterestOnArrears.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeFinancialInterests.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDFinancialInterests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInterestOnArrears, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinancingInterestSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciArrearsInterestInvoiced, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDBeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDateYearMonth As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDLciYearMonth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeFinancialInterests As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDFinancialInterests As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeInterestOnArrears As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciInterestOnArrears As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeArrearsInterestInvoiced As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSeFinancingInterestSale As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciFinancingInterestSale As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciArrearsInterestInvoiced As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
End Class
