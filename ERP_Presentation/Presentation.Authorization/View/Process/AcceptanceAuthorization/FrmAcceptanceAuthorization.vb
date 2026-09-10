'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/07/2020
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
Imports Presentation.Billing.MVP

#End Region

Public Class FrmAcceptanceAuthorization

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PAcceptanceAuthorization

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
    ''' Carga la información a la rejilla
    ''' </summary>
    Private Sub LoadInformation()
        If INDgcInformation.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewInformation.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewAcceptanceAuthorizationXpo(INDsleCareCenter.EditValue, INDsleFunctionalUnit.EditValue)
                                  INDgcInformation.BeginInvoke(Sub()
                                                                   INDgcInformation.DataSource = result
                                                                   INDviewInformation.HideLoadingPanel()
                                                               End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.Tag)

        Dim ListActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(109) Then 'Si tiene permiso de aceptar
            ListActions.Add(eAcciones.Acceptance)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(110) Then 'Si tiene permiso de rechazar
            ListActions.Add(eAcciones.Reject)
        End If

        If ListActions.Count > 0 Then 'Si hay menu se asigna al gridView 
            IndigoGridView1.SetListAcction(INDviewInformation, ListActions)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewInformation.Columns
                If col.Name = "colActions" Then
                    col.Width = 100
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Actualiza el estado de aceptación del trámite
    ''' </summary>
    Private Async Sub Guardar(status As Integer, observations As String, authorizationRejectionId As Integer)
        Dim list = (From x In INDviewInformation.GetSelectedRows() Where INDviewInformation.IsGroupRow(x) = False Select CType(INDviewInformation.GetRow(x), ViewAcceptanceAuthorizationXpo)).ToList()
        If list.Count = 0 Then
            list = {CType(INDviewInformation.GetFocusedRow(), ViewAcceptanceAuthorizationXpo)}.ToList()
        End If

        Dim listTuple As New List(Of Tuple(Of Integer, Integer, String, Integer))
        For Each item In list
            listTuple.Add(New Tuple(Of Integer, Integer, String, Integer)(item.TraceabilityPaperworkId, status, observations, authorizationRejectionId))
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MAcceptanceAuthorization("")
                Dim result = Await model.SP_SaveAcceptanceAuthorization(listTuple)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se actualizó el estado de aceptación correctamente"
                    INDgcInformation.DataSource = Nothing
                    LoadInformation()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que abre el form para asignar la observación de rechazo
    ''' </summary>
    Private Sub OpenFormReject()
        Using formulario As New FrmReject
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 400
            formulario.ToolBar.Visible = False
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al cerrar el form de observaciones de rechazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As RejectEventArgs)
        Guardar(2, e.ObservationsReject, e.AuthorizationRejectionId)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAcceptanceAuthorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetActionsColumns()
        Me.ToolBar.Hide()
        Presenter = New PAcceptanceAuthorization()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAcceptanceAuthorization_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Focus()
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

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Acceptance"
                Guardar(1, "", 0)
            Case "Reject"
                OpenFormReject()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If

        Select Case tagGrid
            Case "Acceptance"
                Guardar(1, "", 0)
            Case "Reject"
                OpenFormReject()
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de cargar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
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

        INDgcInformation.DataSource = Nothing
        LoadInformation()
    End Sub

#End Region

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

End Class