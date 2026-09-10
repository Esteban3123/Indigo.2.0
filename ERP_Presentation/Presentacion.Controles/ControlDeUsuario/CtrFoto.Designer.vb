<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrFoto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrFoto))
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.INDpanelControles = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDicIconos = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDBtnCapturar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnCargar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnIniciar = New DevExpress.XtraEditors.SimpleButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.INDfotoUsuario = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDpanelControles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelControles.SuspendLayout()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDfotoUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTipController1
        '
        Me.ToolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
        '
        'INDpanelControles
        '
        resources.ApplyResources(Me.INDpanelControles, "INDpanelControles")
        Me.ToolTipController1.SetAllowHtmlText(Me.INDpanelControles, CType(resources.GetObject("INDpanelControles.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.INDpanelControles.Controls.Add(Me.INDBtnCancelar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnCapturar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnCargar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnIniciar)
        Me.INDpanelControles.Name = "INDpanelControles"
        Me.ToolTipController1.SetTitle(Me.INDpanelControles, resources.GetString("INDpanelControles.Title"))
        Me.ToolTipController1.SetToolTip(Me.INDpanelControles, resources.GetString("INDpanelControles.ToolTip"))
        Me.ToolTipController1.SetToolTipIconType(Me.INDpanelControles, CType(resources.GetObject("INDpanelControles.ToolTipIconType"), DevExpress.Utils.ToolTipIconType))
        '
        'INDBtnCancelar
        '
        resources.ApplyResources(Me.INDBtnCancelar, "INDBtnCancelar")
        Me.INDBtnCancelar.ImageIndex = 3
        Me.INDBtnCancelar.ImageList = Me.INDicIconos
        Me.INDBtnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCancelar.Name = "INDBtnCancelar"
        Me.INDBtnCancelar.ToolTipController = Me.ToolTipController1
        '
        'INDicIconos
        '
        Me.INDicIconos.ImageStream = CType(resources.GetObject("INDicIconos.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconos.Images.SetKeyName(0, "Abrir.png")
        Me.INDicIconos.Images.SetKeyName(1, "Captura.png")
        Me.INDicIconos.Images.SetKeyName(2, "Play.png")
        Me.INDicIconos.Images.SetKeyName(3, "Stop.png")
        '
        'INDBtnCapturar
        '
        resources.ApplyResources(Me.INDBtnCapturar, "INDBtnCapturar")
        Me.INDBtnCapturar.ImageIndex = 1
        Me.INDBtnCapturar.ImageList = Me.INDicIconos
        Me.INDBtnCapturar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCapturar.Name = "INDBtnCapturar"
        Me.INDBtnCapturar.ToolTipController = Me.ToolTipController1
        '
        'INDBtnCargar
        '
        resources.ApplyResources(Me.INDBtnCargar, "INDBtnCargar")
        Me.INDBtnCargar.ImageIndex = 0
        Me.INDBtnCargar.ImageList = Me.INDicIconos
        Me.INDBtnCargar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCargar.Name = "INDBtnCargar"
        Me.INDBtnCargar.ToolTipController = Me.ToolTipController1
        '
        'INDBtnIniciar
        '
        resources.ApplyResources(Me.INDBtnIniciar, "INDBtnIniciar")
        Me.INDBtnIniciar.ImageIndex = 2
        Me.INDBtnIniciar.ImageList = Me.INDicIconos
        Me.INDBtnIniciar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnIniciar.Name = "INDBtnIniciar"
        Me.INDBtnIniciar.ToolTipController = Me.ToolTipController1
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        '
        'INDfotoUsuario
        '
        resources.ApplyResources(Me.INDfotoUsuario, "INDfotoUsuario")
        Me.INDfotoUsuario.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        Me.INDfotoUsuario.Name = "INDfotoUsuario"
        Me.INDfotoUsuario.Properties.AccessibleDescription = resources.GetString("INDfotoUsuario.Properties.AccessibleDescription")
        Me.INDfotoUsuario.Properties.AccessibleName = resources.GetString("INDfotoUsuario.Properties.AccessibleName")
        Me.INDfotoUsuario.Properties.AllowFocused = False
        Me.INDfotoUsuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDfotoUsuario.Properties.ReadOnly = True
        Me.INDfotoUsuario.Properties.ShowMenu = False
        Me.INDfotoUsuario.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'PanelControl2
        '
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.ToolTipController1.SetAllowHtmlText(Me.PanelControl2, CType(resources.GetObject("PanelControl2.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelControl2.Name = "PanelControl2"
        Me.ToolTipController1.SetTitle(Me.PanelControl2, resources.GetString("PanelControl2.Title"))
        Me.ToolTipController1.SetToolTip(Me.PanelControl2, resources.GetString("PanelControl2.ToolTip"))
        Me.ToolTipController1.SetToolTipIconType(Me.PanelControl2, CType(resources.GetObject("PanelControl2.ToolTipIconType"), DevExpress.Utils.ToolTipIconType))
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.ToolTipController1.SetAllowHtmlText(Me.PanelControl1, CType(resources.GetObject("PanelControl1.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelControl1.Name = "PanelControl1"
        Me.ToolTipController1.SetTitle(Me.PanelControl1, resources.GetString("PanelControl1.Title"))
        Me.ToolTipController1.SetToolTip(Me.PanelControl1, resources.GetString("PanelControl1.ToolTip"))
        Me.ToolTipController1.SetToolTipIconType(Me.PanelControl1, CType(resources.GetObject("PanelControl1.ToolTipIconType"), DevExpress.Utils.ToolTipIconType))
        '
        'PanelControl3
        '
        resources.ApplyResources(Me.PanelControl3, "PanelControl3")
        Me.ToolTipController1.SetAllowHtmlText(Me.PanelControl3, CType(resources.GetObject("PanelControl3.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelControl3.Name = "PanelControl3"
        Me.ToolTipController1.SetTitle(Me.PanelControl3, resources.GetString("PanelControl3.Title"))
        Me.ToolTipController1.SetToolTip(Me.PanelControl3, resources.GetString("PanelControl3.ToolTip"))
        Me.ToolTipController1.SetToolTipIconType(Me.PanelControl3, CType(resources.GetObject("PanelControl3.ToolTipIconType"), DevExpress.Utils.ToolTipIconType))
        '
        'CtrFoto
        '
        resources.ApplyResources(Me, "$this")
        Me.ToolTipController1.SetAllowHtmlText(Me, CType(resources.GetObject("$this.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDfotoUsuario)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.INDpanelControles)
        Me.Name = "CtrFoto"
        Me.ToolTipController1.SetTitle(Me, resources.GetString("$this.Title"))
        Me.ToolTipController1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.ToolTipController1.SetToolTipIconType(Me, CType(resources.GetObject("$this.ToolTipIconType"), DevExpress.Utils.ToolTipIconType))
        CType(Me.INDpanelControles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelControles.ResumeLayout(False)
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDfotoUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDfotoUsuario As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDBtnIniciar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnCapturar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnCargar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents INDBtnCancelar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpanelControles As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDicIconos As DevExpress.Utils.ImageCollection
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl

End Class
