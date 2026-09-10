#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Transactions

#End Region

Public Class AccesoryAdminService
    Implements IAccesoryAdminService

    'Repositorio del accesorio
    Private _AccesoryRepository As IAccesoryRepository
    Private _sequenceRepository As IMaintenanceSequenceDetailRepository
    'Repositorio de la aseguradora
    Private _AccesoryDetailRepository As IAccesoryDetailRepository

    ''' <summary>
    ''' inicia el repositorio de sucursal
    ''' </summary>
    ''' <param name="AccesoryRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal AccesoryRepository As IAccesoryRepository, AccesoryDetailRepository As IAccesoryDetailRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (AccesoryRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de accesorio vacio")
        End If
        _sequenceRepository = sequenceRepository
        _AccesoryRepository = AccesoryRepository
        _AccesoryDetailRepository = AccesoryDetailRepository
    End Sub

    Public Function DeleteAccessory(Accessory As Domain.Entities.Accessory, audit As AuditMessage) As Boolean Implements IAccesoryAdminService.DeleteAccessory
        If Accessory Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _AccesoryRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While Accessory.AccesoryDetail.Any()
                    Accessory.AccesoryDetail.FirstOrDefault().MarkAsDeleted()
                End While
                Accessory.MarkAsDeleted()
                _AccesoryRepository.DeleteEntity(Accessory)
                unitWork.Commit()
                Dim auditObject As New IndigoAuditSimpleEntity(Of Accessory)(Accessory, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                scope.Complete()
                Return True
            End Using
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetAccessory(codeAccessory As String) As Accessory Implements IAccesoryAdminService.GetAccessory
        If String.IsNullOrEmpty(codeAccessory) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _AccesoryRepository.GetAccessory(codeAccessory)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Accessory()
        End Try
    End Function

    Public Function ListAllAccessory() As List(Of Accessory) Implements IAccesoryAdminService.ListAllAccessory
        Try
            Return _AccesoryRepository.ListAllAccessory
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveAccessory(Accessory As Accessory, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Accessory) Implements IAccesoryAdminService.SaveAccessory
        If Accessory Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _AccesoryRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
                Dim auxAccesory As Domain.Entities.Accessory
                If Accessory.ChangeTracker.State = ObjectState.Modified Then
                    auxAccesory = _AccesoryRepository.GetAccessory(Accessory.Code, True)
                End If

                If Accessory.Code Is Nothing OrElse Accessory.Code.Trim().Equals(String.Empty) Then
                    seq = _sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Accessory.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Accessory) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Accessory) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                If Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Me._AccesoryRepository.SaveEntity(Accessory)
                End If

                unitWork.Commit()
                sequenseUnitOfWork.Commit()

                If Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(Accessory).Name, audit.Functional, Accessory.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Accessory)(Accessory, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(Accessory).Name, audit.Functional, Accessory.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Accessory)(Accessory, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxAccesory)
                    auditObject.Execute()
                End If
                scope.Complete()
                Return New ActionResult(Of Accessory) With {.StateResult = True, .ObjectEmbbeded = Accessory}
            End Using

        Catch ex As System.Data.Entity.Validation.DbEntityValidationException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Accessory) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Accessory) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ListEquipmentType() As List(Of Domain.Entities.FixedAssetEquipmentType) Implements IAccesoryAdminService.ListEquipmentType
        Return _AccesoryRepository.ListEquipmentType
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Accessory) Implements IAccesoryAdminService.ChangeState
        Dim Accessory As Accessory = _AccesoryRepository.GetAccessory(code)
        Accessory.State = state
        Return SaveAccessory(Accessory, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _AccesoryRepository = Nothing
            _AccesoryDetailRepository = Nothing
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
