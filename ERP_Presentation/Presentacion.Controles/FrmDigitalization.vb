Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.DocumentalSystem.Entities
Imports System.Security.Permissions

''' <summary>
''' Frontal de digitalización
''' </summary>
Public Class FrmDigitalization

#Region "Fields"

    ''' <summary>
    ''' Objeto que permite almacenar controladamente archivos temporales
    ''' </summary>
    ''' <remarks></remarks>
    Private _tempFileCollection As System.CodeDom.Compiler.TempFileCollection
    ''' <summary>
    '''  Variable para guardar la extension del archivo escaneado
    ''' </summary>
    Private _fileExtension As String
    ''' <summary>
    '''  Variable para guardar la ruta del archivo escaneado
    ''' </summary>
    Private _filePath As String
    ''' <summary>
    ''' Objeto para abrir cuadro de adjuntar archivos
    ''' </summary>
    Private fileOpener As OpenFileDialog
    ''' <summary>
    ''' FormularioBase
    ''' </summary>
    Private _formBase As Presentation.Controls.FormBase
    ''' <summary>
    ''' Integer que representa el Id del Formulario
    ''' </summary>
    Private _idForm As Integer
    ''' <summary>
    ''' Integer que representa el Id del registro que tiene documentos
    ''' </summary>
    Private _idEntityAux As Integer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor del frontal
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdEntity">Id Registro o Entidad</param>
    Sub New(IdForm As Integer, IdEntity As Integer)
        InitializeComponent()
        Me._idForm = IdForm
        Me._idEntityAux = IdEntity
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, "")
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, "")
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad formulario Base
    ''' </summary>
    Public Property TopForm As Presentation.Controls.FormBase
        Get
            Return Me._formBase
        End Get
        Set(value As Presentation.Controls.FormBase)
            Me._formBase = value
        End Set
    End Property

#End Region

