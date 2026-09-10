#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrInventoryProduct

#Region "Variables"

    Private WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#Region "Properties"

    Public Property EditMode As Boolean

    Public Property CostInventoryGroupDetail As CostInventoryGroupDetail

    Public Property ListCostInventoryGroupDetail As List(Of CostInventoryGroupDetail)

    Private Property InventoryProductId As Integer
        Get
            Return INDsleInventoryProduct.EditValue
        End Get
        Set(value As Integer)
            INDsleInventoryProduct.EditValue = value
        End Set
    End Property

    Private Property InventoryProductCodeName As String
        Get
            Return If(String.IsNullOrEmpty(INDsleInventoryProduct.Text), INDsleInventoryProduct.Properties.NullText, INDsleInventoryProduct.Text)
        End Get
        Set(value As String)
            INDsleInventoryProduct.Properties.NullText = value
        End Set
    End Property

    Private Property MeasurementUnitCodeName As String
        Get
            Return INDteMeasurementUnit.EditValue
        End Get
        Set(value As String)
            INDteMeasurementUnit.EditValue = value
        End Set
    End Property

    Private Property Quantity As Decimal
        Get
            Return INDseQuantity.EditValue
        End Get
        Set(value As Decimal)
            INDseQuantity.EditValue = value
        End Set
    End Property

#End Region

#Region "XPO"

    Private Property InventoryProductXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleInventoryProduct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleInventoryProduct.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostInventoryGroupDetail(ByVal CostInventoryGroupDetail As CostInventoryGroupDetail)

    Private Sub CtrInventoryProduct_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Private Sub INDsleCostInventoryGroupDetail_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInventoryProduct.EditValueChanged
        If Not EditMode AndAlso InventoryProductId > 0 Then
            Dim product As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo = Nothing

            If INDgvInventory.GetFocusedRow Is Nothing Then
                product = Me.GetInventoryProductById(InventoryProductId)
            Else
                product = DirectCast(DirectCast(INDgvInventory.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
            End If

            If product.MeasurementUnitId IsNot Nothing Then
                MeasurementUnitCodeName = product.MeasurementUnitId.CodeName
            End If
        End If
    End Sub

    Private Sub INDsleCostInventoryGroupDetail_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleInventoryProduct.QueryPopUp
        If InventoryProductXpo Is Nothing Then
            Using model As New MBusqueda
                InventoryProductXpo = model.ConsultarEntidades(eDataSource.ListInventoryProduct)
            End Using
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            CostInventoryGroupDetail.InventoryProductId = InventoryProductId
            CostInventoryGroupDetail.InventoryProductCodeName = InventoryProductCodeName
            CostInventoryGroupDetail.MeasurementUnitCodeName = MeasurementUnitCodeName
            CostInventoryGroupDetail.Quantity = Quantity
            RaiseEvent AddCostInventoryGroupDetail(_CostInventoryGroupDetail)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        _CostInventoryGroupDetail = New CostInventoryGroupDetail()
        INDsleInventoryProduct.Properties.ReadOnly = False
        INDsleInventoryProduct.EditValue = Nothing
        INDsleInventoryProduct.Properties.NullText = String.Empty
        MeasurementUnitCodeName = String.Empty
        Quantity = 0
        INDsleInventoryProduct.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True
        INDsleInventoryProduct.Properties.ReadOnly = True
        Me.InventoryProductId = Me.CostInventoryGroupDetail.InventoryProductId
        Me.InventoryProductCodeName = Me.CostInventoryGroupDetail.InventoryProductCodeName
        Me.MeasurementUnitCodeName = Me.CostInventoryGroupDetail.MeasurementUnitCodeName
        Me.Quantity = Me.CostInventoryGroupDetail.Quantity
        INDsleInventoryProduct.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If INDsleInventoryProduct.EditValue Is Nothing Then
            errorList.AppendLine("Producto")
        End If

        If INDseQuantity.EditValue <= 0 Then
            errorList.AppendLine("Cantidad")
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Hay Campos sin diligenciar: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        If CostInventoryGroupDetail.InventoryProductId <> InventoryProductId AndAlso ListCostInventoryGroupDetail.Any(Function(d) d.InventoryProductId = InventoryProductId) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Producto " + CostInventoryGroupDetail.InventoryProductCodeName + " ya existe en la lista."
            Return False
        End If

        Return True
    End Function

    Public Function GetInventoryProductById(id As Integer) As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo
        Dim filtroConsulta As String = "Id = " & id
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.GetCollection(Of Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)(Nothing, filtroConsulta)(0)
    End Function

#End Region

End Class