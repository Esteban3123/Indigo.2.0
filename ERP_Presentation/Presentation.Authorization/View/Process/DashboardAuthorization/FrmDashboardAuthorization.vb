'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/05/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports IndigoSingleton
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Presentation.Controls
Imports System.Timers

#End Region

Public Class FrmDashboardAuthorization

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Authorization"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variables de sesión del EHR
    ''' </summary>
    Private _valoresSesion As IndigoValoresSesion

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsAuthorization As SettingsAuthorization

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

    ''' <summary>
    ''' Representa la entidad xpo de ingreso para cuando se consulte desde el search de la pestaña de trazabilidad
    ''' </summary>
    Private Admission As Object

    Private lastCriteriaRequests As String

    Private _timer As System.Timers.Timer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Permite identificar si el usuario es administrador, es decir puede gestionar solicitudes no asignadas
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsAdmin As Boolean
        Get
            Dim admin = False
            If BarraBotones.PermissionsForm.ContainsKey(113) Then
                admin = True
            End If
            Return admin
        End Get
    End Property

    ''' <summary>
    ''' Indica si se habilita la opción de asignación automática
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EnableAutomaticAllocation As Boolean
        Get
            Return If(Me._settingsAuthorization Is Nothing, False, Not Me._settingsAuthorization.AutomaticAllocation)
        End Get
    End Property

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

#End Region

