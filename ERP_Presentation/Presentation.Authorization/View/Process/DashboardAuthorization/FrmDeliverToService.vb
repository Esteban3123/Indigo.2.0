'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2020
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
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.XtraSplashScreen
Imports Presentation.Billing.MVP

#End Region

Public Class FrmDeliverToService

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
    Private Async Sub Guardar()
        Dim errors As New StringBuilder

        If INDsleCareCenter.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
            errors.AppendLine("Seleccione un centro de atención")
        End If

        If INDsleFunctionalUnit.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una unidad funcional")
        End If

        If errors.ToString.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If

        Dim ListTraceabilityPaperwork As New List(Of TraceabilityPaperwork)
        For Each ViewListRequestsXpo In ListViewListRequestsXpo
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
                .IsManual = ViewListRequestsXpo.IsManual
                .CareGroupId = ViewListRequestsXpo.CareGroupId
                .HealthAdministratorId = ViewListRequestsXpo.HealthAdministratorId
                .AuthorizationSourceId = Nothing
                If ViewListRequestsXpo.AuthorizationSourceId <> Nothing AndAlso ViewListRequestsXpo.AuthorizationSourceId > 0 Then
                    .AuthorizationSourceId = ViewListRequestsXpo.AuthorizationSourceId
                End If
                .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode
                .AssignUserCode = If(ViewListRequestsXpo.AssignUser IsNot Nothing, ViewListRequestsXpo.AssignUser.Split(" - ")(0).ToString(), Nothing)

                .CareCenterTargetCode = INDsleCareCenter.EditValue
                .FunctionalUnitTargetId = INDsleFunctionalUnit.EditValue
                .ServiceId = ViewListRequestsXpo.ServiceId
                .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId
                .PreviousStatus = If(ViewListRequestsXpo.TraceabilityPaperworkStatus = 0, 1, ViewListRequestsXpo.TraceabilityPaperworkStatus)

                .Status = 6
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
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDeliverToService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        Presenter = New PDashboardAuthorization()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDeliverToService_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDeliverToService_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDeliverToService_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el botón plus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
                Using model As New MControlOutpatientServices("")
                    INDsleFunctionalUnit.Properties.DataSource = model.ListFunctionalUnitCareCenter(INDsleCareCenter.EditValue)
                End Using
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                INDsleCareCenter.Properties.DataSource = model.ListCentersHIS()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If INDsleFunctionalUnit.Properties.DataSource Is Nothing AndAlso INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            Using model As New MControlOutpatientServices("")
                INDsleFunctionalUnit.Properties.DataSource = model.ListAllFunctionalUnitCareCenter(INDsleCareCenter.EditValue)
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambia el valor del control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        If INDsleCareCenter.EditValue IsNot Nothing AndAlso String.IsNullOrEmpty(INDsleCareCenter.EditValue) = False Then
            INDsleFunctionalUnit.EditValue = Nothing
            INDsleFunctionalUnit.Properties.DataSource = Nothing
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Guardar()
    End Sub

#End Region

#End Region

End Class