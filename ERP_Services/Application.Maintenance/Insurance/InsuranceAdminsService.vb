

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region

Public Class InsuranceAdminsService
    Implements IInsuranceAdminService

    'Repositorio de la aseguradora
    Private _InsuranceRepository As IInsuranceRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InsuranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal InsuranceRepository As IInsuranceRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (InsuranceRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de aseguradora vacio")
        End If
        _sequenceRepository = sequenceRepository
        _InsuranceRepository = InsuranceRepository
    End Sub

    ''' <summary>
    ''' funcion para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteInsurance(Insurance As Domain.Maintenance.Entities.Insurance, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IInsuranceAdminService.DeleteInsurance
        If Insurance Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _InsuranceRepository.UnitWork
        Try
            _InsuranceRepository.DeleteEntity(Insurance)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Insurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' funcion para consultar una aseguradora por codigo
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsurance(codeInsurance As String) As Domain.Maintenance.Entities.Insurance Implements IInsuranceAdminService.GetInsurance
        If String.IsNullOrEmpty(codeInsurance) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _InsuranceRepository.GetInsurance(codeInsurance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New Insurance()
        End Try
    End Function

    ''' <summary>
    ''' funcion para listar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllInsurance() As List(Of Domain.Maintenance.Entities.Insurance) Implements IInsuranceAdminService.ListAllInsurance
        Try
            Return _InsuranceRepository.ListAllInsurance
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' funcion para almacenar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveInsurance(Insurance As Domain.Maintenance.Entities.Insurance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Insurance) Implements IInsuranceAdminService.SaveInsurance
        If Insurance Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _InsuranceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxEntity As Domain.Maintenance.Entities.Insurance
            If Insurance.ChangeTracker.State = ObjectState.Modified Then
                auxEntity = _InsuranceRepository.GetInsurance(Insurance.Nit, True)
            End If

            If Insurance.Nit Is Nothing OrElse Insurance.Nit.Trim().Equals(String.Empty) Then
                seq = _sequenceRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Insurance.Nit = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Insurance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Insurance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._InsuranceRepository.SaveEntity(Insurance)
            End If

            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Insurance).Name, audit.Functional, Insurance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Insurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Insurance).Name, audit.Functional, Insurance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Insurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxEntity)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Insurance) With {.StateResult = True, .ObjectEmbbeded = Insurance}
        Catch ex As System.Data.Entity.Validation.DbEntityValidationException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Insurance) With {.StateResult = False}

        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Insurance) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Insurance) Implements IInsuranceAdminService.ChangeState
        Dim Insurance As Insurance = _InsuranceRepository.GetInsurance(code)
        Insurance.State = state
        Insurance.MarkAsModified()
        Return SaveInsurance(Insurance, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _InsuranceRepository = Nothing
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
