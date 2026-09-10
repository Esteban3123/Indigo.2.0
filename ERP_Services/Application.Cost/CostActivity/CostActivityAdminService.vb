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

Public Class CostActivityAdminService
    Implements ICostActivityAdminService

#Region "Properties"

    Private _costActivityRepository As ICostActivityRepository

#End Region

#Region "Builder"

    Public Sub New(costActivityRepository As ICostActivityRepository)
        If costActivityRepository Is Nothing Then
            Throw New ArgumentNullException("costActivityRepository")
        End If

        Me._costActivityRepository = costActivityRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostActivityById(id As Integer) As ActionResult(Of CostActivity) Implements ICostActivityAdminService.GetCostActivityById
        Try
            Dim costActivity As CostActivity = Me._costActivityRepository.GetCostActivityByIdWithAggregates(id)
            Return New ActionResult(Of CostActivity) With {.StateResult = True, .ObjectEmbbeded = costActivity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetCostActivityByCode(code As String, audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostActivityAdminService.GetCostActivityByCode
        Try
            Dim costActivity As CostActivity = Me._costActivityRepository.GetCostActivityByCode(code)
            Return New ActionResult(Of CostActivity) With {.StateResult = True, .ObjectEmbbeded = costActivity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveCostActivity(costActivity As CostActivity, 
                                        listCostActivityProductionCenter As List(Of CostActivityProductionCenter), listCostActivityStep As List(Of CostActivityStep), 
                                        listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset), listCostActivityStepPayroll As List(Of CostActivityStepPayroll),
                                        listCostActivityStepInventory As List(Of CostActivityStepInventory), listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost),
                                        audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostActivityAdminService.SaveCostActivity
        If costActivity Is Nothing Then
            Throw New ArgumentNullException("costActivity")
        End If

        Dim UnitOfWork As IUnitWork = _costActivityRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim costActivityXml As String = ConvertToXmlCostActivity(costActivity)
                Dim listCostActivityProductionCenterXml As String = ConvertToXmlListCostActivityProductionCenter(listCostActivityProductionCenter)
                Dim listCostActivityStepXml As String = ConvertToXmlListCostActivityStep(listCostActivityStep)
                Dim listCostActivityStepFixedAssetXml As String = ConvertToXmlListCostActivityStepFixedAsset(listCostActivityStepFixedAsset)
                Dim listCostActivityStepPayrollXml As String = ConvertToXmlListCostActivityStepPayroll(listCostActivityStepPayroll)
                Dim listCostActivityStepInventoryXml As String = ConvertToXmlListCostActivityStepInventory(listCostActivityStepInventory)
                Dim listCostActivityStepAddictionalCostXml As String = ConvertToXmlListCostActivityStepAddictionalCost(listCostActivityStepAddictionalCost)

                Dim resultStore = Me._costActivityRepository.SP_SaveCostActivity(costActivityXml, listCostActivityProductionCenterXml, listCostActivityStepXml, listCostActivityStepFixedAssetXml, listCostActivityStepPayrollXml, listCostActivityStepInventoryXml, listCostActivityStepAddictionalCostXml, audit.CodeUser)
                If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NoSaved"), resultStore(0).Message)}
                End If

                costActivity.Id = resultStore(0).Id
                costActivity.Code = resultStore(0).Code

                transaction.Complete()
                Return New ActionResult(Of CostActivity) With {.StateResult = True, .ObjectEmbbeded = costActivity, .Message = String.Format(ResourceManager.GetString("SavedWithCode"), costActivity.Code)}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NoSaved"), Utils.GetInnerExceptionMessageToString(ex))}
            End Try
        End Using
    End Function

    Public Function ChangeStateCostActivity(CostActivity As CostActivity, audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostActivityAdminService.ChangeStateCostActivity
        If costActivity Is Nothing Then
            Throw New ArgumentNullException("costActivity")
        End If

        Dim UnitOfWork As IUnitWork = _costActivityRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim entity As CostActivity = Me._costActivityRepository.GetCostActivityById(CostActivity.Id)

                If CostActivity.Status Then
                    Dim errors = _costActivityRepository.ValidateBeforeActive(entity.Id, entity.CUPSEntityId)
                    If errors.Length > 0 Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = String.Format("Los siguientes centros de producción se encuentran en otra actividad activa: " & Environment.NewLine & "{0}", errors)}
                    End If
                End If

                entity.Status = CostActivity.Status
                entity.ModificationUser = audit.CodeUser
                entity.ModificationDate = DateTime.Now

                Me._costActivityRepository.SaveEntity(entity)
                unitOfWork.Commit()
                transaction.Complete()
                Return New ActionResult(Of CostActivity) With {.StateResult = True, .ObjectEmbbeded = entity}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostActivity) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "Copy & Paste"

    Function CopyAndPasteProductionCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityProductionCenter)) Implements ICostActivityAdminService.CopyAndPasteProductionCenter
        If data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostActivityProductionCenter As New List(Of CostActivityProductionCenter)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteProductionCenter(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._costActivityRepository.SP_CostActivityCopyAndPasteProductionCenter(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostActivityProductionCenter.Add(New CostActivityProductionCenter With
                        {
                            .CostProductionCenterId = itemXml.CostProductionCenterId,
                            .CostProductionCenterCodeName = String.Format("{0} - {1}", itemXml.CostProductionCenterCode, itemXml.CostProductionCenterName)
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostActivityProductionCenter)) With {.StateResult = True, .ObjectEmbbeded = ListCostActivityProductionCenter, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostActivityProductionCenter)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostActivityProductionCenter)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostActivityProductionCenter)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Function CopyAndPasteFixedAsset(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepFixedAsset)) Implements ICostActivityAdminService.CopyAndPasteFixedAsset
        If data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostActivityStepFixedAsset As New List(Of CostActivityStepFixedAsset)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteFixedAsset(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._costActivityRepository.SP_CostActivityCopyAndPasteFixedAsset(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostActivityStepFixedAsset.Add(New CostActivityStepFixedAsset With
                        {
                            .CostActivityStepId = itemXml.CostActivityStepId,
                            .FixedAssetItemId = itemXml.FixedAssetItemId,
                            .FixedAssetItemCodeName = String.Format("{0} - {1}", itemXml.FixedAssetItemCode, itemXml.FixedAssetItemName),
                            .Hours = itemXml.Hours
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostActivityStepFixedAsset)) With {.StateResult = True, .ObjectEmbbeded = ListCostActivityStepFixedAsset, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostActivityStepFixedAsset)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostActivityStepFixedAsset)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostActivityStepFixedAsset)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Function CopyAndPastePayroll(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepPayroll)) Implements ICostActivityAdminService.CopyAndPastePayroll
        If data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostActivityStepPayroll As New List(Of CostActivityStepPayroll)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPastePayroll(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._costActivityRepository.SP_CostActivityCopyAndPastePayroll(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostActivityStepPayroll.Add(New CostActivityStepPayroll With
                        {
                            .CostActivityStepId = itemXml.CostActivityStepId,
                            .PayrollPositionId = itemXml.PayrollPositionId,
                            .PositionCodeName = String.Format("{0} - {1}", itemXml.PayrollPositionCode, itemXml.PayrollPositionName),
                            .Hours = itemXml.Hours
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostActivityStepPayroll)) With {.StateResult = True, .ObjectEmbbeded = ListCostActivityStepPayroll, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostActivityStepPayroll)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostActivityStepPayroll)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostActivityStepPayroll)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Function CopyAndPasteInventory(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepInventory)) Implements ICostActivityAdminService.CopyAndPasteInventory
        If data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostActivityStepInventory As New List(Of CostActivityStepInventory)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteInventory(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._costActivityRepository.SP_CostActivityCopyAndPasteInventory(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostActivityStepInventory.Add(New CostActivityStepInventory With
                        {
                            .CostActivityStepId = itemXml.CostActivityStepId,
                            .CostInventoryGroupId = itemXml.CostInventoryGroupId,
                            .CostInventoryGroupCodeName = String.Format("{0} - {1}", itemXml.CostInventoryGroupCode, itemXml.CostInventoryGroupName),
                            .MeasurementUnitCodeName = String.Format("{0} - {1}", itemXml.MeasurementUnitCode, itemXml.MeasurementUnitName),
                            .Quantity = itemXml.Quantity
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostActivityStepInventory)) With {.StateResult = True, .ObjectEmbbeded = ListCostActivityStepInventory, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostActivityStepInventory)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostActivityStepInventory)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostActivityStepInventory)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#End Region

