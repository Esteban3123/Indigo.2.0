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

Public Class FrmPopupAddBudget

#Region "GLOBALS"
    Dim _listBudget As XPCollection

    Dim _listObligationDetail As List(Of ObligationDetail)
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' id de la vigencia para realizar la consulta y obtener los reconocimientos
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
    Dim _validityYear As String
    Public WriteOnly Property ValidityYear As String
        Set(value As String)
            _validityYear = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad  para mostar los mensajes en el visor 
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
    ''' <summary>
    ''' Propiedad que devuelce el detalle de la lista de obligaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListObligationDetail As List(Of ObligationDetail)
        Get
            Return _listObligationDetail
        End Get
    End Property
#End Region

#Region "HANDLES"
#Region "KeyDown"
    ''' <summary>
    ''' Evento que muestra la lista de presupuestos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupAddBudget_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetListBudget()
        End If
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Libera la memoria al cerrar el frm 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listBudget = Nothing
        _listObligationDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Evento que muestra los rubros por vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupAddCommitment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MObligation(Me.Tag)
            _listBudget = model.ListButget(_validatyId)

            If _listBudget.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron rubros para esa vigencia"
                IndigoGridControl1.RefreshGrid(INDGcBudget)
                Exit Sub
            End If
            INDGcBudget.DataSource = _listBudget
            INDGcBudget.Focus()
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
        GetListBudget()
    End Sub
#End Region

#End Region


#Region "METHODS"
    ''' <summary>
    ''' Obtiene la lista de presupuestos
    ''' </summary>
    Private Sub GetListBudget()
        If INDGVBudget.SelectedRowsCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado ningún rubro"
            Exit Sub
        End If

        For Each item In INDGVBudget.GetSelectedRows()
            Dim budget = _listBudget((INDGVBudget.GetDataSourceRowIndex(item)))

            Dim obligatioDetail As New ObligationDetail
            With obligatioDetail
                .CategoryId = budget.CategoryId.Id
                .RevenueTypeId = budget.RevenueTypeId.Id
                .ExpiredDate = CDate("31/12/" + _validityYear + " 23:59:59")

                .CodeNameBudget = budget.CategoryId.NameCode
                .CodeNameFinancialSource = budget.CategoryId.FinancialSourceId.NameCode
                .BalanceCommitment = budget.Balance
            End With
            If _listObligationDetail Is Nothing Then
                _listObligationDetail = New List(Of ObligationDetail)
            End If
            _listObligationDetail.Add(obligatioDetail)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

    End Sub
#End Region
End Class