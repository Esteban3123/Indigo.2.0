#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions

#End Region
Public Class FixedAssetItemTypeAdminService
    Implements IFixedAssetItemTypeAdminService


    'Repositorio de el tipo de equipo
    Private _EquipmentTypeRepository As IFixedAssetItemTypeRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentTypeAdminService">Repositorio de tipo de equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentTypeAdminService As IFixedAssetItemTypeRepository)
        If (EquipmentTypeAdminService Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del tipo de equipo")
        End If
        _EquipmentTypeRepository = EquipmentTypeAdminService
    End Sub

    Public Function DeleteEquipmentType(EquipmentType As FixedAssetItemType, audit As AuditMessage) As Boolean Implements IFixedAssetItemTypeAdminService.DeleteEquipmentType
        If EquipmentType Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentTypeRepository.UnitWork
        Try
            EquipmentType.MarkAsDeleted()
            _EquipmentTypeRepository.DeleteEntity(EquipmentType)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetItemType)(EquipmentType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetEquipmentType(codeEquipmentType As String) As FixedAssetItemType Implements IFixedAssetItemTypeAdminService.GetEquipmentType
        If String.IsNullOrEmpty(codeEquipmentType) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _EquipmentTypeRepository.GetEquipmentType(codeEquipmentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New FixedAssetItemType()
        End Try
    End Function

    Public Function ListAllEquipmentType() As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeAdminService.ListAllEquipmentType
        Try
            Return _EquipmentTypeRepository.ListAllEquipmentType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveFixedAssetItemType(EquipmentType As FixedAssetItemType, audit As AuditMessage) As ActionResult Implements IFixedAssetItemTypeAdminService.SaveFixedAssetItemType
        If EquipmentType Is Nothing Then
            Throw New ArgumentNullException("EquipmentType")
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim unitOfWork As IUnitWork = _EquipmentTypeRepository.UnitWork

                Dim auxRefund As FixedAssetItemType = Nothing
                Dim status As Integer
                If EquipmentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    EquipmentType.CreationDate = Date.Now
                    EquipmentType.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    EquipmentType.ModificationDate = Date.Now
                    EquipmentType.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxRefund = EquipmentType.OriginalValue
                End If

                Me._EquipmentTypeRepository.SaveEntity(EquipmentType)
                unitOfWork.Commit()

                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetItemType)(EquipmentType, audit, status, auxRefund)
                auditProcess.Execute()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveEquipmentType(EquipmentType As List(Of FixedAssetItemType), audit As AuditMessage) As Boolean Implements IFixedAssetItemTypeAdminService.SaveEquipmentType
        If EquipmentType Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentTypeRepository.UnitWork
        Try
            For Each Objeto As FixedAssetItemType In EquipmentType

                'If Objeto.ChangeTracker.State = ObjectState.Added Then
                '    Objeto.CreationDate = Date.Now
                '    Objeto.CreationUser = audit.CodeUser
                '    Objeto.Status = True
                'End If

                'If Objeto.ChangeTracker.State = ObjectState.Modified Then
                '    Objeto.ModificationDate = Date.Now()
                '    Objeto.ModificationUser = audit.CodeUser
                '    Objeto.Status = True
                'End If

                'If Objeto.ChangeTracker.State = ObjectState.Added Or Objeto.ChangeTracker.State = ObjectState.Modified Then
                _EquipmentTypeRepository.SaveEntity(Objeto)
                'End If

                '    'Valido si se guarda o se edita
                '    If Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then

                '    ElseIf Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '_EquipmentTypeAdminService.SaveEntity(Objeto)
                '    ElseIf Objeto.ChangeTracker.State = ObjectState.Deleted Then
                '_EquipmentTypeAdminService.DeleteEntity(Objeto)
                '    End If
            Next



            unitWork.Commit()

            For Each Objeto As FixedAssetItemType In EquipmentType
                If Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(FixedAssetItemType).Name, audit.Functional, Objeto.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetItemType)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(FixedAssetItemType).Name, audit.Functional, Objeto.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetItemType)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Update, Objeto)
                    auditObject.Execute()
                End If
            Next
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeAdminService.ListEquipmentTypeInventoryType
        Try
            Return _EquipmentTypeRepository.ListEquipmentTypeInventoryType(IdInventoryType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetEquipmentTypeById(IdEquipmentType As Integer) As FixedAssetItemType Implements IFixedAssetItemTypeAdminService.GetEquipmentTypeById
        If String.IsNullOrEmpty(IdEquipmentType) Then
            Throw New ArgumentNullException("IdEquipmentType vacio")
        End If
        Try

            Return _EquipmentTypeRepository.GetEquipmentTypeById(IdEquipmentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New FixedAssetItemType()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EquipmentTypeRepository = Nothing
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
