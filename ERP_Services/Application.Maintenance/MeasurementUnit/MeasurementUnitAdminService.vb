

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class MeasurementUnitAdminService
    Implements IMeasurementUnitAdminService


    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    'Repositorio de tipo de unidad de medida
    Private _MeasurementUnitRepository As IMeasurementUnitRepository

    ''' <summary>
    ''' inicia el repositorio de unidad de medida
    ''' </summary>
    ''' <param name="MeasurementUnitRepository">Repositorio de unidad de medida</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal MeasurementUnitRepository As IMeasurementUnitRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (MeasurementUnitRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de unidad de medida")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _MeasurementUnitRepository = MeasurementUnitRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeleteMeasurementUnit(MeasurementUnit As MeasurementUnit, audit As AuditMessage) As actionresult Implements IMeasurementUnitAdminService.DeleteMeasurementUnit
        If MeasurementUnit Is Nothing Then
            Throw New ArgumentNullException("parte vacio")
        End If
        Dim unitWork As IUnitWork = _MeasurementUnitRepository.UnitWork
        Try
            MeasurementUnit.MarkAsDeleted()
            _MeasurementUnitRepository.DeleteEntity(MeasurementUnit)
            unitWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(Domain.Entities.MeasurementUnit).Name, audit.Functional, MeasurementUnit.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.MeasurementUnit)(MeasurementUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}

        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    Public Function GetMeasurementUnit(codeMeasurementUnit As String, audit As AuditMessage) As MeasurementUnit Implements IMeasurementUnitAdminService.GetMeasurementUnit
        If String.IsNullOrEmpty(codeMeasurementUnit) Then
            Throw New ArgumentNullException("Codigo de unidad de medida")
        End If
        Try
            Dim MeasurementUnit = _MeasurementUnitRepository.GetMeasurementUnit(codeMeasurementUnit)
            If MeasurementUnit IsNot Nothing And MeasurementUnit.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.MeasurementUnit)(MeasurementUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return MeasurementUnit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MeasurementUnit()
        End Try

    End Function

    Public Function ListAllMeasurementUnit() As List(Of MeasurementUnit) Implements IMeasurementUnitAdminService.ListAllMeasurementUnit
        Try
            Return _MeasurementUnitRepository.ListAllMeasurementUnit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveMeasurementUnit(MeasurementUnit As MeasurementUnit, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of MeasurementUnit) Implements IMeasurementUnitAdminService.SaveMeasurementUnit
        If MeasurementUnit Is Nothing Then
            Throw New ArgumentNullException("parte vacio")
        End If
        Dim unitWork As IUnitWork = _MeasurementUnitRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxMeasurementUnit = _MeasurementUnitRepository.GetMeasurementUnit(MeasurementUnit.Code, False)

            If MeasurementUnit.Code Is Nothing OrElse MeasurementUnit.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        MeasurementUnit.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Entities.MeasurementUnit) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Entities.MeasurementUnit) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If MeasurementUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse MeasurementUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._MeasurementUnitRepository.SaveEntity(MeasurementUnit)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If MeasurementUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.MeasurementUnit).Name, audit.Functional, MeasurementUnit.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.MeasurementUnit)(MeasurementUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf MeasurementUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.MeasurementUnit).Name, audit.Functional, MeasurementUnit.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.MeasurementUnit)(MeasurementUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxMeasurementUnit)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.MeasurementUnit) With {.StateResult = True, .ObjectEmbbeded = MeasurementUnit}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.MeasurementUnit) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.MeasurementUnit) With {.StateResult = False}
        End Try
    End Function

    Public Function ChangeStateMeasurementUnit(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MeasurementUnit) Implements IMeasurementUnitAdminService.ChangeStateMeasurementUnit
        Dim MeasurementUnit As Domain.Entities.MeasurementUnit = GetMeasurementUnit(code, audit)
        MeasurementUnit.State = state
        MeasurementUnit.MarkAsModified()
        Return SaveMeasurementUnit(MeasurementUnit, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _MeasurementUnitRepository = Nothing
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
