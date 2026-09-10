Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.DocumentalSystem.Entities
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.Controls
Imports System.IO

Public Class FrmAttach

#Region "Fields"

    ''' <summary>
    ''' Objeto para openFileDialog
    ''' </summary>
    Private fileOpener As New OpenFileDialog
    ''' <summary>
    ''' Colleccion de archivos guardados temporalmente
    ''' </summary>
    Private _tempFileCollection As System.CodeDom.Compiler.TempFileCollection
    ''' <summary>
    ''' Model para digitalización
    ''' </summary>
    Private modelDocument As MDigitalization
    ''' <summary>
    ''' Integer que representa el Id del Formulario
    ''' </summary>
    Private _idForm As Integer
    ''' <summary>
    ''' Nombre de la entidad que se va auditar
    ''' </summary>
    ''' <remarks></remarks>
    Private _entityName As String
    ''' <summary>
    ''' Integer que representa el Id del registro que tiene documentos
    ''' </summary>
    Private _idEntityAux As Integer
    ''' <summary>
    ''' Lista de documentos
    ''' </summary>
    Private listDocFullText As List(Of DocumentsStore)
    ''' <summary>
    ''' FormularioBase
    ''' </summary>
    Private _formBase As Presentation.Controls.FormBase

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


#Region "Builders"

    ''' <summary>
    ''' Constructor para Formulario adjuntar
    ''' </summary>
    ''' <param name="IdForm">Id del formulario del documento</param>
    ''' <param name="IdEntity">Id del registro del documento</param>
    Sub New(IdForm As Integer, IdEntity As Integer, Optional entityName As String = Nothing, Optional listDocuments As List(Of DocumentsStore) = Nothing)
        InitializeComponent()
        Me._idForm = IdForm
        Me._idEntityAux = IdEntity
        Me._entityName = entityName
        Me.listDocFullText = listDocuments
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento al cargar el formulario
    ''' </summary>
    Private Async Sub FrmAttach_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _tempFileCollection = New System.CodeDom.Compiler.TempFileCollection
        If Not TopForm.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Adjuntar) Then
            Me.INDAttachSmb.Enabled = False
        End If
        Me.Text = obtenerRecurso(Eresources.TituloAdjuntar, Eform.Adjuntar)
        Me.BarraBotones.Visible = False
        modelDocument = New MDigitalization

        If Me._idForm = 2178 Then
            Me.INDAttachSmb.Enabled = False
            Me.listDocFullText = Nothing
            Dim result = Await modelDocument.GetAttachmentsByFormAndEntity(Me._idForm, Me._entityName, Me._idEntityAux, False)
            If result.StateResult Then
                Dim attachments = result.ObjectEmbbeded
                If attachments IsNot Nothing AndAlso attachments.Any Then
                    Me.listDocFullText = New List(Of DocumentsStore)
                    For Each attachment In attachments
                        Me.listDocFullText.Add(New DocumentsStore With
                        {
                            .IdEntity = attachment.Id,
                            .Name = attachment.Name,
                            .Type = attachment.Extension,
                            .MetaData = attachment.Description,
                            .AttachDate = attachment.CreationDate
                        })
                    Next
                End If
            End If
        ElseIf Me._idForm = 2176 Then
            Me.INDAttachSmb.Enabled = False
        Else
            Me.listDocFullText = Await modelDocument.getDocumentsByIdFormAndIdEntity(Me._idForm, Me._idEntityAux, False)
        End If

        INDAttachGc.DataSource = If(listDocFullText IsNot Nothing, listDocFullText.ToList, Nothing)
    End Sub

    ''' <summary>
    ''' Evento al adjuntar un archivo
    ''' </summary>
    Private Sub INDAttachSmb_Click_(sender As Object, e As EventArgs) Handles INDAttachSmb.Click
        Dim fileOpener = GetOpenFileDialog()
        If (fileOpener.ShowDialog() = DialogResult.OK) Then
            'Guardamos los archivos temporalmente en una colección
            '_tempFileCollection.AddFile(fileOpener.FileName, keepFile:=False)
            Dim documentInfo As DocumentInfo = Nothing
            Dim fileInfo = New IO.FileInfo(fileOpener.FileName)
            documentInfo = New DocumentInfo With {.DocumentStream = System.IO.File.OpenRead(fileOpener.FileName), .DocumentStreamLength = fileInfo.Length, .FileName = fileInfo.Name, .SessionInf = Nothing}
            Dim frmMetaData As New FrmMetaData(Me._idForm, Me._idEntityAux, documentInfo)
            frmMetaData.TopForm = CType(Me.TopForm, Presentation.Controls.FormBase)
            frmMetaData.FilePath = fileOpener.FileName
            If frmMetaData.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Me.listDocFullText.Add(frmMetaData.DocumentStoreMetaData)
                Me.INDAttachGc.RefreshDataSource()
                Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                Me.TopForm.BarraBotones.LoadDocuments()
            Else
                '_tempFileCollection.Delete()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que al dar doble click se posiciona sobre un documento y permite visualizarlo
    ''' </summary>
    Private Async Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If TopForm.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Abrir) Then

            Me.INDAttachGc.Enabled = False
            Dim Document = CType(GridView1.GetRow(GridView1.FocusedRowHandle), DocumentsStore)
            'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
            Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
            tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension(Document.Name))
            _tempFileCollection.AddFile(tempFilePath, keepFile:=False)
            System.IO.File.Create(tempFilePath).Dispose()

            If Me._idForm = 2178 Then
                Dim result = Await modelDocument.GetAttachmentById(Document.IdEntity.Value)
                If result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing Then
                    File.WriteAllBytes(tempFilePath, result.ObjectEmbbeded.FileAttached)
                End If
            ElseIf Me._idForm = 2176 Then
                File.WriteAllBytes(tempFilePath, Document.Content)
            Else
                Using sqlFileStream = Await modelDocument.getData(Document.Id.ToString)
                    Using localFileStream = New IO.FileStream(tempFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        sqlFileStream.CopyTo(localFileStream)
                    End Using
                End Using
            End If

            Process.Start(tempFilePath)
            Me.INDAttachGc.Enabled = True
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SinPermisoAbrirDocumentos, Eform.CtrBarraBotones)
        End If
    End Sub

    ''' <summary>
    ''' Evento para cambiar el valor de la columna tipo en el gridControl
    ''' </summary>
    Private Sub BandedGridView1_CustomRowCellEdit(sender As Object, e As Views.Grid.CustomRowCellEditEventArgs) Handles GridView1.CustomRowCellEdit

        Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim data = CType(view.GetRow(e.RowHandle), DocumentsStore)
        Dim lista = (From document As ImageComboBoxItem In Me.INDImageDocumentsRepositoryIcb.Items Select document.Value).ToList()
        If lista IsNot Nothing AndAlso lista.Count > 0 Then
            If data IsNot Nothing Then
                If Not lista.Contains(data.Type) Then
                    If Not view.GetRowCellValue(e.RowHandle, Me.TipoDoc) = ".otro" Then
                        view.SetRowCellValue(e.RowHandle, Me.TipoDoc, ".otro")
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento cerrar formulario
    ''' </summary>
    Private Sub FrmAttach_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me._tempFileCollection IsNot Nothing AndAlso Me._tempFileCollection.Count > 0 Then
            Me._tempFileCollection.Delete()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configurar y obtener un objeto openFileDialog
    ''' </summary>
    ''' <returns>Objeto openFileDialog</returns>
    Private Function GetOpenFileDialog() As OpenFileDialog
        fileOpener.CheckPathExists = True
        fileOpener.CheckFileExists = True
        'fileOpener.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.GIF)|*.bmp;*.jpg;*.jpeg;*.GIF|" + _
        '   "PNG files (*.png)|*.png|text files (*.text)|*.txt|doc files (*.doc)|*.doc|docx files (*.docx)|*.docx|pdf files (*.pdf)|*.pdf"
        fileOpener.Multiselect = False
        fileOpener.AddExtension = True
        fileOpener.ValidateNames = True
        'fileOpener.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        fileOpener.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
        Return fileOpener
    End Function

#End Region

End Class


