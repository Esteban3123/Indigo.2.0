Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCausation
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCausation))
        Me.INDGcCausation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCausation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rptItemImageComboBoxState = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImgStates = New DevExpress.Utils.ImageCollection(Me.components)
        Me.ColProfessional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColContract = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColContractType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColProvider = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAdmission = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAdminEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColService = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColServiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTotalBilled = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCausationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCausedValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColServiceStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColInvoice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColErrors = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rptItemImageComboBoxState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgStates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1600, 500)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1600, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1600, 98)
        '
        'INDGcCausation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCausation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCausation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCausation, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCausation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCausation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCausation, False)
        Me.INDGcCausation.Location = New System.Drawing.Point(5, 5)
        Me.INDGcCausation.MainView = Me.INDGvCausation
        Me.INDGcCausation.Name = "INDGcCausation"
        Me.INDGcCausation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rptItemImageComboBoxState})
        Me.INDGcCausation.Size = New System.Drawing.Size(1586, 481)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCausation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCausation.TabIndex = 0
        Me.INDGcCausation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCausation})
        '
        'INDGvCausation
        '
        Me.INDGvCausation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCausation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCausation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCausation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCausation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCausation.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCausation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCausation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCausation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCausation.Appearance.Row.Options.UseFont = True
        Me.INDGvCausation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCausation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCausation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColState, Me.ColProfessional, Me.ColContract, Me.ColContractType, Me.ColProvider, Me.ColAdmission, Me.ColPatient, Me.ColCareGroup, Me.ColAdminEntity, Me.ColService, Me.ColQuantity, Me.ColServiceDate, Me.ColFunctionalUnit, Me.ColTotalBilled, Me.ColCausationDate, Me.ColCausedValue, Me.ColServiceStatus, Me.ColInvoice, Me.ColErrors})
        Me.INDGvCausation.GridControl = Me.INDGcCausation
        Me.INDGvCausation.Name = "INDGvCausation"
        Me.INDGvCausation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCausation.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCausation.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCausation.OptionsView.ShowFooter = True
        Me.INDGvCausation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCausation, False)
        '
        'ColState
        '
        Me.ColState.Caption = " "
        Me.ColState.ColumnEdit = Me.rptItemImageComboBoxState
        Me.ColState.FieldName = "StateOperation"
        Me.ColState.Name = "ColState"
        Me.ColState.OptionsColumn.AllowEdit = False
        Me.ColState.OptionsColumn.AllowFocus = False
        Me.ColState.OptionsColumn.AllowMove = False
        Me.ColState.OptionsColumn.AllowSize = False
        Me.ColState.OptionsColumn.FixedWidth = True
        Me.ColState.Visible = True
        Me.ColState.VisibleIndex = 0
        Me.ColState.Width = 26
        '
        'rptItemImageComboBoxState
        '
        Me.rptItemImageComboBoxState.Appearance.Options.UseTextOptions = True
        Me.rptItemImageComboBoxState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rptItemImageComboBoxState.AutoHeight = False
        Me.rptItemImageComboBoxState.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(1, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(2, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(3, Byte), 2)})
        Me.rptItemImageComboBoxState.LargeImages = Me.ImgStates
        Me.rptItemImageComboBoxState.Name = "rptItemImageComboBoxState"
        Me.rptItemImageComboBoxState.SmallImages = Me.ImgStates
        '
        'ImgStates
        '
        Me.ImgStates.ImageStream = CType(resources.GetObject("ImgStates.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImgStates.Images.SetKeyName(0, "Acceptance.png")
        Me.ImgStates.Images.SetKeyName(1, "carga.gif")
        Me.ImgStates.Images.SetKeyName(2, "cancelar16x16.png")
        '
        'ColProfessional
        '
        Me.ColProfessional.Caption = "Profesional"
        Me.ColProfessional.FieldName = "ProfessionalCodeName"
        Me.ColProfessional.Name = "ColProfessional"
        Me.ColProfessional.OptionsColumn.AllowEdit = False
        Me.ColProfessional.OptionsColumn.AllowFocus = False
        Me.ColProfessional.Visible = True
        Me.ColProfessional.VisibleIndex = 1
        Me.ColProfessional.Width = 150
        '
        'ColContract
        '
        Me.ColContract.Caption = "Contrato"
        Me.ColContract.FieldName = "ContractCodeName"
        Me.ColContract.Name = "ColContract"
        Me.ColContract.OptionsColumn.AllowEdit = False
        Me.ColContract.OptionsColumn.AllowFocus = False
        Me.ColContract.Visible = True
        Me.ColContract.VisibleIndex = 2
        Me.ColContract.Width = 150
        '
        'ColContractType
        '
        Me.ColContractType.Caption = "Tipo de Contrato"
        Me.ColContractType.FieldName = "ContractTypeName"
        Me.ColContractType.Name = "ColContractType"
        Me.ColContractType.OptionsColumn.AllowEdit = False
        Me.ColContractType.OptionsColumn.AllowFocus = False
        Me.ColContractType.Visible = True
        Me.ColContractType.VisibleIndex = 3
        Me.ColContractType.Width = 120
        '
        'ColProvider
        '
        Me.ColProvider.Caption = "Proveedor"
        Me.ColProvider.FieldName = "SupplierName"
        Me.ColProvider.Name = "ColProvider"
        Me.ColProvider.OptionsColumn.AllowEdit = False
        Me.ColProvider.OptionsColumn.AllowFocus = False
        Me.ColProvider.Visible = True
        Me.ColProvider.VisibleIndex = 4
        Me.ColProvider.Width = 150
        '
        'ColAdmission
        '
        Me.ColAdmission.Caption = "Ingreso/Admisión"
        Me.ColAdmission.FieldName = "AdmissionNumber"
        Me.ColAdmission.Name = "ColAdmission"
        Me.ColAdmission.OptionsColumn.AllowEdit = False
        Me.ColAdmission.OptionsColumn.AllowFocus = False
        Me.ColAdmission.Visible = True
        Me.ColAdmission.VisibleIndex = 5
        Me.ColAdmission.Width = 100
        '
        'ColPatient
        '
        Me.ColPatient.Caption = "Paciente"
        Me.ColPatient.FieldName = "PatientCodeName"
        Me.ColPatient.Name = "ColPatient"
        Me.ColPatient.OptionsColumn.AllowEdit = False
        Me.ColPatient.OptionsColumn.AllowFocus = False
        Me.ColPatient.Visible = True
        Me.ColPatient.VisibleIndex = 6
        Me.ColPatient.Width = 150
        '
        'ColCareGroup
        '
        Me.ColCareGroup.Caption = "Grupo de Atención"
        Me.ColCareGroup.FieldName = "CareGroupCodeName"
        Me.ColCareGroup.Name = "ColCareGroup"
        Me.ColCareGroup.OptionsColumn.AllowEdit = False
        Me.ColCareGroup.OptionsColumn.AllowFocus = False
        Me.ColCareGroup.Visible = True
        Me.ColCareGroup.VisibleIndex = 7
        Me.ColCareGroup.Width = 130
        '
        'ColAdminEntity
        '
        Me.ColAdminEntity.Caption = "Entidad Administradora"
        Me.ColAdminEntity.FieldName = "HealthAdministratorCodeName"
        Me.ColAdminEntity.Name = "ColAdminEntity"
        Me.ColAdminEntity.OptionsColumn.AllowEdit = False
        Me.ColAdminEntity.OptionsColumn.AllowFocus = False
        Me.ColAdminEntity.Visible = True
        Me.ColAdminEntity.VisibleIndex = 8
        Me.ColAdminEntity.Width = 150
        '
        'ColService
        '
        Me.ColService.Caption = "Servicio"
        Me.ColService.FieldName = "CupsEntityCodeName"
        Me.ColService.Name = "ColService"
        Me.ColService.OptionsColumn.AllowEdit = False
        Me.ColService.OptionsColumn.AllowFocus = False
        Me.ColService.Visible = True
        Me.ColService.VisibleIndex = 9
        Me.ColService.Width = 200
        '
        'ColQuantity
        '
        Me.ColQuantity.Caption = "Cantidad"
        Me.ColQuantity.DisplayFormat.FormatString = "N0"
        Me.ColQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColQuantity.FieldName = "InvoiceQuantity"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.OptionsColumn.AllowEdit = False
        Me.ColQuantity.OptionsColumn.AllowFocus = False
        Me.ColQuantity.Visible = True
        Me.ColQuantity.VisibleIndex = 10
        Me.ColQuantity.Width = 70
        '
        'ColServiceDate
        '
        Me.ColServiceDate.Caption = "Fecha Servicio"
        Me.ColServiceDate.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.ColServiceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColServiceDate.FieldName = "ServiceDate"
        Me.ColServiceDate.Name = "ColServiceDate"
        Me.ColServiceDate.OptionsColumn.AllowEdit = False
        Me.ColServiceDate.OptionsColumn.AllowFocus = False
        Me.ColServiceDate.Visible = True
        Me.ColServiceDate.VisibleIndex = 11
        Me.ColServiceDate.Width = 90
        '
        'ColFunctionalUnit
        '
        Me.ColFunctionalUnit.Caption = "Unidad Funcional del Servicio"
        Me.ColFunctionalUnit.FieldName = "FunctionalUnitCodeName"
        Me.ColFunctionalUnit.Name = "ColFunctionalUnit"
        Me.ColFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.ColFunctionalUnit.OptionsColumn.AllowFocus = False
        Me.ColFunctionalUnit.Visible = True
        Me.ColFunctionalUnit.VisibleIndex = 12
        Me.ColFunctionalUnit.Width = 150
        '
        'ColTotalBilled
        '
        Me.ColTotalBilled.Caption = "Total Facturado"
        Me.ColTotalBilled.DisplayFormat.FormatString = "C0"
        Me.ColTotalBilled.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColTotalBilled.FieldName = "TotalSalesPrice"
        Me.ColTotalBilled.Name = "ColTotalBilled"
        Me.ColTotalBilled.OptionsColumn.AllowEdit = False
        Me.ColTotalBilled.OptionsColumn.AllowFocus = False
        Me.ColTotalBilled.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalSalesPrice", "Total {0:C0}")})
        Me.ColTotalBilled.Visible = True
        Me.ColTotalBilled.VisibleIndex = 13
        Me.ColTotalBilled.Width = 110
        '
        'ColCausationDate
        '
        Me.ColCausationDate.Caption = "Fecha Causación"
        Me.ColCausationDate.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.ColCausationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColCausationDate.FieldName = "CausationDate"
        Me.ColCausationDate.Name = "ColCausationDate"
        Me.ColCausationDate.OptionsColumn.AllowEdit = False
        Me.ColCausationDate.OptionsColumn.AllowFocus = False
        Me.ColCausationDate.Visible = True
        Me.ColCausationDate.VisibleIndex = 14
        Me.ColCausationDate.Width = 90
        '
        'ColCausedValue
        '
        Me.ColCausedValue.Caption = "Valor Causado"
        Me.ColCausedValue.DisplayFormat.FormatString = "C0"
        Me.ColCausedValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColCausedValue.FieldName = "CausationValue"
        Me.ColCausedValue.Name = "ColCausedValue"
        Me.ColCausedValue.OptionsColumn.AllowEdit = False
        Me.ColCausedValue.OptionsColumn.AllowFocus = False
        Me.ColCausedValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CausationValue", "Total {0:C0}")})
        Me.ColCausedValue.Visible = True
        Me.ColCausedValue.VisibleIndex = 15
        Me.ColCausedValue.Width = 110
        '
        'ColServiceStatus
        '
        Me.ColServiceStatus.Caption = "Estado del Servicio"
        Me.ColServiceStatus.FieldName = "ServiceStatus"
        Me.ColServiceStatus.Name = "ColServiceStatus"
        Me.ColServiceStatus.OptionsColumn.AllowEdit = False
        Me.ColServiceStatus.OptionsColumn.AllowFocus = False
        Me.ColServiceStatus.Visible = True
        Me.ColServiceStatus.VisibleIndex = 16
        Me.ColServiceStatus.Width = 120
        '
        'ColInvoice
        '
        Me.ColInvoice.Caption = "Factura"
        Me.ColInvoice.FieldName = "InvoiceNumber"
        Me.ColInvoice.Name = "ColInvoice"
        Me.ColInvoice.OptionsColumn.AllowEdit = False
        Me.ColInvoice.OptionsColumn.AllowFocus = False
        Me.ColInvoice.Visible = True
        Me.ColInvoice.VisibleIndex = 17
        Me.ColInvoice.Width = 100
        '
        'ColErrors
        '
        Me.ColErrors.Caption = "Errores"
        Me.ColErrors.FieldName = "ErrorMessage"
        Me.ColErrors.Name = "ColErrors"
        Me.ColErrors.OptionsColumn.AllowEdit = False
        Me.ColErrors.OptionsColumn.AllowFocus = False
        Me.ColErrors.Visible = False
        Me.ColErrors.VisibleIndex = 18
        Me.ColErrors.Width = 200
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDGcCausation)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(1596, 491)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1596, 491)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCausation
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1596, 491)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmCausation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1600, 622)
        Me.Name = "FrmCausation"
        Me.Opacity = 1.0R
        Me.Tag = "2859"
        Me.Text = "Reconocimiento de Costo Proveedores de la Salud"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCausation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCausation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rptItemImageComboBoxState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgStates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDGcCausation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCausation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents ColState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColProfessional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColContract As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColContractType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColProvider As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAdmission As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAdminEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColService As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColServiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTotalBilled As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCausationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCausedValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColServiceStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColInvoice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColErrors As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents rptItemImageComboBoxState As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImgStates As DevExpress.Utils.ImageCollection
End Class

