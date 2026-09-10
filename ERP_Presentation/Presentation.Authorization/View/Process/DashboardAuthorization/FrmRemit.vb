'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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
Imports System.Text
Imports Presentation.Authorization.MVP
Imports DevExpress.Xpo
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraEditors.BaseCheckedListBoxControl
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Authorization
Imports Infrastructure.Data.Xpo
Imports System.Dynamic
Imports System.Collections.Concurrent
Imports Presentation.Billing.MVP
Imports Presentation.Billing

#End Region

Public Class FrmRemit

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Entidad que se envia desde los diferentes procesos
    ''' </summary>
    Public ListViewListRequestsXpo As List(Of ViewListRequestsXpo)

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda una reasignación de usuarios
    ''' </summary>
    Private Async Sub OpenFormAuthorizationOutsourcedServices()
        If INDsleCareGroup.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un grupo de atención"
            Exit Sub
        End If

        AsyncLoader(True)

        'Se construye el objeto que se envía para los servicios de homologación
        Dim arg As Object = GenerateSendData()

        'Se consultan las homologaciones
        Using model As New MControlOutpatientServices(Me.Tag)
            Dim result As ActionResult(Of List(Of List(Of CupsHomologation))) = Await model.GetHomologationsCups(arg, CInt(INDsleCareGroup.EditValue))

            If Not result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                Dim formulario As New FrmHomologationsCups
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.ListHomologation = result.ObjectEmbbeded
                formulario.arguments = arg
                formulario.CanClose = False
                Dim transParent As New FrmTransparent(formulario, False)
                transParent.ShowDialog(Me)
            ElseIf result.StateResult AndAlso result.ObjectEmbbeded.Count > 0 Then
                Dim args As New HomologationCupsEventArgs
                Dim listHomologationReturn As New List(Of List(Of CupsHomologation))
                result.ObjectEmbbeded.ForEach(Sub(o)
                                                  o(0).Activated = True
                                                  If o.Count = 1 Then
                                                      listHomologationReturn.Add(o)
                                                  Else
                                                      listHomologationReturn.Add(o.Where(Function(x) x.Activated = True).ToList())
                                                  End If
                                              End Sub)
                args.ListHomologations = listHomologationReturn
                args.arguments = arg
                ReturnSetHomologation(Nothing, args)
            Else
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        End Using
    End Sub

    Private Async Sub ReturnSetHomologation(sender As Object, e As HomologationCupsEventArgs)
        Dim arg As Object = e.arguments
        arg.AdmissionNumber = ""
        arg.PatientCode = ListViewListRequestsXpo(0).PatientCode
        arg.CareCenterCode = ListViewListRequestsXpo(0).CareCenterCode
        arg.OperativeUnitId = 0
        arg.IsProcedureQx = False
        arg.AutorizationNumber = ""

        Using m As New MAccountControl(Me.Tag)
            Dim res As ActionResult(Of ServiceOrder) = Await m.GetServiceOrderDetailHomologation(CInt(INDsleCareGroup.EditValue), e.ListHomologations, arg)

            If res.StateResult Then
                'Se abre el formulario de autorización servicios tercerizados
                Using formulario As New FrmAuthorizationOutsourcedServices()
                    'AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
                    formulario.ViewModeEditHold = True
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.Width = 900
                    formulario.Height = 800
                    formulario.IsDashboard = True
                    formulario.ListTraceabilityPaperworkIds = (From x In ListViewListRequestsXpo Select x.TraceabilityPaperworkId).ToList()
                    formulario.CareGroupId = INDsleCareGroup.EditValue
                    formulario.CareGroupCodeName = INDsleCareGroup.Text
                    formulario.ListServiceOrderDetail = res.ObjectEmbbeded.ServiceOrderDetail.ToList()
                    formulario.PatientThirdPartyId = ListViewListRequestsXpo(0).PatientThirdPartyId
                    formulario.PatientThirdPartyCodeName = ListViewListRequestsXpo(0).PatientCode.Trim + " - " + ListViewListRequestsXpo(0).PatientName.Trim
                    Dim frm As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    frm.ShowDialog(Me)

                    AsyncLoader(False)
                    If formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                        RaiseEvent ReturnModalArgs(Nothing, Nothing)
                        Me.Close()
                    End If
                End Using
            Else
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Genera el objeto que se envia a los servicios de homologación
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateSendData() As Object
        Dim arg As Object = New ExpandoObject()
        'AsyncLoader(True)
        Dim myListDetail As New ConcurrentBag(Of Object)()
        Parallel.ForEach(ListViewListRequestsXpo, Sub(obj As ViewListRequestsXpo)
                                                      Dim detail As Object = New ExpandoObject()
                                                      detail.CupsEntityCode = obj.ServiceCode
                                                      'detail.TipoSolicitud = obj.TipoSolicitud '1 - Cita Medica; 2 - Apoyo Dx; 3 - Tratamiento Especial
                                                      detail.FunctionalUnitCode = obj.FunctionalUnitCode
                                                      detail.ProfessionalCode = obj.ProfessionalCode
                                                      detail.Date = obj.RequestDate
                                                      detail.ProfessionalSpecialistCode = ""
                                                      detail.PatientCode = obj.PatientCode
                                                      detail.Quantity = obj.Quantity
                                                      detail.NitMedico = obj.ProfessionalCode
                                                      If obj.ContractDescriptionId IsNot Nothing AndAlso obj.ContractDescriptionId > 0 Then
                                                          detail.ContractDescriptionId = obj.ContractDescriptionId
                                                      End If
                                                      'detail.IdCita = obj.Codigo
                                                      'detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                                      'detail.ClaseCita = obj.TipoCita
                                                      myListDetail.Add(detail)
                                                  End Sub)
        arg.Details = myListDetail
        Return arg
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRemit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardAuthorization()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de plantilla de turnos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(985, Nothing, True)
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRemit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareGroup.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        OpenFormAuthorizationOutsourcedServices()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRemit_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleCareGroup.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAccept.Focus()
        End If
    End Sub

#End Region

#End Region

End Class