#Region "Datasource"

    Public Property ListRequestsXpo As List(Of ViewListRequestsXpo)
        Get
            Return INDgcRequests.DataSource
        End Get
        Set(value As List(Of ViewListRequestsXpo))
            INDgcRequests.DataSource = value
        End Set
    End Property

    Public Property ListRadicatedXpo As List(Of ViewListRequestsXpo)
        Get
            Return INDgcRadicated.DataSource
        End Get
        Set(value As List(Of ViewListRequestsXpo))
            INDgcRadicated.DataSource = value
        End Set
    End Property

    Public Property ListAuthorizedXpo As List(Of ViewListRequestsXpo)
        Get
            Return INDgcAuthorized.DataSource
        End Get
        Set(value As List(Of ViewListRequestsXpo))
            INDgcAuthorized.DataSource = value
        End Set
    End Property

    Public Property ListTraceabilityXpo As List(Of ViewListRequestsTraceabilityXpo)
        Get
            Return INDgcTraceability.DataSource
        End Get
        Set(value As List(Of ViewListRequestsTraceabilityXpo))
            INDgcTraceability.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de consulta
    ''' </summary>
    Private _searchType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property SearchType As List(Of Tuple(Of Byte, String))
        Get
            If _searchType Is Nothing Then
                _searchType = New List(Of Tuple(Of Byte, String))
                _searchType.Add(New Tuple(Of Byte, String)(1, "Ingreso"))
                _searchType.Add(New Tuple(Of Byte, String)(2, "Paciente"))
            End If
            Return _searchType
        End Get
    End Property

    ''' <summary>
    ''' Tipo de documento
    ''' </summary>
    Private _documentType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property DocumentType As List(Of Tuple(Of Byte, String))
        Get
            If _documentType Is Nothing Then
                _documentType = New List(Of Tuple(Of Byte, String))
                _documentType.Add(New Tuple(Of Byte, String)(1, "CC"))
                _documentType.Add(New Tuple(Of Byte, String)(2, "CE"))
                _documentType.Add(New Tuple(Of Byte, String)(3, "TI"))
                _documentType.Add(New Tuple(Of Byte, String)(4, "RC"))
                _documentType.Add(New Tuple(Of Byte, String)(5, "PA"))
                _documentType.Add(New Tuple(Of Byte, String)(6, "AS"))
                _documentType.Add(New Tuple(Of Byte, String)(7, "MS"))
                _documentType.Add(New Tuple(Of Byte, String)(8, "NU"))
                _documentType.Add(New Tuple(Of Byte, String)(9, "CN"))
                _documentType.Add(New Tuple(Of Byte, String)(10, "CD"))
                _documentType.Add(New Tuple(Of Byte, String)(11, "SC"))
                _documentType.Add(New Tuple(Of Byte, String)(12, "PE"))
                _documentType.Add(New Tuple(Of Byte, String)(13, "PT"))
                _documentType.Add(New Tuple(Of Byte, String)(14, "DE"))
            End If
            Return _documentType
        End Get
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que abre el form para cancelar la solicitud
    ''' </summary>
    Private Sub OpenFormPostponementReasons(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmAddPostponementReasons()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 460
            formulario.ToolBar.Visible = False
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MSettingsAuthorization(Me.MyTag)
            Dim result = Await Model.GetSettingsAuthorization()
            If result.StateResult Then
                _settingsAuthorization = result.ObjectEmbbeded
                If _settingsAuthorization Is Nothing OrElse _settingsAuthorization.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        End Using
    End Function

    ''' <summary>
    ''' Carga las solicitudes
    ''' </summary>
    Private Sub LoadRequests(Optional loadData As Boolean = False)
        If INDgcRequests.DataSource IsNot Nothing And loadData = False Then
            Exit Sub
        End If
        INDviewRequests.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      Dim topRows As Integer = 5000
                                      lastCriteriaRequests = INDviewRequests.ActiveFilterString
                                      Dim filter As String = "CareCenterCode in (" & CareCenterFilters & ") and TraceabilityPaperworkStatus in (0, 1, 12)"

                                      If lastCriteriaRequests IsNot Nothing AndAlso lastCriteriaRequests <> "" Then
                                          filter = String.Format("{0} and {1}", filter, lastCriteriaRequests)
                                      End If

                                      result = Presenter.ListViewListRequests(filter, topRows)
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    If (result Is Nothing OrElse Not result.Any()) AndAlso Not String.IsNullOrEmpty(lastCriteriaRequests) Then
                                                                        INDviewRequests.ActiveFilterString = String.Empty
                                                                        LoadRequests(True)
                                                                        Exit Sub
                                                                    End If
                                                                    INDgcRequests.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                    INDviewRequests.HideLoadingPanel()
                                                                End Sub)
                                  Catch ex As Exception
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    INDviewRequests.HideLoadingPanel()
                                                                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga los radicados
    ''' </summary>
    Private Sub LoadRadicated(Optional loadData As Boolean = False)
        If INDgcRadicated.DataSource IsNot Nothing And loadData = False Then
            Exit Sub
        End If
        INDviewRadicated.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      Dim topRows As Integer = 5000

                                      Dim Filter As String = String.Empty
                                      lastCriteriaRequests = INDviewRadicated.ActiveFilterString
                                      If lastCriteriaRequests IsNot Nothing AndAlso lastCriteriaRequests <> "" Then
                                          Filter = String.Format("{0} ", lastCriteriaRequests)
                                      End If
                                      result = Presenter.ListViewListRadicated(CareCenterFilters, topRows, Filter)
                                      INDgcRadicated.BeginInvoke(Sub()
                                                                     If (result Is Nothing OrElse Not result.Any()) AndAlso Not String.IsNullOrEmpty(lastCriteriaRequests) Then
                                                                         INDviewRadicated.ActiveFilterString = String.Empty
                                                                         LoadRadicated(True)
                                                                         Exit Sub
                                                                     End If

                                                                     INDgcRadicated.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                     INDviewRadicated.HideLoadingPanel()
                                                                 End Sub)
                                  Catch ex As Exception
                                      INDgcRadicated.BeginInvoke(Sub()
                                                                     INDviewRadicated.HideLoadingPanel()
                                                                     Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                 End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga los autorizados
    ''' </summary>
    Private Sub LoadAuthorized(Optional loadData As Boolean = False)
        If INDgcAuthorized.DataSource IsNot Nothing And loadData = False Then
            Exit Sub
        End If
        INDviewAuthorized.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      Dim topRows As Integer = 5000

                                      Dim Filter As String = String.Empty
                                      lastCriteriaRequests = INDviewAuthorized.ActiveFilterString
                                      If lastCriteriaRequests IsNot Nothing AndAlso lastCriteriaRequests <> "" Then
                                          Filter = String.Format("{0} ", lastCriteriaRequests)
                                      End If

                                      result = Presenter.ListViewListAuthorized(CareCenterFilters, topRows, Filter)
                                      INDgcAuthorized.BeginInvoke(Sub()
                                                                      If (result Is Nothing OrElse Not result.Any()) AndAlso Not String.IsNullOrEmpty(lastCriteriaRequests) Then
                                                                          INDviewAuthorized.ActiveFilterString = String.Empty
                                                                          LoadAuthorized(True)
                                                                          Exit Sub
                                                                      End If

                                                                      INDgcAuthorized.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                      INDviewAuthorized.HideLoadingPanel()
                                                                  End Sub)
                                  Catch ex As Exception
                                      INDgcAuthorized.BeginInvoke(Sub()
                                                                      INDviewAuthorized.HideLoadingPanel()
                                                                      Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                  End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga la pestaña de trazabilidad
    ''' </summary>
    ''' <param name="TypeSearch"> tipo de consulta , 1-ingreso, 2-paciente</param>
    Private Sub LoadTraceability(Optional TypeSearch As Byte? = Nothing)
        If Admission Is Nothing Then
            Exit Sub
        End If

        INDgcTraceability.DataSource = Nothing
        INDviewTraceability.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsTraceabilityXpo) = Nothing
                                  Try
                                      result = Presenter.ListViewListRequestsByAdmissionNumberAndPatientCode(Admission.AdmissionCode, Admission.PatientCode, TypeSearch)
                                      INDgcTraceability.BeginInvoke(Sub()
                                                                        INDgcTraceability.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                        INDviewTraceability.HideLoadingPanel()
                                                                    End Sub)
                                  Catch ex As Exception
                                      INDgcTraceability.BeginInvoke(Sub()
                                                                        INDviewTraceability.HideLoadingPanel()
                                                                        Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                    End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga los autorizados
    ''' </summary>
    Private Sub LoadPostponement()
        If INDgcPostponement.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewPostponement.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      result = Presenter.ListViewListPostponement(CareCenterFilters)
                                      INDgcPostponement.BeginInvoke(Sub()
                                                                        INDgcPostponement.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                        INDviewPostponement.HideLoadingPanel()
                                                                    End Sub)
                                  Catch ex As Exception
                                      INDgcPostponement.BeginInvoke(Sub()
                                                                        INDviewPostponement.HideLoadingPanel()
                                                                        Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                    End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                INDgcRequests.DataSource = Nothing
                LoadRequests()
            Case INDlycgRadicated.Name 'Radicado
                INDgcRadicated.DataSource = Nothing
                LoadRadicated()
            Case INDlycgAuthorized.Name 'Autorizado
                INDgcAuthorized.DataSource = Nothing
                LoadAuthorized()
            Case INDlycgTraceability.Name 'Trazabilidad
                If Admission IsNot Nothing Then
                    LoadTraceability()
                End If
            Case INDlycgPostponement.Name 'Postergados
                INDgcPostponement.DataSource = Nothing
                LoadPostponement()
        End Select
    End Sub

    ''' <summary>
    ''' Agrega una solicitud
    ''' </summary>
    Private Sub OpenFormAddRequest()
        Using formulario As New FrmAddServices()
            AddHandler formulario.ReturnOpenItemModalArgs, AddressOf ReturnOpenItemModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 900
            formulario.Height = 700
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el control de autorizaciones
    ''' </summary>
    Private Sub OpenFormAuthorizationControl(ViewRequestsXpo As ViewListRequestsXpo)
        Using Formulario As New PopUpRequests(ViewRequestsXpo.EntityName, ViewRequestsXpo.EntityId, ViewRequestsXpo.ItemCodeOriginal)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 780
            Formulario.Height = 768
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form que visualiza las solicitudes
    ''' </summary>
    Private Sub OpenFormOpenItem(ViewRequestsXpo As ViewListRequestsXpo, stringStatus As String, Optional FromTraceability As Boolean = False)
        Using formulario As New FrmOpenItem()
            AddHandler formulario.ReturnOpenItemModalArgs, AddressOf ReturnOpenItemModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 990
            formulario.Height = 600
            formulario.ViewListRequestsXpo = ViewRequestsXpo
            formulario.FromTraceability = FromTraceability
            formulario.StringStatus = stringStatus
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form para reasignar usuario
    ''' </summary>
    Private Sub OpenFormAssignUser(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmAssignUser()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form de solicitudes a enviar al proceso de asignación automática
    ''' </summary>
    Private Sub OpenFormAutomaticAllocationDetail(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmRequestsToAutomaticAllocation(ListViewRequestsXpo)
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 900
            formulario.Height = 800
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form de cotización
    ''' </summary>
    Private Sub OpenFormRequestQuotation(ListViewRequestsXpo As List(Of ViewListRequestsXpo), status As Integer)
        ReentryItems(ListViewRequestsXpo, status, False)
    End Sub

    ''' <summary>
    ''' Método que abre el form de anexos
    ''' </summary>
    Private Sub OpenFormGenerateAnnex(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmAddAnnexes()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 700
            formulario.Height = 750
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form de eventos
    ''' </summary>
    Private Sub OpenFormAddEvent(ViewRequestsXpo As ViewListRequestsXpo, Optional TraceabilityPaperworkAnnexesId As Integer? = Nothing)
        Using formulario As New FrmAddEvent()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 900
            formulario.Height = 820
            formulario.ViewListRequestsXpo = ViewRequestsXpo
            formulario.TraceabilityPaperworkAnnexesId = TraceabilityPaperworkAnnexesId
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form de entrega al servicio
    ''' </summary>
    Private Sub OpenFormDeliverToService(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmDeliverToService()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 300
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form para remitir
    ''' </summary>
    Private Sub OpenFormRemit(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmRemit()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Guarda un reingreso
    ''' </summary>
    Private Async Sub ReentryItems(ListViewRequestsXpo As List(Of ViewListRequestsXpo), status As Integer, IsTabPostponement As Boolean)
        Dim ListTraceabilityPaperwork As New List(Of TraceabilityPaperwork)
        For Each ViewListRequestsXpo In ListViewRequestsXpo
            Dim TraceabilityPaperwork = New TraceabilityPaperwork
            With TraceabilityPaperwork
                If ViewListRequestsXpo.TraceabilityPaperworkId <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkId > 0 Then 'Si ya hay un registro se asigna el id
                    .Id = ViewListRequestsXpo.TraceabilityPaperworkId
                End If

                .AdmissionNumber = ViewListRequestsXpo.AdmissionNumber
                .Folio = ViewListRequestsXpo.Folio
                .ServiceCode = ViewListRequestsXpo.ItemCodeOriginal
                .Type = ViewListRequestsXpo.Type
                .PatientCode = ViewListRequestsXpo.PatientCode
                .CareCenterCode = ViewListRequestsXpo.CareCenterCode
                .RequestDate = ViewListRequestsXpo.RequestDate
                .RequestQuantity = ViewListRequestsXpo.Quantity
                .FunctionalUnitCode = ViewListRequestsXpo.FunctionalUnitCode
                .EntityId = ViewListRequestsXpo.EntityId
                .EntityName = ViewListRequestsXpo.EntityName
                .AssignUserCode = ViewListRequestsXpo.AssignUserCode
                .IsManual = ViewListRequestsXpo.IsManual
                .CareGroupId = ViewListRequestsXpo.CareGroupId
                .HealthAdministratorId = ViewListRequestsXpo.HealthAdministratorId
                .AuthorizationSourceId = Nothing
                If ViewListRequestsXpo.AuthorizationSourceId <> Nothing AndAlso ViewListRequestsXpo.AuthorizationSourceId > 0 Then
                    .AuthorizationSourceId = ViewListRequestsXpo.AuthorizationSourceId
                End If
                .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode
                .ServiceId = ViewListRequestsXpo.ServiceId
                .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId
                .PreviousStatus = ViewListRequestsXpo.PreviousStatus
                .Status = status

                If IsTabPostponement Then 'Si viene desde la pestaña de postergados se asigna el id del último registro de postergados
                    .TraceabilityPaperworkPostponementReasonsId = ViewListRequestsXpo.TraceabilityPaperworkPostponementReasonsId
                End If
            End With

            ListTraceabilityPaperwork.Add(TraceabilityPaperwork)
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardAuthorization("")
                Dim result = Await model.SaveTraceabilityPaperwork(ListTraceabilityPaperwork)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    INDgcPostponement.DataSource = Nothing
                    LoadPostponement()
                    LoadTraceability()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que abre el form para cancelar la solicitud
    ''' </summary>
    Private Sub OpenFormCancellationReason(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmAddCancellationReasons()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 400
            formulario.ToolBar.Visible = False
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form quede alertas
    ''' </summary>
    Private Sub OpenFormOpenAlert(ViewListRequestsXpo As ViewListRequestsXpo)
        Using formulario As New FrmOpenAlert(ViewListRequestsXpo)
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.CanSaveAlert = Me.IsAdmin OrElse (ViewListRequestsXpo.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 990
            formulario.Height = 600
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub


    ''' <summary>
    ''' Método que se ejecuta al retornar el modal de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando se abre el form modal OpenItem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnOpenItemModalArgs(sender As Object, e As OpenItemEventArgs)
        Select Case e.Action
            Case 0 'Cuando envían cero es porque se agrego un servicio y solo se actualiza la rejilla principal que tiene el foco
                BeginReloadDatasource()
            Case 1 '1.RequestQuotation

            Case 2 '2.AddEvent
                OpenFormAddEvent(e.ViewListRequestsXpo, e.TraceabilityPaperworkAnnexesId)
            Case 3 '3.CancelRequest
                OpenFormCancellationReason(e.ListViewListRequestsXpo)
            Case 4 '4.Reasign
                OpenFormAssignUser(e.ListViewListRequestsXpo)
            Case 5 '5.OpenFormGenerateAnnex
                OpenFormGenerateAnnex(e.ListViewListRequestsXpo)
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el ingreso cuando se realiza el keydown en el control de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Private Function KeyDownAdmission(code As String) As Object
        'Si viene vacío el código
        If String.IsNullOrEmpty(code) Then
            Exit Function
        End If

        'Se obtiene el ingreso
        Admission = Presenter.GetAdmissionByAdmissionNumber(code)

        'Se valida que el ingreso exista
        If Admission Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
            Admission = Nothing
            INDsleAdmission.EditValue = Nothing
            INDsleAdmission.DisplayNullText = String.Empty
            CleanControlsAdmission()
            INDsleAdmission.Focus()
            Exit Function
        End If

        'Se asigna la información a los controles
        SetAdmission(Admission)
    End Function

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdmission()
        INDTxtAdmissionCode.Text = String.Empty
        INDTxtStay.Text = String.Empty
        INDTxtPatient.Text = String.Empty
        INDTxtAdmissionDate.Text = String.Empty
        INDTxtAdmissionType.Text = String.Empty
        INDTxtAdmissionPlace.Text = String.Empty
        INDTxtLiquidationType.Text = String.Empty
        INDTxtEntity.Text = String.Empty
        INDTxtBenefitsPlan.Text = String.Empty
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtResponsibleName.Text = String.Empty
        INDTxtResponsiblePhone.Text = String.Empty
        Admission = Nothing
    End Sub

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(record As Object)
        If record IsNot Nothing Then
            Admission = record
            If INDGleSearchType.EditValue = 1 Then
                INDsleAdmission.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), Admission.AdmissionCode.ToString().Trim(), If(Admission.PatientCode Is Nothing, "", Admission.PatientCode.ToString().Trim()), If(Admission.PatientName Is Nothing, "", Admission.PatientName.ToString().Trim()))
                If record.AdmissionType IsNot Nothing Then
                    record.AdmissionTypeName = ResourceManager.GetString(String.Concat("AdmissionType", record.AdmissionType.ToString().Trim()))
                End If
                If record.AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", "Billing") Then
                    INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                If record.LiquidationType IsNot Nothing Then
                    record.LiquidationTypeName = ResourceManager.GetString(String.Concat("LiquidationType", record.LiquidationType.ToString().Trim()))
                End If
                If record.PatientCode IsNot Nothing And record.PatientName IsNot Nothing Then
                    record.PatientCodeName = record.PatientCode.ToString().Trim() + " - " + record.PatientName.ToString().Trim()
                End If
                If record.PlaceEntry IsNot Nothing Then
                    record.AdmissionPlace = ResourceManager.GetString(String.Concat("PlaceEntry", record.PlaceEntry.ToString().Trim()))
                End If
                INDsleAdmission.SetMoreInfoData(record)
            Else
                INDsleAdmission.DisplayNullText = Admission.CodeFullName
            End If

            LoadTraceability(If(INDGleSearchType.EditValue IsNot Nothing, Convert.ToByte(INDGleSearchType.EditValue), Nothing))
        End If
    End Sub

    ''' <summary>
    ''' Método que carga el datasource del control de ingreso
    ''' </summary>
    Private Sub InitializeAdmission()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.InitializeAdmission()
                                  INDsleAdmission.SafeInvoke(Sub()
                                                                 INDsleAdmission.Datasource = result
                                                             End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Metodo que inicializa los pacientes
    ''' </summary>
    Private Sub InitializePatient()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.InitializePatiens()
                                  INDslePatients.SafeInvoke(Sub()
                                                                INDslePatients.Properties.DataSource = result
                                                            End Sub)
                              End Sub)

    End Sub

#Region "Multiselect"

    ''' <summary>
    ''' Metodo que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleCheck()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                RequestVisibleCheck()
            Case INDlycgRadicated.Name 'Radicado
                RadicatedVisibleCheck()
            Case INDlycgAuthorized.Name 'Autorizado
                AuthorizedVisibleCheck()
            Case INDlycgTraceability.Name 'Trazabilidad
                TraceabilityVisibleCheck()
        End Select
    End Sub

    Private Sub RequestVisibleCheck()
        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any Then
            If ListRequestsXpo.Where(Function(item) item.SelectOption = True).Count = ListRequestsXpo.Count Then
                Me.INDviewRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
            Else
                Me.INDviewRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub RadicatedVisibleCheck()
        If ListRadicatedXpo IsNot Nothing AndAlso ListRadicatedXpo.Any Then
            If ListRadicatedXpo.Where(Function(item) item.SelectOption = True).Count = ListRadicatedXpo.Count Then
                Me.INDviewRadicated_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
            Else
                Me.INDviewRadicated_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub AuthorizedVisibleCheck()
        If ListAuthorizedXpo IsNot Nothing AndAlso ListAuthorizedXpo.Any Then
            If ListAuthorizedXpo.Where(Function(item) item.SelectOption = True).Count = ListAuthorizedXpo.Count Then
                Me.INDviewAuthorized_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
            Else
                Me.INDviewAuthorized_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub TraceabilityVisibleCheck()
        If ListTraceabilityXpo IsNot Nothing AndAlso ListTraceabilityXpo.Any Then
            If ListTraceabilityXpo.Where(Function(item) item.SelectOption = True).Count = ListTraceabilityXpo.Count Then
                Me.INDviewTraceability_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
            Else
                Me.INDviewTraceability_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(view As GridView, optionCheck As Integer, Optional selectGroupRow As Boolean = True)
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    If selectGroupRow Then
                        GetChildsRows(view, listHandlesSelected(i), optionCheck)
                    End If
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If

        VisibleCheck()
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, value As Decimal)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, value)
            Else
                Dim row As ViewListRequestsXpo = view.GetRow(childHandle)
                row.SelectOption = If(value = 0, False, True)
            End If
        Next
    End Sub

