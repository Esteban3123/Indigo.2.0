Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingBudget
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDRgDocumentsGroupping = New DevExpress.XtraEditors.RadioGroup()
        Me.INDRgCreateReceivableAccounts = New DevExpress.XtraEditors.RadioGroup()
        Me.INDRgCreatePayableAccounts = New DevExpress.XtraEditors.RadioGroup()
        Me.INDRgCreateReserves = New DevExpress.XtraEditors.RadioGroup()
        Me.INDRgMovementAccounting = New DevExpress.XtraEditors.RadioGroup()
        Me.INDRgEnabledInterface = New DevExpress.XtraEditors.RadioGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyEnabledInterface = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyMovementAccounting = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyCreateReserves = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyCreatePayableAccounts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyCreateReceivableAccounts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDocumentsGroupping = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDRgDocumentsGroupping.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgCreateReceivableAccounts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgCreatePayableAccounts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgCreateReserves.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgMovementAccounting.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgEnabledInterface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyEnabledInterface, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyMovementAccounting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCreateReserves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCreatePayableAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCreateReceivableAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDocumentsGroupping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1154, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1154, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1154, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDRgDocumentsGroupping)
        Me.LayoutControl1.Controls.Add(Me.INDRgCreateReceivableAccounts)
        Me.LayoutControl1.Controls.Add(Me.INDRgCreatePayableAccounts)
        Me.LayoutControl1.Controls.Add(Me.INDRgCreateReserves)
        Me.LayoutControl1.Controls.Add(Me.INDRgMovementAccounting)
        Me.LayoutControl1.Controls.Add(Me.INDRgEnabledInterface)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(950, 574)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDRgDocumentsGroupping
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgDocumentsGroupping, False)
        Me.INDRgDocumentsGroupping.EnterMoveNextControl = True
        Me.INDRgDocumentsGroupping.Location = New System.Drawing.Point(750, 167)
        Me.INDRgDocumentsGroupping.Name = "INDRgDocumentsGroupping"
        Me.INDRgDocumentsGroupping.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgDocumentsGroupping.Properties.Appearance.Options.UseFont = True
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgDocumentsGroupping.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgDocumentsGroupping.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgDocumentsGroupping.Size = New System.Drawing.Size(174, 32)
        Me.INDRgDocumentsGroupping.StyleController = Me.LayoutControl1
        Me.INDRgDocumentsGroupping.TabIndex = 5
        '
        'INDRgCreateReceivableAccounts
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgCreateReceivableAccounts, False)
        Me.INDRgCreateReceivableAccounts.EnterMoveNextControl = True
        Me.INDRgCreateReceivableAccounts.Location = New System.Drawing.Point(750, 131)
        Me.INDRgCreateReceivableAccounts.Name = "INDRgCreateReceivableAccounts"
        Me.INDRgCreateReceivableAccounts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreateReceivableAccounts.Properties.Appearance.Options.UseFont = True
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgCreateReceivableAccounts.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgCreateReceivableAccounts.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgCreateReceivableAccounts.Size = New System.Drawing.Size(174, 32)
        Me.INDRgCreateReceivableAccounts.StyleController = Me.LayoutControl1
        Me.INDRgCreateReceivableAccounts.TabIndex = 4
        '
        'INDRgCreatePayableAccounts
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgCreatePayableAccounts, False)
        Me.INDRgCreatePayableAccounts.EnterMoveNextControl = True
        Me.INDRgCreatePayableAccounts.Location = New System.Drawing.Point(750, 95)
        Me.INDRgCreatePayableAccounts.Name = "INDRgCreatePayableAccounts"
        Me.INDRgCreatePayableAccounts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreatePayableAccounts.Properties.Appearance.Options.UseFont = True
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgCreatePayableAccounts.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgCreatePayableAccounts.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgCreatePayableAccounts.Size = New System.Drawing.Size(174, 32)
        Me.INDRgCreatePayableAccounts.StyleController = Me.LayoutControl1
        Me.INDRgCreatePayableAccounts.TabIndex = 3
        '
        'INDRgCreateReserves
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgCreateReserves, False)
        Me.INDRgCreateReserves.EnterMoveNextControl = True
        Me.INDRgCreateReserves.Location = New System.Drawing.Point(750, 59)
        Me.INDRgCreateReserves.Name = "INDRgCreateReserves"
        Me.INDRgCreateReserves.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreateReserves.Properties.Appearance.Options.UseFont = True
        Me.INDRgCreateReserves.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgCreateReserves.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgCreateReserves.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgCreateReserves.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgCreateReserves.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgCreateReserves.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgCreateReserves.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgCreateReserves.Size = New System.Drawing.Size(174, 32)
        Me.INDRgCreateReserves.StyleController = Me.LayoutControl1
        Me.INDRgCreateReserves.TabIndex = 2
        '
        'INDRgMovementAccounting
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgMovementAccounting, False)
        Me.INDRgMovementAccounting.EnterMoveNextControl = True
        Me.INDRgMovementAccounting.Location = New System.Drawing.Point(296, 95)
        Me.INDRgMovementAccounting.Name = "INDRgMovementAccounting"
        Me.INDRgMovementAccounting.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgMovementAccounting.Properties.Appearance.Options.UseFont = True
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgMovementAccounting.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgMovementAccounting.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgMovementAccounting.Size = New System.Drawing.Size(184, 32)
        Me.INDRgMovementAccounting.StyleController = Me.LayoutControl1
        Me.INDRgMovementAccounting.TabIndex = 1
        '
        'INDRgEnabledInterface
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgEnabledInterface, False)
        Me.INDRgEnabledInterface.EnterMoveNextControl = True
        Me.INDRgEnabledInterface.Location = New System.Drawing.Point(296, 59)
        Me.INDRgEnabledInterface.Name = "INDRgEnabledInterface"
        Me.INDRgEnabledInterface.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgEnabledInterface.Properties.Appearance.Options.UseFont = True
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgEnabledInterface.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgEnabledInterface.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgEnabledInterface.Size = New System.Drawing.Size(184, 32)
        Me.INDRgEnabledInterface.StyleController = Me.LayoutControl1
        Me.INDRgEnabledInterface.TabIndex = 0
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(950, 574)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "Parametros"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyEnabledInterface, Me.INDLyMovementAccounting})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(484, 554)
        Me.LayoutControlGroup2.Text = "Parametros"
        '
        'INDLyEnabledInterface
        '
        Me.INDLyEnabledInterface.AllowHide = False
        Me.INDLyEnabledInterface.Control = Me.INDRgEnabledInterface
        Me.INDLyEnabledInterface.CustomizationFormText = "Activar interfaz con todos los módulos"
        Me.INDLyEnabledInterface.Location = New System.Drawing.Point(0, 0)
        Me.INDLyEnabledInterface.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDLyEnabledInterface.MinSize = New System.Drawing.Size(460, 36)
        Me.INDLyEnabledInterface.Name = "INDLyEnabledInterface"
        Me.INDLyEnabledInterface.ShowInCustomizationForm = False
        Me.INDLyEnabledInterface.Size = New System.Drawing.Size(460, 36)
        Me.INDLyEnabledInterface.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyEnabledInterface.Text = "Activar interfaz con todos los módulos"
        Me.INDLyEnabledInterface.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyEnabledInterface.TextSize = New System.Drawing.Size(260, 21)
        Me.INDLyEnabledInterface.TextToControlDistance = 12
        '
        'INDLyMovementAccounting
        '
        Me.INDLyMovementAccounting.AllowHide = False
        Me.INDLyMovementAccounting.Control = Me.INDRgMovementAccounting
        Me.INDLyMovementAccounting.CustomizationFormText = "Contabilizar Movimientos"
        Me.INDLyMovementAccounting.Location = New System.Drawing.Point(0, 36)
        Me.INDLyMovementAccounting.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDLyMovementAccounting.MinSize = New System.Drawing.Size(460, 36)
        Me.INDLyMovementAccounting.Name = "INDLyMovementAccounting"
        Me.INDLyMovementAccounting.ShowInCustomizationForm = False
        Me.INDLyMovementAccounting.Size = New System.Drawing.Size(460, 459)
        Me.INDLyMovementAccounting.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyMovementAccounting.Text = "Contabilizar Movimientos"
        Me.INDLyMovementAccounting.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyMovementAccounting.TextSize = New System.Drawing.Size(260, 21)
        Me.INDLyMovementAccounting.TextToControlDistance = 12
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "Proceso de Cierre Anual"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyCreateReserves, Me.INDLyCreatePayableAccounts, Me.INDLyCreateReceivableAccounts, Me.INDLyDocumentsGroupping})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(484, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(446, 554)
        Me.LayoutControlGroup3.Text = "Proceso de Cierre Anual"
        '
        'INDLyCreateReserves
        '
        Me.INDLyCreateReserves.AllowHide = False
        Me.INDLyCreateReserves.Control = Me.INDRgCreateReserves
        Me.INDLyCreateReserves.CustomizationFormText = "Crear Reservas"
        Me.INDLyCreateReserves.Location = New System.Drawing.Point(0, 0)
        Me.INDLyCreateReserves.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreateReserves.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreateReserves.Name = "INDLyCreateReserves"
        Me.INDLyCreateReserves.ShowInCustomizationForm = False
        Me.INDLyCreateReserves.Size = New System.Drawing.Size(422, 36)
        Me.INDLyCreateReserves.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyCreateReserves.Text = "Crear Reservas"
        Me.INDLyCreateReserves.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyCreateReserves.TextSize = New System.Drawing.Size(230, 21)
        Me.INDLyCreateReserves.TextToControlDistance = 12
        '
        'INDLyCreatePayableAccounts
        '
        Me.INDLyCreatePayableAccounts.AllowHide = False
        Me.INDLyCreatePayableAccounts.Control = Me.INDRgCreatePayableAccounts
        Me.INDLyCreatePayableAccounts.CustomizationFormText = "Crear Cuentas Por Cobrar"
        Me.INDLyCreatePayableAccounts.Location = New System.Drawing.Point(0, 36)
        Me.INDLyCreatePayableAccounts.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreatePayableAccounts.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreatePayableAccounts.Name = "INDLyCreatePayableAccounts"
        Me.INDLyCreatePayableAccounts.ShowInCustomizationForm = False
        Me.INDLyCreatePayableAccounts.Size = New System.Drawing.Size(422, 36)
        Me.INDLyCreatePayableAccounts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyCreatePayableAccounts.Text = "Crear Cuentas Por Cobrar"
        Me.INDLyCreatePayableAccounts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyCreatePayableAccounts.TextSize = New System.Drawing.Size(230, 21)
        Me.INDLyCreatePayableAccounts.TextToControlDistance = 12
        '
        'INDLyCreateReceivableAccounts
        '
        Me.INDLyCreateReceivableAccounts.AllowHide = False
        Me.INDLyCreateReceivableAccounts.Control = Me.INDRgCreateReceivableAccounts
        Me.INDLyCreateReceivableAccounts.CustomizationFormText = "Crear Cuentas Por Pagar"
        Me.INDLyCreateReceivableAccounts.Location = New System.Drawing.Point(0, 72)
        Me.INDLyCreateReceivableAccounts.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreateReceivableAccounts.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyCreateReceivableAccounts.Name = "INDLyCreateReceivableAccounts"
        Me.INDLyCreateReceivableAccounts.ShowInCustomizationForm = False
        Me.INDLyCreateReceivableAccounts.Size = New System.Drawing.Size(422, 36)
        Me.INDLyCreateReceivableAccounts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyCreateReceivableAccounts.Text = "Crear Cuentas Por Pagar"
        Me.INDLyCreateReceivableAccounts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyCreateReceivableAccounts.TextSize = New System.Drawing.Size(230, 21)
        Me.INDLyCreateReceivableAccounts.TextToControlDistance = 12
        '
        'INDLyDocumentsGroupping
        '
        Me.INDLyDocumentsGroupping.AllowHide = False
        Me.INDLyDocumentsGroupping.Control = Me.INDRgDocumentsGroupping
        Me.INDLyDocumentsGroupping.CustomizationFormText = "Agrupar Documentos Por Tercero"
        Me.INDLyDocumentsGroupping.Location = New System.Drawing.Point(0, 108)
        Me.INDLyDocumentsGroupping.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyDocumentsGroupping.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyDocumentsGroupping.Name = "INDLyDocumentsGroupping"
        Me.INDLyDocumentsGroupping.ShowInCustomizationForm = False
        Me.INDLyDocumentsGroupping.Size = New System.Drawing.Size(422, 387)
        Me.INDLyDocumentsGroupping.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDocumentsGroupping.Text = "Agrupar Documentos Por Tercero"
        Me.INDLyDocumentsGroupping.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyDocumentsGroupping.TextSize = New System.Drawing.Size(230, 21)
        Me.INDLyDocumentsGroupping.TextToControlDistance = 12
        '
        'FrmSettingBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 701)
        Me.Name = "FrmSettingBudget"
        Me.Opacity = 1.0R
        Me.Tag = ""
        Me.Text = "Parametros de Presupuesto"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDRgDocumentsGroupping.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgCreateReceivableAccounts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgCreatePayableAccounts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgCreateReserves.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgMovementAccounting.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgEnabledInterface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyEnabledInterface, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyMovementAccounting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCreateReserves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCreatePayableAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCreateReceivableAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDocumentsGroupping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDRgMovementAccounting As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDRgEnabledInterface As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyEnabledInterface As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyMovementAccounting As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRgCreateReserves As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyCreateReserves As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRgDocumentsGroupping As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDRgCreateReceivableAccounts As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDRgCreatePayableAccounts As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDLyCreatePayableAccounts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyCreateReceivableAccounts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDocumentsGroupping As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
End Class
