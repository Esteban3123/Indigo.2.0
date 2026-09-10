Imports Domain.Payroll.Entities
Imports System.Transactions
Imports Domain.Payroll
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core

Public Class PositionLevelAdminService
    Implements IPositionLevelAdminService

    Private Const FORM_NAME As String = "FrmPositionLevel"
    Private _PositionLevelRepository As IPositionLevelRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="PositionLevelAdminService" />.
    ''' </summary>
    ''' <param name="PositionLevelRepository">el repositorio para el manejo de los Niveles.</param>
    Public Sub New(ByVal PositionLevelRepository As IPositionLevelRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If PositionLevelRepository Is Nothing Then
            Throw New ArgumentNullException("PositionLevelRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PositionLevelRepository = PositionLevelRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un nivel
    ''' </summary>
    ''' <param name="PositionLevel">el nivel</param>
    ''' <returns></returns>
    Public Function DeletePositionLevel(PositionLevel As Domain.Payroll.Entities.PositionLevel, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IPositionLevelAdminService.DeletePositionLevel
        'Dim result As New ActionMessageResult(Of PositionLevel)
        'result.StateResult = True
        'If PositionLevel Is Nothing Then
        '    Throw New ArgumentNullException("positionLevel Vacio")
        'End If
        'Dim unitOfWork As IUnitWork = _PositionLevelRepository.UnitWork
        'Try
        '    'elimino el nivel del cargo
        '    _PositionLevelRepository.DeleteEntity(PositionLevel)
        '    unitOfWork.Commit()
        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute(PositionLevel.GetType.Name, audit.Functional, PositionLevel.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of PositionLevel)(PositionLevel, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return result
        'Catch ex As DbUpdateException
        '    unitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", PositionLevel.Code))
        '    Return result
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    result.StateResult = False
        '    Return result
        'End Try


        If PositionLevel Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PositionLevelRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                PositionLevel.ModificationUser = audit.CodeUser
                PositionLevel.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PositionLevel)(PositionLevel, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(PositionLevel.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                PositionLevel.MarkAsDeleted()
                Me._PositionLevelRepository.SaveEntity(PositionLevel)
                unitOfWork.Commit()

                IndigoAuditBasic.Execute(PositionLevel.GetType.Name, audit.Functional, PositionLevel.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
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
    ''' Obtiene un nivel especifico
    ''' </summary>
    ''' <param name="codePositionLevel">codigo del nivel</param>
    ''' <returns></returns>
    Public Function GetPositionLevel(codePositionLevel As String) As Domain.Payroll.Entities.PositionLevel Implements IPositionLevelAdminService.GetPositionLevel
        If String.IsNullOrEmpty(codePositionLevel) = True Then
            Throw New ArgumentNullException("codePositionLevel Vacio")
        End If
        Try
            Return _PositionLevelRepository.GetPositionLevel(codePositionLevel)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los niveles.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllPositionLevel() As List(Of Domain.Payroll.Entities.PositionLevel) Implements IPositionLevelAdminService.ListAllPositionLevel
        Try
            Return _PositionLevelRepository.ListAllPositionLevel
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' graba un nivel
    ''' </summary>
    ''' <param name="Detail">el nivel</param>
    ''' <returns></returns>
    Public Function SavePositionLevel(Detail As Domain.Payroll.Entities.PositionLevel, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of PositionLevel) Implements IPositionLevelAdminService.SavePositionLevel
        'If Detail Is Nothing Then
        '    Throw New ArgumentNullException("Detail Vacio")
        'End If
        'Dim unitOfWork As IUnitWork = _PositionLevelRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of PositionLevel)
        '    Dim auxPositionLevel As PositionLevel = Nothing
        '    Dim status As Integer

        '    If Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        Detail.ModificationUser = audit.CodeUser
        '        Detail.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxPositionLevel = _PositionLevelRepository.GetPositionLevel(Detail.Code, False)
        '    Else
        '        Detail.CreationUser = audit.CodeUser
        '        Detail.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If

        '    'Valido si se va a guardar o a eliminar
        '    _PositionLevelRepository.SaveEntity(Detail)
        '    unitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of PositionLevel)(Detail, audit, status, auxPositionLevel)
        '    auditProcess.Execute()
        '    Return True

        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try




        If Detail Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._PositionLevelRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Detail.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Detail.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Domain.Payroll.Entities.PositionLevel) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Detail.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Domain.Payroll.Entities.PositionLevel) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Domain.Payroll.Entities.PositionLevel = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Payroll.Entities.PositionLevel)
                Dim status As Integer

                If Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Detail.CreationUser = audit.CodeUser
                    Detail.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Me._PositionLevelRepository.GetPositionLevel(Detail.Code.Trim(), False)
                    Detail.ModificationUser = audit.CodeUser
                    Detail.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._PositionLevelRepository.SaveEntity(Detail)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Payroll.Entities.PositionLevel)(Detail, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Detail.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Domain.Payroll.Entities.PositionLevel) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Detail, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Payroll.Entities.PositionLevel) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Payroll.Entities.PositionLevel) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    Function ChangeStatePositionLevel(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PositionLevel) Implements IPositionLevelAdminService.ChangeStatePositionLevel
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
            Dim PositionLevel As PositionLevel = Me._PositionLevelRepository.GetPositionLevel(code.Trim())
            If PositionLevel IsNot Nothing AndAlso PositionLevel.Id > 0 Then
                PositionLevel.State = state
            End If
            Dim result = Me.SavePositionLevel(PositionLevel, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PositionLevel) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PositionLevelRepository = Nothing
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
