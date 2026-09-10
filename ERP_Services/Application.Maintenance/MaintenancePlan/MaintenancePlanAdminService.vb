'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 04-09-2015
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
Imports System.Transactions

#End Region

Public Class MaintenancePlanAdminService
    Implements IMaintenancePlanAdminService

    'Repositorio de tipo de ubicacion
    Private _MaintenancePlanRespository As IMaintenancePlanRepository

    'repositorio de la secuencia
    Private _sequenceDRepository As IMaintenanceSequenceDetailRepository

    'repositorio de la secuencia
    Private _MaintenancePlanDetailRepository As IMaintenancePlanDetailRepository


    Private _MaintenanceActivityRepository As IMaintenanceActivityRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="MaintenancePlanRespository">Repositorio de MaintenancePlanRespository</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal MaintenancePlanRespository As IMaintenancePlanRepository, sequenceRepository As IMaintenanceSequenceDetailRepository, MaintenancePlanDetailRepository As IMaintenancePlanDetailRepository, MaintenanceActivityRepository As IMaintenanceActivityRepository)
        If (MaintenancePlanRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de MaintenancePlanRespository")
        End If
        _MaintenancePlanRespository = MaintenancePlanRespository
        _sequenceDRepository = sequenceRepository
        _MaintenancePlanDetailRepository = MaintenancePlanDetailRepository
        _MaintenanceActivityRepository = MaintenanceActivityRepository
    End Sub

    Public Function DeleteMaintenancePlan(MaintenancePlan As MaintenancePlan, audit As AuditMessage) As Boolean Implements IMaintenancePlanAdminService.DeleteMaintenancePlan
        If MaintenancePlan Is Nothing Then
            Throw New ArgumentNullException("Plan de Mantenimiento vacio")
        End If
        Dim unitWork As IUnitWork = _MaintenancePlanRespository.UnitWork
        Dim unitWorkPlanDetail As IUnitWork = _MaintenancePlanDetailRepository.UnitWork
        Dim unitWorkMaintenanceActivity As IUnitWork = _MaintenanceActivityRepository.UnitWork

        Try

            While MaintenancePlan.MaintenancePlanDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = MaintenancePlan.MaintenancePlanDetail.Count - 1
                _MaintenancePlanDetailRepository.DeleteEntity(MaintenancePlan.MaintenancePlanDetail(index))
            End While

            While MaintenancePlan.MaintenancePlanDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = MaintenancePlan.MaintenancePlanDetail.Count - 1

                While MaintenancePlan.MaintenancePlanDetail.Item(index).MaintenanceActivity.Count > 0
                    Dim index1 = MaintenancePlan.MaintenancePlanDetail.Item(index).MaintenanceActivity.Count - 1
                    _MaintenanceActivityRepository.DeleteEntity(MaintenancePlan.MaintenancePlanDetail(index).MaintenanceActivity(index1))
                End While

            End While

            unitWorkPlanDetail.Commit()
            unitWorkMaintenanceActivity.Commit()

            _MaintenancePlanRespository.DeleteEntity(MaintenancePlan)

            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of EquipmentRegistration).Execute(EquipmentRegistration, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)
            Return True
        Catch ex As Exception
            unitWorkPlanDetail.RollbackChanges()
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetMaintenancePlan(Code As String, Optional tracking As Boolean = False) As MaintenancePlan Implements IMaintenancePlanAdminService.GetMaintenancePlan
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code vacio")
        End If
        Try

            Return _MaintenancePlanRespository.GetMaintenancePlan(Code, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenancePlan()
        End Try
    End Function

    Public Function SaveMaintenancePlan(MaintenancePlan As MaintenancePlan, idSequense As Long, audit As AuditMessage) As ActionResult(Of MaintenancePlan) Implements IMaintenancePlanAdminService.SaveMaintenancePlan
        If MaintenancePlan Is Nothing Then
            Throw New ArgumentNullException("MaintenancePlan vacio")
        End If
        Dim unitWork As IUnitWork = _MaintenancePlanRespository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Dim result As New ActionResult(Of EquipmentRegistration)
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                'Valido si se guarda o se edita
                If MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
                    If MaintenancePlan.Code Is Nothing OrElse MaintenancePlan.Code.Trim().Equals(String.Empty) Then
                        seq = Me._sequenceDRepository.GetSequenseDById(idSequense)
                        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                                MaintenancePlan.Code = res
                                seq.Next += 1
                                Me._sequenceDRepository.SaveEntity(seq)
                            Else
                                transaction.Dispose()
                                Return New ActionResult(Of MaintenancePlan) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                            End If
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of MaintenancePlan) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                        End If
                    End If
                    MaintenancePlan.CreationUser = audit.IdUser
                    MaintenancePlan.CreationDate = Date.Now()
                    _MaintenancePlanRespository.SaveEntity(MaintenancePlan)
                    sequenceUnitOfWork.Commit()
                ElseIf MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    MaintenancePlan.ModificationUser = audit.IdUser
                    MaintenancePlan.ModificationDate = Date.Now()
                    _MaintenancePlanRespository.SaveEntity(MaintenancePlan)
                End If
                If MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of MaintenancePlan)(MaintenancePlan, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of MaintenancePlan)(MaintenancePlan, audit, Infrastructure.CrossCutting.Audit.Actions.Update, MaintenancePlan)
                    auditObject.Execute()
                End If
                unitWork.Commit()
                transaction.Complete()
            End Using
            Return New ActionResult(Of MaintenancePlan) With {.StateResult = True, .ObjectEmbbeded = MaintenancePlan}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenancePlan) With {.StateResult = False, .ObjectEmbbeded = MaintenancePlan, .MessageResult = {ex.Message.ToString()}.ToList()}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _MaintenancePlanRespository = Nothing
            _sequenceDRepository = Nothing
            _MaintenancePlanDetailRepository = Nothing
            _MaintenanceActivityRepository = Nothing
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
