Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmWorkOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWorkOrder))
        Me.INDLcWorkOrder = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccWorkOrderNotification = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcElectronicDocumentNotification = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcWorkOrderNotification = New DevExpress.XtraGrid.GridControl()
        Me.INDGvWorkOrderNotification = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvWorkOrderNotification_Type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvWorkOrderNotification_ThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvWorkOrderNotification_Email = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvWorkOrderNotification_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvWorkOrderNotification_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiWorkOrder = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiApprove = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiReject = New DevExpress.XtraBars.BarButtonItem()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciWorkOrderNotification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcMaintenanceToBeEvaluated = New DevExpress.XtraGrid.GridControl()
        Me.INDGvMaintenanceToBeEvaluated = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvMaintenanceToBeEvaluated_Year = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Month = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_SelectOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Protocol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Serie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Model = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Part = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Trademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Location = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_Notification = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceToBeEvaluated_PceNotification = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPccFilter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtreeLocation = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDChkResponsable = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkTipoEquipo = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkTipoInventario = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkTipoResponsable = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkArticulo = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.INDBtnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleActivo = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleArticulo = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleTipoResponsable = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleTipoInventario = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleTipoEquipo = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleResponsable = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleSoloProgramado = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDGcUnscheduledMaintenance = New DevExpress.XtraGrid.GridControl()
        Me.INDGvUnscheduledMaintenance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvUnscheduledMaintenance_Year = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Month = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_ProgramDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Serie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Model = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Part = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Trademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Location = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_BranchOffice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Protocol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Responsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_WorkOrderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_Notification = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvUnscheduledMaintenance_PceNotification = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPceFilters = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDGcScheduledMaintenance = New DevExpress.XtraGrid.GridControl()
        Me.INDGvScheduledMaintenance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvScheduledMaintenance_Year = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Month = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_ProgramDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Serie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Model = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Trademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Location = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_BranchOffice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Protocol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Responsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_WorkOrderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_Notification = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvScheduledMaintenance_PceNotification = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcgWorkOrder = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgWorkOrder = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgMaintenanceToBeEvaluated = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciMaintenanceToBeEvaluated = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgScheduledMaintenance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciScheduledMaintenance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgUnscheduledMaintenance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciUnscheduledMaintenance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDPopMenuActions1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcWorkOrder.SuspendLayout()
        CType(Me.INDPccWorkOrderNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccWorkOrderNotification.SuspendLayout()
        CType(Me.INDLcElectronicDocumentNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcElectronicDocumentNotification.SuspendLayout()
        CType(Me.INDGcWorkOrderNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWorkOrderNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWorkOrderNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvMaintenanceToBeEvaluated_PceNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccFilter.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkTipoEquipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkTipoInventario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkTipoResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkArticulo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleArticulo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTipoResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTipoInventario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTipoEquipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleSoloProgramado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvUnscheduledMaintenance_PceNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcScheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvScheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvScheduledMaintenance_PceNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgScheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciScheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcWorkOrder)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1252, 571)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1252, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1252, 98)
        '
        'INDLcWorkOrder
        '
        Me.INDLcWorkOrder.Controls.Add(Me.INDPccWorkOrderNotification)
        Me.INDLcWorkOrder.Controls.Add(Me.INDGcMaintenanceToBeEvaluated)
        Me.INDLcWorkOrder.Controls.Add(Me.INDPccFilter)
        Me.INDLcWorkOrder.Controls.Add(Me.INDGcUnscheduledMaintenance)
        Me.INDLcWorkOrder.Controls.Add(Me.INDPceFilters)
        Me.INDLcWorkOrder.Controls.Add(Me.INDGcScheduledMaintenance)
        Me.INDLcWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcWorkOrder.Location = New System.Drawing.Point(2, 7)
        Me.INDLcWorkOrder.Name = "INDLcWorkOrder"
        Me.INDLcWorkOrder.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2352, 399, 574, 569)
        Me.INDLcWorkOrder.Root = Me.INDLcgWorkOrder
        Me.INDLcWorkOrder.Size = New System.Drawing.Size(1248, 562)
        Me.INDLcWorkOrder.TabIndex = 5
        Me.INDLcWorkOrder.Text = "LayoutControl2"
        '
        'INDPccWorkOrderNotification
        '
        Me.INDPccWorkOrderNotification.Controls.Add(Me.INDLcElectronicDocumentNotification)
        Me.INDPccWorkOrderNotification.Location = New System.Drawing.Point(380, 421)
        Me.INDPccWorkOrderNotification.Name = "INDPccWorkOrderNotification"
        Me.INDPccWorkOrderNotification.Size = New System.Drawing.Size(858, 128)
        Me.INDPccWorkOrderNotification.TabIndex = 26
        '
        'INDLcElectronicDocumentNotification
        '
        Me.INDLcElectronicDocumentNotification.Controls.Add(Me.INDGcWorkOrderNotification)
        Me.INDLcElectronicDocumentNotification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcElectronicDocumentNotification.Location = New System.Drawing.Point(0, 0)
        Me.INDLcElectronicDocumentNotification.Name = "INDLcElectronicDocumentNotification"
        Me.INDLcElectronicDocumentNotification.Root = Me.Root
        Me.INDLcElectronicDocumentNotification.Size = New System.Drawing.Size(858, 128)
        Me.INDLcElectronicDocumentNotification.TabIndex = 0
        Me.INDLcElectronicDocumentNotification.Text = "LayoutControl1"
        '
        'INDGcWorkOrderNotification
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcWorkOrderNotification, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcWorkOrderNotification, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcWorkOrderNotification, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcWorkOrderNotification, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcWorkOrderNotification, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcWorkOrderNotification, False)
        Me.INDGcWorkOrderNotification.Location = New System.Drawing.Point(12, 12)
        Me.INDGcWorkOrderNotification.MainView = Me.INDGvWorkOrderNotification
        Me.INDGcWorkOrderNotification.MenuManager = Me.BarManager1
        Me.INDGcWorkOrderNotification.Name = "INDGcWorkOrderNotification"
        Me.INDGcWorkOrderNotification.Size = New System.Drawing.Size(834, 104)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcWorkOrderNotification, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcWorkOrderNotification.TabIndex = 4
        Me.INDGcWorkOrderNotification.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvWorkOrderNotification})
        '
        'INDGvWorkOrderNotification
        '
        Me.INDGvWorkOrderNotification.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWorkOrderNotification.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWorkOrderNotification.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWorkOrderNotification.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWorkOrderNotification.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkOrderNotification.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWorkOrderNotification.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkOrderNotification.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWorkOrderNotification.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWorkOrderNotification.Appearance.Row.Options.UseFont = True
        Me.INDGvWorkOrderNotification.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvWorkOrderNotification.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvWorkOrderNotification.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvWorkOrderNotification_Type, Me.INDGvWorkOrderNotification_ThirdParty, Me.INDGvWorkOrderNotification_Email, Me.INDGvWorkOrderNotification_Status, Me.INDGvWorkOrderNotification_ShippingDate})
        Me.INDGvWorkOrderNotification.GridControl = Me.INDGcWorkOrderNotification
        Me.INDGvWorkOrderNotification.Name = "INDGvWorkOrderNotification"
        Me.INDGvWorkOrderNotification.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWorkOrderNotification.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWorkOrderNotification.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWorkOrderNotification.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWorkOrderNotification, False)
        '
        'INDGvWorkOrderNotification_Type
        '
        Me.INDGvWorkOrderNotification_Type.Caption = "Tipo"
        Me.INDGvWorkOrderNotification_Type.FieldName = "TypeName"
        Me.INDGvWorkOrderNotification_Type.Name = "INDGvWorkOrderNotification_Type"
        Me.INDGvWorkOrderNotification_Type.OptionsColumn.AllowEdit = False
        Me.INDGvWorkOrderNotification_Type.OptionsColumn.AllowFocus = False
        Me.INDGvWorkOrderNotification_Type.Visible = True
        Me.INDGvWorkOrderNotification_Type.VisibleIndex = 0
        Me.INDGvWorkOrderNotification_Type.Width = 180
        '
        'INDGvWorkOrderNotification_ThirdParty
        '
        Me.INDGvWorkOrderNotification_ThirdParty.Caption = "Tercero"
        Me.INDGvWorkOrderNotification_ThirdParty.FieldName = "ThirdPartyNitName"
        Me.INDGvWorkOrderNotification_ThirdParty.Name = "INDGvWorkOrderNotification_ThirdParty"
        Me.INDGvWorkOrderNotification_ThirdParty.OptionsColumn.AllowEdit = False
        Me.INDGvWorkOrderNotification_ThirdParty.OptionsColumn.AllowFocus = False
        Me.INDGvWorkOrderNotification_ThirdParty.Visible = True
        Me.INDGvWorkOrderNotification_ThirdParty.VisibleIndex = 1
        Me.INDGvWorkOrderNotification_ThirdParty.Width = 240
        '
        'INDGvWorkOrderNotification_Email
        '
        Me.INDGvWorkOrderNotification_Email.Caption = "Correo"
        Me.INDGvWorkOrderNotification_Email.FieldName = "Email"
        Me.INDGvWorkOrderNotification_Email.Name = "INDGvWorkOrderNotification_Email"
        Me.INDGvWorkOrderNotification_Email.OptionsColumn.AllowEdit = False
        Me.INDGvWorkOrderNotification_Email.OptionsColumn.AllowFocus = False
        Me.INDGvWorkOrderNotification_Email.Visible = True
        Me.INDGvWorkOrderNotification_Email.VisibleIndex = 2
        Me.INDGvWorkOrderNotification_Email.Width = 180
        '
        'INDGvWorkOrderNotification_Status
        '
        Me.INDGvWorkOrderNotification_Status.Caption = "Estado"
        Me.INDGvWorkOrderNotification_Status.FieldName = "StatusName"
        Me.INDGvWorkOrderNotification_Status.Name = "INDGvWorkOrderNotification_Status"
        Me.INDGvWorkOrderNotification_Status.OptionsColumn.AllowEdit = False
        Me.INDGvWorkOrderNotification_Status.OptionsColumn.AllowFocus = False
        Me.INDGvWorkOrderNotification_Status.Visible = True
        Me.INDGvWorkOrderNotification_Status.VisibleIndex = 3
        Me.INDGvWorkOrderNotification_Status.Width = 100
        '
        'INDGvWorkOrderNotification_ShippingDate
        '
        Me.INDGvWorkOrderNotification_ShippingDate.Caption = "Fecha de Envío"
        Me.INDGvWorkOrderNotification_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvWorkOrderNotification_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvWorkOrderNotification_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvWorkOrderNotification_ShippingDate.Name = "INDGvWorkOrderNotification_ShippingDate"
        Me.INDGvWorkOrderNotification_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvWorkOrderNotification_ShippingDate.OptionsColumn.AllowFocus = False
        Me.INDGvWorkOrderNotification_ShippingDate.Visible = True
        Me.INDGvWorkOrderNotification_ShippingDate.VisibleIndex = 4
        Me.INDGvWorkOrderNotification_ShippingDate.Width = 109
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl5)
        Me.BarManager1.DockControls.Add(Me.BarDockControl6)
        Me.BarManager1.DockControls.Add(Me.BarDockControl7)
        Me.BarManager1.DockControls.Add(Me.BarDockControl8)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiWorkOrder, Me.INDBbiApprove, Me.INDBbiReject})
        Me.BarManager1.MaxItemId = 34
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl5.Manager = Me.BarManager1
        Me.BarDockControl5.Size = New System.Drawing.Size(1252, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 693)
        Me.BarDockControl6.Manager = Me.BarManager1
        Me.BarDockControl6.Size = New System.Drawing.Size(1252, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl7.Manager = Me.BarManager1
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 688)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(1252, 5)
        Me.BarDockControl8.Manager = Me.BarManager1
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 688)
        '
        'INDBbiWorkOrder
        '
        Me.INDBbiWorkOrder.Caption = "Orden de Trabajo"
        Me.INDBbiWorkOrder.Id = 19
        Me.INDBbiWorkOrder.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiWorkOrder.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiWorkOrder.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiWorkOrder.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiWorkOrder.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiWorkOrder.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiWorkOrder.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiWorkOrder.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiWorkOrder.Name = "INDBbiWorkOrder"
        '
        'INDBbiApprove
        '
        Me.INDBbiApprove.Caption = "Aprobar"
        Me.INDBbiApprove.Id = 32
        Me.INDBbiApprove.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiApprove.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiApprove.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiApprove.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiApprove.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiApprove.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiApprove.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiApprove.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiApprove.Name = "INDBbiApprove"
        '
        'INDBbiReject
        '
        Me.INDBbiReject.Caption = "Rechazar"
        Me.INDBbiReject.Id = 33
        Me.INDBbiReject.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiReject.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiReject.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiReject.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiReject.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiReject.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiReject.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiReject.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiReject.Name = "INDBbiReject"
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
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciWorkOrderNotification})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(858, 128)
        Me.Root.TextVisible = False
        '
        'INDLciWorkOrderNotification
        '
        Me.INDLciWorkOrderNotification.Control = Me.INDGcWorkOrderNotification
        Me.INDLciWorkOrderNotification.Location = New System.Drawing.Point(0, 0)
        Me.INDLciWorkOrderNotification.Name = "INDLciWorkOrderNotification"
        Me.INDLciWorkOrderNotification.Size = New System.Drawing.Size(838, 108)
        Me.INDLciWorkOrderNotification.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciWorkOrderNotification.TextVisible = False
        '
        'INDGcMaintenanceToBeEvaluated
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcMaintenanceToBeEvaluated, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcMaintenanceToBeEvaluated, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcMaintenanceToBeEvaluated, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcMaintenanceToBeEvaluated, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcMaintenanceToBeEvaluated, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcMaintenanceToBeEvaluated, False)
        Me.INDGcMaintenanceToBeEvaluated.Location = New System.Drawing.Point(14, 143)
        Me.INDGcMaintenanceToBeEvaluated.MainView = Me.INDGvMaintenanceToBeEvaluated
        Me.INDGcMaintenanceToBeEvaluated.MenuManager = Me.BarManager1
        Me.INDGcMaintenanceToBeEvaluated.Name = "INDGcMaintenanceToBeEvaluated"
        Me.INDGcMaintenanceToBeEvaluated.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvMaintenanceToBeEvaluated_PceNotification})
        Me.INDGcMaintenanceToBeEvaluated.Size = New System.Drawing.Size(1220, 405)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcMaintenanceToBeEvaluated, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcMaintenanceToBeEvaluated.TabIndex = 7
        Me.INDGcMaintenanceToBeEvaluated.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvMaintenanceToBeEvaluated})
        '
        'INDGvMaintenanceToBeEvaluated
        '
        Me.INDGvMaintenanceToBeEvaluated.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvMaintenanceToBeEvaluated.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvMaintenanceToBeEvaluated.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvMaintenanceToBeEvaluated.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvMaintenanceToBeEvaluated.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMaintenanceToBeEvaluated.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvMaintenanceToBeEvaluated.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMaintenanceToBeEvaluated.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvMaintenanceToBeEvaluated.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvMaintenanceToBeEvaluated.Appearance.Row.Options.UseFont = True
        Me.INDGvMaintenanceToBeEvaluated.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvMaintenanceToBeEvaluated.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvMaintenanceToBeEvaluated.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvMaintenanceToBeEvaluated_Year, Me.INDGvMaintenanceToBeEvaluated_Month, Me.INDGvMaintenanceToBeEvaluated_SelectOption, Me.INDGvMaintenanceToBeEvaluated_ProgramDate, Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode, Me.INDGvMaintenanceToBeEvaluated_Protocol, Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible, Me.INDGvMaintenanceToBeEvaluated_Plate, Me.INDGvMaintenanceToBeEvaluated_Item, Me.INDGvMaintenanceToBeEvaluated_Serie, Me.INDGvMaintenanceToBeEvaluated_Model, Me.INDGvMaintenanceToBeEvaluated_Part, Me.INDGvMaintenanceToBeEvaluated_Trademark, Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible, Me.INDGvMaintenanceToBeEvaluated_Location, Me.INDGvMaintenanceToBeEvaluated_BranchOffice, Me.INDGvMaintenanceToBeEvaluated_Notification})
        Me.INDGvMaintenanceToBeEvaluated.GridControl = Me.INDGcMaintenanceToBeEvaluated
        Me.INDGvMaintenanceToBeEvaluated.GroupCount = 2
        Me.INDGvMaintenanceToBeEvaluated.Name = "INDGvMaintenanceToBeEvaluated"
        Me.INDGvMaintenanceToBeEvaluated.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDGvMaintenanceToBeEvaluated.OptionsSelection.MultiSelect = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.ShowAutoFilterRow = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.ShowDetailButtons = False
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.ShowFooter = True
        Me.INDGvMaintenanceToBeEvaluated.OptionsView.ShowGroupPanel = False
        Me.INDGvMaintenanceToBeEvaluated.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvMaintenanceToBeEvaluated_Year, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvMaintenanceToBeEvaluated_Month, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvMaintenanceToBeEvaluated_ProgramDate, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvMaintenanceToBeEvaluated, False)
        '
        'INDGvMaintenanceToBeEvaluated_Year
        '
        Me.INDGvMaintenanceToBeEvaluated_Year.Caption = "Año"
        Me.INDGvMaintenanceToBeEvaluated_Year.FieldName = "ProgramDateYear"
        Me.INDGvMaintenanceToBeEvaluated_Year.Name = "INDGvMaintenanceToBeEvaluated_Year"
        Me.INDGvMaintenanceToBeEvaluated_Year.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Year.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Year.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Year.VisibleIndex = 0
        '
        'INDGvMaintenanceToBeEvaluated_Month
        '
        Me.INDGvMaintenanceToBeEvaluated_Month.Caption = "Mes"
        Me.INDGvMaintenanceToBeEvaluated_Month.FieldName = "ProgramDateMonth"
        Me.INDGvMaintenanceToBeEvaluated_Month.Name = "INDGvMaintenanceToBeEvaluated_Month"
        Me.INDGvMaintenanceToBeEvaluated_Month.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Month.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Month.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Month.VisibleIndex = 1
        '
        'INDGvMaintenanceToBeEvaluated_SelectOption
        '
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.Caption = " "
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.FieldName = "SelectOption"
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.Name = "INDGvMaintenanceToBeEvaluated_SelectOption"
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowMove = False
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowShowHide = False
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowSize = False
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsFilter.AllowAutoFilter = False
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.OptionsFilter.AllowFilter = False
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.VisibleIndex = 0
        Me.INDGvMaintenanceToBeEvaluated_SelectOption.Width = 41
        '
        'INDGvMaintenanceToBeEvaluated_ProgramDate
        '
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.Caption = "Fecha Orden"
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.FieldName = "ProgramDate"
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.Name = "INDGvMaintenanceToBeEvaluated_ProgramDate"
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.VisibleIndex = 1
        Me.INDGvMaintenanceToBeEvaluated_ProgramDate.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_WorkOrderCode
        '
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.Caption = "Orden de Trabajo"
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.FieldName = "WorkOrderCode"
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.Name = "INDGvMaintenanceToBeEvaluated_WorkOrderCode"
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.VisibleIndex = 2
        Me.INDGvMaintenanceToBeEvaluated_WorkOrderCode.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_Protocol
        '
        Me.INDGvMaintenanceToBeEvaluated_Protocol.Caption = "Protocolo"
        Me.INDGvMaintenanceToBeEvaluated_Protocol.FieldName = "ProtocolCodeName"
        Me.INDGvMaintenanceToBeEvaluated_Protocol.Name = "INDGvMaintenanceToBeEvaluated_Protocol"
        Me.INDGvMaintenanceToBeEvaluated_Protocol.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Protocol.OptionsColumn.AllowFocus = False
        '
        'INDGvMaintenanceToBeEvaluated_MaintenanceResponsible
        '
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible.Caption = "Responsable Mantenimiento"
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible.FieldName = "MaintenanceResponsibleCodeName"
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible.Name = "INDGvMaintenanceToBeEvaluated_MaintenanceResponsible"
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_MaintenanceResponsible.OptionsColumn.AllowFocus = False
        '
        'INDGvMaintenanceToBeEvaluated_Plate
        '
        Me.INDGvMaintenanceToBeEvaluated_Plate.Caption = "Placa"
        Me.INDGvMaintenanceToBeEvaluated_Plate.FieldName = "Plate"
        Me.INDGvMaintenanceToBeEvaluated_Plate.Name = "INDGvMaintenanceToBeEvaluated_Plate"
        Me.INDGvMaintenanceToBeEvaluated_Plate.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Plate.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Plate.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Plate.VisibleIndex = 3
        Me.INDGvMaintenanceToBeEvaluated_Plate.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_Item
        '
        Me.INDGvMaintenanceToBeEvaluated_Item.Caption = "Artículo"
        Me.INDGvMaintenanceToBeEvaluated_Item.FieldName = "ItemCodeName"
        Me.INDGvMaintenanceToBeEvaluated_Item.Name = "INDGvMaintenanceToBeEvaluated_Item"
        Me.INDGvMaintenanceToBeEvaluated_Item.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Item.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Item.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Item.VisibleIndex = 4
        Me.INDGvMaintenanceToBeEvaluated_Item.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_Serie
        '
        Me.INDGvMaintenanceToBeEvaluated_Serie.Caption = "Serie"
        Me.INDGvMaintenanceToBeEvaluated_Serie.FieldName = "Serie"
        Me.INDGvMaintenanceToBeEvaluated_Serie.Name = "INDGvMaintenanceToBeEvaluated_Serie"
        Me.INDGvMaintenanceToBeEvaluated_Serie.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Serie.OptionsColumn.AllowFocus = False
        '
        'INDGvMaintenanceToBeEvaluated_Model
        '
        Me.INDGvMaintenanceToBeEvaluated_Model.Caption = "Modelo"
        Me.INDGvMaintenanceToBeEvaluated_Model.FieldName = "Model"
        Me.INDGvMaintenanceToBeEvaluated_Model.Name = "INDGvMaintenanceToBeEvaluated_Model"
        Me.INDGvMaintenanceToBeEvaluated_Model.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Model.OptionsColumn.AllowFocus = False
        '
        'INDGvMaintenanceToBeEvaluated_Part
        '
        Me.INDGvMaintenanceToBeEvaluated_Part.Caption = "Parte"
        Me.INDGvMaintenanceToBeEvaluated_Part.FieldName = "PartCodeDescription"
        Me.INDGvMaintenanceToBeEvaluated_Part.Name = "INDGvMaintenanceToBeEvaluated_Part"
        Me.INDGvMaintenanceToBeEvaluated_Part.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Part.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Part.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Part.VisibleIndex = 5
        Me.INDGvMaintenanceToBeEvaluated_Part.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_Trademark
        '
        Me.INDGvMaintenanceToBeEvaluated_Trademark.Caption = "Marca"
        Me.INDGvMaintenanceToBeEvaluated_Trademark.FieldName = "TrademarkCodeName"
        Me.INDGvMaintenanceToBeEvaluated_Trademark.Name = "INDGvMaintenanceToBeEvaluated_Trademark"
        Me.INDGvMaintenanceToBeEvaluated_Trademark.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Trademark.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Trademark.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Trademark.VisibleIndex = 6
        Me.INDGvMaintenanceToBeEvaluated_Trademark.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_FixedAssetResponsible
        '
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible.Caption = "Responsable Activo"
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible.FieldName = "FixedAssetResponsibleCodeName"
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible.Name = "INDGvMaintenanceToBeEvaluated_FixedAssetResponsible"
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_FixedAssetResponsible.OptionsColumn.AllowFocus = False
        '
        'INDGvMaintenanceToBeEvaluated_Location
        '
        Me.INDGvMaintenanceToBeEvaluated_Location.Caption = "Ubicación"
        Me.INDGvMaintenanceToBeEvaluated_Location.FieldName = "LocationCodeName"
        Me.INDGvMaintenanceToBeEvaluated_Location.Name = "INDGvMaintenanceToBeEvaluated_Location"
        Me.INDGvMaintenanceToBeEvaluated_Location.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_Location.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_Location.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Location.VisibleIndex = 7
        Me.INDGvMaintenanceToBeEvaluated_Location.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_BranchOffice
        '
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.Caption = "Sucursal"
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.FieldName = "BranchOfficeCodeName"
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.Name = "INDGvMaintenanceToBeEvaluated_BranchOffice"
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.VisibleIndex = 8
        Me.INDGvMaintenanceToBeEvaluated_BranchOffice.Width = 134
        '
        'INDGvMaintenanceToBeEvaluated_Notification
        '
        Me.INDGvMaintenanceToBeEvaluated_Notification.Caption = "Notificación"
        Me.INDGvMaintenanceToBeEvaluated_Notification.ColumnEdit = Me.INDGvMaintenanceToBeEvaluated_PceNotification
        Me.INDGvMaintenanceToBeEvaluated_Notification.Name = "INDGvMaintenanceToBeEvaluated_Notification"
        Me.INDGvMaintenanceToBeEvaluated_Notification.Visible = True
        Me.INDGvMaintenanceToBeEvaluated_Notification.VisibleIndex = 9
        Me.INDGvMaintenanceToBeEvaluated_Notification.Width = 80
        '
        'INDGvMaintenanceToBeEvaluated_PceNotification
        '
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.AutoHeight = False
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.Name = "INDGvMaintenanceToBeEvaluated_PceNotification"
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.PopupControl = Me.INDPccWorkOrderNotification
        Me.INDGvMaintenanceToBeEvaluated_PceNotification.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPccFilter
        '
        Me.INDPccFilter.Controls.Add(Me.PanelControl1)
        Me.INDPccFilter.Location = New System.Drawing.Point(93, 297)
        Me.INDPccFilter.Name = "INDPccFilter"
        Me.INDPccFilter.Size = New System.Drawing.Size(944, 209)
        Me.INDPccFilter.TabIndex = 3
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.LayoutControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(944, 209)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtreeLocation)
        Me.LayoutControl1.Controls.Add(Me.INDChkResponsable)
        Me.LayoutControl1.Controls.Add(Me.INDChkTipoEquipo)
        Me.LayoutControl1.Controls.Add(Me.INDChkTipoInventario)
        Me.LayoutControl1.Controls.Add(Me.INDChkTipoResponsable)
        Me.LayoutControl1.Controls.Add(Me.INDChkArticulo)
        Me.LayoutControl1.Controls.Add(Me.INDChkActivo)
        Me.LayoutControl1.Controls.Add(Me.INDChkUbicacion)
        Me.LayoutControl1.Controls.Add(Me.INDBtnAplicar)
        Me.LayoutControl1.Controls.Add(Me.INDSleActivo)
        Me.LayoutControl1.Controls.Add(Me.INDSleArticulo)
        Me.LayoutControl1.Controls.Add(Me.INDSleTipoResponsable)
        Me.LayoutControl1.Controls.Add(Me.INDSleTipoInventario)
        Me.LayoutControl1.Controls.Add(Me.INDSleTipoEquipo)
        Me.LayoutControl1.Controls.Add(Me.INDSleResponsable)
        Me.LayoutControl1.Controls.Add(Me.INDgleSoloProgramado)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2352, 399, 574, 569)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(944, 209)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDtreeLocation
        '
        Me.INDtreeLocation.EnterMoveNextControl = True
        Me.INDtreeLocation.Location = New System.Drawing.Point(123, 43)
        Me.INDtreeLocation.Name = "INDtreeLocation"
        Me.INDtreeLocation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtreeLocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDtreeLocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDtreeLocation.Properties.Appearance.Options.UseFont = True
        Me.INDtreeLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtreeLocation.Properties.DisplayMember = "CodeName"
        Me.INDtreeLocation.Properties.NullText = ""
        Me.INDtreeLocation.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.INDtreeLocation.Properties.ValueMember = "Id"
        Me.INDtreeLocation.Size = New System.Drawing.Size(277, 24)
        Me.INDtreeLocation.StyleController = Me.LayoutControl1
        Me.INDtreeLocation.TabIndex = 0
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1})
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(2, -5)
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Matches
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "PadreId"
        Me.TreeListLookUpEdit1TreeList.Size = New System.Drawing.Size(400, 200)
        Me.TreeListLookUpEdit1TreeList.TabIndex = 0
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        '
        'INDChkResponsable
        '
        Me.INDChkResponsable.EditValue = True
        Me.INDChkResponsable.Location = New System.Drawing.Point(864, 99)
        Me.INDChkResponsable.Name = "INDChkResponsable"
        Me.INDChkResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDChkResponsable.Properties.Caption = "Todos"
        Me.INDChkResponsable.Size = New System.Drawing.Size(66, 21)
        Me.INDChkResponsable.StyleController = Me.LayoutControl1
        Me.INDChkResponsable.TabIndex = 19
        '
        'INDChkTipoEquipo
        '
        Me.INDChkTipoEquipo.EditValue = True
        Me.INDChkTipoEquipo.Location = New System.Drawing.Point(864, 71)
        Me.INDChkTipoEquipo.Name = "INDChkTipoEquipo"
        Me.INDChkTipoEquipo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkTipoEquipo.Properties.Appearance.Options.UseFont = True
        Me.INDChkTipoEquipo.Properties.Caption = "Todos"
        Me.INDChkTipoEquipo.Size = New System.Drawing.Size(66, 21)
        Me.INDChkTipoEquipo.StyleController = Me.LayoutControl1
        Me.INDChkTipoEquipo.TabIndex = 18
        '
        'INDChkTipoInventario
        '
        Me.INDChkTipoInventario.EditValue = True
        Me.INDChkTipoInventario.Location = New System.Drawing.Point(864, 43)
        Me.INDChkTipoInventario.Name = "INDChkTipoInventario"
        Me.INDChkTipoInventario.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkTipoInventario.Properties.Appearance.Options.UseFont = True
        Me.INDChkTipoInventario.Properties.Caption = "Todos"
        Me.INDChkTipoInventario.Size = New System.Drawing.Size(66, 21)
        Me.INDChkTipoInventario.StyleController = Me.LayoutControl1
        Me.INDChkTipoInventario.TabIndex = 17
        '
        'INDChkTipoResponsable
        '
        Me.INDChkTipoResponsable.EditValue = True
        Me.INDChkTipoResponsable.Location = New System.Drawing.Point(404, 127)
        Me.INDChkTipoResponsable.Name = "INDChkTipoResponsable"
        Me.INDChkTipoResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkTipoResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDChkTipoResponsable.Properties.Caption = "Todos"
        Me.INDChkTipoResponsable.Size = New System.Drawing.Size(66, 21)
        Me.INDChkTipoResponsable.StyleController = Me.LayoutControl1
        Me.INDChkTipoResponsable.TabIndex = 16
        '
        'INDChkArticulo
        '
        Me.INDChkArticulo.EditValue = True
        Me.INDChkArticulo.Location = New System.Drawing.Point(404, 99)
        Me.INDChkArticulo.Name = "INDChkArticulo"
        Me.INDChkArticulo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkArticulo.Properties.Appearance.Options.UseFont = True
        Me.INDChkArticulo.Properties.Caption = "Todos"
        Me.INDChkArticulo.Size = New System.Drawing.Size(66, 21)
        Me.INDChkArticulo.StyleController = Me.LayoutControl1
        Me.INDChkArticulo.TabIndex = 15
        '
        'INDChkActivo
        '
        Me.INDChkActivo.EditValue = True
        Me.INDChkActivo.Location = New System.Drawing.Point(404, 71)
        Me.INDChkActivo.Name = "INDChkActivo"
        Me.INDChkActivo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkActivo.Properties.Appearance.Options.UseFont = True
        Me.INDChkActivo.Properties.Caption = "Todos"
        Me.INDChkActivo.Size = New System.Drawing.Size(66, 21)
        Me.INDChkActivo.StyleController = Me.LayoutControl1
        Me.INDChkActivo.TabIndex = 14
        '
        'INDChkUbicacion
        '
        Me.INDChkUbicacion.EditValue = True
        Me.INDChkUbicacion.Location = New System.Drawing.Point(404, 43)
        Me.INDChkUbicacion.Name = "INDChkUbicacion"
        Me.INDChkUbicacion.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkUbicacion.Properties.Appearance.Options.UseFont = True
        Me.INDChkUbicacion.Properties.Caption = "Todos"
        Me.INDChkUbicacion.Size = New System.Drawing.Size(66, 21)
        Me.INDChkUbicacion.StyleController = Me.LayoutControl1
        Me.INDChkUbicacion.TabIndex = 13
        '
        'INDBtnAplicar
        '
        Me.INDBtnAplicar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnAplicar.Appearance.Options.UseFont = True
        Me.INDBtnAplicar.Location = New System.Drawing.Point(863, 167)
        Me.INDBtnAplicar.Name = "INDBtnAplicar"
        Me.INDBtnAplicar.Size = New System.Drawing.Size(79, 28)
        Me.INDBtnAplicar.StyleController = Me.LayoutControl1
        Me.INDBtnAplicar.TabIndex = 8
        Me.INDBtnAplicar.Text = "Aplicar"
        '
        'INDSleActivo
        '
        Me.INDSleActivo.EnterMoveNextControl = True
        Me.INDSleActivo.Location = New System.Drawing.Point(123, 71)
        Me.INDSleActivo.Name = "INDSleActivo"
        Me.INDSleActivo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleActivo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleActivo.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleActivo.Properties.Appearance.Options.UseFont = True
        Me.INDSleActivo.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleActivo.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleActivo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleActivo.Properties.DisplayMember = "Plate"
        Me.INDSleActivo.Properties.NullText = ""
        Me.INDSleActivo.Properties.PopupView = Me.GridView2
        Me.INDSleActivo.Properties.ValueMember = "Id"
        Me.INDSleActivo.Size = New System.Drawing.Size(277, 24)
        Me.INDSleActivo.StyleController = Me.LayoutControl1
        Me.INDSleActivo.TabIndex = 2
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
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13, Me.GridColumn14, Me.GridColumn15})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Placa"
        Me.GridColumn13.FieldName = "Plate"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        Me.GridColumn13.Width = 147
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Descripción"
        Me.GridColumn14.FieldName = "ItemId.Description"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 1
        Me.GridColumn14.Width = 370
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Serie"
        Me.GridColumn15.FieldName = "Serie"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 2
        Me.GridColumn15.Width = 179
        '
        'INDSleArticulo
        '
        Me.INDSleArticulo.EnterMoveNextControl = True
        Me.INDSleArticulo.Location = New System.Drawing.Point(123, 99)
        Me.INDSleArticulo.Name = "INDSleArticulo"
        Me.INDSleArticulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleArticulo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleArticulo.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleArticulo.Properties.Appearance.Options.UseFont = True
        Me.INDSleArticulo.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleArticulo.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleArticulo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleArticulo.Properties.DisplayMember = "CodeDescription"
        Me.INDSleArticulo.Properties.NullText = ""
        Me.INDSleArticulo.Properties.PopupView = Me.GridView3
        Me.INDSleArticulo.Properties.ValueMember = "Id"
        Me.INDSleArticulo.Size = New System.Drawing.Size(277, 24)
        Me.INDSleArticulo.StyleController = Me.LayoutControl1
        Me.INDSleArticulo.TabIndex = 4
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
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn17})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "Code"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 358
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Descripción"
        Me.GridColumn17.FieldName = "Description"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 863
        '
        'INDSleTipoResponsable
        '
        Me.INDSleTipoResponsable.EnterMoveNextControl = True
        Me.INDSleTipoResponsable.Location = New System.Drawing.Point(123, 127)
        Me.INDSleTipoResponsable.Name = "INDSleTipoResponsable"
        Me.INDSleTipoResponsable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleTipoResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleTipoResponsable.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTipoResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDSleTipoResponsable.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleTipoResponsable.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleTipoResponsable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTipoResponsable.Properties.DisplayMember = "CodeDescription"
        Me.INDSleTipoResponsable.Properties.NullText = ""
        Me.INDSleTipoResponsable.Properties.PopupView = Me.GridView4
        Me.INDSleTipoResponsable.Properties.ValueMember = "Id"
        Me.INDSleTipoResponsable.Size = New System.Drawing.Size(277, 24)
        Me.INDSleTipoResponsable.StyleController = Me.LayoutControl1
        Me.INDSleTipoResponsable.TabIndex = 6
        '
        'GridView4
        '
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
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18, Me.GridColumn19})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Código"
        Me.GridColumn18.FieldName = "Code"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        Me.GridColumn18.Width = 342
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Descripción"
        Me.GridColumn19.FieldName = "Description"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 1
        Me.GridColumn19.Width = 879
        '
        'INDSleTipoInventario
        '
        Me.INDSleTipoInventario.EnterMoveNextControl = True
        Me.INDSleTipoInventario.Location = New System.Drawing.Point(583, 43)
        Me.INDSleTipoInventario.Name = "INDSleTipoInventario"
        Me.INDSleTipoInventario.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleTipoInventario.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleTipoInventario.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTipoInventario.Properties.Appearance.Options.UseFont = True
        Me.INDSleTipoInventario.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleTipoInventario.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleTipoInventario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTipoInventario.Properties.DisplayMember = "CodeName"
        Me.INDSleTipoInventario.Properties.NullText = ""
        Me.INDSleTipoInventario.Properties.PopupView = Me.GridView5
        Me.INDSleTipoInventario.Properties.ValueMember = "Id"
        Me.INDSleTipoInventario.Size = New System.Drawing.Size(277, 24)
        Me.INDSleTipoInventario.StyleController = Me.LayoutControl1
        Me.INDSleTipoInventario.TabIndex = 1
        '
        'GridView5
        '
        Me.GridView5.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView5.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView5.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView5.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView5.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.GroupRow.Options.UseFont = True
        Me.GridView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn20, Me.GridColumn21})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView5.OptionsView.EnableAppearanceOddRow = True
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView5, False)
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Código"
        Me.GridColumn20.FieldName = "Code"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 0
        Me.GridColumn20.Width = 355
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Nombre"
        Me.GridColumn21.FieldName = "Name"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 1
        Me.GridColumn21.Width = 866
        '
        'INDSleTipoEquipo
        '
        Me.INDSleTipoEquipo.EnterMoveNextControl = True
        Me.INDSleTipoEquipo.Location = New System.Drawing.Point(583, 71)
        Me.INDSleTipoEquipo.Name = "INDSleTipoEquipo"
        Me.INDSleTipoEquipo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleTipoEquipo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleTipoEquipo.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTipoEquipo.Properties.Appearance.Options.UseFont = True
        Me.INDSleTipoEquipo.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleTipoEquipo.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleTipoEquipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTipoEquipo.Properties.DisplayMember = "CodeName"
        Me.INDSleTipoEquipo.Properties.NullText = ""
        Me.INDSleTipoEquipo.Properties.PopupView = Me.GridView6
        Me.INDSleTipoEquipo.Properties.ValueMember = "Id"
        Me.INDSleTipoEquipo.Size = New System.Drawing.Size(277, 24)
        Me.INDSleTipoEquipo.StyleController = Me.LayoutControl1
        Me.INDSleTipoEquipo.TabIndex = 3
        '
        'GridView6
        '
        Me.GridView6.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView6.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView6.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView6.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView6.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView6.Appearance.GroupRow.Options.UseFont = True
        Me.GridView6.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView6.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView6.Appearance.Row.Options.UseFont = True
        Me.GridView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22, Me.GridColumn23})
        Me.GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView6.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView6.OptionsView.EnableAppearanceOddRow = True
        Me.GridView6.OptionsView.ShowAutoFilterRow = True
        Me.GridView6.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView6, False)
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Código"
        Me.GridColumn22.FieldName = "Code"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        Me.GridColumn22.Width = 354
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Nombre"
        Me.GridColumn23.FieldName = "Name"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 1
        Me.GridColumn23.Width = 867
        '
        'INDSleResponsable
        '
        Me.INDSleResponsable.EnterMoveNextControl = True
        Me.INDSleResponsable.Location = New System.Drawing.Point(583, 99)
        Me.INDSleResponsable.Name = "INDSleResponsable"
        Me.INDSleResponsable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleResponsable.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDSleResponsable.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleResponsable.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleResponsable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleResponsable.Properties.DisplayMember = "CodeNitName"
        Me.INDSleResponsable.Properties.NullText = ""
        Me.INDSleResponsable.Properties.PopupView = Me.GridView7
        Me.INDSleResponsable.Properties.ValueMember = "Id"
        Me.INDSleResponsable.Size = New System.Drawing.Size(277, 24)
        Me.INDSleResponsable.StyleController = Me.LayoutControl1
        Me.INDSleResponsable.TabIndex = 5
        '
        'GridView7
        '
        Me.GridView7.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView7.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView7.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView7.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView7.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.GroupRow.Options.UseFont = True
        Me.GridView7.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView7.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView7.Appearance.Row.Options.UseFont = True
        Me.GridView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25})
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView7.OptionsView.EnableAppearanceOddRow = True
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView7, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Código"
        Me.GridColumn24.FieldName = "Code"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 0
        Me.GridColumn24.Width = 331
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Tercero"
        Me.GridColumn25.FieldName = "ThirdPartyId.NitName"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 890
        '
        'INDgleSoloProgramado
        '
        Me.INDgleSoloProgramado.Location = New System.Drawing.Point(583, 127)
        Me.INDgleSoloProgramado.Name = "INDgleSoloProgramado"
        Me.INDgleSoloProgramado.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleSoloProgramado.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDgleSoloProgramado.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleSoloProgramado.Properties.Appearance.Options.UseFont = True
        Me.INDgleSoloProgramado.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleSoloProgramado.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleSoloProgramado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleSoloProgramado.Properties.DisplayMember = "Item2"
        Me.INDgleSoloProgramado.Properties.NullText = ""
        Me.INDgleSoloProgramado.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDgleSoloProgramado.Properties.ValueMember = "Item1"
        Me.INDgleSoloProgramado.Size = New System.Drawing.Size(277, 24)
        Me.INDgleSoloProgramado.StyleController = Me.LayoutControl1
        Me.INDgleSoloProgramado.TabIndex = 7
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn26})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Solo Programado"
        Me.GridColumn26.FieldName = "Item2"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 0
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlGroup2, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(944, 209)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDBtnAplicar
        Me.LayoutControlItem9.Location = New System.Drawing.Point(861, 165)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(83, 32)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(83, 32)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(83, 44)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem18, Me.LayoutControlItem12, Me.LayoutControlItem13, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem14, Me.LayoutControlItem15, Me.LayoutControlItem16})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(944, 165)
        Me.LayoutControlGroup2.Text = "Filtros"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.INDSleActivo
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 28)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Activo Fijo"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.INDSleTipoInventario
        Me.LayoutControlItem3.Location = New System.Drawing.Point(460, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Tipo Inventario"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.INDSleTipoEquipo
        Me.LayoutControlItem4.Location = New System.Drawing.Point(460, 28)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Tipo Equipo"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDSleArticulo
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 56)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Artículo"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.Control = Me.INDSleResponsable
        Me.LayoutControlItem6.Location = New System.Drawing.Point(460, 56)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Responsable"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.INDSleTipoResponsable
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 84)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Tipo Responsable"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.INDgleSoloProgramado
        Me.LayoutControlItem8.Location = New System.Drawing.Point(460, 84)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(460, 28)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Solo Programado"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(106, 17)
        Me.LayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.INDtreeLocation
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.Text = "Ubicación"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(106, 17)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDChkArticulo
        Me.LayoutControlItem12.Location = New System.Drawing.Point(390, 56)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDChkTipoResponsable
        Me.LayoutControlItem13.Location = New System.Drawing.Point(390, 84)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDChkUbicacion
        Me.LayoutControlItem10.Location = New System.Drawing.Point(390, 0)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.INDChkActivo
        Me.LayoutControlItem11.Location = New System.Drawing.Point(390, 28)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.INDChkTipoInventario
        Me.LayoutControlItem14.Location = New System.Drawing.Point(850, 0)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.INDChkTipoEquipo
        Me.LayoutControlItem15.Location = New System.Drawing.Point(850, 28)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.INDChkResponsable
        Me.LayoutControlItem16.Location = New System.Drawing.Point(850, 56)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 165)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(861, 44)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDGcUnscheduledMaintenance
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcUnscheduledMaintenance, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcUnscheduledMaintenance, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcUnscheduledMaintenance, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcUnscheduledMaintenance, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcUnscheduledMaintenance, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcUnscheduledMaintenance, False)
        Me.INDGcUnscheduledMaintenance.Location = New System.Drawing.Point(14, 143)
        Me.INDGcUnscheduledMaintenance.MainView = Me.INDGvUnscheduledMaintenance
        Me.INDGcUnscheduledMaintenance.MenuManager = Me.BarManager1
        Me.INDGcUnscheduledMaintenance.Name = "INDGcUnscheduledMaintenance"
        Me.INDGcUnscheduledMaintenance.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvUnscheduledMaintenance_PceNotification})
        Me.INDGcUnscheduledMaintenance.Size = New System.Drawing.Size(1220, 405)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcUnscheduledMaintenance, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcUnscheduledMaintenance.TabIndex = 6
        Me.INDGcUnscheduledMaintenance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvUnscheduledMaintenance})
        '
        'INDGvUnscheduledMaintenance
        '
        Me.INDGvUnscheduledMaintenance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvUnscheduledMaintenance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvUnscheduledMaintenance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvUnscheduledMaintenance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvUnscheduledMaintenance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvUnscheduledMaintenance.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvUnscheduledMaintenance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvUnscheduledMaintenance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvUnscheduledMaintenance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvUnscheduledMaintenance.Appearance.Row.Options.UseFont = True
        Me.INDGvUnscheduledMaintenance.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvUnscheduledMaintenance.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvUnscheduledMaintenance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvUnscheduledMaintenance_Year, Me.INDGvUnscheduledMaintenance_Month, Me.INDGvUnscheduledMaintenance_ProgramDate, Me.INDGvUnscheduledMaintenance_Plate, Me.INDGvUnscheduledMaintenance_Item, Me.INDGvUnscheduledMaintenance_Serie, Me.INDGvUnscheduledMaintenance_Model, Me.INDGvUnscheduledMaintenance_Part, Me.INDGvUnscheduledMaintenance_Trademark, Me.INDGvUnscheduledMaintenance_Location, Me.INDGvUnscheduledMaintenance_BranchOffice, Me.INDGvUnscheduledMaintenance_Protocol, Me.INDGvUnscheduledMaintenance_Responsible, Me.INDGvUnscheduledMaintenance_WorkOrderCode, Me.INDGvUnscheduledMaintenance_Notification})
        Me.INDGvUnscheduledMaintenance.GridControl = Me.INDGcUnscheduledMaintenance
        Me.INDGvUnscheduledMaintenance.GroupCount = 2
        Me.INDGvUnscheduledMaintenance.Name = "INDGvUnscheduledMaintenance"
        Me.INDGvUnscheduledMaintenance.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvUnscheduledMaintenance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvUnscheduledMaintenance.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvUnscheduledMaintenance.OptionsView.ShowAutoFilterRow = True
        Me.INDGvUnscheduledMaintenance.OptionsView.ShowFooter = True
        Me.INDGvUnscheduledMaintenance.OptionsView.ShowGroupPanel = False
        Me.INDGvUnscheduledMaintenance.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvUnscheduledMaintenance_Year, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvUnscheduledMaintenance_Month, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvUnscheduledMaintenance_ProgramDate, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvUnscheduledMaintenance, False)
        '
        'INDGvUnscheduledMaintenance_Year
        '
        Me.INDGvUnscheduledMaintenance_Year.Caption = "Año"
        Me.INDGvUnscheduledMaintenance_Year.FieldName = "ProgramDateYear"
        Me.INDGvUnscheduledMaintenance_Year.Name = "INDGvUnscheduledMaintenance_Year"
        Me.INDGvUnscheduledMaintenance_Year.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Year.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Year.Visible = True
        Me.INDGvUnscheduledMaintenance_Year.VisibleIndex = 0
        '
        'INDGvUnscheduledMaintenance_Month
        '
        Me.INDGvUnscheduledMaintenance_Month.Caption = "Mes"
        Me.INDGvUnscheduledMaintenance_Month.FieldName = "ProgramDateMonth"
        Me.INDGvUnscheduledMaintenance_Month.Name = "INDGvUnscheduledMaintenance_Month"
        Me.INDGvUnscheduledMaintenance_Month.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Month.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Month.Visible = True
        Me.INDGvUnscheduledMaintenance_Month.VisibleIndex = 0
        '
        'INDGvUnscheduledMaintenance_ProgramDate
        '
        Me.INDGvUnscheduledMaintenance_ProgramDate.Caption = "Fecha Orden"
        Me.INDGvUnscheduledMaintenance_ProgramDate.FieldName = "ProgramDate"
        Me.INDGvUnscheduledMaintenance_ProgramDate.Name = "INDGvUnscheduledMaintenance_ProgramDate"
        Me.INDGvUnscheduledMaintenance_ProgramDate.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_ProgramDate.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_ProgramDate.Visible = True
        Me.INDGvUnscheduledMaintenance_ProgramDate.VisibleIndex = 0
        Me.INDGvUnscheduledMaintenance_ProgramDate.Width = 115
        '
        'INDGvUnscheduledMaintenance_Plate
        '
        Me.INDGvUnscheduledMaintenance_Plate.Caption = "Placa"
        Me.INDGvUnscheduledMaintenance_Plate.FieldName = "Plate"
        Me.INDGvUnscheduledMaintenance_Plate.Name = "INDGvUnscheduledMaintenance_Plate"
        Me.INDGvUnscheduledMaintenance_Plate.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Plate.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Plate.Visible = True
        Me.INDGvUnscheduledMaintenance_Plate.VisibleIndex = 1
        Me.INDGvUnscheduledMaintenance_Plate.Width = 116
        '
        'INDGvUnscheduledMaintenance_Item
        '
        Me.INDGvUnscheduledMaintenance_Item.Caption = "Artículo"
        Me.INDGvUnscheduledMaintenance_Item.FieldName = "ItemCodeName"
        Me.INDGvUnscheduledMaintenance_Item.Name = "INDGvUnscheduledMaintenance_Item"
        Me.INDGvUnscheduledMaintenance_Item.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Item.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Item.Visible = True
        Me.INDGvUnscheduledMaintenance_Item.VisibleIndex = 2
        Me.INDGvUnscheduledMaintenance_Item.Width = 251
        '
        'INDGvUnscheduledMaintenance_Serie
        '
        Me.INDGvUnscheduledMaintenance_Serie.Caption = "Serie"
        Me.INDGvUnscheduledMaintenance_Serie.FieldName = "Serie"
        Me.INDGvUnscheduledMaintenance_Serie.Name = "INDGvUnscheduledMaintenance_Serie"
        Me.INDGvUnscheduledMaintenance_Serie.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Serie.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Serie.Width = 124
        '
        'INDGvUnscheduledMaintenance_Model
        '
        Me.INDGvUnscheduledMaintenance_Model.Caption = "Modelo"
        Me.INDGvUnscheduledMaintenance_Model.FieldName = "Model"
        Me.INDGvUnscheduledMaintenance_Model.Name = "INDGvUnscheduledMaintenance_Model"
        Me.INDGvUnscheduledMaintenance_Model.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Model.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Model.Width = 54
        '
        'INDGvUnscheduledMaintenance_Part
        '
        Me.INDGvUnscheduledMaintenance_Part.Caption = "Parte"
        Me.INDGvUnscheduledMaintenance_Part.FieldName = "PartCodeDescription"
        Me.INDGvUnscheduledMaintenance_Part.Name = "INDGvUnscheduledMaintenance_Part"
        Me.INDGvUnscheduledMaintenance_Part.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Part.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Part.Visible = True
        Me.INDGvUnscheduledMaintenance_Part.VisibleIndex = 3
        Me.INDGvUnscheduledMaintenance_Part.Width = 115
        '
        'INDGvUnscheduledMaintenance_Trademark
        '
        Me.INDGvUnscheduledMaintenance_Trademark.Caption = "Marca"
        Me.INDGvUnscheduledMaintenance_Trademark.FieldName = "TrademarkCodeName"
        Me.INDGvUnscheduledMaintenance_Trademark.Name = "INDGvUnscheduledMaintenance_Trademark"
        Me.INDGvUnscheduledMaintenance_Trademark.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Trademark.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Trademark.Visible = True
        Me.INDGvUnscheduledMaintenance_Trademark.VisibleIndex = 4
        Me.INDGvUnscheduledMaintenance_Trademark.Width = 145
        '
        'INDGvUnscheduledMaintenance_Location
        '
        Me.INDGvUnscheduledMaintenance_Location.Caption = "Ubicación"
        Me.INDGvUnscheduledMaintenance_Location.FieldName = "LocationCodeName"
        Me.INDGvUnscheduledMaintenance_Location.Name = "INDGvUnscheduledMaintenance_Location"
        Me.INDGvUnscheduledMaintenance_Location.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Location.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_Location.Visible = True
        Me.INDGvUnscheduledMaintenance_Location.VisibleIndex = 5
        Me.INDGvUnscheduledMaintenance_Location.Width = 219
        '
        'INDGvUnscheduledMaintenance_BranchOffice
        '
        Me.INDGvUnscheduledMaintenance_BranchOffice.Caption = "Sucursal"
        Me.INDGvUnscheduledMaintenance_BranchOffice.FieldName = "BranchOfficeCodeName"
        Me.INDGvUnscheduledMaintenance_BranchOffice.Name = "INDGvUnscheduledMaintenance_BranchOffice"
        Me.INDGvUnscheduledMaintenance_BranchOffice.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_BranchOffice.OptionsColumn.AllowFocus = False
        Me.INDGvUnscheduledMaintenance_BranchOffice.Visible = True
        Me.INDGvUnscheduledMaintenance_BranchOffice.VisibleIndex = 6
        Me.INDGvUnscheduledMaintenance_BranchOffice.Width = 115
        '
        'INDGvUnscheduledMaintenance_Protocol
        '
        Me.INDGvUnscheduledMaintenance_Protocol.Caption = "Protocolo"
        Me.INDGvUnscheduledMaintenance_Protocol.FieldName = "ProtocolCodeName"
        Me.INDGvUnscheduledMaintenance_Protocol.Name = "INDGvUnscheduledMaintenance_Protocol"
        Me.INDGvUnscheduledMaintenance_Protocol.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Protocol.OptionsColumn.AllowFocus = False
        '
        'INDGvUnscheduledMaintenance_Responsible
        '
        Me.INDGvUnscheduledMaintenance_Responsible.Caption = "Responsable"
        Me.INDGvUnscheduledMaintenance_Responsible.FieldName = "ResponsibleCodeName"
        Me.INDGvUnscheduledMaintenance_Responsible.Name = "INDGvUnscheduledMaintenance_Responsible"
        Me.INDGvUnscheduledMaintenance_Responsible.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_Responsible.OptionsColumn.AllowFocus = False
        '
        'INDGvUnscheduledMaintenance_WorkOrderCode
        '
        Me.INDGvUnscheduledMaintenance_WorkOrderCode.Caption = "Orden de Trabajo"
        Me.INDGvUnscheduledMaintenance_WorkOrderCode.FieldName = "WorkOrderCode"
        Me.INDGvUnscheduledMaintenance_WorkOrderCode.Name = "INDGvUnscheduledMaintenance_WorkOrderCode"
        Me.INDGvUnscheduledMaintenance_WorkOrderCode.OptionsColumn.AllowEdit = False
        Me.INDGvUnscheduledMaintenance_WorkOrderCode.OptionsColumn.AllowFocus = False
        '
        'INDGvUnscheduledMaintenance_Notification
        '
        Me.INDGvUnscheduledMaintenance_Notification.Caption = "Notificación"
        Me.INDGvUnscheduledMaintenance_Notification.ColumnEdit = Me.INDGvUnscheduledMaintenance_PceNotification
        Me.INDGvUnscheduledMaintenance_Notification.Name = "INDGvUnscheduledMaintenance_Notification"
        Me.INDGvUnscheduledMaintenance_Notification.Visible = True
        Me.INDGvUnscheduledMaintenance_Notification.VisibleIndex = 7
        Me.INDGvUnscheduledMaintenance_Notification.Width = 80
        '
        'INDGvUnscheduledMaintenance_PceNotification
        '
        Me.INDGvUnscheduledMaintenance_PceNotification.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvUnscheduledMaintenance_PceNotification.AutoHeight = False
        Me.INDGvUnscheduledMaintenance_PceNotification.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvUnscheduledMaintenance_PceNotification.Name = "INDGvUnscheduledMaintenance_PceNotification"
        Me.INDGvUnscheduledMaintenance_PceNotification.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPceFilters
        '
        Me.INDPceFilters.Location = New System.Drawing.Point(14, 43)
        Me.INDPceFilters.Name = "INDPceFilters"
        Me.INDPceFilters.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.INDPceFilters.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPceFilters.Properties.Appearance.Options.UseFont = True
        Me.INDPceFilters.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceFilters.Properties.AutoHeight = False
        Me.INDPceFilters.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceFilters.Properties.PopupControl = Me.INDPccFilter
        Me.INDPceFilters.Size = New System.Drawing.Size(1220, 41)
        Me.INDPceFilters.StyleController = Me.INDLcWorkOrder
        Me.INDPceFilters.TabIndex = 2
        '
        'INDGcScheduledMaintenance
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcScheduledMaintenance, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcScheduledMaintenance, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcScheduledMaintenance, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcScheduledMaintenance, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcScheduledMaintenance, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcScheduledMaintenance, False)
        Me.INDGcScheduledMaintenance.Location = New System.Drawing.Point(14, 143)
        Me.INDGcScheduledMaintenance.MainView = Me.INDGvScheduledMaintenance
        Me.INDGcScheduledMaintenance.MenuManager = Me.BarManager1
        Me.INDGcScheduledMaintenance.Name = "INDGcScheduledMaintenance"
        Me.INDGcScheduledMaintenance.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvScheduledMaintenance_PceNotification})
        Me.INDGcScheduledMaintenance.Size = New System.Drawing.Size(1220, 405)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcScheduledMaintenance, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcScheduledMaintenance.TabIndex = 1
        Me.INDGcScheduledMaintenance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvScheduledMaintenance})
        '
        'INDGvScheduledMaintenance
        '
        Me.INDGvScheduledMaintenance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvScheduledMaintenance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvScheduledMaintenance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvScheduledMaintenance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvScheduledMaintenance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvScheduledMaintenance.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvScheduledMaintenance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvScheduledMaintenance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvScheduledMaintenance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvScheduledMaintenance.Appearance.Row.Options.UseFont = True
        Me.INDGvScheduledMaintenance.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvScheduledMaintenance.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvScheduledMaintenance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvScheduledMaintenance_Year, Me.INDGvScheduledMaintenance_Month, Me.INDGvScheduledMaintenance_ProgramDate, Me.INDGvScheduledMaintenance_Plate, Me.INDGvScheduledMaintenance_Item, Me.INDGvScheduledMaintenance_Serie, Me.INDGvScheduledMaintenance_Model, Me.INDGvScheduledMaintenance_Trademark, Me.INDGvScheduledMaintenance_Location, Me.INDGvScheduledMaintenance_BranchOffice, Me.INDGvScheduledMaintenance_Protocol, Me.INDGvScheduledMaintenance_Responsible, Me.INDGvScheduledMaintenance_WorkOrderCode, Me.INDGvScheduledMaintenance_Notification})
        Me.INDGvScheduledMaintenance.GridControl = Me.INDGcScheduledMaintenance
        Me.INDGvScheduledMaintenance.GroupCount = 2
        Me.INDGvScheduledMaintenance.Name = "INDGvScheduledMaintenance"
        Me.INDGvScheduledMaintenance.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvScheduledMaintenance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvScheduledMaintenance.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvScheduledMaintenance.OptionsView.ShowAutoFilterRow = True
        Me.INDGvScheduledMaintenance.OptionsView.ShowFooter = True
        Me.INDGvScheduledMaintenance.OptionsView.ShowGroupPanel = False
        Me.INDGvScheduledMaintenance.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvScheduledMaintenance_Year, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvScheduledMaintenance_Month, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvScheduledMaintenance_ProgramDate, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvScheduledMaintenance, False)
        '
        'INDGvScheduledMaintenance_Year
        '
        Me.INDGvScheduledMaintenance_Year.Caption = "Año"
        Me.INDGvScheduledMaintenance_Year.FieldName = "ProgramDateYear"
        Me.INDGvScheduledMaintenance_Year.Name = "INDGvScheduledMaintenance_Year"
        Me.INDGvScheduledMaintenance_Year.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Year.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Year.Visible = True
        Me.INDGvScheduledMaintenance_Year.VisibleIndex = 0
        '
        'INDGvScheduledMaintenance_Month
        '
        Me.INDGvScheduledMaintenance_Month.Caption = "Mes"
        Me.INDGvScheduledMaintenance_Month.FieldName = "ProgramDateMonth"
        Me.INDGvScheduledMaintenance_Month.Name = "INDGvScheduledMaintenance_Month"
        Me.INDGvScheduledMaintenance_Month.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Month.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Month.Visible = True
        Me.INDGvScheduledMaintenance_Month.VisibleIndex = 1
        '
        'INDGvScheduledMaintenance_ProgramDate
        '
        Me.INDGvScheduledMaintenance_ProgramDate.Caption = "Fecha Orden"
        Me.INDGvScheduledMaintenance_ProgramDate.FieldName = "ProgramDate"
        Me.INDGvScheduledMaintenance_ProgramDate.Name = "INDGvScheduledMaintenance_ProgramDate"
        Me.INDGvScheduledMaintenance_ProgramDate.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_ProgramDate.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_ProgramDate.Visible = True
        Me.INDGvScheduledMaintenance_ProgramDate.VisibleIndex = 0
        Me.INDGvScheduledMaintenance_ProgramDate.Width = 127
        '
        'INDGvScheduledMaintenance_Plate
        '
        Me.INDGvScheduledMaintenance_Plate.Caption = "Placa"
        Me.INDGvScheduledMaintenance_Plate.FieldName = "Plate"
        Me.INDGvScheduledMaintenance_Plate.Name = "INDGvScheduledMaintenance_Plate"
        Me.INDGvScheduledMaintenance_Plate.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Plate.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Plate.Visible = True
        Me.INDGvScheduledMaintenance_Plate.VisibleIndex = 1
        Me.INDGvScheduledMaintenance_Plate.Width = 129
        '
        'INDGvScheduledMaintenance_Item
        '
        Me.INDGvScheduledMaintenance_Item.Caption = "Artículo"
        Me.INDGvScheduledMaintenance_Item.FieldName = "ItemCodeName"
        Me.INDGvScheduledMaintenance_Item.Name = "INDGvScheduledMaintenance_Item"
        Me.INDGvScheduledMaintenance_Item.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Item.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Item.Visible = True
        Me.INDGvScheduledMaintenance_Item.VisibleIndex = 2
        Me.INDGvScheduledMaintenance_Item.Width = 278
        '
        'INDGvScheduledMaintenance_Serie
        '
        Me.INDGvScheduledMaintenance_Serie.Caption = "Serie"
        Me.INDGvScheduledMaintenance_Serie.FieldName = "Serie"
        Me.INDGvScheduledMaintenance_Serie.Name = "INDGvScheduledMaintenance_Serie"
        Me.INDGvScheduledMaintenance_Serie.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Serie.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Serie.Width = 124
        '
        'INDGvScheduledMaintenance_Model
        '
        Me.INDGvScheduledMaintenance_Model.Caption = "Modelo"
        Me.INDGvScheduledMaintenance_Model.FieldName = "Model"
        Me.INDGvScheduledMaintenance_Model.Name = "INDGvScheduledMaintenance_Model"
        Me.INDGvScheduledMaintenance_Model.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Model.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Model.Width = 54
        '
        'INDGvScheduledMaintenance_Trademark
        '
        Me.INDGvScheduledMaintenance_Trademark.Caption = "Marca"
        Me.INDGvScheduledMaintenance_Trademark.FieldName = "TrademarkCodeName"
        Me.INDGvScheduledMaintenance_Trademark.Name = "INDGvScheduledMaintenance_Trademark"
        Me.INDGvScheduledMaintenance_Trademark.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Trademark.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Trademark.Visible = True
        Me.INDGvScheduledMaintenance_Trademark.VisibleIndex = 3
        Me.INDGvScheduledMaintenance_Trademark.Width = 161
        '
        'INDGvScheduledMaintenance_Location
        '
        Me.INDGvScheduledMaintenance_Location.Caption = "Ubicación"
        Me.INDGvScheduledMaintenance_Location.FieldName = "LocationCodeName"
        Me.INDGvScheduledMaintenance_Location.Name = "INDGvScheduledMaintenance_Location"
        Me.INDGvScheduledMaintenance_Location.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Location.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Location.Visible = True
        Me.INDGvScheduledMaintenance_Location.VisibleIndex = 4
        Me.INDGvScheduledMaintenance_Location.Width = 243
        '
        'INDGvScheduledMaintenance_BranchOffice
        '
        Me.INDGvScheduledMaintenance_BranchOffice.Caption = "Sucursal"
        Me.INDGvScheduledMaintenance_BranchOffice.FieldName = "BranchOfficeCodeName"
        Me.INDGvScheduledMaintenance_BranchOffice.Name = "INDGvScheduledMaintenance_BranchOffice"
        Me.INDGvScheduledMaintenance_BranchOffice.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_BranchOffice.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_BranchOffice.Visible = True
        Me.INDGvScheduledMaintenance_BranchOffice.VisibleIndex = 5
        Me.INDGvScheduledMaintenance_BranchOffice.Width = 127
        '
        'INDGvScheduledMaintenance_Protocol
        '
        Me.INDGvScheduledMaintenance_Protocol.Caption = "Protocolo"
        Me.INDGvScheduledMaintenance_Protocol.FieldName = "ProtocolCodeName"
        Me.INDGvScheduledMaintenance_Protocol.Name = "INDGvScheduledMaintenance_Protocol"
        Me.INDGvScheduledMaintenance_Protocol.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Protocol.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Protocol.Width = 118
        '
        'INDGvScheduledMaintenance_Responsible
        '
        Me.INDGvScheduledMaintenance_Responsible.Caption = "Responsable"
        Me.INDGvScheduledMaintenance_Responsible.FieldName = "ResponsibleCodeName"
        Me.INDGvScheduledMaintenance_Responsible.Name = "INDGvScheduledMaintenance_Responsible"
        Me.INDGvScheduledMaintenance_Responsible.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_Responsible.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_Responsible.Width = 60
        '
        'INDGvScheduledMaintenance_WorkOrderCode
        '
        Me.INDGvScheduledMaintenance_WorkOrderCode.Caption = "Orden de Trabajo"
        Me.INDGvScheduledMaintenance_WorkOrderCode.FieldName = "WorkOrderCode"
        Me.INDGvScheduledMaintenance_WorkOrderCode.Name = "INDGvScheduledMaintenance_WorkOrderCode"
        Me.INDGvScheduledMaintenance_WorkOrderCode.OptionsColumn.AllowEdit = False
        Me.INDGvScheduledMaintenance_WorkOrderCode.OptionsColumn.AllowFocus = False
        Me.INDGvScheduledMaintenance_WorkOrderCode.Width = 143
        '
        'INDGvScheduledMaintenance_Notification
        '
        Me.INDGvScheduledMaintenance_Notification.Caption = "Notificación"
        Me.INDGvScheduledMaintenance_Notification.ColumnEdit = Me.INDGvScheduledMaintenance_PceNotification
        Me.INDGvScheduledMaintenance_Notification.Name = "INDGvScheduledMaintenance_Notification"
        Me.INDGvScheduledMaintenance_Notification.Visible = True
        Me.INDGvScheduledMaintenance_Notification.VisibleIndex = 6
        Me.INDGvScheduledMaintenance_Notification.Width = 80
        '
        'INDGvScheduledMaintenance_PceNotification
        '
        Me.INDGvScheduledMaintenance_PceNotification.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvScheduledMaintenance_PceNotification.AutoHeight = False
        Me.INDGvScheduledMaintenance_PceNotification.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvScheduledMaintenance_PceNotification.Name = "INDGvScheduledMaintenance_PceNotification"
        Me.INDGvScheduledMaintenance_PceNotification.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDLcgWorkOrder
        '
        Me.INDLcgWorkOrder.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWorkOrder.AppearanceGroup.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWorkOrder.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkOrder.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkOrder.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgWorkOrder.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkOrder.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgWorkOrder.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgWorkOrder.GroupBordersVisible = False
        Me.INDLcgWorkOrder.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilter, Me.INDTcgWorkOrder})
        Me.INDLcgWorkOrder.Name = "INDLcgWorkOrder"
        Me.INDLcgWorkOrder.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLcgWorkOrder.Size = New System.Drawing.Size(1248, 562)
        Me.INDLcgWorkOrder.TextVisible = False
        '
        'INDLcgFilter
        '
        Me.INDLcgFilter.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilter.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgFilter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem19})
        Me.INDLcgFilter.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilter.Name = "INDLcgFilter"
        Me.INDLcgFilter.Size = New System.Drawing.Size(1248, 98)
        Me.INDLcgFilter.Text = "Filtros"
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.INDPceFilters
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 45)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(1, 45)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(1224, 45)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem19.TextVisible = False
        '
        'INDTcgWorkOrder
        '
        Me.INDTcgWorkOrder.Location = New System.Drawing.Point(0, 98)
        Me.INDTcgWorkOrder.Name = "INDTcgWorkOrder"
        Me.INDTcgWorkOrder.SelectedTabPage = Me.INDLcgScheduledMaintenance
        Me.INDTcgWorkOrder.Size = New System.Drawing.Size(1248, 464)
        Me.INDTcgWorkOrder.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgScheduledMaintenance, Me.INDLcgUnscheduledMaintenance, Me.INDLcgMaintenanceToBeEvaluated})
        '
        'INDLcgMaintenanceToBeEvaluated
        '
        Me.INDLcgMaintenanceToBeEvaluated.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciMaintenanceToBeEvaluated})
        Me.INDLcgMaintenanceToBeEvaluated.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMaintenanceToBeEvaluated.Name = "INDLcgMaintenanceToBeEvaluated"
        Me.INDLcgMaintenanceToBeEvaluated.Size = New System.Drawing.Size(1224, 409)
        Me.INDLcgMaintenanceToBeEvaluated.Text = "Mantenimientos por Evaluar"
        '
        'INDLciMaintenanceToBeEvaluated
        '
        Me.INDLciMaintenanceToBeEvaluated.Control = Me.INDGcMaintenanceToBeEvaluated
        Me.INDLciMaintenanceToBeEvaluated.Location = New System.Drawing.Point(0, 0)
        Me.INDLciMaintenanceToBeEvaluated.Name = "INDLciMaintenanceToBeEvaluated"
        Me.INDLciMaintenanceToBeEvaluated.Size = New System.Drawing.Size(1224, 409)
        Me.INDLciMaintenanceToBeEvaluated.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciMaintenanceToBeEvaluated.TextVisible = False
        '
        'INDLcgScheduledMaintenance
        '
        Me.INDLcgScheduledMaintenance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciScheduledMaintenance})
        Me.INDLcgScheduledMaintenance.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgScheduledMaintenance.Name = "INDLcgScheduledMaintenance"
        Me.INDLcgScheduledMaintenance.Size = New System.Drawing.Size(1224, 409)
        Me.INDLcgScheduledMaintenance.Text = "Mantenimientos Programados"
        '
        'INDLciScheduledMaintenance
        '
        Me.INDLciScheduledMaintenance.Control = Me.INDGcScheduledMaintenance
        Me.INDLciScheduledMaintenance.Location = New System.Drawing.Point(0, 0)
        Me.INDLciScheduledMaintenance.Name = "INDLciScheduledMaintenance"
        Me.INDLciScheduledMaintenance.Size = New System.Drawing.Size(1224, 409)
        Me.INDLciScheduledMaintenance.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciScheduledMaintenance.TextVisible = False
        '
        'INDLcgUnscheduledMaintenance
        '
        Me.INDLcgUnscheduledMaintenance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciUnscheduledMaintenance})
        Me.INDLcgUnscheduledMaintenance.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgUnscheduledMaintenance.Name = "INDLcgUnscheduledMaintenance"
        Me.INDLcgUnscheduledMaintenance.Size = New System.Drawing.Size(1224, 409)
        Me.INDLcgUnscheduledMaintenance.Text = "Mantenimientos no programados"
        '
        'INDLciUnscheduledMaintenance
        '
        Me.INDLciUnscheduledMaintenance.Control = Me.INDGcUnscheduledMaintenance
        Me.INDLciUnscheduledMaintenance.Location = New System.Drawing.Point(0, 0)
        Me.INDLciUnscheduledMaintenance.Name = "INDLciUnscheduledMaintenance"
        Me.INDLciUnscheduledMaintenance.Size = New System.Drawing.Size(1224, 409)
        Me.INDLciUnscheduledMaintenance.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciUnscheduledMaintenance.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDPopMenuActions1
        '
        Me.INDPopMenuActions1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiWorkOrder), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiApprove), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiReject)})
        Me.INDPopMenuActions1.Manager = Me.BarManager1
        Me.INDPopMenuActions1.Name = "INDPopMenuActions1"
        '
        'FrmWorkOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1252, 693)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmWorkOrder.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmWorkOrder"
        Me.Opacity = 1.0R
        Me.Tag = "2134"
        Me.Text = "Orden de Trabajo"
        Me.Controls.SetChildIndex(Me.BarDockControl5, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl6, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl8, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl7, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcWorkOrder.ResumeLayout(False)
        CType(Me.INDPccWorkOrderNotification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccWorkOrderNotification.ResumeLayout(False)
        CType(Me.INDLcElectronicDocumentNotification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcElectronicDocumentNotification.ResumeLayout(False)
        CType(Me.INDGcWorkOrderNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWorkOrderNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWorkOrderNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvMaintenanceToBeEvaluated_PceNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccFilter.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkTipoEquipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkTipoInventario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkTipoResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkArticulo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleArticulo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTipoResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTipoInventario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTipoEquipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleSoloProgramado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvUnscheduledMaintenance_PceNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcScheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvScheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvScheduledMaintenance_PceNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMaintenanceToBeEvaluated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgScheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciScheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUnscheduledMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcWorkOrder As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPccFilter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtreeLocation As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDChkResponsable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoEquipo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoInventario As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoResponsable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkArticulo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDBtnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSleActivo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleArticulo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleTipoResponsable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleTipoInventario As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleTipoEquipo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleResponsable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDPceFilters As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDGcScheduledMaintenance As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvScheduledMaintenance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvScheduledMaintenance_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Trademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Model As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Serie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Location As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Protocol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Responsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_WorkOrderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgWorkOrder As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgFilter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgleSoloProgramado As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTcgWorkOrder As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgUnscheduledMaintenance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgScheduledMaintenance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciScheduledMaintenance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcUnscheduledMaintenance As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvUnscheduledMaintenance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvUnscheduledMaintenance_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Trademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Model As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Serie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Location As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciUnscheduledMaintenance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvScheduledMaintenance_Year As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_Month As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_ProgramDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_BranchOffice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Year As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Month As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_ProgramDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Part As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_BranchOffice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Protocol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Responsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_WorkOrderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiWorkOrder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDLcgMaintenanceToBeEvaluated As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcMaintenanceToBeEvaluated As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvMaintenanceToBeEvaluated As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Year As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Month As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_ProgramDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_WorkOrderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Protocol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_MaintenanceResponsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Serie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Model As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Part As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Trademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_FixedAssetResponsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Location As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_BranchOffice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciMaintenanceToBeEvaluated As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBbiApprove As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiReject As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDGvMaintenanceToBeEvaluated_SelectOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccWorkOrderNotification As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcElectronicDocumentNotification As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcWorkOrderNotification As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvWorkOrderNotification As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvWorkOrderNotification_Email As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvWorkOrderNotification_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvWorkOrderNotification_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciWorkOrderNotification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvScheduledMaintenance_Notification As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvScheduledMaintenance_PceNotification As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvWorkOrderNotification_Type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_Notification As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvUnscheduledMaintenance_PceNotification As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvMaintenanceToBeEvaluated_Notification As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceToBeEvaluated_PceNotification As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvWorkOrderNotification_ThirdParty As DevExpress.XtraGrid.Columns.GridColumn
End Class
