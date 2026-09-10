'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class BookAdminService
    Implements IBookAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _bookRepository As IBookRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal bookRepository As IBookRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        If bookRepository Is Nothing Then
            Throw New ArgumentNullException("bookRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        Me._bookRepository = bookRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del libro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of LegalBook) Implements IBookAdminService.ChangeState
        Dim Book As LegalBook = _bookRepository.GetBookByCode(code)
        Book.Status = state
        Return SaveBook(Book, audit)
    End Function

    ''' <summary>
    ''' Elimina un libro
    ''' </summary>
    ''' <param name="Book"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBook(Book As LegalBook, audit As AuditMessage) As ActionResult Implements IBookAdminService.DeleteBook
        If Book Is Nothing Then
            Throw New ArgumentNullException("Book")
        End If
        Dim unitOfWork As IUnitWork = Me._bookRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of LegalBook)
            auditProcess = New IndigoAuditSimpleEntity(Of LegalBook)(Book, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._bookRepository.DeleteEntity(Book)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un libro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookByCode(code As String, audit As AuditMessage) As ActionResult(Of LegalBook) Implements IBookAdminService.GetBookByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Book As LegalBook = Me._bookRepository.GetBookByCode(code.Trim())
            If Book IsNot Nothing AndAlso Book.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of LegalBook)(Book, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of LegalBook) With {.StateResult = True, .ObjectEmbbeded = Book}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un libro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookById(id As Integer, audit As AuditMessage) As ActionResult(Of LegalBook) Implements IBookAdminService.GetBookById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Book As LegalBook = Me._bookRepository.GetBookById(id)
            If Book IsNot Nothing AndAlso Book.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of LegalBook)(Book, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of LegalBook) With {.StateResult = True, .ObjectEmbbeded = Book}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un libro
    ''' </summary>
    ''' <param name="Book"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBook(Book As LegalBook, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of LegalBook) Implements IBookAdminService.SaveBook
        If Book Is Nothing Then
            Throw New ArgumentNullException("Book")
        End If
        Dim unitOfWork As IUnitWork = Me._bookRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            'Se valida que el registro no tenga el campo officialBook en true siempre y cuando ya exista un registro de esa manera
            Dim validate = _bookRepository.ValidateOfficialBook()
            If validate IsNot Nothing AndAlso (validate.Id <> Book.Id AndAlso Book.OfficialBook = True) Then
                Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {"ErrorOfficialBook"}.ToList}
            End If

            If Book.Id > 0 Then
                Dim updateBook = Book
                Book = _bookRepository.GetBookById(Book.Id)

                If Book Is Nothing OrElse Book.Id = 0 Then
                    Throw New ArgumentNullException("Book")
                End If

                Book.Name = updateBook.Name
                Book.Description = updateBook.Description
            Else
                Dim seq As GeneralLedgerSequenceDetail = Nothing
                If Book.Code Is Nothing OrElse Book.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Book.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If
            End If


            Dim auxBook As LegalBook = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of LegalBook)
            Dim status As Integer

            If Book.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Book.CreationUser = audit.CodeUser
                Book.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxBook = Book.OriginalValue
                Book.ModificationUser = audit.CodeUser
                Book.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._bookRepository.SaveEntity(Book)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of LegalBook)(Book, audit, status, auxBook)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            Book.MarkAsUnchanged()

            Return New ActionResult(Of LegalBook) With {.StateResult = True, .ObjectEmbbeded = Book}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LegalBook) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Valida si ya hay libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateOfficialBook() As ActionResult(Of LegalBook) Implements IBookAdminService.ValidateOfficialBook
        Try
            Dim LegalBook = Me._bookRepository.ValidateOfficialBook()
            Return New ActionResult(Of LegalBook) With {.StateResult = True, .ObjectEmbbeded = LegalBook}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LegalBook) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para ejecutar el cierre contable por libro
    ''' </summary>
    ''' <param name="legalBookId"></param>
    ''' <param name="year"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="nitCompany"></param>
    ''' <param name="user"></param>
    ''' <returns></returns>
    Public Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IBookAdminService.FiscalYearClose
        Dim resultReturn As ActionResult(Of List(Of Tuple(Of String, Integer))) = Nothing

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_bookRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _bookRepository.FiscalYearClose(legalBookId, year, operativeUnitId, nitCompany, user)
                Dim resultList = result.ToList()

                'valido que los debitos y creditos esten bien
                If resultList.FindAll(Function(x) x.CodeMessage = 999).Count > 0 Then
                    Dim listErrors As New List(Of Tuple(Of String, Integer))
                    For Each item In resultList
                        listErrors.Add(New Tuple(Of String, Integer)(item.Message, 2))
                    Next
                    scope.Dispose()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .Message = resultList(0).Message, .ObjectEmbbeded = listErrors}
                Else
                    scope.Complete()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.SUCCESS, .Message = resultList(0).Message}
                End If

            Catch ex As Exception
                scope.Dispose()
                resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using

        Return resultReturn
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._bookRepository = Nothing
            Me._secuenseDRepository = Nothing
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
