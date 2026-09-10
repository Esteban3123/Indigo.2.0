'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 25-10-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports CommonEntities = Domain.Payroll.Entities
Imports Presentation.Common
#End Region

''' <summary>
''' Formulario de aprovación de eventos
''' </summary>
Public Class FrmEventApproval
    Implements IEventApproval

#Region "Fields"
    ''' <summary>
    ''' Variable para utilizar el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PEventApproval

    ''' <summary>
    ''' Objeto que contiene el listado de los schedule detail
    ''' </summary>
    ''' <remarks></remarks>
    Dim list_SchDet As List(Of ScheduleDetail)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Establece la accion en los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEventApproval.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los schedule detail que son eventos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EventScheduleDetailDataSource As List(Of ScheduleDetail) Implements IEventApproval.EventScheduleDetailDataSource
        Set(value As List(Of ScheduleDetail))
            INDgcScheduleDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el Datasource de los grupos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property GroupDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEventApproval.GroupDatasource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        list_SchDet = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEventApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PEventApproval(Me)
        CleanControls()
    End Sub

    Private Sub FrmEventApproval_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDdeDateInitial.Enabled Then
            INDdeDateInitial.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento Click para mandar a consultar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnSearch_Click(sender As Object, e As EventArgs) Handles INDBtnSearch.Click
        AsyncLoader(True)
        If INDSleFunctionalUnit.EditValue IsNot Nothing AndAlso INDdeDateEnding.EditValue > INDdeDateInitial.EditValue Then
            Dim de As Date = INDdeDateEnding.EditValue
            Dim di As Date = INDdeDateInitial.EditValue
            di = di.Date
            de = de.Date
            Using model As New MSchedule
                list_SchDet = Await model.GetScheduleDetailWithEventsAsync(INDSleFunctionalUnit.EditValue, di, de)
            End Using
            Dim list_to_datasource As List(Of ScheduleDetailHour) = New List(Of ScheduleDetailHour)
            If list_SchDet IsNot Nothing AndAlso list_SchDet.Count > 0 Then
                For Each itemS As ScheduleDetail In list_SchDet
                    For Each itemH As ScheduleDetailHour In itemS.ScheduleDetailHour.Where(Function(x) x.Event = True)
                        list_to_datasource.Add(itemH)
                    Next
                Next
                If list_to_datasource.Count > 0 Then
                    INDgcScheduleDetail.DataSource = list_to_datasource
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                INDgcScheduleDetail.DataSource = Nothing
            End If
        End If
        AsyncLoader(False)
    End Sub

    Private Function ValidateDate(list_SchDet As List(Of ScheduleDetail)) As Boolean

        Dim Count As Integer = 0
        Dim ListString As String

        For Each objScheduleDetail As ScheduleDetail In list_SchDet

            If objScheduleDetail.DateDetail < objScheduleDetail.Group.LastDateLiquidation Then

                If objScheduleDetail.ScheduleDetailHour.Any(Function(x) x.Approved = True) Then
                    If Count = 0 Then
                        ListString = objScheduleDetail.Employee.ThirdParty.Nit + " - " + objScheduleDetail.Employee.ThirdParty.Name + " Turno: " + objScheduleDetail.DateDetail.ToShortDateString()
                    Else
                        ListString = ListString + objScheduleDetail.Employee.ThirdParty.Nit + " - " + objScheduleDetail.Employee.ThirdParty.Name + " Turno: " + objScheduleDetail.DateDetail.ToShortDateString()
                    End If

                    Count += 1
                End If
            End If

        Next

        If Count = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = "A los siguientes empleados no se les puede guardar estos eventos porque este mes ya fue liquidado: " + ListString
            Return False
        End If


    End Function

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INlyEventApproval.BeginUpdate()
        INDSleFunctionalUnit.EditValue = Nothing
        INDSleFunctionalUnit.Properties.DataSource = Nothing
        INDgcScheduleDetail.DataSource = Nothing
        INDdeDateInitial.EditValue = DateTime.Now()
        INDdeDateEnding.EditValue = DateTime.Now()
        Presenter.Initializes()
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        INlyEventApproval.EndUpdate()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
#End Region

#Region "Icrud Base"
    ''' <summary>
    ''' Evento para Abrir el frm de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Evento IcrudBase para buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Buscar() Implements IcrudBase.Buscar
  
    End Sub

    ''' <summary>
    ''' Evento IcrudBase para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento IcrudBase para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Evento IcrudBase para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        AsyncLoader(True)
        If ValidateDate(list_SchDet) = False Then
            AsyncLoader(False)
            Return
        End If
        If list_SchDet IsNot Nothing AndAlso list_SchDet.Count > 0 Then
            Using model As New MSchedule
                If Await model.SaveScheduleDetailWithEventMasiveAsync(list_SchDet) = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesError)
                End If
            End Using
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento IcrudBase para logica de boton guardar o actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para establecer los mensajes dentro del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar,"")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento IcrudBase para Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub
    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

#End Region
End Class