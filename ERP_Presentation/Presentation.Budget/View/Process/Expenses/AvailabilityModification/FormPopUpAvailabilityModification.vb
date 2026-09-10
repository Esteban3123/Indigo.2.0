'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 01-09-2015
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

Public Class FormPopUpAvailabilityModification

#Region "GLOBALS"
    ''' <summary>
    ''' listado de items del presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBudget As List(Of Domain.Entities.Budget)
    ''' <summary>
    ''' listado de los rubros que se seleccionaron
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAvailabilityModificationDetail As List(Of AvailabilityModificationDetail)
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
    ''' listado que retorna los rubros seleccionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListAvailabilityModificationDetail As List(Of AvailabilityModificationDetail)
        Get
            Return _listAvailabilityModificationDetail
        End Get
    End Property
    ''' <summary>
    ''' propiedad para mostrar los mensajes en el visor
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
        Dim view As GridView = INDGvCategoryBudget

        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If
        _listAvailabilityModificationDetail = New List(Of Domain.Entities.AvailabilityModificationDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As New Domain.Entities.Budget
            row = view.GetRow(listHandlesSelected(i))
            If row.Id <> 0 Then
                row.MarkAsModified()
            End If

            Dim availabilityModificationDetail = New AvailabilityModificationDetail With
            {
                .AvailabilityDetailId = row.Id,
                .BalanceAvailability = row.Balance,
                .CategoryId = row.CategoryId,
                .CodeNameCategory = row.CodeCategory + " - " + row.NameCategory,
                .RevenueTypeId = row.RevenueTypeId,
                .CodeNameRevenueType = row.CodeNameRevenueType,
                .CodeNameFinancialSource = row.CodeNameFinancialSource,
                .BalanceBudget = row.Balance,
                .CCPETLinkAccount = row.CCPETLinkAccount
            }

            _listAvailabilityModificationDetail.Add(availabilityModificationDetail)
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
            Dim filter() As Object = {_validatyId, 2, 2, True}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBudgetByBudgetValidityId, filter)
            LoadListBudget(listXpo)
            INDGcCategoryBudget.DataSource = Nothing
            INDGcCategoryBudget.DataSource = _listBudget
            INDGcCategoryBudget.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListBudget(listXpo As XPCollection)
        _listBudget = New List(Of Domain.Entities.Budget)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As BudgetXpo In listXpo
                Dim budget As New Domain.Entities.Budget
                With budget

                    .Id = itemXpo.Id
                    .CategoryId = itemXpo.CategoryId.Id
                    .CodeCategory = itemXpo.CategoryId.Code
                    .NameCategory = itemXpo.CategoryId.Name

                    .FinancialSourceId = itemXpo.CategoryId.FinancialSourceId.Id
                    .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.NameCode

                    .RevenueTypeId = itemXpo.RevenueTypeId.Id
                    .CodeNameRevenueType = itemXpo.RevenueTypeId.NameCode

                    .BudgetHeaderId = itemXpo.BudgetHeaderId.Id
                    .InitialValue = itemXpo.InitialValue
                    .DebitValueModification = itemXpo.DebitValueModification
                    .CreditValueModification = itemXpo.CreditValueModification
                    .DebitValueTransfer = itemXpo.DebitValueTransfer
                    .CreditValueTransfer = itemXpo.CreditValueTransfer
                    .TotalBudget = itemXpo.TotalBudget
                    .ExecutedValue = itemXpo.ExecutedValue
                    .SuspendedValue = itemXpo.SuspendedValue
                    .Balance = itemXpo.Balance
                    .CreationUser = itemXpo.CreationUser
                    .CreationDate = itemXpo.CreationDate
                End With
                If itemXpo.CategoryId.CCPETCodeId IsNot Nothing Then
                    budget.CCPETCodeId = itemXpo.CategoryId.CCPETCodeId.Id
                    budget.CodeNameCCPET = itemXpo.CategoryId.CCPETCodeId.NameCode
                    budget.CCPETLinkAccount = itemXpo.CategoryId.CCPETCodeId.LinkAccount
                End If
                If itemXpo.CategoryId.CPCCodeId IsNot Nothing Then
                    budget.CPCCodeId = itemXpo.CategoryId.CPCCodeId.Id
                    budget.CodeNameCPC = itemXpo.CategoryId.CPCCodeId.NameCode
                End If
                _listBudget.Add(budget)
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
        _listBudget = Nothing
        _listAvailabilityModificationDetail = Nothing
    End Sub
    ''' <summary>
    ''' pop up con el FrmPopUpAvailabilityModification
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpAvailabilityModification_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IndigoGridControl1.RefreshGrid(INDGcCategoryBudget)
        LoadBudgetByValidityId()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' evento al dar click en agregar nueva categoria
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