#End Region

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmDashboardAuthorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.MyTag)

        'Inicializamos la referencia
        Presenter = New PDashboardAuthorization()
        _valoresSesion = IndigoValoresSesion.Instancia

        '******************************
        AsyncLoader(True)
        Await Me.LoadParameters()
        AsyncLoader(False)

        INDsleAdmission.FuncQueryOnKeyEnterPressed = AddressOf KeyDownAdmission
        InitializeAdmission()
        InitializePatient()
        INDGLETypeDocument.DataSource = DocumentType
        'Se inicializa el Datasource de tipo de consulta
        INDGleSearchType.Properties.DataSource = SearchType
        INDGleSearchType.EditValue = 1

        'De acuerdo con los permisos se permite o no seleccionar muchos centros de atención
        INDviewSearchCareCenter.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardAuthorization_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtcgInformation.SelectedTabPageIndex = 0
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            INDsleCareCenter.Properties.DataSource = Presenter.InitializeCareCenter()
        End If
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el search de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleCareCenter.CloseUp
        'Se obtienen los códigos de centro de atención seleccionados
        CareCenterFilters = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select "'" & DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE & "'"))
        Dim NameCareCenter = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.NOMCENATE))
        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(CareCenterFilters) Then
            INDsleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        ElseIf INDviewSearchCareCenter.GetSelectedRows().Count() > 1 Then
            INDsleCareCenter.Properties.NullText = INDviewSearchCareCenter.GetSelectedRows().Count().ToString() + " Item Seleccionados"
        Else
            INDsleCareCenter.Properties.NullText = NameCareCenter.Trim()
        End If

        BeginReloadDatasource()
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInformation_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInformation.SelectedPageChanged
        If String.IsNullOrEmpty(CareCenterFilters) Then
            Exit Sub
        End If

        Select Case e.Page.Name
            Case INDlycgRequests.Name 'Solicitudes
                LoadRequests()
            Case INDlycgRadicated.Name 'Radicado
                LoadRadicated()
            Case INDlycgAuthorized.Name 'Autorizado
                LoadAuthorized()
            Case INDlycgTraceability.Name 'Trazabilidad
                INDsleAdmission.Focus()
            Case INDlycgPostponement.Name 'Postergados
                LoadPostponement()
        End Select
    End Sub

