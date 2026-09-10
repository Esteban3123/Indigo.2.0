'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference
Imports System.IO

#End Region

''' <summary>
''' Utilidad para la carga de un archivo
''' </summary>
Public Class FrmUploadSingleFile

#Region "Fields"

    ''' <summary>
    ''' Token del archivo a cargar
    ''' </summary>
    Private _token As String

    ''' <summary>
    ''' Bandera que marca la carga de un paquete
    ''' </summary>
    Private _flagUploading As Boolean

    ''' <summary>
    ''' Bandera para cancelar la carga
    ''' </summary>
    Private _flagCancel As Boolean

    ''' <summary>
    ''' Ruta del archivo seleccionado
    ''' </summary>
    Private _pathSelectedFile As String

    ''' <summary>
    ''' Tamaño del buffer a usar
    ''' </summary>
    Private _bufferSize As Int64

    ''' <summary>
    ''' Lista de filtros a usar
    ''' </summary>
    Private _listFilters As List(Of FileFilter)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el token que identifica el archivo cargado
    ''' </summary>
    ''' <returns>El token del archivo</returns>
    Public ReadOnly Property Token As String
        Get
            Return Me._token
        End Get
    End Property

#End Region

#Region "Events"

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._bufferSize = 10240 '10KB
        Me._flagCancel = False
        Me._flagUploading = False
        Me._pathSelectedFile = String.Empty
        Me._token = String.Empty
        Me._listFilters = New List(Of FileFilter)({New FileFilter("Todos los Archivos", "*.*")})
        For Each f As FileFilter In Me._listFilters
            If Me.INDofdPackages.Filter.Equals(String.Empty) Then
                Me.INDofdPackages.Filter = f.ToString()
            Else
                Me.INDofdPackages.Filter &= "|" & f.ToString()
            End If
        Next
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="listFilters">Lista de filtros a usar</param>
    Public Sub New(ByVal listFilters As List(Of FileFilter))
        Me.New()
        Me._listFilters = listFilters
        For Each f As FileFilter In Me._listFilters
            If Me.INDofdPackages.Filter.Equals(String.Empty) Then
                Me.INDofdPackages.Filter = f.ToString()
            Else
                Me.INDofdPackages.Filter &= "|" & f.ToString()
            End If
        Next
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Selecciona el archivo
    ''' </summary>
    ''' <param name="files">Archivo seleccionado</param>
    Private Sub SelectFile(files As String)
        Me.BtnSelectedFile.Text = files.Substring(files.LastIndexOf("\") + 1)
        Me._pathSelectedFile = files

        Dim finf As New FileInfo(Me._pathSelectedFile)

        Me.LblNamePackage.Text = "=INFORMACIÓN DEL ARCHIVO=" & Environment.NewLine
        Me.LblNamePackage.Text &= "=========================" & Environment.NewLine
        Me.LblNamePackage.Text &= "NOMBRE: " & finf.Name & Environment.NewLine
        Me.LblNamePackage.Text &= "TIPO: " & Infrastructure.CrossCutting.Base.Utils.GetNameTypeFile(finf.Extension) & Environment.NewLine
        Me.LblNamePackage.Text &= "TAMAÑO: " & Infrastructure.CrossCutting.Base.Utils.CalcSizeFile(finf.Length) & Environment.NewLine
        Me.LblNamePackage.Text &= "Fecha de Modificación: " & finf.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") & Environment.NewLine

        Me.BtnLoadFile.Enabled = True
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se realiza la consulta del tamaño del buffer
    ''' </summary>
    Private Async Sub FrmUploadSingleFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._bufferSize = Await IndigoConecta.Instancia.CurrentCloud.IndigoFileManager.GetBufferSizeAsync()
    End Sub

    ''' <summary>
    ''' Cancela la carga de un archivo
    ''' </summary>
    Private Async Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        If Me._flagUploading Then
            If MessageBox.Show("Está seguro que desea cancelar la carga del archivo?", "Carga de Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Me._flagCancel = True
                If Not Me._token.Equals(String.Empty) Then
                    Await IndigoConecta.Instancia.CurrentCloud.IndigoFileManager.CancelUploadAsync(Me._token)
                End If
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            End If
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    ''' <summary>
    ''' Realiza la carga del archivo seleccionado
    ''' </summary>
    Private Async Sub BtnLoadFile_Click(sender As Object, e As EventArgs) Handles BtnLoadFile.Click
        Me.Cursor = Cursors.WaitCursor
        Me.BtnLoadFile.Enabled = False
        Me.BtnSelectedFile.Enabled = False
        Me.INDLyciLoading.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Me._flagUploading = True

        Dim ff As New FileInfo(Me._pathSelectedFile)
        Me._token = Await IndigoConecta.Instancia.CurrentCloud.IndigoFileManager.GetTokenAsync(ff)
        Dim stream As Stream = File.OpenRead(ff.FullName)

        While Not Me._flagCancel
            Dim buffer As Byte() = New Byte(Me._bufferSize - 1) {}
            Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
            If bytesRead = 0 Then
                Exit While
            Else
                Dim bufferAux As Byte() = New Byte(bytesRead - 1) {}
                System.Buffer.BlockCopy(buffer, 0, bufferAux, 0, bytesRead)
                Me.INDpgbLoading.EditValue = ((CDbl(stream.Position) * 100.0) / CDbl(stream.Length))
                If Not Await IndigoConecta.Instancia.CurrentCloud.IndigoFileManager.UploadFileAsync(Me._token, bufferAux, (stream.Position = stream.Length)) Then
                    Exit While
                End If
            End If
        End While
        Me._flagUploading = False
        If Not Me._flagCancel Then
            MessageBox.Show("El archivo se ha cargado correctamente..!!", "Carga de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If

        Me.INDLyciLoading.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BtnLoadFile.Enabled = True
        Me.BtnSelectedFile.Enabled = True
        Me.Cursor = Cursors.[Default]
    End Sub

    ''' <summary>
    ''' Abre el dialogo para seleccionar un archivo
    ''' </summary>
    Private Sub BtnSelectedFile_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BtnSelectedFile.ButtonClick
        If Me.INDofdPackages.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            If Not Me.INDofdPackages.FileName.Equals(String.Empty) Then
                Me.SelectFile(Me.INDofdPackages.FileName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se modifica el cursor para dar el efecto de arrastrado y soltado
    ''' </summary>
    Private Sub LblNamePackage_DragEnter(sender As Object, e As DragEventArgs) Handles LblNamePackage.DragEnter, Me.DragEnter, LycRoot.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    ''' <summary>
    ''' Aqui se agrega el archivo arrastrado y soltado
    ''' </summary>
    Private Sub LblNamePackage_DragDrop(sender As Object, e As DragEventArgs) Handles LblNamePackage.DragDrop, Me.DragDrop, LycRoot.DragDrop
        Dim files As String() = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files.Length > 0 AndAlso Directory.Exists(files(0)) Then
            Me.SelectFile(files(0))
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los datos del filtro a usar en la carga de archivos
''' </summary>
Public Class FileFilter

#Region "Fields"

    ''' <summary>
    ''' Nombre del filtro
    ''' </summary>
    Private _nameFilter As String

    ''' <summary>
    ''' Extensiòn del archivo que se va a filtrar
    ''' </summary>
    Private _extensionFilter As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre del filtro a mostrar
    ''' </summary>
    ''' <value>Nombre del filtro a mostrar</value>
    ''' <returns>El nombre del filtro a mostrar</returns>
    Public Property NameFilter As String
        Get
            Return Me._nameFilter
        End Get
        Set(value As String)
            Me._nameFilter = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la extensiòn del los archivos a filtrar
    ''' </summary>
    ''' <value>Extensiòn del los archivos a filtrar</value>
    ''' <returns>La extensiòn del los archivos a filtrar</returns>
    Public Property ExtensionFilter As String
        Get
            Return Me._extensionFilter
        End Get
        Set(value As String)
            Me._extensionFilter = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="nameFilter">Nombre del filtro</param>
    ''' <param name="extensionFilter">Extensiòn de los archivos a filtrar</param>
    Public Sub New(ByVal nameFilter As String, ByVal extensionFilter As String)
        Me._nameFilter = nameFilter
        Me._extensionFilter = extensionFilter
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la cadena que representa el filtro
    ''' </summary>
    ''' <returns>Cadena que representa el filtro</returns>
    Public Overrides Function ToString() As String
        Return Me._nameFilter.Trim() & "|" & Me._extensionFilter.Trim()
    End Function

#End Region

End Class