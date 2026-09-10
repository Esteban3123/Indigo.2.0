'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 25-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Budget.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository

#End Region

Public Class FrmPopupCategoryExpense

#Region "GLOBALS"
    ''' <summary>
    ''' listado de los rubros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnnualizedCashFlow As XPCollection
    ''' <summary>
    ''' listado de los rubros que se seleccionaron
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnnualizedCashFlowModificationDetail As List(Of AnnualizedCashFlowModificationDetail)
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
    Public ReadOnly Property ListAnnualizedCashFlowModificationDetail As List(Of AnnualizedCashFlowModificationDetail)
        Get
            Return _listAnnualizedCashFlowModificationDetail
        End Get
    End Property
    ''' <summary>
    ''' Prpiedad para mostrar el mensaje en el visor
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
    ''' Metodo para obtener GetListAnnualizedCashFlow
    ''' </summary>
    Private Sub GetListAnnualizedCashFlow()
        If INDGvCategory.SelectedRowsCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado ningun rubro"
            Exit Sub
        End If

        For Each item In INDGvCategory.GetSelectedRows()
            Dim annualizedCashFlow = _listAnnualizedCashFlow((INDGvCategory.GetDataSourceRowIndex(item)))
            Dim annualizedCashFlowModificationDetail As New AnnualizedCashFlowModificationDetail
            With annualizedCashFlowModificationDetail
                .AnnualizedCashFlowId = annualizedCashFlow.Id
                .CodeNameCategory = annualizedCashFlow.CategoryId.NameCode
                .CategoryResource = annualizedCashFlow.CategoryId.FinancialSourceId.NameCode
                .Month = annualizedCashFlow.Month
                .Balance = annualizedCashFlow.Balance
                .Nature = 0
            End With
            If _listAnnualizedCashFlowModificationDetail Is Nothing Then
                _listAnnualizedCashFlowModificationDetail = New List(Of AnnualizedCashFlowModificationDetail)
            End If
            _listAnnualizedCashFlowModificationDetail.Add(annualizedCashFlowModificationDetail)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

#End Region

#Region "HANDLES"
#Region "Shown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listAnnualizedCashFlow = Nothing
        _listAnnualizedCashFlowModificationDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Muestra el frm de categorias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupCategory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MAnnualizedCashFlowModification(Me.Tag)
            _listAnnualizedCashFlow = model.ListAnnualizedCashFlowValidityId(_validatyId, 2, _monthOpen)

            If _listAnnualizedCashFlow.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron rubros para esa vigencia"
                IndigoGridControl1.RefreshGrid(INDGcCategoty)
                Exit Sub
            End If
            INDGcCategoty.DataSource = _listAnnualizedCashFlow
            INDGcCategoty.Focus()
        End Using
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        GetListAnnualizedCashFlow()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento al dar enter en code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupCategory_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetListAnnualizedCashFlow()
        End If
    End Sub
#End Region
#End Region


End Class