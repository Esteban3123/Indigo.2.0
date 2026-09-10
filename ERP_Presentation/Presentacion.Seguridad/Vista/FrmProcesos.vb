'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Jorge Leonardo Vernaza
' Created          : 10-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.IO
Imports Presentation.Base
Imports System.Xml
Imports System.Text
Imports System.Xml.Serialization
#End Region


''' <summary>
''' Clase con toda la funcionalidad del formulario para agregar los procesos del menu de la barra inferior
''' </summary>
Public Class FrmProcesos

#Region "Variables Globales"
    ''' <summary>
    ''' Variable que contiene el listado de procesos
    ''' </summary>
    Dim Procesos As New List(Of MyProcess)
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo que se usa para cargar las imagenes con el open file  
    ''' </summary>
    ''' <returns></returns>
    Private Function CargarImagen() As Boolean
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    INDpeIcono.EditValue = Image.FromFile(openFileDialog1.FileName)
                    Return True
                End If
            Catch Ex As Exception
            Finally
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        Else
        End If
        Return False
    End Function

    ''' <summary>
    ''' Metodo para validar que los campos no esten vacios
    ''' </summary>
    ''' <returns></returns>
    Private Function Validar() As Boolean
        Validar = True
        If INDtxtDescripcion.Text = String.Empty Then
            Validar = False
        End If
        If INDtxtProceso.Text = String.Empty Then
            Validar = False
        End If
        If INDpeIcono.Image Is Presentation.Security.My.Resources.Resources.procesos Then
            Validar = False
        End If
    End Function

    ''' <summary>
    ''' Metodo para convertir el listado de procesos en xml y guardarlo en el disco
    ''' </summary>
    Private Sub GuardarXML()
        'Dim XML As New StreamWriter(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", "Process"))
        Dim XML As New StreamWriter(String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\", "Process"))
        Dim Serializer = New XmlSerializer(GetType(List(Of MyProcess)))
        Serializer.Serialize(XML, Procesos)
        XML.Dispose()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar el proceso seleccionado.
    ''' </summary>
    Private Sub Eliminar()
        Dim Proceso As MyProcess = CType(INDgvProcesos.GetFocusedRow(), MyProcess)
        Procesos.Remove(Proceso)
        INDgcProcesos.DataSource = Nothing
        INDgcProcesos.DataSource = Procesos
    End Sub
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento Clic en agregar donde agregamos un nuevo proceso a la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        'Validamos que los campos no esten vacios
        If Validar() = False Then Exit Sub
        Dim Proceso As New MyProcess
        Proceso.DescriptionProcess = INDtxtDescripcion.Text
        Proceso.NameProcess = INDtxtProceso.Text
        Dim Image As Bitmap
        Image = New Bitmap(INDpeIcono.Image)
        'Creamos la Matriz de Bytes
        Dim FotoProfesional As New MemoryStream()
        Image.Save(FotoProfesional, System.Drawing.Imaging.ImageFormat.Png)
        Dim ImagenMatriz As Byte() = FotoProfesional.GetBuffer()
        'Convertimos el arraydebytes a string
        Proceso.IconProcess = Convert.ToBase64String(ImagenMatriz)
        Procesos.Add(Proceso)
        INDgcProcesos.DataSource = Nothing
        INDgcProcesos.DataSource = Procesos

        INDtxtDescripcion.Text = String.Empty
        INDtxtProceso.Text = String.Empty
        INDpeIcono.Image = Presentation.Security.My.Resources.Resources.procesos
    End Sub

    ''' <summary>
    ''' Evento load del formulario donde buscamos el xml con los procesos y cargamos el listado de procesos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProcesos_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", "Process")) = True Then
        If File.Exists(String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\", "Process")) = True Then
            Dim xmlDoc = New XmlDocument
            'xmlDoc.Load(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", "Process"))
            xmlDoc.Load(String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\", "Process"))
            Dim swo As StringWriter = New StringWriter()
            Dim xw As XmlTextWriter = New XmlTextWriter(swo)
            xmlDoc.WriteTo(xw)
            Dim file = swo.ToString()
            Dim doc As XDocument = XDocument.Parse(file)
            Dim ms As MemoryStream = New MemoryStream(Encoding.ASCII.GetBytes(file))
            Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(List(Of MyProcess)))
            Procesos = CType(xs.Deserialize(ms), Global.System.Collections.Generic.List(Of Global.Presentation.Base.MyProcess))
            INDgcProcesos.DataSource = Procesos
        End If
    End Sub

    ''' <summary>
    ''' Evento clic en el boton eliminar de la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAcciones_Click(sender As Object, e As EventArgs) Handles INDpceAcciones.Click
        Eliminar()
    End Sub

    Private Sub FrmProcesos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GuardarXML()
    End Sub

    ''' <summary>
    '''Evento clic en el boton buscar donde disparamos el metodo para buscar la imagen en los archivos.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnBuscar_Click(sender As Object, e As EventArgs) Handles INDbtnBuscar.Click
        CargarImagen()
    End Sub
#End Region


End Class

