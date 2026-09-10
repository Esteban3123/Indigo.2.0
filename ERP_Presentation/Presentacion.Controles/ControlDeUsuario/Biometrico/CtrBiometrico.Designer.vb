<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrBiometrico
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
        Dim IndicatorState1 As DevExpress.XtraGauges.Core.Model.IndicatorState = New DevExpress.XtraGauges.Core.Model.IndicatorState()
        Dim IndicatorState2 As DevExpress.XtraGauges.Core.Model.IndicatorState = New DevExpress.XtraGauges.Core.Model.IndicatorState()
        Dim IndicatorState3 As DevExpress.XtraGauges.Core.Model.IndicatorState = New DevExpress.XtraGauges.Core.Model.IndicatorState()
        Dim IndicatorState4 As DevExpress.XtraGauges.Core.Model.IndicatorState = New DevExpress.XtraGauges.Core.Model.IndicatorState()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrBiometrico))
        Me.StateIndicatorComponent1 = New DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent()
        Me.INDbtnCerrarDispositivo = New DevExpress.XtraEditors.SimpleButton()
        Me.INDicIconos = New DevExpress.Utils.ImageCollection()
        Me.INDBtnIniciar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDicBiometrico = New DevExpress.Utils.ImageCollection()
        Me.INDpeImagenBiometrico = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.StateIndicatorComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicBiometrico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpeImagenBiometrico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StateIndicatorComponent1
        '
        Me.StateIndicatorComponent1.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(124.0!, 124.0!)
        Me.StateIndicatorComponent1.Name = "stateIndicatorComponent4"
        Me.StateIndicatorComponent1.Size = New System.Drawing.SizeF(100.0!, 200.0!)
        Me.StateIndicatorComponent1.StateIndex = 3
        IndicatorState1.Name = "State1"
        IndicatorState1.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.TrafficLight1
        IndicatorState2.Name = "State2"
        IndicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.TrafficLight2
        IndicatorState3.Name = "State3"
        IndicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.TrafficLight3
        IndicatorState4.Name = "State4"
        IndicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.TrafficLight4
        Me.StateIndicatorComponent1.States.AddRange(New DevExpress.XtraGauges.Core.Model.IIndicatorState() {IndicatorState1, IndicatorState2, IndicatorState3, IndicatorState4})
        '
        'INDbtnCerrarDispositivo
        '
        resources.ApplyResources(Me.INDbtnCerrarDispositivo, "INDbtnCerrarDispositivo")
        Me.INDbtnCerrarDispositivo.ImageIndex = 1
        Me.INDbtnCerrarDispositivo.ImageList = Me.INDicIconos
        Me.INDbtnCerrarDispositivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnCerrarDispositivo.Name = "INDbtnCerrarDispositivo"
        '
        'INDicIconos
        '
        Me.INDicIconos.ImageStream = CType(resources.GetObject("INDicIconos.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconos.Images.SetKeyName(0, "Play.png")
        Me.INDicIconos.Images.SetKeyName(1, "Stop.png")
        '
        'INDBtnIniciar
        '
        resources.ApplyResources(Me.INDBtnIniciar, "INDBtnIniciar")
        Me.INDBtnIniciar.ImageIndex = 0
        Me.INDBtnIniciar.ImageList = Me.INDicIconos
        Me.INDBtnIniciar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnIniciar.Name = "INDBtnIniciar"
        '
        'INDicBiometrico
        '
        resources.ApplyResources(Me.INDicBiometrico, "INDicBiometrico")
        Me.INDicBiometrico.ImageStream = CType(resources.GetObject("INDicBiometrico.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicBiometrico.Images.SetKeyName(0, "BiometricosinHuella.png")
        Me.INDicBiometrico.Images.SetKeyName(1, "Biometrico1.png")
        Me.INDicBiometrico.Images.SetKeyName(2, "Biometrico2.png")
        Me.INDicBiometrico.Images.SetKeyName(3, "Biometrico3.png")
        Me.INDicBiometrico.Images.SetKeyName(4, "BiometricoX.png")
        Me.INDicBiometrico.Images.SetKeyName(5, "BiometricoY.png")
        '
        'INDpeImagenBiometrico
        '
        resources.ApplyResources(Me.INDpeImagenBiometrico, "INDpeImagenBiometrico")
        Me.INDpeImagenBiometrico.EditValue = Global.Presentation.Controls.My.Resources.Resources.BiometricosinHuella
        Me.INDpeImagenBiometrico.Name = "INDpeImagenBiometrico"
        Me.INDpeImagenBiometrico.Properties.AccessibleDescription = resources.GetString("INDpeImagenBiometrico.Properties.AccessibleDescription")
        Me.INDpeImagenBiometrico.Properties.AccessibleName = resources.GetString("INDpeImagenBiometrico.Properties.AccessibleName")
        Me.INDpeImagenBiometrico.Properties.AllowFocused = False
        Me.INDpeImagenBiometrico.Properties.Appearance.BackColor = CType(resources.GetObject("INDpeImagenBiometrico.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDpeImagenBiometrico.Properties.Appearance.GradientMode = CType(resources.GetObject("INDpeImagenBiometrico.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDpeImagenBiometrico.Properties.Appearance.Image = CType(resources.GetObject("INDpeImagenBiometrico.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDpeImagenBiometrico.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeImagenBiometrico.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeImagenBiometrico.Properties.ReadOnly = True
        Me.INDpeImagenBiometrico.Properties.ShowMenu = False
        '
        'CtrBiometrico
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDbtnCerrarDispositivo)
        Me.Controls.Add(Me.INDBtnIniciar)
        Me.Controls.Add(Me.INDpeImagenBiometrico)
        Me.Name = "CtrBiometrico"
        CType(Me.StateIndicatorComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicBiometrico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpeImagenBiometrico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpeImagenBiometrico As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDBtnIniciar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnCerrarDispositivo As DevExpress.XtraEditors.SimpleButton
    Private WithEvents StateIndicatorComponent1 As DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent
    Friend WithEvents INDicIconos As DevExpress.Utils.ImageCollection
    Friend WithEvents INDicBiometrico As DevExpress.Utils.ImageCollection


End Class
