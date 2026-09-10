#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class EquipmentCatalogAdminService

    Implements IEquipmentCatalogAdminService

    'Repositorio de la aseguradora
    Private _EquipmentCatalogRepository As IFixedAssetItemCatalogRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    Private _EquipmentCatalogDetailRepository As IFixedAssetItemCatalogDetailRepository


    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InsuranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentCatalogRepository As IFixedAssetItemCatalogRepository, sequenceRepository As IFixedAssetSequenceDetailRepository, EquipmentCatalogDetailRepository As IFixedAssetItemCatalogDetailRepository)
        If (EquipmentCatalogRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de aseguradora vacio")
        End If
        _sequenceRepository = sequenceRepository
        _EquipmentCatalogRepository = EquipmentCatalogRepository
        _EquipmentCatalogDetailRepository = EquipmentCatalogDetailRepository
    End Sub

    Public Function DeleteEquipmentCatalog(FixedAssetItemCatalog As FixedAssetItemCatalog, audit As AuditMessage) As ActionResult Implements IEquipmentCatalogAdminService.DeleteEquipmentCatalog
        'If FixedAssetItemCatalog Is Nothing Then
        '    Throw New ArgumentNullException("SettingFixedAsset vacio")
        'End If
        'Dim unitWork As IUnitWork = _EquipmentCatalogRepository.UnitWork
        '' Dim unitWorkDetail As IUnitWork = _EquipmentCatalogDetailRepository.UnitWork

        'Try

        '    While FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
        '        FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Item(FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Count() - 1).MarkAsDeleted()
        '    End While
        '    FixedAssetItemCatalog.MarkAsDeleted()
        '    '  unitWorkDetail.Commit()

        '    _EquipmentCatalogRepository.SaveEntity(FixedAssetItemCatalog)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetItemCatalog)(FixedAssetItemCatalog, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    '  unitWorkDetail.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If FixedAssetItemCatalog Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._EquipmentCatalogRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                FixedAssetItemCatalog.ModificationUser = audit.CodeUser
                FixedAssetItemCatalog.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetItemCatalog)(FixedAssetItemCatalog, audit, status)

                While FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Count > 0
                    FixedAssetItemCatalog.FixedAssetItemCatalogDetail(FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Count - 1).MarkAsDeleted()
                End While
                FixedAssetItemCatalog.MarkAsDeleted()
                Me._EquipmentCatalogRepository.SaveEntity(FixedAssetItemCatalog)
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

    Public Function GetEquipmentCatalogByCode(CodeCatalog As String) As FixedAssetItemCatalog Implements IEquipmentCatalogAdminService.GetEquipmentCatalogByCode
        Try
            Return _EquipmentCatalogRepository.GetFixedAssetItemCatalogByCode(CodeCatalog)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetItemCatalog()
        End Try
    End Function

    Public Function SaveEquipmentCatalog(FixedAssetItemCatalog As FixedAssetItemCatalog, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetItemCatalog) Implements IEquipmentCatalogAdminService.SaveEquipmentCatalog
        If FixedAssetItemCatalog Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentCatalogRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.FixedAssetSequenceDetail = Nothing
            Dim auxFixedAssetItemCatalog = FixedAssetItemCatalog.OriginalValue

            If FixedAssetItemCatalog.Code Is Nothing OrElse FixedAssetItemCatalog.Code.Trim().Equals(String.Empty) Then
                seq = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        FixedAssetItemCatalog.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Entities.FixedAssetItemCatalog) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Entities.FixedAssetItemCatalog) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                FixedAssetItemCatalog.CreationDate = Date.Now()
                FixedAssetItemCatalog.CreationUser = audit.CodeUser
            End If

            If FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                FixedAssetItemCatalog.ModificationDate = Date.Now()
                FixedAssetItemCatalog.ModificationUser = audit.CodeUser
            End If

            'If FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            Me._EquipmentCatalogRepository.SaveEntity(FixedAssetItemCatalog)
            'End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.FixedAssetItemCatalog).Name, audit.Functional, FixedAssetItemCatalog.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.FixedAssetItemCatalog)(FixedAssetItemCatalog, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.FixedAssetItemCatalog).Name, audit.Functional, FixedAssetItemCatalog.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.FixedAssetItemCatalog)(FixedAssetItemCatalog, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxFixedAssetItemCatalog)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.FixedAssetItemCatalog) With {.StateResult = True, .ObjectEmbbeded = FixedAssetItemCatalog}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.FixedAssetItemCatalog) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim message As String = ex.Message
            If ex.InnerException.InnerException IsNot Nothing Then
                message = ex.InnerException.InnerException.Message
            End If
            Return New ActionResult(Of Domain.Entities.FixedAssetItemCatalog) With {.StateResult = False, .StateResultAux = False, .Message = message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetItemCatalog) Implements IEquipmentCatalogAdminService.ChangeState
        'Dim Equipment As FixedAssetItemCatalog = _EquipmentCatalogRepository.GetFixedAssetItemCatalogByCode(code)
        ''Equipment.Status = state
        'Equipment.Active = state
        'Equipment.MarkAsModified()
        'Return SaveEquipmentCatalog(Equipment, audit)


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
            Dim Equipment As FixedAssetItemCatalog = Me._EquipmentCatalogRepository.GetFixedAssetItemCatalogByCode(code.Trim())
            If Equipment IsNot Nothing AndAlso Equipment.Id > 0 Then
                Equipment.Status = state
            End If
            Dim result = Me.SaveEquipmentCatalog(Equipment, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetItemCatalog) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _sequenceRepository = Nothing
            _EquipmentCatalogRepository = Nothing
            _EquipmentCatalogDetailRepository = Nothing
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
