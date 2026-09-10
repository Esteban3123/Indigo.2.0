'************************************************************
' Assembly         : Application.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Contract
#End Region

Public Class RIPSServicesAdminService
    Implements IRIPSServicesAdminService

    Private _RIPSServiceRepository As IRIPSServicesRepository
    Private _SequenceDRepository As ISequenseContractDRepository

    Public Const FORM_NAME As String = "FrmRIPSService"

    Public Sub New(ByVal serviceRepository As IRIPSServicesRepository, sequenceDRepository As ISequenseContractDRepository)
        If serviceRepository Is Nothing Then
            Throw New ArgumentNullException("Service vacío")
        End If

        _RIPSServiceRepository = serviceRepository
        _SequenceDRepository = sequenceDRepository
    End Sub

    ''' <summary>
    ''' Lista todos los servicios RIPS
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllRIPSServices() As List(Of RIPSServices) Implements IRIPSServicesAdminService.ListAllRIPSServices
        Try
            Return _RIPSServiceRepository.ListAllRIPSServices()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un servicio RIPS por su ID
    ''' </summary>
    ''' <param name="id">ID del servicio RIPS</param>
    ''' <returns>Objeto RIPSServices</returns>
    Public Function GetRIPSServiceById(id As Integer) As ActionResult(Of RIPSServices) Implements IRIPSServicesAdminService.GetRIPSServiceById
        If Not (id > 0) Then
            Throw New ArgumentNullException("id del service group vacío")
        End If
        Try
            Dim service As RIPSServices = _RIPSServiceRepository.GetRIPSServiceById(id)
            Return New ActionResult(Of RIPSServices) With {.StateResult = True, .ObjectEmbbeded = service}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServices) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un servicio RIPS por su codigo.
    ''' </summary>
    ''' <param name="code">Código del servicio</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Objeto RIPSServices</returns>
    Public Function GetRIPSServiceByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServices) Implements IRIPSServicesAdminService.GetRIPSServiceByCode
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code is null or empty")
        End If

        Try
            Dim service As RIPSServices = _RIPSServiceRepository.GetRIPSServiceByCode(code)
            Return New ActionResult(Of RIPSServices) With {.StateResult = True, .ObjectEmbbeded = service}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServices) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un servicio RIPS
    ''' </summary>
    ''' <param name="Service">Instancia de RIPSServies</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense">Secuencia numerica</param>
    ''' <returns>Objeto RIPSServices</returns>
    Public Function SaveRIPSService(Service As RIPSServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServices) Implements IRIPSServicesAdminService.SaveRIPSService
        If Service Is Nothing Then
            Throw New ArgumentNullException("service group vacío")
        End If

        Dim UnitOfWork As IUnitWork = _RIPSServiceRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._SequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Service.Code) Then
                    Dim seq As ContractSequenceDetail = Me._SequenceDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Service.Code = res
                            seq.Next += 1
                            Me._SequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RIPSServices) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.ContractSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Service.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RIPSServices) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCommon As RIPSServices = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RIPSServices)
                Dim status As Integer

                If Service.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Service.CreationUser = audit.CodeUser
                    Service.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = Service.OriginalValue
                    Service.ModificationUser = audit.CodeUser
                    Service.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._RIPSServiceRepository.SaveEntity(Service)
                UnitOfWork.Commit()
                sequenceUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RIPSServices)(Service, audit, status, auxCommon)
                auditProcess.Execute()

                Service.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RIPSServices) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = Service, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of RIPSServices) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServices) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Borra un servicio RIPS
    ''' </summary>
    ''' <param name="Service">Instancia de RIPSServices</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Objeto RIPSServices</returns>
    Public Function DeleteRIPSService(Service As RIPSServices, audit As AuditMessage) As ActionResult Implements IRIPSServicesAdminService.DeleteRIPSService
        If Service Is Nothing Then
            Throw New ArgumentNullException("BillingJustificationControl")
        End If
        Dim unitOfWork As IUnitWork = _RIPSServiceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RIPSServices)(Service, audit, status)
                Service.MarkAsDeleted()
                Me._RIPSServiceRepository.SaveEntity(Service)
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
    ''' Cambia el estado del servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del servicio RIPS</param>
    ''' <param name="state">Estado nuevo</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Objeto RIPSServices</returns>
    Public Function ChangeStateRIPSService(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServices) Implements IRIPSServicesAdminService.ChangeStateRIPSService
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code vacío")
        End If
        Dim ServiceGroup As RIPSServices = _RIPSServiceRepository.GetRIPSServiceByCode(code)
        ServiceGroup.Status = state
        Return SaveRIPSService(ServiceGroup, audit)
    End Function


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
