'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 21-09-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.MixingStation
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region

Public Class DefectClassificationItemLineService

    Implements IDefectClassificationItemLineService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmDefects"
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _defectClassificationItemRepository As IDefectClassificationItemRepository

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _defectsUnitDoseTypeRepository As IDefectsUnitRepository
    ''' <summary>
    ''' Repositorio de secuencias numéricas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository
    ''' <summary>
    ''' Repository of cm external care center
    ''' </summary>
    Private ReadOnly _cMExternalCareCenterRepository As ICMExternalCareCenterRepository
    ''' <summary>
    ''' Repository of CMCenterAttention
    ''' </summary>
    Private ReadOnly _cmCenterAttentionRepository As ICMCenterAttentionRepository
#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal defectClassificationItemRepository As IDefectClassificationItemRepository,
                   cmCenterAttentionRepository As ICMCenterAttentionRepository,
                   ByVal defectsUnitDoseTypeRepository As IDefectsUnitRepository,
                   ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository,
                   cMExternalCareCenterRepository As ICMExternalCareCenterRepository)
        If defectClassificationItemRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If defectsUnitDoseTypeRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If

        Me._defectClassificationItemRepository = defectClassificationItemRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
        _cmCenterAttentionRepository = cmCenterAttentionRepository
        _cMExternalCareCenterRepository = cMExternalCareCenterRepository
        Me._defectsUnitDoseTypeRepository = defectsUnitDoseTypeRepository
    End Sub

    ''' <summary>
    ''' Trae todos los defectos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllDefectClassificationItem(audit As AuditMessage) As List(Of DefectClassificationItem) Implements IDefectClassificationItemLineService.ListAllDefectClassificationItem
        Try
            Dim defectClassificationItem = Me._defectClassificationItemRepository.GetAll()
            For Each item As DefectClassificationItem In defectClassificationItem
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefectClassificationItem)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return defectClassificationItem
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una linea de produccion
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDefectClassificationItem(defectClassificationItem As DefectClassificationItem, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DefectClassificationItem) Implements IDefectClassificationItemLineService.SaveDefectClassificationItem
        If defectClassificationItem Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._defectClassificationItemRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(defectClassificationItem.Code) Then
                    Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            defectClassificationItem.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DefectClassificationItem) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), defectClassificationItem.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DefectClassificationItem) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As DefectClassificationItem = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DefectClassificationItem)
                Dim status As Integer

                If defectClassificationItem.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    defectClassificationItem.CreationUser = audit.CodeUser
                    defectClassificationItem.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = defectClassificationItem.OriginalValue
                    defectClassificationItem.ModificationUser = audit.CodeUser
                    defectClassificationItem.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._defectClassificationItemRepository.SaveEntity(defectClassificationItem)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DefectClassificationItem)(defectClassificationItem, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                defectClassificationItem.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DefectClassificationItem) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = defectClassificationItem, .Message = MessageResult}
            End Using
        Catch ex As IndigoValidationException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveUnitDoseType(defectsUnitDoseType As DefectsUnitDoseType) As ActionResult(Of DefectsUnitDoseType) Implements IDefectClassificationItemLineService.SaveUnitDoseType
        If defectsUnitDoseType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._defectsUnitDoseTypeRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(defectsUnitDoseType.Id) Then
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ProductionLineUnitDoseType = Nothing
                'Dim auditProcess As IndigoAuditSimpleEntity(Of ProductionLine)
                'Dim status As Integer

                Me._defectsUnitDoseTypeRepository.SaveEntity(defectsUnitDoseType)
                unitOfWork.Commit()
                'sequenseUnitOfWork.Commit()
                'auditProcess = New IndigoAuditSimpleEntity(Of ProductionLine)(ProductionLine, audit, status, auxObjEntity)
                'auditProcess.Execute()

                'Se marca la entidad como sin cambios
                defectsUnitDoseType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DefectsUnitDoseType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = defectsUnitDoseType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DefectsUnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectsUnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un defecto
    ''' </summary>
    ''' <param name="defects">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function DeleteDefectClassificationItem(defectClassificationItem As DefectClassificationItem, audit As AuditMessage) As ActionResult Implements IDefectClassificationItemLineService.DeleteDefectClassificationItem
        If defectClassificationItem Is Nothing Then
            Throw New ArgumentNullException("Defects")
        End If
        Dim unitOfWork As IUnitWork = Me._defectClassificationItemRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                defectClassificationItem.ModificationUser = audit.CodeUser
                defectClassificationItem.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DefectClassificationItem)(defectClassificationItem, audit, status)

                If defectClassificationItem.DefectsUnitDoseType IsNot Nothing Then
                    defectClassificationItem.DefectsUnitDoseType.ToList().ForEach(Sub(x)
                                                                                      x.MarkAsDeleted()
                                                                                  End Sub)
                End If
                defectClassificationItem.MarkAsDeleted()
                Me._defectClassificationItemRepository.SaveEntity(defectClassificationItem)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}

        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}

        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}

        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function UpdateDefectClassificationItem(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IDefectClassificationItemLineService.UpdateDefectClassificationItem
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim defectClassificationItem As DefectClassificationItem = Me._defectClassificationItemRepository.GetDefectClassificationItemByCode(code)
            If defectClassificationItem IsNot Nothing AndAlso defectClassificationItem.Id > 0 Then
                defectClassificationItem.State = state
            End If
            Return Me.SaveDefectClassificationItem(defectClassificationItem, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectClassificationItem) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function GetDefectClassificationItemByCode(code As String, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IDefectClassificationItemLineService.GetDefectClassificationItemByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim defectClassificationItem As DefectClassificationItem = Me._defectClassificationItemRepository.GetDefectClassificationItemByCode(code)
            If defectClassificationItem IsNot Nothing AndAlso defectClassificationItem.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefectClassificationItem)(defectClassificationItem, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = True, .ObjectEmbbeded = defectClassificationItem}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetDefectClassificationItemById(id As String, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IDefectClassificationItemLineService.GetDefectClassificationItemById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim defectClassificationItem As DefectClassificationItem = Me._defectClassificationItemRepository.GetDefectClassificationItemById(id)
            If defectClassificationItem IsNot Nothing AndAlso defectClassificationItem.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefectClassificationItem)(defectClassificationItem, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = True, .ObjectEmbbeded = defectClassificationItem}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectClassificationItem) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function ListAllDefectsUnitDoseType(Id_DefectsClassificationItem As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements IDefectClassificationItemLineService.ListAllDefectsUnitDoseType
        If Id_DefectsClassificationItem = 0 Then
            Throw New ArgumentNullException("Id_DefectsClassificationItem")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim defectsLineUnit = Me._defectsUnitDoseTypeRepository.ListAllDefectsUnitDoseType(Id_DefectsClassificationItem)

            Return defectsLineUnit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ValidateDefectsUnitDoseType(defectClassificationItemId As Integer, unitDoseTypeId As Integer) As ActionResult Implements IDefectClassificationItemLineService.ValidateDefectsUnitDoseType
        Try
            Dim cmExternals = _cMExternalCareCenterRepository _
                        .Query(Function(m) m.ProductionLine.ProductionLineUnitDoseType.Any(Function(o) o.Id_ProductionLine = defectClassificationItemId OrElse o.Id_UnitDoseType = unitDoseTypeId),
                               includes:={"ExternalCareCenter", "CMConfiguration", "ProductionLine"}, tracking:=False) _
                        .ToList()

            If cmExternals IsNot Nothing AndAlso cmExternals.Any() Then
                Dim repetidas = cmExternals _
                            .GroupBy(Function(m) m.ExternalCareCenterId) _
                            .Where(Function(m) m.Count() > 1) _
                            .FirstOrDefault()

                If repetidas IsNot Nothing Then
                    Dim line = repetidas.Where(Function(m) m.ProductionLineId <> defectClassificationItemId).FirstOrDefault()
                    Dim msg = $"El tipo de dosis unitaria no puede ser agregado porque genera conflicto por asociación del centro de atención externo ({line.ExternalCareCenter.Code} - {line.ExternalCareCenter.Description}) 
mediante la línea de producción ({line.ProductionLine.Code} - {line.ProductionLine.Name}) entre las centrales de mezclas ({String.Join(", ", repetidas.Select(Function(m) $"{m.CMConfiguration.Code} - {m.CMConfiguration.Name}").ToArray())})"
                    Throw New IndigoValidationException(msg)
                End If
            End If

            Dim cmInternals = _cmCenterAttentionRepository _
                        .Query(Function(m) m.ProductionLine.ProductionLineUnitDoseType.Any(Function(o) o.Id_UnitDoseType = unitDoseTypeId OrElse o.Id_ProductionLine = defectClassificationItemId),
                               includes:={"CMConfiguration", "ProductionLine"}, tracking:=False) _
                        .ToList()

            If cmInternals IsNot Nothing AndAlso cmInternals.Any() Then
                Dim repetidas = cmInternals _
                            .GroupBy(Function(m) m.CodeCenterAttention) _
                            .Where(Function(m) m.Count() > 1) _
                            .FirstOrDefault()

                If repetidas?.Count > 0 Then
                    Dim line = repetidas.Where(Function(m) m.IdProductionLine <> defectClassificationItemId).FirstOrDefault()
                    Dim msg = $"El tipo de dosis unitaria no puede ser agregado porque genera conflicto por asociación del centro de atención propio {line.CodeCenterAttention} 
mediante la línea de producción ({line.ProductionLine.Code} - {line.ProductionLine.Name}) entre las centrales de mezclas ({String.Join(", ", repetidas.Select(Function(m) $"{m.CMConfiguration.Code} - {m.CMConfiguration.Name}").ToArray())})"
                    Throw New IndigoValidationException(msg)
                End If
            End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
