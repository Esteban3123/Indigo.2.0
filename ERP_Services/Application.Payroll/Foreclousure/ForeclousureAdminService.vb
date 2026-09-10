'***********************************************************************
' Assembly         : Application.Payrol
' Author           : Daniel Eduardo Arévalo
' Created          : 12-09-2018
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ForeclousureAdminService

    Implements IForeclousureAdminService

    Private _ForeclousureRepository As IForeclousureRepository
    'repositorio de la secuencia
    Private _sequenceRepository As IPayrollSequenceDetailRepository

    Public Sub New(ByVal foreclousureRepository As IForeclousureRepository, ByVal sequenceRepository As IPayrollSequenceDetailRepository)
        If foreclousureRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _ForeclousureRepository = foreclousureRepository
        _sequenceRepository = sequenceRepository
    End Sub

    Public Function DeleteForeclousure(Foreclousure As Foreclousure, audit As AuditMessage) As ActionResult Implements IForeclousureAdminService.DeleteForeclousure
        If Foreclousure Is Nothing Then
            Throw New ArgumentNullException("Convenio vacio")
        End If
        Dim _unitWork As IUnitWork = _ForeclousureRepository.UnitWork
        Try
            _ForeclousureRepository.SaveEntity(Foreclousure)
            _unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Foreclousure)(Foreclousure, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch Ex As OptimisticConcurrencyException
            _unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch Ex As UpdateException
            _unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"000"})}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)()}
        End Try
    End Function

    Public Function GetForeclousure(consecutive As String, audit As AuditMessage) As Foreclousure Implements IForeclousureAdminService.GetForeclousure
        If String.IsNullOrEmpty(consecutive) Then
            Throw New ArgumentNullException("Consecutivo vacio")
        End If
        Try
            Dim _foreclousure = _ForeclousureRepository.GetForeclousure(consecutive, False)
            'If _AgreementsC.Id > 0 Then
            '    IndigoAuditSimpleEntity(Of AgreementsC).Execute(_AgreementsC, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            'End If
            Return _foreclousure
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Foreclousure
        End Try
    End Function

    Public Function ListForeclousure(audit As AuditMessage) As List(Of Foreclousure) Implements IForeclousureAdminService.ListForeclousure
        Try
            Dim ObjListForeclousure = _ForeclousureRepository.ListForeclousure()

            Return ObjListForeclousure
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Se guarda un Embargo
    ''' </summary>
    ''' <param name="Foreclousure">Foreclousure</param>
    ''' <param name="audit">audit</param>
    ''' <returns> ActionResult(Of Foreclousure)</returns>
    Public Function SaveForeclousure(Foreclousure As Foreclousure, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Foreclousure) Implements IForeclousureAdminService.SaveForeclousure
        If Foreclousure Is Nothing Then
            Throw New ArgumentNullException("Foreclousure vacio")
        End If
        Dim unitWork As IUnitWork = _ForeclousureRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.PayrollSequenceDetail = Nothing
            Dim auxForeclousure = Foreclousure.OriginalValue

            If Foreclousure.Code Is Nothing OrElse Foreclousure.Code.Trim().Equals(String.Empty) Then
                seq = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Foreclousure.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Foreclousure) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Foreclousure) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Foreclousure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Foreclousure.CreationDate = Date.Now()
                Foreclousure.CreationUser = audit.CodeUser
            End If

            If Foreclousure.State = 2 Then
                Foreclousure.ConfirmationDate = Date.Now()
                Foreclousure.ConfirmationUser = audit.CodeUser
            End If

            If Foreclousure.State = 3 Then
                Foreclousure.SuspendUser = audit.CodeUser
            End If

            If Foreclousure.State = 4 Then
                Foreclousure.AnnulmentUser = audit.CodeUser
            End If

            'If FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse FixedAssetItemCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            Me._ForeclousureRepository.SaveEntity(Foreclousure)
            'End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Foreclousure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.FixedAssetItemCatalog).Name, audit.Functional, Foreclousure.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Foreclousure)(Foreclousure, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Foreclousure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Foreclousure).Name, audit.Functional, Foreclousure.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Foreclousure)(Foreclousure, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxForeclousure)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Foreclousure) With {.StateResult = True, .ObjectEmbbeded = Foreclousure}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Foreclousure) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim message As String = ex.Message
            If ex.InnerException.InnerException IsNot Nothing Then
                message = ex.InnerException.InnerException.Message
            End If
            Return New ActionResult(Of Foreclousure) With {.StateResult = False, .StateResultAux = False, .Message = message}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
