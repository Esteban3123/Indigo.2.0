'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region
Public Class ProductionLineService
    Implements IProductionLineService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmProductionLine"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _productionLineRepository As IProductionLineRepository
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _productionLineUnitDoseTypeRepository As IProductionLineUnitRepository
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
    Public Sub New(ByVal productionLineRepository As IProductionLineRepository,
                   cmCenterAttentionRepository As ICMCenterAttentionRepository,
                   ByVal productionLineUnitDoseTypeRepository As IProductionLineUnitRepository,
                   ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository,
                   cMExternalCareCenterRepository As ICMExternalCareCenterRepository)
        If productionLineRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If productionLineUnitDoseTypeRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If

        Me._productionLineRepository = productionLineRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
        _cmCenterAttentionRepository = cmCenterAttentionRepository
        _cMExternalCareCenterRepository = cMExternalCareCenterRepository
        Me._productionLineUnitDoseTypeRepository = productionLineUnitDoseTypeRepository
    End Sub

    ''' <summary>
    ''' Trae todos las lineas de productos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllProductionLine(audit As AuditMessage) As List(Of ProductionLine) Implements IProductionLineService.ListAllProductionLine
        Try
            Dim productionLine = Me._productionLineRepository.GetAll()
            For Each item As ProductionLine In productionLine
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProductionLine)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return productionLine
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Elimina una linea de producto
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function DeleteProductionLine(productionLine As ProductionLine, audit As AuditMessage) As ActionResult Implements IProductionLineService.DeleteProductionLine
        If productionLine Is Nothing Then
            Throw New ArgumentNullException("ProductionLine")
        End If
        Dim unitOfWork As IUnitWork = Me._productionLineRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                productionLine.ModificationUser = audit.CodeUser
                productionLine.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProductionLine)(productionLine, audit, status)
                If productionLine.ProductionLineSchedule IsNot Nothing Then
                    productionLine.ProductionLineSchedule.ToList().ForEach(Sub(x)
                                                                               x.MarkAsDeleted()
                                                                           End Sub)
                End If
                If productionLine.ProductionLineScheduleException IsNot Nothing Then
                    productionLine.ProductionLineScheduleException.ToList().ForEach(Sub(x)
                                                                                        x.MarkAsDeleted()
                                                                                    End Sub)
                End If
                If productionLine.ProductionLineUnitDoseType IsNot Nothing Then
                    productionLine.ProductionLineUnitDoseType.ToList().ForEach(Sub(x)
                                                                                   x.MarkAsDeleted()
                                                                               End Sub)
                End If
                productionLine.MarkAsDeleted()
                Me._productionLineRepository.SaveEntity(productionLine)
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

    ''' <summary>
    ''' Obtiene la linea de produccion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetProductionLine(code As String, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IProductionLineService.GetProductionLine
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionLine As ProductionLine = Me._productionLineRepository.GetProductionLine(code)
            If productionLine IsNot Nothing AndAlso productionLine.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProductionLine)(productionLine, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ProductionLine) With {.StateResult = True, .ObjectEmbbeded = productionLine}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionLine) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una linea de produccion por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetProductionLineId(id As Integer, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IProductionLineService.GetProductionLineId
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionLine As ProductionLine = Me._productionLineRepository.GetProductionLineId(id)
            If productionLine IsNot Nothing AndAlso productionLine.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProductionLine)(productionLine, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ProductionLine) With {.StateResult = True, .ObjectEmbbeded = productionLine}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionLine) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_ProductionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Public Function ListAllProductionLineUnitDoseType(Id_ProductionLine As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements IProductionLineService.ListAllProductionLineUnitDoseType
        If Id_ProductionLine = 0 Then
            Throw New ArgumentNullException("Id_ProductionLine")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionLineUnit = Me._productionLineUnitDoseTypeRepository.ListAllProductionLineUnitDoseType(Id_ProductionLine)

            Return productionLineUnit
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
    Public Function SaveProductionLine(productionLine As ProductionLine, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ProductionLine) Implements IProductionLineService.SaveProductionLine
        If productionLine Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._productionLineRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try
            ValidateProductionLine(productionLine.Id)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(productionLine.Code) Then
                    Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            productionLine.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ProductionLine) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), productionLine.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ProductionLine) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ProductionLine = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ProductionLine)
                Dim status As Integer

                If productionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    productionLine.CreationUser = audit.CodeUser
                    productionLine.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = productionLine.OriginalValue
                    productionLine.ModificationUser = audit.CodeUser
                    productionLine.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._productionLineRepository.SaveEntity(productionLine)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ProductionLine)(productionLine, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                productionLine.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ProductionLine) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionLine, .Message = MessageResult}
            End Using
        Catch ex As IndigoValidationException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProductionLine) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProductionLine) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionLine) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Aplica las validaciones para las líneas de producción y los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="productionLineId"></param>
    Public Sub ValidateProductionLine(productionLineId As Integer)
        If productionLineId > 0 Then
            Dim productionLine = _productionLineRepository.FirstOrDefault(Function(m) m.Id = productionLineId, includes:={"ProductionLineUnitDoseType"}, tracking:=False)

            For Each ud In productionLine.ProductionLineUnitDoseType
                Dim res = ValidateProductionLineUnitDoseType(productionLineId, ud.Id)

                If Not res.StateResult Then Throw New IndigoValidationException(res.Message)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Aplica las validaciones para las líneas de producción y los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="productionLineId"></param>
    Public Function ValidateProductionLineUnitDoseType(productionLineId As Integer, unitDoseTypeId As Integer) As ActionResult Implements IProductionLineService.ValidateProductionLineUnitDoseType
        Try
            Dim cmExternals = _cMExternalCareCenterRepository _
                        .Query(Function(m) m.ProductionLine.ProductionLineUnitDoseType.Any(Function(o) o.Id_ProductionLine = productionLineId AndAlso o.Id_UnitDoseType = unitDoseTypeId),
                               includes:={"ExternalCareCenter", "CMConfiguration", "ProductionLine"}, tracking:=False) _
                        .ToList()

            If cmExternals IsNot Nothing AndAlso cmExternals.Any() Then
                Dim repetidas = cmExternals _
                            .GroupBy(Function(m) m.ExternalCareCenterId) _
                            .Where(Function(m) m.Count() > 1) _
                            .FirstOrDefault()

                If repetidas IsNot Nothing Then
                    Dim line = repetidas.Where(Function(m) m.ProductionLineId <> productionLineId).FirstOrDefault()
                    Dim msg = $"El tipo de dosis unitaria no puede ser agregado porque genera conflicto por asociación del centro de atención externo ({line.ExternalCareCenter.Code} - {line.ExternalCareCenter.Description}) 
