'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Common
Imports Presentation.Common
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Reporter
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class FrmMonthlyAmortization
    Implements IMonthlyAmortization

#Region "Properties"

    ''' <summary>
    ''' Definicion de layouts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMonthlyAmortization.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IMonthlyAmortization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador de amortizacion mensual
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PMonthlyAmortization

    ''' <summary>
    ''' Representa el listado de causaciones diferidas
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeferredCausationShare As List(Of DeferredCausationShare)

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ListDeferredCausationShare = Nothing
        INDgcDeferredCausation.DataSource = Nothing
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Metodo que carga la informacion a la rejilla segun los parametros escogidos
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadInformation() As Task
        Dim ban As Integer = 0
        If ListDeferredCausationShare IsNot Nothing Then
            Dim cont As Integer = ValidateSelectedItems(ListDeferredCausationShare)
            If cont > 0 Then
                If MessageIndigo.Show(ResourceManager.GetString("SelectedItems", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    ban = 1
                End If
            End If
        End If

        If ban = 0 Then
            Using model As New MMonthlyAmortization("")
                Dim resultDeferredCausation As ActionResult(Of List(Of DeferredCausationShare))
                resultDeferredCausation = Await model.GetDeferredCausationByDate(CtrDateNavigator1.GetYear, CtrDateNavigator1.GetMonth)
                If resultDeferredCausation.StateResult Then

                    ListDeferredCausationShare = resultDeferredCausation.ObjectEmbbeded

                    INDgcDeferredCausation.DataSource = Nothing
                    INDgcDeferredCausation.DataSource = ListDeferredCausationShare
                Else
                    ListDeferredCausationShare = Nothing
                    INDgcDeferredCausation.DataSource = Nothing
                    Dim nameMonth As String = GetMonthOfControl(CtrDateNavigator1.GetMonth)
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("DontDeferredCausation", NAME_MODULE), nameMonth, CtrDateNavigator1.GetYear)
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que confirma las causaciones diferidas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function Confirm() As Task
        If ListDeferredCausationShare IsNot Nothing Then
            Dim cont As Integer = ValidateSelectedItems(ListDeferredCausationShare)
            If cont = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontSelectedItems", NAME_MODULE)
                Exit Function
            End If

            Try
                Using model As New MMonthlyAmortization(Tag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmMonthlyAmortization(ListDeferredCausationShare, BarraBotones.OperatingUnit.Id)
                    AsyncLoader(False)
                    If Result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentConfirmSatisfactory", NAME_MODULE), Result.Message, Result.MessageResult(0))
                        Dim listId = (From tp In CType(INDgcDeferredCausation.DataSource, List(Of Domain.Entities.DeferredCausationShare)) Where tp.Amortized = True Select tp.Id).ToList
                        If MessageIndigo.Show(ResourceManager.GetString("PrintReport", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            Dim reportDef As New Reporter.rptMonthlyAmortization()
                            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listId)
                        End If
                        Deshacer()
                    Else
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontItemsAmortized", NAME_MODULE)
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateSelectedItems(ByVal _listDeferred As List(Of DeferredCausationShare)) As Integer
        Dim cont As Integer = ListDeferredCausationShare.FindAll(Function(item) item.Amortized = True).Cast(Of DeferredCausationShare).ToList().Count
        Return cont
    End Function

    ''' <summary>
    ''' Genera los mensajes de error.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <remarks></remarks>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' Obtiene el nombre del mes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMonthOfControl(ByVal month As Integer) As String
        Dim nameMonth As String = ""
        Select Case month
            Case 1
                nameMonth = "Enero"
            Case 2
                nameMonth = "Febrero"
            Case 3
                nameMonth = "Marzo"
            Case 4
                nameMonth = "Abril"
            Case 5
                nameMonth = "Mayo"
            Case 6
                nameMonth = "Junio"
            Case 7
                nameMonth = "Julio"
            Case 8
                nameMonth = "Agosto"
            Case 9
                nameMonth = "Septiembre"
            Case 10
                nameMonth = "Octubre"
            Case 11
                nameMonth = "Noviembre"
            Case 12
                nameMonth = "Diciembre"
        End Select
        Return nameMonth
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListDeferredCausationShare = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando carga un formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMonthlyAmortization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PMonthlyAmortization(Me)
        IndigoGridControl1.RefreshGrid(INDgcDeferredCausation)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de cargar datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnLoadData_Click(sender As Object, e As EventArgs) Handles INDbtnLoadData.Click
        LoadInformation()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Actualiza los permisos de la barra cuando carga.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await Confirm()
    End Sub

#End Region

End Class