#Region "Methods or Functions"

    ''' <summary>
    ''' Se establecen los valores iniciales del control twain
    ''' </summary>
    Private Sub CargarDispositivos()
        DynamicDotNetTwainThumb.MouseShape = True
        DynamicDotNetTwainThumb.MaxImagesInBuffer = 100
        DynamicDotNetTwainThumb.SetViewMode(1, 3)
        DynamicDotNetTwainThumb.AllowMultiSelect = True
        DynamicDotNetTwainView.MaxImagesInBuffer = 1
        DynamicDotNetTwainView.SetViewMode(-1, -1)
        DynamicDotNetTwainView.IfFitWindow = True
        DynamicDotNetTwainView.MouseShape = False
        For i As Integer = 0 To DynamicDotNetTwainView.SourceCount - 1
            INDcbDispositivos.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(DynamicDotNetTwainView.SourceNameItems(i), i, i)})
            INDbtnEscanear.Enabled = True
        Next
        INDcbDispositivos.EditValue = 0
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para actualizar valores de navegacion adelante, atras, ultimo, primero etc.
    ''' </summary>
    Private Sub UpdateImageInfo()

        DynamicDotNetTwainThumb.CopyToClipboard(DynamicDotNetTwainThumb.CurrentImageIndexInBuffer)
        DynamicDotNetTwainView.RemoveAllImages()
        DynamicDotNetTwainView.LoadDibFromClipboard()
        My.Computer.Clipboard.Clear()
        Dim total As String = CStr(DynamicDotNetTwainThumb.HowManyImagesInBuffer)
        INDlblImagenActual.Text = obtenerRecurso(Eresources.PaginaDigitalizacion, Eform.Digitalizacion) & " " & CStr(DynamicDotNetTwainThumb.CurrentImageIndexInBuffer + 1) & " " & obtenerRecurso(Eresources.DeDigitalización, Eform.Digitalizacion) & " " & total
        If (DynamicDotNetTwainThumb.HowManyImagesInBuffer = 100) Then
            INDbtnEscanear.Enabled = False
        End If
        If (DynamicDotNetTwainThumb.HowManyImagesInBuffer = 0) Then
            INDbtnDerecha.Enabled = False
            INDbtnIzquierda.Enabled = False
            INDbtnRotarDerecha.Enabled = False
            INDbtnRotarIzquierda.Enabled = False
            INDbtnEliminar.Enabled = False
            INDbtnUltimo.Enabled = False
            INDbtnPrimero.Enabled = False
            INDbtnGuardar.Enabled = False
        Else
            INDbtnDerecha.Enabled = True
            INDbtnIzquierda.Enabled = True
            INDbtnRotarDerecha.Enabled = True
            INDbtnRotarIzquierda.Enabled = True
            INDbtnUltimo.Enabled = True
            INDbtnPrimero.Enabled = True
            INDbtnEliminar.Enabled = True
            If (DynamicDotNetTwainThumb.CurrentImageIndexInBuffer >= 1) Then
                INDbtnPrimero.Enabled = True
                INDbtnIzquierda.Enabled = True
            Else
                INDbtnPrimero.Enabled = False
                INDbtnIzquierda.Enabled = False
            End If
            If (DynamicDotNetTwainThumb.CurrentImageIndexInBuffer < DynamicDotNetTwainThumb.HowManyImagesInBuffer - 1) Then
                INDbtnDerecha.Enabled = True
                INDbtnUltimo.Enabled = True
            Else
                INDbtnDerecha.Enabled = False
                INDbtnUltimo.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para scanear los documentos
    ''' </summary>
    Private Sub Scan()

        If INDcbDispositivos.EditValue Is Nothing Then
            Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.SeleccioneDispositivo, Eform.Digitalizacion)
            INDcbDispositivos.Focus()
            Exit Sub
        End If
        DynamicDotNetTwainThumb.SelectSourceByIndex(INDcbDispositivos.EditValue)
        DynamicDotNetTwainThumb.IfShowUI = False
        DynamicDotNetTwainThumb.OpenSource()
        DynamicDotNetTwainThumb.IfDisableSourceAfterAcquire = True
        'Aplicamos la resolución 
        DynamicDotNetTwainThumb.PixelType = 1
        DynamicDotNetTwainThumb.BitDepth = 8
        INDMarqueeProgressBarControl.Visible = True
        Me.INDLycProcessPrgM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Verificamos si es scaneado duplex
        If INDchkDuplex.Checked = True Then
            DynamicDotNetTwainThumb.IfDuplexEnabled = True
        Else
            DynamicDotNetTwainThumb.IfDuplexEnabled = False
        End If
        'Verificamos si es scaneado ADF
        If INDchkADF.Checked = True Then
            DynamicDotNetTwainThumb.IfFeederEnabled = True
            DynamicDotNetTwainThumb.IfAutoFeed = True
        Else
            DynamicDotNetTwainThumb.IfFeederEnabled = False
            DynamicDotNetTwainThumb.IfAutoFeed = False
        End If
        If (DynamicDotNetTwainThumb.Duplex = 0 And INDchkDuplex.CheckState = 1) Then

            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.EscaneoDuplex, Eform.Digitalizacion)
            INDchkDuplex.CheckState = False
            INDMarqueeProgressBarControl.Visible = False
            Me.INDLycProcessPrgM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Exit Sub
        End If
        DynamicDotNetTwainThumb.AcquireImage()
        INDbtnGuardar.Enabled = True
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento al cargar el frontal
    ''' </summary>
    Private Sub CtrDigitalizationDocuments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _tempFileCollection = New System.CodeDom.Compiler.TempFileCollection
        Me.Text = obtenerRecurso(Eresources.TituloDigitalizacion, Eform.Digitalizacion)
        Me.BarraBotones.Visible = False
        DynamicDotNetTwainView.OpenSourceManager()
        UpdateImageInfo()
        CargarDispositivos()
    End Sub

    ''' <summary>
    ''' Evento sobre el envio de transferencia
    ''' </summary>
    Private Sub dynamicDotNetTwainThumb_OnPostTransfer() Handles DynamicDotNetTwainThumb.OnPostTransfer
        INDMarqueeProgressBarControl.Visible = False
        Me.INDMarqueeProgressBarControl.Visible = False
        Me.INDLycProcessPrgM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento al hacer click sobre alguno de los thumbnails
    ''' </summary>
    Private Sub dynamicDotNetTwainThum_OnMouseClick(ByVal sImageIndex As System.Int16) Handles DynamicDotNetTwainThumb.OnMouseClick
        INDMarqueeProgressBarControl.Visible = False
        DynamicDotNetTwainThumb.CurrentImageIndexInBuffer = sImageIndex
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento al dar click en guardar
    ''' </summary>
    Private Sub INDbtnGuardar_Click(sender As Object, e As EventArgs) Handles INDbtnGuardar.Click
        Dim data As Byte() = DynamicDotNetTwainThumb.SaveAllAsMultiPageTIFFToBytes()
        Dim model As New MDigitalization
        Dim documentInfo As DocumentInfo = Nothing
        'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
        Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
        tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension("d.tiff"))
        _tempFileCollection.AddFile(tempFilePath, keepFile:=False)
        System.IO.File.Create(tempFilePath).Dispose()
        If data IsNot Nothing Then
            System.IO.File.WriteAllBytes(tempFilePath, data)
            Dim fileInfo = New IO.FileInfo(tempFilePath)
            'Escribimos los bytes en el archivo temporal
            documentInfo = New DocumentInfo With {.DocumentStream = System.IO.File.OpenRead(tempFilePath), .DocumentStreamLength = fileInfo.Length, .FileName = fileInfo.Name, .SessionInf = Nothing}
            Dim frmMetaData As New FrmMetaData(Me._idForm, Me._idEntityAux, documentInfo)
            frmMetaData.TopForm = Me.TopForm
            frmMetaData.FilePath = tempFilePath
            If frmMetaData.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                Me.TopForm.BarraBotones.LoadDocuments()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmDigitalization_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me._tempFileCollection IsNot Nothing AndAlso Me._tempFileCollection.Count > 0 Then
            Me._tempFileCollection.Delete()
        End If
    End Sub

    ''' <summary>
    ''' Evento al dar click sobre el boton escanear
    ''' </summary>
    Private Sub INDbtnEscanear_Click(sender As Object, e As EventArgs) Handles INDbtnEscanear.Click
        DynamicDotNetTwainThumb.Resolution = 300
        Scan()
    End Sub

    ''' <summary>
    ''' Evento que al dar click permite cargar imagenes para visualizar
    ''' </summary>
    Private Sub INDbtnEscanear_Click_1(sender As Object, e As EventArgs)
        fileOpener = New OpenFileDialog()
        fileOpener.Filter = "TIFF Images (*.tif)|*.tif"
        If (fileOpener.ShowDialog() = DialogResult.OK) Then
            _fileExtension = My.Computer.FileSystem.GetFileInfo(fileOpener.FileName).Extension
            _filePath = fileOpener.FileName
            If _fileExtension = ".tif" Then
                If (DynamicDotNetTwainThumb.LoadImageEx(fileOpener.FileName, Dynamsoft.DotNet.TWAIN.Enums.DWTImageFileFormat.WEBTW_TIF) = False) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso("No se puede abrir el archivo - puede que no sea un archivo TIFF")
                Else
                    UpdateImageInfo()
                    INDbtnGuardar.Enabled = True
                    _fileExtension = ".tiff"
                End If
            Else
                If DynamicDotNetTwainThumb.CurrentImageIndexInBuffer >= 0 Then
                    _fileExtension = ".tiff"
                    Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso("No se puede adjuntar un archivo con diferente formato, guarde el archivo actual y continue con la operacion")
                    Exit Sub
                End If
            End If
        End If
    End Sub

#Region "Navigation Control"

    ''' <summary>
    ''' Evento primer registro
    ''' </summary>
    Private Sub INDbtnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnPrimero.Click
        DynamicDotNetTwainThumb.CurrentImageIndexInBuffer = 0
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento hacia atras
    ''' </summary>
    Private Sub INDbtnIzquierda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnIzquierda.Click
        DynamicDotNetTwainThumb.CurrentImageIndexInBuffer = DynamicDotNetTwainThumb.CurrentImageIndexInBuffer - 1
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' ''' Evento hacia adelante
    ''' </summary>
    Private Sub INDbtnDerecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnDerecha.Click
        DynamicDotNetTwainThumb.CurrentImageIndexInBuffer = DynamicDotNetTwainThumb.CurrentImageIndexInBuffer + 1
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento ultimo registro
    ''' </summary>
    Private Sub INDbtnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnUltimo.Click
        DynamicDotNetTwainThumb.CurrentImageIndexInBuffer = DynamicDotNetTwainThumb.HowManyImagesInBuffer - 1
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento girar imagen hacia la izquierda
    ''' </summary>
    Private Sub INDbtnRotarIzquierda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnRotarIzquierda.Click
        DynamicDotNetTwainThumb.RotateLeft(DynamicDotNetTwainThumb.CurrentImageIndexInBuffer)
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento girar imagen hacia la derecha
    ''' </summary>
    Private Sub INDbtnRotarDerecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnRotarDerecha.Click
        DynamicDotNetTwainThumb.RotateRight(DynamicDotNetTwainThumb.CurrentImageIndexInBuffer)
        UpdateImageInfo()
    End Sub

    ''' <summary>
    ''' Evento eliminar pagina
    ''' </summary>
    Private Sub INDbtnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnEliminar.Click
        DynamicDotNetTwainThumb.RemoveImage(DynamicDotNetTwainThumb.CurrentImageIndexInBuffer)
        UpdateImageInfo()
    End Sub

#End Region

#End Region

    Private Sub FrmDigitalization_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If WindowState = FormWindowState.Minimized Then
            Me.Hide()
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub

    'Sub minimizeToTrayEvent()


    'End Sub
    'Private minimizeToTray As Boolean = True
    'Private Const WM_SYSCOMMAND As Integer = &H112
    'Private Const SC_MINIMIZE As Integer = &HF020

    '<SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
    'Protected Overrides Sub WndProc(ByRef m As Message)
    '    Select Case m.Msg
    '        Case WM_SYSCOMMAND
    '            Dim command As Integer = m.WParam.ToInt32() And &HFFF0
    '            If command = SC_MINIMIZE AndAlso Me.minimizeToTray Then
    '                ' For example
    '                minimizeToTrayEvent()
    '            End If
    '            Exit Select
    '    End Select
    '    MyBase.WndProc(m)
    'End Sub

End Class