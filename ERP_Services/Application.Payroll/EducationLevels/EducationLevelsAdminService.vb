'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class EducationLevelsAdminService
    Implements IEducationLevelsAdminService
    Private Const FORM_NAME As String = "FrmEducationLevels"


    Private _EducationRepository As IEducationLevelsRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    ''' <summary>
    ''' incia el repositorio de educationLevels
    ''' </summary>
    ''' <param name="repository">Repositorio de educations levels</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IEducationLevelsRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("EducationLevelsRepository Vacion")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _EducationRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function ChangeStateEducationLevel(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EducationLevel) Implements IEducationLevelsAdminService.ChangeStateEducationLevel
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
            Dim EducationLevel As EducationLevel = Me._EducationRepository.GetEducationLevels(code.Trim())
            If EducationLevel IsNot Nothing AndAlso EducationLevel.Id > 0 Then
                EducationLevel.State = state
            End If
            Dim result = Me.SaveEducationLevels(EducationLevel, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EducationLevel) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un nivel de educacion
    ''' </summary>
    ''' <param name="EducationLevel">Nivel de educacion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IEducationLevelsAdminService.DeleteEducationLevels
        'Dim result As New ActionMessageResult(Of EducationLevel)
        'result.StateResult = True
        'If (EducationLevel Is Nothing = True) Then
        '    Throw New ArgumentNullException("EducacionLevel Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _EducationRepository.UnitWork
        'Try
        '    _EducationRepository.DeleteEntity(EducationLevel)
        '    UnitOfWork.Commit()

        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute("EducationLevel", audit.Functional, EducationLevel.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of EducationLevel)(EducationLevel, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()

        '    'IndigoAuditSimpleEntity(Of EducationLevel).Execute(EducationLevel, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, EducationLevel)
        '    Return result
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", EducationLevel.Code))
        '    Return result
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return result
        'End Try

        If EducationLevel Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._EducationRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                EducationLevel.ModificationUser = audit.CodeUser
                EducationLevel.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EducationLevel)(EducationLevel, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                EducationLevel.MarkAsDeleted()
                Me._EducationRepository.SaveEntity(EducationLevel)
                UnitOfWork.Commit()
                IndigoAuditBasic.Execute("EducationLevel", audit.Functional, EducationLevel.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un nivel de educacion especifico
    ''' </summary>
    ''' <param name="code">Codigo nivel de educacion</param>
    ''' <returns>Nivel de educacion</returns>
    ''' <remarks></remarks>
    Public Function GetEducationLevels(code As String) As Domain.Payroll.Entities.EducationLevel Implements IEducationLevelsAdminService.GetEducationLevels
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try

            Return _EducationRepository.GetEducationLevels(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns>Lista de niveles de educacion</returns>
    ''' <remarks></remarks>
    Public Function ListAllEducationLevels() As List(Of Domain.Payroll.Entities.EducationLevel) Implements IEducationLevelsAdminService.ListAllEducationLevels
        Try

            Return _EducationRepository.ListAllEducationLevels()


        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza los niveles de educacion
    ''' </summary>
    ''' <param name="EducationLevel">Nivel de educacion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of Domain.Payroll.Entities.EducationLevel) Implements IEducationLevelsAdminService.SaveEducationLevels
        'If (EducationLevel Is Nothing = True) Then
        '    Throw New ArgumentNullException("EducationLevel Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _EducationRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of EducationLevel)
        '    Dim AuxEducationLevel As EducationLevel = Nothing
        '    Dim status As Integer

        '    If EducationLevel.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        EducationLevel.ModificationUser = audit.CodeUser
        '        EducationLevel.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        AuxEducationLevel = _EducationRepository.GetEducationLevels(EducationLevel.Code, False)
        '    Else
        '        EducationLevel.CreationUser = audit.CodeUser
        '        EducationLevel.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If

        '    'Valido si se va a guardar o a eliminar
        '    _EducationRepository.SaveEntity(EducationLevel)
        '    UnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of EducationLevel)(EducationLevel, audit, status, AuxEducationLevel)
        '    auditProcess.Execute()
        '    Return True

        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If EducationLevel Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._EducationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(EducationLevel.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            EducationLevel.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of EducationLevel) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), EducationLevel.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of EducationLevel) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As EducationLevel = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of EducationLevel)
                Dim status As Integer

                If EducationLevel.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    EducationLevel.CreationUser = audit.CodeUser
                    EducationLevel.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _EducationRepository.GetEducationLevels(EducationLevel.Code, False)
                    EducationLevel.ModificationUser = audit.CodeUser
                    EducationLevel.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._EducationRepository.SaveEntity(EducationLevel)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of EducationLevel)(EducationLevel, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                EducationLevel.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of EducationLevel) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = EducationLevel, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of EducationLevel) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EducationLevel) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EducationRepository = Nothing
            _secuenseDRepository = Nothing
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
