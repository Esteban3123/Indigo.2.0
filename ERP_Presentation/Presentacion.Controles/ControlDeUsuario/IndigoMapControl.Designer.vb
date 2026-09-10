<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IndigoMapControl
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IndigoMapControl))
        Me.MapControl = New DevExpress.XtraMap.MapControl()
        Me.ImcLogo = New DevExpress.Utils.ImageCollection(Me.components)
        Me.PnlOptions = New DevExpress.XtraEditors.PanelControl()
        Me.LycOptions = New DevExpress.XtraLayout.LayoutControl()
        Me.BtnExportResultPoints = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.GdcResultPoints = New DevExpress.XtraGrid.GridControl()
        Me.GdvResultPoints = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GdcSelectedPointInfo = New DevExpress.XtraVerticalGrid.VGridControl()
        Me.BtnExportSelectedPoints = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.LblCountPoints = New DevExpress.XtraEditors.LabelControl()
        Me.ChkSelectPoints = New DevExpress.XtraEditors.CheckButton()
        Me.ChkShowPremises = New DevExpress.XtraEditors.CheckButton()
        Me.ChkShowCopyright = New DevExpress.XtraEditors.CheckButton()
        Me.TxtFindText = New DevExpress.XtraEditors.TextEdit()
        Me.LycgOptions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciSelectedPointInfo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciExportSelectedPoints = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciResultPoints = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciExportResultPoints = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.MapControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImcLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlOptions.SuspendLayout()
        CType(Me.LycOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LycOptions.SuspendLayout()
        CType(Me.GdcResultPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvResultPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdcSelectedPointInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFindText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciSelectedPointInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciExportSelectedPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciResultPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciExportResultPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MapControl
        '
        Me.MapControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MapControl.Location = New System.Drawing.Point(263, 0)
        Me.MapControl.Name = "MapControl"
        Me.MapControl.NavigationPanelOptions.Visible = False
        Me.MapControl.SelectionMode = DevExpress.XtraMap.ElementSelectionMode.[Single]
        Me.MapControl.Size = New System.Drawing.Size(176, 629)
        Me.MapControl.TabIndex = 0
        '
        'ImcLogo
        '
        Me.ImcLogo.ImageSize = New System.Drawing.Size(48, 48)
        Me.ImcLogo.ImageStream = CType(resources.GetObject("ImcLogo.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImcLogo.Images.SetKeyName(0, "Marca.png")
        '
        'PnlOptions
        '
        Me.PnlOptions.Controls.Add(Me.LycOptions)
        Me.PnlOptions.Dock = System.Windows.Forms.DockStyle.Left
        Me.PnlOptions.Location = New System.Drawing.Point(0, 0)
        Me.PnlOptions.Name = "PnlOptions"
        Me.PnlOptions.Size = New System.Drawing.Size(263, 629)
        Me.PnlOptions.TabIndex = 1
        '
        'LycOptions
        '
        Me.LycOptions.AllowCustomization = False
        Me.LycOptions.Controls.Add(Me.BtnExportResultPoints)
        Me.LycOptions.Controls.Add(Me.GdcResultPoints)
        Me.LycOptions.Controls.Add(Me.GdcSelectedPointInfo)
        Me.LycOptions.Controls.Add(Me.BtnExportSelectedPoints)
        Me.LycOptions.Controls.Add(Me.LblCountPoints)
        Me.LycOptions.Controls.Add(Me.ChkSelectPoints)
        Me.LycOptions.Controls.Add(Me.ChkShowPremises)
        Me.LycOptions.Controls.Add(Me.ChkShowCopyright)
        Me.LycOptions.Controls.Add(Me.TxtFindText)
        Me.LycOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LycOptions.Location = New System.Drawing.Point(2, 2)
        Me.LycOptions.Name = "LycOptions"
        Me.LycOptions.Root = Me.LycgOptions
        Me.LycOptions.Size = New System.Drawing.Size(259, 625)
        Me.LycOptions.TabIndex = 0
        Me.LycOptions.Text = "LayoutControl1"
        '
        'BtnExportResultPoints
        '
        Me.BtnExportResultPoints.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportResultPoints.Location = New System.Drawing.Point(14, 235)
        Me.BtnExportResultPoints.Name = "BtnExportResultPoints"
        Me.BtnExportResultPoints.Size = New System.Drawing.Size(231, 13)
        Me.BtnExportResultPoints.StyleController = Me.LycOptions
        Me.BtnExportResultPoints.TabIndex = 15
        Me.BtnExportResultPoints.Text = "Exportar Resultados"
        '
        'GdcResultPoints
        '
        Me.GdcResultPoints.Location = New System.Drawing.Point(14, 106)
        Me.GdcResultPoints.MainView = Me.GdvResultPoints
        Me.GdcResultPoints.Name = "GdcResultPoints"
        Me.GdcResultPoints.Size = New System.Drawing.Size(231, 125)
        Me.GdcResultPoints.TabIndex = 14
        Me.GdcResultPoints.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvResultPoints})
        '
        'GdvResultPoints
        '
        Me.GdvResultPoints.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdvResultPoints.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvResultPoints.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdvResultPoints.Appearance.Row.Options.UseFont = True
        Me.GdvResultPoints.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.GdvResultPoints.GridControl = Me.GdcResultPoints
        Me.GdvResultPoints.Name = "GdvResultPoints"
        Me.GdvResultPoints.OptionsBehavior.Editable = False
        Me.GdvResultPoints.OptionsBehavior.ReadOnly = True
        Me.GdvResultPoints.OptionsView.ShowAutoFilterRow = True
        Me.GdvResultPoints.OptionsView.ShowGroupPanel = False
        Me.GdvResultPoints.OptionsView.ShowIndicator = False
        '
        'GdcSelectedPointInfo
        '
        Me.GdcSelectedPointInfo.Appearance.RecordValue.Font = New System.Drawing.Font("Segoe UI Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdcSelectedPointInfo.Appearance.RecordValue.Options.UseFont = True
        Me.GdcSelectedPointInfo.Appearance.RecordValue.Options.UseTextOptions = True
        Me.GdcSelectedPointInfo.Appearance.RecordValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GdcSelectedPointInfo.Appearance.RowHeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdcSelectedPointInfo.Appearance.RowHeaderPanel.Options.UseFont = True
        Me.GdcSelectedPointInfo.Location = New System.Drawing.Point(14, 493)
        Me.GdcSelectedPointInfo.Name = "GdcSelectedPointInfo"
        Me.GdcSelectedPointInfo.OptionsBehavior.Editable = False
        Me.GdcSelectedPointInfo.Size = New System.Drawing.Size(231, 126)
        Me.GdcSelectedPointInfo.TabIndex = 12
        '
        'BtnExportSelectedPoints
        '
        Me.BtnExportSelectedPoints.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportSelectedPoints.Location = New System.Drawing.Point(14, 394)
        Me.BtnExportSelectedPoints.Name = "BtnExportSelectedPoints"
        Me.BtnExportSelectedPoints.Size = New System.Drawing.Size(231, 13)
        Me.BtnExportSelectedPoints.StyleController = Me.LycOptions
        Me.BtnExportSelectedPoints.TabIndex = 11
        Me.BtnExportSelectedPoints.Text = "Exportar Puntos Seleccionados"
        '
        'LblCountPoints
        '
        Me.LblCountPoints.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountPoints.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblCountPoints.Location = New System.Drawing.Point(149, 448)
        Me.LblCountPoints.Name = "LblCountPoints"
        Me.LblCountPoints.Size = New System.Drawing.Size(96, 20)
        Me.LblCountPoints.StyleController = Me.LycOptions
        Me.LblCountPoints.TabIndex = 9
        Me.LblCountPoints.Text = "0"
        '
        'ChkSelectPoints
        '
        Me.ChkSelectPoints.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSelectPoints.Appearance.Options.UseFont = True
        Me.ChkSelectPoints.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkSelectPoints.Image = CType(resources.GetObject("ChkSelectPoints.Image"), System.Drawing.Image)
        Me.ChkSelectPoints.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.ChkSelectPoints.Location = New System.Drawing.Point(159, 362)
        Me.ChkSelectPoints.Name = "ChkSelectPoints"
        Me.ChkSelectPoints.Size = New System.Drawing.Size(41, 28)
        Me.ChkSelectPoints.StyleController = Me.LycOptions
        Me.ChkSelectPoints.TabIndex = 7
        Me.ChkSelectPoints.ToolTip = "Inicia la herramienta para seleccionar un área en el mapa"
        Me.ChkSelectPoints.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.ChkSelectPoints.ToolTipTitle = "Área"
        '
        'ChkShowPremises
        '
        Me.ChkShowPremises.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowPremises.Appearance.Options.UseFont = True
        Me.ChkShowPremises.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkShowPremises.Image = CType(resources.GetObject("ChkShowPremises.Image"), System.Drawing.Image)
        Me.ChkShowPremises.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.ChkShowPremises.Location = New System.Drawing.Point(159, 330)
        Me.ChkShowPremises.Name = "ChkShowPremises"
        Me.ChkShowPremises.Size = New System.Drawing.Size(41, 28)
        Me.ChkShowPremises.StyleController = Me.LycOptions
        Me.ChkShowPremises.TabIndex = 6
        Me.ChkShowPremises.ToolTip = "Mostrar la ubicación de los puntos"
        Me.ChkShowPremises.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.ChkShowPremises.ToolTipTitle = "Puntos"
        '
        'ChkShowCopyright
        '
        Me.ChkShowCopyright.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowCopyright.Appearance.Options.UseFont = True
        Me.ChkShowCopyright.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkShowCopyright.Image = CType(resources.GetObject("ChkShowCopyright.Image"), System.Drawing.Image)
        Me.ChkShowCopyright.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.ChkShowCopyright.Location = New System.Drawing.Point(159, 298)
        Me.ChkShowCopyright.Name = "ChkShowCopyright"
        Me.ChkShowCopyright.Size = New System.Drawing.Size(41, 28)
        Me.ChkShowCopyright.StyleController = Me.LycOptions
        Me.ChkShowCopyright.TabIndex = 5
        Me.ChkShowCopyright.ToolTip = "Mostrar la ubicación de las oficinas Indigo Technologies"
        Me.ChkShowCopyright.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.ChkShowCopyright.ToolTipTitle = "Oficinas Indigo"
        '
        'TxtFindText
        '
        Me.TxtFindText.Location = New System.Drawing.Point(14, 57)
        Me.TxtFindText.Name = "TxtFindText"
        Me.TxtFindText.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFindText.Properties.Appearance.Options.UseFont = True
        Me.TxtFindText.Size = New System.Drawing.Size(231, 24)
        Me.TxtFindText.StyleController = Me.LycOptions
        Me.TxtFindText.TabIndex = 4
        '
        'LycgOptions
        '
        Me.LycgOptions.AllowCustomizeChildren = False
        Me.LycgOptions.AllowHide = False
        Me.LycgOptions.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgOptions.GroupBordersVisible = False
        Me.LycgOptions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LycgOptions.Location = New System.Drawing.Point(0, 0)
        Me.LycgOptions.Name = "LycgOptions"
        Me.LycgOptions.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgOptions.ShowInCustomizationForm = False
        Me.LycgOptions.Size = New System.Drawing.Size(259, 625)
        Me.LycgOptions.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.LyciSelectedPointInfo})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 421)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(259, 204)
        Me.LayoutControlGroup1.Text = "Información General"
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.Control = Me.LblCountPoints
        Me.LayoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(172, 24)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(172, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(235, 24)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "No. Puntos:"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(67, 20)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LyciSelectedPointInfo
        '
        Me.LyciSelectedPointInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyciSelectedPointInfo.AppearanceItemCaption.Options.UseFont = True
        Me.LyciSelectedPointInfo.Control = Me.GdcSelectedPointInfo
        Me.LyciSelectedPointInfo.Location = New System.Drawing.Point(0, 24)
        Me.LyciSelectedPointInfo.MaxSize = New System.Drawing.Size(235, 151)
        Me.LyciSelectedPointInfo.MinSize = New System.Drawing.Size(235, 151)
        Me.LyciSelectedPointInfo.Name = "LyciSelectedPointInfo"
        Me.LyciSelectedPointInfo.Size = New System.Drawing.Size(235, 152)
        Me.LyciSelectedPointInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciSelectedPointInfo.Text = "Punto Seleccionado"
        Me.LyciSelectedPointInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LyciSelectedPointInfo.TextLocation = DevExpress.Utils.Locations.Top
        Me.LyciSelectedPointInfo.TextSize = New System.Drawing.Size(50, 20)
        Me.LyciSelectedPointInfo.TextToControlDistance = 1
        Me.LyciSelectedPointInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlItem2, Me.LyciExportSelectedPoints})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 262)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(259, 159)
        Me.LayoutControlGroup2.Text = "Opciones & Herramientas"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.ChkSelectPoints
        Me.LayoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(235, 32)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Seleccionar Área"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(140, 0)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.ChkShowPremises
        Me.LayoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(235, 32)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Mostrar Otros Puntos"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(140, 0)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.ChkShowCopyright
        Me.LayoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(190, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(235, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Mostrar Puntos Indigo"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(140, 0)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LyciExportSelectedPoints
        '
        Me.LyciExportSelectedPoints.Control = Me.BtnExportSelectedPoints
        Me.LyciExportSelectedPoints.Location = New System.Drawing.Point(0, 96)
        Me.LyciExportSelectedPoints.MaxSize = New System.Drawing.Size(235, 17)
        Me.LyciExportSelectedPoints.MinSize = New System.Drawing.Size(235, 17)
        Me.LyciExportSelectedPoints.Name = "LyciExportSelectedPoints"
        Me.LyciExportSelectedPoints.Size = New System.Drawing.Size(235, 17)
        Me.LyciExportSelectedPoints.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciExportSelectedPoints.TextSize = New System.Drawing.Size(0, 0)
        Me.LyciExportSelectedPoints.TextVisible = False
        Me.LyciExportSelectedPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LyciResultPoints, Me.LyciExportResultPoints})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(259, 262)
        Me.LayoutControlGroup3.Text = "Panel de Búsqueda"
        Me.LayoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.TxtFindText
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(235, 49)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(235, 49)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(235, 49)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Buscar"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem1.TextToControlDistance = 1
        '
        'LyciResultPoints
        '
        Me.LyciResultPoints.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyciResultPoints.AppearanceItemCaption.Options.UseFont = True
        Me.LyciResultPoints.Control = Me.GdcResultPoints
        Me.LyciResultPoints.Location = New System.Drawing.Point(0, 49)
        Me.LyciResultPoints.MaxSize = New System.Drawing.Size(235, 150)
        Me.LyciResultPoints.MinSize = New System.Drawing.Size(235, 150)
        Me.LyciResultPoints.Name = "LyciResultPoints"
        Me.LyciResultPoints.Size = New System.Drawing.Size(235, 150)
        Me.LyciResultPoints.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciResultPoints.Text = "Resultado de la Búsqueda"
        Me.LyciResultPoints.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LyciResultPoints.TextLocation = DevExpress.Utils.Locations.Top
        Me.LyciResultPoints.TextSize = New System.Drawing.Size(50, 20)
        Me.LyciResultPoints.TextToControlDistance = 1
        Me.LyciResultPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LyciExportResultPoints
        '
        Me.LyciExportResultPoints.Control = Me.BtnExportResultPoints
        Me.LyciExportResultPoints.Location = New System.Drawing.Point(0, 199)
        Me.LyciExportResultPoints.MaxSize = New System.Drawing.Size(235, 17)
        Me.LyciExportResultPoints.MinSize = New System.Drawing.Size(235, 17)
        Me.LyciExportResultPoints.Name = "LyciExportResultPoints"
        Me.LyciExportResultPoints.Size = New System.Drawing.Size(235, 17)
        Me.LyciExportResultPoints.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciExportResultPoints.TextSize = New System.Drawing.Size(0, 0)
        Me.LyciExportResultPoints.TextVisible = False
        Me.LyciExportResultPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoMapControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MapControl)
        Me.Controls.Add(Me.PnlOptions)
        Me.MinimumSize = New System.Drawing.Size(439, 629)
        Me.Name = "IndigoMapControl"
        Me.Size = New System.Drawing.Size(439, 629)
        CType(Me.MapControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImcLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlOptions.ResumeLayout(False)
        CType(Me.LycOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LycOptions.ResumeLayout(False)
        CType(Me.GdcResultPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvResultPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdcSelectedPointInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFindText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgOptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciSelectedPointInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciExportSelectedPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciResultPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciExportResultPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImcLogo As DevExpress.Utils.ImageCollection
    Private WithEvents MapControl As DevExpress.XtraMap.MapControl
    Private WithEvents PnlOptions As DevExpress.XtraEditors.PanelControl
    Private WithEvents LycOptions As DevExpress.XtraLayout.LayoutControl
    Private WithEvents LycgOptions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TxtFindText As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LblCountPoints As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ChkSelectPoints As DevExpress.XtraEditors.CheckButton
    Friend WithEvents ChkShowPremises As DevExpress.XtraEditors.CheckButton
    Friend WithEvents ChkShowCopyright As DevExpress.XtraEditors.CheckButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtnExportSelectedPoints As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents LyciExportSelectedPoints As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GdcSelectedPointInfo As DevExpress.XtraVerticalGrid.VGridControl
    Friend WithEvents LyciSelectedPointInfo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtnExportResultPoints As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents GdcResultPoints As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvResultPoints As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LyciResultPoints As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LyciExportResultPoints As DevExpress.XtraLayout.LayoutControlItem

End Class
