Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRIPSInvoice
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim ColumnStateRepository1 As Presentation.Controls.ColumnStateRepository = New Presentation.Controls.ColumnStateRepository()
        Dim ColumnStateRepository2 As Presentation.Controls.ColumnStateRepository = New Presentation.Controls.ColumnStateRepository()
        Dim AppearanceObject13 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject14 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject15 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject16 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject17 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject18 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRIPSInvoice))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleTypeRisk = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvTypeRisk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvTypeRisk_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvTypeRisk_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvTypeRisk_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleIncomeCause = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvIncomeCause = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvIncomeCause_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIncomeCause_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIncomeCause_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlePopulationGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvPopulationGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvPopulationGroup_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvPopulationGroup_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvPopulationGroup_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleInvoiceCategory = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvInvoiceCategory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvInvoiceCategory_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoiceCategory_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoiceCategory_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoiceCategory_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCareCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCareCenter_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareCenter_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareCenter_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareCenter_CodeIPS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleContract = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvContract = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvContract_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvContract_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvContract_HealthAdministrator = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvContract_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvContract_Object = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvContract_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcInvoices = New DevExpress.XtraGrid.GridControl()
        Me.GvInvoices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESel = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleAdmission = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCareGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCareGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCareGroup_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareGroup_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareGroup_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleHealthAdministrator = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvHealthAdministrator = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvHealthAdministrator_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvHealthAdministrator_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvHealthAdministrator_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvHealthAdministrator_ThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvHealthAdministrator_Type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvHealthAdministrator_EntityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleReportType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeCutoffDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDGleInvoiceType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleDetailPackage = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInvoiceType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReportType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHealthAdministrator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInvoiceCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPopulationGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIncomeCause = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeRisk = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCutoffDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetailPackage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.GridViewColumnHeaderExtender1 = New Presentation.Controls.GridViewColumnHeaderExtender()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDSleTypeRisk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvTypeRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleIncomeCause.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvIncomeCause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePopulationGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPopulationGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInvoiceCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvInvoiceCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleContract.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRICESel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCareGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleReportType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeCutoffDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeCutoffDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleInvoiceType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDetailPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDetailPackage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInvoiceCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPopulationGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIncomeCause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCutoffDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetailPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1766, 530)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1766, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1766, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSleTypeRisk)
        Me.INDLcRoot.Controls.Add(Me.INDSleIncomeCause)
        Me.INDLcRoot.Controls.Add(Me.INDSlePopulationGroup)
        Me.INDLcRoot.Controls.Add(Me.INDSleInvoiceCategory)
        Me.INDLcRoot.Controls.Add(Me.INDSleCareCenter)
        Me.INDLcRoot.Controls.Add(Me.INDSleContract)
        Me.INDLcRoot.Controls.Add(Me.INDGcInvoices)
        Me.INDLcRoot.Controls.Add(Me.INDBtnFilter)
        Me.INDLcRoot.Controls.Add(Me.INDSleAdmission)
        Me.INDLcRoot.Controls.Add(Me.INDSleCareGroup)
        Me.INDLcRoot.Controls.Add(Me.INDSleHealthAdministrator)
        Me.INDLcRoot.Controls.Add(Me.INDGleReportType)
        Me.INDLcRoot.Controls.Add(Me.INDDeCutoffDate)
        Me.INDLcRoot.Controls.Add(Me.INDGleInvoiceType)
        Me.INDLcRoot.Controls.Add(Me.INDDeEndDate)
        Me.INDLcRoot.Controls.Add(Me.INDDeInitialDate)
        Me.INDLcRoot.Controls.Add(Me.INDSleDetailPackage)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(1562, 521)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSleTypeRisk
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleTypeRisk, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleTypeRisk, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleTypeRisk, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleTypeRisk, False)
        Me.INDSleTypeRisk.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleTypeRisk, False)
        Me.INDSleTypeRisk.Location = New System.Drawing.Point(149, 469)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleTypeRisk, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleTypeRisk.Name = "INDSleTypeRisk"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleTypeRisk, False)
        Me.INDSleTypeRisk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleTypeRisk.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTypeRisk.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleTypeRisk.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTypeRisk.Properties.Appearance.Options.UseFont = True
        Me.INDSleTypeRisk.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleTypeRisk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTypeRisk.Properties.NullText = ""
        Me.INDSleTypeRisk.Properties.PopupSizeable = False
        Me.INDSleTypeRisk.Properties.PopupView = Me.INDGvTypeRisk
        Me.INDSleTypeRisk.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleTypeRisk, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleTypeRisk, True)
        Me.INDSleTypeRisk.Size = New System.Drawing.Size(261, 28)
        Me.INDSleTypeRisk.StyleController = Me.INDLcRoot
        Me.INDSleTypeRisk.TabIndex = 16
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleTypeRisk, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleTypeRisk, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleTypeRisk, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleTypeRisk, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleTypeRisk, False)
        '
        'INDGvTypeRisk
        '
        Me.INDGvTypeRisk.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvTypeRisk.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvTypeRisk.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvTypeRisk.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvTypeRisk.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTypeRisk.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvTypeRisk.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTypeRisk.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvTypeRisk.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvTypeRisk.Appearance.Row.Options.UseFont = True
        Me.INDGvTypeRisk.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvTypeRisk_UnboundSelection, Me.INDGvTypeRisk_Code, Me.INDGvTypeRisk_Name})
        Me.INDGvTypeRisk.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvTypeRisk.Name = "INDGvTypeRisk"
        Me.INDGvTypeRisk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvTypeRisk.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvTypeRisk.OptionsSelection.MultiSelect = True
        Me.INDGvTypeRisk.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvTypeRisk.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvTypeRisk.OptionsView.ShowAutoFilterRow = True
        Me.INDGvTypeRisk.OptionsView.ShowDetailButtons = False
        Me.INDGvTypeRisk.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvTypeRisk, False)
        '
        'INDGvTypeRisk_UnboundSelection
        '
        Me.INDGvTypeRisk_UnboundSelection.Caption = " "
        Me.INDGvTypeRisk_UnboundSelection.FieldName = "INDGvTypeRisk_UnboundSelection"
        Me.INDGvTypeRisk_UnboundSelection.Name = "INDGvTypeRisk_UnboundSelection"
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvTypeRisk_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvTypeRisk_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvTypeRisk_UnboundSelection.Visible = True
        Me.INDGvTypeRisk_UnboundSelection.VisibleIndex = 0
        Me.INDGvTypeRisk_UnboundSelection.Width = 40
        '
        'INDGvTypeRisk_Code
        '
        Me.INDGvTypeRisk_Code.Caption = "Código"
        Me.INDGvTypeRisk_Code.FieldName = "Item1"
        Me.INDGvTypeRisk_Code.Name = "INDGvTypeRisk_Code"
        Me.INDGvTypeRisk_Code.OptionsColumn.AllowEdit = False
        Me.INDGvTypeRisk_Code.OptionsColumn.AllowFocus = False
        Me.INDGvTypeRisk_Code.Visible = True
        Me.INDGvTypeRisk_Code.VisibleIndex = 1
        Me.INDGvTypeRisk_Code.Width = 128
        '
        'INDGvTypeRisk_Name
        '
        Me.INDGvTypeRisk_Name.Caption = "Riesgo"
        Me.INDGvTypeRisk_Name.FieldName = "Item2"
        Me.INDGvTypeRisk_Name.Name = "INDGvTypeRisk_Name"
        Me.INDGvTypeRisk_Name.OptionsColumn.AllowEdit = False
        Me.INDGvTypeRisk_Name.OptionsColumn.AllowFocus = False
        Me.INDGvTypeRisk_Name.Visible = True
        Me.INDGvTypeRisk_Name.VisibleIndex = 2
        Me.INDGvTypeRisk_Name.Width = 128
        '
        'INDSleIncomeCause
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleIncomeCause, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleIncomeCause, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleIncomeCause, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleIncomeCause, False)
        Me.INDSleIncomeCause.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleIncomeCause, False)
        Me.INDSleIncomeCause.Location = New System.Drawing.Point(149, 437)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleIncomeCause, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleIncomeCause.Name = "INDSleIncomeCause"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleIncomeCause, False)
        Me.INDSleIncomeCause.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleIncomeCause.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleIncomeCause.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleIncomeCause.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleIncomeCause.Properties.Appearance.Options.UseFont = True
        Me.INDSleIncomeCause.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleIncomeCause.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleIncomeCause.Properties.NullText = ""
        Me.INDSleIncomeCause.Properties.PopupSizeable = False
        Me.INDSleIncomeCause.Properties.PopupView = Me.INDGvIncomeCause
        Me.INDSleIncomeCause.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleIncomeCause, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleIncomeCause, True)
        Me.INDSleIncomeCause.Size = New System.Drawing.Size(261, 28)
        Me.INDSleIncomeCause.StyleController = Me.INDLcRoot
        Me.INDSleIncomeCause.TabIndex = 15
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleIncomeCause, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleIncomeCause, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleIncomeCause, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleIncomeCause, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleIncomeCause, False)
        '
        'INDGvIncomeCause
        '
        Me.INDGvIncomeCause.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvIncomeCause.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvIncomeCause.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvIncomeCause.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvIncomeCause.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvIncomeCause.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvIncomeCause.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvIncomeCause.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvIncomeCause.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvIncomeCause.Appearance.Row.Options.UseFont = True
        Me.INDGvIncomeCause.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvIncomeCause_UnboundSelection, Me.INDGvIncomeCause_Code, Me.INDGvIncomeCause_Name})
        Me.INDGvIncomeCause.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvIncomeCause.Name = "INDGvIncomeCause"
        Me.INDGvIncomeCause.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvIncomeCause.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvIncomeCause.OptionsSelection.MultiSelect = True
        Me.INDGvIncomeCause.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvIncomeCause.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvIncomeCause.OptionsView.ShowAutoFilterRow = True
        Me.INDGvIncomeCause.OptionsView.ShowDetailButtons = False
        Me.INDGvIncomeCause.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvIncomeCause, False)
        '
        'INDGvIncomeCause_UnboundSelection
        '
        Me.INDGvIncomeCause_UnboundSelection.Caption = " "
        Me.INDGvIncomeCause_UnboundSelection.FieldName = "INDGvIncomeCause_UnboundSelection"
        Me.INDGvIncomeCause_UnboundSelection.Name = "INDGvIncomeCause_UnboundSelection"
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvIncomeCause_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvIncomeCause_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvIncomeCause_UnboundSelection.Visible = True
        Me.INDGvIncomeCause_UnboundSelection.VisibleIndex = 0
        '
        'INDGvIncomeCause_Code
        '
        Me.INDGvIncomeCause_Code.Caption = "Código"
        Me.INDGvIncomeCause_Code.FieldName = "Code"
        Me.INDGvIncomeCause_Code.Name = "INDGvIncomeCause_Code"
        Me.INDGvIncomeCause_Code.OptionsColumn.AllowEdit = False
        Me.INDGvIncomeCause_Code.OptionsColumn.AllowFocus = False
        Me.INDGvIncomeCause_Code.Visible = True
        Me.INDGvIncomeCause_Code.VisibleIndex = 1
        '
        'INDGvIncomeCause_Name
        '
        Me.INDGvIncomeCause_Name.Caption = "Causa"
        Me.INDGvIncomeCause_Name.FieldName = "Name"
        Me.INDGvIncomeCause_Name.Name = "INDGvIncomeCause_Name"
        Me.INDGvIncomeCause_Name.OptionsColumn.AllowEdit = False
        Me.INDGvIncomeCause_Name.OptionsColumn.AllowFocus = False
        Me.INDGvIncomeCause_Name.Visible = True
        Me.INDGvIncomeCause_Name.VisibleIndex = 2
        '
        'INDSlePopulationGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlePopulationGroup, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlePopulationGroup, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSlePopulationGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.INDSlePopulationGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.INDSlePopulationGroup.Location = New System.Drawing.Point(149, 405)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlePopulationGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlePopulationGroup.Name = "INDSlePopulationGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.INDSlePopulationGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlePopulationGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlePopulationGroup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSlePopulationGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlePopulationGroup.Properties.Appearance.Options.UseFont = True
        Me.INDSlePopulationGroup.Properties.Appearance.Options.UseForeColor = True
        Me.INDSlePopulationGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlePopulationGroup.Properties.NullText = ""
        Me.INDSlePopulationGroup.Properties.PopupSizeable = False
        Me.INDSlePopulationGroup.Properties.PopupView = Me.INDGvPopulationGroup
        Me.INDSlePopulationGroup.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlePopulationGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlePopulationGroup, True)
        Me.INDSlePopulationGroup.Size = New System.Drawing.Size(261, 28)
        Me.INDSlePopulationGroup.StyleController = Me.INDLcRoot
        Me.INDSlePopulationGroup.TabIndex = 14
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlePopulationGroup, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlePopulationGroup, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlePopulationGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlePopulationGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlePopulationGroup, False)
        '
        'INDGvPopulationGroup
        '
        Me.INDGvPopulationGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPopulationGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPopulationGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPopulationGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPopulationGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPopulationGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPopulationGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPopulationGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPopulationGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPopulationGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvPopulationGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvPopulationGroup_UnboundSelection, Me.INDGvPopulationGroup_Code, Me.INDGvPopulationGroup_Name})
        Me.INDGvPopulationGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvPopulationGroup.Name = "INDGvPopulationGroup"
        Me.INDGvPopulationGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvPopulationGroup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvPopulationGroup.OptionsSelection.MultiSelect = True
        Me.INDGvPopulationGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPopulationGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPopulationGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPopulationGroup.OptionsView.ShowDetailButtons = False
        Me.INDGvPopulationGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPopulationGroup, False)
        '
        'INDGvPopulationGroup_UnboundSelection
        '
        Me.INDGvPopulationGroup_UnboundSelection.Caption = " "
        Me.INDGvPopulationGroup_UnboundSelection.FieldName = "INDGvPopulationGroup_UnboundSelection"
        Me.INDGvPopulationGroup_UnboundSelection.Name = "INDGvPopulationGroup_UnboundSelection"
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvPopulationGroup_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvPopulationGroup_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvPopulationGroup_UnboundSelection.Visible = True
        Me.INDGvPopulationGroup_UnboundSelection.VisibleIndex = 0
        Me.INDGvPopulationGroup_UnboundSelection.Width = 40
        '
        'INDGvPopulationGroup_Code
        '
        Me.INDGvPopulationGroup_Code.Caption = "Código"
        Me.INDGvPopulationGroup_Code.FieldName = "CODIGO"
        Me.INDGvPopulationGroup_Code.Name = "INDGvPopulationGroup_Code"
        Me.INDGvPopulationGroup_Code.OptionsColumn.AllowEdit = False
        Me.INDGvPopulationGroup_Code.OptionsColumn.AllowFocus = False
        Me.INDGvPopulationGroup_Code.Visible = True
        Me.INDGvPopulationGroup_Code.VisibleIndex = 1
        Me.INDGvPopulationGroup_Code.Width = 172
        '
        'INDGvPopulationGroup_Name
        '
        Me.INDGvPopulationGroup_Name.Caption = "Descripción"
        Me.INDGvPopulationGroup_Name.FieldName = "DESCRIPCION"
        Me.INDGvPopulationGroup_Name.Name = "INDGvPopulationGroup_Name"
        Me.INDGvPopulationGroup_Name.OptionsColumn.AllowEdit = False
        Me.INDGvPopulationGroup_Name.OptionsColumn.AllowFocus = False
        Me.INDGvPopulationGroup_Name.Visible = True
        Me.INDGvPopulationGroup_Name.VisibleIndex = 2
        Me.INDGvPopulationGroup_Name.Width = 172
        '
        'INDSleInvoiceCategory
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleInvoiceCategory, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleInvoiceCategory, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleInvoiceCategory, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.INDSleInvoiceCategory.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.INDSleInvoiceCategory.Location = New System.Drawing.Point(149, 373)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleInvoiceCategory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleInvoiceCategory.Name = "INDSleInvoiceCategory"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.INDSleInvoiceCategory.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleInvoiceCategory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleInvoiceCategory.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleInvoiceCategory.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleInvoiceCategory.Properties.Appearance.Options.UseFont = True
        Me.INDSleInvoiceCategory.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleInvoiceCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleInvoiceCategory.Properties.NullText = ""
        Me.INDSleInvoiceCategory.Properties.PopupSizeable = False
        Me.INDSleInvoiceCategory.Properties.PopupView = Me.INDGvInvoiceCategory
        Me.INDSleInvoiceCategory.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleInvoiceCategory, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleInvoiceCategory, True)
        Me.INDSleInvoiceCategory.Size = New System.Drawing.Size(261, 28)
        Me.INDSleInvoiceCategory.StyleController = Me.INDLcRoot
        Me.INDSleInvoiceCategory.TabIndex = 13
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleInvoiceCategory, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleInvoiceCategory, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleInvoiceCategory, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleInvoiceCategory, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleInvoiceCategory, False)
        '
        'INDGvInvoiceCategory
        '
        Me.INDGvInvoiceCategory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvInvoiceCategory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvInvoiceCategory.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvInvoiceCategory.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvInvoiceCategory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvInvoiceCategory.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvInvoiceCategory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvInvoiceCategory.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvInvoiceCategory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvInvoiceCategory.Appearance.Row.Options.UseFont = True
        Me.INDGvInvoiceCategory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvInvoiceCategory_UnboundSelection, Me.INDGvInvoiceCategory_Code, Me.INDGvInvoiceCategory_Name, Me.INDGvInvoiceCategory_Status})
        Me.INDGvInvoiceCategory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvInvoiceCategory.Name = "INDGvInvoiceCategory"
        Me.INDGvInvoiceCategory.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvInvoiceCategory.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvInvoiceCategory.OptionsSelection.MultiSelect = True
        Me.INDGvInvoiceCategory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvInvoiceCategory.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvInvoiceCategory.OptionsView.ShowAutoFilterRow = True
        Me.INDGvInvoiceCategory.OptionsView.ShowDetailButtons = False
        Me.INDGvInvoiceCategory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvInvoiceCategory, False)
        '
        'INDGvInvoiceCategory_UnboundSelection
        '
        Me.INDGvInvoiceCategory_UnboundSelection.Caption = " "
        Me.INDGvInvoiceCategory_UnboundSelection.FieldName = "INDGvInvoiceCategory_UnboundSelection"
        Me.INDGvInvoiceCategory_UnboundSelection.Name = "INDGvInvoiceCategory_UnboundSelection"
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvInvoiceCategory_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvInvoiceCategory_UnboundSelection.Visible = True
        Me.INDGvInvoiceCategory_UnboundSelection.VisibleIndex = 0
        '
        'INDGvInvoiceCategory_Code
        '
        Me.INDGvInvoiceCategory_Code.Caption = "Código"
        Me.INDGvInvoiceCategory_Code.FieldName = "Code"
        Me.INDGvInvoiceCategory_Code.Name = "INDGvInvoiceCategory_Code"
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowEdit = False
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowFocus = False
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowMove = False
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowShowHide = False
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowSize = False
        Me.INDGvInvoiceCategory_Code.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Code.Visible = True
        Me.INDGvInvoiceCategory_Code.VisibleIndex = 1
        '
        'INDGvInvoiceCategory_Name
        '
        Me.INDGvInvoiceCategory_Name.Caption = "Nombre"
        Me.INDGvInvoiceCategory_Name.FieldName = "Name"
        Me.INDGvInvoiceCategory_Name.Name = "INDGvInvoiceCategory_Name"
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowEdit = False
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowFocus = False
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowMove = False
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowShowHide = False
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowSize = False
        Me.INDGvInvoiceCategory_Name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Name.Visible = True
        Me.INDGvInvoiceCategory_Name.VisibleIndex = 2
        '
        'INDGvInvoiceCategory_Status
        '
        Me.INDGvInvoiceCategory_Status.Caption = "Estado"
        Me.INDGvInvoiceCategory_Status.FieldName = "StatusName"
        Me.INDGvInvoiceCategory_Status.Name = "INDGvInvoiceCategory_Status"
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowEdit = False
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowFocus = False
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowMove = False
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowShowHide = False
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowSize = False
        Me.INDGvInvoiceCategory_Status.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoiceCategory_Status.Visible = True
        Me.INDGvInvoiceCategory_Status.VisibleIndex = 3
        '
        'INDSleCareCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCareCenter, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCareCenter, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCareCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.INDSleCareCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCareCenter, False)
        Me.INDSleCareCenter.Location = New System.Drawing.Point(149, 245)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCareCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCareCenter.Name = "INDSleCareCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCareCenter, False)
        Me.INDSleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCareCenter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCareCenter.Properties.NullText = ""
        Me.INDSleCareCenter.Properties.PopupSizeable = False
        Me.INDSleCareCenter.Properties.PopupView = Me.INDGvCareCenter
        Me.INDSleCareCenter.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCareCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCareCenter, True)
        Me.INDSleCareCenter.Size = New System.Drawing.Size(261, 28)
        Me.INDSleCareCenter.StyleController = Me.INDLcRoot
        Me.INDSleCareCenter.TabIndex = 9
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCareCenter, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCareCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCareCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCareCenter, False)
        '
        'INDGvCareCenter
        '
        Me.INDGvCareCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCareCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCareCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCareCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCareCenter.Appearance.Row.Options.UseFont = True
        Me.INDGvCareCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCareCenter_UnboundSelection, Me.INDGvCareCenter_Code, Me.INDGvCareCenter_Name, Me.INDGvCareCenter_CodeIPS})
        Me.INDGvCareCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCareCenter.Name = "INDGvCareCenter"
        Me.INDGvCareCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCareCenter.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvCareCenter.OptionsSelection.MultiSelect = True
        Me.INDGvCareCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCareCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCareCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCareCenter.OptionsView.ShowDetailButtons = False
        Me.INDGvCareCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCareCenter, False)
        '
        'INDGvCareCenter_UnboundSelection
        '
        Me.INDGvCareCenter_UnboundSelection.Caption = " "
        Me.INDGvCareCenter_UnboundSelection.FieldName = "INDGvCareCenter_UnboundSelection"
        Me.INDGvCareCenter_UnboundSelection.Name = "INDGvCareCenter_UnboundSelection"
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvCareCenter_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareCenter_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvCareCenter_UnboundSelection.Visible = True
        Me.INDGvCareCenter_UnboundSelection.VisibleIndex = 0
        Me.INDGvCareCenter_UnboundSelection.Width = 40
        '
        'INDGvCareCenter_Code
        '
        Me.INDGvCareCenter_Code.Caption = "Código"
        Me.INDGvCareCenter_Code.FieldName = "CODCENATE"
        Me.INDGvCareCenter_Code.Name = "INDGvCareCenter_Code"
        Me.INDGvCareCenter_Code.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_Code.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_Code.Visible = True
        Me.INDGvCareCenter_Code.VisibleIndex = 3
        Me.INDGvCareCenter_Code.Width = 116
        '
        'INDGvCareCenter_Name
        '
        Me.INDGvCareCenter_Name.Caption = "Nombre"
        Me.INDGvCareCenter_Name.FieldName = "NOMCENATE"
        Me.INDGvCareCenter_Name.Name = "INDGvCareCenter_Name"
        Me.INDGvCareCenter_Name.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_Name.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_Name.Visible = True
        Me.INDGvCareCenter_Name.VisibleIndex = 1
        Me.INDGvCareCenter_Name.Width = 114
        '
        'INDGvCareCenter_CodeIPS
        '
        Me.INDGvCareCenter_CodeIPS.Caption = "Código de Habilitación"
        Me.INDGvCareCenter_CodeIPS.FieldName = "CODIPSSEC"
        Me.INDGvCareCenter_CodeIPS.Name = "INDGvCareCenter_CodeIPS"
        Me.INDGvCareCenter_CodeIPS.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_CodeIPS.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_CodeIPS.Visible = True
        Me.INDGvCareCenter_CodeIPS.VisibleIndex = 2
        Me.INDGvCareCenter_CodeIPS.Width = 114
        '
        'INDSleContract
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleContract, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleContract, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleContract, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleContract, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.Location = New System.Drawing.Point(149, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleContract, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleContract.Name = "INDSleContract"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleContract.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleContract.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleContract.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleContract.Properties.Appearance.Options.UseFont = True
        Me.INDSleContract.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleContract.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleContract.Properties.NullText = ""
        Me.INDSleContract.Properties.PopupSizeable = False
        Me.INDSleContract.Properties.PopupView = Me.INDGvContract
        Me.INDSleContract.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleContract, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleContract, True)
        Me.INDSleContract.Size = New System.Drawing.Size(261, 28)
        Me.INDSleContract.StyleController = Me.INDLcRoot
        Me.INDSleContract.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleContract, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleContract, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleContract, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleContract, False)
        '
        'INDGvContract
        '
        Me.INDGvContract.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvContract.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvContract.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvContract.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvContract.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvContract.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvContract.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvContract.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvContract.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvContract.Appearance.Row.Options.UseFont = True
        Me.INDGvContract.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvContract_UnboundSelection, Me.INDGvContract_Code, Me.INDGvContract_HealthAdministrator, Me.INDGvContract_Name, Me.INDGvContract_Object, Me.INDGvContract_Status})
        Me.INDGvContract.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvContract.Name = "INDGvContract"
        Me.INDGvContract.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvContract.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvContract.OptionsSelection.MultiSelect = True
        Me.INDGvContract.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvContract.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvContract.OptionsView.ShowAutoFilterRow = True
        Me.INDGvContract.OptionsView.ShowDetailButtons = False
        Me.INDGvContract.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvContract, False)
        '
        'INDGvContract_UnboundSelection
        '
        Me.INDGvContract_UnboundSelection.Caption = " "
        Me.INDGvContract_UnboundSelection.FieldName = "INDGvContract_UnboundSelection"
        Me.INDGvContract_UnboundSelection.Name = "INDGvContract_UnboundSelection"
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvContract_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvContract_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvContract_UnboundSelection.Visible = True
        Me.INDGvContract_UnboundSelection.VisibleIndex = 0
        Me.INDGvContract_UnboundSelection.Width = 40
        '
        'INDGvContract_Code
        '
        Me.INDGvContract_Code.Caption = "Código"
        Me.INDGvContract_Code.FieldName = "Code"
        Me.INDGvContract_Code.Name = "INDGvContract_Code"
        Me.INDGvContract_Code.OptionsColumn.AllowEdit = False
        Me.INDGvContract_Code.OptionsColumn.AllowFocus = False
        Me.INDGvContract_Code.Visible = True
        Me.INDGvContract_Code.VisibleIndex = 1
        Me.INDGvContract_Code.Width = 68
        '
        'INDGvContract_HealthAdministrator
        '
        Me.INDGvContract_HealthAdministrator.Caption = "Entidad"
        Me.INDGvContract_HealthAdministrator.FieldName = "HealthAdministratorId.CodeName"
        Me.INDGvContract_HealthAdministrator.Name = "INDGvContract_HealthAdministrator"
        Me.INDGvContract_HealthAdministrator.OptionsColumn.AllowEdit = False
        Me.INDGvContract_HealthAdministrator.OptionsColumn.AllowFocus = False
        Me.INDGvContract_HealthAdministrator.Visible = True
        Me.INDGvContract_HealthAdministrator.VisibleIndex = 2
        Me.INDGvContract_HealthAdministrator.Width = 68
        '
        'INDGvContract_Name
        '
        Me.INDGvContract_Name.Caption = "Nombre"
        Me.INDGvContract_Name.FieldName = "ContractName"
        Me.INDGvContract_Name.Name = "INDGvContract_Name"
        Me.INDGvContract_Name.OptionsColumn.AllowEdit = False
        Me.INDGvContract_Name.OptionsColumn.AllowFocus = False
        Me.INDGvContract_Name.Visible = True
        Me.INDGvContract_Name.VisibleIndex = 3
        Me.INDGvContract_Name.Width = 68
        '
        'INDGvContract_Object
        '
        Me.INDGvContract_Object.Caption = "Objeto"
        Me.INDGvContract_Object.FieldName = "ContractObject"
        Me.INDGvContract_Object.Name = "INDGvContract_Object"
        Me.INDGvContract_Object.OptionsColumn.AllowEdit = False
        Me.INDGvContract_Object.OptionsColumn.AllowFocus = False
        Me.INDGvContract_Object.Visible = True
        Me.INDGvContract_Object.VisibleIndex = 4
        Me.INDGvContract_Object.Width = 68
        '
        'INDGvContract_Status
        '
        Me.INDGvContract_Status.Caption = "Estado"
        Me.INDGvContract_Status.FieldName = "StatusName"
        Me.INDGvContract_Status.Name = "INDGvContract_Status"
        Me.INDGvContract_Status.OptionsColumn.AllowEdit = False
        Me.INDGvContract_Status.OptionsColumn.AllowFocus = False
        Me.INDGvContract_Status.Visible = True
        Me.INDGvContract_Status.VisibleIndex = 5
        Me.INDGvContract_Status.Width = 72
        '
        'INDGcInvoices
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcInvoices, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcInvoices, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcInvoices, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcInvoices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcInvoices, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcInvoices, False)
        Me.INDGcInvoices.Location = New System.Drawing.Point(438, 53)
        Me.INDGcInvoices.MainView = Me.GvInvoices
        Me.INDGcInvoices.MinimumSize = New System.Drawing.Size(640, 0)
        Me.INDGcInvoices.Name = "INDGcInvoices"
        Me.INDGcInvoices.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRICESel})
        Me.INDGcInvoices.Size = New System.Drawing.Size(1083, 505)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcInvoices, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcInvoices.TabIndex = 19
        Me.INDGcInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GvInvoices})
        '
        'GvInvoices
        '
        Me.GvInvoices.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GvInvoices.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GvInvoices.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GvInvoices.Appearance.FocusedRow.Options.UseFont = True
        Me.GvInvoices.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GvInvoices.Appearance.GroupRow.Options.UseFont = True
        Me.GvInvoices.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GvInvoices.Appearance.HeaderPanel.Options.UseFont = True
        Me.GvInvoices.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GvInvoices.Appearance.Row.Options.UseFont = True
        Me.GvInvoices.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GvInvoices.Appearance.ViewCaption.Options.UseFont = True
        Me.GvInvoices.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSel, Me.GridColumn14, Me.GridColumn10, Me.GridColumn15, Me.GridColumn11, Me.GridColumn13, Me.GridColumn16, Me.GridColumn12})
        Me.GvInvoices.GridControl = Me.INDGcInvoices
        Me.GvInvoices.Name = "GvInvoices"
        Me.GvInvoices.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.GvInvoices.OptionsView.EnableAppearanceEvenRow = True
        Me.GvInvoices.OptionsView.EnableAppearanceOddRow = True
        Me.GvInvoices.OptionsView.ShowAutoFilterRow = True
        Me.GvInvoices.OptionsView.ShowDetailButtons = False
        Me.GvInvoices.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GvInvoices, False)
        '
        'INDColSel
        '
        Me.INDColSel.ColumnEdit = Me.INDRICESel
        Me.INDColSel.FieldName = "CheckValue"
        Me.INDColSel.Name = "INDColSel"
        Me.INDColSel.OptionsColumn.ShowCaption = False
        Me.INDColSel.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.INDColSel.Visible = True
        Me.INDColSel.VisibleIndex = 0
        '
        'INDRICESel
        '
        Me.INDRICESel.AutoHeight = False
        Me.INDRICESel.Name = "INDRICESel"
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Ingreso"
        Me.GridColumn14.FieldName = "AdmissionNumber"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 1
        Me.GridColumn14.Width = 216
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Factura"
        Me.GridColumn10.FieldName = "InvoiceNumber"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 246
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Entidad"
        Me.GridColumn15.FieldName = "HealthAdministratorCodeName"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 3
        Me.GridColumn15.Width = 425
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Fecha"
        Me.GridColumn11.FieldName = "InvoiceDate"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 4
        Me.GridColumn11.Width = 203
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Valor Factura"
        Me.GridColumn13.DisplayFormat.FormatString = "C2"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.FieldName = "TotalInvoice"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 5
        Me.GridColumn13.Width = 257
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Estado"
        Me.GridColumn16.FieldName = "StateName"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        ColumnStateRepository1.Checked = False
        ColumnStateRepository1.State = DevExpress.Utils.Drawing.ObjectState.Normal
        Me.GridColumn16.Tag = ColumnStateRepository1
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 6
        Me.GridColumn16.Width = 143
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "# Radicado"
        Me.GridColumn12.FieldName = "RadicatedConsecutive"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        ColumnStateRepository2.Checked = False
        ColumnStateRepository2.State = DevExpress.Utils.Drawing.ObjectState.Normal
        Me.GridColumn12.Tag = ColumnStateRepository2
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 7
        Me.GridColumn12.Width = 142
        '
        'INDBtnFilter
        '
        Me.INDBtnFilter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnFilter.Appearance.Options.UseFont = True
        Me.INDBtnFilter.Location = New System.Drawing.Point(24, 533)
        Me.INDBtnFilter.Name = "INDBtnFilter"
        Me.INDBtnFilter.Size = New System.Drawing.Size(386, 25)
        Me.INDBtnFilter.StyleController = Me.INDLcRoot
        Me.INDBtnFilter.TabIndex = 18
        Me.INDBtnFilter.Text = "Procesar"
        '
        'INDSleAdmission
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleAdmission, AppearanceObject13)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleAdmission, AppearanceObject14)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleAdmission, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleAdmission, False)
        Me.INDSleAdmission.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleAdmission, False)
        Me.INDSleAdmission.Location = New System.Drawing.Point(149, 501)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleAdmission.Name = "INDSleAdmission"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleAdmission, False)
        Me.INDSleAdmission.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleAdmission.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleAdmission.Properties.Appearance.Options.UseFont = True
        Me.INDSleAdmission.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleAdmission.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleAdmission.Properties.DisplayMember = "AdmissionNumber"
        Me.INDSleAdmission.Properties.NullText = ""
        Me.INDSleAdmission.Properties.PopupSizeable = False
        Me.INDSleAdmission.Properties.PopupView = Me.SearchLookUpEdit3View
        Me.INDSleAdmission.Properties.ShowFooter = False
        Me.INDSleAdmission.Properties.ValueMember = "AdmissionNumber"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleAdmission, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleAdmission, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleAdmission, True)
        Me.INDSleAdmission.Size = New System.Drawing.Size(261, 28)
        Me.INDSleAdmission.StyleController = Me.INDLcRoot
        Me.INDSleAdmission.TabIndex = 17
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleAdmission, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleAdmission, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleAdmission, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleAdmission, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleAdmission, False)
        '
        'SearchLookUpEdit3View
        '
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.SearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit3View.Name = "SearchLookUpEdit3View"
        Me.SearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Ingreso"
        Me.GridColumn7.FieldName = "AdmissionNumber"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 285
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Código Paciente"
        Me.GridColumn8.FieldName = "PatientCode"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 372
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nombre Paciente"
        Me.GridColumn9.FieldName = "IPNOMCOMP"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 975
        '
        'INDSleCareGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCareGroup, AppearanceObject15)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCareGroup, AppearanceObject16)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCareGroup, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCareGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.Location = New System.Drawing.Point(149, 341)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCareGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCareGroup.Name = "INDSleCareGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCareGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCareGroup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCareGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareGroup.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareGroup.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareGroup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCareGroup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCareGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCareGroup.Properties.NullText = ""
        Me.INDSleCareGroup.Properties.PopupSizeable = False
        Me.INDSleCareGroup.Properties.PopupView = Me.INDGvCareGroup
        Me.INDSleCareGroup.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCareGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCareGroup, True)
        Me.INDSleCareGroup.Size = New System.Drawing.Size(261, 28)
        Me.INDSleCareGroup.StyleController = Me.INDLcRoot
        Me.INDSleCareGroup.TabIndex = 12
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCareGroup, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCareGroup, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCareGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCareGroup, False)
        '
        'INDGvCareGroup
        '
        Me.INDGvCareGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCareGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCareGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCareGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCareGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvCareGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCareGroup_UnboundSelection, Me.INDGvCareGroup_Code, Me.INDGvCareGroup_Name})
        Me.INDGvCareGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCareGroup.Name = "INDGvCareGroup"
        Me.INDGvCareGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCareGroup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvCareGroup.OptionsSelection.MultiSelect = True
        Me.INDGvCareGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCareGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCareGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCareGroup.OptionsView.ShowDetailButtons = False
        Me.INDGvCareGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCareGroup, False)
        '
        'INDGvCareGroup_UnboundSelection
        '
        Me.INDGvCareGroup_UnboundSelection.Caption = " "
        Me.INDGvCareGroup_UnboundSelection.FieldName = "INDGvCareGroup_UnboundSelection"
        Me.INDGvCareGroup_UnboundSelection.Name = "INDGvCareGroup_UnboundSelection"
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvCareGroup_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCareGroup_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvCareGroup_UnboundSelection.Visible = True
        Me.INDGvCareGroup_UnboundSelection.VisibleIndex = 0
        Me.INDGvCareGroup_UnboundSelection.Width = 40
        '
        'INDGvCareGroup_Code
        '
        Me.INDGvCareGroup_Code.Caption = "Código"
        Me.INDGvCareGroup_Code.FieldName = "Code"
        Me.INDGvCareGroup_Code.Name = "INDGvCareGroup_Code"
        Me.INDGvCareGroup_Code.OptionsColumn.AllowEdit = False
        Me.INDGvCareGroup_Code.OptionsColumn.AllowFocus = False
        Me.INDGvCareGroup_Code.Visible = True
        Me.INDGvCareGroup_Code.VisibleIndex = 1
        Me.INDGvCareGroup_Code.Width = 82
        '
        'INDGvCareGroup_Name
        '
        Me.INDGvCareGroup_Name.Caption = "Nombre"
        Me.INDGvCareGroup_Name.FieldName = "Name"
        Me.INDGvCareGroup_Name.Name = "INDGvCareGroup_Name"
        Me.INDGvCareGroup_Name.OptionsColumn.AllowEdit = False
        Me.INDGvCareGroup_Name.OptionsColumn.AllowFocus = False
        Me.INDGvCareGroup_Name.Visible = True
        Me.INDGvCareGroup_Name.VisibleIndex = 2
        Me.INDGvCareGroup_Name.Width = 262
        '
        'INDSleHealthAdministrator
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleHealthAdministrator, AppearanceObject17)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleHealthAdministrator, AppearanceObject18)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleHealthAdministrator, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleHealthAdministrator, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.INDSleHealthAdministrator.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.INDSleHealthAdministrator.Location = New System.Drawing.Point(149, 309)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleHealthAdministrator, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleHealthAdministrator.Name = "INDSleHealthAdministrator"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.INDSleHealthAdministrator.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleHealthAdministrator.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleHealthAdministrator.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleHealthAdministrator.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleHealthAdministrator.Properties.Appearance.Options.UseFont = True
        Me.INDSleHealthAdministrator.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleHealthAdministrator.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleHealthAdministrator.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleHealthAdministrator.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleHealthAdministrator.Properties.NullText = ""
        Me.INDSleHealthAdministrator.Properties.PopupSizeable = False
        Me.INDSleHealthAdministrator.Properties.PopupView = Me.INDGvHealthAdministrator
        Me.INDSleHealthAdministrator.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleHealthAdministrator, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleHealthAdministrator, True)
        Me.INDSleHealthAdministrator.Size = New System.Drawing.Size(261, 28)
        Me.INDSleHealthAdministrator.StyleController = Me.INDLcRoot
        Me.INDSleHealthAdministrator.TabIndex = 11
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleHealthAdministrator, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleHealthAdministrator, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleHealthAdministrator, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleHealthAdministrator, False)
        '
        'INDGvHealthAdministrator
        '
        Me.INDGvHealthAdministrator.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvHealthAdministrator.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvHealthAdministrator.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvHealthAdministrator.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvHealthAdministrator.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvHealthAdministrator.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvHealthAdministrator.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvHealthAdministrator.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvHealthAdministrator.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvHealthAdministrator.Appearance.Row.Options.UseFont = True
        Me.INDGvHealthAdministrator.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvHealthAdministrator_UnboundSelection, Me.INDGvHealthAdministrator_Code, Me.INDGvHealthAdministrator_Name, Me.INDGvHealthAdministrator_ThirdParty, Me.INDGvHealthAdministrator_Type, Me.INDGvHealthAdministrator_EntityCode})
        Me.INDGvHealthAdministrator.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvHealthAdministrator.Name = "INDGvHealthAdministrator"
        Me.INDGvHealthAdministrator.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvHealthAdministrator.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvHealthAdministrator.OptionsSelection.MultiSelect = True
        Me.INDGvHealthAdministrator.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvHealthAdministrator.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvHealthAdministrator.OptionsView.ShowAutoFilterRow = True
        Me.INDGvHealthAdministrator.OptionsView.ShowDetailButtons = False
        Me.INDGvHealthAdministrator.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvHealthAdministrator, False)
        '
        'INDGvHealthAdministrator_UnboundSelection
        '
        Me.INDGvHealthAdministrator_UnboundSelection.Caption = " "
        Me.INDGvHealthAdministrator_UnboundSelection.FieldName = "INDGvHealthAdministrator_UnboundSelection"
        Me.INDGvHealthAdministrator_UnboundSelection.Name = "INDGvHealthAdministrator_UnboundSelection"
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvHealthAdministrator_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvHealthAdministrator_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvHealthAdministrator_UnboundSelection.Visible = True
        Me.INDGvHealthAdministrator_UnboundSelection.VisibleIndex = 0
        Me.INDGvHealthAdministrator_UnboundSelection.Width = 40
        '
        'INDGvHealthAdministrator_Code
        '
        Me.INDGvHealthAdministrator_Code.Caption = "Código"
        Me.INDGvHealthAdministrator_Code.FieldName = "Code"
        Me.INDGvHealthAdministrator_Code.Name = "INDGvHealthAdministrator_Code"
        Me.INDGvHealthAdministrator_Code.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_Code.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_Code.Visible = True
        Me.INDGvHealthAdministrator_Code.VisibleIndex = 1
        Me.INDGvHealthAdministrator_Code.Width = 50
        '
        'INDGvHealthAdministrator_Name
        '
        Me.INDGvHealthAdministrator_Name.Caption = "Nombre"
        Me.INDGvHealthAdministrator_Name.FieldName = "Name"
        Me.INDGvHealthAdministrator_Name.Name = "INDGvHealthAdministrator_Name"
        Me.INDGvHealthAdministrator_Name.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_Name.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_Name.Visible = True
        Me.INDGvHealthAdministrator_Name.VisibleIndex = 2
        Me.INDGvHealthAdministrator_Name.Width = 85
        '
        'INDGvHealthAdministrator_ThirdParty
        '
        Me.INDGvHealthAdministrator_ThirdParty.Caption = "Tercero"
        Me.INDGvHealthAdministrator_ThirdParty.FieldName = "ThirdPartyId.NitName"
        Me.INDGvHealthAdministrator_ThirdParty.Name = "INDGvHealthAdministrator_ThirdParty"
        Me.INDGvHealthAdministrator_ThirdParty.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_ThirdParty.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_ThirdParty.Visible = True
        Me.INDGvHealthAdministrator_ThirdParty.VisibleIndex = 3
        Me.INDGvHealthAdministrator_ThirdParty.Width = 81
        '
        'INDGvHealthAdministrator_Type
        '
        Me.INDGvHealthAdministrator_Type.Caption = "Tipo Entidad"
        Me.INDGvHealthAdministrator_Type.FieldName = "EntityTypeName"
        Me.INDGvHealthAdministrator_Type.Name = "INDGvHealthAdministrator_Type"
        Me.INDGvHealthAdministrator_Type.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_Type.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_Type.Visible = True
        Me.INDGvHealthAdministrator_Type.VisibleIndex = 4
        Me.INDGvHealthAdministrator_Type.Width = 62
        '
        'INDGvHealthAdministrator_EntityCode
        '
        Me.INDGvHealthAdministrator_EntityCode.Caption = "Código Entidad"
        Me.INDGvHealthAdministrator_EntityCode.FieldName = "HealthEntityCode"
        Me.INDGvHealthAdministrator_EntityCode.Name = "INDGvHealthAdministrator_EntityCode"
        Me.INDGvHealthAdministrator_EntityCode.OptionsColumn.AllowEdit = False
        Me.INDGvHealthAdministrator_EntityCode.OptionsColumn.AllowFocus = False
        Me.INDGvHealthAdministrator_EntityCode.Visible = True
        Me.INDGvHealthAdministrator_EntityCode.VisibleIndex = 5
        Me.INDGvHealthAdministrator_EntityCode.Width = 66
        '
        'INDGleReportType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleReportType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleReportType, False)
        Me.INDGleReportType.EnterMoveNextControl = True
        Me.INDGleReportType.Location = New System.Drawing.Point(149, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleReportType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleReportType.Name = "INDGleReportType"
        Me.INDGleReportType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleReportType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleReportType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleReportType.Properties.Appearance.Options.UseFont = True
        Me.INDGleReportType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleReportType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleReportType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleReportType.Properties.DisplayMember = "Item2"
        Me.INDGleReportType.Properties.NullText = ""
        Me.INDGleReportType.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleReportType.Properties.ValueMember = "Item1"
        Me.INDGleReportType.Size = New System.Drawing.Size(261, 28)
        Me.INDGleReportType.StyleController = Me.INDLcRoot
        Me.INDGleReportType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleReportType, 0)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Reporte"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'INDDeCutoffDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeCutoffDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeCutoffDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeCutoffDate, False)
        Me.INDDeCutoffDate.EditValue = Nothing
        Me.INDDeCutoffDate.EnterMoveNextControl = True
        Me.INDDeCutoffDate.Location = New System.Drawing.Point(149, 117)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeCutoffDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeCutoffDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeCutoffDate.Name = "INDDeCutoffDate"
        Me.INDDeCutoffDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeCutoffDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeCutoffDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeCutoffDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeCutoffDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeCutoffDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeCutoffDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeCutoffDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeCutoffDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeCutoffDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeCutoffDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeCutoffDate.Size = New System.Drawing.Size(261, 28)
        Me.INDDeCutoffDate.StyleController = Me.INDLcRoot
        Me.INDDeCutoffDate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeCutoffDate, 0)
        '
        'INDGleInvoiceType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleInvoiceType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleInvoiceType, False)
        Me.INDGleInvoiceType.EnterMoveNextControl = True
        Me.INDGleInvoiceType.Location = New System.Drawing.Point(149, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleInvoiceType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleInvoiceType.Name = "INDGleInvoiceType"
        Me.INDGleInvoiceType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleInvoiceType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleInvoiceType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleInvoiceType.Properties.Appearance.Options.UseFont = True
        Me.INDGleInvoiceType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleInvoiceType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleInvoiceType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleInvoiceType.Properties.DisplayMember = "Item2"
        Me.INDGleInvoiceType.Properties.NullText = ""
        Me.INDGleInvoiceType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleInvoiceType.Properties.ValueMember = "Item1"
        Me.INDGleInvoiceType.Size = New System.Drawing.Size(261, 28)
        Me.INDGleInvoiceType.StyleController = Me.INDLcRoot
        Me.INDGleInvoiceType.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleInvoiceType, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo Factura"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDDeEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDate, False)
        Me.INDDeEndDate.EditValue = Nothing
        Me.INDDeEndDate.EnterMoveNextControl = True
        Me.INDDeEndDate.Location = New System.Drawing.Point(149, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDate.Name = "INDDeEndDate"
        Me.INDDeEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDate.Size = New System.Drawing.Size(261, 28)
        Me.INDDeEndDate.StyleController = Me.INDLcRoot
        Me.INDDeEndDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeEndDate, 0)
        '
        'INDDeInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInitialDate, False)
        Me.INDDeInitialDate.EditValue = Nothing
        Me.INDDeInitialDate.EnterMoveNextControl = True
        Me.INDDeInitialDate.Location = New System.Drawing.Point(149, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInitialDate.Name = "INDDeInitialDate"
        Me.INDDeInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInitialDate.Size = New System.Drawing.Size(261, 28)
        Me.INDDeInitialDate.StyleController = Me.INDLcRoot
        Me.INDDeInitialDate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInitialDate, 0)
        '
        'INDSleDetailPackage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleDetailPackage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleDetailPackage, False)
        Me.INDSleDetailPackage.EnterMoveNextControl = True
        Me.INDSleDetailPackage.Location = New System.Drawing.Point(149, 181)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleDetailPackage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleDetailPackage.Name = "INDSleDetailPackage"
        Me.INDSleDetailPackage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleDetailPackage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleDetailPackage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleDetailPackage.Properties.Appearance.Options.UseFont = True
        Me.INDSleDetailPackage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleDetailPackage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleDetailPackage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleDetailPackage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleDetailPackage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleDetailPackage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleDetailPackage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDetailPackage.Properties.DataSource = CType(resources.GetObject("INDSleDetailPackage.Properties.DataSource"), Object)
        Me.INDSleDetailPackage.Properties.DisplayMember = "Item2"
        Me.INDSleDetailPackage.Properties.ImmediatePopup = True
        Me.INDSleDetailPackage.Properties.NullText = ""
        Me.INDSleDetailPackage.Properties.PopupView = Me.CtrYesNo2View
        Me.INDSleDetailPackage.Properties.ValueMember = "Item1"
        Me.INDSleDetailPackage.Size = New System.Drawing.Size(261, 28)
        Me.INDSleDetailPackage.StyleController = Me.INDLcRoot
        Me.INDSleDetailPackage.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleDetailPackage, 0)
        '
        'CtrYesNo2View
        '
        Me.CtrYesNo2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo2View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.CtrYesNo2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo2View.Name = "CtrYesNo2View"
        Me.CtrYesNo2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo2View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo2View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo2View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo2View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo2View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1545, 582)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciInitialDate, Me.INDLciInvoiceType, Me.INDLciReportType, Me.INDLciCareGroup, Me.LayoutControlItem9, Me.INDLciEndDate, Me.INDLciHealthAdministrator, Me.INDLciAdmission, Me.INDLciContract, Me.INDLciCareCenter, Me.INDLciInvoiceCategory, Me.INDLciPopulationGroup, Me.INDLciIncomeCause, Me.INDLciTypeRisk, Me.INDLciCutoffDate, Me.INDLciDetailPackage})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 562)
        Me.LayoutControlGroup2.Text = "Filtros"
        '
        'INDLciInitialDate
        '
        Me.INDLciInitialDate.Control = Me.INDDeInitialDate
        Me.INDLciInitialDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciInitialDate.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciInitialDate.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciInitialDate.Name = "INDLciInitialDate"
        Me.INDLciInitialDate.Size = New System.Drawing.Size(390, 32)
        Me.INDLciInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInitialDate.Text = "Fecha Inicial"
        Me.INDLciInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInitialDate.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciInitialDate.TextToControlDistance = 5
        '
        'INDLciInvoiceType
        '
        Me.INDLciInvoiceType.Control = Me.INDGleInvoiceType
        Me.INDLciInvoiceType.Location = New System.Drawing.Point(0, 96)
        Me.INDLciInvoiceType.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceType.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceType.Name = "INDLciInvoiceType"
        Me.INDLciInvoiceType.Size = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInvoiceType.Text = "Tipo de Factura"
        Me.INDLciInvoiceType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInvoiceType.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciInvoiceType.TextToControlDistance = 5
        '
        'INDLciReportType
        '
        Me.INDLciReportType.Control = Me.INDGleReportType
        Me.INDLciReportType.Location = New System.Drawing.Point(0, 160)
        Me.INDLciReportType.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciReportType.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciReportType.Name = "INDLciReportType"
        Me.INDLciReportType.Size = New System.Drawing.Size(390, 32)
        Me.INDLciReportType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReportType.Text = "Tipo de Reporte"
        Me.INDLciReportType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReportType.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciReportType.TextToControlDistance = 5
        '
        'INDLciCareGroup
        '
        Me.INDLciCareGroup.Control = Me.INDSleCareGroup
        Me.INDLciCareGroup.Location = New System.Drawing.Point(0, 288)
        Me.INDLciCareGroup.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.Name = "INDLciCareGroup"
        Me.INDLciCareGroup.Size = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareGroup.Text = "Grupo Atención"
        Me.INDLciCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCareGroup.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciCareGroup.TextToControlDistance = 5
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDBtnFilter
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 480)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(390, 29)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'INDLciEndDate
        '
        Me.INDLciEndDate.Control = Me.INDDeEndDate
        Me.INDLciEndDate.Location = New System.Drawing.Point(0, 32)
        Me.INDLciEndDate.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciEndDate.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciEndDate.Name = "INDLciEndDate"
        Me.INDLciEndDate.Size = New System.Drawing.Size(390, 32)
        Me.INDLciEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndDate.Text = "Fecha Final"
        Me.INDLciEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEndDate.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciEndDate.TextToControlDistance = 5
        '
        'INDLciHealthAdministrator
        '
        Me.INDLciHealthAdministrator.Control = Me.INDSleHealthAdministrator
        Me.INDLciHealthAdministrator.Location = New System.Drawing.Point(0, 256)
        Me.INDLciHealthAdministrator.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciHealthAdministrator.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciHealthAdministrator.Name = "INDLciHealthAdministrator"
        Me.INDLciHealthAdministrator.Size = New System.Drawing.Size(390, 32)
        Me.INDLciHealthAdministrator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHealthAdministrator.Text = "Entidad"
        Me.INDLciHealthAdministrator.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciHealthAdministrator.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciHealthAdministrator.TextToControlDistance = 5
        '
        'INDLciAdmission
        '
        Me.INDLciAdmission.Control = Me.INDSleAdmission
        Me.INDLciAdmission.Location = New System.Drawing.Point(0, 448)
        Me.INDLciAdmission.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciAdmission.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciAdmission.Name = "INDLciAdmission"
        Me.INDLciAdmission.Size = New System.Drawing.Size(390, 32)
        Me.INDLciAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdmission.Text = "Ingreso"
        Me.INDLciAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdmission.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciAdmission.TextToControlDistance = 5
        '
        'INDLciContract
        '
        Me.INDLciContract.Control = Me.INDSleContract
        Me.INDLciContract.Location = New System.Drawing.Point(0, 224)
        Me.INDLciContract.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciContract.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciContract.Name = "INDLciContract"
        Me.INDLciContract.Size = New System.Drawing.Size(390, 32)
        Me.INDLciContract.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciContract.Text = "Contrato"
        Me.INDLciContract.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciContract.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciContract.TextToControlDistance = 5
        '
        'INDLciCareCenter
        '
        Me.INDLciCareCenter.Control = Me.INDSleCareCenter
        Me.INDLciCareCenter.Location = New System.Drawing.Point(0, 192)
        Me.INDLciCareCenter.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareCenter.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareCenter.Name = "INDLciCareCenter"
        Me.INDLciCareCenter.Size = New System.Drawing.Size(390, 32)
        Me.INDLciCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareCenter.Text = "Centro Atención"
        Me.INDLciCareCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCareCenter.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciCareCenter.TextToControlDistance = 5
        '
        'INDLciInvoiceCategory
        '
        Me.INDLciInvoiceCategory.Control = Me.INDSleInvoiceCategory
        Me.INDLciInvoiceCategory.Location = New System.Drawing.Point(0, 320)
        Me.INDLciInvoiceCategory.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceCategory.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceCategory.Name = "INDLciInvoiceCategory"
        Me.INDLciInvoiceCategory.Size = New System.Drawing.Size(390, 32)
        Me.INDLciInvoiceCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInvoiceCategory.Text = "Categoría Factura"
        Me.INDLciInvoiceCategory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInvoiceCategory.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciInvoiceCategory.TextToControlDistance = 5
        '
        'INDLciPopulationGroup
        '
        Me.INDLciPopulationGroup.Control = Me.INDSlePopulationGroup
        Me.INDLciPopulationGroup.Location = New System.Drawing.Point(0, 352)
        Me.INDLciPopulationGroup.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciPopulationGroup.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciPopulationGroup.Name = "INDLciPopulationGroup"
        Me.INDLciPopulationGroup.Size = New System.Drawing.Size(390, 32)
        Me.INDLciPopulationGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPopulationGroup.Text = "Grupo Poblacional"
        Me.INDLciPopulationGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPopulationGroup.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciPopulationGroup.TextToControlDistance = 5
        '
        'INDLciIncomeCause
        '
        Me.INDLciIncomeCause.Control = Me.INDSleIncomeCause
        Me.INDLciIncomeCause.Location = New System.Drawing.Point(0, 384)
        Me.INDLciIncomeCause.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciIncomeCause.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciIncomeCause.Name = "INDLciIncomeCause"
        Me.INDLciIncomeCause.Size = New System.Drawing.Size(390, 32)
        Me.INDLciIncomeCause.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIncomeCause.Text = "Causa Ingreso"
        Me.INDLciIncomeCause.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIncomeCause.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciIncomeCause.TextToControlDistance = 5
        '
        'INDLciTypeRisk
        '
        Me.INDLciTypeRisk.Control = Me.INDSleTypeRisk
        Me.INDLciTypeRisk.Location = New System.Drawing.Point(0, 416)
        Me.INDLciTypeRisk.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciTypeRisk.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciTypeRisk.Name = "INDLciTypeRisk"
        Me.INDLciTypeRisk.Size = New System.Drawing.Size(390, 32)
        Me.INDLciTypeRisk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeRisk.Text = "Tipo de Riesgo"
        Me.INDLciTypeRisk.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTypeRisk.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciTypeRisk.TextToControlDistance = 5
        '
        'INDLciCutoffDate
        '
        Me.INDLciCutoffDate.Control = Me.INDDeCutoffDate
        Me.INDLciCutoffDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciCutoffDate.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciCutoffDate.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciCutoffDate.Name = "INDLciCutoffDate"
        Me.INDLciCutoffDate.Size = New System.Drawing.Size(390, 32)
        Me.INDLciCutoffDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCutoffDate.Text = "Fecha de Corte"
        Me.INDLciCutoffDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCutoffDate.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciCutoffDate.TextToControlDistance = 5
        '
        'INDLciDetailPackage
        '
        Me.INDLciDetailPackage.Control = Me.INDSleDetailPackage
        Me.INDLciDetailPackage.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDetailPackage.CustomizationFormText = "Detallar Paquete"
        Me.INDLciDetailPackage.Location = New System.Drawing.Point(0, 128)
        Me.INDLciDetailPackage.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciDetailPackage.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciDetailPackage.Name = "INDLciDetailPackage"
        Me.INDLciDetailPackage.Size = New System.Drawing.Size(390, 32)
        Me.INDLciDetailPackage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetailPackage.Text = "Detallar Paquete"
        Me.INDLciDetailPackage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetailPackage.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciDetailPackage.TextSize = New System.Drawing.Size(120, 13)
        Me.INDLciDetailPackage.TextToControlDistance = 5
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1111, 562)
        Me.LayoutControlGroup3.Text = "Facturas"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDGcInvoices
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(5, 5)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(1087, 509)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 521)
        Me.CtrNavigationControlPanel1.TabIndex = 3
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'GridViewColumnHeaderExtender1
        '
        Me.GridViewColumnHeaderExtender1.DrawCheckBoxByDefault = False
        Me.GridViewColumnHeaderExtender1.View = Me.GvInvoices
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Selección"
        Me.GridColumn80.FieldName = "Item2"
        Me.GridColumn80.Name = "GridColumn80"
        '
        'FrmRIPSInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1766, 665)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmRIPSInvoice"
        Me.Opacity = 1.0R
        Me.Tag = "775"
        Me.Text = "Generador de RIPS"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDSleTypeRisk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvTypeRisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleIncomeCause.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvIncomeCause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePopulationGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPopulationGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInvoiceCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvInvoiceCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleContract.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRICESel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCareGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleReportType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeCutoffDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeCutoffDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleInvoiceType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDetailPackage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDetailPackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInvoiceCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPopulationGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIncomeCause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeRisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCutoffDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetailPackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDeCutoffDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDGleInvoiceType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDDeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciInvoiceType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCutoffDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleReportType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciReportType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleAdmission As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleCareGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCareGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleHealthAdministrator As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvHealthAdministrator As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciHealthAdministrator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCareGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents GvInvoices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDBtnFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDGvCareGroup_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareGroup_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_ThirdParty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_Type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_EntityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleContract As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvContract As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciContract As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvContract_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContract_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContract_HealthAdministrator As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContract_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContract_Object As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContract_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCareCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvCareCenter_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareCenter_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareCenter_CodeIPS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleInvoiceCategory As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvInvoiceCategory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciInvoiceCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvInvoiceCategory_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoiceCategory_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoiceCategory_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoiceCategory_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleTypeRisk As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvTypeRisk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleIncomeCause As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvIncomeCause As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlePopulationGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvPopulationGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciPopulationGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciIncomeCause As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTypeRisk As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvPopulationGroup_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvPopulationGroup_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvPopulationGroup_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIncomeCause_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIncomeCause_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTypeRisk_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTypeRisk_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESel As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridViewColumnHeaderExtender1 As GridViewColumnHeaderExtender
    Friend WithEvents INDGvCareCenter_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvHealthAdministrator_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareGroup_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIncomeCause_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTypeRisk_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleDetailPackage As CtrYesNo
    Friend WithEvents CtrYesNo2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciDetailPackage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
