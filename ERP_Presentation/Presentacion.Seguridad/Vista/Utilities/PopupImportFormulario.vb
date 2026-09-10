Imports System.Threading.Tasks
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Domain.Security.Entities
Imports Presentation.Base
Imports Presentation.Security.MVP

Public Class PopupImportFormulario
    Inherits Presentation.Controls.FormBase

#Region "Propiedades"
    ''' <summary>
    ''' Url de ambiente seleccionado por usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property Url As String
        Get
            Return INDTxtUrlDev.Text
        End Get
        Set(value As String)
            INDTxtUrlDev.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Alamcena formularios con acciones del ambiente seleccionado de desarrollo
    ''' </summary>
    Private ListFormDev As List(Of VieDBForm)
    ''' <summary>
    ''' Alamcena formularios con acciones
    ''' </summary>
    Private ListForm As List(Of VieDBForm)
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor del formulario
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "metodos"
    ''' <summary>
    ''' Cargar los listados de formularios
    ''' </summary>
    Private Async Function LoadStatus() As Task
        Using model As New MFormulario()
            If ListFormDev Is Nothing Then
                ListFormDev = Await model.ConsultarFormulariosDesarrollo(Url)
            End If

            If ListForm Is Nothing Then
                ListForm = Await model.ConsultarFormularios()
            End If

            For Each item As VieDBForm In ListFormDev.Where(Function(x) x.Crud = ECrud.Unchanged AndAlso Not ListForm.Any(Function(y) x.IdForm = y.IdForm)).ToList
                item.Crud = ECrud.Added
            Next

            INDGcForms.DataSource = ListFormDev
        End Using
    End Function
#End Region

#Region "Handles"
    ''' <summary>
    ''' Inicializador de formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupImportFormulario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Url = "https://erptx-qa.azurewebsites.net/" 'Se deja quemado el valor de la url de desarrollo. Url ambiente actual(indigo.UriWebServices)
    End Sub

    ''' <summary>
    ''' btn buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbSearch_Click(sender As Object, e As EventArgs) Handles INDSbSearch.Click
        If String.IsNullOrEmpty(Url) Then
            MessageIndigo.Show("Debe diligenciar la url.", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Dim validatedUri As Uri = Nothing
        Uri.TryCreate(Url, UriKind.RelativeOrAbsolute, validatedUri)
        If Not validatedUri.IsAbsoluteUri Then
            MessageIndigo.Show("Url no valida.", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Try
            INDSbSearch.Enabled = False
            Await LoadStatus()
            INDSbSearch.Enabled = True
        Catch ex As Exception
            INDSbSearch.Enabled = True
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Seleccionar formularios a importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDRiceSelect.CheckedChanged
        Dim gridRow = CType(INDGvForms.GetFocusedRow(), VieDBForm)
        Dim control As DevExpress.XtraEditors.CheckEdit = CType(sender, DevExpress.XtraEditors.CheckEdit)
        'Se usa el estado para validar si fue seleccionado, por defecto llega en 1, 0 cuando se selecciona.
        If gridRow.Crud = ECrud.Added Then
            If control.Checked Then
                gridRow.State = 0
            Else
                gridRow.State = 1
            End If
        Else
            gridRow.State = 1
            control.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Accion de importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbImport_Click(sender As Object, e As EventArgs) Handles INDSbImport.Click
        If ListFormDev Is Nothing Then
            Exit Sub
        End If
        Dim ListSelected As List(Of VieDBForm) = ListFormDev.Where(Function(x) x.State = 0 AndAlso x.Crud = ECrud.Added).ToList
        If ListSelected Is Nothing OrElse ListSelected.Count = 0 Then
            MessageIndigo.Show("Debe seleccionar registros.", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If
        Using model As New MFormulario()
            Try
                INDSbImport.Enabled = False
                Dim _created As Boolean = Await model.GuardarFormulariosImportar(ListSelected)
                If _created Then
                    MessageIndigo.Show("Proceso importar exitoso.", Infrastructure.CrossCutting.Base.MessageType.Information, Me.Text)
                    Me.Close()
                Else
                    MessageIndigo.Show("Proceso importar fallido.", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
                End If
                INDSbImport.Enabled = True
            Catch ex As Exception
                INDSbImport.Enabled = True
                Throw ex
            End Try
        End Using
    End Sub



#End Region

End Class