

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class InventoryTypeAdminService
    Implements IInventoryTypeAdminService



    'Repositorio de tipo de inventario
    Private _InventoryTypeRepository As IInvetoryTypeRepository

    Private _sequenceRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InventoryTypeRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>

    Public Sub New(ByVal InventoryTypeRepository As IInvetoryTypeRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (InventoryTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de tipo de inventario")
        End If
        _sequenceRepository = sequenceRepository
        _InventoryTypeRepository = InventoryTypeRepository
    End Sub



    Public Function DeleteInsurance(InventoryType As Domain.Maintenance.Entities.InventoryType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IInventoryTypeAdminService.DeleteInventoryType
        If InventoryType Is Nothing Then
            Throw New ArgumentNullException("tipo de inventario vacio")
        End If
        Dim unitWork As IUnitWork = _InventoryTypeRepository.UnitWork
        Try
            _InventoryTypeRepository.DeleteEntity(InventoryType)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of InventoryType)(InventoryType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetInsurance(codeInventoryType As String) As Domain.Maintenance.Entities.InventoryType Implements IInventoryTypeAdminService.GetInventoryType
        If String.IsNullOrEmpty(codeInventoryType) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try

            Return _InventoryTypeRepository.GetInventoryType(codeInventoryType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New InventoryType()
        End Try
    End Function

    Public Function ListAllInventoryType() As List(Of Domain.Maintenance.Entities.InventoryType) Implements IInventoryTypeAdminService.ListAllInventoryType
        Try
            Return _InventoryTypeRepository.ListAllInventoryType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveInventoryType(InventoryType As Domain.Maintenance.Entities.InventoryType, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InventoryType) Implements IInventoryTypeAdminService.SaveInventoryType
        If InventoryType Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _InventoryTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxEntity As Domain.Maintenance.Entities.InventoryType
            If InventoryType.ChangeTracker.State = ObjectState.Modified Then
                auxEntity = _InventoryTypeRepository.GetInventoryType(InventoryType.Code, False)
            End If

            If InventoryType.Code Is Nothing OrElse InventoryType.Code.Trim().Equals(String.Empty) Then
                seq = _sequenceRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        InventoryType.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of InventoryType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of InventoryType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If InventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse InventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._InventoryTypeRepository.SaveEntity(InventoryType)
            End If

            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If InventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(InventoryType).Name, audit.Functional, InventoryType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of InventoryType)(InventoryType, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf InventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(InventoryType).Name, audit.Functional, InventoryType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of InventoryType)(InventoryType, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxEntity)
                auditObject.Execute()
            End If
            Return New ActionResult(Of InventoryType) With {.StateResult = True, .ObjectEmbbeded = InventoryType}

        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InventoryType) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of InventoryType) Implements IInventoryTypeAdminService.ChangeState
        'Dim InventoryType As InventoryType = _InventoryTypeRepository.GetInventoryType(code)
        'InventoryType.State = state
        'InventoryType.MarkAsModified()
        'Return SaveInventoryType(InventoryType, audit)

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
            Dim InventoryType As InventoryType = Me._InventoryTypeRepository.GetInventoryType(code.Trim())
            If InventoryType IsNot Nothing AndAlso InventoryType.Id > 0 Then
                InventoryType.State = state
            End If
            Dim result = Me.SaveInventoryType(InventoryType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InventoryType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _InventoryTypeRepository = Nothing
            _sequenceRepository = Nothing
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
