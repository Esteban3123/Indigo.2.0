

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre plantilla de tipos de equipo
''' </summary>
''' <remarks></remarks>
Public Class TemplateEquipmentTypeAdminService
    Implements ITemplateEquipmentTypeAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    'Repositorio de tipo de registro tecnico
    Private _TemplateEquipmentTypeRepository As ITemplateEquipmentTypeRepository

    ''' <summary>
    ''' inicia el repositorio de plantilla de tipo de equipo
    ''' </summary>
    ''' <param name="TemplateEquipmentTypeRepository">Repositorio de habitacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TemplateEquipmentTypeRepository As ITemplateEquipmentTypeRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (TemplateEquipmentTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Plantilla de tipo de equipo")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _TemplateEquipmentTypeRepository = TemplateEquipmentTypeRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeleteTemplateEquipmentType(TemplateEquipmentType As TemplateEquipmentType, audit As AuditMessage) As ActionResult Implements ITemplateEquipmentTypeAdminService.DeleteTemplateEquipmentType
        If TemplateEquipmentType Is Nothing Then
            Throw New ArgumentNullException("registro plantilla vacio")
        End If
        Dim unitWork As IUnitWork = _TemplateEquipmentTypeRepository.UnitWork
        Try
            TemplateEquipmentType.MarkAsDeleted()
            _TemplateEquipmentTypeRepository.DeleteEntity(TemplateEquipmentType)
            unitWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(Domain.Entities.TemplateEquipmentType).Name, audit.Functional, TemplateEquipmentType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TemplateEquipmentType)(TemplateEquipmentType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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

    Public Function GetTemplateEquipmentType(CodeTemplateEquipmentType As String, audit As AuditMessage) As TemplateEquipmentType Implements ITemplateEquipmentTypeAdminService.GetTemplateEquipmentType
        Try
            Dim template = _TemplateEquipmentTypeRepository.GetTemplateEquipmentType(CodeTemplateEquipmentType)
            If template IsNot Nothing AndAlso template.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TemplateEquipmentType)(template, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return template
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TemplateEquipmentType()
        End Try
    End Function

    Public Function ListAllTemplateEquipmentType() As List(Of TemplateEquipmentType) Implements ITemplateEquipmentTypeAdminService.ListAllTemplateEquipmentType
        Try
            Return _TemplateEquipmentTypeRepository.ListAllTemplateEquipmentType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveTemplateEquipmentType(TemplateEquipmentType As TemplateEquipmentType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.TemplateEquipmentType) Implements ITemplateEquipmentTypeAdminService.SaveTemplateEquipmentType
        If TemplateEquipmentType Is Nothing Then
            Throw New ArgumentNullException("TemplateEquipmentType vacio")
        End If
        Dim unitWork As IUnitWork = _TemplateEquipmentTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxTemplate = _TemplateEquipmentTypeRepository.GetTemplateEquipmentType(TemplateEquipmentType.Code, False)

            If TemplateEquipmentType.Code Is Nothing OrElse TemplateEquipmentType.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        TemplateEquipmentType.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Entities.TemplateEquipmentType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Entities.TemplateEquipmentType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If TemplateEquipmentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse TemplateEquipmentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._TemplateEquipmentTypeRepository.SaveEntity(TemplateEquipmentType)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If TemplateEquipmentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.TemplateEquipmentType).Name, audit.Functional, TemplateEquipmentType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TemplateEquipmentType)(TemplateEquipmentType, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf TemplateEquipmentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.TemplateEquipmentType).Name, audit.Functional, TemplateEquipmentType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TemplateEquipmentType)(TemplateEquipmentType, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxTemplate)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.TemplateEquipmentType) With {.StateResult = True, .ObjectEmbbeded = TemplateEquipmentType}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.TemplateEquipmentType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.TemplateEquipmentType) With {.StateResult = False}
        End Try
    End Function


    Public Function ChangeStateTemplate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TemplateEquipmentType) Implements ITemplateEquipmentTypeAdminService.ChangeStateTemplate
        Dim template = GetTemplateEquipmentType(code, audit)
        template.State = state
        template.MarkAsModified()
        Return SaveTemplateEquipmentType(template, audit)
    End Function

    Public Function GetTemplateEquipmentTypeByEquipmentTypeId(IdEquipmentType As Integer) As TemplateEquipmentType Implements ITemplateEquipmentTypeAdminService.GetTemplateEquipmentTypeByEquipmentTypeId
        Try
            Dim template = _TemplateEquipmentTypeRepository.GetTemplateEquipmentTypeByEquipmentTypeId(IdEquipmentType)
            Return template
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TemplateEquipmentType()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _TemplateEquipmentTypeRepository = Nothing
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
