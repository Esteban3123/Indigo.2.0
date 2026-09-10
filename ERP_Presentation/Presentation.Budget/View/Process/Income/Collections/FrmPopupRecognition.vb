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

Public Class FrmPopupRecognition

#Region "GLOBALS"
    ''' <summary>
    ''' lista los reconocimientos por tercero y vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRecognitionDetail As XPCollection
    ''' <summary>
    ''' lista con del detalle del recaudo con los reconocimientos seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCollectionDetail As List(Of CollectionDetail)
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
    ''' Propiedad para mostrar le mensaje en el visor 
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
    ''' propiedad para obtener el detalle del recaudo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListCollectionDetail As List(Of CollectionDetail)
        Get
            Return _listCollectionDetail
        End Get
    End Property
#End Region

#Region "HANDLES"

#Region "KeyDown"
    ''' <summary>
    ''' Evento al dar enter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupRecognition_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetListRecognitionDetail()
        End If
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Limpia la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listRecognitionDetail = Nothing
        _listCollectionDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Popup de frm Recognition
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupRecognition_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MCollection(Me.Tag)
            _listRecognitionDetail = model.ListRecognitionDetailByValidatyIdAndThirdPartyId(_validatyId, _thirdPartyId)

            If _listRecognitionDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron rubros para esa vigencia"
                IndigoGridControl1.RefreshGrid(INDGcRecognition)
                Exit Sub
            End If
            INDGcRecognition.DataSource = _listRecognitionDetail
            INDGcRecognition.Focus()
        End Using
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Click en agregar el detalle de Recognition
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        GetListRecognitionDetail()
    End Sub
#End Region

#End Region

#Region "METHODS"
    ''' <summary>
    ''' Obtiene la lista de detalles de reconomiento
    ''' </summary>
    Private Sub GetListRecognitionDetail()
        If INDGvRecognition.SelectedRowsCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado ningun rubro"
            Exit Sub
        End If

        For Each item In INDGvRecognition.GetSelectedRows()
            Dim recognitionDetail = _listRecognitionDetail((INDGvRecognition.GetDataSourceRowIndex(item)))
            Dim collectionDetail As New CollectionDetail
            With collectionDetail
                .RecognitionDetailId = recognitionDetail.Id
                .CodeRecognition = recognitionDetail.RecognitionId.Code
                .CodeNameCategory = recognitionDetail.CategoryId.NameCode
                .CodeNameFinancialSource = recognitionDetail.CategoryId.FinancialSourceId.NameCode
                .RecognitionBalance = recognitionDetail.Balance
                .CollectionType = 1
            End With
            If _listCollectionDetail Is Nothing Then
                _listCollectionDetail = New List(Of CollectionDetail)
            End If
            _listCollectionDetail.Add(collectionDetail)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

    End Sub
#End Region

End Class