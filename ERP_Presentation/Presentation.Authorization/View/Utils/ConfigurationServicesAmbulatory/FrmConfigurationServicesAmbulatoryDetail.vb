'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2020
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
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class FrmConfigurationServicesAmbulatoryDetail

#Region "Public Event"

    Public Event EventDetail()

#End Region

#Region "Properties"

    Private WriteOnly Property SetReadOnlyControls()
        Set(value)
            INDseAssignmentException.Properties.ReadOnly = Not value
            INDseRequestException.Properties.ReadOnly = Not value
            INDseRadicatedException.Properties.ReadOnly = Not value
            INDseDeliveryServiceException.Properties.ReadOnly = Not value
            INDsleAssignmentUnitException.Properties.ReadOnly = Not value
            INDsleRequestUnitException.Properties.ReadOnly = Not value
            INDsleRadicatedUnitException.Properties.ReadOnly = Not value
            INDsleDeliveryServiceUnitException.Properties.ReadOnly = Not value
        End Set
    End Property

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

#Region "Variables"

    Dim Presenter As PConfigurationServicesAmbulatory

    Dim ListConfigurationServicesAmbulatoryExceptions As List(Of ConfigurationServicesAmbulatoryExceptions)

    Dim ListDeleteConfigurationServicesAmbulatoryExceptions As List(Of ConfigurationServicesAmbulatoryExceptions)

    Dim ConfigurationServicesAmbulatoryExceptions As ConfigurationServicesAmbulatoryExceptions

    Public ListPortfolioCUPSEntityIds As List(Of Integer)

    Public ListPortfolioInventoryProductIds As List(Of Integer)

    Public ConfigurationServicesAmbulatoryId As Integer

    Dim ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory

#End Region

