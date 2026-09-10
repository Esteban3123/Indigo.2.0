'***********************************************************************
' Assembly         : Application.Payments
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 2021-01-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class DocumentSupportAdminService
    Implements IDocumentSupportAdminService

    Private Const FORM_NAME As String = "FrmDocumentSupport"

#Region "Properties"
    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository
    Private _DocumentSupportRepository As IDocumentSupportRepository

    ''' <summary>
    ''' Aplicación
    ''' </summary>
    ''' <remarks></remarks>
    Private _userAdminService As IUserAdminService

#End Region

#Region "Methods"

    Public Sub New(secuenceDRepository As ISequensePaymentsDRepository, ByVal DocumentSupport As IDocumentSupportRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If DocumentSupport Is Nothing Then
            Throw New ArgumentNullException("DocumentSupport vacío")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        Me._secuenseDRepository = secuenceDRepository
        Me._DocumentSupportRepository = DocumentSupport
        Me._userAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de los documentos autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListDocumentSupportByUserCode(userCode As String) As List(Of DocumentSupport) Implements IDocumentSupportAdminService.ListDocumentSupportByUserCode
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode Vacio")
        End If
        Try
            Return _DocumentSupportRepository.ListDocumentSupportByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una autorizacion del Documento
    ''' </summary>
    ''' <param name="DocumentSupport"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDocumentSupport(DocumentSupport As DocumentSupport, audit As AuditMessage) As ActionResult Implements IDocumentSupportAdminService.DeleteDocumentSupport
        If DocumentSupport Is Nothing Then
            Throw New ArgumentNullException("DocumentSupport")
        End If
        Dim unitOfWork As IUnitWork = Me._DocumentSupportRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                DocumentSupport.ModificationUser = audit.CodeUser
                DocumentSupport.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DocumentSupport)(DocumentSupport, audit, status)

                While DocumentSupport.DocumentSupportUser.Count > 0
                    DocumentSupport.DocumentSupportUser(DocumentSupport.DocumentSupportUser.Count - 1).MarkAsDeleted()
                End While
                DocumentSupport.MarkAsDeleted()
                Me._DocumentSupportRepository.SaveEntity(DocumentSupport)
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
    ''' Guarda o actualiza una autorización
    ''' </summary>
    ''' <param name="DocumentSupport">The billing authorization.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">distributionManpower</exception>
    Public Function SaveDocumentSupport(DocumentSupport As DocumentSupport, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of DocumentSupport) Implements IDocumentSupportAdminService.SaveDocumentSupport
        If DocumentSupport Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._DocumentSupportRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(DocumentSupport.Code) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            DocumentSupport.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DocumentSupport) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), DocumentSupport.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DocumentSupport) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As DocumentSupport = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DocumentSupport)
                Dim status As Integer

                If DocumentSupport.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    DocumentSupport.CreationUser = audit.CodeUser
                    DocumentSupport.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    'Permitir actualizar el consecutivo, pero solo numeros mayores
                    Dim DocumentSupportAux As Domain.Entities.DocumentSupport = _DocumentSupportRepository.GetDocumentSupportById(DocumentSupport.Id, False)
                    If DocumentSupportAux IsNot Nothing AndAlso DocumentSupportAux.Consecutive > DocumentSupport.Consecutive Then
                        DocumentSupport.Consecutive = DocumentSupportAux.Consecutive
                    End If

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = DocumentSupport.OriginalValue
                    DocumentSupport.ModificationUser = audit.CodeUser
                    DocumentSupport.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._DocumentSupportRepository.SaveEntity(DocumentSupport)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DocumentSupport)(DocumentSupport, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                DocumentSupport.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DocumentSupport) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = DocumentSupport, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización del documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportById(id As Integer, audit As AuditMessage) As ActionResult(Of DocumentSupport) Implements IDocumentSupportAdminService.GetDocumentSupportById

        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DocumentSupport As Domain.Entities.DocumentSupport = _DocumentSupportRepository.GetDocumentSupportById(id)
            Return New ActionResult(Of Domain.Entities.DocumentSupport) With {.StateResult = True, .ObjectEmbbeded = DocumentSupport}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene una autorización del documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportByCode(code As String, audit As AuditMessage) As ActionResult(Of DocumentSupport) Implements IDocumentSupportAdminService.GetDocumentSupportByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DocumentSupport As DocumentSupport = _DocumentSupportRepository.GetDocumentSupportByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If DocumentSupport IsNot Nothing AndAlso DocumentSupport.Id > 0 AndAlso DocumentSupport.DocumentSupportUser IsNot Nothing AndAlso DocumentSupport.DocumentSupportUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In DocumentSupport.DocumentSupportUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _userAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In DocumentSupport.DocumentSupportUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of DocumentSupport) With {.StateResult = True, .ObjectEmbbeded = DocumentSupport}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDocumentSupport(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DocumentSupport) Implements IDocumentSupportAdminService.ChangeStateDocumentSupport

        'Dim DocumentSupport As DocumentSupport = _DocumentSupportRepository.GetDocumentSupportByCode(code)
        'DocumentSupport.Status = state
        'Return SaveDocumentSupport(DocumentSupport, audit)

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
            Dim DocumentSupport As DocumentSupport = Me._DocumentSupportRepository.GetDocumentSupportByCode(code.Trim())
            If DocumentSupport IsNot Nothing AndAlso DocumentSupport.Id > 0 Then
                DocumentSupport.Status = state
            End If
            Dim result = Me.SaveDocumentSupport(DocumentSupport, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la resolución 
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportResolution(operatingUnitId As Integer, DocumentSupport As DocumentSupport, audit As AuditMessage) As ActionResult(Of DocumentSupport) Implements IDocumentSupportAdminService.GetDocumentSupportResolution
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Return New ActionResult(Of DocumentSupport) With {.StateResult = False, .Message = "Sin normatividad"}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _userAdminService.Dispose()
            End If
            _secuenseDRepository = Nothing
            _DocumentSupportRepository = Nothing
            _userAdminService = Nothing
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
