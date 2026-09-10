#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrCostActivityStepInventory

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

    Public Property CostActivityStepInventory As CostActivityStepInventory

    Public Property ListCostActivityStepInventory As List(Of CostActivityStepInventory)

    Private CostActivityStepUUID As String

    Private Property CostActivityStepId As Integer

    Private Property CostActivityStepOrderDescription As String

    Private Property CostInventoryGroupId As Integer
        Get
            Return INDsleCostActivityStepInventory.EditValue
        End Get
        Set(value As Integer)
            INDsleCostActivityStepInventory.EditValue = value
        End Set
    End Property

    Private Property CostInventoryGroupCodeName As String
        Get
            Return If(String.IsNullOrEmpty(INDsleCostActivityStepInventory.Text), INDsleCostActivityStepInventory.Properties.NullText, INDsleCostActivityStepInventory.Text)
        End Get
        Set(value As String)
            INDsleCostActivityStepInventory.Properties.NullText = value
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

    Public Property ListCostActivityStep As List(Of CostActivityStep)
        Get
            Return CType(INDsleCostActivityStep.Properties.DataSource, List(Of CostActivityStep))
        End Get
        Set(value As List(Of CostActivityStep))
            INDsleCostActivityStep.Properties.DataSource = Nothing
            INDsleCostActivityStep.Properties.DataSource = value
        End Set
    End Property

    Private Property CostInventoryGroupXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostActivityStepInventory.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostActivityStepInventory.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityStepInventory(ByVal CostActivityStepInventory As CostActivityStepInventory)

    Private Sub CtrCostActivityStepInventory_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Private Sub INDsleCostActivityStep_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostActivityStep.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleCostActivityStep.EditValue) Then
            If INDgvCostActivityStep.GetFocusedRow IsNot Nothing
                Dim activityStep = DirectCast(INDgvCostActivityStep.GetFocusedRow, CostActivityStep)
                CostActivityStepUUID = activityStep.UUID
                CostActivityStepId = activityStep.Id
                CostActivityStepOrderDescription = activityStep.OrderDescription
            End If
        End If
    End Sub

    Private Sub INDsleCostActivityStepInventory_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostActivityStepInventory.EditValueChanged
        If Not EditMode AndAlso CostInventoryGroupId > 0 Then
            Dim product As Infrastructure.Data.Xpo.CostRepository.CostInventoryGroupXpo = Nothing

            If INDgvInventory.GetFocusedRow IsNot Nothing Then
                product = DirectCast(DirectCast(INDgvInventory.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CostRepository.CostInventoryGroupXpo)
            Else
                product = Me.GetCostInventoryGroupById(CostInventoryGroupId)
            End If

            MeasurementUnitCodeName = product.InventoryMeasurementUnitId.CodeName
        End If
    End Sub

    Private Sub INDsleCostActivityStepInventory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostActivityStepInventory.QueryPopUp
        If CostInventoryGroupXpo Is Nothing Then
            Using model As New MBusqueda
                CostInventoryGroupXpo = model.ConsultarEntidades(eDataSource.ListCostInventoryGroups)
            End Using
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            CostActivityStepInventory.ParentUUID = CostActivityStepUUID
            CostActivityStepInventory.CostActivityStepId = CostActivityStepId
            CostActivityStepInventory.CostActivityStepOrderDescription = CostActivityStepOrderDescription
            CostActivityStepInventory.CostInventoryGroupId = CostInventoryGroupId
            CostActivityStepInventory.CostInventoryGroupCodeName = CostInventoryGroupCodeName            
            CostActivityStepInventory.MeasurementUnitCodeName = MeasurementUnitCodeName
            CostActivityStepInventory.Quantity = Quantity
            RaiseEvent AddCostActivityStepInventory(_CostActivityStepInventory)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        _CostActivityStepInventory = New CostActivityStepInventory()
        CostActivityStepUUID = Nothing
        CostActivityStepId = Nothing
        INDsleCostActivityStep.EditValue = Nothing
        INDsleCostActivityStep.Properties.NullText = String.Empty
        INDsleCostActivityStepInventory.Properties.ReadOnly = False
        INDsleCostActivityStepInventory.EditValue = Nothing
        INDsleCostActivityStepInventory.Properties.NullText = String.Empty        
        MeasurementUnitCodeName = String.Empty
        Quantity = 0
        INDsleCostActivityStep.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True
        Me.CostActivityStepUUID = Me.CostActivityStepInventory.ParentUUID
        Me.CostActivityStepId = Me.CostActivityStepInventory.CostActivityStepId
        Me.CostActivityStepOrderDescription = Me.CostActivityStepInventory.CostActivityStepOrderDescription
        INDsleCostActivityStep.Properties.NullText = Me.CostActivityStepOrderDescription
        INDsleCostActivityStepInventory.Properties.ReadOnly = True
        Me.CostInventoryGroupId = Me.CostActivityStepInventory.CostInventoryGroupId
        Me.CostInventoryGroupCodeName = Me.CostActivityStepInventory.CostInventoryGroupCodeName
        Me.MeasurementUnitCodeName = Me.CostActivityStepInventory.MeasurementUnitCodeName
        Me.Quantity = Me.CostActivityStepInventory.Quantity
        INDsleCostActivityStep.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If String.IsNullOrEmpty(Me.CostActivityStepUUID) AndAlso Me.CostActivityStepId = 0 Then
            errorList.AppendLine("Paso")
        End If

        If INDsleCostActivityStepInventory.EditValue Is Nothing Then
            errorList.AppendLine("Grupo de Productos")
        End If

        If INDseQuantity.EditValue <= 0 Then
            errorList.AppendLine("Cantidad")
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Hay Campos sin diligenciar: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        If CostActivityStepInventory.CostInventoryGroupId <> CostInventoryGroupId AndAlso ListCostActivityStepInventory.Any(Function(d) d.CostInventoryGroupId = CostInventoryGroupId) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Grupo de Productos " + CostActivityStepInventory.CostInventoryGroupCodeName + " ya existe en la lista."
            Return False
        End If

        Return True
    End Function

    Public Function GetCostInventoryGroupById(id As Integer) As Infrastructure.Data.Xpo.CostRepository.CostInventoryGroupXpo
        Dim filtroConsulta As String = "Id = " & id
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CostService.GetCollection(Of Infrastructure.Data.Xpo.CostRepository.CostInventoryGroupXpo)(Nothing, filtroConsulta)(0)
    End Function

#End Region

End Class