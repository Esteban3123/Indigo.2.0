Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmHighEquipmentTechnique
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
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcFixedAssetPhysicalAsset = New DevExpress.XtraGrid.GridControl()
        Me.INDGvFixedAssetPhysicalAsset = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDGcFixedAssetPhysicalAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFixedAssetPhysicalAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcMainData)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1284, 495)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1284, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1284, 98)
        '
        'INDLcMainData
        '
        Me.INDLcMainData.AllowCustomization = False
        Me.INDLcMainData.Controls.Add(Me.INDGcFixedAssetPhysicalAsset)
        Me.INDLcMainData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData, False)
        Me.INDLcMainData.Location = New System.Drawing.Point(2, 7)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.Root = Me.LayoutControlGroup2
        Me.INDLcMainData.Size = New System.Drawing.Size(1280, 486)
        Me.INDLcMainData.TabIndex = 11
        Me.INDLcMainData.Text = "LayoutControl2"
        '
        'INDGcFixedAssetPhysicalAsset
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcFixedAssetPhysicalAsset, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcFixedAssetPhysicalAsset, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcFixedAssetPhysicalAsset, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcFixedAssetPhysicalAsset, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcFixedAssetPhysicalAsset, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcFixedAssetPhysicalAsset, False)
        Me.INDGcFixedAssetPhysicalAsset.Location = New System.Drawing.Point(12, 12)
        Me.INDGcFixedAssetPhysicalAsset.MainView = Me.INDGvFixedAssetPhysicalAsset
        Me.INDGcFixedAssetPhysicalAsset.Name = "INDGcFixedAssetPhysicalAsset"
        Me.INDGcFixedAssetPhysicalAsset.Size = New System.Drawing.Size(1256, 462)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcFixedAssetPhysicalAsset, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcFixedAssetPhysicalAsset.TabIndex = 4
        Me.INDGcFixedAssetPhysicalAsset.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvFixedAssetPhysicalAsset})
        '
        'INDGvFixedAssetPhysicalAsset
        '
        Me.INDGvFixedAssetPhysicalAsset.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFixedAssetPhysicalAsset.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFixedAssetPhysicalAsset.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFixedAssetPhysicalAsset.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFixedAssetPhysicalAsset.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFixedAssetPhysicalAsset.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFixedAssetPhysicalAsset.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFixedAssetPhysicalAsset.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFixedAssetPhysicalAsset.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFixedAssetPhysicalAsset.Appearance.Row.Options.UseFont = True
        Me.INDGvFixedAssetPhysicalAsset.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvFixedAssetPhysicalAsset.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvFixedAssetPhysicalAsset.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.INDGvFixedAssetPhysicalAsset.GridControl = Me.INDGcFixedAssetPhysicalAsset
        Me.INDGvFixedAssetPhysicalAsset.Name = "INDGvFixedAssetPhysicalAsset"
        Me.INDGvFixedAssetPhysicalAsset.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFixedAssetPhysicalAsset.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFixedAssetPhysicalAsset.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFixedAssetPhysicalAsset.OptionsView.ShowFooter = True
        Me.INDGvFixedAssetPhysicalAsset.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFixedAssetPhysicalAsset, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Placa"
        Me.GridColumn1.FieldName = "Plate"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 176
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Artículo"
        Me.GridColumn2.FieldName = "ItemId.CodeDescription"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 362
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Marca"
        Me.GridColumn3.FieldName = "TrademarkId.CodeDescription"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 167
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Modelo"
        Me.GridColumn4.FieldName = "Model"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 138
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Serie"
        Me.GridColumn5.FieldName = "Serie"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 138
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Ubicación"
        Me.GridColumn6.FieldName = "LocationId.CodeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 138
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Responsable"
        Me.GridColumn7.FieldName = "ResponsibleId.CodeNitName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 148
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1280, 486)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcFixedAssetPhysicalAsset
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1260, 466)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmHighEquipmentTechnique
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 617)
        Me.Name = "FrmHighEquipmentTechnique"
        Me.Opacity = 1.0R
        Me.Tag = "2027"
        Me.Text = "WorkList Alta técnica de Activos Fijos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDGcFixedAssetPhysicalAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFixedAssetPhysicalAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcFixedAssetPhysicalAsset As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvFixedAssetPhysicalAsset As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
End Class
