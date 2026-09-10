'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 29-08-2015
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


Public Class FrmPopUpCommitment

#Region "GLOBALS"
    ''' <summary>
    ''' listado de las disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAvailabilityDetail As List(Of Domain.Entities.AvailabilityDetail)

    ''' <summary>
    ''' listado de las disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCommitmentDetail As List(Of CommitmentDetail)
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
    ''' listado que retorna los rubros seleccionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListCommitmentDetail As List(Of CommitmentDetail)
        Get
            Return _listCommitmentDetail
        End Get
    End Property
    ''' <summary>
    ''' Propiedad para mostrar los mensajes en el visor 
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
        _listCommitmentDetail = New List(Of Domain.Entities.CommitmentDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As New Domain.Entities.AvailabilityDetail
            row = view.GetRow(listHandlesSelected(i))
            If row.Id <> 0 Then
                row.MarkAsModified()
            End If
            Dim commitmentDetail = New CommitmentDetail With {.AvailabilityDetailId = row.Id,
                                                                .ExpiredDate = CDate("31/12/" & _year),
                                                                .InitialValue = 0,
                                                                .DebitModificationValue = 0,
                                                                .CreditModificationValue = 0,
                                                                .TotalCommitment = 0,
                                                                .ExecutedValue = 0,
                                                                .Balance = 0,
                                                                .CodeNameCategory = row.CodeCategory + " - " + row.NameCategory,
                                                                .CodeNameFinancialSource = row.CodeNameFinancialSource,
                                                                .CodeNameRevenueType = row.CodeNameRevenueType,
                                                                .BalanceAffects = row.Balance,
                                                                .CategoryId = row.CategoryId,
                                                                .RevenueTypeId = row.RevenueTypeId,
                                                                .CodeAvailability = row.CodeAvailability,
                                                                .DateAvailability = row.DateAvailability,
                                                                .DateExpirationAvailability = row.DateExpirationAvailability}
            
            _listCommitmentDetail.Add(commitmentDetail)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Carga el datasource de la rejilla con el presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBudgetByValidityId()
        Using model As New MBusqueda
            '************Cargo los rubros*************'
            Dim filter() As Object = {_validatyId, 2, False}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAvailabilityDetailByValidityIdAndStatus, filter)
            LoadListAvailability(listXpo)
            INDGcCategoryBudget.DataSource = Nothing
            INDGcCategoryBudget.DataSource = _listAvailabilityDetail
            INDGcCategoryBudget.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListAvailability(listXpo As XPCollection)
        _listAvailabilityDetail = New List(Of Domain.Entities.AvailabilityDetail)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As BudgetAvailabilityDetailXpo In listXpo
                Dim availabilityDetail As New Domain.Entities.AvailabilityDetail
                With availabilityDetail

                    .Id = itemXpo.Id
                    .CategoryId = itemXpo.BudgetId.CategoryId.Id
                    .CodeCategory = itemXpo.BudgetId.CategoryId.Code
                    .NameCategory = itemXpo.BudgetId.CategoryId.Name

                    .FinancialSourceId = itemXpo.BudgetId.CategoryId.FinancialSourceId.Id
                    .CodeNameFinancialSource = itemXpo.BudgetId.CategoryId.FinancialSourceId.NameCode

                    .RevenueTypeId = itemXpo.BudgetId.RevenueTypeId.Id
                    .CodeNameRevenueType = itemXpo.BudgetId.RevenueTypeId.NameCode

                    .AvailabilityId = itemXpo.AvailabilityId.Id
                    .BudgetId = itemXpo.BudgetId.Id
                    .InitialValue = itemXpo.InitialValue
                    .DebitModificationValue = itemXpo.DebitModificationValue
                    .CreditModificationValue = itemXpo.CreditModificationValue
                    .TotalAvailability = itemXpo.TotalAvailability
                    .ExecutedValue = itemXpo.ExecutedValue
                    .Balance = itemXpo.Balance
                    .CodeAvailability = itemXpo.AvailabilityId.Code
                    .DateAvailability = itemXpo.AvailabilityId.DocumentDate
                    .DateExpirationAvailability = itemXpo.AvailabilityId.ExpirationDate
                End With
                _listAvailabilityDetail.Add(availabilityDetail)
            Next
        End If
    End Sub

#End Region

#Region "Handles"

#Region "Show"
    ''' <summary>
    ''' Libera la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listAvailabilityDetail = Nothing
        _listCommitmentDetail = Nothing
    End Sub
    ''' <summary>
    ''' Muestra FrmPopUpRecognitionModification
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub FrmPopUpRecognitionModification_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IndigoGridControl1.RefreshGrid(INDGcCategoryBudget)
        LoadBudgetByValidityId()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en el btn INDbtnAddCategory
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