#Region "Methods"

    Private Sub LoadControls()
        Try
            AsyncLoader(True)

            Dim entityXpo = Presenter.GetConfigurationServicesAmbulatoryById(ConfigurationServicesAmbulatoryId)

            With entityXpo
                INDseAssignment.EditValue = .Assignment
                INDseRequest.EditValue = .Request
                INDseRadicated.EditValue = .Radicated
                INDseDeliveryService.EditValue = .DeliveryService

                INDsleAssignmentUnit.EditValue = .AssignmentUnit
                INDsleRequestUnit.EditValue = .RequestUnit
                INDsleRadicatedUnit.EditValue = .RadicatedUnit
                INDsleDeliveryServiceUnit.EditValue = .DeliveryServiceUnit

                If .ConfigurationServicesAmbulatoryExceptionsXpo IsNot Nothing AndAlso .ConfigurationServicesAmbulatoryExceptionsXpo.Count > 0 Then
                    ListConfigurationServicesAmbulatoryExceptions = New List(Of ConfigurationServicesAmbulatoryExceptions)

                    For Each item In .ConfigurationServicesAmbulatoryExceptionsXpo
                        Dim exception As New ConfigurationServicesAmbulatoryExceptions
                        exception.Id = item.Id
                        exception.ConfigurationServicesAmbulatoryId = item.ConfigurationServicesAmbulatoryId.Id
                        exception.CareGroupId = item.CareGroupId.Id
                        exception.CareGroupCodeName = item.CareGroupId.CodeName
                        exception.SusceptibleAuthorization = item.SusceptibleAuthorization
                        exception.Assignment = item.Assignment
                        exception.AssignmentUnit = item.AssignmentUnit
                        exception.Request = item.Request
                        exception.RequestUnit = item.RequestUnit
                        exception.Radicated = item.Radicated
                        exception.RadicatedUnit = item.RadicatedUnit
                        exception.DeliveryService = item.DeliveryService
                        exception.DeliveryServiceUnit = item.DeliveryServiceUnit

                        ListConfigurationServicesAmbulatoryExceptions.Add(exception)
                    Next

                    INDgcExceptions.DataSource = Nothing
                    INDgcExceptions.DataSource = ListConfigurationServicesAmbulatoryExceptions
                End If
            End With

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub AddException()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If ConfigurationServicesAmbulatoryExceptions Is Nothing Then
            Dim listCareGroup = (From x In INDviewSearchCareGroup.GetSelectedRows() Select CType(INDviewSearchCareGroup.GetRow(x), ContractCareGroupXpo)).ToList()

            If ListConfigurationServicesAmbulatoryExceptions Is Nothing Then
                ListConfigurationServicesAmbulatoryExceptions = New List(Of ConfigurationServicesAmbulatoryExceptions)
            Else
                Dim listIds = (From x In listCareGroup Select x.Id).ToList()
                Dim listRepeat = (From x In ListConfigurationServicesAmbulatoryExceptions Where listIds.Contains(x.CareGroupId) Select x).ToList()
                If listRepeat IsNot Nothing AndAlso listRepeat.Count > 0 Then
                    Dim message As New StringBuilder
                    listRepeat.ForEach(Sub(x) message.AppendLine("El grupo de atención " + x.CareGroupCodeName + " ya existe en la lista"))
                    Mensaje(EeventViewerImages.Advertencia) = message.ToString()
                    Exit Sub
                End If
            End If

            For Each item In listCareGroup
                Dim entity As New ConfigurationServicesAmbulatoryExceptions
                With entity
                    .CareGroupId = item.Id
                    .CareGroupCodeName = item.CodeName
                    .SusceptibleAuthorization = INDsleSusceptibleAuthorization.EditValue
                    If INDsleSusceptibleAuthorization.EditValue Then
                        .Assignment = CInt(INDseAssignmentException.EditValue)
                        .AssignmentUnit = CInt(INDsleAssignmentUnitException.EditValue)
                        .Request = CInt(INDseRequestException.EditValue)
                        .RequestUnit = CInt(INDsleRequestUnitException.EditValue)
                        .Radicated = CInt(INDseRadicatedException.EditValue)
                        .RadicatedUnit = CInt(INDsleRadicatedUnitException.EditValue)
                        .DeliveryService = CInt(INDseDeliveryServiceException.EditValue)
                        .DeliveryServiceUnit = CInt(INDsleDeliveryServiceUnitException.EditValue)
                    Else
                        .Assignment = Nothing
                        .AssignmentUnit = Nothing
                        .Request = Nothing
                        .RequestUnit = Nothing
                        .Radicated = Nothing
                        .RadicatedUnit = Nothing
                        .DeliveryService = Nothing
                        .DeliveryServiceUnit = Nothing
                    End If
                End With
                ListConfigurationServicesAmbulatoryExceptions.Add(entity)
            Next
        Else
            With ConfigurationServicesAmbulatoryExceptions
                .SusceptibleAuthorization = INDsleSusceptibleAuthorization.EditValue
                If INDsleSusceptibleAuthorization.EditValue Then
                    .Assignment = CInt(INDseAssignmentException.EditValue)
                    .AssignmentUnit = CInt(INDsleAssignmentUnitException.EditValue)
                    .Request = CInt(INDseRequestException.EditValue)
                    .RequestUnit = CInt(INDsleRequestUnitException.EditValue)
                    .Radicated = CInt(INDseRadicatedException.EditValue)
                    .RadicatedUnit = CInt(INDsleRadicatedUnitException.EditValue)
                    .DeliveryService = CInt(INDseDeliveryServiceException.EditValue)
                    .DeliveryServiceUnit = CInt(INDsleDeliveryServiceUnitException.EditValue)
                Else
                    .Assignment = Nothing
                    .AssignmentUnit = Nothing
                    .Request = Nothing
                    .RequestUnit = Nothing
                    .Radicated = Nothing
                    .RadicatedUnit = Nothing
                    .DeliveryService = Nothing
                    .DeliveryServiceUnit = Nothing
                End If
            End With
        End If

        INDgcExceptions.DataSource = Nothing
        INDgcExceptions.DataSource = ListConfigurationServicesAmbulatoryExceptions
        Mensaje(EeventViewerImages.Informacion) = "Excepciones agregadas correctamente"
        CleanControlsPopup()
        INDsleCareGroup.Focus()
    End Sub

    Private Sub EditException()
        ConfigurationServicesAmbulatoryExceptions = DirectCast(INDviewExceptions.GetFocusedRow(), ConfigurationServicesAmbulatoryExceptions)

        With ConfigurationServicesAmbulatoryExceptions
            INDsleCareGroup.EditValue = .CareGroupId
            INDsleCareGroup.Properties.NullText = "1 Item Seleccionados"
            INDsleCareGroup.Properties.ReadOnly = True
            INDsleSusceptibleAuthorization.EditValue = .SusceptibleAuthorization
            INDseAssignmentException.EditValue = .Assignment
            INDsleAssignmentUnitException.EditValue = .AssignmentUnit
            INDseRequestException.EditValue = .Request
            INDsleRequestUnitException.EditValue = .RequestUnit
            INDseRadicatedException.EditValue = .Radicated
            INDsleRadicatedUnitException.EditValue = .RadicatedUnit
            INDseDeliveryServiceException.EditValue = .DeliveryService
            INDsleDeliveryServiceUnitException.EditValue = .DeliveryServiceUnit
        End With

        INDpceExceptions.ShowPopup()
    End Sub

    Private Sub DeleteException()
        Dim entity = DirectCast(INDviewExceptions.GetFocusedRow(), ConfigurationServicesAmbulatoryExceptions)
        If entity IsNot Nothing AndAlso entity.Id > 0 Then
            If ListDeleteConfigurationServicesAmbulatoryExceptions Is Nothing Then
                ListDeleteConfigurationServicesAmbulatoryExceptions = New List(Of ConfigurationServicesAmbulatoryExceptions)
            End If
            ListDeleteConfigurationServicesAmbulatoryExceptions.Add(entity)
        End If

        ListConfigurationServicesAmbulatoryExceptions.Remove(entity)
        INDgcExceptions.DataSource = Nothing
        INDgcExceptions.DataSource = ListConfigurationServicesAmbulatoryExceptions
    End Sub

    Private Function ValidateControlsForm() As String
        Dim listErrors As New StringBuilder

        If INDseAssignment.EditValue Is Nothing Then
            listErrors.AppendLine("Debe ingresar un esfuerzo")
        End If
        If INDsleAssignmentUnit.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una unidad de esfuerzo")
        End If

        If INDseRequest.EditValue Is Nothing Then
            listErrors.AppendLine("Debe ingresar una solicitud")
        End If
        If INDsleRequestUnit.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una unidad de solicitud")
        End If

        If INDseRadicated.EditValue Is Nothing Then
            listErrors.AppendLine("Debe ingresar un radicado")
        End If
        If INDsleRequestUnit.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una unidad de radicado")
        End If

        If INDseDeliveryService.EditValue Is Nothing Then
            listErrors.AppendLine("Debe ingresar una entrega al servicio")
        End If
        If INDsleDeliveryServiceUnit.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una unidad de entrega al servicio")
        End If

        Return listErrors.ToString()
    End Function

    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder

        If ConfigurationServicesAmbulatoryExceptions Is Nothing Then
            Dim listCareGroup = (From x In INDviewSearchCareGroup.GetSelectedRows() Select CType(INDviewSearchCareGroup.GetRow(x), ContractCareGroupXpo)).ToList()
            If listCareGroup Is Nothing OrElse listCareGroup.Count = 0 Then
                listErrors.AppendLine("Debe seleccionar un grupo de atención")
            End If
        End If

        If INDsleSusceptibleAuthorization.EditValue Then
            If INDseAssignmentException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar una asignación")
            End If
            If INDsleAssignmentUnitException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una unidad de asignación")
            End If

            If INDseRequestException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar una solicitud")
            End If
            If INDsleRequestUnitException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una unidad de solicitud")
            End If

            If INDseRadicatedException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar un radicado")
            End If
            If INDsleRequestUnitException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una unidad de radicado")
            End If

            If INDseDeliveryServiceException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar una entrega al servicio")
            End If
            If INDsleDeliveryServiceUnitException.EditValue Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una unidad de entrega al servicio")
            End If
        End If

        Return listErrors.ToString()
    End Function

    Private Sub InitializeTuples()
        Dim listYesNot As New List(Of Tuple(Of Boolean, String))
        listYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleSusceptibleAuthorization.Properties.DataSource = listYesNot.ToList()

        Dim listUnit As New List(Of Tuple(Of Integer, String))
        listUnit.Add(New Tuple(Of Integer, String)(1, "Minutos"))
        listUnit.Add(New Tuple(Of Integer, String)(2, "Horas"))
        listUnit.Add(New Tuple(Of Integer, String)(3, "Días"))
        INDsleAssignmentUnit.Properties.DataSource = listUnit.ToList()
        INDsleAssignmentUnitException.Properties.DataSource = listUnit.ToList()
        INDsleRequestUnit.Properties.DataSource = listUnit.ToList()
        INDsleRequestUnitException.Properties.DataSource = listUnit.ToList()
        INDsleRadicatedUnit.Properties.DataSource = listUnit.ToList()
        INDsleRadicatedUnitException.Properties.DataSource = listUnit.ToList()
        INDsleDeliveryServiceUnit.Properties.DataSource = listUnit.ToList()
        INDsleDeliveryServiceUnitException.Properties.DataSource = listUnit.ToList()
    End Sub

    Private Sub Deshacer()
        INDseAssignment.EditValue = Nothing
        INDseRequest.EditValue = Nothing
        INDseRadicated.EditValue = Nothing
        INDseDeliveryService.EditValue = Nothing

        INDsleAssignmentUnit.EditValue = Nothing
        INDsleRequestUnit.EditValue = Nothing
        INDsleRadicatedUnit.EditValue = Nothing
        INDsleDeliveryServiceUnit.EditValue = Nothing

        ListConfigurationServicesAmbulatoryExceptions = Nothing
        ListDeleteConfigurationServicesAmbulatoryExceptions = Nothing

        CleanControlsPopup()
    End Sub

    Private Sub CleanControlsPopup()
        INDsleCareGroup.EditValue = Nothing
        INDsleCareGroup.Properties.NullText = "0 Item Seleccionados"
        INDsleCareGroup.Properties.ReadOnly = False
        INDsleSusceptibleAuthorization.EditValue = False

        INDseAssignmentException.EditValue = Nothing
        INDseRequestException.EditValue = Nothing
        INDseRadicatedException.EditValue = Nothing
        INDseDeliveryServiceException.EditValue = Nothing

        INDsleAssignmentUnitException.EditValue = Nothing
        INDsleRequestUnitException.EditValue = Nothing
        INDsleRadicatedUnitException.EditValue = Nothing
        INDsleDeliveryServiceUnitException.EditValue = Nothing

        ConfigurationServicesAmbulatoryExceptions = Nothing

        Dim listRowHandles = INDviewSearchCareGroup.GetSelectedRows()
        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                INDviewSearchCareGroup.UnselectRow(listRowHandles(i))
            Next
        End If
    End Sub

    Private Sub AssigningValues()
        ConfigurationServicesAmbulatory = New ConfigurationServicesAmbulatory
        With ConfigurationServicesAmbulatory
            If ConfigurationServicesAmbulatoryId <> Nothing AndAlso ConfigurationServicesAmbulatoryId > 0 Then
                .Id = ConfigurationServicesAmbulatoryId
            End If

            .Assignment = CInt(INDseAssignment.EditValue)
            .Request = CInt(INDseRequest.EditValue)
            .Radicated = CInt(INDseRadicated.EditValue)
            .DeliveryService = CInt(INDseDeliveryService.EditValue)

            .AssignmentUnit = CInt(INDsleAssignmentUnit.EditValue)
            .RequestUnit = CInt(INDsleRequestUnit.EditValue)
            .RadicatedUnit = CInt(INDsleRadicatedUnit.EditValue)
            .DeliveryServiceUnit = CInt(INDsleDeliveryServiceUnit.EditValue)

            If ListConfigurationServicesAmbulatoryExceptions IsNot Nothing AndAlso ListConfigurationServicesAmbulatoryExceptions.Count > 0 Then
                ListConfigurationServicesAmbulatoryExceptions.ForEach(Sub(item) .ConfigurationServicesAmbulatoryExceptions.Add(item))
            End If

            If ListDeleteConfigurationServicesAmbulatoryExceptions IsNot Nothing AndAlso ListDeleteConfigurationServicesAmbulatoryExceptions.Count > 0 Then
                ListDeleteConfigurationServicesAmbulatoryExceptions.ForEach(Sub(item) .ConfigurationServicesAmbulatoryExceptions.Add(item.MarkAsDeleted()))
            End If
        End With
    End Sub

    Private Async Sub Guardar()
        Dim errors = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MConfigurationServicesAmbulatory(Me.Tag.ToString())
                Dim result = Await model.SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds, ListPortfolioInventoryProductIds)
                If result.StateResult Then
                    If ConfigurationServicesAmbulatoryId = Nothing OrElse ConfigurationServicesAmbulatoryId = 0 Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                    RaiseEvent EventDetail()
                    Me.Close()
                Else
                    Me.AsyncLoader(False)
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

    Private Sub FrmConfigurationServicesAmbulatoryDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PConfigurationServicesAmbulatory()
        InitializeTuples()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewExceptions, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewExceptions.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        If ConfigurationServicesAmbulatoryId <> Nothing AndAlso ConfigurationServicesAmbulatoryId > 0 Then
            LoadControls()
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmConfigurationServicesAmbulatoryDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDseAssignment.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDpceExceptions_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceExceptions.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceExceptions.ShowPopup()
        End If
    End Sub

    Private Sub FrmConfigurationServicesAmbulatoryDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceExceptions_Popup(sender As Object, e As EventArgs) Handles INDpceExceptions.Popup
        INDsleCareGroup.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddException_Click(sender As Object, e As EventArgs) Handles INDbtnAddException.Click
        AddException()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            INDsleCareGroup.Properties.DataSource = Presenter.InitializeCareGroup()
        End If
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDsleCareGroup_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleCareGroup.CloseUp
        INDsleCareGroup.Properties.NullText = INDviewSearchCareGroup.GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    Private Sub INDpceExceptions_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceExceptions.CloseUp
        If ConfigurationServicesAmbulatoryExceptions IsNot Nothing Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleSusceptibleAuthorization_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSusceptibleAuthorization.EditValueChanged
        SetReadOnlyControls = INDsleSusceptibleAuthorization.EditValue
        If INDsleSusceptibleAuthorization.EditValue = False Then
            INDseAssignmentException.EditValue = Nothing
            INDseRequestException.EditValue = Nothing
            INDseRadicatedException.EditValue = Nothing
            INDseDeliveryServiceException.EditValue = Nothing

            INDsleAssignmentUnitException.EditValue = Nothing
            INDsleRequestUnitException.EditValue = Nothing
            INDsleRadicatedUnitException.EditValue = Nothing
            INDsleDeliveryServiceUnitException.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Select Case sender.Tag
            Case "Edit"
                EditException()
            Case "Remove"
                DeleteException()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditException()
            Case "Remove"
                DeleteException()
        End Select
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class