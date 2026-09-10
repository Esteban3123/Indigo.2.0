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

Public Class FrmPopupAddCommitment

#Region "GLOBALS"
    Dim _listCommitmentDetail As XPCollection

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
    ''' id del tercero para filtrar y obtener los reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _thirdPartyId As Integer
    Public WriteOnly Property ThirdPartyId As Integer
        Set(value As Integer)
            _thirdPartyId = value
        End Set
    End Property
    ''' <summary>
    ''' fecha de la obligacion para validar la fecha de vencimiento del compromniso
    ''' </summary>
    ''' <remarks></remarks>
    Dim _documentDate As DateTime
    Public WriteOnly Property DocumentDate As DateTime
        Set(value As DateTime)
            _documentDate = value
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

    Public ReadOnly Property ListObligationDetail As List(Of ObligationDetail)
        Get
            Return _listObligationDetail
        End Get
    End Property
#End Region

#Region "HANDLES"
#Region "KeyDown"
    ''' <summary>
    ''' Evento al dar enter en codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupAddCommitment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetListCommitmentDetail()
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
        _listCommitmentDetail = Nothing
        _listObligationDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Evento 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupAddCommitment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MObligation(Me.Tag)
            _listCommitmentDetail = model.ListCommitmentDetailObligation(_validatyId, _thirdPartyId, _documentDate)

            If _listCommitmentDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron compromisos"
                IndigoGridControl1.RefreshGrid(INDGcCommitment)
                Exit Sub
            End If
            INDGcCommitment.DataSource = _listCommitmentDetail
            INDGvCommitment.ExpandAllGroups()
            INDGcCommitment.Focus()
        End Using
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al dar click en INDBtnAdd_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        GetListCommitmentDetail()
    End Sub
#End Region

#End Region

#Region "METHODS"
    ''' <summary>
    ''' Genea a lista de compromisos
    ''' </summary>
    Private Sub GetListCommitmentDetail()
        If INDGvCommitment.SelectedRowsCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado ningún compromiso"
            Exit Sub
        End If

        For Each item In INDGvCommitment.GetSelectedRows()
            If item < 0 Then
                Continue For
            End If
            Dim commitmentDetail = _listCommitmentDetail((INDGvCommitment.GetDataSourceRowIndex(item)))

            Dim obligatioDetail As New ObligationDetail
            With obligatioDetail
                .CommitmentDetailId = commitmentDetail.Id
                .CategoryId = commitmentDetail.CategoryId.Id
                .RevenueTypeId = commitmentDetail.RevenueTypeId.Id
                .ExpiredDate = commitmentDetail.ExpiredDate
                .CommitmentCode = commitmentDetail.CommitmentId.Code
                .CommitmentType = commitmentDetail.CommitmentId.CommitmentType
                .CommitmentDocument = commitmentDetail.CommitmentId.Document
                .CodeNameBudget = commitmentDetail.CategoryId.NameCode
                .CodeNameFinancialSource = commitmentDetail.CategoryId.FinancialSourceId.NameCode
                .BalanceCommitment = commitmentDetail.Balance
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