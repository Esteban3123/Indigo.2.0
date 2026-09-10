'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 10-09-2015
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
Imports Domain.Entities.RecognitionDetail
Imports Domain.Base.Entities

#End Region

Public Class FrmPopUpPaymentOrder

#Region "GLOBALS"
    ''' <summary>
    ''' listado de los detalles de las obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listObligationDetail As List(Of Domain.Entities.ObligationDetail)
    ''' <summary>
    ''' listado de los detalles de las ordenes de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPaymentOrderDetail As List(Of PaymentOrderDetail)
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
    ''' año de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _year As Integer
    Public WriteOnly Property Year As Integer
        Set(value As Integer)
            _year = value
        End Set
    End Property
    ''' <summary>
    ''' año de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ThirdPartyId As Integer
    Public WriteOnly Property ThirdPartyId As Integer
        Set(value As Integer)
            _ThirdPartyId = value
        End Set
    End Property
    ''' <summary>
    ''' listado que retorna los rubros seleccionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListPaymentOrderDetail As List(Of PaymentOrderDetail)
        Get
            Return _listPaymentOrderDetail
        End Get
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

#Region "METHODS"

    ''' <summary>
    ''' Adiciona la informacion a la rejilla del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInfo()
        Dim view As GridView = INDGvCategoryBudget

        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If
        _listPaymentOrderDetail = New List(Of Domain.Entities.PaymentOrderDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As New Domain.Entities.ObligationDetail
            row = view.GetRow(listHandlesSelected(i))
            If row.Id <> 0 Then
                row.MarkAsModified()
            End If
            Dim paymentOrderDetail = New PaymentOrderDetail With {.ObligationDetailId = row.Id,
                                                                .ExpiredDate = CDate("31/12/" & _year),
                                                                .InitialValue = 0,
                                                                .DebitModificationValue = 0,
                                                                .CreditModificationValue = 0,
                                                                .TotalPaymentOrder = 0,
                                                                .ExecutedValue = 0,
                                                                .Balance = 0,
                                                                .CodeNameCategory = row.CodeNameBudget,
                                                                .CodeNameFinancialSource = row.CodeNameFinancialSource,
                                                                .CodeNameRevenueType = row.CodeNameRevenueType,
                                                                .CodeObligation = row.ObligationCode,
                                                                .CodeCommitment = row.CommitmentCode,
                                                                .BalanceAffects = row.Balance,
                                                                .CategoryId = row.CategoryId,
                                                                .RevenueTypeId = row.RevenueTypeId}

            _listPaymentOrderDetail.Add(PaymentOrderDetail)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Caraga el datasource de la rejilla con el presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBudgetByValidityId()
        Using model As New MBusqueda
            '************Cargo los rubros*************'
            Dim filter() As Object = {_validatyId, 2, _ThirdPartyId}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListObligationDetailByValidityIdAndStatus, filter)
            LoadListAvailability(listXpo)
            INDGcCategoryBudget.DataSource = Nothing
            INDGcCategoryBudget.DataSource = _listObligationDetail
            INDGcCategoryBudget.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListAvailability(listXpo As XPCollection)
        _listObligationDetail = New List(Of Domain.Entities.ObligationDetail)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As BudgetObligationDetailXpo In listXpo
                Dim obligationDetail As New Domain.Entities.ObligationDetail
                With obligationDetail

                    .Id = itemXpo.Id
                    .CategoryId = itemXpo.CategoryId.Id
                    .CodeNameBudget = itemXpo.CategoryId.NameCode
                    .ExpiredDate = itemXpo.ExpiredDate
                    .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.NameCode

                    .RevenueTypeId = itemXpo.RevenueTypeId.Id
                    .CodeNameRevenueType = itemXpo.RevenueTypeId.NameCode
                    .ObligationId = itemXpo.ObligationId.Id
                    If itemXpo.CommitmentDetailId IsNot Nothing Then
                        .CommitmentDetailId = itemXpo.CommitmentDetailId.Id
                    End If
                    .InitialValue = itemXpo.InitialValue
                    .DebitModificationValue = itemXpo.DebitModificationValue
                    .CreditModificationValue = itemXpo.CreditModificationValue
                    .TotalObligation = itemXpo.TotalObligation
                    .ExecutedValue = itemXpo.ExecutedValue
                    .Balance = itemXpo.Balance
                    If itemXpo.CommitmentDetailId IsNot Nothing Then
                        .CommitmentCode = itemXpo.CommitmentDetailId.CommitmentId.Code
                    End If
                    .ObligationCode = itemXpo.ObligationId.Code
                    .ObligationDate = itemXpo.ObligationId.DocumentDate
                End With
                _listObligationDetail.Add(obligationDetail)
            Next
        End If
    End Sub

#End Region

#Region "Handles"

#Region "Show"
    ''' <summary>
    ''' Libera memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listObligationDetail = Nothing
        _listPaymentOrderDetail = Nothing
    End Sub
    ''' <summary>
    ''' Muestra el FrmPopUpPaymentOrder
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpPaymentOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IndigoGridControl1.RefreshGrid(INDGcCategoryBudget)
        LoadBudgetByValidityId()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al agregar una categoria
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
    Private Sub INDGvCategoryBudget_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDGvCategoryBudget.RowClick
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
    Private Sub INDGvCategoryBudget_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles INDGvCategoryBudget.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#End Region

End Class