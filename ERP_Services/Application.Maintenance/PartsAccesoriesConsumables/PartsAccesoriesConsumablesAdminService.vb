'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class PartsAccesoriesConsumablesAdminService

    Implements IPartsAccesoriesConsumablesAdminService


    Private Const FORM_NAME As String = "FrmPartsAccesoriesConsumables"

    'Repositorio de tipo de ubicacion
    Private _PartsAccesoriesConsumablesRespository As IPartsAccesoriesConsumablesRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumablesRespository">Repositorio de PartsAccesoriesConsumablesRespository</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PartsAccesoriesConsumablesRespository As IPartsAccesoriesConsumablesRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (PartsAccesoriesConsumablesRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de PartsAccesoriesConsumablesRespository")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PartsAccesoriesConsumablesRespository = PartsAccesoriesConsumablesRespository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As PartsAccesoriesConsumables, audit As AuditMessage) As Boolean Implements IPartsAccesoriesConsumablesAdminService.DeletePartsAccesoriesConsumables
        If PartsAccesoriesConsumables Is Nothing Then
            Throw New ArgumentNullException("PartsAccesoriesConsumables vacio")
        End If
        Dim unitWork As IUnitWork = _PartsAccesoriesConsumablesRespository.UnitWork
        Try
            _PartsAccesoriesConsumablesRespository.DeleteEntity(PartsAccesoriesConsumables)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables por Código
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String) As PartsAccesoriesConsumables Implements IPartsAccesoriesConsumablesAdminService.GetPartsAccesoriesConsumablesByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de PartsAccesoriesConsumables vacio")
        End If
        Try
            Return _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todos PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function ListAllPartsAccesoriesConsumables() As List(Of PartsAccesoriesConsumables) Implements IPartsAccesoriesConsumablesAdminService.ListAllPartsAccesoriesConsumables
        Try
            Return _PartsAccesoriesConsumablesRespository.ListAllPartsAccesoriesConsumables()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As PartsAccesoriesConsumables, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PartsAccesoriesConsumables) Implements IPartsAccesoriesConsumablesAdminService.SavePartsAccesoriesConsumables
        'If PartsAccesoriesConsumables Is Nothing Then
        '    Throw New ArgumentNullException("PartsAccesoriesConsumables Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _PartsAccesoriesConsumablesRespository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try


        '    Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
        '    'Dim auxPartsAccesoriesConsumables = _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(PartsAccesoriesConsumables.Code, False)

        '    If PartsAccesoriesConsumables.Code Is Nothing OrElse PartsAccesoriesConsumables.Code.Trim().Equals(String.Empty) OrElse PartsAccesoriesConsumables.Code = "Nuevo" Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                PartsAccesoriesConsumables.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    'Dim auditProcess As IndigoAuditSimpleEntity(Of PartsAccesoriesConsumables)
        '    Dim auxPartsAccesoriesConsumables As PartsAccesoriesConsumables = Nothing
        '    Dim status As Integer
        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
        '        PartsAccesoriesConsumables.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxPartsAccesoriesConsumables = _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(PartsAccesoriesConsumables.Code, False)
        '    Else
        '        PartsAccesoriesConsumables.CreationUser = audit.CodeUser
        '        PartsAccesoriesConsumables.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If


        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        Me._PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
        '    End If
        '    UnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()

        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
        '        auditObject.Execute()
        '    ElseIf PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPartsAccesoriesConsumables)
        '        auditObject.Execute()
        '    End If
        '    Return True


        '    'Dim auditProcess As IndigoAuditSimpleEntity(Of PartsAccesoriesConsumables)
        '    'Dim auxPartsAccesoriesConsumables As PartsAccesoriesConsumables = Nothing
        '    'Dim status As Integer
        '    'If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '    '    PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
        '    '    PartsAccesoriesConsumables.ModificationDate = Date.Now()
        '    '    status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    '    auxPartsAccesoriesConsumables = _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(PartsAccesoriesConsumables.Code, False)
        '    'Else
        '    '    PartsAccesoriesConsumables.CreationUser = audit.CodeUser
        '    '    PartsAccesoriesConsumables.CreationDate = Date.Now()
        '    '    status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    'End If
        '    ''Valido si se va a guardar o a eliminar
        '    '_PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
        '    'UnitOfWork.Commit()
        '    'auditProcess = New IndigoAuditSimpleEntity(Of PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, status, audit.Company, auxPartsAccesoriesConsumables)
        '    'auditProcess.Execute()
        '    'Return True
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try




        If PartsAccesoriesConsumables Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._PartsAccesoriesConsumablesRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(PartsAccesoriesConsumables.Code) Then
                    Dim seq As Domain.Entities.MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PartsAccesoriesConsumables.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PartsAccesoriesConsumables) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), PartsAccesoriesConsumables.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PartsAccesoriesConsumables) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PartsAccesoriesConsumables = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PartsAccesoriesConsumables)
                Dim status As Integer

                If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PartsAccesoriesConsumables.CreationUser = audit.CodeUser
                    PartsAccesoriesConsumables.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(PartsAccesoriesConsumables.Code, False)
                    PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
                    PartsAccesoriesConsumables.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.PartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxObjEntity)
                    auditObject.Execute()
                End If

                'Se marca la entidad como sin cambios
                PartsAccesoriesConsumables.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PartsAccesoriesConsumables) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = PartsAccesoriesConsumables, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of PartsAccesoriesConsumables) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PartsAccesoriesConsumables) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PartsAccesoriesConsumablesRespository = Nothing
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
