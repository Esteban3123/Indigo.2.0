Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMassiveExternalEntities  
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMassiveExternalEntities))
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbBills = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcInformation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEmpleado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNombreFondo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoFondo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFechaInicioFondo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAporteVoluntario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColValorAporteVoluntario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBanco = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCuentaBancaria = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoCuentaBancaria = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.ColCedulaEmpleado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCodigoFondo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCodigobanco = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.INDgcInformation,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDviewInfo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = true
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDBtnImportFile)
        Me.LayoutControl1.Controls.Add(Me.INDEsbBills)
        Me.LayoutControl1.Controls.Add(Me.INDgcInformation)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(435, 231, 421, 552)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"),System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(70, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, false)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(42, 32)
        Me.INDBtnImportFile.StyleController = Me.LayoutControl1
        Me.INDBtnImportFile.TabIndex = 22
        Me.INDBtnImportFile.Text = "SimpleButton1"
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbBills
        '
        Me.INDEsbBills.ImageOptions.Image = CType(resources.GetObject("INDEsbBills.ImageOptions.Image"),System.Drawing.Image)
        Me.INDEsbBills.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbBills.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDEsbBills, false)
        Me.INDEsbBills.Name = "INDEsbBills"
        Me.INDEsbBills.Size = New System.Drawing.Size(42, 32)
        Me.INDEsbBills.StyleController = Me.LayoutControl1
        Me.INDEsbBills.TabIndex = 17
        Me.INDEsbBills.Text = "ExportStructureButton3"
        Me.INDEsbBills.ToolTip = "Exportar Estructura"
        '
        'INDgcInformation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInformation, false)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInformation, true)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInformation, false)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInformation, false)
        Me.INDgcInformation.Location = New System.Drawing.Point(24, 95)
        Me.INDgcInformation.MainView = Me.INDviewInfo
        Me.INDgcInformation.Name = "INDgcInformation"
        Me.INDgcInformation.Size = New System.Drawing.Size(1213, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInformation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInformation.TabIndex = 4
        Me.INDgcInformation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInfo})
        '
        'INDviewInfo
        '
        Me.INDviewInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInfo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseFont = true
        Me.INDviewInfo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDviewInfo.Appearance.GroupRow.Options.UseFont = true
        Me.INDviewInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDviewInfo.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDviewInfo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInfo.Appearance.Row.Options.UseFont = true
        Me.INDviewInfo.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13!)
        Me.INDviewInfo.Appearance.ViewCaption.Options.UseFont = true
        Me.INDviewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEmpleado, Me.ColNombreFondo, Me.ColTipoFondo, Me.ColFechaInicioFondo, Me.ColAporteVoluntario, Me.ColValorAporteVoluntario, Me.ColBanco, Me.ColCuentaBancaria, Me.ColTipoCuentaBancaria, Me.ColCedulaEmpleado, Me.ColCodigoFondo, Me.ColCodigobanco})
        Me.INDviewInfo.GridControl = Me.INDgcInformation
        Me.INDviewInfo.Name = "INDviewInfo"
        Me.INDviewInfo.OptionsView.EnableAppearanceEvenRow = true
        Me.INDviewInfo.OptionsView.EnableAppearanceOddRow = true
        Me.INDviewInfo.OptionsView.ShowAutoFilterRow = true
        Me.INDviewInfo.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInfo, false)
        '
        'ColEmpleado
        '
        Me.ColEmpleado.AppearanceCell.Options.UseTextOptions = true
        Me.ColEmpleado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEmpleado.Caption = "Empleado"
        Me.ColEmpleado.FieldName = "Employee"
        Me.ColEmpleado.Name = "ColEmpleado"
        Me.ColEmpleado.OptionsColumn.AllowEdit = false
        Me.ColEmpleado.Visible = true
        Me.ColEmpleado.VisibleIndex = 1
        '
        'ColNombreFondo
        '
        Me.ColNombreFondo.Caption = "Nombre Fondo"
        Me.ColNombreFondo.FieldName = "FundName"
        Me.ColNombreFondo.Name = "ColNombreFondo"
        Me.ColNombreFondo.OptionsColumn.AllowFocus = false
        Me.ColNombreFondo.Visible = true
        Me.ColNombreFondo.VisibleIndex = 0
        '
        'ColTipoFondo
        '
        Me.ColTipoFondo.Caption = "Tipo Fondo"
        Me.ColTipoFondo.FieldName = "FundType"
        Me.ColTipoFondo.Name = "ColTipoFondo"
        Me.ColTipoFondo.OptionsColumn.AllowFocus = false
        Me.ColTipoFondo.Visible = true
        Me.ColTipoFondo.VisibleIndex = 2
        '
        'ColFechaInicioFondo
        '
        Me.ColFechaInicioFondo.Caption = "Fecha Inicio Fondo"
        Me.ColFechaInicioFondo.FieldName = "FundInitialDate"
        Me.ColFechaInicioFondo.Name = "ColFechaInicioFondo"
        Me.ColFechaInicioFondo.OptionsColumn.AllowFocus = false
        Me.ColFechaInicioFondo.Visible = true
        Me.ColFechaInicioFondo.VisibleIndex = 3
        '
        'ColAporteVoluntario
        '
        Me.ColAporteVoluntario.Caption = "Aporte Voluntario"
        Me.ColAporteVoluntario.FieldName = "VoluntaryContribution"
        Me.ColAporteVoluntario.Name = "ColAporteVoluntario"
        Me.ColAporteVoluntario.OptionsColumn.AllowFocus = false
        Me.ColAporteVoluntario.Visible = true
        Me.ColAporteVoluntario.VisibleIndex = 4
        '
        'ColValorAporteVoluntario
        '
        Me.ColValorAporteVoluntario.Caption = "Valor Aporte Voluntario"
        Me.ColValorAporteVoluntario.FieldName = "VoluntaryContributionValue"
        Me.ColValorAporteVoluntario.Name = "ColValorAporteVoluntario"
        Me.ColValorAporteVoluntario.OptionsColumn.AllowFocus = false
        Me.ColValorAporteVoluntario.Visible = true
        Me.ColValorAporteVoluntario.VisibleIndex = 5
        '
        'ColBanco
        '
        Me.ColBanco.Caption = "Banco"
        Me.ColBanco.FieldName = "Bankname"
        Me.ColBanco.Name = "ColBanco"
        Me.ColBanco.OptionsColumn.AllowFocus = false
        Me.ColBanco.Visible = true
        Me.ColBanco.VisibleIndex = 6
        '
        'ColCuentaBancaria
        '
        Me.ColCuentaBancaria.Caption = "#Cuenta Bancaria"
        Me.ColCuentaBancaria.FieldName = "BankAccountNumber"
        Me.ColCuentaBancaria.Name = "ColCuentaBancaria"
        Me.ColCuentaBancaria.OptionsColumn.AllowFocus = false
        Me.ColCuentaBancaria.Visible = true
        Me.ColCuentaBancaria.VisibleIndex = 7
        '
        'ColTipoCuentaBancaria
        '
        Me.ColTipoCuentaBancaria.Caption = "Tipo Cuenta Bancaria (1 - Ahorros, 2 - Corriente)"
        Me.ColTipoCuentaBancaria.FieldName = "BankAccountNumberType"
        Me.ColTipoCuentaBancaria.Name = "ColTipoCuentaBancaria"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, false)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1241, 550)
        Me.LayoutControlGroup2.Text = "Cargue masivo de entidades externes"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcInformation
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Empleados"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1217, 455)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = false
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDEsbBills
        Me.LayoutControlItem3.CustomizationFormText = "Exportar Estructura"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = false
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDBtnImportFile
        Me.LayoutControlItem4.CustomizationFormText = "Importar Información"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(46, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1171, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = false
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'ColCedulaEmpleado
        '
        Me.ColCedulaEmpleado.Caption = "Cedula Empleado"
        Me.ColCedulaEmpleado.FieldName = "Nit"
        Me.ColCedulaEmpleado.Name = "ColCedulaEmpleado"
        '
        'ColCodigoFondo
        '
        Me.ColCodigoFondo.Caption = "Codigo Fondo"
        Me.ColCodigoFondo.FieldName = "FundCode"
        Me.ColCodigoFondo.Name = "ColCodigoFondo"
        '
        'ColCodigobanco
        '
        Me.ColCodigobanco.Caption = "Codigo Banco"
        Me.ColCodigobanco.FieldName = "BankCode"
        Me.ColCodigobanco.Name = "ColCodigobanco"
        '
        'FrmMassiveExternalEntities
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmMassiveExternalEntities"
        Me.Opacity = 1R
        Me.Tag = "1971"
        Me.Text = "Cargue masivo de entidades externes"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDgcInformation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDviewInfo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcInformation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDEsbBills As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents ColEmpleado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNombreFondo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoFondo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFechaInicioFondo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAporteVoluntario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColValorAporteVoluntario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBanco As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCuentaBancaria As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoCuentaBancaria As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCedulaEmpleado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodigoFondo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodigobanco As DevExpress.XtraGrid.Columns.GridColumn
End Class
