<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpShowRecognitionDetail
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopUpShowRecognitionDetail))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcRecognitionDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRecognitionDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColVUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiExpand = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiCollapse = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcRecognitionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRecognitionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcRecognitionDetails)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(842, 549)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcRecognitionDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRecognitionDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRecognitionDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRecognitionDetails, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRecognitionDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRecognitionDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRecognitionDetails, False)
        Me.INDGcRecognitionDetails.Location = New System.Drawing.Point(12, 12)
        Me.INDGcRecognitionDetails.MainView = Me.INDGvRecognitionDetails
        Me.INDGcRecognitionDetails.Name = "INDGcRecognitionDetails"
        Me.INDGcRecognitionDetails.Size = New System.Drawing.Size(818, 525)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRecognitionDetails, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRecognitionDetails.TabIndex = 7
        Me.INDGcRecognitionDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRecognitionDetails})
        '
        'INDGvRecognitionDetails
        '
        Me.INDGvRecognitionDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRecognitionDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRecognitionDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRecognitionDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRecognitionDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRecognitionDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRecognitionDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRecognitionDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRecognitionDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRecognitionDetails.Appearance.Row.Options.UseFont = True
        Me.INDGvRecognitionDetails.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRecognitionDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRecognitionDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCode, Me.ColDescription, Me.ColDate, Me.ColQuantity, Me.ColVUnit, Me.ColTotal, Me.GridColumn1})
        Me.INDGvRecognitionDetails.GridControl = Me.INDGcRecognitionDetails
        Me.INDGvRecognitionDetails.GroupCount = 1
        Me.INDGvRecognitionDetails.Name = "INDGvRecognitionDetails"
        Me.INDGvRecognitionDetails.OptionsFind.AlwaysVisible = True
        Me.INDGvRecognitionDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRecognitionDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRecognitionDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRecognitionDetails.OptionsView.ShowFooter = True
        Me.INDGvRecognitionDetails.OptionsView.ShowGroupPanel = False
        Me.INDGvRecognitionDetails.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRecognitionDetails, False)
        '
        'ColCode
        '
        Me.ColCode.Caption = "Código"
        Me.ColCode.FieldName = "ServiceCode"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.OptionsColumn.AllowEdit = False
        Me.ColCode.OptionsColumn.AllowFocus = False
        Me.ColCode.Visible = True
        Me.ColCode.VisibleIndex = 0
        Me.ColCode.Width = 102
        '
        'ColDescription
        '
        Me.ColDescription.Caption = "Descripción"
        Me.ColDescription.FieldName = "ServiceName"
        Me.ColDescription.Name = "ColDescription"
        Me.ColDescription.OptionsColumn.AllowEdit = False
        Me.ColDescription.OptionsColumn.AllowFocus = False
        Me.ColDescription.Visible = True
        Me.ColDescription.VisibleIndex = 1
        Me.ColDescription.Width = 280
        '
        'ColDate
        '
        Me.ColDate.Caption = "Fecha"
        Me.ColDate.FieldName = "ServiceDate"
        Me.ColDate.Name = "ColDate"
        Me.ColDate.OptionsColumn.AllowEdit = False
        Me.ColDate.OptionsColumn.AllowFocus = False
        Me.ColDate.Visible = True
        Me.ColDate.VisibleIndex = 2
        Me.ColDate.Width = 103
        '
        'ColQuantity
        '
        Me.ColQuantity.Caption = "Cantidad"
        Me.ColQuantity.FieldName = "InvoicedQuantity"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.OptionsColumn.AllowEdit = False
        Me.ColQuantity.OptionsColumn.AllowFocus = False
        Me.ColQuantity.Visible = True
        Me.ColQuantity.VisibleIndex = 3
        Me.ColQuantity.Width = 79
        '
        'ColVUnit
        '
        Me.ColVUnit.Caption = "V. Unitario"
        Me.ColVUnit.DisplayFormat.FormatString = "C0"
        Me.ColVUnit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColVUnit.FieldName = "TotalSalesPrice"
        Me.ColVUnit.Name = "ColVUnit"
        Me.ColVUnit.OptionsColumn.AllowEdit = False
        Me.ColVUnit.OptionsColumn.AllowFocus = False
        Me.ColVUnit.Visible = True
        Me.ColVUnit.VisibleIndex = 4
        Me.ColVUnit.Width = 98
        '
        'ColTotal
        '
        Me.ColTotal.Caption = "Total"
        Me.ColTotal.DisplayFormat.FormatString = "C0"
        Me.ColTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColTotal.FieldName = "GrandTotalSalesPrice"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.OptionsColumn.AllowEdit = False
        Me.ColTotal.OptionsColumn.AllowFocus = False
        Me.ColTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GrandTotalSalesPrice", "Total {0:C0}")})
        Me.ColTotal.Visible = True
        Me.ColTotal.VisibleIndex = 5
        Me.ColTotal.Width = 138
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Ingreso"
        Me.GridColumn1.FieldName = "AdmissionNumberPatient"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 6
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(842, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDGcRecognitionDetails
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(822, 529)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiExpand, Me.INDBbiCollapse})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(842, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 549)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(842, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 549)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(842, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 549)
        '
        'INDBbiExpand
        '
        Me.INDBbiExpand.Caption = "Expandir Todo"
        Me.INDBbiExpand.Id = 0
        Me.INDBbiExpand.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBbiExpand.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiExpand.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiExpand.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiExpand.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiExpand.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiExpand.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiExpand.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiExpand.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.INDBbiExpand.Name = "INDBbiExpand"
        '
        'INDBbiCollapse
        '
        Me.INDBbiCollapse.Caption = "Contraer Todo"
        Me.INDBbiCollapse.Id = 1
        Me.INDBbiCollapse.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiCollapse.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiCollapse.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiCollapse.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiCollapse.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiCollapse.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiCollapse.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDBbiCollapse.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiCollapse.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.INDBbiCollapse.Name = "INDBbiCollapse"
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiExpand), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiCollapse)})
        Me.PopupMenuActions.Manager = Me.BarManager1
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'FrmPopUpShowRecognitionDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 549)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpShowRecognitionDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "750"
        Me.Text = "Detalle de Reconocimiento de Ingreso"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcRecognitionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRecognitionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcRecognitionDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRecognitionDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents ColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColVUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiExpand As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiCollapse As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
End Class
