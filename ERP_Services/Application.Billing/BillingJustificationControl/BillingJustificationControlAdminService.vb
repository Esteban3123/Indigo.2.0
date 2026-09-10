'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2022-07-29
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Billing
Imports Application.Security
#End Region

Public Class BillingJustificationControlAdminService
    Implements IBillingJustificationControlAdminService

    Private _JustificationControlRepository As IBillingJustificationControlRepository
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Public Const FORM_NAME As String = "FrmBillingJustificationControl"

    ''' <summary>
    ''' Aplicación
    ''' </summary>
    ''' <remarks></remarks>
    Private _IUserAdminService As IUserAdminService

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="DetailedConceptAdminService" />.
    ''' </summary>
    ''' <param name="justificationControlRepository">el repositorio para el manejo de las justificacion control.</param>
    Public Sub New(ByVal justificationControlRepository As IBillingJustificationControlRepository, secuenseDRepository As IBillingSequenceDetailRepository, IUserAdminService As IUserAdminService)
        If justificationControlRepository Is Nothing Then
            Throw New ArgumentNullException("justificationControl Vacio")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        _JustificationControlRepository = justificationControlRepository
        _secuenseDRepository = secuenseDRepository
        _IUserAdminService = IUserAdminService
    End Sub

    Public Function ListAllJustificationControl() As List(Of BillingJustificationControl) Implements IBillingJustificationControlAdminService.ListAllJustificationControl
        Try
            Return _JustificationControlRepository.ListAllJustificationControl()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteJustificationControl(justificationControl As BillingJustificationControl, audit As AuditMessage) As ActionResult Implements IBillingJustificationControlAdminService.DeleteJustificationControl
        If justificationControl Is Nothing Then
            Throw New ArgumentNullException("BillingJustificationControl")
        End If
        Dim unitOfWork As IUnitWork = _JustificationControlRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingJustificationControl)(justificationControl, audit, status)

                While justificationControl.BillingJustificationControlUser.Count > 0
                    justificationControl.BillingJustificationControlUser(justificationControl.BillingJustificationControlUser.Count - 1).MarkAsDeleted()
                End While
                justificationControl.MarkAsDeleted()
                Me._JustificationControlRepository.SaveEntity(justificationControl)
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

    Public Function SaveJustificationControl(justificationControl As BillingJustificationControl, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of BillingJustificationControl) Implements IBillingJustificationControlAdminService.SaveJustificationControl
        If justificationControl Is Nothing Then
            Throw New ArgumentNullException("Justificacion Control Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _JustificationControlRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(justificationControl.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            justificationControl.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BillingJustificationControl) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), justificationControl.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BillingJustificationControl) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If


                Dim auxCommon As BillingJustificationControl = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BillingJustificationControl)
                Dim status As Integer

                If justificationControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    justificationControl.CreationUser = audit.CodeUser
                    justificationControl.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = justificationControl.OriginalValue
                    justificationControl.ModificationUser = audit.CodeUser
                    justificationControl.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._JustificationControlRepository.SaveEntity(justificationControl)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BillingJustificationControl)(justificationControl, audit, status, auxCommon)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                justificationControl.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingJustificationControl) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = justificationControl, .Message = MessageResult}

            End Using

        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingJustificationControl) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingJustificationControl) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetBillingJustification(code As String, audit As AuditMessage) As ActionResult(Of BillingJustificationControl) Implements IBillingJustificationControlAdminService.GetBillingJustification
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code JustificationControl vacio")
        End If
        Try
            Dim justificationControl = _JustificationControlRepository.GetJustificationControl(code)

            'Se saca el listado de ids de usuario para enviar
            If justificationControl IsNot Nothing AndAlso justificationControl.Id > 0 _
                AndAlso justificationControl.BillingJustificationControlUser IsNot Nothing _
                AndAlso justificationControl.BillingJustificationControlUser.Any() Then

                'Se obtiene el listado de usuarios
                Dim listUsers = _IUserAdminService.ListUsersByIds(justificationControl.BillingJustificationControlUser.Select(Function(m) m.UserId).ToList())
                If listUsers IsNot Nothing AndAlso listUsers.Any() Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In justificationControl.BillingJustificationControlUser

                        Dim user = (From e In listUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.UserFullName = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of BillingJustificationControl) With {.StateResult = True, .ObjectEmbbeded = justificationControl}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetBillingJustificationById(id As Integer) As ActionResult(Of BillingJustificationControl) Implements IBillingJustificationControlAdminService.GetBillingJustificationById
        If Not (id > 0) Then
            Throw New ArgumentNullException("idJustificationControl Vacio")
        End If
        Try
            Dim billingJustification As BillingJustificationControl = _JustificationControlRepository.GetJustificationControlById(id)
            Return New ActionResult(Of BillingJustificationControl) With {.StateResult = True, .ObjectEmbbeded = billingJustification}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingJustificationControl) With {.StateResult = False, .Message = {ex.Message}.ToString}
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
