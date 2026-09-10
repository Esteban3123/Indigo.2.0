<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrNavigationRecord
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrNavigationRecord))
        Me.INDBtnFirst = New DevExpress.XtraEditors.SimpleButton()
        Me.INDImagenesMetroLarges = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDBtnBack = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDdbDetails = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupControlContainer1 = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDGcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDBtnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnLast = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDImagenesMetroLarges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupControlContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupControlContainer1.SuspendLayout()
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDBtnFirst
        '
        Me.INDBtnFirst.Appearance.BackColor = System.Drawing.Color.White
        Me.INDBtnFirst.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDBtnFirst.Appearance.Options.UseBackColor = True
        Me.INDBtnFirst.Appearance.Options.UseBorderColor = True
        Me.INDBtnFirst.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDBtnFirst.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDBtnFirst.ImageIndex = 2
        Me.INDBtnFirst.ImageList = Me.INDImagenesMetroLarges
        Me.INDBtnFirst.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnFirst.Location = New System.Drawing.Point(0, 0)
        Me.INDBtnFirst.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDBtnFirst.Name = "INDBtnFirst"
        Me.INDBtnFirst.Size = New System.Drawing.Size(40, 563)
        Me.INDBtnFirst.TabIndex = 0
        '
        'INDImagenesMetroLarges
        '
        Me.INDImagenesMetroLarges.ImageSize = New System.Drawing.Size(24, 24)
        Me.INDImagenesMetroLarges.ImageStream = CType(resources.GetObject("INDImagenesMetroLarges.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDImagenesMetroLarges.Images.SetKeyName(0, "1.png")
        Me.INDImagenesMetroLarges.Images.SetKeyName(1, "3.png")
        Me.INDImagenesMetroLarges.Images.SetKeyName(2, "10.png")
        Me.INDImagenesMetroLarges.Images.SetKeyName(3, "12.png")
        Me.INDImagenesMetroLarges.Images.SetKeyName(4, "14.png")
        '
        'INDBtnBack
        '
        Me.INDBtnBack.Appearance.BackColor = System.Drawing.Color.White
        Me.INDBtnBack.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDBtnBack.Appearance.Options.UseBackColor = True
        Me.INDBtnBack.Appearance.Options.UseBorderColor = True
        Me.INDBtnBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDBtnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDBtnBack.ImageIndex = 1
        Me.INDBtnBack.ImageList = Me.INDImagenesMetroLarges
        Me.INDBtnBack.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnBack.Location = New System.Drawing.Point(40, 0)
        Me.INDBtnBack.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDBtnBack.Name = "INDBtnBack"
        Me.INDBtnBack.Size = New System.Drawing.Size(40, 563)
        Me.INDBtnBack.TabIndex = 1
        '
        'INDDdbDetails
        '
        Me.INDDdbDetails.Appearance.BackColor = System.Drawing.Color.White
        Me.INDDdbDetails.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDDdbDetails.Appearance.Options.UseBackColor = True
        Me.INDDdbDetails.Appearance.Options.UseBorderColor = True
        Me.INDDdbDetails.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDDdbDetails.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDDdbDetails.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show
        Me.INDDdbDetails.DropDownControl = Me.PopupControlContainer1
        Me.INDDdbDetails.ImageIndex = 0
        Me.INDDdbDetails.ImageList = Me.INDImagenesMetroLarges
        Me.INDDdbDetails.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDDdbDetails.Location = New System.Drawing.Point(80, 0)
        Me.INDDdbDetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDDdbDetails.Name = "INDDdbDetails"
        Me.INDDdbDetails.Size = New System.Drawing.Size(70, 563)
        Me.INDDdbDetails.TabIndex = 2
        '
        'PopupControlContainer1
        '
        Me.PopupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupControlContainer1.Controls.Add(Me.INDGcDetail)
        Me.PopupControlContainer1.Location = New System.Drawing.Point(514, 244)
        Me.PopupControlContainer1.Manager = Me.BarManager1
        Me.PopupControlContainer1.Name = "PopupControlContainer1"
        Me.PopupControlContainer1.Size = New System.Drawing.Size(531, 223)
        Me.PopupControlContainer1.TabIndex = 5
        Me.PopupControlContainer1.Visible = False
        '
        'INDGcDetail
        '
        Me.INDGcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDGcDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDGcDetail.MainView = Me.INDGvDetail
        Me.INDGcDetail.MenuManager = Me.BarManager1
        Me.INDGcDetail.Name = "INDGcDetail"
        Me.INDGcDetail.Size = New System.Drawing.Size(531, 223)
        Me.INDGcDetail.TabIndex = 0
        Me.INDGcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDetail})
        '
        'INDGvDetail
        '
        Me.INDGvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvDetail.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvDetail.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvDetail.GridControl = Me.INDGcDetail
        Me.INDGvDetail.Name = "INDGvDetail"
        Me.INDGvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetail.OptionsView.ShowDetailButtons = False
        Me.INDGvDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetail, False)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(232, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 563)
        Me.barDockControlBottom.Size = New System.Drawing.Size(232, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 563)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(232, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 563)
        '
        'INDBtnNext
        '
        Me.INDBtnNext.Appearance.BackColor = System.Drawing.Color.White
        Me.INDBtnNext.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDBtnNext.Appearance.Options.UseBackColor = True
        Me.INDBtnNext.Appearance.Options.UseBorderColor = True
        Me.INDBtnNext.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDBtnNext.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDBtnNext.ImageIndex = 4
        Me.INDBtnNext.ImageList = Me.INDImagenesMetroLarges
        Me.INDBtnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnNext.Location = New System.Drawing.Point(150, 0)
        Me.INDBtnNext.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDBtnNext.Name = "INDBtnNext"
        Me.INDBtnNext.Size = New System.Drawing.Size(40, 563)
        Me.INDBtnNext.TabIndex = 3
        '
        'INDBtnLast
        '
        Me.INDBtnLast.Appearance.BackColor = System.Drawing.Color.White
        Me.INDBtnLast.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDBtnLast.Appearance.Options.UseBackColor = True
        Me.INDBtnLast.Appearance.Options.UseBorderColor = True
        Me.INDBtnLast.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDBtnLast.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDBtnLast.ImageIndex = 3
        Me.INDBtnLast.ImageList = Me.INDImagenesMetroLarges
        Me.INDBtnLast.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnLast.Location = New System.Drawing.Point(190, 0)
        Me.INDBtnLast.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDBtnLast.Name = "INDBtnLast"
        Me.INDBtnLast.Size = New System.Drawing.Size(40, 563)
        Me.INDBtnLast.TabIndex = 4
        '
        'CtrNavigationRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PopupControlContainer1)
        Me.Controls.Add(Me.INDBtnLast)
        Me.Controls.Add(Me.INDBtnNext)
        Me.Controls.Add(Me.INDDdbDetails)
        Me.Controls.Add(Me.INDBtnBack)
        Me.Controls.Add(Me.INDBtnFirst)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "CtrNavigationRecord"
        Me.Size = New System.Drawing.Size(232, 563)
        CType(Me.INDImagenesMetroLarges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupControlContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupControlContainer1.ResumeLayout(False)
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDBtnFirst As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDDdbDetails As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDBtnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnLast As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDImagenesMetroLarges As DevExpress.Utils.ImageCollection
    Friend WithEvents PopupControlContainer1 As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDGcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl

End Class
