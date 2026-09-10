#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.Utils.DragDrop
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base

#End Region

Public Class FrmRequestsToAutomaticAllocation

#Region "Variables"

    Dim dt As DataTable

    Dim _listRequestsXpo As List(Of ViewListRequestsXpo)

#End Region

#Region "Builder"

    Public Sub New(listRequestsXpo As List(Of ViewListRequestsXpo))
        InitializeComponent()

        Me._listRequestsXpo = listRequestsXpo
        HandleBehaviorDragDropEvents()
    End Sub

#End Region

#Region "Properties"

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub HandleBehaviorDragDropEvents()
        Dim gridControlBehavior As DragDropBehavior = BehaviorManager1.GetBehavior(Of DragDropBehavior)(Me.INDGvRequest)
        AddHandler gridControlBehavior.DragDrop, AddressOf Behavior_DragDrop
        AddHandler gridControlBehavior.DragOver, AddressOf Behavior_DragOver
    End Sub

    ''' <summary>
    ''' Guarda una reasignación de usuarios
    ''' </summary>
    Private Async Sub Guardar()
        Dim ListTraceabilityPaperwork As New List(Of TraceabilityPaperwork)

        For Each patient In dt.Rows
            Dim ListViewListRequestsXpo = Me._listRequestsXpo.Where(Function(d) d.PatientCode = patient("PatientCode")).ToList()
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
                    .ServiceId = ViewListRequestsXpo.ServiceId
                    .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId

                    'Si viene el registro con estado se asigna, sino se coloca solicitado
                    If ViewListRequestsXpo.TraceabilityPaperworkStatus <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkStatus > 0 Then
                        .Status = ViewListRequestsXpo.TraceabilityPaperworkStatus
                    Else
                        .Status = 1
                    End If
                    .Order = patient("Order")
                End With
                ListTraceabilityPaperwork.Add(TraceabilityPaperwork)
            Next
        Next

        Try
            Using model As New MDashboardAuthorization("")
                Me.AsyncLoader(True)
                Dim result = Await model.AssignTraceabilityPaperwork(ListTraceabilityPaperwork)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                RaiseEvent ReturnModalArgs(Nothing, Nothing)
                Me.Close()
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Custom"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Load"

    Private Sub PopupHomologation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolBar.Visible = False
        If Me._listRequestsXpo Is Nothing OrElse Not Me._listRequestsXpo.Any() Then
            Exit Sub
        End If

        dt = New DataTable()
        dt.Columns.Add("PatientCode")
        dt.Columns.Add("PatientName")
        dt.Columns.Add("PatientAge")
        dt.Columns.Add("Order", GetType(Integer))

        Dim order As Integer = 1
        For Each request In Me._listRequestsXpo
            If dt.Select(String.Format("PatientCode = '{0}'", request.PatientCode)).Length > 0 Then
                Continue For
            End If

            dt.Rows.Add(request.PatientCode, request.PatientName, request.PatientAge, order)
            order = order + 1
        Next

        INDGcRequest.DataSource = dt
        INDGcRequest.RefreshDataSource()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmRequestsToAutomaticAllocation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Sort"

    Class Order
        Public IsDragRow As Boolean
        Public RowHande As Integer
        Public RowIndex As Integer
        Public Order As Integer
    End Class

    Private Sub INDGvRequest_CustomColumnSort(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs) Handles INDGvRequest.CustomColumnSort
        e.Handled = True
        e.Result = System.Collections.Comparer.Default.Compare(e.Value1, e.Value2)
    End Sub

    Private Sub INDGvRequest_EndSorting(sender As Object, e As EventArgs) Handles INDGvRequest.EndSorting
        For i As Integer = 0 To INDGvRequest.DataRowCount - 1
            INDGvRequest.SetRowCellValue(i, "Order", (i + 1))
        Next
    End Sub

    Private Sub Behavior_DragOver(ByVal sender As Object, ByVal e As DevExpress.Utils.DragDrop.DragOverEventArgs)
        INDGvRequest.ClearSorting()
        INDGvRequest.Columns("Order").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        Dim args As DragOverGridEventArgs = DragOverGridEventArgs.GetDragOverGridEventArgs(e)
        args.Handled = True
    End Sub

    Private Sub Behavior_DragDrop(ByVal sender As Object, ByVal e As DevExpress.Utils.DragDrop.DragDropEventArgs)
        If e.Action = DragDropActions.None Then
            Return
        End If

        Dim hitPoint As Point = INDGvRequest.GridControl.PointToClient(Cursor.Position)
        Dim hitInfo As GridHitInfo = INDGvRequest.CalcHitInfo(hitPoint)
        Dim targetRowHandle As Integer = hitInfo.RowHandle
        Dim initialHandle As Integer = targetRowHandle + If(e.InsertType = InsertType.After, 1, 0)

        Dim currentOrder = initialHandle
        Dim initialRow As Integer = 0
        Dim endRow As Integer = 0

        Dim orderRows As New List(Of Order)
        Dim sourceHandle() As Integer = e.GetData(Of Integer())()
        For Each rowHandle As Integer In sourceHandle
            Dim rowIndex As Integer = INDGvRequest.GetDataSourceRowIndex(rowHandle)
            Dim order = orderRows.FirstOrDefault(Function(d) d.RowIndex = rowIndex)
            If order Is Nothing Then
                order = New [Order]
                orderRows.Add(order)
            End If

            currentOrder = currentOrder + 1

            order.IsDragRow = True
            order.RowHande = rowHandle
            order.RowIndex = rowIndex
            order.Order = currentOrder

            If rowHandle > initialHandle Then
                initialRow = initialHandle
                endRow = rowHandle - 1
            Else
                initialRow = rowHandle + 1
                endRow = initialHandle - 1
            End If

            For i As Integer = initialRow To endRow
                rowIndex = INDGvRequest.GetDataSourceRowIndex(i)
                order = orderRows.FirstOrDefault(Function(d) d.RowIndex = rowIndex)
                If order Is Nothing Then
                    order = New [Order]
                    orderRows.Add(order)
                End If

                If order.IsDragRow Then
                    Continue For
                End If

                order.RowHande = i
                order.RowIndex = rowIndex
                order.Order += If(rowHandle > initialHandle, 1, -1)
            Next
        Next rowHandle

        For Each order In orderRows
            If order.IsDragRow Then
                dt.Rows(order.RowIndex)("Order") = order.Order
            Else
                dt.Rows(order.RowIndex)("Order") += order.Order
            End If
        Next
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDRptPceDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRptPceDetail.QueryPopUp
        Dim patient = CType(INDGvRequest.GetFocusedRow, Object)
        INDGcRequestDetail.DataSource = Me._listRequestsXpo.Where(Function(d) d.PatientCode = patient("PatientCode")).ToList()
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Guardar()
    End Sub

#End Region

#End Region

End Class