Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportGeneralProfitabilityB
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
        Me.INDCnpReport = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrDateNavigator2 = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SearchLookUpEditEx1 = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.SearchLookUpEditEx2 = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnpReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditEx1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditEx2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnpReport)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 24)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 705)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'INDCnpReport
        '
        Me.INDCnpReport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnpReport.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnpReport.LayoutControl = Nothing
        Me.INDCnpReport.Location = New System.Drawing.Point(2, 7)
        Me.INDCnpReport.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnpReport.Name = "INDCnpReport"
        Me.INDCnpReport.Size = New System.Drawing.Size(200, 696)
        Me.INDCnpReport.TabIndex = 0
        Me.INDCnpReport.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.SearchLookUpEditEx2)
        Me.INDLcBase.Controls.Add(Me.SearchLookUpEditEx1)
        Me.INDLcBase.Controls.Add(Me.CtrDateNavigator2)
        Me.INDLcBase.Controls.Add(Me.CtrDateNavigator1)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 696)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDLcgBase
        '
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 696)
        Me.INDLcgBase.TextVisible = False
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Nothing
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator1.Location = New System.Drawing.Point(24, 58)
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.Size = New System.Drawing.Size(356, 71)
        Me.CtrDateNavigator1.TabIndex = 4
        Me.CtrDateNavigator1.WithEvent = True
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.CtrDateNavigator1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(360, 91)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(360, 91)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(760, 91)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Inicio"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(96, 13)
        '
        'CtrDateNavigator2
        '
        Me.CtrDateNavigator2.CtrCalendar = Nothing
        Me.CtrDateNavigator2.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator2.Location = New System.Drawing.Point(24, 149)
        Me.CtrDateNavigator2.Name = "CtrDateNavigator2"
        Me.CtrDateNavigator2.Size = New System.Drawing.Size(356, 71)
        Me.CtrDateNavigator2.TabIndex = 5
        Me.CtrDateNavigator2.WithEvent = True
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.CtrDateNavigator2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 91)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(360, 91)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(360, 91)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(760, 91)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Final"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(96, 13)
        '
        'SearchLookUpEditEx1
        '
        Me.SearchLookUpEditEx1.AllowQueryOne = False
        Me.SearchLookUpEditEx1.Datasource = Nothing
        Me.SearchLookUpEditEx1.DisplayMember = ""
        Me.SearchLookUpEditEx1.DisplayNullText = ""
        Me.SearchLookUpEditEx1.EditValue = Nothing
        Me.SearchLookUpEditEx1.EnterMoveNextControl = False
        Me.SearchLookUpEditEx1.FuncQueryOnKeyEnterPressed = Nothing
        Me.SearchLookUpEditEx1.IdOpenForm = 0
        Me.SearchLookUpEditEx1.Location = New System.Drawing.Point(24, 240)
        Me.SearchLookUpEditEx1.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.SearchLookUpEditEx1.MinimumSize = New System.Drawing.Size(100, 28)
        Me.SearchLookUpEditEx1.Name = "SearchLookUpEditEx1"
        Me.SearchLookUpEditEx1.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.SearchLookUpEditEx1.Size = New System.Drawing.Size(356, 28)
        Me.SearchLookUpEditEx1.TabIndex = 6
        Me.SearchLookUpEditEx1.ValueMember = ""
        Me.SearchLookUpEditEx1.View = Me.SearchLookUpEditExView1
        '
        'SearchLookUpEditExView1
        '
        Me.SearchLookUpEditExView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView1.Name = "SearchLookUpEditExView1"
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView1.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.SearchLookUpEditEx1
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 182)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(760, 56)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem2, Me.LayoutControlItem1, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(784, 676)
        '
        'SearchLookUpEditEx2
        '
        Me.SearchLookUpEditEx2.AllowQueryOne = False
        Me.SearchLookUpEditEx2.Datasource = Nothing
        Me.SearchLookUpEditEx2.DisplayMember = ""
        Me.SearchLookUpEditEx2.DisplayNullText = ""
        Me.SearchLookUpEditEx2.EditValue = Nothing
        Me.SearchLookUpEditEx2.EnterMoveNextControl = False
        Me.SearchLookUpEditEx2.FuncQueryOnKeyEnterPressed = Nothing
        Me.SearchLookUpEditEx2.IdOpenForm = 0
        Me.SearchLookUpEditEx2.Location = New System.Drawing.Point(24, 297)
        Me.SearchLookUpEditEx2.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.SearchLookUpEditEx2.MinimumSize = New System.Drawing.Size(100, 28)
        Me.SearchLookUpEditEx2.Name = "SearchLookUpEditEx2"
        Me.SearchLookUpEditEx2.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.SearchLookUpEditEx2.Size = New System.Drawing.Size(356, 28)
        Me.SearchLookUpEditEx2.TabIndex = 7
        Me.SearchLookUpEditEx2.ValueMember = ""
        Me.SearchLookUpEditEx2.View = Me.SearchLookUpEditExView2
        '
        'SearchLookUpEditExView2
        '
        Me.SearchLookUpEditExView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView2.Name = "SearchLookUpEditExView2"
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView2.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.SearchLookUpEditEx2
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 238)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(760, 396)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(96, 13)
        '
        'FrmReportGeneralProfitabilityB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmReportGeneralProfitabilityB"
        Me.Opacity = 1.0R
        Me.Text = "FrmReportGeneralProfitabilityB"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnpReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditEx1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditEx2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents SearchLookUpEditEx2 As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SearchLookUpEditEx1 As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CtrDateNavigator2 As Presentation.Controls.CtrDateNavigator
    Friend WithEvents CtrDateNavigator1 As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCnpReport As Presentation.Controls.CtrNavigationControlPanel
End Class
