#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class EquipmentAdminService
    Implements IEquipmentAdminService

    'Repositorio de el tipo de equipo
    Private _EquipmentRepository As IEquipmentAllRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentRepository">Repositorio de tipo de equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentRepository As IEquipmentAllRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (EquipmentRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del tipo de equipo")
        End If
        _sequenceRepository = sequenceRepository
        _EquipmentRepository = EquipmentRepository
    End Sub

    Public Function DeleteEquipment(Equipment As Equipment, audit As AuditMessage) As Boolean Implements IEquipmentAdminService.DeleteEquipment
        If Equipment Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentRepository.UnitWork
        Try
            _EquipmentRepository.DeleteEntity(Equipment)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Equipment)(Equipment, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetEquipment(codeEquipment As String) As Equipment Implements IEquipmentAdminService.GetEquipment
        If String.IsNullOrEmpty(codeEquipment) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _EquipmentRepository.GetEquipment(codeEquipment)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Equipment()
        End Try
    End Function

    Public Function ListAllEquipment() As List(Of Equipment) Implements IEquipmentAdminService.ListAllEquipment
        Try
            Return _EquipmentRepository.ListAllEquipment
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveEquipment(Equipment As Equipment, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Equipment) Implements IEquipmentAdminService.SaveEquipment
        If Equipment Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxEntity As Domain.Entities.Equipment
            If Equipment.ChangeTracker.State = ObjectState.Modified Then
                auxEntity = _EquipmentRepository.GetEquipment(Equipment.Code, True)
            End If

            If Equipment.Code Is Nothing OrElse Equipment.Code.Trim().Equals(String.Empty) Then
                seq = _sequenceRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Equipment.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Equipment) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Equipment) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._EquipmentRepository.SaveEntity(Equipment)
            End If

            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Equipment).Name, audit.Functional, Equipment.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Equipment)(Equipment, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Equipment).Name, audit.Functional, Equipment.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Equipment)(Equipment, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxEntity)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Equipment) With {.StateResult = True, .ObjectEmbbeded = Equipment}

        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Equipment) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Equipment) Implements IEquipmentAdminService.ChangeState
        Dim Equipment As Equipment = _EquipmentRepository.GetEquipment(code)
        Equipment.State = state
        Equipment.MarkAsModified()
        Return SaveEquipment(Equipment, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _EquipmentRepository = Nothing
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
