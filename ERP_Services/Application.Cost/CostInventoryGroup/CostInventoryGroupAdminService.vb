#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Text
Imports Application.Cost

#End Region

Public Class CostInventoryGroupAdminService
    Implements ICostInventoryGroupAdminService

#Region "Properties"

    Private _CostInventoryGroupRepository As ICostInventoryGroupRepository

#End Region

#Region "Builder"

    Public Sub New(CostInventoryGroupRepository As ICostInventoryGroupRepository)
        If CostInventoryGroupRepository Is Nothing Then
            Throw New ArgumentNullException("CostInventoryGroupRepository")
        End If

        Me._CostInventoryGroupRepository = CostInventoryGroupRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostInventoryGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostInventoryGroupAdminService.GetCostInventoryGroupByCode
        Try
            Dim CostInventoryGroup As CostInventoryGroup = Me._CostInventoryGroupRepository.GetCostInventoryGroupByCode(code)
            Return New ActionResult(Of CostInventoryGroup) With {.StateResult = True, .ObjectEmbbeded = CostInventoryGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostInventoryGroup) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveCostInventoryGroup(CostInventoryGroup As CostInventoryGroup, listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail), audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostInventoryGroupAdminService.SaveCostInventoryGroup
        If CostInventoryGroup Is Nothing Then
            Throw New ArgumentNullException("CostInventoryGroup")
        End If

        Dim UnitOfWork As IUnitWork = _CostInventoryGroupRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim CostInventoryGroupXml As String = ConvertToXmlCostInventoryGroup(CostInventoryGroup)
                Dim listCostInventoryGroupDetailXml As String = ConvertToXmlListCostInventoryGroupDetail(listCostInventoryGroupDetail)

                Dim resultStore = Me._CostInventoryGroupRepository.SP_SaveCostInventoryGroup(CostInventoryGroupXml, listCostInventoryGroupDetailXml, audit.CodeUser)
                If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CostInventoryGroup) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NoSaved"), resultStore(0).Message)}
                End If

                CostInventoryGroup.Id = resultStore(0).Id
                CostInventoryGroup.Code = resultStore(0).Code

                transaction.Complete()
                Return New ActionResult(Of CostInventoryGroup) With {.StateResult = True, .ObjectEmbbeded = CostInventoryGroup, .Message = String.Format(ResourceManager.GetString("SavedWithCode"), CostInventoryGroup.Code)}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostInventoryGroup) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NoSaved"), Utils.GetInnerExceptionMessageToString(ex))}
            End Try
        End Using
    End Function

    Public Function ChangeStateCostInventoryGroup(CostInventoryGroup As CostInventoryGroup, audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostInventoryGroupAdminService.ChangeStateCostInventoryGroup
        If CostInventoryGroup Is Nothing Then
            Throw New ArgumentNullException("CostInventoryGroup")
        End If

        Dim UnitOfWork As IUnitWork = _CostInventoryGroupRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim entity As CostInventoryGroup = Me._CostInventoryGroupRepository.GetCostInventoryGroupById(CostInventoryGroup.Id)

                If CostInventoryGroup.Status Then
                    Dim errors = _CostInventoryGroupRepository.ValidateBeforeActive(entity.Id)
                    If errors.Length > 0 Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of CostInventoryGroup) With {.StateResult = False, .Message = String.Format("Los siguientes productos ya fueron asociados: " & Environment.NewLine & "{0}", errors)}
                    End If
                End If

                entity.Status = CostInventoryGroup.Status
                entity.ModificationUser = audit.CodeUser
                entity.ModificationDate = DateTime.Now

                Me._CostInventoryGroupRepository.SaveEntity(entity)
                unitOfWork.Commit()
                transaction.Complete()
                Return New ActionResult(Of CostInventoryGroup) With {.StateResult = True, .ObjectEmbbeded = entity}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostInventoryGroup) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "Copy & Paste"

    Function CopyAndPasteCostInventoryGroupDetail(data As List(Of List(Of String))) As ActionResult(Of List(Of CostInventoryGroupDetail)) Implements ICostInventoryGroupAdminService.CopyAndPasteCostInventoryGroupDetail
        If data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostInventoryGroupDetail As New List(Of CostInventoryGroupDetail)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteInventory(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._CostInventoryGroupRepository.SP_CopyAndPasteCostInventoryGroupDetail(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostInventoryGroupDetail.Add(New CostInventoryGroupDetail With
                        {
                            .InventoryProductId = itemXml.InventoryProductId,
                            .InventoryProductCodeName = String.Format("{0} - {1}", itemXml.InventoryProductCode, itemXml.InventoryProductName),
                            .MeasurementUnitCodeName = String.Format("{0} - {1}", itemXml.MeasurementUnitCode, itemXml.MeasurementUnitName),
                            .Quantity = itemXml.Quantity
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostInventoryGroupDetail)) With {.StateResult = True, .ObjectEmbbeded = ListCostInventoryGroupDetail, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostInventoryGroupDetail)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostInventoryGroupDetail)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostInventoryGroupDetail)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#End Region

#Region "Function Privates"

    Private Function ConvertToXmlCostInventoryGroup(CostInventoryGroup As CostInventoryGroup) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CostInventoryGroup>")

        builder.Append("<Id>" & CostInventoryGroup.Id & "</Id>")
        builder.Append("<Code>" & CostInventoryGroup.Code & "</Code>")
        builder.Append("<Name>" & CostInventoryGroup.Name & "</Name>")
        builder.Append("<OperatingUnitId>" & CostInventoryGroup.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<InventoryMeasurementUnitId>" & CostInventoryGroup.InventoryMeasurementUnitId & "</InventoryMeasurementUnitId>")
        builder.Append("<Description>" & CostInventoryGroup.Description & "</Description>")

        builder.Append("</CostInventoryGroup>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostInventoryGroupDetail(list As List(Of CostInventoryGroupDetail)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostInventoryGroupDetail>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostInventoryGroupDetail>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<InventoryProductId>" & item.InventoryProductId & "</InventoryProductId>")
                builder.Append("<Quantity>" & item.Quantity.ToString().Replace(",", ".") & "</Quantity>")
                builder.Append("</CostInventoryGroupDetail>")
            Next
        End If

        builder.Append("</ListCostInventoryGroupDetail>")

        Return builder.ToString()
    End Function

#Region "Copy & Paste"

    Private Function ConvertToXmlCopyAndPasteInventory(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<InventoryProductCode>" & item(0) & "</InventoryProductCode>")
            builder.Append("<Quantity>" & item(1).ToString.Replace(",", ".") & "</Quantity>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _CostInventoryGroupRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
