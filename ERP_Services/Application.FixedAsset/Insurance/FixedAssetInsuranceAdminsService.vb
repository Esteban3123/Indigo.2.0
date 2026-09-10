

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetInsuranceAdminsService
    Implements IFixedAssetInsuranceAdminService

    Private Const FORM_NAME As String = "FrmFixedAssetInsurance"
    'Repositorio de la aseguradora
    Private _InsuranceRepository As IFixedAssetInsuranceRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InsuranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal InsuranceRepository As IFixedAssetInsuranceRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
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
    Public Function DeleteInsurance(Insurance As FixedAssetInsurance, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IFixedAssetInsuranceAdminService.DeleteInsurance
        'If Insurance Is Nothing Then
        '    Throw New ArgumentNullException("Banco vacio")
        'End If
        'Dim unitWork As IUnitWork = _InsuranceRepository.UnitWork
        'Try
        '    _InsuranceRepository.DeleteEntity(Insurance)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInsurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If Insurance Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._InsuranceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Insurance.ModificationUser = audit.CodeUser
                Insurance.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetInsurance)(Insurance, audit, status)

                'While Insurance.InvoiceCategoriesUser.Count > 0
                '    Insurance.InvoiceCategoriesUser(Insurance.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Insurance.MarkAsDeleted()
                Me._InsuranceRepository.SaveEntity(Insurance)
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

    ''' <summary>
    ''' funcion para consultar una aseguradora por codigo
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsurance(codeInsurance As String) As FixedAssetInsurance Implements IFixedAssetInsuranceAdminService.GetInsurance
        If String.IsNullOrEmpty(codeInsurance) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _InsuranceRepository.GetInsurance(codeInsurance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetInsurance()
        End Try
    End Function

    ''' <summary>
    ''' funcion para listar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllInsurance() As List(Of FixedAssetInsurance) Implements IFixedAssetInsuranceAdminService.ListAllInsurance
        Try
            Return _InsuranceRepository.ListAllInsurance
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
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
    Public Function SaveInsurance(Insurance As FixedAssetInsurance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetInsurance) Implements IFixedAssetInsuranceAdminService.SaveInsurance
        'If Insurance Is Nothing Then
        '    Throw New ArgumentNullException("Aseguradora vacio")
        'End If
        'Dim unitWork As IUnitWork = _InsuranceRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        'Try
        '    Dim seq As Domain.Entities.FixedAssetSequenceDetail = Nothing
        '    Dim auxEntity As FixedAssetInsurance
        '    auxEntity = Insurance.OriginalValue

        '    If Insurance.Code Is Nothing OrElse Insurance.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenceRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Insurance.Code = res
        '                seq.Next += 1
        '                Me._sequenceRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Insurance.CreationUser = audit.CodeUser
        '        Insurance.CreationDate = Date.Now()
        '    Else
        '        Insurance.ModificationUser = audit.CodeUser
        '        Insurance.ModificationDate = Date.Now()
        '    End If

        '    Me._InsuranceRepository.SaveEntity(Insurance)
        '    unitWork.Commit()
        '    sequenseUnitOfWork.Commit()

        '    If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(FixedAssetInsurance).Name, audit.Functional, Insurance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInsurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
        '        auditObject.Execute()
        '    ElseIf Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        '/***** Auditoria Basica ********/
        '        IndigoAuditBasic.Execute(GetType(FixedAssetInsurance).Name, audit.Functional, Insurance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
        '        '/***** Auditoria Avanzada ******/
        '        Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInsurance)(Insurance, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxEntity)
        '        auditObject.Execute()
        '    End If
        '    Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = True, .ObjectEmbbeded = Insurance}
        'Catch ex As System.Data.Entity.Validation.DbEntityValidationException
        '    unitWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False}

        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False}
        'End Try



        If Insurance Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._InsuranceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Insurance.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Insurance.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetInsurance) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Insurance.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetInsurance) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetInsurance = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetInsurance)
                Dim status As Integer

                If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Insurance.CreationUser = audit.CodeUser
                    Insurance.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Insurance.OriginalValue
                    Insurance.ModificationUser = audit.CodeUser
                    Insurance.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._InsuranceRepository.SaveEntity(Insurance)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetInsurance)(Insurance, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Insurance.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Insurance, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance) Implements IFixedAssetInsuranceAdminService.ChangeState
        'Dim Insurance As FixedAssetInsurance = _InsuranceRepository.GetInsurance(code)
        'Insurance.Status = state
        'Insurance.MarkAsModified()
        'Return SaveInsurance(Insurance, audit)


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
            Dim Insurance As FixedAssetInsurance = Me._InsuranceRepository.GetInsurance(code.Trim())
            If Insurance IsNot Nothing AndAlso Insurance.Id > 0 Then
                Insurance.Status = state
            End If
            Dim result = Me.SaveInsurance(Insurance, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInsurance) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _InsuranceRepository = Nothing
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
