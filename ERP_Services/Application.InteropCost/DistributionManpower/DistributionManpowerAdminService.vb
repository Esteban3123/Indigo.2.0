'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports Domain.Entities.Service
Imports System.Transactions
Imports Domain.Payroll
Imports System.Text

Public Class DistributionManpowerAdminService
    Implements IDistributionManpowerAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribucion de mano de obra
    ''' </summary>
    Private _distributionManpowerRepository As IDistributionManpowerRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository
    Private _employeeRepository As IEmployeeRepository
    Private _interopCostService As IInteropCostServices

#End Region

#Region "Methods"

    Public Sub New(ByVal distributionManpowerRepository As IDistributionManpowerRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository,
                   employeeRepository As IEmployeeRepository, interopCostService As IInteropCostServices)
        If distributionManpowerRepository Is Nothing Then
            Throw New ArgumentNullException("distributionManpowerRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionManpowerRepository = distributionManpowerRepository
        _sequenceDRepository = sequenceDRepository
        _employeeRepository = employeeRepository
        _interopCostService = interopCostService
    End Sub

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Function DeleteDistributionManpower(distributionManpower As DistributionManpower, audit As AuditMessage) As ActionResult Implements IDistributionManpowerAdminService.DeleteDistributionManpower
        If distributionManpower Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionManpowerRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionManpower.DistributionManpowerDetail.Count > 0
                    distributionManpower.DistributionManpowerDetail.Item(distributionManpower.DistributionManpowerDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionManpower.MarkAsDeleted()
                distributionManpower.ModificationUser = audit.CodeUser
                distributionManpower.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionManpower)(distributionManpower, audit, status)
                Me._distributionManpowerRepository.SaveEntity(distributionManpower)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Function GetDistributionManpower(code As String, audit As AuditMessage) As ActionResult(Of DistributionManpower) Implements IDistributionManpowerAdminService.GetDistributionManpower
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionManpower As DistributionManpower = Me._distributionManpowerRepository.GetDistributionManpower(code.Trim())
            If distributionManpower IsNot Nothing AndAlso distributionManpower.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionManpower)(distributionManpower, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Function GetDistributionManpowerByEmployeeIdAndYearMonth(employeeId As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionManpower) Implements IDistributionManpowerAdminService.GetDistributionManpowerByEmployeeIdAndYearMonth
        If employeeId = 0 Then
            Throw New ArgumentNullException("employeeId")
        End If
        Try
            Dim distributionManpower As DistributionManpower = Me._distributionManpowerRepository.GetDistributionManpowerByEmployeeIdAndYearMonth(employeeId, year, month)
            Return New ActionResult(Of DistributionManpower) With {.StateResult = True, .ObjectEmbbeded = distributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionManpower) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Function GetDistributionManpowerById(id As Integer) As DistributionManpower Implements IDistributionManpowerAdminService.GetDistributionManpowerById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionManpowerRepository.GetDistributionManpowerById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of DistributionManpower) Implements IDistributionManpowerAdminService.ListDistributionManpowerByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim listManPower As List(Of DistributionManpower) = Me._distributionManpowerRepository.ListDistributionManpowerByYearMonth(year, month)
            listManPower.ForEach(Sub(o)
                                     Dim employee As Domain.Payroll.Entities.Employee = _employeeRepository.GetEmployeeWithGroup(o.EmployeeId)
                                     If employee IsNot Nothing AndAlso employee.Id > 0 Then
                                         o.FullNameEmployeeGroup = String.Concat(employee.Contract(0).Group.Code, " - ", employee.Contract(0).Group.Name)
                                     End If
                                 End Sub)
            Return listManPower
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    Public Function SaveDistributionManpower(distributionManpower As DistributionManpower, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DistributionManpower) Implements IDistributionManpowerAdminService.SaveDistributionManpower
        If distributionManpower Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionManpowerRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            Dim resultValidate As ActionResult = _interopCostService.ValidateDistributionManpowerSave(distributionManpower)
            If Not resultValidate.StateResult Then
                unitOfWork.RollbackChangesUnitOfWork()
                Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionManpower.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionManpower.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionManpower.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxDistributionManpower As DistributionManpower = Nothing
                Dim status As Integer
                If distributionManpower.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionManpower.Code)
                    End If
                    distributionManpower.CreationDate = Date.Now
                    distributionManpower.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionManpower.ModificationDate = Date.Now
                    distributionManpower.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxDistributionManpower = distributionManpower.OriginalValue
                End If
                Me._distributionManpowerRepository.SaveEntity(distributionManpower)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionManpower)(distributionManpower, audit, status, auxDistributionManpower)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionManpower, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state distribution manpower.
    ''' </summary>
    Public Function UpdateStateDistributionManpower(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionManpower) Implements IDistributionManpowerAdminService.UpdateStateDistributionManpower
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
            Dim distributionManpower As DistributionManpower = Me._distributionManpowerRepository.GetDistributionManpower(code.Trim())
            If distributionManpower IsNot Nothing AndAlso distributionManpower.Id > 0 Then
                distributionManpower.Status = state
            End If
            Return Me.SaveDistributionManpower(distributionManpower, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionManpower) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IDistributionManpowerAdminService.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionManpowerRepository.ListPeriodWithDataByMaximumPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda masivamente la distribucion de mano de obra
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmMasiveInteropCost(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult Implements IDistributionManpowerAdminService.ConfirmMasiveInteropCost
        If ListIds Is Nothing OrElse ListIds.Count = 0 Then
            Throw New ArgumentNullException("ListIds")
        End If
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                'Se convierte el listado de ids a xml para enviarlo al sp
                Dim xmlObject = ConvertToXml(ListIds)
                Dim resultStore = _distributionManpowerRepository.SP_ConfirmMasiveInteropCostDistributionManpower(xmlObject, Year, Month, OperatingUnitId, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convertir el objeto en xml
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(ListIds As List(Of Tuple(Of Integer, Integer))) As String
        Dim builder As StringBuilder = New StringBuilder()

        For Each itemTuple In ListIds
            builder.Append("<Ids>")
            builder.Append("<LiquidationId>" & itemTuple.Item1 & "</LiquidationId>")
            builder.Append("<EmployeeId>" & itemTuple.Item2 & "</EmployeeId>")
            builder.Append("</Ids>")
        Next

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ExportExcelInteropCostDistributionManPower(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result)) Implements IDistributionManpowerAdminService.SP_ExportExcelInteropCostDistributionManPower
        If ListIds Is Nothing OrElse ListIds.Count = 0 Then
            Throw New ArgumentNullException("ListIds")
        End If
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            'Se convierte el listado de ids a xml para enviarlo al sp
            Dim xmlObject = ConvertToXml(ListIds)
            Dim resultStore = _distributionManpowerRepository.SP_ExportExcelInteropCostDistributionManPower(xmlObject, Year, Month, OperatingUnitId, audit.CodeUser)
            Return New ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = resultStore}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _interopCostService.Dispose()
            End If
            _distributionManpowerRepository = Nothing
            _sequenceDRepository = Nothing
            _employeeRepository = Nothing
            _interopCostService = Nothing
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