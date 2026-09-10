<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrMenuForm
    Inherits System.Windows.Forms.UserControl


    'UserControl overrides dispose to clean up the component list.
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
        Dim TileViewItemElement1 As DevExpress.XtraGrid.Views.Tile.TileViewItemElement = New DevExpress.XtraGrid.Views.Tile.TileViewItemElement()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrMenuForm))
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.TileViewColumn()
        Me.INDcolumnType = New DevExpress.XtraGrid.Columns.TileViewColumn()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.TileView1 = New DevExpress.XtraGrid.Views.Tile.TileView()
        Me.INDColOrder = New DevExpress.XtraGrid.Columns.TileViewColumn()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.MnuAddToFavorites = New DevExpress.XtraBars.BarButtonItem()
        Me.MnuDeleteFromFavorites = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TileView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Formulario"
        Me.GridColumn1.FieldName = "FormName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'INDcolumnType
        '
        Me.INDcolumnType.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDcolumnType.AppearanceHeader.Options.UseFont = True
        Me.INDcolumnType.Caption = "Tipo"
        Me.INDcolumnType.FieldName = "Title.TitleName"
        Me.INDcolumnType.Name = "INDcolumnType"
        Me.INDcolumnType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDcolumnType.OptionsEditForm.Caption = "Tipo:"
        Me.INDcolumnType.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolumnType.Visible = True
        Me.INDcolumnType.VisibleIndex = 0
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.TileView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(664, 550)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.TileView1})
        '
        'TileView1
        '
        Me.TileView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolumnType, Me.GridColumn1, Me.INDColOrder})
        Me.TileView1.ColumnSet.GroupColumn = Me.INDcolumnType
        Me.TileView1.GridControl = Me.GridControl1
        Me.TileView1.Images = Me.ImageCollection1
        Me.TileView1.Name = "TileView1"
        Me.TileView1.OptionsFind.AlwaysVisible = True
        Me.TileView1.OptionsFind.ShowClearButton = False
        Me.TileView1.OptionsFind.ShowFindButton = False
        Me.TileView1.OptionsTiles.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.TileView1.OptionsTiles.ItemSize = New System.Drawing.Size(200, 50)
        Me.TileView1.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TileView1.OptionsTiles.RowCount = 3
        Me.TileView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDcolumnType, DevExpress.Data.ColumnSortOrder.Ascending)})
        TileViewItemElement1.Appearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TileViewItemElement1.Appearance.Normal.Options.UseFont = True
        TileViewItemElement1.Column = Me.GridColumn1
        TileViewItemElement1.Text = "GridColumn1"
        Me.TileView1.TileTemplate.Add(TileViewItemElement1)
        '
        'INDColOrder
        '
        Me.INDColOrder.Caption = "Orden"
        Me.INDColOrder.FieldName = "FormOrder"
        Me.INDColOrder.Name = "INDColOrder"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "ProcessSmallGA.png")
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MnuAddToFavorites), New DevExpress.XtraBars.LinkPersistInfo(Me.MnuDeleteFromFavorites)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'MnuAddToFavorites
        '
        Me.MnuAddToFavorites.Caption = "Agregar a Mis Favoritos"
        Me.MnuAddToFavorites.Id = 2
        Me.MnuAddToFavorites.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MnuAddToFavorites.ItemAppearance.Disabled.Options.UseFont = True
        Me.MnuAddToFavorites.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemAppearance.Hovered.Options.UseFont = True
        Me.MnuAddToFavorites.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemAppearance.Normal.Options.UseFont = True
        Me.MnuAddToFavorites.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemAppearance.Pressed.Options.UseFont = True
        Me.MnuAddToFavorites.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MnuAddToFavorites.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MnuAddToFavorites.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MnuAddToFavorites.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuAddToFavorites.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MnuAddToFavorites.Name = "MnuAddToFavorites"
        '
        'MnuDeleteFromFavorites
        '
        Me.MnuDeleteFromFavorites.Caption = "Quitar de Mis Favoritos"
        Me.MnuDeleteFromFavorites.Id = 3
        Me.MnuDeleteFromFavorites.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemAppearance.Disabled.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemAppearance.Hovered.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemAppearance.Normal.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemAppearance.Pressed.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!)
        Me.MnuDeleteFromFavorites.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MnuDeleteFromFavorites.Name = "MnuDeleteFromFavorites"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MnuAddToFavorites, Me.MnuDeleteFromFavorites})
        Me.BarManager1.MaxItemId = 4
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(664, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 550)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(664, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 550)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(664, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 550)
        '
        'CtrMenuForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "CtrMenuForm"
        Me.Size = New System.Drawing.Size(664, 550)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TileView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents TileView1 As DevExpress.XtraGrid.Views.Tile.TileView
    Friend WithEvents INDcolumnType As DevExpress.XtraGrid.Columns.TileViewColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.TileViewColumn
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents MnuAddToFavorites As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MnuDeleteFromFavorites As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDColOrder As DevExpress.XtraGrid.Columns.TileViewColumn
End Class
