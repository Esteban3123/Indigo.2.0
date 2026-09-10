Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMaintenancePlanDashboard
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMaintenancePlanDashboard))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtreeLocation = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDChkUnidadFuncional = New DevExpress.XtraEditors.CheckEdit()
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
        Me.INDSleUnidadFuncional = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDGcProgramacion = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProgramacion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemColorPickEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptProgramming = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDGvSinProgramar = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcSinProgramar = New DevExpress.XtraGrid.GridControl()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemColorPickEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDPceFilters = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPccFilter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.DdbMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDBbiMassive = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiRemoveProgramacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgData = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgWithProgramming = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgWithtOutProgramming = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkUnidadFuncional.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.INDSleUnidadFuncional.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProgramacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProgramacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemColorPickEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptProgramming, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSinProgramar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcSinProgramar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemColorPickEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccFilter.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgWithProgramming, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgWithtOutProgramming, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl2)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1234, 655)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1234, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1234, 98)
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
        Me.LayoutControl1.Controls.Add(Me.INDChkUnidadFuncional)
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
        Me.LayoutControl1.Controls.Add(Me.INDSleUnidadFuncional)
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
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(1, -12)
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
        'INDChkUnidadFuncional
        '
        Me.INDChkUnidadFuncional.EditValue = True
        Me.INDChkUnidadFuncional.Location = New System.Drawing.Point(864, 127)
        Me.INDChkUnidadFuncional.Name = "INDChkUnidadFuncional"
        Me.INDChkUnidadFuncional.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDChkUnidadFuncional.Properties.Appearance.Options.UseFont = True
        Me.INDChkUnidadFuncional.Properties.Caption = "Todos"
        Me.INDChkUnidadFuncional.Size = New System.Drawing.Size(66, 21)
        Me.INDChkUnidadFuncional.StyleController = Me.LayoutControl1
        Me.INDChkUnidadFuncional.TabIndex = 20
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView2, False)
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView3, False)
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView4, False)
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView5, False)
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView6, False)
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView7, False)
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
        'INDSleUnidadFuncional
        '
        Me.INDSleUnidadFuncional.EnterMoveNextControl = True
        Me.INDSleUnidadFuncional.Location = New System.Drawing.Point(583, 127)
        Me.INDSleUnidadFuncional.Name = "INDSleUnidadFuncional"
        Me.INDSleUnidadFuncional.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleUnidadFuncional.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSleUnidadFuncional.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleUnidadFuncional.Properties.Appearance.Options.UseFont = True
        Me.INDSleUnidadFuncional.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleUnidadFuncional.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleUnidadFuncional.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleUnidadFuncional.Properties.NullText = ""
        Me.INDSleUnidadFuncional.Properties.PopupView = Me.GridView8
        Me.INDSleUnidadFuncional.Size = New System.Drawing.Size(277, 24)
        Me.INDSleUnidadFuncional.StyleController = Me.LayoutControl1
        Me.INDSleUnidadFuncional.TabIndex = 7
        '
        'GridView8
        '
        Me.GridView8.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView8.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView8.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView8.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView8.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView8.Appearance.GroupRow.Options.UseFont = True
        Me.GridView8.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView8.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView8.Appearance.Row.Options.UseFont = True
        Me.GridView8.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView8.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView8.OptionsView.EnableAppearanceOddRow = True
        Me.GridView8.OptionsView.ShowAutoFilterRow = True
        Me.GridView8.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView8, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView8, False)
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem14, Me.LayoutControlItem15, Me.LayoutControlItem16, Me.LayoutControlItem17, Me.LayoutControlItem18, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem12, Me.LayoutControlItem13})
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
        Me.LayoutControlItem8.Control = Me.INDSleUnidadFuncional
        Me.LayoutControlItem8.Location = New System.Drawing.Point(460, 84)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(390, 28)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Unidad Funcional"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(106, 17)
        Me.LayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.INDChkUnidadFuncional
        Me.LayoutControlItem17.Location = New System.Drawing.Point(850, 84)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(70, 23)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(70, 28)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem17.TextVisible = False
        Me.LayoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 165)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(861, 44)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDGcProgramacion
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProgramacion, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProgramacion, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProgramacion, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProgramacion, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProgramacion, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProgramacion, False)
        Me.INDGcProgramacion.Location = New System.Drawing.Point(14, 148)
        Me.INDGcProgramacion.MainView = Me.INDGvProgramacion
        Me.INDGcProgramacion.Name = "INDGcProgramacion"
        Me.INDGcProgramacion.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptProgramming, Me.RepositoryItemColorPickEdit1})
        Me.INDGcProgramacion.Size = New System.Drawing.Size(1202, 484)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProgramacion, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcProgramacion.TabIndex = 1
        Me.INDGcProgramacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProgramacion})
        '
        'INDGvProgramacion
        '
        Me.INDGvProgramacion.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProgramacion.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProgramacion.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProgramacion.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProgramacion.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProgramacion.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProgramacion.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProgramacion.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProgramacion.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProgramacion.Appearance.Row.Options.UseFont = True
        Me.INDGvProgramacion.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProgramacion.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProgramacion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn32})
        Me.INDGvProgramacion.GridControl = Me.INDGcProgramacion
        Me.INDGvProgramacion.GroupCount = 1
        Me.INDGvProgramacion.Name = "INDGvProgramacion"
        Me.INDGvProgramacion.OptionsSelection.MultiSelect = True
        Me.INDGvProgramacion.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvProgramacion.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProgramacion.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProgramacion.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProgramacion.OptionsView.ShowGroupPanel = False
        Me.INDGvProgramacion.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn32, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvProgramacion, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProgramacion, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Placa"
        Me.GridColumn1.FieldName = "Plate"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 89
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "ArticleName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 193
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Marca"
        Me.GridColumn3.FieldName = "TrademarkCodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 111
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Modelo"
        Me.GridColumn4.FieldName = "Model"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.OptionsColumn.AllowMove = False
        Me.GridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 64
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Serie"
        Me.GridColumn5.FieldName = "Serie"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 146
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Ubicación"
        Me.GridColumn6.FieldName = "LocationCodeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn6.OptionsColumn.AllowMove = False
        Me.GridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 168
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Protocolo"
        Me.GridColumn7.FieldName = "ProtocolCodeName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 8
        Me.GridColumn7.Width = 139
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Periodicidad"
        Me.GridColumn8.FieldName = "Periodicidad"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 9
        Me.GridColumn8.Width = 71
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Responsable"
        Me.GridColumn9.FieldName = "ResponsibleCodeName"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 10
        Me.GridColumn9.Width = 195
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Último Mantenimiento"
        Me.GridColumn10.DisplayFormat.FormatString = "f"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn10.FieldName = "UltimoMantenimiento"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 11
        Me.GridColumn10.Width = 116
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Próximo Mantenimiento"
        Me.GridColumn11.DisplayFormat.FormatString = "f"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn11.FieldName = "ProximoMantenimiento"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 12
        Me.GridColumn11.Width = 153
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = " "
        Me.GridColumn12.ColumnEdit = Me.RepositoryItemColorPickEdit1
        Me.GridColumn12.FieldName = "ProtocolColor"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn12.OptionsColumn.AllowMove = False
        Me.GridColumn12.OptionsColumn.AllowSize = False
        Me.GridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 7
        Me.GridColumn12.Width = 48
        '
        'RepositoryItemColorPickEdit1
        '
        Me.RepositoryItemColorPickEdit1.AutoHeight = False
        Me.RepositoryItemColorPickEdit1.AutomaticColor = System.Drawing.Color.Black
        Me.RepositoryItemColorPickEdit1.ColorAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemColorPickEdit1.Name = "RepositoryItemColorPickEdit1"
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Tipo"
        Me.GridColumn32.FieldName = "TypeName"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowFocus = False
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 7
        Me.GridColumn32.Width = 92
        '
        'INDRptProgramming
        '
        Me.INDRptProgramming.AutoHeight = False
        Me.INDRptProgramming.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptProgramming.CloseOnLostFocus = False
        Me.INDRptProgramming.CloseOnOuterMouseClick = False
        Me.INDRptProgramming.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDRptProgramming.Name = "INDRptProgramming"
        Me.INDRptProgramming.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDGvSinProgramar
        '
        Me.INDGvSinProgramar.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSinProgramar.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSinProgramar.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSinProgramar.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSinProgramar.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSinProgramar.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSinProgramar.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSinProgramar.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSinProgramar.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSinProgramar.Appearance.Row.Options.UseFont = True
        Me.INDGvSinProgramar.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvSinProgramar.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvSinProgramar.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn26, Me.GridColumn27, Me.GridColumn28, Me.GridColumn29, Me.GridColumn30, Me.GridColumn31})
        Me.INDGvSinProgramar.GridControl = Me.INDGcSinProgramar
        Me.INDGvSinProgramar.Name = "INDGvSinProgramar"
        Me.INDGvSinProgramar.OptionsSelection.MultiSelect = True
        Me.INDGvSinProgramar.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvSinProgramar.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSinProgramar.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSinProgramar.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSinProgramar.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvSinProgramar, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSinProgramar, False)
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Placa"
        Me.GridColumn26.FieldName = "Plate"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        Me.GridColumn26.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn26.OptionsColumn.AllowMove = False
        Me.GridColumn26.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        Me.GridColumn26.Width = 73
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Descripción"
        Me.GridColumn27.FieldName = "ArticleName"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        Me.GridColumn27.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn27.OptionsColumn.AllowMove = False
        Me.GridColumn27.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 2
        Me.GridColumn27.Width = 157
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Marca"
        Me.GridColumn28.FieldName = "TrademarkCodeName"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        Me.GridColumn28.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn28.OptionsColumn.AllowMove = False
        Me.GridColumn28.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 3
        Me.GridColumn28.Width = 91
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Modelo"
        Me.GridColumn29.FieldName = "Model"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowFocus = False
        Me.GridColumn29.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn29.OptionsColumn.AllowMove = False
        Me.GridColumn29.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 4
        Me.GridColumn29.Width = 52
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Serie"
        Me.GridColumn30.FieldName = "Serie"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowEdit = False
        Me.GridColumn30.OptionsColumn.AllowFocus = False
        Me.GridColumn30.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn30.OptionsColumn.AllowMove = False
        Me.GridColumn30.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 5
        Me.GridColumn30.Width = 119
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Ubicación"
        Me.GridColumn31.FieldName = "LocationCodeName"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn31.OptionsColumn.AllowMove = False
        Me.GridColumn31.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 6
        Me.GridColumn31.Width = 137
        '
        'INDGcSinProgramar
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcSinProgramar, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcSinProgramar, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcSinProgramar, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcSinProgramar, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcSinProgramar, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcSinProgramar, False)
        Me.INDGcSinProgramar.Location = New System.Drawing.Point(14, 148)
        Me.INDGcSinProgramar.MainView = Me.INDGvSinProgramar
        Me.INDGcSinProgramar.Name = "INDGcSinProgramar"
        Me.INDGcSinProgramar.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit1, Me.RepositoryItemColorPickEdit2})
        Me.INDGcSinProgramar.Size = New System.Drawing.Size(1202, 484)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcSinProgramar, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcSinProgramar.TabIndex = 4
        Me.INDGcSinProgramar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvSinProgramar})
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.CloseOnLostFocus = False
        Me.RepositoryItemPopupContainerEdit1.CloseOnOuterMouseClick = False
        Me.RepositoryItemPopupContainerEdit1.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        Me.RepositoryItemPopupContainerEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'RepositoryItemColorPickEdit2
        '
        Me.RepositoryItemColorPickEdit2.AutoHeight = False
        Me.RepositoryItemColorPickEdit2.AutomaticColor = System.Drawing.Color.Black
        Me.RepositoryItemColorPickEdit2.ColorAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemColorPickEdit2.Name = "RepositoryItemColorPickEdit2"
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
        Me.INDPceFilters.Size = New System.Drawing.Size(1152, 46)
        Me.INDPceFilters.StyleController = Me.LayoutControl2
        Me.INDPceFilters.TabIndex = 2
        '
        'INDPccFilter
        '
        Me.INDPccFilter.Controls.Add(Me.PanelControl1)
        Me.INDPccFilter.Location = New System.Drawing.Point(40, 267)
        Me.INDPccFilter.Name = "INDPccFilter"
        Me.INDPccFilter.Size = New System.Drawing.Size(944, 209)
        Me.INDPccFilter.TabIndex = 3
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.DdbMenu)
        Me.LayoutControl2.Controls.Add(Me.INDGcSinProgramar)
        Me.LayoutControl2.Controls.Add(Me.INDPccFilter)
        Me.LayoutControl2.Controls.Add(Me.INDPceFilters)
        Me.LayoutControl2.Controls.Add(Me.INDGcProgramacion)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2352, 399, 574, 569)
        Me.LayoutControl2.Root = Me.LayoutControlGroup3
        Me.LayoutControl2.Size = New System.Drawing.Size(1230, 646)
        Me.LayoutControl2.TabIndex = 4
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'DdbMenu
        '
        Me.DdbMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DdbMenu.Appearance.Options.UseBackColor = True
        Me.DdbMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DdbMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.DdbMenu.DropDownControl = Me.PopupMenu1
        Me.DdbMenu.ImageOptions.Image = CType(resources.GetObject("DdbMenu.ImageOptions.Image"), System.Drawing.Image)
        Me.DdbMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.DdbMenu.Location = New System.Drawing.Point(1170, 43)
        Me.DdbMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.DdbMenu.Name = "DdbMenu"
        Me.DdbMenu.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DdbMenu.Size = New System.Drawing.Size(46, 46)
        Me.DdbMenu.StyleController = Me.LayoutControl2
        Me.DdbMenu.TabIndex = 5
        Me.DdbMenu.ToolTip = "Click para desplegar el menú de acciones"
        Me.DdbMenu.ToolTipTitle = "Menú de Acciones"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiMassive), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRemoveProgramacion)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'INDBbiMassive
        '
        Me.INDBbiMassive.Caption = "Programación Masiva"
        Me.INDBbiMassive.Id = 0
        Me.INDBbiMassive.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.Acceptance
        Me.INDBbiMassive.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBbiMassive.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiMassive.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiMassive.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiMassive.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiMassive.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiMassive.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiMassive.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiMassive.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
        Me.INDBbiMassive.Name = "INDBbiMassive"
        '
        'INDBbiRemoveProgramacion
        '
        Me.INDBbiRemoveProgramacion.Caption = "Eliminar Programación Masiva"
        Me.INDBbiRemoveProgramacion.Id = 1
        Me.INDBbiRemoveProgramacion.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.Reject
        Me.INDBbiRemoveProgramacion.ImageOptions.LargeImage = Global.Presentation.Maintenance.My.Resources.Resources.Reject
        Me.INDBbiRemoveProgramacion.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiRemoveProgramacion.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiRemoveProgramacion.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiRemoveProgramacion.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiRemoveProgramacion.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiRemoveProgramacion.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiRemoveProgramacion.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBbiRemoveProgramacion.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiRemoveProgramacion.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.INDBbiRemoveProgramacion.Name = "INDBbiRemoveProgramacion"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiMassive, Me.INDBbiRemoveProgramacion})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1234, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 777)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1234, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 772)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1234, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 772)
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
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup4, Me.INDTcgData})
        Me.LayoutControlGroup3.Name = "Root"
        Me.LayoutControlGroup3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1230, 646)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem19, Me.LayoutControlItem21})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(1230, 103)
        Me.LayoutControlGroup4.Text = "Filtros"
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.INDPceFilters
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 50)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(1, 50)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(1156, 50)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem19.TextVisible = False
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.DdbMenu
        Me.LayoutControlItem21.Location = New System.Drawing.Point(1156, 0)
        Me.LayoutControlItem21.MaxSize = New System.Drawing.Size(50, 50)
        Me.LayoutControlItem21.MinSize = New System.Drawing.Size(50, 50)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(50, 50)
        Me.LayoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem21.TextVisible = False
        '
        'INDTcgData
        '
        Me.INDTcgData.Location = New System.Drawing.Point(0, 103)
        Me.INDTcgData.Name = "INDTcgData"
        Me.INDTcgData.SelectedTabPage = Me.INDLcgWithProgramming
        Me.INDTcgData.Size = New System.Drawing.Size(1230, 543)
        Me.INDTcgData.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgWithtOutProgramming, Me.INDLcgWithProgramming})
        '
        'INDLcgWithProgramming
        '
        Me.INDLcgWithProgramming.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWithProgramming.AppearanceGroup.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWithProgramming.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithProgramming.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithProgramming.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgWithProgramming.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithProgramming.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgWithProgramming, False)
        Me.INDLcgWithProgramming.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem20})
        Me.INDLcgWithProgramming.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgWithProgramming.Name = "INDLcgWithProgramming"
        Me.INDLcgWithProgramming.Size = New System.Drawing.Size(1206, 488)
        Me.INDLcgWithProgramming.Text = "Activos con Plan de Mantenimiento y Metrología"
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.INDGcProgramacion
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(1206, 488)
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextVisible = False
        '
        'INDLcgWithtOutProgramming
        '
        Me.INDLcgWithtOutProgramming.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWithtOutProgramming.AppearanceGroup.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWithtOutProgramming.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWithtOutProgramming.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgWithtOutProgramming, False)
        Me.INDLcgWithtOutProgramming.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDLcgWithtOutProgramming.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgWithtOutProgramming.Name = "INDLcgWithtOutProgramming"
        Me.INDLcgWithtOutProgramming.Size = New System.Drawing.Size(1206, 488)
        Me.INDLcgWithtOutProgramming.Text = "Activos sin Programación"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcSinProgramar
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1206, 488)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        '
        'FrmMaintenancePlanDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 777)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmMaintenancePlanDashboard.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmMaintenancePlanDashboard"
        Me.Opacity = 1.0R
        Me.Tag = "2133"
        Me.Text = "Plan de Mantenimiento Preventivo y Metrología"
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
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkUnidadFuncional.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.INDSleUnidadFuncional.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProgramacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProgramacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemColorPickEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptProgramming, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSinProgramar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcSinProgramar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemColorPickEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccFilter.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgWithProgramming, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgWithtOutProgramming, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcProgramacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProgramacion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDBtnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDPccFilter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDPceFilters As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDChkUnidadFuncional As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkResponsable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoEquipo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoInventario As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkTipoResponsable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkArticulo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleActivo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleArticulo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleTipoResponsable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleTipoInventario As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleTipoEquipo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleResponsable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleUnidadFuncional As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtreeLocation As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRptProgramming As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemColorPickEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit
    Friend WithEvents INDTcgData As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgWithProgramming As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgWithtOutProgramming As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcSinProgramar As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvSinProgramar As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemColorPickEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DdbMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiMassive As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiRemoveProgramacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents IndigoGridView2 As IndigoGridView
End Class
