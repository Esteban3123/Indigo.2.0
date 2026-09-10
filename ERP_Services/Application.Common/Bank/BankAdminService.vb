Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class BankAdminService
    Implements IBankAdminService

    'Repositorio de bancos
    Private _BankRepository As IBankRepository
    Private _secuenseDRepository As ISequenseCommonDRepository
    Private Const FORM_NAME As String = "Banco"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="bankRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal bankRepository As IBankRepository, secuenseDRepository As ISequenseCommonDRepository)
        If (bankRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de bancos vacio")
        End If
        _BankRepository = bankRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function DeleteBank(bank As Bank, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Bank) Implements IBankAdminService.DeleteBank
        Dim result As New ActionMessageResult(Of Bank)
        result.StateResult = True
        If bank Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BankRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                bank.ModificationUser = audit.CodeUser
                bank.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Bank)(bank, audit, status)

                bank.MarkAsDeleted()
                Me._BankRepository.SaveEntity(bank)
                unitWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of Bank) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}

                '_BankRepository.DeleteEntity(bank)
                'unitWork.Commit()
                ''/***** Auditoria Basica ********/
                'IndigoAuditBasic.Execute(bank.GetType.Name, audit.Functional, bank.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                ''/*****Auditoria Avanzada ******/
                'Dim auditObject As New IndigoAuditSimpleEntity(Of Bank)(bank, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                'auditObject.Execute()
                'Return result
            End Using
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.MessageResult.Add(New MessageResult("c-0000", bank.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StatusCode = eStatusResult.EXCEPTION
            result.StateResult = False
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    Public Function GetBank(code As String) As Bank Implements IBankAdminService.GetBank
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _BankRepository.GetBank(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Bank()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Public Function ListAllBank() As List(Of Bank) Implements IBankAdminService.ListAllBank
        Try
            Return _BankRepository.ListAllBank()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function SaveBank(bank As Bank, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of Bank) Implements IBankAdminService.SaveBank
        If bank Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BankRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If bank.Code Is Nothing OrElse bank.Code.Trim().Equals(String.Empty) Then
                    Dim seq As CommonSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CommonSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            bank.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Bank) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.CommonSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), bank.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Bank) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Bank = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Bank)
                Dim status As Integer

                If bank.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    bank.CreationUser = audit.CodeUser
                    bank.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _BankRepository.GetBank(bank.Code, False)
                    bank.ModificationUser = audit.CodeUser
                    bank.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                _BankRepository.SaveEntity(bank)
                unitWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Bank)(bank, audit, status, auxObjEntity)
                auditProcess.Execute()
                bank.MarkAsUnchanged()
                'If (bank.ChangeTracker.State = ObjectState.Added) Then
                '    '/***** Auditoria Basica ********/
                '    IndigoAuditBasic.Execute(bank.GetType.Name, audit.Functional, bank.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '    '/*****Auditoria Avanzada ******/
                '    Dim auditObject As New IndigoAuditSimpleEntity(Of Bank)(bank, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                '    auditObject.Execute()
                'ElseIf bank.ChangeTracker.State = ObjectState.Modified Then
                '    '/***** Auditoria Basica ********/
                '    IndigoAuditBasic.Execute(bank.GetType.Name, audit.Functional, bank.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '    '/*****Auditoria Avanzada ******/
                '    Dim auditObject As New IndigoAuditSimpleEntity(Of Bank)(bank, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxBank)
                '    auditObject.Execute()
                'End If
                'If bank.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                'IndigoAuditSimpleEntity(Of Bank).Execute(bank, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, bank)
                'ElseIf bank.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                'IndigoAuditSimpleEntity(Of Bank).Execute(bank, audit, Infrastructure.CrossCutting.Audit.Actions.Update, bank)
                'End If
                scope.Complete()

                Return New ActionResult(Of Bank) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = bank, .Message = MessageResult}
            End Using
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Bank) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function UpdateStateBank(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Bank) Implements IBankAdminService.UpdateStateBank
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
            Dim bank As Bank = Me._BankRepository.GetBank(code.Trim())
            If bank IsNot Nothing AndAlso bank.Id > 0 Then
                bank.State = state
            End If
            Dim result = Me.SaveBank(bank, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Bank) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Codigo vacio</exception>
    Public Function GetBankById(Id As Integer) As Bank Implements IBankAdminService.GetBankById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _BankRepository.GetBankById(Id)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Bank()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _BankRepository = Nothing
            _secuenseDRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
