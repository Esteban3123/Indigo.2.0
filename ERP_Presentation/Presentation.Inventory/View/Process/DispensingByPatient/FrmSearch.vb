'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/04/2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports System.Windows.Forms
Imports System.Threading

#End Region

Public Class FrmSearch

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Se lanza cuando se cerrando el frontal
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event ReturnMedicalOrderRecipe(ByVal sender As Object, ByVal e As SearchMedicalOrderRecipeArgs)

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece la fecha actual
    ''' </summary>
    Public WriteOnly Property SetDateValues() As Date
        Set(value As Date)
            INDdteInitialDate.EditValue = value
            INDdteEndDate.EditValue = value.AddDays(1)
        End Set
    End Property

    ''' <summary>
    ''' Establece si el control esta disponible
    ''' </summary>
    Private WriteOnly Property ActionsOnControls() As Boolean
        Set(value As Boolean)
            INDdteInitialDate.Enabled = value
            INDdteEndDate.Enabled = value
            INDbtnSearch.Enabled = value
            INDbtnAccept.Enabled = value
        End Set
    End Property

    Private _logisticOperator As Integer

    Public Property LogisticOperator As Integer
        Get
            Return _logisticOperator
        End Get
        Set(value As Integer)
            _logisticOperator = value
        End Set
    End Property

    Private _officeType As Integer

    Public Property OfficeType As Integer
        Get
            Return _officeType
        End Get
        Set(value As Integer)
            _officeType = value
        End Set
    End Property

    Private _habilitationCode As String

    Public Property HabilitationCode As String
        Get
            Return _habilitationCode
        End Get
        Set(value As String)
            _habilitationCode = value
        End Set
    End Property

    ''' <summary>
    ''' Número de la orden medica seleccionada
    ''' </summary>
    Private _medicalOrderRecipe As String

    Public ReadOnly Property MedicalOrderRecipe As String
        Get
            Return Me._medicalOrderRecipe
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cierra el frontal al presionar la tecla Escape
    ''' </summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Me.Cancel()
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ''' <summary>
    ''' Obtiene el listado de dispensacion por rango de fechas
    ''' </summary>
    Private Async Sub GetListByRangeDate()
        Try
            ActionsOnControls = False
            INDviewResult.ShowLoadingPanel()

            Dim result = Await ExecuteGetList()
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If

            ActionsOnControls = True
            INDviewResult.HideLoadingPanel()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            ActionsOnControls = True
            INDviewResult.HideLoadingPanel()
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos
    ''' </summary>
    ''' <returns></returns>
    Private Function ExecuteGetList() As Task(Of ActionResult(Of String))
        tokenAsync = New CancellationTokenSource()
        Return Task.Factory.StartNew(Of ActionResult(Of String))(Function()
                                                                     Dim result As Task(Of ActionResult(Of String))

                                                                     Dim args As Object = New Dynamic.ExpandoObject()
                                                                     args.LogisticOperator = LogisticOperator
                                                                     args.OfficeType = OfficeType
                                                                     args.HabilitationCode = HabilitationCode
                                                                     args.InitialDate = INDdteInitialDate.EditValue
                                                                     args.EndDate = INDdteEndDate.EditValue

                                                                     Using model As New MDispensingByPatient("")
                                                                         result = model.GetDispensingByDateRange(args)
                                                                     End Using

                                                                     If result.Result.StateResult = False Then
                                                                         Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Result.Message}
                                                                     End If

                                                                     If Not tokenAsync.IsCancellationRequested Then
                                                                         INDgcResult.SafeInvoke(Sub()
                                                                                                    INDgcResult.DataSource = JsonConvert.DeserializeObject(Of List(Of ResultSearchObject))(result.Result.ObjectEmbbeded)
                                                                                                End Sub)
                                                                     End If

                                                                     Return New ActionResult(Of String) With {.StateResult = True}
                                                                 End Function, tokenAsync.Token)
    End Function

    ''' <summary>
    ''' Obtiene el número de ingreso seleccionado
    ''' </summary>
    Private Sub Acept()
        If INDviewResult.DataSource IsNot Nothing Then
            Dim objTemp = INDviewResult.GetFocusedRow()
            If objTemp IsNot Nothing Then
                If objTemp IsNot Nothing AndAlso objTemp.identPaciente IsNot Nothing Then
                    Dim args As New SearchMedicalOrderRecipeArgs With {.PatientIdentification = objTemp.identPaciente.ToString(), .TypeIdentification = objTemp.tipoIdenPaciente}
                    RaiseEvent ReturnMedicalOrderRecipe(Nothing, args)
                    Me.Close()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cierra el frontal
    ''' </summary>
    Private Sub Cancel()
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
        Me.Close()
    End Sub

#End Region

#Region "Handlers"

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnSearch_Click(sender As Object, e As EventArgs) Handles INDbtnSearch.Click
        Dim errors As New StringBuilder
        If INDdteInitialDate.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha inicial")
        End If
        If INDdteEndDate.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha final")
        End If
        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If
        GetListByRangeDate()
    End Sub

    ''' <summary>
    ''' Ejecuta el aceptado de un ingreso
    ''' </summary>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Me.Acept()
    End Sub

    ''' <summary>
    ''' Ejecuta el cerrado del frontal
    ''' </summary>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        Me.Cancel()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Ejecutamos el aceptar para seleccionar la orden medica
    ''' </summary>
    Private Sub INDviewResult_KeyDown(sender As Object, e As KeyEventArgs) Handles INDviewResult.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.Acept()
        End If
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Aqui retornamos el número de la orden medica del registro seleccionado
    ''' </summary>
    Private Sub INDviewResult_DoubleClick(sender As Object, e As EventArgs) Handles INDviewResult.DoubleClick
        Dim pMouse As System.Drawing.Point = Control.MousePosition
        Dim obj = Me.INDviewResult.CalcHitInfo(pMouse)
        If obj IsNot Nothing AndAlso obj.View.FocusedRowHandle >= 0 Then
            Me.Acept()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmSearch_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "Shown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        tokenAsync = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmSearch_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdteInitialDate.Focus()
    End Sub

#End Region

#End Region

End Class