mediante la línea de producción ({line.ProductionLine.Code} - {line.ProductionLine.Name}) entre las centrales de mezclas ({String.Join(", ", repetidas.Select(Function(m) $"{m.CMConfiguration.Code} - {m.CMConfiguration.Name}").ToArray())})"
                    Throw New IndigoValidationException(msg)
                End If
            End If

            Dim cmInternals = _cmCenterAttentionRepository _
                        .Query(Function(m) m.ProductionLine.ProductionLineUnitDoseType.Any(Function(o) o.Id_UnitDoseType = unitDoseTypeId AndAlso o.Id_ProductionLine = productionLineId),
                               includes:={"CMConfiguration", "ProductionLine"}, tracking:=False) _
                        .ToList()

            If cmInternals IsNot Nothing AndAlso cmInternals.Any() Then
                Dim repetidas = cmInternals _
                            .GroupBy(Function(m) m.CodeCenterAttention) _
                            .Where(Function(m) m.Count() > 1) _
                            .FirstOrDefault()

                If repetidas?.Count > 0 Then
                    Dim line = repetidas.Where(Function(m) m.IdProductionLine <> productionLineId).FirstOrDefault()
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

    ''' <summary>
    ''' Guarda o actualiza los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="productionLineUnitDoseType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveProductionLineUnitDoseType(productionLineUnitDoseType As ProductionLineUnitDoseType) As ActionResult(Of ProductionLineUnitDoseType) Implements IProductionLineService.SaveProductionLineUnitDoseType
        If productionLineUnitDoseType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._productionLineUnitDoseTypeRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(productionLineUnitDoseType.Id) Then
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ProductionLineUnitDoseType = Nothing
                'Dim auditProcess As IndigoAuditSimpleEntity(Of ProductionLine)
                'Dim status As Integer

                Me._productionLineUnitDoseTypeRepository.SaveEntity(productionLineUnitDoseType)
                unitOfWork.Commit()
                'sequenseUnitOfWork.Commit()
                'auditProcess = New IndigoAuditSimpleEntity(Of ProductionLine)(ProductionLine, audit, status, auxObjEntity)
                'auditProcess.Execute()

                'Se marca la entidad como sin cambios
                productionLineUnitDoseType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ProductionLineUnitDoseType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionLineUnitDoseType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProductionLineUnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionLineUnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Actualiza el estado de un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStateProductionLine(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IProductionLineService.UpdateStateProductionLine
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
            Dim productionLine As ProductionLine = Me._productionLineRepository.GetProductionLine(code)
            If productionLine IsNot Nothing AndAlso productionLine.Id > 0 Then
                productionLine.State = state
            End If
            Return Me.SaveProductionLine(productionLine, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionLine) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
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
