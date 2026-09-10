'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Budget.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities.AnnualizedCashFlow
Imports Domain.Base.Entities

#End Region

Public Class FrmPopupPacTransferExpense

#Region "GLOBALS"
    ''' <summary>
    ''' listado de los rubros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnnualizedCashFlow As List(Of AnnualizedCashFlow)
    ''' <summary>
    ''' listado de los rubros que se seleccionaron
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnnualizedCashFlowTransferDetail As List(Of AnnualizedCashFlowTransferDetail)
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' id de la vigencia para realizar la consulta y obtener los rubros de esta
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validatyId As Integer
    Public WriteOnly Property ValidatyId As Integer
        Set(value As Integer)
            _validatyId = value
        End Set
    End Property
    ''' <summary>
    ''' mes que se encuentra abierto para obtener los registros del pac
    ''' </summary>
    ''' <remarks></remarks>
    Dim _monthOpen As Integer
    Public WriteOnly Property monthOpen As Integer
        Set(value As Integer)
            _monthOpen = value
        End Set
    End Property
    ''' <summary>
    ''' listado que retorna los rubros seleccionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListAnnualizedCashFlowTransferDetail As List(Of AnnualizedCashFlowTransferDetail)
        Get
            Return _listAnnualizedCashFlowTransferDetail
        End Get
    End Property
    ''' <summary>
    ''' Prpoiedad para mostrar los mensajes en el visor
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

#Region "METHODS"

    ''' <summary>
    ''' Adiciona la informacion a la rejilla del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInfo()
        Dim view As GridView = INDgvCategoryPac

        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If
        _listAnnualizedCashFlowTransferDetail = New List(Of Domain.Entities.AnnualizedCashFlowTransferDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As New Domain.Entities.AnnualizedCashFlow
            row = view.GetRow(listHandlesSelected(i))
            If row.Id <> 0 Then
                row.MarkAsModified()
            End If
            Dim annualizedCashFlowTransferDetail = New AnnualizedCashFlowTransferDetail With {.AnnualizedCashFlowId = row.Id, .Nature = 0, .Value = 0, .Month = row.Month, .CodeNameCategory = row.CodeNameCategory, .Balance = row.Balance, .CodeNameFinancialSource = row.CodeNameFinancialSource}
            _listAnnualizedCashFlowTransferDetail.Add(annualizedCashFlowTransferDetail)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Carga el datasource de la rejilla con el presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAnnualizedcashFlowByValidityId()
        Using model As New MBusqueda
            '************Cargo los rubros*************'
            Dim filter() As Object = {_validatyId, 2, 2, _monthOpen}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAnnualizedCashFlowValidityId, filter)
            LoadListAnnualizedCashFlow(listXpo)
            INDGcCategoryPac.DataSource = Nothing
            INDGcCategoryPac.DataSource = _listAnnualizedCashFlow
            INDGcCategoryPac.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListAnnualizedCashFlow(listXpo As XPCollection)
        _listAnnualizedCashFlow = New List(Of Domain.Entities.AnnualizedCashFlow)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As BudgetAnnualizedCashFlowXpo In listXpo
                Dim annualizedCashFlow As New Domain.Entities.AnnualizedCashFlow
                With annualizedCashFlow

                    .Id = itemXpo.Id
                    .CategoryId = itemXpo.CategoryId.Id
                    .CodeNameCategory = itemXpo.CategoryId.Code + " - " + itemXpo.CategoryId.Name

                    .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.NameCode

                    .Month = itemXpo.Month
                    .InitialValue = itemXpo.InitialValue
                    .DebitModificationValue = itemXpo.DebitModificationValue
                    .CreditModificationValue = itemXpo.CreditModificationValue
                    .DebitTransferValue = itemXpo.DebitTransferValue
                    .CreditTransferValue = itemXpo.CreditTransferValue
                    .TotalScheduled = itemXpo.TotalScheduled
                    .ExecutedValue = itemXpo.ExecutedValue
                    .Balance = itemXpo.Balance
                    .ReserveValue = itemXpo.ReserveValue
                    .DebitReserveModValue = itemXpo.DebitReserveModValue
                    .CreditReserveModValie = itemXpo.CreditReserveModValie
                    .DebitReserveTransValue = itemXpo.DebitReserveTransValue
                    .CreditReserveTransValue = itemXpo.CreditReserveTransValue
                    .ExecutedReserveValue = itemXpo.ExecutedReserveValue
                    .CxPValue = itemXpo.CxPValue
                    .DebitCxPModificationValue = itemXpo.DebitCxPModificationValue
                    .CreditCxPModificationValue = itemXpo.CreditCxPModificationValue
                    .DebitCxPTransferValue = itemXpo.DebitCxPTransferValue
                    .CreditCxPTransferValue = itemXpo.CreditCxPTransferValue
                    .ExecutedCxPValue = itemXpo.ExecutedCxPValue
                    .Status = itemXpo.Status
                    .CreationUser = itemXpo.CreationUser
                    .CreationDate = itemXpo.CreationDate
                End With
                _listAnnualizedCashFlow.Add(annualizedCashFlow)
            Next
        End If
    End Sub

#End Region

#Region "Handles"

#Region "Show"
    ''' <summary>
    ''' Evento para que libera la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listAnnualizedCashFlow = Nothing
        _listAnnualizedCashFlowTransferDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Metodo que muestra el frm FrmPopupPacTransfer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupPacTransfer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IndigoGridControl1.RefreshGrid(INDGcCategoryPac)
        LoadAnnualizedcashFlowByValidityId()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en agregar categoria 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddCategory_Click(sender As Object, e As EventArgs) Handles INDbtnAddCategory.Click
        AddInfo()
    End Sub

#End Region

#Region "RowClick"

    ''' <summary>
    ''' Doble click en el registro del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvCategoryPac_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDgvCategoryPac.RowClick
        If e.Clicks = 2 AndAlso e.RowHandle >= 0 Then
            AddInfo()
        End If
    End Sub

#End Region

#Region "InvalidRowException"

    ''' <summary>
    ''' Controla la excepcion arrojada por la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvCategoryPac_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles INDgvCategoryPac.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#Region "CustomDrawCell"
    ''' <summary>
    ''' Evento que customiza el mes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvCategoryPac_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvCategoryPac.CustomDrawCell
        If DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView).Equals(Me.INDgvCategoryPac) Then
            Dim obj = Nothing
            obj = CType(Me.INDgvCategoryPac.GetRow(e.RowHandle), Domain.Entities.AnnualizedCashFlow)
            If obj IsNot Nothing Then
                If e.Column.Equals(Me.INDgcolMonth) Then
                    Select Case obj.Month
                        Case 1
                            e.DisplayText = "Enero"
                        Case 2
                            e.DisplayText = "Febrero"
                        Case 3
                            e.DisplayText = "Marzo"
                        Case 4
                            e.DisplayText = "Abril"
                        Case 5
                            e.DisplayText = "Mayo"
                        Case 6
                            e.DisplayText = "Junio"
                        Case 7
                            e.DisplayText = "Julio"
                        Case 8
                            e.DisplayText = "Agosto"
                        Case 9
                            e.DisplayText = "Septiembre"
                        Case 10
                            e.DisplayText = "Octubre"
                        Case 11
                            e.DisplayText = "Noviembre"
                        Case 12
                            e.DisplayText = "Diciembre"
                    End Select
                End If
            End If
        End If

    End Sub

#End Region

#End Region

End Class