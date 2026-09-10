'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetPartsAccesoriesConsumablesAdminService

    Implements IFixedAssetPartsAccesoriesConsumablesAdminService
    Private Const FORM_NAME As String = "FrmFixedAssetPartsAccesoriesConsumables"
    'Repositorio de tipo de ubicacion
    Private _PartsAccesoriesConsumablesRespository As IFixedAssetPartsAccesoriesConsumablesRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumablesRespository">Repositorio de PartsAccesoriesConsumablesRespository</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PartsAccesoriesConsumablesRespository As IFixedAssetPartsAccesoriesConsumablesRepository, ByVal secuenseDRepository As IFixedAssetSequenceDetailRepository)
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
    Public Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IFixedAssetPartsAccesoriesConsumablesAdminService.DeletePartsAccesoriesConsumables
        'If PartsAccesoriesConsumables Is Nothing Then
        '    Throw New ArgumentNullException("PartsAccesoriesConsumables vacio")
        'End If
        'Dim unitWork As IUnitWork = _PartsAccesoriesConsumablesRespository.UnitWork
        'Try
        '    _PartsAccesoriesConsumablesRespository.DeleteEntity(PartsAccesoriesConsumables)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetPartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If PartsAccesoriesConsumables Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PartsAccesoriesConsumablesRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
                PartsAccesoriesConsumables.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetPartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, status)

                While PartsAccesoriesConsumables.FixedAssetPartsAccesoriesConsumablesDetail.Count > 0
                    PartsAccesoriesConsumables.FixedAssetPartsAccesoriesConsumablesDetail(PartsAccesoriesConsumables.FixedAssetPartsAccesoriesConsumablesDetail.Count - 1).MarkAsDeleted()
                End While
                PartsAccesoriesConsumables.MarkAsDeleted()
                Me._PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
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
    ''' Función que obtiene PartsAccesoriesConsumables por Código
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String) As FixedAssetPartsAccesoriesConsumables Implements IFixedAssetPartsAccesoriesConsumablesAdminService.GetPartsAccesoriesConsumablesByCode
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
    Public Function ListAllPartsAccesoriesConsumables() As List(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesAdminService.ListAllPartsAccesoriesConsumables
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
    Public Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesAdminService.SavePartsAccesoriesConsumables
        'If PartsAccesoriesConsumables Is Nothing Then
        '    Throw New ArgumentNullException("PartsAccesoriesConsumables Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _PartsAccesoriesConsumablesRespository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try


        '    Dim seq As Domain.Entities.FixedAssetSequenceDetail = Nothing
        '    Dim auxPartAccesoriesConsumables = PartsAccesoriesConsumables.OriginalValue

        '    If PartsAccesoriesConsumables.Code Is Nothing OrElse PartsAccesoriesConsumables.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
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

        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        PartsAccesoriesConsumables.CreationUser = audit.CodeUser
        '        PartsAccesoriesConsumables.CreationDate = DateTime.Now
        '        'status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxPartAccesoriesConsumables = PartsAccesoriesConsumables.OriginalValue
        '        PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
        '        PartsAccesoriesConsumables.ModificationDate = DateTime.Now
        '        'status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        Me._PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
        '    End If
        '    UnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()

        '    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(Domain.Entities.FixedAssetPartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
        '        auditObject.Execute()
        '    ElseIf PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(Domain.Entities.FixedAssetPartsAccesoriesConsumables).Name, audit.Functional, PartsAccesoriesConsumables.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPartAccesoriesConsumables)
        '        auditObject.Execute()
        '    End If
        '    Return True

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
                    Dim seq As FixedAssetSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PartsAccesoriesConsumables.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), PartsAccesoriesConsumables.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetPartsAccesoriesConsumables = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetPartsAccesoriesConsumables)
                Dim status As Integer

                If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PartsAccesoriesConsumables.CreationUser = audit.CodeUser
                    PartsAccesoriesConsumables.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = PartsAccesoriesConsumables.OriginalValue
                    PartsAccesoriesConsumables.ModificationUser = audit.CodeUser
                    PartsAccesoriesConsumables.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._PartsAccesoriesConsumablesRespository.SaveEntity(PartsAccesoriesConsumables)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetPartsAccesoriesConsumables)(PartsAccesoriesConsumables, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                PartsAccesoriesConsumables.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = PartsAccesoriesConsumables, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    Function ChangeFixedAssetPartsAccesoriesConsumablesState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesAdminService.ChangeFixedAssetPartsAccesoriesConsumablesState
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
            Dim PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables = Me._PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByCode(code.Trim())
            If PartsAccesoriesConsumables IsNot Nothing AndAlso PartsAccesoriesConsumables.Id > 0 Then
                'PartsAccesoriesConsumables.Status = state
            End If
            Dim result = Me.SavePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPartsAccesoriesConsumables) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType As Integer) As List(Of FixedAssetItemTypePartsAccesories) Implements IFixedAssetPartsAccesoriesConsumablesAdminService.GetPartsAccesoriesConsumablesByEquipmentType
        If String.IsNullOrEmpty(IdEquipmentType) Then
            Throw New ArgumentNullException("IdEquipmentType vacio")
        End If
        Try
            Return _PartsAccesoriesConsumablesRespository.GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
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
