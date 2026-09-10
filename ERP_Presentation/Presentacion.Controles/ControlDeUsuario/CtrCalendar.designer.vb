<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCalendar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrCalendar))
        Me.INDPcMain = New DevExpress.XtraEditors.PanelControl()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbarButtonAddTurn = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonAddNovelty = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonAddEvent = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDPcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPcMain
        '
        Me.INDPcMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.INDPcMain, "INDPcMain")
        Me.INDPcMain.Name = "INDPcMain"
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbarButtonAddTurn, Me.INDbarButtonAddNovelty, Me.INDbarButtonAddEvent})
        Me.BarManager.MaxItemId = 9
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager
        '
        'INDbarButtonAddTurn
        '
        resources.ApplyResources(Me.INDbarButtonAddTurn, "INDbarButtonAddTurn")
        Me.INDbarButtonAddTurn.Id = 3
        Me.INDbarButtonAddTurn.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue
        Me.INDbarButtonAddTurn.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDbarButtonAddTurn.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDbarButtonAddTurn.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonAddTurn.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDbarButtonAddTurn.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDbarButtonAddTurn.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonAddTurn.ItemAppearance.Normal.Font = CType(resources.GetObject("INDbarButtonAddTurn.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDbarButtonAddTurn.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonAddTurn.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDbarButtonAddTurn.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDbarButtonAddTurn.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonAddTurn.Name = "INDbarButtonAddTurn"
        '
        'INDbarButtonAddNovelty
        '
        resources.ApplyResources(Me.INDbarButtonAddNovelty, "INDbarButtonAddNovelty")
        Me.INDbarButtonAddNovelty.Id = 6
        Me.INDbarButtonAddNovelty.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue
        Me.INDbarButtonAddNovelty.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDbarButtonAddNovelty.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDbarButtonAddNovelty.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonAddNovelty.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDbarButtonAddNovelty.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDbarButtonAddNovelty.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonAddNovelty.ItemAppearance.Normal.Font = CType(resources.GetObject("INDbarButtonAddNovelty.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDbarButtonAddNovelty.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonAddNovelty.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDbarButtonAddNovelty.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDbarButtonAddNovelty.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonAddNovelty.Name = "INDbarButtonAddNovelty"
        '
        'INDbarButtonAddEvent
        '
        resources.ApplyResources(Me.INDbarButtonAddEvent, "INDbarButtonAddEvent")
        Me.INDbarButtonAddEvent.Id = 8
        Me.INDbarButtonAddEvent.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue
        Me.INDbarButtonAddEvent.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDbarButtonAddEvent.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDbarButtonAddEvent.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonAddEvent.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDbarButtonAddEvent.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDbarButtonAddEvent.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonAddEvent.ItemAppearance.Normal.Font = CType(resources.GetObject("INDbarButtonAddEvent.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDbarButtonAddEvent.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonAddEvent.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDbarButtonAddEvent.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDbarButtonAddEvent.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonAddEvent.Name = "INDbarButtonAddEvent"
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonAddTurn), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonAddNovelty), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonAddEvent)})
        Me.PopupMenuActions.Manager = Me.BarManager
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'CtrCalendar
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDPcMain)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "CtrCalendar"
        CType(Me.INDPcMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDPcMain As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbarButtonAddTurn As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonAddNovelty As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonAddEvent As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
End Class