#Region "Function Privates"

    Private Function ConvertToXmlCostActivity(costActivity As CostActivity) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CostActivity>")

        builder.Append("<Id>" & costActivity.Id & "</Id>")
        builder.Append("<Code>" & costActivity.Code & "</Code>")
        builder.Append("<Name>" & costActivity.Name & "</Name>")
        builder.Append("<OperatingUnitId>" & costActivity.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<CUPSEntityId>" & costActivity.CUPSEntityId & "</CUPSEntityId>")
        builder.Append("<InitialDate>" & costActivity.InitialDate.ToString("dd/MM/yyyy") & "</InitialDate>")
        builder.Append("<EndDate>" & costActivity.EndDate.ToString("dd/MM/yyyy") & "</EndDate>")
        builder.Append("<Description>" & costActivity.Description & "</Description>")

        builder.Append("</CostActivity>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityProductionCenter(list As List(Of CostActivityProductionCenter)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityProductionCenter>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityProductionCenter>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<CostProductionCenterId>" & item.CostProductionCenterId & "</CostProductionCenterId>")
                builder.Append("</CostActivityProductionCenter>")
            Next
        End If

        builder.Append("</ListCostActivityProductionCenter>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityStep(list As List(Of CostActivityStep)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityStep>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityStep>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<UUID>" & item.UUID & "</UUID>")
                builder.Append("<Order>" & item.Order & "</Order>")
                builder.Append("<Description>" & item.Description & "</Description>")
                builder.Append("</CostActivityStep>")
            Next
        End If

        builder.Append("</ListCostActivityStep>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityStepFixedAsset(list As List(Of CostActivityStepFixedAsset)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityStepFixedAsset>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityStepFixedAsset>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<ParentUUID>" & item.ParentUUID & "</ParentUUID>")
                builder.Append("<CostActivityStepId>" & item.CostActivityStepId & "</CostActivityStepId>")
                builder.Append("<FixedAssetItemId>" & item.FixedAssetItemId & "</FixedAssetItemId>")
                builder.Append("<Hours>" & item.Hours.ToString().Replace(",", ".") & "</Hours>")
                builder.Append("</CostActivityStepFixedAsset>")
            Next
        End If

        builder.Append("</ListCostActivityStepFixedAsset>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityStepPayroll(list As List(Of CostActivityStepPayroll)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityStepPayroll>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityStepPayroll>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<ParentUUID>" & item.ParentUUID & "</ParentUUID>")
                builder.Append("<CostActivityStepId>" & item.CostActivityStepId & "</CostActivityStepId>")
                builder.Append("<PayrollPositionId>" & item.PayrollPositionId & "</PayrollPositionId>")
                builder.Append("<Hours>" & item.Hours.ToString().Replace(",", ".") & "</Hours>")
                builder.Append("</CostActivityStepPayroll>")
            Next
        End If

        builder.Append("</ListCostActivityStepPayroll>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityStepInventory(list As List(Of CostActivityStepInventory)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityStepInventory>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityStepInventory>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<ParentUUID>" & item.ParentUUID & "</ParentUUID>")
                builder.Append("<CostActivityStepId>" & item.CostActivityStepId & "</CostActivityStepId>")
                builder.Append("<CostInventoryGroupId>" & item.CostInventoryGroupId & "</CostInventoryGroupId>")
                builder.Append("<Quantity>" & item.Quantity.ToString().Replace(",", ".") & "</Quantity>")
                builder.Append("</CostActivityStepInventory>")
            Next
        End If

        builder.Append("</ListCostActivityStepInventory>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListCostActivityStepAddictionalCost(list As List(Of CostActivityStepAddictionalCost)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListCostActivityStepAddictionalCost>")

        If list IsNot Nothing Then
            For Each item In list
                builder.Append("<CostActivityStepAddictionalCost>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<ParentUUID>" & item.ParentUUID & "</ParentUUID>")
                builder.Append("<CostActivityStepId>" & item.CostActivityStepId & "</CostActivityStepId>")
                builder.Append("<Description>" & item.Description & "</Description>")
                builder.Append("<Value>" & item.Value.ToString().Replace(",", ".") & "</Value>")
                builder.Append("</CostActivityStepAddictionalCost>")
            Next
        End If

        builder.Append("</ListCostActivityStepAddictionalCost>")

        Return builder.ToString()
    End Function

#Region "Copy & Paste"

    Private Function ConvertToXmlCopyAndPasteProductionCenter(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CostProductionCenterCode>" & item(0) & "</CostProductionCenterCode>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyAndPasteFixedAsset(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CostActivityStepId>" &  item(0) & "</CostActivityStepId>")
            builder.Append("<FixedAssetItemCode>" &  item(1) & "</FixedAssetItemCode>")
            builder.Append("<Hours>" &  item(2).ToString.Replace(",", ".") & "</Hours>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyAndPastePayroll(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CostActivityStepId>" & item(0) & "</CostActivityStepId>")
            builder.Append("<PayrollPositionCode>" & item(1) & "</PayrollPositionCode>")
            builder.Append("<Hours>" & item(2).ToString.Replace(",", ".") & "</Hours>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyAndPasteInventory(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CostActivityStepId>" & item(0) & "</CostActivityStepId>")
            builder.Append("<CostInventoryGroupCode>" & item(1) & "</CostInventoryGroupCode>")
            builder.Append("<Quantity>" & item(2).ToString.Replace(",", ".") & "</Quantity>")

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

            _costActivityRepository = Nothing
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
