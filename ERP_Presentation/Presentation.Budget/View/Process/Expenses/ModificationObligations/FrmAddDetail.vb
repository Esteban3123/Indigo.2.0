'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Budget.MVP
#End Region

Public Class FrmAddDetail

#Region "Globals"

    ''' <summary>
    ''' Listado de detalles de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollection As XPCollection

#End Region

#Region "PublicEvents"
    ''' <summary>
    ''' Evento que libera memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListXpCollection = Nothing
    End Sub

    ''' <summary>
    ''' Evento para enviar la info al form principal
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInfoFormModalToFormPrincipal(e As AddInfoEventArgs)

#End Region

#Region "Properties"

    Private _obligationId As Integer
    ''' <summary>
    ''' Obtiene el id de la obligación para poder realizar la consulta
    ''' de los detalles de la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationId As Integer
        Get
            Return _obligationId
        End Get
        Set(value As Integer)
            _obligationId = value
        End Set
    End Property

    Private _obligationCode As String
    ''' <summary>
    ''' Obtiene el codigo de la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationCode As String
        Get
            Return _obligationCode
        End Get
        Set(value As String)
            _obligationCode = value
        End Set
    End Property

    Private _listObligationModificationDetail As List(Of ObligationModificationDetail)
    ''' <summary>
    ''' Obtiene el listado para poder validar que el item que escojan
    ''' no exista en la rejilla del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListObligationModificationDetail As List(Of ObligationModificationDetail)
        Get
            Return _listObligationModificationDetail
        End Get
        Set(value As List(Of ObligationModificationDetail))
            _listObligationModificationDetail = value
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDgcDetail)
        LoadDatasourceGrid()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddInfo()
    End Sub

#End Region

#Region "RowClick"

    ''' <summary>
    ''' Evento que se dispara al presionar dobleClick sobre un registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewDetail_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles viewDetail.RowClick
        If e.Clicks = 2 AndAlso e.RowHandle >= 0 Then
            AddInfo()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Envia la informacion al formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInfo()
        Dim view As GridView = viewDetail
        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If

        Dim ListErrors As New StringBuilder
        Dim ListSend As New List(Of ObligationModificationDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim itemXpo As BudgetObligationDetailXpo = view.GetRow(listHandlesSelected(i))

            'Se valida que el item escogido no exista en el listado del form principal
            If ListObligationModificationDetail IsNot Nothing AndAlso ListObligationModificationDetail.Count > 0 Then
                Dim cont = (From l In ListObligationModificationDetail Where l.ObligationDetailId = itemXpo.Id Select l).Count
                If cont > 0 Then
                    ListErrors.AppendLine("El detalle seleccionado con el rubro " + itemXpo.CategoryId.NameCode + " y el recurso " + itemXpo.CategoryId.FinancialSourceId.NameCode + " ya existe en la lista.")
                    Continue For
                End If
            End If

            Dim obligationModificationDetail As New ObligationModificationDetail
            With obligationModificationDetail
                .ObligationDetailId = itemXpo.Id
                .ExpiredDate = itemXpo.ExpiredDate
                .BalanceObligation = itemXpo.Balance
                .CategoryId = itemXpo.CategoryId.Id
                .CodeNameCategory = itemXpo.CategoryId.NameCode
                .RevenueTypeId = itemXpo.RevenueTypeId.Id
                .CodeNameRevenueType = itemXpo.RevenueTypeId.NameCode
                .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.NameCode
                If itemXpo.CommitmentDetailId IsNot Nothing Then
                    .CommitmentDetailId = itemXpo.CommitmentDetailId.Id
                    .BalanceCommitment = itemXpo.CommitmentDetailId.Balance
                End If
            End With
            ListSend.Add(obligationModificationDetail)
        Next

        'Verifico que no hayan errores en el stringBuilder
        If ListErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
            Exit Sub
        End If

        Dim args As New AddInfoEventArgs With {.ListObligationModificationDetail = ListSend}
        RaiseEvent AddInfoFormModalToFormPrincipal(args)
    End Sub

    ''' <summary>
    ''' Metodo que consulta los detalles de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDatasourceGrid()
        Using model As New MModificationObligation(Me.Tag)
            ListXpCollection = model.ListObligationDetailByObligationId(ObligationId)
            If ListXpCollection Is Nothing OrElse ListXpCollection.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron detalles de la Obligación " + ObligationCode
                Exit Sub
            End If
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = ListXpCollection
            INDbtnAdd.Focus()
        End Using
    End Sub

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

End Class

Public Class AddInfoEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Lista para los detalles de la modificacion de obligacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListObligationModificationDetail As List(Of ObligationModificationDetail)

End Class