Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBase
    Inherits DevExpress.XtraEditors.XtraForm

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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBase))
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDPanelControlBase = New DevExpress.XtraEditors.PanelControl()
        Me.ToolBars = New DevExpress.XtraEditors.PanelControl()
        Me.BarraBotones = New Presentation.Controls.CtrBarraBotones()
        Me.LayoutControls = New IndigoLayoutControl(Me)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Name = "INDPanelControlBase"
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ToolBars.Controls.Add(Me.BarraBotones)
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        Me.ToolBars.Name = "ToolBars"
        '
        'BarraBotones
        '
        Me.BarraBotones.ChangeMessageProgressBar = Nothing
        Me.BarraBotones.ClicBotonActualizar = False
        Me.BarraBotones.ColumnInfo = Nothing
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        Me.BarraBotones.FilterDataSource = Nothing
        Me.BarraBotones.HomologationsCount = 0
        Me.BarraBotones.Huella = Nothing
        Me.BarraBotones.LegalBookId = 0
        Me.BarraBotones.ListOperatingUnit = Nothing
        Me.BarraBotones.LyHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.BarraBotones.LySaveHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.Name = "BarraBotones"
        Me.BarraBotones.OperatingUnitValue = 0
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.PermissionsForm = Nothing
        Me.BarraBotones.PermiteConsultar = False
        Me.BarraBotones.PermiteGuardarResponsablePagoTercero = False
        Me.BarraBotones.ProgressBar = False
        Me.BarraBotones.States = CType(resources.GetObject("BarraBotones.States"), System.Collections.Generic.List(Of Presentation.Controls.StatusRecord))
        Me.BarraBotones.StatesWhitActions = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.StatusRecordEnabled = True
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.XtraLabelImage = Nothing
        Me.BarraBotones.XtraLabelText = "---"
        Me.BarraBotones.XtraLabelVisibility = False
        '
        'FormBase
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDPanelControlBase)
        Me.Controls.Add(Me.ToolBars)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FormBase"
        Me.ShowInTaskbar = False
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Public WithEvents INDPanelControlBase As DevExpress.XtraEditors.PanelControl
    Public WithEvents ToolBars As DevExpress.XtraEditors.PanelControl
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Public WithEvents BarraBotones As Presentation.Controls.CtrBarraBotones
    Protected WithEvents LayoutControls As Presentation.Controls.IndigoLayoutControl

End Class
