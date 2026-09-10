Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmINSReport
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmINSReport))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDBtnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcAutoliquidation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcListEmployee = New DevExpress.XtraGrid.GridControl()
        Me.GridView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColIdentificationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIdNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCCSSCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEmployeeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNacionalityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIngress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDaysWorked = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColINSCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSlCompany = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnProccess = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSlWorkCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvWorkCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlPeriodDate = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvDateLiquidated = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolDate1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcExportExcel = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtPolicyNumber = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciImportButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcMain = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton2 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSimpleButton3 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit13 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit14 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit15 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit16 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit17 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit18 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit19 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcAutoliquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcAutoliquidation.SuspendLayout()
        CType(Me.INDGcListEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlWorkCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWorkCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlPeriodDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDateLiquidated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPolicyNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciImportButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.AutoSize = True
        Me.INDPanelControlBase.Controls.Add(Me.INDLcAutoliquidation)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1451, 559)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1451, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDBtnExport
        '
        Me.INDBtnExport.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDBtnExport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.INDBtnExport.Location = New System.Drawing.Point(438, 56)
        Me.INDBtnExport.MaximumSize = New System.Drawing.Size(41, 33)
        Me.INDBtnExport.MinimumSize = New System.Drawing.Size(41, 33)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.INDBtnExport.Name = "INDBtnExport"
        Me.INDBtnExport.Size = New System.Drawing.Size(41, 33)
        Me.INDBtnExport.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnExport.TabIndex = 17
        Me.INDBtnExport.ToolTip = "Exportar Archivo"
        '
        'INDLcAutoliquidation
        '
        Me.INDLcAutoliquidation.Controls.Add(Me.INDGcListEmployee)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnExport)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnImportFile)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlCompany)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnProccess)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlWorkCenter)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlPeriodDate)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDGcExportExcel)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDtxtPolicyNumber)
        Me.INDLcAutoliquidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcAutoliquidation.Location = New System.Drawing.Point(202, 7)
        Me.INDLcAutoliquidation.Name = "INDLcAutoliquidation"
        Me.INDLcAutoliquidation.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(696, 301, 574, 569)
        Me.INDLcAutoliquidation.Root = Me.Root
        Me.INDLcAutoliquidation.Size = New System.Drawing.Size(1247, 550)
        Me.INDLcAutoliquidation.TabIndex = 1
        Me.INDLcAutoliquidation.Text = "INDLcAutoliquidation"
        '
        'INDGcListEmployee
        '
        Me.INDGcMain.SetAddActions(Me.INDGcListEmployee, Nothing)
        Me.INDGcMain.SetControlNextFocus(Me.INDGcListEmployee, Nothing)
        Me.INDGcMain.SetExportButton(Me.INDGcListEmployee, False)
        Me.INDGcMain.SetGuardarXml(Me.INDGcListEmployee, True)
        Me.INDGcMain.SetHoldSize(Me.INDGcListEmployee, False)
        Me.INDGcMain.SetHotTrack(Me.INDGcListEmployee, False)
        Me.INDGcListEmployee.Location = New System.Drawing.Point(438, 99)
        Me.INDGcListEmployee.MainView = Me.GridView11
        Me.INDGcListEmployee.Name = "INDGcListEmployee"
        Me.INDGcListEmployee.Size = New System.Drawing.Size(1196, 410)
        Me.INDGcMain.SetSizeConstraintsType(Me.INDGcListEmployee, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcListEmployee.TabIndex = 12
        Me.INDGcListEmployee.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView11})
        '
        'GridView11
        '
        Me.GridView11.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView11.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView11.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView11.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView11.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView11.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView11.Appearance.GroupRow.Options.UseFont = True
        Me.GridView11.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView11.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView11.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView11.Appearance.Row.Options.UseFont = True
        Me.GridView11.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView11.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView11.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColIdentificationType, Me.INDColIdNumber, Me.INDColCCSSCode, Me.INDColEmployeeName, Me.INDColNacionalityCode, Me.INDColIngress, Me.INDColDaysWorked, Me.INDColINSCode})
        Me.GridView11.GridControl = Me.INDGcListEmployee
        Me.GridView11.Name = "GridView11"
        Me.GridView11.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView11.OptionsView.EnableAppearanceOddRow = True
        Me.GridView11.OptionsView.ShowAutoFilterRow = True
        Me.GridView11.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView11, False)
        '
        'INDColIdentificationType
        '
        Me.INDColIdentificationType.Caption = "Tipo de Identificación"
        Me.INDColIdentificationType.FieldName = "IdentificationType"
        Me.INDColIdentificationType.MinWidth = 21
        Me.INDColIdentificationType.Name = "INDColIdentificationType"
        Me.INDColIdentificationType.OptionsColumn.AllowEdit = False
        Me.INDColIdentificationType.OptionsColumn.AllowFocus = False
        Me.INDColIdentificationType.Visible = True
        Me.INDColIdentificationType.VisibleIndex = 0
        '
        'INDColIdNumber
        '
        Me.INDColIdNumber.Caption = "No. Identificación"
        Me.INDColIdNumber.FieldName = "IdNumber"
        Me.INDColIdNumber.Name = "INDColIdNumber"
        Me.INDColIdNumber.OptionsColumn.AllowEdit = False
        Me.INDColIdNumber.Visible = True
        Me.INDColIdNumber.VisibleIndex = 1
        Me.INDColIdNumber.Width = 129
        '
        'INDColCCSSCode
        '
        Me.INDColCCSSCode.Caption = "Código de asegurado"
        Me.INDColCCSSCode.FieldName = "CCSSCode"
        Me.INDColCCSSCode.MinWidth = 21
        Me.INDColCCSSCode.Name = "INDColCCSSCode"
        Me.INDColCCSSCode.OptionsColumn.AllowEdit = False
        Me.INDColCCSSCode.Visible = True
        Me.INDColCCSSCode.VisibleIndex = 2
        '
        'INDColEmployeeName
        '
        Me.INDColEmployeeName.Caption = "Nombre"
        Me.INDColEmployeeName.FieldName = "NameEmployee"
        Me.INDColEmployeeName.Name = "INDColEmployeeName"
        Me.INDColEmployeeName.OptionsColumn.AllowEdit = False
        Me.INDColEmployeeName.Visible = True
        Me.INDColEmployeeName.VisibleIndex = 3
        Me.INDColEmployeeName.Width = 129
        '
        'INDColNacionalityCode
        '
        Me.INDColNacionalityCode.Caption = "Código Nacionalidad"
        Me.INDColNacionalityCode.FieldName = "NationalityCode"
        Me.INDColNacionalityCode.Name = "INDColNacionalityCode"
        Me.INDColNacionalityCode.Visible = True
        Me.INDColNacionalityCode.VisibleIndex = 4
        '
        'INDColIngress
        '
        Me.INDColIngress.Caption = "Monto Salario"
        Me.INDColIngress.DisplayFormat.FormatString = "c2"
        Me.INDColIngress.FieldName = "IBCHealth"
        Me.INDColIngress.Name = "INDColIngress"
        Me.INDColIngress.OptionsColumn.AllowEdit = False
        Me.INDColIngress.Visible = True
        Me.INDColIngress.VisibleIndex = 5
        Me.INDColIngress.Width = 66
        '
        'INDColDaysWorked
        '
        Me.INDColDaysWorked.Caption = "Días laborados"
        Me.INDColDaysWorked.FieldName = "HealthDays"
        Me.INDColDaysWorked.MinWidth = 21
        Me.INDColDaysWorked.Name = "INDColDaysWorked"
        Me.INDColDaysWorked.OptionsColumn.AllowEdit = False
        Me.INDColDaysWorked.Visible = True
        Me.INDColDaysWorked.VisibleIndex = 6
        '
        'INDColINSCode
        '
        Me.INDColINSCode.Caption = "Ocupación"
        Me.INDColINSCode.FieldName = "INSCode"
        Me.INDColINSCode.MinWidth = 21
        Me.INDColINSCode.Name = "INDColINSCode"
        Me.INDColINSCode.OptionsColumn.AllowEdit = False
        Me.INDColINSCode.OptionsColumn.AllowFocus = False
        Me.INDColINSCode.Visible = True
        Me.INDColINSCode.VisibleIndex = 7
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(483, 56)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(41, 33)
        Me.INDBtnImportFile.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnImportFile.TabIndex = 23
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDSlCompany
        '
        Me.INDSlCompany.EnterMoveNextControl = True
        Me.INDSlCompany.Location = New System.Drawing.Point(24, 73)
        Me.INDSlCompany.Name = "INDSlCompany"
        Me.INDSlCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlCompany.Properties.Appearance.Options.UseFont = True
        Me.INDSlCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlCompany.Properties.DisplayMember = "Name"
        Me.INDSlCompany.Properties.NullText = ""
        Me.INDSlCompany.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSlCompany.Properties.ValueMember = "Id"
        Me.INDSlCompany.Size = New System.Drawing.Size(386, 28)
        Me.INDSlCompany.StyleController = Me.INDLcAutoliquidation
        Me.INDSlCompany.TabIndex = 4
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNit, Me.INDColName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDColNit
        '
        Me.INDColNit.Caption = "Código"
        Me.INDColNit.FieldName = "Nit"
        Me.INDColNit.Name = "INDColNit"
        Me.INDColNit.OptionsColumn.AllowEdit = False
        Me.INDColNit.Visible = True
        Me.INDColNit.VisibleIndex = 0
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        '
        'INDBtnProccess
        '
        Me.INDBtnProccess.Location = New System.Drawing.Point(24, 293)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.INDBtnProccess.Name = "INDBtnProccess"
        Me.INDBtnProccess.Size = New System.Drawing.Size(386, 32)
        Me.INDBtnProccess.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnProccess.TabIndex = 8
        Me.INDBtnProccess.Text = "Procesar"
        '
        'INDSlWorkCenter
        '
        Me.INDSlWorkCenter.EnterMoveNextControl = True
        Me.INDSlWorkCenter.Location = New System.Drawing.Point(24, 193)
        Me.INDSlWorkCenter.Name = "INDSlWorkCenter"
        Me.INDSlWorkCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlWorkCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSlWorkCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlWorkCenter.Properties.DisplayMember = "Name"
        Me.INDSlWorkCenter.Properties.NullText = ""
        Me.INDSlWorkCenter.Properties.PopupView = Me.INDGvWorkCenter
        Me.INDSlWorkCenter.Properties.ValueMember = "Id"
        Me.INDSlWorkCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDSlWorkCenter.StyleController = Me.INDLcAutoliquidation
        Me.INDSlWorkCenter.TabIndex = 6
        '
        'INDGvWorkCenter
        '
        Me.INDGvWorkCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWorkCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWorkCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWorkCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWorkCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWorkCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWorkCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWorkCenter.Appearance.Row.Options.UseFont = True
        Me.INDGvWorkCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.INDGvWorkCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvWorkCenter.Name = "INDGvWorkCenter"
        Me.INDGvWorkCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvWorkCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWorkCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWorkCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWorkCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWorkCenter, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 50
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'INDSlPeriodDate
        '
        Me.INDSlPeriodDate.Location = New System.Drawing.Point(24, 253)
        Me.INDSlPeriodDate.Name = "INDSlPeriodDate"
        Me.INDSlPeriodDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlPeriodDate.Properties.Appearance.Options.UseFont = True
        Me.INDSlPeriodDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlPeriodDate.Properties.DisplayMember = "PayrollDateLiquidated"
        Me.INDSlPeriodDate.Properties.NullText = ""
        Me.INDSlPeriodDate.Properties.PopupView = Me.INDGvDateLiquidated
        Me.INDSlPeriodDate.Properties.ValueMember = "PayrollDateLiquidated"
        Me.INDSlPeriodDate.Size = New System.Drawing.Size(386, 28)
        Me.INDSlPeriodDate.StyleController = Me.INDLcAutoliquidation
        Me.INDSlPeriodDate.TabIndex = 7
        '
        'INDGvDateLiquidated
        '
        Me.INDGvDateLiquidated.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDateLiquidated.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDateLiquidated.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDateLiquidated.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDateLiquidated.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDateLiquidated.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDateLiquidated.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDateLiquidated.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDateLiquidated.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDateLiquidated.Appearance.Row.Options.UseFont = True
        Me.INDGvDateLiquidated.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolDate1})
        Me.INDGvDateLiquidated.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvDateLiquidated.Name = "INDGvDateLiquidated"
        Me.INDGvDateLiquidated.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvDateLiquidated.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDateLiquidated.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDateLiquidated.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDateLiquidated.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDateLiquidated, False)
        '
        'INDcolDate1
        '
        Me.INDcolDate1.Caption = "Fecha"
        Me.INDcolDate1.Name = "INDcolDate1"
        Me.INDcolDate1.OptionsColumn.AllowEdit = False
        Me.INDcolDate1.Visible = True
        Me.INDcolDate1.VisibleIndex = 0
        '
        'INDGcExportExcel
        '
        Me.INDGcMain.SetAddActions(Me.INDGcExportExcel, Nothing)
        Me.INDGcMain.SetControlNextFocus(Me.INDGcExportExcel, Nothing)
        Me.INDGcMain.SetExportButton(Me.INDGcExportExcel, False)
        Me.INDGcMain.SetGuardarXml(Me.INDGcExportExcel, True)
        Me.INDGcMain.SetHoldSize(Me.INDGcExportExcel, False)
        Me.INDGcMain.SetHotTrack(Me.INDGcExportExcel, False)
        Me.INDGcExportExcel.Location = New System.Drawing.Point(24, 329)
        Me.INDGcExportExcel.MainView = Me.GridView4
        Me.INDGcExportExcel.Name = "INDGcExportExcel"
        Me.INDGcExportExcel.Size = New System.Drawing.Size(386, 180)
        Me.INDGcMain.SetSizeConstraintsType(Me.INDGcExportExcel, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExportExcel.TabIndex = 18
        Me.INDGcExportExcel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView4.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView4.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn68, Me.GridColumn69, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn70, Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn71, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn72, Me.GridColumn73, Me.GridColumn28, Me.GridColumn29, Me.GridColumn30, Me.GridColumn74, Me.GridColumn31, Me.GridColumn32, Me.GridColumn33, Me.GridColumn34, Me.GridColumn75, Me.GridColumn35, Me.GridColumn36, Me.GridColumn37, Me.GridColumn38, Me.GridColumn39, Me.GridColumn40, Me.GridColumn41, Me.GridColumn42, Me.GridColumn43, Me.GridColumn44, Me.GridColumn45, Me.GridColumn46, Me.GridColumn47, Me.GridColumn48, Me.GridColumn49, Me.GridColumn50, Me.GridColumn51, Me.GridColumn52, Me.GridColumn53, Me.GridColumn54, Me.GridColumn55, Me.GridColumn57, Me.GridColumn58, Me.GridColumn59, Me.GridColumn60, Me.GridColumn61, Me.GridColumn62, Me.GridColumn63, Me.GridColumn64, Me.GridColumn65, Me.GridColumn66, Me.GridColumn56})
        Me.GridView4.GridControl = Me.INDGcExportExcel
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'GridColumn68
        '
        Me.GridColumn68.Caption = "Id"
        Me.GridColumn68.FieldName = "Id"
        Me.GridColumn68.Name = "GridColumn68"
        Me.GridColumn68.Visible = True
        Me.GridColumn68.VisibleIndex = 0
        '
        'GridColumn69
        '
        Me.GridColumn69.Caption = "Tipo Documento"
        Me.GridColumn69.FieldName = "IdentificationType"
        Me.GridColumn69.Name = "GridColumn69"
        Me.GridColumn69.Visible = True
        Me.GridColumn69.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Numero Identificación"
        Me.GridColumn5.DisplayFormat.FormatString = "G"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "Nit"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tipo Cotizante"
        Me.GridColumn6.FieldName = "TypeContractEmployee"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Empleado"
        Me.GridColumn7.FieldName = "NameEmployee"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 4
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Ingreso"
        Me.GridColumn8.FieldName = "Entry"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 5
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Fecha Ingreso"
        Me.GridColumn9.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "IngressDate"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 6
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Retiro"
        Me.GridColumn10.FieldName = "Retirement"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 7
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Fecha Retiro"
        Me.GridColumn11.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn11.FieldName = "DateRetirement"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 8
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "IBC Otros Parafiscales"
        Me.GridColumn12.FieldName = "IBCOtrosParafiscales"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 9
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "VSP"
        Me.GridColumn13.FieldName = "VSP"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 10
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Fecha Inicio VSP"
        Me.GridColumn14.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn14.FieldName = "FechaInicioVSP"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 11
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Valor VSP"
        Me.GridColumn15.FieldName = "ValueVSP"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 12
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "VST"
        Me.GridColumn16.FieldName = "VST"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 13
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "SLN"
        Me.GridColumn17.FieldName = "SLN"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 14
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Fecha Inicio SLN"
        Me.GridColumn18.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn18.FieldName = "SanctionInitialDate"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 15
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Fecha Final SLN"
        Me.GridColumn19.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn19.FieldName = "SanctionEndDate"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 16
        '
        'GridColumn70
        '
        Me.GridColumn70.Caption = "Base Calculo SLN"
        Me.GridColumn70.FieldName = "BaseLiquidacionSLN"
        Me.GridColumn70.Name = "GridColumn70"
        Me.GridColumn70.Visible = True
        Me.GridColumn70.VisibleIndex = 17
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "IBC SLN"
        Me.GridColumn20.FieldName = "IBCSLN"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 18
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "IGE"
        Me.GridColumn21.FieldName = "IGE"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 19
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Fecha Inicio IGE"
        Me.GridColumn22.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn22.FieldName = "AmbulatoryDisabilityInitialDate"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 20
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Fecha Final IGE"
        Me.GridColumn23.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn23.FieldName = "AmbulatoryDisabiltyEndDate"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 21
        '
        'GridColumn71
        '
        Me.GridColumn71.Caption = "Base Calculo IGE"
        Me.GridColumn71.FieldName = "BaseLiquidacionIGE"
        Me.GridColumn71.Name = "GridColumn71"
        Me.GridColumn71.Visible = True
        Me.GridColumn71.VisibleIndex = 22
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Base IGE"
        Me.GridColumn24.FieldName = "BaseIGE"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 23
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "LMA"
        Me.GridColumn25.FieldName = "LMA"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 24
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Fecha Inicio LMA"
        Me.GridColumn26.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn26.FieldName = "MaternityLeaveInitialDate"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 25
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Fecha Fin LMA"
        Me.GridColumn27.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn27.FieldName = "MaternityLeaveEndDate"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 26
        '
        'GridColumn72
        '
        Me.GridColumn72.Caption = "Base Calculo LMA"
        Me.GridColumn72.FieldName = "BaseLiquidacionLMA"
        Me.GridColumn72.Name = "GridColumn72"
        Me.GridColumn72.Visible = True
        Me.GridColumn72.VisibleIndex = 27
        '
        'GridColumn73
        '
        Me.GridColumn73.Caption = "IBC LMA"
        Me.GridColumn73.FieldName = "BaseLMA"
        Me.GridColumn73.Name = "GridColumn73"
        Me.GridColumn73.Visible = True
        Me.GridColumn73.VisibleIndex = 28
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "VAC"
        Me.GridColumn28.FieldName = "VAC"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 29
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Fecha Inicio Vacaciones"
        Me.GridColumn29.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn29.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn29.FieldName = "VacationInitialDate"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 30
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Fecha Fin Vacaciones"
        Me.GridColumn30.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn30.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn30.FieldName = "VacationEndDate"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 31
        '
        'GridColumn74
        '
        Me.GridColumn74.Caption = "Base Calculo VAC"
        Me.GridColumn74.FieldName = "BaseLiquidacionVAC"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.Visible = True
        Me.GridColumn74.VisibleIndex = 32
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Base Liq VAC"
        Me.GridColumn31.FieldName = "BaseVAC"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 33
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "IRL"
        Me.GridColumn32.FieldName = "IRL"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 34
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Fecha Inicio IRL"
        Me.GridColumn33.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn33.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn33.FieldName = "FechaInicioIRL"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 35
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "Fecha Fin IRL"
        Me.GridColumn34.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.GridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn34.FieldName = "FechaFinIRL"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Visible = True
        Me.GridColumn34.VisibleIndex = 36
        '
        'GridColumn75
        '
        Me.GridColumn75.Caption = "Base Calculo IRL"
        Me.GridColumn75.FieldName = "BaseLiquidacionIRL"
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.Visible = True
        Me.GridColumn75.VisibleIndex = 37
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "Base IRL"
        Me.GridColumn35.FieldName = "BaseIRL"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 38
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Cod AFP"
        Me.GridColumn36.FieldName = "PensionAdministratorCode"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 39
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Cod EPS"
        Me.GridColumn37.FieldName = "EPSCode"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 40
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "Cod CCF"
        Me.GridColumn38.FieldName = "CCFCode"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 41
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "Dias AFP"
        Me.GridColumn39.FieldName = "PensionDays"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 42
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "Dias EPS"
        Me.GridColumn40.FieldName = "HealthDays"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 43
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Dias ARP"
        Me.GridColumn41.FieldName = "ProfessionalRiskDays"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 44
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "Dias CCF"
        Me.GridColumn42.FieldName = "CompensationFundDays"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.Visible = True
        Me.GridColumn42.VisibleIndex = 45
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "Salario Basico"
        Me.GridColumn43.FieldName = "BasicSalary"
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.Visible = True
        Me.GridColumn43.VisibleIndex = 46
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Integral"
        Me.GridColumn44.FieldName = "IntegralSalary"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.Visible = True
        Me.GridColumn44.VisibleIndex = 47
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "Valor VST"
        Me.GridColumn45.FieldName = "VSTValue"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.Visible = True
        Me.GridColumn45.VisibleIndex = 48
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "IBC AFP"
        Me.GridColumn46.FieldName = "IBCPension"
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.Visible = True
        Me.GridColumn46.VisibleIndex = 49
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "IBC EPS"
        Me.GridColumn47.FieldName = "IBCHealth"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.Visible = True
        Me.GridColumn47.VisibleIndex = 50
        '
        'GridColumn48
        '
        Me.GridColumn48.Caption = "IBC ARP"
        Me.GridColumn48.FieldName = "IBCProfessionalRisk"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.Visible = True
        Me.GridColumn48.VisibleIndex = 51
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "IBC CCF"
        Me.GridColumn49.FieldName = "IBCCompensationFund"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.Visible = True
        Me.GridColumn49.VisibleIndex = 52
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "Tarifa AFP"
        Me.GridColumn50.FieldName = "RateContributionPension"
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.Visible = True
        Me.GridColumn50.VisibleIndex = 53
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Aporte AFP"
        Me.GridColumn51.FieldName = "ValuePension"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.Visible = True
        Me.GridColumn51.VisibleIndex = 54
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "FSP Subcuenta Solidaridad"
        Me.GridColumn52.FieldName = "PensionSolidarityFundValueContribution"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 55
        '
        'GridColumn53
        '
        Me.GridColumn53.Caption = "FSP Subcuenta Subsistencia"
        Me.GridColumn53.FieldName = "PensionSolidarityFundValueContributionSubsistence"
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.Visible = True
        Me.GridColumn53.VisibleIndex = 56
        '
        'GridColumn54
        '
        Me.GridColumn54.Caption = "Tarifa EPS"
        Me.GridColumn54.FieldName = "RateContributionHealth"
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.Visible = True
        Me.GridColumn54.VisibleIndex = 57
        '
        'GridColumn55
        '
        Me.GridColumn55.Caption = "Aporte EPS"
        Me.GridColumn55.FieldName = "ValueHealth"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.Visible = True
        Me.GridColumn55.VisibleIndex = 58
        '
        'GridColumn57
        '
        Me.GridColumn57.Caption = "Tarifa ARP"
        Me.GridColumn57.FieldName = "RateContributionProfessionalRisk"
        Me.GridColumn57.Name = "GridColumn57"
        Me.GridColumn57.Visible = True
        Me.GridColumn57.VisibleIndex = 59
        '
        'GridColumn58
        '
        Me.GridColumn58.Caption = "Centro Trabajo"
        Me.GridColumn58.FieldName = "WorkCenter"
        Me.GridColumn58.Name = "GridColumn58"
        Me.GridColumn58.Visible = True
        Me.GridColumn58.VisibleIndex = 60
        '
        'GridColumn59
        '
        Me.GridColumn59.Caption = "Aporte ARP"
        Me.GridColumn59.FieldName = "ValueContributionProfessionalRisk"
        Me.GridColumn59.Name = "GridColumn59"
        Me.GridColumn59.Visible = True
        Me.GridColumn59.VisibleIndex = 61
        '
        'GridColumn60
        '
        Me.GridColumn60.Caption = "Tarifa CCF"
        Me.GridColumn60.FieldName = "RateContributorCCF"
        Me.GridColumn60.Name = "GridColumn60"
        Me.GridColumn60.Visible = True
        Me.GridColumn60.VisibleIndex = 62
        '
        'GridColumn61
        '
        Me.GridColumn61.Caption = "Aporte CCF"
        Me.GridColumn61.FieldName = "ValueContributionCCF"
        Me.GridColumn61.Name = "GridColumn61"
        Me.GridColumn61.Visible = True
        Me.GridColumn61.VisibleIndex = 63
        '
        'GridColumn62
        '
        Me.GridColumn62.Caption = "Tarifa SENA"
        Me.GridColumn62.FieldName = "RateContributorSENA"
        Me.GridColumn62.Name = "GridColumn62"
        Me.GridColumn62.Visible = True
        Me.GridColumn62.VisibleIndex = 64
        '
        'GridColumn63
        '
        Me.GridColumn63.Caption = "Aporte SENA"
        Me.GridColumn63.FieldName = "ValueSena"
        Me.GridColumn63.Name = "GridColumn63"
        Me.GridColumn63.Visible = True
        Me.GridColumn63.VisibleIndex = 65
        '
        'GridColumn64
        '
        Me.GridColumn64.Caption = "Tarifa ICBF"
        Me.GridColumn64.FieldName = "RateContributionICBF"
        Me.GridColumn64.Name = "GridColumn64"
        Me.GridColumn64.Visible = True
        Me.GridColumn64.VisibleIndex = 66
        '
        'GridColumn65
        '
        Me.GridColumn65.Caption = "Aporte ICBF"
        Me.GridColumn65.FieldName = "ValueICBF"
        Me.GridColumn65.Name = "GridColumn65"
        Me.GridColumn65.Visible = True
        Me.GridColumn65.VisibleIndex = 67
        '
        'GridColumn66
        '
        Me.GridColumn66.Caption = "Tarifa Especial Pensiones"
        Me.GridColumn66.FieldName = "TarifaEspecialPensiones"
        Me.GridColumn66.Name = "GridColumn66"
        Me.GridColumn66.Visible = True
        Me.GridColumn66.VisibleIndex = 68
        '
        'GridColumn56
        '
        Me.GridColumn56.Caption = "Observaciones"
        Me.GridColumn56.FieldName = "Observations"
        Me.GridColumn56.Name = "GridColumn56"
        Me.GridColumn56.Visible = True
        Me.GridColumn56.VisibleIndex = 69
        '
        'INDtxtPolicyNumber
        '
        Me.INDtxtPolicyNumber.Location = New System.Drawing.Point(24, 133)
        Me.INDtxtPolicyNumber.Name = "INDtxtPolicyNumber"
        Me.INDtxtPolicyNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPolicyNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPolicyNumber.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPolicyNumber.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPolicyNumber.Properties.MaxLength = 7
        Me.INDtxtPolicyNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPolicyNumber.StyleController = Me.INDLcAutoliquidation
        Me.INDtxtPolicyNumber.TabIndex = 5
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1658, 533)
        Me.Root.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "Datos de los trabajadores"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.INDLcExcel, Me.INDLciImportButton})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1224, 513)
        Me.LayoutControlGroup3.Text = "Datos de los trabajadores"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDGcListEmployee
        Me.LayoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem4.CustomizationFormText = "Lista de Empleados"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 46)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(1200, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(1200, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1200, 414)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Lista de Empleados"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDLcExcel
        '
        Me.INDLcExcel.Control = Me.INDBtnExport
        Me.INDLcExcel.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLcExcel.CustomizationFormText = "Botón Exportar"
        Me.INDLcExcel.Location = New System.Drawing.Point(0, 0)
        Me.INDLcExcel.MaxSize = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.MinSize = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.Name = "INDLcExcel"
        Me.INDLcExcel.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2)
        Me.INDLcExcel.Size = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcExcel.Text = "Botón Exportar"
        Me.INDLcExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcExcel.TextVisible = False
        Me.INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciImportButton
        '
        Me.INDLciImportButton.Control = Me.INDBtnImportFile
        Me.INDLciImportButton.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciImportButton.CustomizationFormText = "Botón Importar"
        Me.INDLciImportButton.Location = New System.Drawing.Point(45, 0)
        Me.INDLciImportButton.MaxSize = New System.Drawing.Size(45, 40)
        Me.INDLciImportButton.MinSize = New System.Drawing.Size(45, 40)
        Me.INDLciImportButton.Name = "INDLciImportButton"
        Me.INDLciImportButton.Size = New System.Drawing.Size(1155, 46)
        Me.INDLciImportButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciImportButton.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0)
        Me.INDLciImportButton.Text = "Botón Importar"
        Me.INDLciImportButton.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciImportButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciImportButton.TextToControlDistance = 0
        Me.INDLciImportButton.TextVisible = False
        Me.INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup2.CustomizationFormText = "Información Patronal"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.INDLciPeriod, Me.LayoutControlItem6, Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 513)
        Me.LayoutControlGroup2.Text = "Información Patronal"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSlCompany
        Me.LayoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem1.CustomizationFormText = "Empresa"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Empresa"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(127, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDBtnProccess
        Me.LayoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem3.CustomizationFormText = "Botón Procesar"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Botón Procesar"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDSlWorkCenter
        Me.LayoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem5.CustomizationFormText = "Centros de Trabajo"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Centros de Trabajo"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(127, 17)
        '
        'INDLciPeriod
        '
        Me.INDLciPeriod.Control = Me.INDSlPeriodDate
        Me.INDLciPeriod.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPeriod.CustomizationFormText = "Periodo Liquidación"
        Me.INDLciPeriod.Location = New System.Drawing.Point(0, 180)
        Me.INDLciPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.Name = "INDLciPeriod"
        Me.INDLciPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPeriod.Text = "Periodo Liquidación"
        Me.INDLciPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPeriod.TextSize = New System.Drawing.Size(127, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDGcExportExcel
        Me.LayoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem6.CustomizationFormText = "Exportar Excel"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 276)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 184)
        Me.LayoutControlItem6.Text = "Exportar Excel"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        Me.LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDtxtPolicyNumber
        Me.LayoutControlItem2.CustomizationFormText = "Numero de Póliza"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Número de Póliza"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(127, 17)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolDate})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'INDcolDate
        '
        Me.INDcolDate.Caption = "Fecha"
        Me.INDcolDate.Name = "INDcolDate"
        Me.INDcolDate.OptionsColumn.AllowEdit = False
        Me.INDcolDate.Visible = True
        Me.INDcolDate.VisibleIndex = 0
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcAutoliquidation
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 550)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit13
        '
        Me.RepositoryItemPopupContainerEdit13.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit13.Name = "RepositoryItemPopupContainerEdit13"
        '
        'RepositoryItemPopupContainerEdit14
        '
        Me.RepositoryItemPopupContainerEdit14.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit14.Name = "RepositoryItemPopupContainerEdit14"
        '
        'RepositoryItemPopupContainerEdit15
        '
        Me.RepositoryItemPopupContainerEdit15.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit15.Name = "RepositoryItemPopupContainerEdit15"
        '
        'RepositoryItemPopupContainerEdit16
        '
        Me.RepositoryItemPopupContainerEdit16.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit16.Name = "RepositoryItemPopupContainerEdit16"
        '
        'RepositoryItemPopupContainerEdit17
        '
        Me.RepositoryItemPopupContainerEdit17.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit17.Name = "RepositoryItemPopupContainerEdit17"
        '
        'RepositoryItemPopupContainerEdit18
        '
        Me.RepositoryItemPopupContainerEdit18.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit18.Name = "RepositoryItemPopupContainerEdit18"
        '
        'RepositoryItemPopupContainerEdit19
        '
        Me.RepositoryItemPopupContainerEdit19.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit19.Name = "RepositoryItemPopupContainerEdit19"
        '
        'FrmINSReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1451, 694)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmINSReport"
        Me.Opacity = 1.0R
        Me.Text = "FrmINSReport"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcAutoliquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcAutoliquidation.ResumeLayout(False)
        CType(Me.INDGcListEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlWorkCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWorkCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlPeriodDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDateLiquidated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPolicyNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciImportButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDGcMain As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton2 As IndigoSimpleButton
    Friend WithEvents IndigoSimpleButton3 As IndigoSimpleButton
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit13 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit14 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit15 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit16 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDLcAutoliquidation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit17 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGcListEmployee As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColIdentificationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIdNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEmployeeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCCSSCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIngress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColINSCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDaysWorked As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciImportButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColNacionalityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit18 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDcolDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlCompany As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnProccess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSlWorkCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvWorkCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlPeriodDate As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvDateLiquidated As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolDate1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcExportExcel As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn60 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn65 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtPolicyNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit19 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
End Class
