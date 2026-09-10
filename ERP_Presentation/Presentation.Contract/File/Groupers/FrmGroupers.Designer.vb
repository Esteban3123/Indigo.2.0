Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGroupers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGroupers))
        Me.INDLyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAddNode = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTlGroupers = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList()
        Me.INDEsbLoadMassive = New Presentation.Controls.ExportStructureButton()
        Me.IndigoSimpleButton11 = New Presentation.Controls.IndigoSimpleButton()
        Me.INDLciEsbAccountPayable = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSimpleButton12 = New Presentation.Controls.IndigoSimpleButton()
        Me.INDlyItemImportFile = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyRoot.SuspendLayout()
        CType(Me.INDTlGroupers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEsbAccountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemImportFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(924, 353)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(924, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(924, 130)
        '
        'INDLyRoot
        '
        Me.INDLyRoot.Controls.Add(Me.INDSbAddNode)
        Me.INDLyRoot.Controls.Add(Me.INDTlGroupers)
        Me.INDLyRoot.Controls.Add(Me.INDEsbLoadMassive)
        Me.INDLyRoot.Controls.Add(Me.INDBtnImportFile)
        Me.INDLyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLyRoot.Name = "INDLyRoot"
        Me.INDLyRoot.Root = Me.LayoutControlGroup1
        Me.INDLyRoot.Size = New System.Drawing.Size(920, 344)
        Me.INDLyRoot.TabIndex = 0
        Me.INDLyRoot.Text = "LayoutControl1"
        '
        'INDSbAddNode
        '
        Me.INDSbAddNode.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddNode.Appearance.Options.UseFont = True
        Me.INDSbAddNode.Location = New System.Drawing.Point(12, 12)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbAddNode, False)
        Me.IndigoSimpleButton12.SetModernUiIndigo(Me.INDSbAddNode, False)
        Me.INDSbAddNode.Name = "INDSbAddNode"
        Me.INDSbAddNode.Size = New System.Drawing.Size(125, 34)
        Me.INDSbAddNode.StyleController = Me.INDLyRoot
        Me.INDSbAddNode.TabIndex = 5
        Me.INDSbAddNode.Text = "Agregar Nodo"
        '
        'INDTlGroupers
        '
        Me.INDTlGroupers.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTlGroupers.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDTlGroupers.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTlGroupers.Appearance.Row.Options.UseFont = True
        Me.INDTlGroupers.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1, Me.TreeListColumn2})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.INDTlGroupers, False)
        Me.INDTlGroupers.KeyFieldName = "Id"
        Me.INDTlGroupers.Location = New System.Drawing.Point(12, 54)
        Me.INDTlGroupers.Name = "INDTlGroupers"
        Me.INDTlGroupers.OptionsBehavior.PopulateServiceColumns = True
        Me.INDTlGroupers.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Matches
        Me.INDTlGroupers.OptionsView.EnableAppearanceEvenRow = True
        Me.INDTlGroupers.OptionsView.EnableAppearanceOddRow = True
        Me.INDTlGroupers.ParentFieldName = "ParentId"
        Me.INDTlGroupers.Size = New System.Drawing.Size(896, 278)
        Me.INDTlGroupers.TabIndex = 4
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Código"
        Me.TreeListColumn1.FieldName = "Code"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 139
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Descripción"
        Me.TreeListColumn2.FieldName = "Description"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowEdit = False
        Me.TreeListColumn2.OptionsColumn.AllowFocus = False
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 756
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.INDLciEsbAccountPayable, Me.INDlyItemImportFile})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(920, 344)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDTlGroupers
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 42)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(900, 282)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbAddNode
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(129, 38)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(129, 38)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(129, 42)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(129, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(679, 42)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDEsbLoadMassive
        '
        Me.INDEsbLoadMassive.ImageOptions.Image = CType(resources.GetObject("ExportStructureButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbLoadMassive.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbLoadMassive.Location = New System.Drawing.Point(820, 12)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDEsbLoadMassive, False)
        Me.IndigoSimpleButton12.SetModernUiIndigo(Me.INDEsbLoadMassive, False)
        Me.INDEsbLoadMassive.Name = "INDEsbLoadMassive"
        Me.INDEsbLoadMassive.Size = New System.Drawing.Size(42, 38)
        Me.INDEsbLoadMassive.StyleController = Me.INDLyRoot
        Me.INDEsbLoadMassive.TabIndex = 11
        Me.INDEsbLoadMassive.Text = "SimpleButton1"
        '
        'INDLciEsbAccountPayable
        '
        Me.INDLciEsbAccountPayable.Control = Me.INDEsbLoadMassive
        Me.INDLciEsbAccountPayable.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciEsbAccountPayable.CustomizationFormText = "INDLciEsbAccountPayable"
        Me.INDLciEsbAccountPayable.Location = New System.Drawing.Point(808, 0)
        Me.INDLciEsbAccountPayable.MaxSize = New System.Drawing.Size(46, 42)
        Me.INDLciEsbAccountPayable.MinSize = New System.Drawing.Size(46, 42)
        Me.INDLciEsbAccountPayable.Name = "INDLciEsbAccountPayable"
        Me.INDLciEsbAccountPayable.Size = New System.Drawing.Size(46, 42)
        Me.INDLciEsbAccountPayable.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEsbAccountPayable.Text = "INDLciEsbAccountPayable"
        Me.INDLciEsbAccountPayable.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciEsbAccountPayable.TextVisible = False
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("SimpleButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(866, 12)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.IndigoSimpleButton12.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(42, 38)
        Me.INDBtnImportFile.StyleController = Me.INDLyRoot
        Me.INDBtnImportFile.TabIndex = 13
        Me.INDBtnImportFile.Text = "SimpleButton1"
        '
        'INDlyItemImportFile
        '
        Me.INDlyItemImportFile.Control = Me.INDBtnImportFile
        Me.INDlyItemImportFile.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemImportFile.CustomizationFormText = "INDlyItemImportFile"
        Me.INDlyItemImportFile.Location = New System.Drawing.Point(854, 0)
        Me.INDlyItemImportFile.MaxSize = New System.Drawing.Size(46, 42)
        Me.INDlyItemImportFile.MinSize = New System.Drawing.Size(46, 42)
        Me.INDlyItemImportFile.Name = "INDlyItemImportFile"
        Me.INDlyItemImportFile.Size = New System.Drawing.Size(46, 42)
        Me.INDlyItemImportFile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemImportFile.Text = "INDlyItemImportFile"
        Me.INDlyItemImportFile.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemImportFile.TextVisible = False
        '
        'FrmGroupers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 488)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmGroupers"
        Me.Opacity = 1.0R
        Me.Tag = "1958"
        Me.Text = "Agrupadores"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyRoot.ResumeLayout(False)
        CType(Me.INDTlGroupers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEsbAccountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemImportFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTlGroupers As DevExpress.XtraTreeList.TreeList
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTreeList1 As IndigoTreeList
    Friend WithEvents INDSbAddNode As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoSimpleButton11 As IndigoSimpleButton
    Friend WithEvents INDEsbLoadMassive As ExportStructureButton
    Friend WithEvents INDLciEsbAccountPayable As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton12 As IndigoSimpleButton
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemImportFile As DevExpress.XtraLayout.LayoutControlItem
End Class
