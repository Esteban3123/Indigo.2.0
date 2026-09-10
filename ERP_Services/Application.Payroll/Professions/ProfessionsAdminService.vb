'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class ProfessionsAdminService
    Implements IProfessionsAdminService
    Private Const FORM_NAME As String = "FrmProfessions"

    ' Repositorio de profesiones
    Private _ProfessionRepository As IProfessionsRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    ''' <summary>
    ''' Contructor el cual inicia la instancia del repositorio de profesiones
    ''' </summary>
    ''' <param name="repository">Repositorio de profesiones</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IProfessionsRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ProfessionsRepository Vacio")
        End If
        _ProfessionRepository = repository

        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Obtiene una profesion especifica-
    ''' </summary>
    ''' <param name="code">Codigo Profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    Public Function GetProfessions(code As String) As Domain.Payroll.Entities.Profession Implements IProfessionsAdminService.GetProfessions
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("Code Profession vacio")
        End If
        Try
            Return _ProfessionRepository.GetProfessions(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las profesiones
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessions() As List(Of Domain.Payroll.Entities.Profession) Implements IProfessionsAdminService.ListAllProfessions
        Try
            Return _ProfessionRepository.ListAllProfessions()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveProfessions(profession As Domain.Payroll.Entities.Profession, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of Domain.Payroll.Entities.Profession) Implements IProfessionsAdminService.SaveProfessions
        'If (profession Is Nothing = True) Then
        '    Throw New ArgumentNullException("Professions Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _ProfessionRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Profession)
        '    Dim auxProfession As Profession = Nothing
        '    Dim status As Integer

        '    If profession.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        profession.ModificationUser = audit.CodeUser
        '        profession.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxProfession = _ProfessionRepository.GetProfessions(profession.Code, False)
        '    Else
        '        profession.CreationUser = audit.CodeUser
        '        profession.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If

        '    'Valido si se va a guardar o a eliminar
        '    _ProfessionRepository.SaveEntity(profession)
        '    UnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of Profession)(profession, audit, status, auxProfession)
        '    auditProcess.Execute()
        '    Return True

        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If profession Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._ProfessionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(profession.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            profession.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Profession) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), profession.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Profession) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Profession = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Profession)
                Dim status As Integer

                If profession.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    profession.CreationUser = audit.CodeUser
                    profession.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _ProfessionRepository.GetProfessions(profession.Code, False)
                    profession.ModificationUser = audit.CodeUser
                    profession.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ProfessionRepository.SaveEntity(profession)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Profession)(profession, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                profession.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Profession) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = profession, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of Profession) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Profession) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteProfession(profession As Profession, audit As AuditMessage) As ActionResult Implements IProfessionsAdminService.DeleteProfession
        'Dim result As New ActionMessageResult(Of Profession)
        'result.StateResult = True
        'If (profession Is Nothing = True) Then
        '    Throw New ArgumentNullException("Professions vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _ProfessionRepository.UnitWork
        'Try
        '    _ProfessionRepository.DeleteEntity(profession)
        '    UnitOfWork.Commit()
        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute(profession.GetType.Name, audit.Functional, profession.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of Profession)(profession, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return result
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", profession.Code))
        '    Return result
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return result
        'End Try


        If profession Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._ProfessionRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                profession.ModificationUser = audit.CodeUser
                profession.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Profession)(profession, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                profession.MarkAsDeleted()
                Me._ProfessionRepository.SaveEntity(profession)
                UnitOfWork.Commit()
                IndigoAuditBasic.Execute(profession.GetType.Name, audit.Functional, profession.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
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

    Public Function ChangeStateProfession(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Profession) Implements IProfessionsAdminService.ChangeStateProfession
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
            Dim Profession As Profession = Me._ProfessionRepository.GetProfessions(code.Trim())
            If Profession IsNot Nothing AndAlso Profession.Id > 0 Then
                Profession.State = state
            End If
            Dim result = Me.SaveProfessions(Profession, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Profession) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ProfessionRepository = Nothing
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
