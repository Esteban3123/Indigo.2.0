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
Public Class BranchAdmisService
    Implements IBranchAdminService

    'Repositorio de la aseguradora
    Private _BranchRepository As IBranchRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de sucursal
    ''' </summary>
    ''' <param name="BranchRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal BranchRepository As IBranchRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (BranchRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de sucursal vacio")
        End If
        _sequenceRepository = sequenceRepository
        _BranchRepository = BranchRepository
    End Sub

    Public Function DeleteBranch(Branch As Branch, audit As AuditMessage) As Boolean Implements IBranchAdminService.DeleteBranch
        If Branch Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BranchRepository.UnitWork
        Try
            _BranchRepository.DeleteEntity(Branch)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Branch)(Branch, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetBranch(codeBranch As String) As Branch Implements IBranchAdminService.GetBranch
        If String.IsNullOrEmpty(codeBranch) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _BranchRepository.GetBranch(codeBranch)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Branch()
        End Try
    End Function

    Public Function ListAllBranch() As List(Of Branch) Implements IBranchAdminService.ListAllBranch
        Try
            Return _BranchRepository.ListAllBranch
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveBranch(Branch As Branch, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Branch) Implements IBranchAdminService.SaveBranch
        If Branch Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _BranchRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxBranch As Domain.Maintenance.Entities.Branch
            If Branch.ChangeTracker.State = ObjectState.Modified Then
                auxBranch = _BranchRepository.GetBranch(Branch.Code, True)
            End If

            If Branch.Code Is Nothing OrElse Branch.Code.Trim().Equals(String.Empty) Then
                seq = _sequenceRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Branch.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Branch) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Branch) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Branch.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Branch.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._BranchRepository.SaveEntity(Branch)
            End If

            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Branch.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Branch).Name, audit.Functional, Branch.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Branch)(Branch, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Branch.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Branch).Name, audit.Functional, Branch.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Branch)(Branch, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxBranch)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Branch) With {.StateResult = True, .ObjectEmbbeded = Branch}

        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Branch) With {.StateResult = False}
        End Try
    End Function

    Public Function ListAllCostCenter() As List(Of CostCenter) Implements IBranchAdminService.ListAllCostCenter
        Try
            Return _BranchRepository.ListAllCostCenter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Branch) Implements IBranchAdminService.ChangeState
        'Dim Branch As Branch = _BranchRepository.GetBranch(code)
        'Branch.State = state
        'Branch.MarkAsModified()
        'Return SaveBranch(Branch, audit)

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
            Dim Branch As Branch = Me._BranchRepository.GetBranch(code.Trim())
            If Branch IsNot Nothing AndAlso Branch.Id > 0 Then
                Branch.State = state
            End If
            Dim result = Me.SaveBranch(Branch, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Branch) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _BranchRepository = Nothing
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