#End Region

#Region "SelectionChanged"

    Private Sub INDviewRequests_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewRequests.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListRequestsXpo IsNot Nothing Then
                For Each viewRequest In ListRequestsXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewRequests, 1, False)
    End Sub

    Private Sub INDviewRadicated_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewRadicated.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListRadicatedXpo IsNot Nothing Then
                For Each viewRequest In ListRadicatedXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewRadicated, 1, False)
    End Sub

    Private Sub INDviewAuthorized_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewAuthorized.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListAuthorizedXpo IsNot Nothing Then
                For Each viewRequest In ListAuthorizedXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewAuthorized, 1, False)
    End Sub

    Private Sub INDviewTraceability_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewTraceability.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListTraceabilityXpo IsNot Nothing Then
                For Each viewRequest In ListTraceabilityXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewTraceability, 1, False)
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para las rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewRequests.PopupMenuShowing, INDviewRadicated.PopupMenuShowing, INDviewAuthorized.PopupMenuShowing, INDviewTraceability.PopupMenuShowing, INDviewPostponement.PopupMenuShowing
        INDBbiSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiUnSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiAuthorizationControl.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiAutomaticAllocation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiRequestQuotation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiGenerateAnnex.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiAddEvent.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiDeliverToService.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiRemit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiReentry.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPostponement.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiReactivate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        Else
            Select Case INDtcgInformation.SelectedTabPageName
                Case INDlycgRequests.Name 'Solicitudes
                    PopupMenuRequest()
                Case INDlycgRadicated.Name 'Radicado
                    PopupMenuRadicated()
                Case INDlycgAuthorized.Name 'Autorizado
                    PopupMenuAuthorized()
                Case INDlycgTraceability.Name 'Trazabilidad
                    PopupMenuTraceability()
                Case INDlycgPostponement.Name 'Postergados
                    PopupMenuPostponement()
            End Select
        End If

        Dim View = CType(sender, GridView)
        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    Private Sub PopupMenuRequest()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListRequestsXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListRequestsXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
        End If

        If Me.IsAdmin Then 'si tiene permiso administrador, puede gestionar solicitudes no asignadas
            assigned = True
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'si tiene permiso de reasignar
            INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
        If EnableAutomaticAllocation AndAlso BarraBotones.PermissionsForm.ContainsKey(111) Then 'si tiene permiso de asignar automáticamente
            INDBbiAutomaticAllocation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If BarraBotones.PermissionsForm.ContainsKey(118) Then 'si tiene permiso de postergar
            INDBbiPostponement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        'Si solo se ha seleccionado uno
        If quantitySelected = 1 Then
            INDBbiAuthorizationControl.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'Si estan asignados al usuario actual
            If assigned Then
                If BarraBotones.PermissionsForm.ContainsKey(18) Then 'Si tiene permiso de abrir
                    INDBbiOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If BarraBotones.PermissionsForm.ContainsKey(98) Then 'Si tiene permiso de agregar evento
                    INDBbiAddEvent.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If BarraBotones.PermissionsForm.ContainsKey(23) Then 'Si tiene permiso de imprimir
                INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        'Si estan asignados al usuario actual
        If assigned Then
            If BarraBotones.PermissionsForm.ContainsKey(97) Then 'Si tiene permiso de solicitar cotización
                INDBbiRequestQuotation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(101) Then 'Si tiene permiso de generar anexo
                INDBbiGenerateAnnex.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
                INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    Private Sub PopupMenuRadicated()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListRadicatedXpo IsNot Nothing AndAlso ListRadicatedXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListRadicatedXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListRadicatedXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
        End If

        If Me.IsAdmin Then 'si tiene permiso administrador, puede gestionar solicitudes no asignadas
            assigned = True
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'si tiene permiso de reasignar
            INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
        If EnableAutomaticAllocation AndAlso BarraBotones.PermissionsForm.ContainsKey(111) Then 'si tiene permiso de asignar automáticamente
            INDBbiAutomaticAllocation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If BarraBotones.PermissionsForm.ContainsKey(118) Then 'si tiene permiso de postergar
            INDBbiPostponement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        'Si solo se ha seleccionado uno
        If quantitySelected = 1 Then
            INDBbiAuthorizationControl.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'Si estan asignados al usuario actual
            If assigned Then
                If BarraBotones.PermissionsForm.ContainsKey(18) Then 'Si tiene permiso de abrir
                    INDBbiOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If BarraBotones.PermissionsForm.ContainsKey(98) Then 'Si tiene permiso de agregar evento
                    INDBbiAddEvent.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If BarraBotones.PermissionsForm.ContainsKey(23) Then 'Si tiene permiso de imprimir
                INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        'Si estan asignados al usuario actual
        If assigned Then
            If BarraBotones.PermissionsForm.ContainsKey(97) Then 'Si tiene permiso de solicitar cotización
                INDBbiRequestQuotation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(101) Then 'Si tiene permiso de generar anexo
                INDBbiGenerateAnnex.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
                INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    Private Sub PopupMenuAuthorized()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListAuthorizedXpo IsNot Nothing AndAlso ListAuthorizedXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListAuthorizedXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListAuthorizedXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
        End If

        If Me.IsAdmin Then 'si tiene permiso administrador, puede gestionar solicitudes no asignadas
            assigned = True
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'si tiene permiso de reasignar
            INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
        If EnableAutomaticAllocation AndAlso BarraBotones.PermissionsForm.ContainsKey(111) Then 'si tiene permiso de asignar automáticamente
            INDBbiAutomaticAllocation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        'Si solo se ha seleccionado uno
        If quantitySelected = 1 Then
            INDBbiAuthorizationControl.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'Si estan asignados al usuario actual
            If assigned Then
                If BarraBotones.PermissionsForm.ContainsKey(18) Then 'Si tiene permiso de abrir
                    INDBbiOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If BarraBotones.PermissionsForm.ContainsKey(23) Then 'Si tiene permiso de imprimir
                INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        'Si estan asignados al usuario actual
        If assigned Then
            If BarraBotones.PermissionsForm.ContainsKey(102) Then 'Si tiene permiso de entregar al servicio
                INDBbiDeliverToService.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(112) Then 'Si tiene permiso de remitir
                INDBbiRemit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
                INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    Private Sub PopupMenuTraceability()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListTraceabilityXpo IsNot Nothing AndAlso ListTraceabilityXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListTraceabilityXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListTraceabilityXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
        End If

        If Me.IsAdmin Then 'si tiene permiso administrador, puede gestionar solicitudes no asignadas
            assigned = True
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'si tiene permiso de reasignar
            INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
        If EnableAutomaticAllocation AndAlso BarraBotones.PermissionsForm.ContainsKey(111) Then 'si tiene permiso de asignar automáticamente
            INDBbiAutomaticAllocation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If BarraBotones.PermissionsForm.ContainsKey(118) Then 'si tiene permiso de postergar
            INDBbiPostponement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If BarraBotones.PermissionsForm.ContainsKey(119) Then 'Si tiene permiso de reactivar
            INDBbiReactivate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        'Si solo se ha seleccionado uno
        If quantitySelected = 1 Then
            INDBbiAuthorizationControl.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'Si estan asignados al usuario actual
            If assigned Then
                If BarraBotones.PermissionsForm.ContainsKey(18) Then 'Si tiene permiso de abrir
                    INDBbiOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If BarraBotones.PermissionsForm.ContainsKey(23) Then 'Si tiene permiso de imprimir
                INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        'Si estan asignados al usuario actual
        If assigned Then
            If BarraBotones.PermissionsForm.ContainsKey(103) Then 'Si tiene permiso de reingresar
                INDBbiReentry.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
                INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    Private Sub PopupMenuPostponement()
        Dim assigned As Boolean = False

        If ListTraceabilityXpo IsNot Nothing AndAlso ListTraceabilityXpo.Any(Function(d) d.SelectOption) Then
            assigned = ListTraceabilityXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)
        End If

        If Me.IsAdmin Then 'si tiene permiso administrador, puede gestionar solicitudes no asignadas
            assigned = True
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'si tiene permiso de reasignar
            INDBbiReasign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If BarraBotones.PermissionsForm.ContainsKey(119) Then 'Si tiene permiso de reactivar
            INDBbiReactivate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        'Si estan asignados al usuario actual
        If assigned Then
            If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
                INDBbiCancelRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDBbiPostponement_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPostponement.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Dim message As String = "Debe seleccionar al menos una solicitud"
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgRadicated.Name 'Radicado
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = ConvertToViewListRequests(ListTraceabilityXpo.Where(Function(d) d.SelectOption AndAlso (d.TraceabilityPaperworkStatus = 1 OrElse d.TraceabilityPaperworkStatus = 2 OrElse d.TraceabilityPaperworkStatus = 3 OrElse d.TraceabilityPaperworkStatus = 4)).ToList())
                If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
                    message = message + " en estado solicitado o radicado para poder postergar"
                End If
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = message
            Exit Sub
        End If

        OpenFormPostponementReasons(ListViewRequestsXpo)
    End Sub

    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    Private Sub INDBarAddRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarAddRequest.ItemClick
        OpenFormAddRequest()
    End Sub

    Private Sub INDBbiAuthorizationControl_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAuthorizationControl.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim stringStatus As String = String.Empty
        Dim ViewRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                ViewRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                ViewRequestsXpo = {ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList().ConvertToViewListRequests.FirstOrDefault
        End Select

        If ViewRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        OpenFormAuthorizationControl(ViewRequestsXpo)
    End Sub

    Private Sub INDBbiOpen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiOpen.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim stringStatus As String = String.Empty
        Dim ViewRequestsXpo As ViewListRequestsXpo = Nothing
        Dim _fromTraceability As Boolean = False
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                stringStatus = "0, 1, 2, 3, 5, 12, 13, 15"
                ViewRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                stringStatus = "0, 1, 2, 3, 5, 12, 13, 15"
                ViewRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                stringStatus = "0, 1, 2, 3, 5, 12, 13, 15"
                ViewRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                stringStatus = "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15"
                ViewRequestsXpo = {ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList().ConvertToViewListRequests.FirstOrDefault
                _fromTraceability = True
        End Select

        If ViewRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        OpenFormOpenItem(ViewRequestsXpo, stringStatus, _fromTraceability)
    End Sub

    Private Sub INDBbiReasign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiReasign.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgRadicated.Name 'Radicado
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgAuthorized.Name 'Autorizado
                ListViewRequestsXpo = ListAuthorizedXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = (ListTraceabilityXpo.Where(Function(d) d.SelectOption).ToList()).ConvertToViewListRequests
            Case INDlycgPostponement.Name 'Postergados
                ListViewRequestsXpo = (From x In DirectCast(INDgcPostponement.FocusedView, GridView).GetSelectedRows() Select CType(DirectCast(INDgcPostponement.FocusedView, GridView).GetRow(x), ViewListRequestsXpo)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud"
            Exit Sub
        End If

        OpenFormAssignUser(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiAutomaticAllocation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAutomaticAllocation.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption AndAlso String.IsNullOrEmpty(d.AssignUserCode)).ToList()
            Case INDlycgRadicated.Name 'Radicado
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption AndAlso String.IsNullOrEmpty(d.AssignUserCode)).ToList()
            Case INDlycgAuthorized.Name 'Autorizado
                ListViewRequestsXpo = ListAuthorizedXpo.Where(Function(d) d.SelectOption AndAlso String.IsNullOrEmpty(d.AssignUserCode)).ToList()
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = (ListTraceabilityXpo.Where(Function(d) d.SelectOption AndAlso String.IsNullOrEmpty(d.AssignUserCode)).ToList()).ConvertToViewListRequests
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud sin asignar"
            Exit Sub
        End If

        OpenFormAutomaticAllocationDetail(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiRequestQuotation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRequestQuotation.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim status As Integer = Nothing
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                status = 12
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
            Case INDlycgRadicated.Name 'Radicado
                status = 13
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud asignada"
            Exit Sub
        End If

        OpenFormRequestQuotation(ListViewRequestsXpo, status)
    End Sub

    Private Sub INDBbiGenerateAnnex_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiGenerateAnnex.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
            Case INDlycgRadicated.Name 'Radicado
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud asignada"
            Exit Sub
        End If

        OpenFormGenerateAnnex(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiAddEvent_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAddEvent.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ViewRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
        End Select

        If ViewRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud asignada"
            Exit Sub
        End If

        OpenFormAddEvent(ViewRequestsXpo)
    End Sub

    Private Sub INDBbiDeliverToService_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiDeliverToService.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgAuthorized.Name 'Autorizado
                ListViewRequestsXpo = ListAuthorizedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud asignada"
            Exit Sub
        End If

        OpenFormDeliverToService(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiRemit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRemit.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgAuthorized.Name 'Autorizado
                ListViewRequestsXpo = ListAuthorizedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud asignada"
            Exit Sub
        End If

        'Se valida que hayan escogido el mismo paciente para remitir
        Dim patientCode = ListViewRequestsXpo(0).PatientCode
        If (From x In ListViewRequestsXpo Where x.PatientCode = patientCode).Count <> ListViewRequestsXpo.Count Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede remitir porque se ha seleccionado diferentes pacientes"
            Exit Sub
        End If

        OpenFormRemit(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiReentry_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiReentry.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = ConvertToViewListRequests(ListTraceabilityXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser) AndAlso d.TraceabilityPaperworkStatus = 11).ToList())
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud asignada cancelada"
            Exit Sub
        End If

        ReentryItems(ListViewRequestsXpo, 1, False)
    End Sub

    Private Sub INDBbiReactivate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiReactivate.ItemClick
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Dim message As String = "Debe seleccionar al menos una solicitud"
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = (ListTraceabilityXpo.Where(Function(d) d.SelectOption AndAlso d.TraceabilityPaperworkStatus = 16 OrElse d.TraceabilityPaperworkStatus = 11).ToList()).ConvertToViewListRequests
                If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
                    message = message + " en estado postergado para poder reactivar"
                End If
            Case INDlycgPostponement.Name 'Postergados
                ListViewRequestsXpo = (From x In DirectCast(INDgcPostponement.FocusedView, GridView).GetSelectedRows() Select CType(DirectCast(INDgcPostponement.FocusedView, GridView).GetRow(x), ViewListRequestsXpo)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = message
            Exit Sub
        End If

        ReentryItems(ListViewRequestsXpo, 1, True)
    End Sub

    Private Sub INDBbiCancelRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCancelRequest.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser) AndAlso d.TraceabilityPaperworkStatus <> 11).ToList()
            Case INDlycgRadicated.Name 'Radicado
                ListViewRequestsXpo = ListRadicatedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser) AndAlso d.TraceabilityPaperworkStatus <> 11).ToList()
            Case INDlycgAuthorized.Name 'Autorizado
                ListViewRequestsXpo = ListAuthorizedXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser) AndAlso d.TraceabilityPaperworkStatus <> 11).ToList()
            Case INDlycgTraceability.Name 'Trazabilidad
                ListViewRequestsXpo = ConvertToViewListRequests(ListTraceabilityXpo.Where(Function(d) d.SelectOption AndAlso (Me.IsAdmin OrElse d.AssignUserCode = indigo.AuditMessageWcf.CodeUser) AndAlso d.TraceabilityPaperworkStatus <> 11).ToList())
            Case INDlycgPostponement.Name 'Postergados
                ListViewRequestsXpo = (From x In DirectCast(INDgcPostponement.FocusedView, GridView).GetSelectedRows() Select CType(DirectCast(INDgcPostponement.FocusedView, GridView).GetRow(x), ViewListRequestsXpo)).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud asignada no cancelada"
            Exit Sub
        End If

        OpenFormCancellationReason(ListViewRequestsXpo)
    End Sub

    Private Sub INDBbiPrintMedicalRecord_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintMedicalRecord.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ViewListRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewListRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewListRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                ViewListRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                ViewListRequestsXpo = ConvertToViewListRequests({ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList()).FirstOrDefault
        End Select

        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoEmergentes.frmHCImpresionDialogo(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.Folio, IndigoEmergentes.frmHCImpresionDialogo.eOrigen.Impresiones, ViewListRequestsXpo.TypeClinicalHistory, ViewListRequestsXpo.CareCenterCode, _valoresSesion.EmpresaIndigo, String.Empty, String.Empty, IndigoEmergentes.frmHCImpresionDialogo.eAperturaControl.DashboardPaciente)
            Dialogo.INDAtencionInicialParto = False
            Dialogo.INDAtencionRecienNacido = False
            Dialogo.INDCodigoEmpresaRegistro = _valoresSesion.EmpresaIndigo
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintNursingRecord_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintNursingRecord.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ViewListRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewListRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewListRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                ViewListRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                ViewListRequestsXpo = ({ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList()).ConvertToViewListRequests.FirstOrDefault
        End Select

        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoeHistorias.frmHCConsultaEnfermeria(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, String.Empty, IndigoeHistorias.frmHCConsultaEnfermeria.eAperturaControl.DashboardPaciente)
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintAccountSupport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintAccountSupport.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ViewListRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewListRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewListRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                ViewListRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                ViewListRequestsXpo = ({ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList()).ConvertToViewListRequests.FirstOrDefault
        End Select

        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoeHistorias.frmHCConsultaControlCuentas(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.TypeClinicalHistory, String.Empty, IndigoeHistorias.frmHCConsultaControlCuentas.eAperturaControl.DashboardPaciente)
            Dialogo.Text = "Soporte de Cuentas"
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintOrders_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintOrders.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo As ViewListRequestsXpo = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ViewListRequestsXpo = ListRequestsXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgRadicated.Name 'Radicado
                ViewListRequestsXpo = ListRadicatedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgAuthorized.Name 'Autorizado
                ViewListRequestsXpo = ListAuthorizedXpo.FirstOrDefault(Function(d) d.SelectOption)
            Case INDlycgTraceability.Name 'Trazabilidad
                ViewListRequestsXpo = ({ListTraceabilityXpo.FirstOrDefault(Function(d) d.SelectOption)}.ToList()).ConvertToViewListRequests.FirstOrDefault
        End Select

        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Dim IsNewborn = ViewListRequestsXpo.TypeClinicalHistory = 8
        Using Dialogo As New IndigoeHistorias.frmHCListarOrdenes()
            Dialogo.INDCargar(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.Folio, ViewListRequestsXpo.TypeClinicalHistory, ViewListRequestsXpo.CareCenterCode, IsNewborn, IsNewborn, IndigoeHistorias.frmHCListarOrdenes.eAperturaControl.DashboardPaciente)
            Dialogo.Text = "Ordenes a Imprimir"
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row = CType(INDviewRequests.GetFocusedRow(), ViewListRequestsXpo)
            If row IsNot Nothing Then
                row.SelectOption = e.NewValue
                INDgcRequests.RefreshDataSource()
                VisibleCheck()
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdmission_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDsleAdmission.EditValueChanged
        If INDsleAdmission.EditValue IsNot Nothing Then
            Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
            If objTemp IsNot Nothing Then
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    SetAdmission(obj.OriginalRow)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento para cambiar el tipo de consulta 1- ingreso , 2-pacientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleSearchType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSearchType.EditValueChanged
        INDlyItemPatiens.HideControl(True)
        INDlyItemAdmission.HideControl(True)
        INDslePatients.EditValue = Nothing
        INDsleAdmission.EditValue = Nothing
        If String.IsNullOrEmpty(INDGleSearchType.EditValue) Then
            INDbtnClean.Enabled = False
            Exit Sub
        End If
        If INDGleSearchType.EditValue = 1 Then
            INDlyItemAdmission.HideControl(False)
            INDbtnClean.Enabled = True
            INDColAdmissionCode.Visible = False
        Else
            INDlyItemPatiens.HideControl(False)
            INDbtnClean.Enabled = True
            INDColAdmissionCode.VisibleIndex = 2
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se selecciona un paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePatients_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePatients.EditValueChanged
        If INDslePatients.EditValue IsNot Nothing Then
            Dim objTemp = Me.INDGvSearchPatients.GetFocusedRow
            If objTemp IsNot Nothing Then
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    SetAdmission(obj.OriginalRow)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Se ejecuta la presionar click sobre limpiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnClean_Click(sender As Object, e As EventArgs) Handles INDbtnClean.Click
        Admission = Nothing
        INDsleAdmission.EditValue = Nothing
        INDsleAdmission.DisplayNullText = String.Empty
        INDslePatients.EditValue = Nothing
        INDslePatients.Properties.NullText = String.Empty
        CleanControlsAdmission()
        INDgcTraceability.DataSource = Nothing
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcRequests_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcRequests.MouseDoubleClick
        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any() Then
            Dim hitPoint = Me.INDviewRequests.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDviewRequests_SelectOption") Then

                    Dim listFilterXpCollection = INDviewRequests.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDviewRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListRequestsXpo.Count Then
                            Me.INDviewRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcRequests.RefreshDataSource()
                    Me.INDgcRequests.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "RowCellClick"

    Private Sub INDviewRequests_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDviewRequests.RowCellClick, INDviewRadicated.RowCellClick, INDviewAuthorized.RowCellClick, INDviewTraceability.RowCellClick
        Dim view = CType(sender, GridView)
        If e.Column.FieldName = "Alert" Then
            Dim row = CType(view.GetFocusedRow(), ViewListRequestsXpo)
            OpenFormOpenAlert(row)
        End If
    End Sub

#End Region

#Region "SizeChanged"

    Private Sub INDgcRequests_SizeChanged(sender As Object, e As EventArgs) Handles INDgcRequests.SizeChanged
        If INDlyItemRequests IsNot Nothing AndAlso INDgcRequests IsNot Nothing Then
            Dim width = INDlyItemRequests.Size.Width - INDlyItemRequests.Padding.Width
            If width > INDgcRequests.Size.Width Then
                INDgcRequests.Width = width
            End If
        End If
    End Sub

    Private Sub INDgcRadicated_SizeChanged(sender As Object, e As EventArgs) Handles INDgcRadicated.SizeChanged
        If INDlyItemRadicated IsNot Nothing AndAlso INDgcRadicated IsNot Nothing Then
            Dim width = INDlyItemRadicated.Size.Width - INDlyItemRadicated.Padding.Width
            If width > INDgcRadicated.Size.Width Then
                INDgcRadicated.Width = width
            End If
        End If
    End Sub

    Private Sub INDgcAuthorized_SizeChanged(sender As Object, e As EventArgs) Handles INDgcAuthorized.SizeChanged
        If INDlyItemAuthorized IsNot Nothing AndAlso INDgcAuthorized IsNot Nothing Then
            Dim width = INDlyItemAuthorized.Size.Width - INDlyItemAuthorized.Padding.Width
            If width > INDgcAuthorized.Size.Width Then
                INDgcAuthorized.Width = width
            End If
        End If
    End Sub

    Private Sub INDgcTraceability_SizeChanged(sender As Object, e As EventArgs) Handles INDgcTraceability.SizeChanged
        If INDlyItemTraceability IsNot Nothing AndAlso INDgcTraceability IsNot Nothing Then
            Dim width = INDlyItemTraceability.Size.Width - INDlyItemTraceability.Padding.Width
            If width > INDgcTraceability.Size.Width Then
                INDgcTraceability.Width = width
            End If
        End If
    End Sub


    Private Sub INDviewRequests_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDviewRequests.ColumnFilterChanged, INDviewAuthorized.ColumnFilterChanged, INDviewRadicated.ColumnFilterChanged
        If lastCriteriaRequests <> DirectCast(sender, GridView).ActiveFilterString Then
            If _timer Is Nothing OrElse Not _timer.Enabled Then
                _timer = New System.Timers.Timer(2000)
                AddHandler _timer.Elapsed, AddressOf OnTimedEvent
                _timer.AutoReset = False
                _timer.Enabled = True
            End If
        End If
    End Sub


    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                LoadRequests(True)
            Case INDlycgAuthorized.Name 'Autorizado
                LoadAuthorized(True)
            Case INDlycgRadicated.Name 'Radicado
                LoadRadicated(True)
        End Select
        _timer.Stop()
        _timer.Enabled = False
    End Sub


#End Region

#End Region

End Class