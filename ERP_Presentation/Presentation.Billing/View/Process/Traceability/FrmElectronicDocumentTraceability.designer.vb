Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmElectronicDocumentTraceability
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmElectronicDocumentTraceability))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccElectronicDocumentNotifications = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcElectronicDocumentNotifications = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcElectronicDocumentNotifications = New DevExpress.XtraGrid.GridControl()
        Me.INDgvElectronicDocumentNotifications = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolElectronicDocumentNotifications_Email = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicDocumentNotifications_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicDocumentNotifications_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicDocumentNotifications_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.SkinBarSubItem1 = New DevExpress.XtraBars.SkinBarSubItem()
        Me.INDBbiRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciElectronicDocumentNotifications = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPccElectronicDocumentDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcElectronicDocumentDetails = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcElectronicDocumentDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDGvElectronicDocumentDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvElectronicDocumentDetails_Destination = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicDocumentDetails_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicDocumentDetails_Response = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicDocumentDetails_Comments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicDocumentDetails_ResponseData = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicDocumentDetails_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgElectronicDocumentDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciElectronicDocumentDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcInvoice = New DevExpress.XtraGrid.GridControl()
        Me.INDGvInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvInvoice_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_DocumentTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_InvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_CUFE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_CustomerNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_StatusName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvInvoice_Notifications = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvInvoice_PceNotifications = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvInvoice_CenterAttention = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCurrency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCreditNote = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCreditNote = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCreditNote_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_InvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_CUFE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_CustomerNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_StatusName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvCreditNote_Notifications = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCreditNote_PceNotifications = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGcDebitNote = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDebitNote = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvDebitNote_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_InvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_CUFE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_CustomerNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvDebitNote_Notifications = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDebitNote_PceNotifications = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDSleOperatingUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvOperatingUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvOperatingUnit_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvOperatingUnit_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPceDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDDdbMenuActions = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPmActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDCcbeStatus = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.INDGcDocumentSupport = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDocumentSupport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvDocumentSupport_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_SourceDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_SupplierThirdPartyId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_StatusName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvDocumentSupport_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvSupportDocument_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvSupportDocument_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcgDocumentSupport = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgElectronicDocument = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgCreditNote = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgInvoice = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgDebitNote = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiProcess = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiSendNotification = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiReSend = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopMenuActions2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDPccElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccElectronicDocumentNotifications.SuspendLayout()
        CType(Me.INDLcElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcElectronicDocumentNotifications.SuspendLayout()
        CType(Me.INDGcElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccElectronicDocumentDetails.SuspendLayout()
        CType(Me.INDLcElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcElectronicDocumentDetails.SuspendLayout()
        CType(Me.INDGcElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvInvoice_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvInvoice_PceNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCreditNote_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCreditNote_PceNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDebitNote_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDebitNote_PceNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDSleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvOperatingUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCcbeStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDocumentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDocumentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSupportDocument_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDocumentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgElectronicDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcMainData)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit3.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        resources.ApplyResources(Me.RepositoryItemPictureEdit1, "RepositoryItemPictureEdit1")
        '
        'INDLcMainData
        '
        Me.INDLcMainData.AllowCustomization = False
        Me.INDLcMainData.Controls.Add(Me.INDPccElectronicDocumentNotifications)
        Me.INDLcMainData.Controls.Add(Me.INDPccElectronicDocumentDetails)
        Me.INDLcMainData.Controls.Add(Me.INDGcInvoice)
        Me.INDLcMainData.Controls.Add(Me.INDGcCreditNote)
        Me.INDLcMainData.Controls.Add(Me.INDGcDebitNote)
        Me.INDLcMainData.Controls.Add(Me.PanelControl1)
        Me.INDLcMainData.Controls.Add(Me.INDDdbMenuActions)
        Me.INDLcMainData.Controls.Add(Me.INDCcbeStatus)
        Me.INDLcMainData.Controls.Add(Me.INDGcDocumentSupport)
        resources.ApplyResources(Me.INDLcMainData, "INDLcMainData")
        Me.INDLcMainData.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgDocumentSupport})
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData, False)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(572, 444, 1245, 542)
        Me.INDLcMainData.Root = Me.LayoutControlGroup2
        '
        'INDPccElectronicDocumentNotifications
        '
        Me.INDPccElectronicDocumentNotifications.Controls.Add(Me.INDLcElectronicDocumentNotifications)
        resources.ApplyResources(Me.INDPccElectronicDocumentNotifications, "INDPccElectronicDocumentNotifications")
        Me.INDPccElectronicDocumentNotifications.Name = "INDPccElectronicDocumentNotifications"
        '
        'INDLcElectronicDocumentNotifications
        '
        Me.INDLcElectronicDocumentNotifications.Controls.Add(Me.INDGcElectronicDocumentNotifications)
        resources.ApplyResources(Me.INDLcElectronicDocumentNotifications, "INDLcElectronicDocumentNotifications")
        Me.INDLcElectronicDocumentNotifications.Name = "INDLcElectronicDocumentNotifications"
        Me.INDLcElectronicDocumentNotifications.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(965, 99, 574, 569)
        Me.INDLcElectronicDocumentNotifications.Root = Me.Root
        '
        'INDGcElectronicDocumentNotifications
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcElectronicDocumentNotifications, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcElectronicDocumentNotifications, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcElectronicDocumentNotifications, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcElectronicDocumentNotifications, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcElectronicDocumentNotifications, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcElectronicDocumentNotifications, False)
        resources.ApplyResources(Me.INDGcElectronicDocumentNotifications, "INDGcElectronicDocumentNotifications")
        Me.INDGcElectronicDocumentNotifications.MainView = Me.INDgvElectronicDocumentNotifications
        Me.INDGcElectronicDocumentNotifications.MenuManager = Me.BarManager1
        Me.INDGcElectronicDocumentNotifications.Name = "INDGcElectronicDocumentNotifications"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcElectronicDocumentNotifications, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcElectronicDocumentNotifications.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvElectronicDocumentNotifications})
        '
        'INDgvElectronicDocumentNotifications
        '
        Me.INDgvElectronicDocumentNotifications.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvElectronicDocumentNotifications.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgvElectronicDocumentNotifications.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgvElectronicDocumentNotifications.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvElectronicDocumentNotifications.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvElectronicDocumentNotifications.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvElectronicDocumentNotifications.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgvElectronicDocumentNotifications.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvElectronicDocumentNotifications.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvElectronicDocumentNotifications.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgvElectronicDocumentNotifications.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvElectronicDocumentNotifications.Appearance.Row.Font = CType(resources.GetObject("INDgvElectronicDocumentNotifications.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvElectronicDocumentNotifications.Appearance.Row.Options.UseFont = True
        Me.INDgvElectronicDocumentNotifications.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgvElectronicDocumentNotifications.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgvElectronicDocumentNotifications.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvElectronicDocumentNotifications.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolElectronicDocumentNotifications_Email, Me.INDcolElectronicDocumentNotifications_Status, Me.INDcolElectronicDocumentNotifications_ShippingDate, Me.INDcolElectronicDocumentNotifications_CreationDate})
        Me.INDgvElectronicDocumentNotifications.GridControl = Me.INDGcElectronicDocumentNotifications
        Me.INDgvElectronicDocumentNotifications.Name = "INDgvElectronicDocumentNotifications"
        Me.INDgvElectronicDocumentNotifications.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvElectronicDocumentNotifications.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvElectronicDocumentNotifications.OptionsView.ShowAutoFilterRow = True
        Me.INDgvElectronicDocumentNotifications.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvElectronicDocumentNotifications, False)
        '
        'INDcolElectronicDocumentNotifications_Email
        '
        resources.ApplyResources(Me.INDcolElectronicDocumentNotifications_Email, "INDcolElectronicDocumentNotifications_Email")
        Me.INDcolElectronicDocumentNotifications_Email.FieldName = "Email"
        Me.INDcolElectronicDocumentNotifications_Email.Name = "INDcolElectronicDocumentNotifications_Email"
        Me.INDcolElectronicDocumentNotifications_Email.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicDocumentNotifications_Email.OptionsColumn.AllowFocus = False
        '
        'INDcolElectronicDocumentNotifications_Status
        '
        resources.ApplyResources(Me.INDcolElectronicDocumentNotifications_Status, "INDcolElectronicDocumentNotifications_Status")
        Me.INDcolElectronicDocumentNotifications_Status.FieldName = "StatusName"
        Me.INDcolElectronicDocumentNotifications_Status.Name = "INDcolElectronicDocumentNotifications_Status"
        Me.INDcolElectronicDocumentNotifications_Status.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicDocumentNotifications_Status.OptionsColumn.AllowFocus = False
        '
        'INDcolElectronicDocumentNotifications_ShippingDate
        '
        resources.ApplyResources(Me.INDcolElectronicDocumentNotifications_ShippingDate, "INDcolElectronicDocumentNotifications_ShippingDate")
        Me.INDcolElectronicDocumentNotifications_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDcolElectronicDocumentNotifications_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolElectronicDocumentNotifications_ShippingDate.FieldName = "ShippingDate"
        Me.INDcolElectronicDocumentNotifications_ShippingDate.Name = "INDcolElectronicDocumentNotifications_ShippingDate"
        Me.INDcolElectronicDocumentNotifications_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicDocumentNotifications_ShippingDate.OptionsColumn.AllowFocus = False
        '
        'INDcolElectronicDocumentNotifications_CreationDate
        '
        resources.ApplyResources(Me.INDcolElectronicDocumentNotifications_CreationDate, "INDcolElectronicDocumentNotifications_CreationDate")
        Me.INDcolElectronicDocumentNotifications_CreationDate.DisplayFormat.FormatString = "G"
        Me.INDcolElectronicDocumentNotifications_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolElectronicDocumentNotifications_CreationDate.FieldName = "CreationDate"
        Me.INDcolElectronicDocumentNotifications_CreationDate.Name = "INDcolElectronicDocumentNotifications_CreationDate"
        Me.INDcolElectronicDocumentNotifications_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicDocumentNotifications_CreationDate.OptionsColumn.AllowFocus = False
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.SkinBarSubItem1, Me.INDBbiRefresh})
        Me.BarManager1.MaxItemId = 13
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager1
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager1
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager1
        '
        'SkinBarSubItem1
        '
        resources.ApplyResources(Me.SkinBarSubItem1, "SkinBarSubItem1")
        Me.SkinBarSubItem1.Id = 0
        Me.SkinBarSubItem1.Name = "SkinBarSubItem1"
        '
        'INDBbiRefresh
        '
        resources.ApplyResources(Me.INDBbiRefresh, "INDBbiRefresh")
        Me.INDBbiRefresh.Id = 2
        Me.INDBbiRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.INDBbiRefresh.Name = "INDBbiRefresh"
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = CType(resources.GetObject("Root.AppearanceGroup.Font"), System.Drawing.Font)
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = CType(resources.GetObject("Root.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = CType(resources.GetObject("Root.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("Root.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("Root.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("Root.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("Root.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciElectronicDocumentNotifications})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(858, 128)
        Me.Root.TextVisible = False
        '
        'INDLciElectronicDocumentNotifications
        '
        Me.INDLciElectronicDocumentNotifications.Control = Me.INDGcElectronicDocumentNotifications
        Me.INDLciElectronicDocumentNotifications.Location = New System.Drawing.Point(0, 0)
        Me.INDLciElectronicDocumentNotifications.Name = "INDLciElectronicDocumentNotifications"
        Me.INDLciElectronicDocumentNotifications.Size = New System.Drawing.Size(838, 108)
        Me.INDLciElectronicDocumentNotifications.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciElectronicDocumentNotifications.TextVisible = False
        '
        'INDPccElectronicDocumentDetails
        '
        Me.INDPccElectronicDocumentDetails.Controls.Add(Me.INDLcElectronicDocumentDetails)
        resources.ApplyResources(Me.INDPccElectronicDocumentDetails, "INDPccElectronicDocumentDetails")
        Me.INDPccElectronicDocumentDetails.Name = "INDPccElectronicDocumentDetails"
        '
        'INDLcElectronicDocumentDetails
        '
        Me.INDLcElectronicDocumentDetails.Controls.Add(Me.INDGcElectronicDocumentDetails)
        resources.ApplyResources(Me.INDLcElectronicDocumentDetails, "INDLcElectronicDocumentDetails")
        Me.INDLcElectronicDocumentDetails.Name = "INDLcElectronicDocumentDetails"
        Me.INDLcElectronicDocumentDetails.Root = Me.INDLcgElectronicDocumentDetails
        '
        'INDGcElectronicDocumentDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcElectronicDocumentDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcElectronicDocumentDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcElectronicDocumentDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcElectronicDocumentDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcElectronicDocumentDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcElectronicDocumentDetails, False)
        resources.ApplyResources(Me.INDGcElectronicDocumentDetails, "INDGcElectronicDocumentDetails")
        Me.INDGcElectronicDocumentDetails.MainView = Me.INDGvElectronicDocumentDetails
        Me.INDGcElectronicDocumentDetails.MenuManager = Me.BarManager1
        Me.INDGcElectronicDocumentDetails.Name = "INDGcElectronicDocumentDetails"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcElectronicDocumentDetails, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcElectronicDocumentDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvElectronicDocumentDetails})
        '
        'INDGvElectronicDocumentDetails
        '
        Me.INDGvElectronicDocumentDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvElectronicDocumentDetails.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvElectronicDocumentDetails.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvElectronicDocumentDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvElectronicDocumentDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvElectronicDocumentDetails.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvElectronicDocumentDetails.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvElectronicDocumentDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvElectronicDocumentDetails.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvElectronicDocumentDetails.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvElectronicDocumentDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvElectronicDocumentDetails.Appearance.Row.Font = CType(resources.GetObject("INDGvElectronicDocumentDetails.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvElectronicDocumentDetails.Appearance.Row.Options.UseFont = True
        Me.INDGvElectronicDocumentDetails.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvElectronicDocumentDetails.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvElectronicDocumentDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvElectronicDocumentDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvElectronicDocumentDetails_Destination, Me.INDGvElectronicDocumentDetails_Status, Me.INDGvElectronicDocumentDetails_Response, Me.INDGvElectronicDocumentDetails_Comments, Me.INDGvElectronicDocumentDetails_ResponseData, Me.INDGvElectronicDocumentDetails_CreationDate})
        Me.INDGvElectronicDocumentDetails.GridControl = Me.INDGcElectronicDocumentDetails
        Me.INDGvElectronicDocumentDetails.Name = "INDGvElectronicDocumentDetails"
        Me.INDGvElectronicDocumentDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvElectronicDocumentDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvElectronicDocumentDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDGvElectronicDocumentDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvElectronicDocumentDetails, False)
        '
        'INDGvElectronicDocumentDetails_Destination
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_Destination, "INDGvElectronicDocumentDetails_Destination")
        Me.INDGvElectronicDocumentDetails_Destination.FieldName = "DestinationName"
        Me.INDGvElectronicDocumentDetails_Destination.Name = "INDGvElectronicDocumentDetails_Destination"
        Me.INDGvElectronicDocumentDetails_Destination.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicDocumentDetails_Destination.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicDocumentDetails_Destination.OptionsColumn.FixedWidth = True
        '
        'INDGvElectronicDocumentDetails_Status
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_Status, "INDGvElectronicDocumentDetails_Status")
        Me.INDGvElectronicDocumentDetails_Status.FieldName = "StatusName"
        Me.INDGvElectronicDocumentDetails_Status.Name = "INDGvElectronicDocumentDetails_Status"
        Me.INDGvElectronicDocumentDetails_Status.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicDocumentDetails_Status.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicDocumentDetails_Status.OptionsColumn.FixedWidth = True
        '
        'INDGvElectronicDocumentDetails_Response
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_Response, "INDGvElectronicDocumentDetails_Response")
        Me.INDGvElectronicDocumentDetails_Response.FieldName = "Response"
        Me.INDGvElectronicDocumentDetails_Response.Name = "INDGvElectronicDocumentDetails_Response"
        Me.INDGvElectronicDocumentDetails_Response.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicDocumentDetails_Response.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicDocumentDetails_Response.OptionsColumn.FixedWidth = True
        '
        'INDGvElectronicDocumentDetails_Comments
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_Comments, "INDGvElectronicDocumentDetails_Comments")
        Me.INDGvElectronicDocumentDetails_Comments.FieldName = "Comments"
        Me.INDGvElectronicDocumentDetails_Comments.Name = "INDGvElectronicDocumentDetails_Comments"
        Me.INDGvElectronicDocumentDetails_Comments.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicDocumentDetails_Comments.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicDocumentDetails_Comments.OptionsColumn.FixedWidth = True
        '
        'INDGvElectronicDocumentDetails_ResponseData
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_ResponseData, "INDGvElectronicDocumentDetails_ResponseData")
        Me.INDGvElectronicDocumentDetails_ResponseData.FieldName = "ResponseData"
        Me.INDGvElectronicDocumentDetails_ResponseData.Name = "INDGvElectronicDocumentDetails_ResponseData"
        Me.INDGvElectronicDocumentDetails_ResponseData.OptionsColumn.FixedWidth = True
        '
        'INDGvElectronicDocumentDetails_CreationDate
        '
        resources.ApplyResources(Me.INDGvElectronicDocumentDetails_CreationDate, "INDGvElectronicDocumentDetails_CreationDate")
        Me.INDGvElectronicDocumentDetails_CreationDate.DisplayFormat.FormatString = """dd/MM/yyyy HH:mm:ss"""
        Me.INDGvElectronicDocumentDetails_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvElectronicDocumentDetails_CreationDate.FieldName = "CreationDate"
        Me.INDGvElectronicDocumentDetails_CreationDate.Name = "INDGvElectronicDocumentDetails_CreationDate"
        Me.INDGvElectronicDocumentDetails_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicDocumentDetails_CreationDate.OptionsColumn.AllowFocus = False
        '
        'INDLcgElectronicDocumentDetails
        '
        Me.INDLcgElectronicDocumentDetails.AppearanceGroup.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceGroup.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgElectronicDocumentDetails.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgElectronicDocumentDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgElectronicDocumentDetails, False)
        Me.INDLcgElectronicDocumentDetails.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgElectronicDocumentDetails.GroupBordersVisible = False
        Me.INDLcgElectronicDocumentDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciElectronicDocumentDetails})
        Me.INDLcgElectronicDocumentDetails.Name = "Root"
        Me.INDLcgElectronicDocumentDetails.Size = New System.Drawing.Size(861, 149)
        Me.INDLcgElectronicDocumentDetails.TextVisible = False
        '
        'INDLciElectronicDocumentDetails
        '
        Me.INDLciElectronicDocumentDetails.Control = Me.INDGcElectronicDocumentDetails
        Me.INDLciElectronicDocumentDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLciElectronicDocumentDetails.Name = "INDLciElectronicDocumentDetails"
        Me.INDLciElectronicDocumentDetails.Size = New System.Drawing.Size(841, 129)
        Me.INDLciElectronicDocumentDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciElectronicDocumentDetails.TextVisible = False
        '
        'INDGcInvoice
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcInvoice, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcInvoice, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcInvoice, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcInvoice, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcInvoice, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcInvoice, False)
        resources.ApplyResources(Me.INDGcInvoice, "INDGcInvoice")
        Me.INDGcInvoice.MainView = Me.INDGvInvoice
        Me.INDGcInvoice.Name = "INDGcInvoice"
        Me.INDGcInvoice.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvInvoice_PceDetails, Me.INDGvInvoice_PceNotifications})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcInvoice, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcInvoice.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvInvoice})
        '
        'INDGvInvoice
        '
        Me.INDGvInvoice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvInvoice.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvInvoice.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvInvoice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvInvoice.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvInvoice.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvInvoice.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvInvoice.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvInvoice.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvInvoice.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvInvoice.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvInvoice.Appearance.Row.Font = CType(resources.GetObject("INDGvInvoice.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvInvoice.Appearance.Row.Options.UseFont = True
        Me.INDGvInvoice.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvInvoice.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvInvoice.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvInvoice_UnboundSelection, Me.INDGvInvoice_DocumentTypeName, Me.INDGvInvoice_InvoiceNumber, Me.INDGvInvoice_DocumentNumber, Me.INDGvInvoice_CUFE, Me.INDGvInvoice_CustomerNitName, Me.INDGvInvoice_StatusName, Me.INDGvInvoice_DocumentDate, Me.INDGvInvoice_ShippingDate, Me.INDGvInvoice_Details, Me.INDGvInvoice_Notifications, Me.INDGvInvoice_CenterAttention, Me.INDColCurrency})
        Me.INDGvInvoice.GridControl = Me.INDGcInvoice
        Me.INDGvInvoice.Name = "INDGvInvoice"
        Me.INDGvInvoice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvInvoice.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvInvoice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvInvoice.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvInvoice.OptionsView.ShowAutoFilterRow = True
        Me.INDGvInvoice.OptionsView.ShowFooter = True
        Me.INDGvInvoice.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvInvoice, False)
        '
        'INDGvInvoice_UnboundSelection
        '
        resources.ApplyResources(Me.INDGvInvoice_UnboundSelection, "INDGvInvoice_UnboundSelection")
        Me.INDGvInvoice_UnboundSelection.FieldName = "INDGvInvoice_UnboundSelection"
        Me.INDGvInvoice_UnboundSelection.Name = "INDGvInvoice_UnboundSelection"
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvInvoice_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvInvoice_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'INDGvInvoice_DocumentTypeName
        '
        resources.ApplyResources(Me.INDGvInvoice_DocumentTypeName, "INDGvInvoice_DocumentTypeName")
        Me.INDGvInvoice_DocumentTypeName.FieldName = "DocumentTypeName"
        Me.INDGvInvoice_DocumentTypeName.Name = "INDGvInvoice_DocumentTypeName"
        Me.INDGvInvoice_DocumentTypeName.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_DocumentTypeName.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_InvoiceNumber
        '
        resources.ApplyResources(Me.INDGvInvoice_InvoiceNumber, "INDGvInvoice_InvoiceNumber")
        Me.INDGvInvoice_InvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDGvInvoice_InvoiceNumber.Name = "INDGvInvoice_InvoiceNumber"
        '
        'INDGvInvoice_DocumentNumber
        '
        resources.ApplyResources(Me.INDGvInvoice_DocumentNumber, "INDGvInvoice_DocumentNumber")
        Me.INDGvInvoice_DocumentNumber.FieldName = "DocumentNumberWithPrefix"
        Me.INDGvInvoice_DocumentNumber.Name = "INDGvInvoice_DocumentNumber"
        Me.INDGvInvoice_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_DocumentNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_CUFE
        '
        resources.ApplyResources(Me.INDGvInvoice_CUFE, "INDGvInvoice_CUFE")
        Me.INDGvInvoice_CUFE.FieldName = "CUFE"
        Me.INDGvInvoice_CUFE.Name = "INDGvInvoice_CUFE"
        Me.INDGvInvoice_CUFE.OptionsColumn.ReadOnly = True
        '
        'INDGvInvoice_CustomerNitName
        '
        resources.ApplyResources(Me.INDGvInvoice_CustomerNitName, "INDGvInvoice_CustomerNitName")
        Me.INDGvInvoice_CustomerNitName.FieldName = "CustomerPartyId.NitName"
        Me.INDGvInvoice_CustomerNitName.Name = "INDGvInvoice_CustomerNitName"
        Me.INDGvInvoice_CustomerNitName.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_CustomerNitName.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_StatusName
        '
        resources.ApplyResources(Me.INDGvInvoice_StatusName, "INDGvInvoice_StatusName")
        Me.INDGvInvoice_StatusName.FieldName = "StatusName"
        Me.INDGvInvoice_StatusName.Name = "INDGvInvoice_StatusName"
        Me.INDGvInvoice_StatusName.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_StatusName.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_DocumentDate
        '
        resources.ApplyResources(Me.INDGvInvoice_DocumentDate, "INDGvInvoice_DocumentDate")
        Me.INDGvInvoice_DocumentDate.DisplayFormat.FormatString = "MMM/d/yyyy hh:mm tt"
        Me.INDGvInvoice_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvInvoice_DocumentDate.FieldName = "DocumentDate"
        Me.INDGvInvoice_DocumentDate.Name = "INDGvInvoice_DocumentDate"
        Me.INDGvInvoice_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_DocumentDate.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_ShippingDate
        '
        resources.ApplyResources(Me.INDGvInvoice_ShippingDate, "INDGvInvoice_ShippingDate")
        Me.INDGvInvoice_ShippingDate.DisplayFormat.FormatString = "MMM/d/yyyy hh:mm tt"
        Me.INDGvInvoice_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvInvoice_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvInvoice_ShippingDate.Name = "INDGvInvoice_ShippingDate"
        Me.INDGvInvoice_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_ShippingDate.OptionsColumn.AllowFocus = False
        '
        'INDGvInvoice_Details
        '
        resources.ApplyResources(Me.INDGvInvoice_Details, "INDGvInvoice_Details")
        Me.INDGvInvoice_Details.ColumnEdit = Me.INDGvInvoice_PceDetails
        Me.INDGvInvoice_Details.FieldName = "INDGvInvoice_Details"
        Me.INDGvInvoice_Details.Name = "INDGvInvoice_Details"
        Me.INDGvInvoice_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvInvoice_PceDetails
        '
        Me.INDGvInvoice_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvInvoice_PceDetails, "INDGvInvoice_PceDetails")
        Me.INDGvInvoice_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvInvoice_PceDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvInvoice_PceDetails.Name = "INDGvInvoice_PceDetails"
        Me.INDGvInvoice_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvInvoice_Notifications
        '
        resources.ApplyResources(Me.INDGvInvoice_Notifications, "INDGvInvoice_Notifications")
        Me.INDGvInvoice_Notifications.ColumnEdit = Me.INDGvInvoice_PceNotifications
        Me.INDGvInvoice_Notifications.FieldName = "INDGvInvoice_Notifications"
        Me.INDGvInvoice_Notifications.Name = "INDGvInvoice_Notifications"
        Me.INDGvInvoice_Notifications.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvInvoice_PceNotifications
        '
        Me.INDGvInvoice_PceNotifications.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvInvoice_PceNotifications, "INDGvInvoice_PceNotifications")
        Me.INDGvInvoice_PceNotifications.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvInvoice_PceNotifications.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvInvoice_PceNotifications.Name = "INDGvInvoice_PceNotifications"
        Me.INDGvInvoice_PceNotifications.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvInvoice_CenterAttention
        '
        resources.ApplyResources(Me.INDGvInvoice_CenterAttention, "INDGvInvoice_CenterAttention")
        Me.INDGvInvoice_CenterAttention.FieldName = "CenterAttention"
        Me.INDGvInvoice_CenterAttention.Name = "INDGvInvoice_CenterAttention"
        Me.INDGvInvoice_CenterAttention.OptionsColumn.AllowEdit = False
        Me.INDGvInvoice_CenterAttention.OptionsColumn.AllowFocus = False
        '
        'INDColCurrency
        '
        resources.ApplyResources(Me.INDColCurrency, "INDColCurrency")
        Me.INDColCurrency.FieldName = "CurrencyAbbreviation"
        Me.INDColCurrency.Name = "INDColCurrency"
        Me.INDColCurrency.OptionsColumn.AllowEdit = False
        Me.INDColCurrency.OptionsColumn.AllowFocus = False
        '
        'INDGcCreditNote
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCreditNote, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCreditNote, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCreditNote, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCreditNote, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCreditNote, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCreditNote, False)
        resources.ApplyResources(Me.INDGcCreditNote, "INDGcCreditNote")
        Me.INDGcCreditNote.MainView = Me.INDGvCreditNote
        Me.INDGcCreditNote.Name = "INDGcCreditNote"
        Me.INDGcCreditNote.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvCreditNote_PceDetails, Me.INDGvCreditNote_PceNotifications})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCreditNote, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCreditNote.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCreditNote})
        '
        'INDGvCreditNote
        '
        Me.INDGvCreditNote.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCreditNote.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvCreditNote.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvCreditNote.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCreditNote.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCreditNote.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvCreditNote.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvCreditNote.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCreditNote.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvCreditNote.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvCreditNote.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCreditNote.Appearance.Row.Font = CType(resources.GetObject("INDGvCreditNote.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvCreditNote.Appearance.Row.Options.UseFont = True
        Me.INDGvCreditNote.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvCreditNote.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvCreditNote.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCreditNote.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCreditNote_UnboundSelection, Me.INDGvCreditNote_InvoiceNumber, Me.INDGvCreditNote_DocumentNumber, Me.INDGvCreditNote_CUFE, Me.INDGvCreditNote_CustomerNitName, Me.INDGvCreditNote_StatusName, Me.INDGvCreditNote_DocumentDate, Me.INDGvCreditNote_ShippingDate, Me.INDGvCreditNote_Details, Me.INDGvCreditNote_Notifications})
        Me.INDGvCreditNote.GridControl = Me.INDGcCreditNote
        Me.INDGvCreditNote.Name = "INDGvCreditNote"
        Me.INDGvCreditNote.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCreditNote.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvCreditNote.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCreditNote.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCreditNote.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCreditNote.OptionsView.ShowFooter = True
        Me.INDGvCreditNote.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCreditNote, False)
        '
        'INDGvCreditNote_UnboundSelection
        '
        resources.ApplyResources(Me.INDGvCreditNote_UnboundSelection, "INDGvCreditNote_UnboundSelection")
        Me.INDGvCreditNote_UnboundSelection.FieldName = "INDGvCreditNote_UnboundSelection"
        Me.INDGvCreditNote_UnboundSelection.Name = "INDGvCreditNote_UnboundSelection"
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvCreditNote_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvCreditNote_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'INDGvCreditNote_InvoiceNumber
        '
        resources.ApplyResources(Me.INDGvCreditNote_InvoiceNumber, "INDGvCreditNote_InvoiceNumber")
        Me.INDGvCreditNote_InvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDGvCreditNote_InvoiceNumber.Name = "INDGvCreditNote_InvoiceNumber"
        Me.INDGvCreditNote_InvoiceNumber.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_InvoiceNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_DocumentNumber
        '
        resources.ApplyResources(Me.INDGvCreditNote_DocumentNumber, "INDGvCreditNote_DocumentNumber")
        Me.INDGvCreditNote_DocumentNumber.FieldName = "DocumentNumberWithPrefix"
        Me.INDGvCreditNote_DocumentNumber.Name = "INDGvCreditNote_DocumentNumber"
        Me.INDGvCreditNote_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_DocumentNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_CUFE
        '
        resources.ApplyResources(Me.INDGvCreditNote_CUFE, "INDGvCreditNote_CUFE")
        Me.INDGvCreditNote_CUFE.FieldName = "CUFE"
        Me.INDGvCreditNote_CUFE.Name = "INDGvCreditNote_CUFE"
        Me.INDGvCreditNote_CUFE.OptionsColumn.ReadOnly = True
        '
        'INDGvCreditNote_CustomerNitName
        '
        resources.ApplyResources(Me.INDGvCreditNote_CustomerNitName, "INDGvCreditNote_CustomerNitName")
        Me.INDGvCreditNote_CustomerNitName.FieldName = "CustomerPartyId.NitName"
        Me.INDGvCreditNote_CustomerNitName.Name = "INDGvCreditNote_CustomerNitName"
        Me.INDGvCreditNote_CustomerNitName.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_CustomerNitName.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_StatusName
        '
        resources.ApplyResources(Me.INDGvCreditNote_StatusName, "INDGvCreditNote_StatusName")
        Me.INDGvCreditNote_StatusName.FieldName = "StatusName"
        Me.INDGvCreditNote_StatusName.Name = "INDGvCreditNote_StatusName"
        Me.INDGvCreditNote_StatusName.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_StatusName.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_DocumentDate
        '
        resources.ApplyResources(Me.INDGvCreditNote_DocumentDate, "INDGvCreditNote_DocumentDate")
        Me.INDGvCreditNote_DocumentDate.DisplayFormat.FormatString = "G"
        Me.INDGvCreditNote_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvCreditNote_DocumentDate.FieldName = "DocumentDate"
        Me.INDGvCreditNote_DocumentDate.Name = "INDGvCreditNote_DocumentDate"
        Me.INDGvCreditNote_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_DocumentDate.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_ShippingDate
        '
        resources.ApplyResources(Me.INDGvCreditNote_ShippingDate, "INDGvCreditNote_ShippingDate")
        Me.INDGvCreditNote_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvCreditNote_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvCreditNote_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvCreditNote_ShippingDate.Name = "INDGvCreditNote_ShippingDate"
        Me.INDGvCreditNote_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvCreditNote_ShippingDate.OptionsColumn.AllowFocus = False
        '
        'INDGvCreditNote_Details
        '
        resources.ApplyResources(Me.INDGvCreditNote_Details, "INDGvCreditNote_Details")
        Me.INDGvCreditNote_Details.ColumnEdit = Me.INDGvCreditNote_PceDetails
        Me.INDGvCreditNote_Details.FieldName = "INDGvCreditNote_Details"
        Me.INDGvCreditNote_Details.Name = "INDGvCreditNote_Details"
        Me.INDGvCreditNote_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvCreditNote_PceDetails
        '
        Me.INDGvCreditNote_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvCreditNote_PceDetails, "INDGvCreditNote_PceDetails")
        Me.INDGvCreditNote_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvCreditNote_PceDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvCreditNote_PceDetails.Name = "INDGvCreditNote_PceDetails"
        Me.INDGvCreditNote_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvCreditNote_Notifications
        '
        resources.ApplyResources(Me.INDGvCreditNote_Notifications, "INDGvCreditNote_Notifications")
        Me.INDGvCreditNote_Notifications.ColumnEdit = Me.INDGvCreditNote_PceNotifications
        Me.INDGvCreditNote_Notifications.FieldName = "INDGvCreditNote_Notifications"
        Me.INDGvCreditNote_Notifications.Name = "INDGvCreditNote_Notifications"
        '
        'INDGvCreditNote_PceNotifications
        '
        Me.INDGvCreditNote_PceNotifications.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvCreditNote_PceNotifications, "INDGvCreditNote_PceNotifications")
        Me.INDGvCreditNote_PceNotifications.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvCreditNote_PceNotifications.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvCreditNote_PceNotifications.Name = "INDGvCreditNote_PceNotifications"
        Me.INDGvCreditNote_PceNotifications.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGcDebitNote
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDebitNote, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDebitNote, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDebitNote, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDebitNote, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDebitNote, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDebitNote, False)
        resources.ApplyResources(Me.INDGcDebitNote, "INDGcDebitNote")
        Me.INDGcDebitNote.MainView = Me.INDGvDebitNote
        Me.INDGcDebitNote.Name = "INDGcDebitNote"
        Me.INDGcDebitNote.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvDebitNote_PceDetails, Me.INDGvDebitNote_PceNotifications})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDebitNote, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDebitNote.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDebitNote})
        '
        'INDGvDebitNote
        '
        Me.INDGvDebitNote.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDebitNote.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvDebitNote.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvDebitNote.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDebitNote.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDebitNote.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvDebitNote.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvDebitNote.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDebitNote.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvDebitNote.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvDebitNote.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDebitNote.Appearance.Row.Font = CType(resources.GetObject("INDGvDebitNote.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvDebitNote.Appearance.Row.Options.UseFont = True
        Me.INDGvDebitNote.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvDebitNote.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvDebitNote.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDebitNote.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvDebitNote_UnboundSelection, Me.INDGvDebitNote_InvoiceNumber, Me.INDGvDebitNote_DocumentNumber, Me.INDGvDebitNote_CUFE, Me.INDGvDebitNote_CustomerNitName, Me.INDGvDebitNote_Status, Me.INDGvDebitNote_DocumentDate, Me.INDGvDebitNote_ShippingDate, Me.INDGvDebitNote_Details, Me.INDGvDebitNote_Notifications})
        Me.INDGvDebitNote.GridControl = Me.INDGcDebitNote
        Me.INDGvDebitNote.Name = "INDGvDebitNote"
        Me.INDGvDebitNote.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvDebitNote.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvDebitNote.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDebitNote.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDebitNote.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDebitNote.OptionsView.ShowFooter = True
        Me.INDGvDebitNote.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDebitNote, False)
        '
        'INDGvDebitNote_UnboundSelection
        '
        resources.ApplyResources(Me.INDGvDebitNote_UnboundSelection, "INDGvDebitNote_UnboundSelection")
        Me.INDGvDebitNote_UnboundSelection.FieldName = "INDGvDebitNote_UnboundSelection"
        Me.INDGvDebitNote_UnboundSelection.Name = "INDGvDebitNote_UnboundSelection"
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvDebitNote_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDebitNote_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'INDGvDebitNote_InvoiceNumber
        '
        resources.ApplyResources(Me.INDGvDebitNote_InvoiceNumber, "INDGvDebitNote_InvoiceNumber")
        Me.INDGvDebitNote_InvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDGvDebitNote_InvoiceNumber.Name = "INDGvDebitNote_InvoiceNumber"
        Me.INDGvDebitNote_InvoiceNumber.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_InvoiceNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_DocumentNumber
        '
        resources.ApplyResources(Me.INDGvDebitNote_DocumentNumber, "INDGvDebitNote_DocumentNumber")
        Me.INDGvDebitNote_DocumentNumber.FieldName = "DocumentNumberWithPrefix"
        Me.INDGvDebitNote_DocumentNumber.Name = "INDGvDebitNote_DocumentNumber"
        Me.INDGvDebitNote_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_DocumentNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_CUFE
        '
        resources.ApplyResources(Me.INDGvDebitNote_CUFE, "INDGvDebitNote_CUFE")
        Me.INDGvDebitNote_CUFE.FieldName = "CUFE"
        Me.INDGvDebitNote_CUFE.Name = "INDGvDebitNote_CUFE"
        Me.INDGvDebitNote_CUFE.OptionsColumn.ReadOnly = True
        '
        'INDGvDebitNote_CustomerNitName
        '
        resources.ApplyResources(Me.INDGvDebitNote_CustomerNitName, "INDGvDebitNote_CustomerNitName")
        Me.INDGvDebitNote_CustomerNitName.FieldName = "CustomerPartyId.NitName"
        Me.INDGvDebitNote_CustomerNitName.Name = "INDGvDebitNote_CustomerNitName"
        Me.INDGvDebitNote_CustomerNitName.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_CustomerNitName.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_Status
        '
        resources.ApplyResources(Me.INDGvDebitNote_Status, "INDGvDebitNote_Status")
        Me.INDGvDebitNote_Status.FieldName = "StatusName"
        Me.INDGvDebitNote_Status.Name = "INDGvDebitNote_Status"
        Me.INDGvDebitNote_Status.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_Status.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_DocumentDate
        '
        resources.ApplyResources(Me.INDGvDebitNote_DocumentDate, "INDGvDebitNote_DocumentDate")
        Me.INDGvDebitNote_DocumentDate.DisplayFormat.FormatString = "G"
        Me.INDGvDebitNote_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvDebitNote_DocumentDate.FieldName = "DocumentDate"
        Me.INDGvDebitNote_DocumentDate.Name = "INDGvDebitNote_DocumentDate"
        Me.INDGvDebitNote_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_DocumentDate.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_ShippingDate
        '
        resources.ApplyResources(Me.INDGvDebitNote_ShippingDate, "INDGvDebitNote_ShippingDate")
        Me.INDGvDebitNote_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvDebitNote_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvDebitNote_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvDebitNote_ShippingDate.Name = "INDGvDebitNote_ShippingDate"
        Me.INDGvDebitNote_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvDebitNote_ShippingDate.OptionsColumn.AllowFocus = False
        '
        'INDGvDebitNote_Details
        '
        resources.ApplyResources(Me.INDGvDebitNote_Details, "INDGvDebitNote_Details")
        Me.INDGvDebitNote_Details.ColumnEdit = Me.INDGvDebitNote_PceDetails
        Me.INDGvDebitNote_Details.FieldName = "INDGvDebitNote_Details"
        Me.INDGvDebitNote_Details.Name = "INDGvDebitNote_Details"
        Me.INDGvDebitNote_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvDebitNote_PceDetails
        '
        Me.INDGvDebitNote_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvDebitNote_PceDetails, "INDGvDebitNote_PceDetails")
        Me.INDGvDebitNote_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvDebitNote_PceDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvDebitNote_PceDetails.Name = "INDGvDebitNote_PceDetails"
        Me.INDGvDebitNote_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvDebitNote_Notifications
        '
        resources.ApplyResources(Me.INDGvDebitNote_Notifications, "INDGvDebitNote_Notifications")
        Me.INDGvDebitNote_Notifications.ColumnEdit = Me.INDGvDebitNote_PceNotifications
        Me.INDGvDebitNote_Notifications.FieldName = "INDGvDebitNote_Notifications"
        Me.INDGvDebitNote_Notifications.Name = "INDGvDebitNote_Notifications"
        Me.INDGvDebitNote_Notifications.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvDebitNote_PceNotifications
        '
        Me.INDGvDebitNote_PceNotifications.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvDebitNote_PceNotifications, "INDGvDebitNote_PceNotifications")
        Me.INDGvDebitNote_PceNotifications.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvDebitNote_PceNotifications.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvDebitNote_PceNotifications.Name = "INDGvDebitNote_PceNotifications"
        Me.INDGvDebitNote_PceNotifications.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDSleOperatingUnit)
        Me.PanelControl1.Controls.Add(Me.INDPceDetail)
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Name = "PanelControl1"
        '
        'INDSleOperatingUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleOperatingUnit, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleOperatingUnit, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleOperatingUnit, False)
        resources.ApplyResources(Me.INDSleOperatingUnit, "INDSleOperatingUnit")
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.INDSleOperatingUnit.Name = "INDSleOperatingUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.INDSleOperatingUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleOperatingUnit.Properties.Appearance.BackColor2 = CType(resources.GetObject("INDSleOperatingUnit.Properties.Appearance.BackColor2"), System.Drawing.Color)
        Me.INDSleOperatingUnit.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleOperatingUnit.Properties.Appearance.Font = CType(resources.GetObject("INDSleOperatingUnit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleOperatingUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleOperatingUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleOperatingUnit.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleOperatingUnit.Properties.Appearance.Options.UseFont = True
        Me.INDSleOperatingUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleOperatingUnit.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleOperatingUnit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleOperatingUnit.Properties.DisplayMember = "UnitName"
        Me.INDSleOperatingUnit.Properties.NullText = resources.GetString("INDSleOperatingUnit.Properties.NullText")
        Me.INDSleOperatingUnit.Properties.PopupSizeable = False
        Me.INDSleOperatingUnit.Properties.PopupView = Me.INDGvOperatingUnit
        Me.INDSleOperatingUnit.Properties.ShowClearButton = False
        Me.INDSleOperatingUnit.Properties.ShowFooter = False
        Me.INDSleOperatingUnit.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleOperatingUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleOperatingUnit, True)
        Me.INDSleOperatingUnit.StyleController = Me.INDLcMainData
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleOperatingUnit, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleOperatingUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleOperatingUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleOperatingUnit, False)
        '
        'INDGvOperatingUnit
        '
        Me.INDGvOperatingUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvOperatingUnit.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvOperatingUnit.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvOperatingUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvOperatingUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvOperatingUnit.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvOperatingUnit.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvOperatingUnit.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvOperatingUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvOperatingUnit.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvOperatingUnit.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvOperatingUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvOperatingUnit.Appearance.Row.Font = CType(resources.GetObject("INDGvOperatingUnit.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvOperatingUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvOperatingUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvOperatingUnit_Code, Me.INDGvOperatingUnit_Name})
        Me.INDGvOperatingUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvOperatingUnit.Name = "INDGvOperatingUnit"
        Me.INDGvOperatingUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvOperatingUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvOperatingUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvOperatingUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvOperatingUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvOperatingUnit, False)
        '
        'INDGvOperatingUnit_Code
        '
        resources.ApplyResources(Me.INDGvOperatingUnit_Code, "INDGvOperatingUnit_Code")
        Me.INDGvOperatingUnit_Code.FieldName = "UnitCode"
        Me.INDGvOperatingUnit_Code.Name = "INDGvOperatingUnit_Code"
        '
        'INDGvOperatingUnit_Name
        '
        resources.ApplyResources(Me.INDGvOperatingUnit_Name, "INDGvOperatingUnit_Name")
        Me.INDGvOperatingUnit_Name.FieldName = "UnitName"
        Me.INDGvOperatingUnit_Name.Name = "INDGvOperatingUnit_Name"
        '
        'INDPceDetail
        '
        resources.ApplyResources(Me.INDPceDetail, "INDPceDetail")
        Me.INDPceDetail.Name = "INDPceDetail"
        Me.INDPceDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPceDetail.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPceDetail.StyleController = Me.INDLcMainData
        '
        'INDDdbMenuActions
        '
        Me.INDDdbMenuActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDdbMenuActions.DropDownControl = Me.INDPmActions
        Me.INDDdbMenuActions.ImageOptions.Image = CType(resources.GetObject("INDDdbMenuActions.ImageOptions.Image"), System.Drawing.Image)
        Me.INDDdbMenuActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.INDDdbMenuActions, "INDDdbMenuActions")
        Me.INDDdbMenuActions.Name = "INDDdbMenuActions"
        Me.INDDdbMenuActions.StyleController = Me.INDLcMainData
        '
        'INDPmActions
        '
        Me.INDPmActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRefresh)})
        Me.INDPmActions.Manager = Me.BarManager1
        Me.INDPmActions.Name = "INDPmActions"
        '
        'INDCcbeStatus
        '
        resources.ApplyResources(Me.INDCcbeStatus, "INDCcbeStatus")
        Me.INDCcbeStatus.Name = "INDCcbeStatus"
        Me.INDCcbeStatus.Properties.Appearance.Font = CType(resources.GetObject("INDCcbeStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDCcbeStatus.Properties.Appearance.Options.UseFont = True
        Me.INDCcbeStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDCcbeStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDCcbeStatus.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.CheckedListBoxItem() {New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items"), Object), resources.GetString("INDCcbeStatus.Properties.Items1"), CType(resources.GetObject("INDCcbeStatus.Properties.Items2"), System.Windows.Forms.CheckState)), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items3"), Object), resources.GetString("INDCcbeStatus.Properties.Items4"), CType(resources.GetObject("INDCcbeStatus.Properties.Items5"), System.Windows.Forms.CheckState)), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items6"), Object), resources.GetString("INDCcbeStatus.Properties.Items7"), CType(resources.GetObject("INDCcbeStatus.Properties.Items8"), System.Windows.Forms.CheckState)), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items9"), Object), resources.GetString("INDCcbeStatus.Properties.Items10"), CType(resources.GetObject("INDCcbeStatus.Properties.Items11"), System.Windows.Forms.CheckState)), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items12"), Object), resources.GetString("INDCcbeStatus.Properties.Items13"), CType(resources.GetObject("INDCcbeStatus.Properties.Items14"), System.Windows.Forms.CheckState)), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDCcbeStatus.Properties.Items15"), Object), resources.GetString("INDCcbeStatus.Properties.Items16"), CType(resources.GetObject("INDCcbeStatus.Properties.Items17"), System.Windows.Forms.CheckState))})
        Me.INDCcbeStatus.StyleController = Me.INDLcMainData
        '
        'INDGcDocumentSupport
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDocumentSupport, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDocumentSupport, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDocumentSupport, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDocumentSupport, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDocumentSupport, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDocumentSupport, False)
        resources.ApplyResources(Me.INDGcDocumentSupport, "INDGcDocumentSupport")
        Me.INDGcDocumentSupport.MainView = Me.INDGvDocumentSupport
        Me.INDGcDocumentSupport.Name = "INDGcDocumentSupport"
        Me.INDGcDocumentSupport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvSupportDocument_PceDetails, Me.RepositoryItemPopupContainerEdit2})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDocumentSupport, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDocumentSupport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDocumentSupport})
        '
        'INDGvDocumentSupport
        '
        Me.INDGvDocumentSupport.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDocumentSupport.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvDocumentSupport.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvDocumentSupport.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDocumentSupport.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDocumentSupport.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvDocumentSupport.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvDocumentSupport.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDocumentSupport.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvDocumentSupport.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvDocumentSupport.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDocumentSupport.Appearance.Row.Font = CType(resources.GetObject("INDGvDocumentSupport.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvDocumentSupport.Appearance.Row.Options.UseFont = True
        Me.INDGvDocumentSupport.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvDocumentSupport.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvDocumentSupport.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDocumentSupport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvDocumentSupport_UnboundSelection, Me.INDGvDocumentSupport_SourceDocument, Me.INDGvDocumentSupport_DocumentNumber, Me.INDGvDocumentSupport_SupplierThirdPartyId, Me.INDGvDocumentSupport_StatusName, Me.INDGvDocumentSupport_DocumentDate, Me.INDGvDocumentSupport_ShippingDate, Me.INDGvSupportDocument_Details})
        Me.INDGvDocumentSupport.GridControl = Me.INDGcDocumentSupport
        Me.INDGvDocumentSupport.Name = "INDGvDocumentSupport"
        Me.INDGvDocumentSupport.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvDocumentSupport.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvDocumentSupport.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDocumentSupport.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDocumentSupport.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDocumentSupport.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDocumentSupport, False)
        '
        'INDGvDocumentSupport_UnboundSelection
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_UnboundSelection, "INDGvDocumentSupport_UnboundSelection")
        Me.INDGvDocumentSupport_UnboundSelection.FieldName = "INDGvDocumentSupport_UnboundSelection"
        Me.INDGvDocumentSupport_UnboundSelection.Name = "INDGvDocumentSupport_UnboundSelection"
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvDocumentSupport_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDocumentSupport_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'INDGvDocumentSupport_SourceDocument
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_SourceDocument, "INDGvDocumentSupport_SourceDocument")
        Me.INDGvDocumentSupport_SourceDocument.FieldName = "EntityNameSource"
        Me.INDGvDocumentSupport_SourceDocument.Name = "INDGvDocumentSupport_SourceDocument"
        Me.INDGvDocumentSupport_SourceDocument.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_SourceDocument.OptionsColumn.AllowFocus = False
        '
        'INDGvDocumentSupport_DocumentNumber
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_DocumentNumber, "INDGvDocumentSupport_DocumentNumber")
        Me.INDGvDocumentSupport_DocumentNumber.FieldName = "DocumentNumber"
        Me.INDGvDocumentSupport_DocumentNumber.Name = "INDGvDocumentSupport_DocumentNumber"
        Me.INDGvDocumentSupport_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_DocumentNumber.OptionsColumn.AllowFocus = False
        '
        'INDGvDocumentSupport_SupplierThirdPartyId
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_SupplierThirdPartyId, "INDGvDocumentSupport_SupplierThirdPartyId")
        Me.INDGvDocumentSupport_SupplierThirdPartyId.FieldName = "SupplierThirdPartyId.NitName"
        Me.INDGvDocumentSupport_SupplierThirdPartyId.Name = "INDGvDocumentSupport_SupplierThirdPartyId"
        Me.INDGvDocumentSupport_SupplierThirdPartyId.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_SupplierThirdPartyId.OptionsColumn.AllowFocus = False
        '
        'INDGvDocumentSupport_StatusName
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_StatusName, "INDGvDocumentSupport_StatusName")
        Me.INDGvDocumentSupport_StatusName.FieldName = "StatusName"
        Me.INDGvDocumentSupport_StatusName.Name = "INDGvDocumentSupport_StatusName"
        Me.INDGvDocumentSupport_StatusName.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_StatusName.OptionsColumn.AllowFocus = False
        '
        'INDGvDocumentSupport_DocumentDate
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_DocumentDate, "INDGvDocumentSupport_DocumentDate")
        Me.INDGvDocumentSupport_DocumentDate.DisplayFormat.FormatString = "G"
        Me.INDGvDocumentSupport_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvDocumentSupport_DocumentDate.FieldName = "DocumentDate"
        Me.INDGvDocumentSupport_DocumentDate.Name = "INDGvDocumentSupport_DocumentDate"
        Me.INDGvDocumentSupport_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_DocumentDate.OptionsColumn.AllowFocus = False
        '
        'INDGvDocumentSupport_ShippingDate
        '
        resources.ApplyResources(Me.INDGvDocumentSupport_ShippingDate, "INDGvDocumentSupport_ShippingDate")
        Me.INDGvDocumentSupport_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvDocumentSupport_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvDocumentSupport_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvDocumentSupport_ShippingDate.Name = "INDGvDocumentSupport_ShippingDate"
        Me.INDGvDocumentSupport_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvDocumentSupport_ShippingDate.OptionsColumn.AllowFocus = False
        '
        'INDGvSupportDocument_Details
        '
        resources.ApplyResources(Me.INDGvSupportDocument_Details, "INDGvSupportDocument_Details")
        Me.INDGvSupportDocument_Details.ColumnEdit = Me.INDGvSupportDocument_PceDetails
        Me.INDGvSupportDocument_Details.FieldName = "INDGvSupportDocument_Details"
        Me.INDGvSupportDocument_Details.Name = "INDGvSupportDocument_Details"
        Me.INDGvSupportDocument_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDGvSupportDocument_PceDetails
        '
        Me.INDGvSupportDocument_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDGvSupportDocument_PceDetails, "INDGvSupportDocument_PceDetails")
        Me.INDGvSupportDocument_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGvSupportDocument_PceDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGvSupportDocument_PceDetails.Name = "INDGvSupportDocument_PceDetails"
        Me.INDGvSupportDocument_PceDetails.PopupControl = Me.INDPccElectronicDocumentDetails
        Me.INDGvSupportDocument_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.RepositoryItemPopupContainerEdit2, "RepositoryItemPopupContainerEdit2")
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit2.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        Me.RepositoryItemPopupContainerEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDLcgDocumentSupport
        '
        Me.INDLcgDocumentSupport.AppearanceGroup.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDocumentSupport.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgDocumentSupport.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgDocumentSupport.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDocumentSupport, False)
        Me.INDLcgDocumentSupport.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.INDLcgDocumentSupport.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgDocumentSupport.Name = "INDLcgDocumentSupport"
        Me.INDLcgDocumentSupport.Size = New System.Drawing.Size(966, 446)
        resources.ApplyResources(Me.INDLcgDocumentSupport, "INDLcgDocumentSupport")
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDGcDocumentSupport
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(966, 446)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.INDTcgElectronicDocument})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1010, 591)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(990, 71)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.PanelControl1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(1, 1)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(709, 65)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDDdbMenuActions
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(709, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(66, 65)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(66, 65)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 2)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(66, 65)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDCcbeStatus
        Me.LayoutControlItem5.Location = New System.Drawing.Point(775, 0)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 0, 2)
        Me.LayoutControlItem5.Size = New System.Drawing.Size(209, 65)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(126, 17)
        Me.LayoutControlItem5.TextToControlDistance = 0
        '
        'INDTcgElectronicDocument
        '
        Me.INDTcgElectronicDocument.Location = New System.Drawing.Point(0, 71)
        Me.INDTcgElectronicDocument.Name = "INDTcgElectronicDocument"
        Me.INDTcgElectronicDocument.SelectedTabPage = Me.INDLcgCreditNote
        Me.INDTcgElectronicDocument.Size = New System.Drawing.Size(990, 500)
        Me.INDTcgElectronicDocument.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgInvoice, Me.INDLcgDebitNote, Me.INDLcgCreditNote})
        '
        'INDLcgCreditNote
        '
        Me.INDLcgCreditNote.AppearanceGroup.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCreditNote.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgCreditNote.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgCreditNote.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCreditNote, False)
        Me.INDLcgCreditNote.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.INDLcgCreditNote.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCreditNote.Name = "INDLcgCreditNote"
        Me.INDLcgCreditNote.Size = New System.Drawing.Size(966, 445)
        resources.ApplyResources(Me.INDLcgCreditNote, "INDLcgCreditNote")
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcCreditNote
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(966, 445)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLcgInvoice
        '
        Me.INDLcgInvoice.AppearanceGroup.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceGroup.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgInvoice.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgInvoice.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgInvoice.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgInvoice, False)
        resources.ApplyResources(Me.INDLcgInvoice, "INDLcgInvoice")
        Me.INDLcgInvoice.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6})
        Me.INDLcgInvoice.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgInvoice.Name = "INDLcgInvoice"
        Me.INDLcgInvoice.Size = New System.Drawing.Size(966, 445)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDGcInvoice
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(966, 445)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'INDLcgDebitNote
        '
        Me.INDLcgDebitNote.AppearanceGroup.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDebitNote.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgDebitNote.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgDebitNote.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDebitNote, False)
        Me.INDLcgDebitNote.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDLcgDebitNote.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgDebitNote.Name = "INDLcgDebitNote"
        Me.INDLcgDebitNote.Size = New System.Drawing.Size(966, 445)
        resources.ApplyResources(Me.INDLcgDebitNote, "INDLcgDebitNote")
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDebitNote
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(966, 445)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'GridColumn11
        '
        Me.GridColumn11.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowIncrementalSearch = False
        Me.GridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowMove = False
        Me.GridColumn11.OptionsColumn.AllowShowHide = False
        Me.GridColumn11.OptionsColumn.AllowSize = False
        Me.GridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.ShowCaption = False
        Me.GridColumn11.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        resources.ApplyResources(Me.GridColumn11, "GridColumn11")
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiProcess, Me.INDBbiSendNotification, Me.INDBbiPrint, Me.INDBbiReSend})
        Me.BarManager2.MaxItemId = 32
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl5, "BarDockControl5")
        Me.BarDockControl5.Manager = Me.BarManager2
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl6, "BarDockControl6")
        Me.BarDockControl6.Manager = Me.BarManager2
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl7, "BarDockControl7")
        Me.BarDockControl7.Manager = Me.BarManager2
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl8, "BarDockControl8")
        Me.BarDockControl8.Manager = Me.BarManager2
        '
        'INDBbiProcess
        '
        resources.ApplyResources(Me.INDBbiProcess, "INDBbiProcess")
        Me.INDBbiProcess.Id = 17
        Me.INDBbiProcess.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Refresh_16x16_blue
        Me.INDBbiProcess.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBbiProcess.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBbiProcess.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBbiProcess.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBbiProcess.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBbiProcess.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBbiProcess.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBbiProcess.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBbiProcess.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiProcess.Name = "INDBbiProcess"
        '
        'INDBbiSendNotification
        '
        resources.ApplyResources(Me.INDBbiSendNotification, "INDBbiSendNotification")
        Me.INDBbiSendNotification.Id = 29
        Me.INDBbiSendNotification.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.calificar16x16
        Me.INDBbiSendNotification.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBbiSendNotification.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBbiSendNotification.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBbiSendNotification.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBbiSendNotification.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBbiSendNotification.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBbiSendNotification.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBbiSendNotification.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBbiSendNotification.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiSendNotification.Name = "INDBbiSendNotification"
        '
        'INDBbiPrint
        '
        resources.ApplyResources(Me.INDBbiPrint, "INDBbiPrint")
        Me.INDBbiPrint.Id = 30
        Me.INDBbiPrint.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Print_16x16_blue
        Me.INDBbiPrint.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBbiPrint.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBbiPrint.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBbiPrint.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBbiPrint.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBbiPrint.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBbiPrint.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBbiPrint.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBbiPrint.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrint.Name = "INDBbiPrint"
        '
        'INDBbiReSend
        '
        resources.ApplyResources(Me.INDBbiReSend, "INDBbiReSend")
        Me.INDBbiReSend.Id = 31
        Me.INDBbiReSend.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Undo_16x16_blue
        Me.INDBbiReSend.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBbiReSend.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBbiReSend.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiReSend.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBbiReSend.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBbiReSend.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiReSend.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBbiReSend.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBbiReSend.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiReSend.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBbiReSend.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBbiReSend.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiReSend.Name = "INDBbiReSend"
        '
        'INDPopMenuActions2
        '
        Me.INDPopMenuActions2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrint), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiProcess), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiSendNotification), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiReSend)})
        Me.INDPopMenuActions2.Manager = Me.BarManager2
        Me.INDPopMenuActions2.Name = "INDPopMenuActions2"
        '
        'FrmElectronicDocumentTraceability
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmElectronicDocumentTraceability"
        Me.Opacity = 1.0R
        Me.Tag = "2039"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.BarDockControl5, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl6, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl8, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl7, 0)
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDPccElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccElectronicDocumentNotifications.ResumeLayout(False)
        CType(Me.INDLcElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcElectronicDocumentNotifications.ResumeLayout(False)
        CType(Me.INDGcElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElectronicDocumentNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccElectronicDocumentDetails.ResumeLayout(False)
        CType(Me.INDLcElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcElectronicDocumentDetails.ResumeLayout(False)
        CType(Me.INDGcElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElectronicDocumentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvInvoice_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvInvoice_PceNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCreditNote_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCreditNote_PceNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDebitNote_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDebitNote_PceNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDSleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvOperatingUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCcbeStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDocumentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDocumentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSupportDocument_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDocumentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgElectronicDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleOperatingUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvOperatingUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvOperatingUnit_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvOperatingUnit_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPceDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDDdbMenuActions As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTcgElectronicDocument As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgDebitNote As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgInvoice As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgCreditNote As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcDebitNote As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDebitNote As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcCreditNote As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCreditNote As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcInvoice As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvInvoice_DocumentTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_CustomerNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_StatusName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_CustomerNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_CustomerNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_StatusName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents SkinBarSubItem1 As DevExpress.XtraBars.SkinBarSubItem
    Friend WithEvents INDBbiRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPmActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDPccElectronicDocumentDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcElectronicDocumentDetails As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgElectronicDocumentDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcElectronicDocumentDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvElectronicDocumentDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElectronicDocumentDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvElectronicDocumentDetails_Destination As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicDocumentDetails_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicDocumentDetails_Response As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicDocumentDetails_Comments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicDocumentDetails_ResponseData As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvCreditNote_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvDebitNote_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDCcbeStatus As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents INDPccElectronicDocumentNotifications As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcElectronicDocumentNotifications As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcElectronicDocumentNotifications As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvElectronicDocumentNotifications As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElectronicDocumentNotifications As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolElectronicDocumentNotifications_Email As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicDocumentNotifications_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicDocumentNotifications_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_Notifications As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_PceNotifications As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvDebitNote_InvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_Notifications As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_PceNotifications As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvCreditNote_InvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_Notifications As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_PceNotifications As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvInvoice_CenterAttention As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_CUFE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCreditNote_CUFE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_CUFE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiProcess As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiSendNotification As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDGvCreditNote_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvInvoice_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDebitNote_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicDocumentNotifications_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicDocumentDetails_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDLcgDocumentSupport As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcDocumentSupport As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDocumentSupport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvDocumentSupport_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_SourceDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_SupplierThirdPartyId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_StatusName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvDocumentSupport_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupportDocument_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupportDocument_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDBbiReSend As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDGvInvoice_InvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDColCurrency As DevExpress.XtraGrid.Columns.GridColumn
End Class
