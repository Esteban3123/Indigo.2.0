

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad tipos de poliza
''' </summary>
''' <remarks></remarks>
Public Class PolizaTypeAdminService
    Implements IPolizaTypeAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    'Repositorio de tipo de inventario
    Private _PolizaTypeRepository As IPolizaTypeRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de poliza
    ''' </summary>
    ''' <param name="PolizaTypeRepository">Repositorio de tipo de poliza</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PolizaTypeRepository As IPolizaTypeRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (PolizaTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de tipo de poliza")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PolizaTypeRepository = PolizaTypeRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeletePolizaType(PolizaType As PolizaType, audit As AuditMessage) As ActionResult Implements IPolizaTypeAdminService.DeletePolizaType
        'If PolizaType Is Nothing Then
        '    Throw New ArgumentNullException("tipo de poliza vacio")
        'End If
        'Dim unitWork As IUnitWork = _PolizaTypeRepository.UnitWork
        'Try
        '    PolizaType.MarkAsDeleted()
        '    _PolizaTypeRepository.DeleteEntity(PolizaType)
        '    unitWork.Commit()
        '    'Auditoria básica
        '    IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PolizaType).Name, audit.Functional, PolizaType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    'Ausitoria avanzada
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PolizaType)(PolizaType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
        '    unitWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try



        If PolizaType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PolizaTypeRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                'PolizaType.ModificationUser = audit.CodeUser
                'PolizaType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                ' Dim auditProcess As New IndigoAuditSimpleEntity(Of PolizaType)(PolizaType, audit, status)

                'While PolizaType.InvoiceCategoriesUser.Count > 0
                '    PolizaType.InvoiceCategoriesUser(PolizaType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                PolizaType.MarkAsDeleted()
                Me._PolizaTypeRepository.SaveEntity(PolizaType)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PolizaType).Name, audit.Functional, PolizaType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PolizaType)(PolizaType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
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

    Public Function GetPolizaType(codePolizaType As String, audit As AuditMessage) As PolizaType Implements IPolizaTypeAdminService.GetPolizaType
        If String.IsNullOrEmpty(codePolizaType) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try
            Dim polizaType As Domain.Maintenance.Entities.PolizaType = _PolizaTypeRepository.GetPolizaType(codePolizaType)
            If polizaType IsNot Nothing AndAlso polizaType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PolizaType)(polizaType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return polizaType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PolizaType()
        End Try
    End Function

    Public Function ListAllPolizaType() As List(Of PolizaType) Implements IPolizaTypeAdminService.ListAllPolizaType
        Try
            Return _PolizaTypeRepository.ListAllPolizaType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SavePolizaType(PolizaType As PolizaType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PolizaType) Implements IPolizaTypeAdminService.SavePolizaType
        If PolizaType Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _PolizaTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxPolizaType = _PolizaTypeRepository.GetPolizaType(PolizaType.Code, False)

            If PolizaType.Code Is Nothing OrElse PolizaType.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        PolizaType.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Maintenance.Entities.PolizaType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Maintenance.Entities.PolizaType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._PolizaTypeRepository.SaveEntity(PolizaType)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PolizaType).Name, audit.Functional, PolizaType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PolizaType)(PolizaType, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PolizaType).Name, audit.Functional, PolizaType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PolizaType)(PolizaType, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPolizaType)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Maintenance.Entities.PolizaType) With {.StateResult = True, .ObjectEmbbeded = PolizaType}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Maintenance.Entities.PolizaType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.PolizaType) With {.StateResult = False}
        End Try
    End Function

    Public Function ChangeStatePoliza(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PolizaType) Implements IPolizaTypeAdminService.ChangeStatePoliza
        'Dim polizatype As Domain.Maintenance.Entities.PolizaType = GetPolizaType(code, audit)
        'polizatype.State = state
        'polizatype.MarkAsModified()
        'Return SavePolizaType(polizatype, audit)

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
            Dim polizatype As Domain.Maintenance.Entities.PolizaType = Me.GetPolizaType(code.Trim(), audit)
            If polizatype IsNot Nothing AndAlso polizatype.Id > 0 Then
                polizatype.State = state
            End If
            Dim result = Me.SavePolizaType(polizatype, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PolizaType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PolizaTypeRepository = Nothing
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
