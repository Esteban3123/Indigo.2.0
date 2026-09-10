<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFirma
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFirma))
        Me.INDPictureEditFirma = New DevExpress.XtraEditors.PictureEdit()
        Me.NavBarControl1 = New DevExpress.XtraNavBar.NavBarControl()
        Me.INDnavbarOpciones = New DevExpress.XtraNavBar.NavBarGroup()
        Me.INDitemIniciar = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemAceptarFirma = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemColor = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemRecortar = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemAjustarTamaño = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemBorrar = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDitemCancelar = New DevExpress.XtraNavBar.NavBarItem()
        Me.INDicNavBarFirma = New DevExpress.Utils.ImageCollection()
        CType(Me.INDPictureEditFirma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicNavBarFirma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPictureEditFirma
        '
        Me.INDPictureEditFirma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPictureEditFirma.Location = New System.Drawing.Point(0, 0)
        Me.INDPictureEditFirma.Name = "INDPictureEditFirma"
        Me.INDPictureEditFirma.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPictureEditFirma.Properties.ReadOnly = True
        Me.INDPictureEditFirma.Properties.ShowMenu = False
        Me.INDPictureEditFirma.Size = New System.Drawing.Size(511, 186)
        Me.INDPictureEditFirma.TabIndex = 0
        '
        'NavBarControl1
        '
        Me.NavBarControl1.ActiveGroup = Me.INDnavbarOpciones
        Me.NavBarControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.NavBarControl1.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.INDnavbarOpciones})
        Me.NavBarControl1.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.INDitemAceptarFirma, Me.INDitemIniciar, Me.INDitemColor, Me.INDitemRecortar, Me.INDitemBorrar, Me.INDitemCancelar, Me.INDitemAjustarTamaño})
        Me.NavBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.NavBarControl1.Name = "NavBarControl1"
        Me.NavBarControl1.OptionsNavPane.ExpandedWidth = 110
        Me.NavBarControl1.Size = New System.Drawing.Size(95, 186)
        Me.NavBarControl1.SmallImages = Me.INDicNavBarFirma
        Me.NavBarControl1.TabIndex = 13
        Me.NavBarControl1.Text = "NavBarControl1"
        Me.NavBarControl1.View = New DevExpress.XtraNavBar.ViewInfo.StandardSkinExplorerBarViewInfoRegistrator("DevExpress Style")
        '
        'INDnavbarOpciones
        '
        Me.INDnavbarOpciones.Caption = "Opciones"
        Me.INDnavbarOpciones.Expanded = True
        Me.INDnavbarOpciones.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemIniciar), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemAceptarFirma), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemColor), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemRecortar), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemAjustarTamaño), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemBorrar), New DevExpress.XtraNavBar.NavBarItemLink(Me.INDitemCancelar)})
        Me.INDnavbarOpciones.Name = "INDnavbarOpciones"
        '
        'INDitemIniciar
        '
        Me.INDitemIniciar.Caption = "Iniciar"
        Me.INDitemIniciar.Name = "INDitemIniciar"
        Me.INDitemIniciar.SmallImageIndex = 6
        '
        'INDitemAceptarFirma
        '
        Me.INDitemAceptarFirma.Caption = "Aceptar"
        Me.INDitemAceptarFirma.Name = "INDitemAceptarFirma"
        Me.INDitemAceptarFirma.SmallImageIndex = 1
        '
        'INDitemColor
        '
        Me.INDitemColor.Caption = "Color"
        Me.INDitemColor.Name = "INDitemColor"
        Me.INDitemColor.SmallImageIndex = 3
        '
        'INDitemRecortar
        '
        Me.INDitemRecortar.Caption = "Recortar"
        Me.INDitemRecortar.Name = "INDitemRecortar"
        Me.INDitemRecortar.SmallImageIndex = 4
        '
        'INDitemAjustarTamaño
        '
        Me.INDitemAjustarTamaño.Caption = "Ajustar"
        Me.INDitemAjustarTamaño.Name = "INDitemAjustarTamaño"
        Me.INDitemAjustarTamaño.SmallImageIndex = 5
        '
        'INDitemBorrar
        '
        Me.INDitemBorrar.Caption = "Borrar"
        Me.INDitemBorrar.Name = "INDitemBorrar"
        Me.INDitemBorrar.SmallImageIndex = 0
        '
        'INDitemCancelar
        '
        Me.INDitemCancelar.Caption = "Cancelar"
        Me.INDitemCancelar.Name = "INDitemCancelar"
        Me.INDitemCancelar.SmallImageIndex = 2
        '
        'INDicNavBarFirma
        '
        Me.INDicNavBarFirma.ImageStream = CType(resources.GetObject("INDicNavBarFirma.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicNavBarFirma.Images.SetKeyName(0, "1320276504_draw_eraser.png")
        Me.INDicNavBarFirma.Images.SetKeyName(1, "aceptar16x16.png")
        Me.INDicNavBarFirma.Images.SetKeyName(2, "cancel_161.png")
        Me.INDicNavBarFirma.Images.SetKeyName(3, "circulode colores16x16.png")
        Me.INDicNavBarFirma.Images.SetKeyName(4, "cut_16x16.png")
        Me.INDicNavBarFirma.Images.SetKeyName(5, "Empty 32x32.png")
        Me.INDicNavBarFirma.Images.SetKeyName(6, "PLAY16X16.png")
        '
        'FrmFirma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 186)
        Me.Controls.Add(Me.NavBarControl1)
        Me.Controls.Add(Me.INDPictureEditFirma)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFirma"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Firma Digital"
        CType(Me.INDPictureEditFirma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicNavBarFirma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDPictureEditFirma As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents NavBarControl1 As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents INDnavbarOpciones As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents INDitemIniciar As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemAceptarFirma As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemColor As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemRecortar As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemAjustarTamaño As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemBorrar As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDitemCancelar As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents INDicNavBarFirma As DevExpress.Utils.ImageCollection
End Class
