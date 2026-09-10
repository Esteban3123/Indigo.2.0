'***********************************************************************
' Assembly         : Application.Billing
' Author           : Antony F. Córdoba P.
' Created          : 20-12-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.Payroll
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.PayrollRepository
Imports Microsoft.VisualBasic.Devices
Imports Application.Billing
Imports Application.Security
Imports System.Globalization

#End Region

Public Class MaritalStatusAdminService
    Implements IMaritalStatusAdminService

    Private _MaritalStatusRepository As IMaritalStatusRepository

    Private _secuenseDRepository As IPayrollSequenceDetailRepository

    Public Const FORM_NAME As String = "FrmMaritalStatus"

    Public Sub New(ByVal maritalStatusRepository As IMaritalStatusRepository, secuenseDRepository As IPayrollSequenceDetailRepository)
        If maritalStatusRepository Is Nothing Then
            Throw New ArgumentNullException("maritalStatusRepository Vacio")
        End If

        _MaritalStatusRepository = maritalStatusRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Lista los tipos de estado civil
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllMaritalStatus() As List(Of MaritalStatus) Implements IMaritalStatusAdminService.ListAllMaritalStatus
        Try
            Return _MaritalStatusRepository.ListAllMaritalStatus()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina un tipo de estado civil
    ''' </summary>
    ''' <param name="maritalStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteMaritalStatus(maritalStatus As MaritalStatus, audit As AuditMessage) As ActionResult Implements IMaritalStatusAdminService.DeleteMaritalStatus
        If maritalStatus Is Nothing Then
            Throw New ArgumentNullException("maritalStatus")
        End If
        Dim unitOfWork As IUnitWork = _MaritalStatusRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaritalStatus)(maritalStatus, audit, status)

                maritalStatus.MarkAsDeleted()
                Me._MaritalStatusRepository.SaveEntity(maritalStatus)
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
    ''' Guarda un tipo de estado civil
    ''' </summary>
    ''' <param name="maritalStatus"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveMaritalStatus(maritalStatus As MaritalStatus, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaritalStatus) Implements IMaritalStatusAdminService.SaveMaritalStatus
        If maritalStatus Is Nothing Then
            Throw New ArgumentNullException("maritalStatus Control Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _MaritalStatusRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(maritalStatus.Code) Then
                    Dim seq As PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            maritalStatus.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaritalStatus) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), maritalStatus.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaritalStatus) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If


                Dim auxCommon As MaritalStatus = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaritalStatus)
                Dim status As Integer

                If maritalStatus.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    maritalStatus.CreationUser = audit.CodeUser
                    maritalStatus.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = maritalStatus.OriginalValue
                    maritalStatus.ModificationUser = audit.CodeUser
                    maritalStatus.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If maritalStatus.Name = "" Then
                    UnitOfWork.RollbackChanges()
                Else
                    Me._MaritalStatusRepository.SaveEntity(maritalStatus)
                End If

                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaritalStatus)(maritalStatus, audit, status, auxCommon)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                maritalStatus.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaritalStatus) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = maritalStatus, .Message = MessageResult}

            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of MaritalStatus) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaritalStatus) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de estado civil por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetMaritalStatusByCode(code As String, audit As AuditMessage) As ActionResult(Of MaritalStatus) Implements IMaritalStatusAdminService.GetMaritalStatusByCode
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code JustificationControl vacio")
        End If
        Try
            Dim maritalStatus = _MaritalStatusRepository.GetMaritalStatusByCode(code)

            Return New ActionResult(Of MaritalStatus) With {.StateResult = True, .ObjectEmbbeded = maritalStatus}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de estado civil por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetMaritalStatusById(id As Integer) As ActionResult(Of MaritalStatus) Implements IMaritalStatusAdminService.GetMaritalStatusById
        If Not (id > 0) Then
            Throw New ArgumentNullException("GetMaritalStatusById Vacio")
        End If
        Try
            Dim maritalStatus As MaritalStatus = _MaritalStatusRepository.GetMaritalStatusById(id)
            Return New ActionResult(Of MaritalStatus) With {.StateResult = True, .ObjectEmbbeded = maritalStatus}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaritalStatus) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

#Region "Culture"

    ''' <summary>
    ''' Obtiene un tipo de estado civil por cultura
    ''' </summary>
    ''' <param name="CultureStatus"></param>
    ''' <returns></returns>
    Public Function GetCultureMaritalStatus(CultureStatus As String) Implements IMaritalStatusAdminService.GetCultureMaritalStatus
        Try

            'Cultura Colombia y Costa Rica
            Dim cultureRegionCO = "es-CO"
            Dim cultureRegionCR = "es-CR"

            If String.IsNullOrEmpty(CultureStatus) Then
                Throw New ArgumentNullException("CultureStatus")
            End If

            If Not {cultureRegionCO, cultureRegionCR}.Contains(CultureStatus) Then
                Return Nothing
            End If

            Dim query = _MaritalStatusRepository?.Any(Function(x) True)

            If query Then
                Return Nothing
            End If

            'Listado de los estados civiles
            Dim listMaritalStatus As New List(Of MaritalStatus) From {
            New MaritalStatus With {.Name = "Soltero", .Code = "001", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Casado", .Code = "002", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Viudo", .Code = "003", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Divorciado", .Code = "004", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Separado", .Code = "005", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Union Libre", .Code = "006", .CreationUser = "999", .CreationDate = Date.Now()},
            New MaritalStatus With {.Name = "Ignorado", .Code = "009", .CreationUser = "999", .CreationDate = Date.Now()}
            }

            If CultureStatus = cultureRegionCO Then
                listMaritalStatus.Remove(listMaritalStatus.LastOrDefault(Function(ms) ms.Code = "009"))
            End If

            'Guarda el listado del estado civil
            For Each item As MaritalStatus In listMaritalStatus
                SaveMaritalStatus(item, New AuditMessage With {.CodeUser = "999"})
            Next

            Return Nothing

        Catch ex As Exception
            Throw New Exception(Utils.GetInnerExceptionMessageToString(ex))
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
