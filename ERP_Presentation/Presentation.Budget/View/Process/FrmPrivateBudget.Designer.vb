Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrivateBudget
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
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTreeList = New DevExpress.XtraTreeList.TreeList()
        Me.INDColRubro = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColAccountingAccount = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColThirdParty = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.IndColCostCenter = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColEnero = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueEnero = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.IndcolFebrero = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueFebrero = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColMarzo = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueMarzo = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColAbril = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueAbril = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColMayo = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueJunio = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColJunio = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColJulio = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueJulio = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColAgosto = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueAgosto = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.IndColSeptiembre = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueSeptiembre = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.IndColOctubre = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueOctubre = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColNoviembre = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueNoviembre = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColDiciembre = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDrepValueDiciembre = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDrepValueMayo = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDTreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueEnero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueFebrero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueMarzo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueAbril, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueJunio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueJulio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueAgosto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueSeptiembre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueOctubre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueNoviembre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueDiciembre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValueMayo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
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
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDTreeList)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1261, 570)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDTreeList
        '
        Me.INDTreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDTreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTreeList.Appearance.Row.Options.UseFont = True
        Me.INDTreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColRubro, Me.INDColAccountingAccount, Me.INDColThirdParty, Me.IndColCostCenter, Me.INDColEnero, Me.IndcolFebrero, Me.INDColMarzo, Me.INDColAbril, Me.INDColMayo, Me.INDColJunio, Me.INDColJulio, Me.INDColAgosto, Me.IndColSeptiembre, Me.IndColOctubre, Me.INDColNoviembre, Me.INDColDiciembre})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.INDTreeList, False)
        Me.INDTreeList.KeyFieldName = "IdVista"
        Me.INDTreeList.Location = New System.Drawing.Point(-659, 59)
        Me.INDTreeList.Name = "INDTreeList"
        Me.INDTreeList.OptionsBehavior.CanCloneNodesOnDrop = True
        Me.INDTreeList.OptionsBehavior.EnableFiltering = True
        Me.INDTreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.INDTreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.INDTreeList.OptionsView.EnableAppearanceOddRow = True
        Me.INDTreeList.ParentFieldName = "IdParent"
        Me.INDTreeList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepValueEnero, Me.INDrepValueFebrero, Me.INDrepValueMarzo, Me.INDrepValueAbril, Me.INDrepValueMayo, Me.INDrepValueJunio, Me.INDrepValueJulio, Me.INDrepValueAgosto, Me.INDrepValueSeptiembre, Me.INDrepValueOctubre, Me.INDrepValueNoviembre, Me.INDrepValueDiciembre})
        Me.INDTreeList.Size = New System.Drawing.Size(1896, 470)
        Me.INDTreeList.TabIndex = 4
        '
        'INDColRubro
        '
        Me.INDColRubro.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColRubro.AppearanceHeader.Options.UseFont = True
        Me.INDColRubro.Caption = "Rubro"
        Me.INDColRubro.FieldName = "NombreRubro"
        Me.INDColRubro.Name = "INDColRubro"
        Me.INDColRubro.OptionsColumn.AllowEdit = False
        Me.INDColRubro.OptionsColumn.FixedWidth = True
        Me.INDColRubro.Visible = True
        Me.INDColRubro.VisibleIndex = 0
        Me.INDColRubro.Width = 120
        '
        'INDColAccountingAccount
        '
        Me.INDColAccountingAccount.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColAccountingAccount.AppearanceHeader.Options.UseFont = True
        Me.INDColAccountingAccount.Caption = "C. Contable"
        Me.INDColAccountingAccount.FieldName = "NumeroCuentaContable"
        Me.INDColAccountingAccount.Name = "INDColAccountingAccount"
        Me.INDColAccountingAccount.OptionsColumn.AllowEdit = False
        Me.INDColAccountingAccount.OptionsColumn.FixedWidth = True
        Me.INDColAccountingAccount.Visible = True
        Me.INDColAccountingAccount.VisibleIndex = 1
        Me.INDColAccountingAccount.Width = 80
        '
        'INDColThirdParty
        '
        Me.INDColThirdParty.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColThirdParty.AppearanceHeader.Options.UseFont = True
        Me.INDColThirdParty.Caption = "Tercero"
        Me.INDColThirdParty.FieldName = "NombreTercero"
        Me.INDColThirdParty.Name = "INDColThirdParty"
        Me.INDColThirdParty.OptionsColumn.AllowEdit = False
        Me.INDColThirdParty.OptionsColumn.FixedWidth = True
        Me.INDColThirdParty.Visible = True
        Me.INDColThirdParty.VisibleIndex = 2
        Me.INDColThirdParty.Width = 80
        '
        'IndColCostCenter
        '
        Me.IndColCostCenter.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndColCostCenter.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndColCostCenter.AppearanceHeader.Options.UseFont = True
        Me.IndColCostCenter.Caption = "Centro Costo"
        Me.IndColCostCenter.FieldName = "NombreCentroCosto"
        Me.IndColCostCenter.Name = "IndColCostCenter"
        Me.IndColCostCenter.OptionsColumn.AllowEdit = False
        Me.IndColCostCenter.OptionsColumn.FixedWidth = True
        Me.IndColCostCenter.Visible = True
        Me.IndColCostCenter.VisibleIndex = 3
        Me.IndColCostCenter.Width = 80
        '
        'INDColEnero
        '
        Me.INDColEnero.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColEnero.AppearanceHeader.Options.UseFont = True
        Me.INDColEnero.Caption = "Enero"
        Me.INDColEnero.ColumnEdit = Me.INDrepValueEnero
        Me.INDColEnero.FieldName = "Enero"
        Me.INDColEnero.Format.FormatString = "c0"
        Me.INDColEnero.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColEnero.Name = "INDColEnero"
        Me.INDColEnero.OptionsColumn.FixedWidth = True
        Me.INDColEnero.Visible = True
        Me.INDColEnero.VisibleIndex = 4
        Me.INDColEnero.Width = 160
        '
        'INDrepValueEnero
        '
        Me.INDrepValueEnero.AutoHeight = False
        Me.INDrepValueEnero.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueEnero.Mask.EditMask = "c0"
        Me.INDrepValueEnero.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueEnero.Name = "INDrepValueEnero"
        '
        'IndcolFebrero
        '
        Me.IndcolFebrero.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndcolFebrero.AppearanceHeader.Options.UseFont = True
        Me.IndcolFebrero.Caption = "Febrero"
        Me.IndcolFebrero.ColumnEdit = Me.INDrepValueFebrero
        Me.IndcolFebrero.FieldName = "Febrero"
        Me.IndcolFebrero.Format.FormatString = "c0"
        Me.IndcolFebrero.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IndcolFebrero.Name = "IndcolFebrero"
        Me.IndcolFebrero.OptionsColumn.FixedWidth = True
        Me.IndcolFebrero.Visible = True
        Me.IndcolFebrero.VisibleIndex = 5
        Me.IndcolFebrero.Width = 160
        '
        'INDrepValueFebrero
        '
        Me.INDrepValueFebrero.AutoHeight = False
        Me.INDrepValueFebrero.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueFebrero.Mask.EditMask = "c0"
        Me.INDrepValueFebrero.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueFebrero.Name = "INDrepValueFebrero"
        '
        'INDColMarzo
        '
        Me.INDColMarzo.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColMarzo.AppearanceHeader.Options.UseFont = True
        Me.INDColMarzo.Caption = "Marzo"
        Me.INDColMarzo.ColumnEdit = Me.INDrepValueMarzo
        Me.INDColMarzo.FieldName = "Marzo"
        Me.INDColMarzo.Format.FormatString = "c0"
        Me.INDColMarzo.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColMarzo.Name = "INDColMarzo"
        Me.INDColMarzo.OptionsColumn.FixedWidth = True
        Me.INDColMarzo.Visible = True
        Me.INDColMarzo.VisibleIndex = 6
        Me.INDColMarzo.Width = 160
        '
        'INDrepValueMarzo
        '
        Me.INDrepValueMarzo.AutoHeight = False
        Me.INDrepValueMarzo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueMarzo.Mask.EditMask = "c0"
        Me.INDrepValueMarzo.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueMarzo.Name = "INDrepValueMarzo"
        '
        'INDColAbril
        '
        Me.INDColAbril.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColAbril.AppearanceHeader.Options.UseFont = True
        Me.INDColAbril.Caption = "Abril"
        Me.INDColAbril.ColumnEdit = Me.INDrepValueAbril
        Me.INDColAbril.FieldName = "Abril"
        Me.INDColAbril.Format.FormatString = "c0"
        Me.INDColAbril.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColAbril.Name = "INDColAbril"
        Me.INDColAbril.OptionsColumn.FixedWidth = True
        Me.INDColAbril.Visible = True
        Me.INDColAbril.VisibleIndex = 7
        Me.INDColAbril.Width = 160
        '
        'INDrepValueAbril
        '
        Me.INDrepValueAbril.AutoHeight = False
        Me.INDrepValueAbril.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueAbril.Mask.EditMask = "c0"
        Me.INDrepValueAbril.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueAbril.Name = "INDrepValueAbril"
        '
        'INDColMayo
        '
        Me.INDColMayo.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColMayo.AppearanceHeader.Options.UseFont = True
        Me.INDColMayo.Caption = "Mayo"
        Me.INDColMayo.ColumnEdit = Me.INDrepValueJunio
        Me.INDColMayo.FieldName = "Mayo"
        Me.INDColMayo.Format.FormatString = "c0"
        Me.INDColMayo.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColMayo.Name = "INDColMayo"
        Me.INDColMayo.OptionsColumn.FixedWidth = True
        Me.INDColMayo.Visible = True
        Me.INDColMayo.VisibleIndex = 8
        Me.INDColMayo.Width = 160
        '
        'INDrepValueJunio
        '
        Me.INDrepValueJunio.AutoHeight = False
        Me.INDrepValueJunio.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueJunio.Mask.EditMask = "c0"
        Me.INDrepValueJunio.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueJunio.Name = "INDrepValueJunio"
        '
        'INDColJunio
        '
        Me.INDColJunio.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColJunio.AppearanceHeader.Options.UseFont = True
        Me.INDColJunio.Caption = "Junio"
        Me.INDColJunio.ColumnEdit = Me.INDrepValueJunio
        Me.INDColJunio.FieldName = "Junio"
        Me.INDColJunio.Format.FormatString = "c0"
        Me.INDColJunio.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColJunio.Name = "INDColJunio"
        Me.INDColJunio.OptionsColumn.FixedWidth = True
        Me.INDColJunio.Visible = True
        Me.INDColJunio.VisibleIndex = 9
        Me.INDColJunio.Width = 160
        '
        'INDColJulio
        '
        Me.INDColJulio.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColJulio.AppearanceHeader.Options.UseFont = True
        Me.INDColJulio.Caption = "Julio"
        Me.INDColJulio.ColumnEdit = Me.INDrepValueJulio
        Me.INDColJulio.FieldName = "Julio"
        Me.INDColJulio.Format.FormatString = "c0"
        Me.INDColJulio.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColJulio.Name = "INDColJulio"
        Me.INDColJulio.OptionsColumn.FixedWidth = True
        Me.INDColJulio.Visible = True
        Me.INDColJulio.VisibleIndex = 10
        Me.INDColJulio.Width = 160
        '
        'INDrepValueJulio
        '
        Me.INDrepValueJulio.AutoHeight = False
        Me.INDrepValueJulio.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueJulio.Mask.EditMask = "c0"
        Me.INDrepValueJulio.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueJulio.Name = "INDrepValueJulio"
        '
        'INDColAgosto
        '
        Me.INDColAgosto.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColAgosto.AppearanceHeader.Options.UseFont = True
        Me.INDColAgosto.Caption = "Agosto"
        Me.INDColAgosto.ColumnEdit = Me.INDrepValueAgosto
        Me.INDColAgosto.FieldName = "Agosto"
        Me.INDColAgosto.Format.FormatString = "c0"
        Me.INDColAgosto.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColAgosto.Name = "INDColAgosto"
        Me.INDColAgosto.OptionsColumn.FixedWidth = True
        Me.INDColAgosto.Visible = True
        Me.INDColAgosto.VisibleIndex = 11
        Me.INDColAgosto.Width = 160
        '
        'INDrepValueAgosto
        '
        Me.INDrepValueAgosto.AutoHeight = False
        Me.INDrepValueAgosto.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueAgosto.Mask.EditMask = "c0"
        Me.INDrepValueAgosto.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueAgosto.Name = "INDrepValueAgosto"
        '
        'IndColSeptiembre
        '
        Me.IndColSeptiembre.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndColSeptiembre.AppearanceHeader.Options.UseFont = True
        Me.IndColSeptiembre.Caption = "Septiembre"
        Me.IndColSeptiembre.ColumnEdit = Me.INDrepValueSeptiembre
        Me.IndColSeptiembre.FieldName = "Septiembre"
        Me.IndColSeptiembre.Format.FormatString = "c0"
        Me.IndColSeptiembre.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IndColSeptiembre.Name = "IndColSeptiembre"
        Me.IndColSeptiembre.OptionsColumn.FixedWidth = True
        Me.IndColSeptiembre.Visible = True
        Me.IndColSeptiembre.VisibleIndex = 12
        Me.IndColSeptiembre.Width = 160
        '
        'INDrepValueSeptiembre
        '
        Me.INDrepValueSeptiembre.AutoHeight = False
        Me.INDrepValueSeptiembre.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueSeptiembre.Mask.EditMask = "c0"
        Me.INDrepValueSeptiembre.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueSeptiembre.Name = "INDrepValueSeptiembre"
        '
        'IndColOctubre
        '
        Me.IndColOctubre.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndColOctubre.AppearanceHeader.Options.UseFont = True
        Me.IndColOctubre.Caption = "Octubre"
        Me.IndColOctubre.ColumnEdit = Me.INDrepValueOctubre
        Me.IndColOctubre.FieldName = "Octubre"
        Me.IndColOctubre.Format.FormatString = "c0"
        Me.IndColOctubre.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IndColOctubre.Name = "IndColOctubre"
        Me.IndColOctubre.OptionsColumn.FixedWidth = True
        Me.IndColOctubre.Visible = True
        Me.IndColOctubre.VisibleIndex = 13
        Me.IndColOctubre.Width = 160
        '
        'INDrepValueOctubre
        '
        Me.INDrepValueOctubre.AutoHeight = False
        Me.INDrepValueOctubre.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueOctubre.Mask.EditMask = "c0"
        Me.INDrepValueOctubre.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueOctubre.Name = "INDrepValueOctubre"
        '
        'INDColNoviembre
        '
        Me.INDColNoviembre.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColNoviembre.AppearanceHeader.Options.UseFont = True
        Me.INDColNoviembre.Caption = "Noviembre"
        Me.INDColNoviembre.ColumnEdit = Me.INDrepValueNoviembre
        Me.INDColNoviembre.FieldName = "Noviembre"
        Me.INDColNoviembre.Format.FormatString = "c0"
        Me.INDColNoviembre.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColNoviembre.Name = "INDColNoviembre"
        Me.INDColNoviembre.OptionsColumn.FixedWidth = True
        Me.INDColNoviembre.Visible = True
        Me.INDColNoviembre.VisibleIndex = 14
        Me.INDColNoviembre.Width = 160
        '
        'INDrepValueNoviembre
        '
        Me.INDrepValueNoviembre.AutoHeight = False
        Me.INDrepValueNoviembre.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueNoviembre.Mask.EditMask = "c0"
        Me.INDrepValueNoviembre.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueNoviembre.Name = "INDrepValueNoviembre"
        '
        'INDColDiciembre
        '
        Me.INDColDiciembre.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDColDiciembre.AppearanceHeader.Options.UseFont = True
        Me.INDColDiciembre.Caption = "Diciembre"
        Me.INDColDiciembre.ColumnEdit = Me.INDrepValueDiciembre
        Me.INDColDiciembre.FieldName = "Diciembre"
        Me.INDColDiciembre.Format.FormatString = "c0"
        Me.INDColDiciembre.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDiciembre.Name = "INDColDiciembre"
        Me.INDColDiciembre.OptionsColumn.FixedWidth = True
        Me.INDColDiciembre.Visible = True
        Me.INDColDiciembre.VisibleIndex = 15
        Me.INDColDiciembre.Width = 160
        '
        'INDrepValueDiciembre
        '
        Me.INDrepValueDiciembre.AutoHeight = False
        Me.INDrepValueDiciembre.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueDiciembre.Mask.EditMask = "c0"
        Me.INDrepValueDiciembre.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueDiciembre.Name = "INDrepValueDiciembre"
        '
        'INDrepValueMayo
        '
        Me.INDrepValueMayo.AutoHeight = False
        Me.INDrepValueMayo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValueMayo.Mask.EditMask = "c0"
        Me.INDrepValueMayo.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValueMayo.Name = "INDrepValueMayo"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(-683, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1944, 553)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1924, 533)
        Me.LayoutControlGroup2.Text = "Presupuesto Privado"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDTreeList
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(1900, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(1900, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1900, 474)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmPrivateBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmPrivateBudget"
        Me.Opacity = 1.0R
        Me.Tag = "1981"
        Me.Text = "FrmPrivateBudget"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDTreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueEnero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueFebrero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueMarzo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueAbril, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueJunio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueJulio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueAgosto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueSeptiembre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueOctubre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueNoviembre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueDiciembre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValueMayo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDTreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColRubro As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As IndigoTreeList
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDColAccountingAccount As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColThirdParty As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndColCostCenter As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColEnero As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndcolFebrero As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColMarzo As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColAbril As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColMayo As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColJunio As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColJulio As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColAgosto As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndColSeptiembre As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndColOctubre As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColNoviembre As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColDiciembre As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDrepValueEnero As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueFebrero As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueMarzo As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueAbril As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueJunio As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueJulio As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueAgosto As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueSeptiembre As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueOctubre As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueNoviembre As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueDiciembre As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDrepValueMayo As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
End Class
