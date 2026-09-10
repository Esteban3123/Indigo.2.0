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

Public Class FrmPopupCollection

#Region "GLOBALS"
    ''' <summary>
    ''' lista los recaudos por id y vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCollectionDetail As XPCollection
    ''' <summary>
    ''' lista con del detalle de  las modificacions del recaudo seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCollectionModificationDetail As List(Of CollectionModificationDetail)
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
    ''' id del recaudo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _collectionId As Integer
    Public WriteOnly Property CollectionId As Integer
        Set(value As Integer)
            _collectionId = value
        End Set
    End Property
    ''' <summary>
    ''' Muestra los mensajes en el visor
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
    Public ReadOnly Property ListCollectionModificationDetail As List(Of CollectionModificationDetail)
        Get
            Return _listCollectionModificationDetail
        End Get
    End Property
#End Region

#Region "HANDLES"
    ''' <summary>
    ''' Libera la memoria del visor 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listCollectionDetail = Nothing
        _listCollectionModificationDetail = Nothing
        _validatyId = Nothing
    End Sub
    ''' <summary>
    ''' Abre o cierra el Popup de colecciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupCollection_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Enter Then
            GetListCollectionModificationDetail()
        End If
    End Sub
    ''' <summary>
    ''' Muestra el popup de colecciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupCollection_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MCollectionModification(Me.Tag)
            _listCollectionDetail = model.ListCollectionDetailByCollectionIdAndValidatyId(_collectionId, _validatyId)

            If _listCollectionDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron rubros para esa vigencia"
                IndigoGridControl1.RefreshGrid(INDGcCollection)
                Exit Sub
            End If
            INDGcCollection.DataSource = _listCollectionDetail
            INDGcCollection.Focus()
        End Using
    End Sub
    ''' <summary>
    ''' Click en agregar(abre el detalle de colecciones)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        GetListCollectionModificationDetail()
    End Sub
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Obtiene la lista de detalles dde las modificaciones de las coleccciones 
    ''' </summary>
    Private Sub GetListCollectionModificationDetail()
        If INDGvCollection.SelectedRowsCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado ningun rubro"
            Exit Sub
        End If

        For Each item In INDGvCollection.GetSelectedRows()
            Dim collectionDetail = _listCollectionDetail((INDGvCollection.GetDataSourceRowIndex(item)))
            Dim collectionModificationDetail As New CollectionModificationDetail
            With collectionModificationDetail
                .CollectionDetailId = collectionDetail.Id
                .CodeRecognition = collectionDetail.CollectionId.Code
                .CodeNameCategory = collectionDetail.RecognitionDetailId.CategoryId.NameCode
                .CodeNameFinancialSource = collectionDetail.RecognitionDetailId.CategoryId.FinancialSourceId.NameCode
                .CategoryType = collectionDetail.RecognitionDetailId.CategoryId.ItemType
                .RecognitionBalance = collectionDetail.RecognitionDetailId.Balance
                .CollectionBalance = collectionDetail.Balance
                .Nature = 1
            End With
            If _listCollectionModificationDetail Is Nothing Then
                _listCollectionModificationDetail = New List(Of CollectionModificationDetail)
            End If
            _listCollectionModificationDetail.Add(collectionModificationDetail)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub
#End Region
End Class