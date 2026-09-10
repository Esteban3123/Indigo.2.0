'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Domain.DocumentalSystem
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.DocumentalSystem.Entities
Imports Application.Base
Imports System.Data.SqlTypes
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Data.SqlClient
Imports System.Configuration
Imports Infrastructure.CrossCutting.Security

#End Region

''' <summary>
''' Servicio documentos.
''' </summary>
''' <remarks></remarks>
Public Class DocumentalSystemAdminService
    Implements IDocumentalSystemAdminService, IDisposable

    Private _DocumentsNewsRepository As IDocumentsStoreRepository
    Private _readCommittedTransactionOptions As New TransactionOptions With {.IsolationLevel = Transactions.IsolationLevel.ReadCommitted, .Timeout = TransactionManager.MaximumTimeout}
    Private _downloadCommittableTransaction As CommittableTransaction
    Private _downloadSqlFileStream As SqlFileStream

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function LogonUser(lpszUsername As String, lpszDomain As String, lpszPassword As String, dwLogonType As Integer, dwLogonProvider As Integer, ByRef phToken As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function CloseHandle(token As IntPtr) As Boolean
    End Function

    Private Enum LogonType
        Interactive = 2
        Network = 3
        Batch = 4
        Service = 5
        Unlock = 7
        NetworkClearText = 8
        NewCredentials = 9
    End Enum

    Private Enum LogonProvider
        [Default] = 0
        WinNT35 = 1
        WinNT40 = 2
        WinNT50 = 3
    End Enum

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="DocumentalSystemAdminService" />.
    ''' </summary>
    ''' <param name="DocumentsNewsRepository">el repositorio para el manejo de los documentos.</param>
    Public Sub New(ByVal DocumentsNewsRepository As IDocumentsStoreRepository)
        If DocumentsNewsRepository Is Nothing Then
            Throw New ArgumentNullException("DocumentsNewsRepository Vacio")
        End If
        _DocumentsNewsRepository = DocumentsNewsRepository
    End Sub
    ''' <summary>
    ''' Obtener un documento.
    ''' </summary>
    ''' <param name="Id">Código Documento</param>
    ''' <returns>Objeto Documento</returns>
    Public Function GetDocument(Id As String, Container As String) As System.IO.Stream Implements IDocumentalSystemAdminService.GetDocument
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Código vacío")
        End If
        Try
            Dim user = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("UserImpersonation"))
            Dim pwd = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("PwdImpersonation"))
            Dim domain = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("DomainImpersonation"))
            Dim token As IntPtr = IntPtr.Zero
            Dim valid As Boolean = LogonUser(user, domain, pwd, CInt(LogonType.NewCredentials), CInt(LogonProvider.WinNT50), token)
            If valid Then
                Using context As WindowsImpersonationContext = WindowsIdentity.Impersonate(token)
                    _downloadCommittableTransaction = New Transactions.CommittableTransaction(_readCommittedTransactionOptions)
                    Dim fsc As FileStreamContext = _DocumentsNewsRepository.GetDocumentContext(Id, _downloadCommittableTransaction, Container)
                    If fsc IsNot Nothing Then
                        _downloadSqlFileStream = New SqlFileStream(fsc.InternalPath, fsc.TransactionContext, System.IO.FileAccess.Read)
                    End If
                    CloseHandle(token)
                    context.Undo()
                End Using
            End If
            Return _downloadSqlFileStream
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteDocument(document As DocumentsStore) As ActionResult Implements IDocumentalSystemAdminService.DeleteDocument
        Try
            Using tran As New TransactionScope(TransactionScopeOption.RequiresNew, _readCommittedTransactionOptions)

                _DocumentsNewsRepository.DeleteEntity(document)
                _DocumentsNewsRepository.UnitWork.Commit()

                tran.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista todas los documentos.
    ''' </summary>
    ''' <returns>Lista de documentos.</returns>
    Public Function ListAllDocuments() As List(Of DocumentsStore) Implements IDocumentalSystemAdminService.ListAllDocuments
        Try
            Return _DocumentsNewsRepository.ListAllDocuments
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Function GetDocumentById(Id As String) As DocumentsStore Implements IDocumentalSystemAdminService.GetDocumentById
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _DocumentsNewsRepository.GetDocumentById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Buscar documentos por fullText
    ''' </summary>
    ''' <param name="searchWords"></param>
    ''' <returns>Lista de documentos</returns>
    Function GetDocumentFullText(searchWords As String, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore) Implements IDocumentalSystemAdminService.GetDocumentFullText
        Try
            Return _DocumentsNewsRepository.GetDocumentFullText(searchWords, inMenu, skip, top)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    ''' <param name="document">DocumentInfo</param>
    Function SaveDocument(Document As DocumentInfo) As ResponseDocument Implements IDocumentalSystemAdminService.SaveDocument
        Dim unitOfWork As IUnitWork = _DocumentsNewsRepository.UnitWork
        Dim doc As DocumentsStore = Nothing
        Try
            Dim user = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("UserImpersonation"))
            Dim pwd = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("PwdImpersonation"))
            Dim domain = IndigoRijndael.Decrypt(ConfigurationManager.AppSettings("DomainImpersonation"))
            Dim token As IntPtr = IntPtr.Zero
            Dim valid As Boolean = LogonUser(user, domain, pwd, CInt(LogonType.NewCredentials), CInt(LogonProvider.WinNT50), token)
            If valid Then
                Using context As WindowsImpersonationContext = WindowsIdentity.Impersonate(token)
                    Using tran As New TransactionScope(TransactionScopeOption.RequiresNew, _readCommittedTransactionOptions)
                        Dim Id = Guid.NewGuid
                        Dim emptyStream = New Byte() {}
                        doc = New DocumentsStore With {.Content = emptyStream, .Name = Document.FileName, .UseFormMetadata = Document.UseFormMetaData, .MetaData = Document.MetaData, .IdForm = Document.IdForm, .Id = Id, .IdEntity = Document.IdEntity, .IdFileContainer = Document.IdFileContainer, .AttachDate = Document.AttachDate, .Type = IO.Path.GetExtension(Document.FileName)}
                        _DocumentsNewsRepository.SaveEntity(doc)
                        unitOfWork.Commit()
                        Dim fsc As FileStreamContext = _DocumentsNewsRepository.GetFileStream(doc.Id.ToString, Document.SessionInf.Container)
                        Using sqlFileStream = New SqlFileStream(fsc.InternalPath, fsc.TransactionContext, IO.FileAccess.Write)
                            Document.DocumentStream.CopyTo(sqlFileStream)
                            tran.Complete()
                        End Using
                    End Using
                    CloseHandle(token)
                    context.Undo()
                End Using
            End If
            Return New ResponseDocument With {.response = True, .documentStore = doc}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ResponseDocument With {.response = False, .message = ex.Message.ToString}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Function getDocumentByIdForm(IdForm As Integer, withTop As Boolean) As List(Of DocumentsStore) Implements IDocumentalSystemAdminService.getDocumentByIdForm
        If String.IsNullOrEmpty(IdForm) = True Then
            Throw New ArgumentNullException("IdForm vacío")
        End If
        Try
            Return _DocumentsNewsRepository.getDocumentByIdForm(IdForm, withTop)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function


    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean) As List(Of DocumentsStore) Implements IDocumentalSystemAdminService.ListDocumentByIdFormAndIdFileContainer
        If String.IsNullOrEmpty(IdForm) Then
            Throw New ArgumentNullException("IdForm vacío")
        End If
        If String.IsNullOrEmpty(IdForm) Then
            Throw New ArgumentNullException("IdFileContainer vacío")
        End If
        Try
            Return _DocumentsNewsRepository.ListDocumentByIdFormAndIdFileContainer(IdForm, IdFileContainer, TextSearch, FullText)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function


    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el registro
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdEntity">Id Entidad</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function getDocumentByIdFormAndIdEntity(IdForm As Integer, IdEntity As Integer, withTop As Boolean) As List(Of DocumentsStore) Implements IDocumentalSystemAdminService.getDocumentByIdFormAndIdEntity
        If String.IsNullOrEmpty(IdForm) = True Then
            Throw New ArgumentNullException("IdForm vacío")
        End If
        If String.IsNullOrEmpty(IdEntity) = True Then
            Throw New ArgumentNullException("IdEntity vacío")
        End If
        Try
            Return _DocumentsNewsRepository.getDocumentByIdFormAndIdEntity(IdForm, IdEntity, withTop)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _downloadSqlFileStream IsNot Nothing Then _downloadSqlFileStream.Dispose()
                If _downloadCommittableTransaction IsNot Nothing Then
                    If _downloadCommittableTransaction.TransactionInformation.Status = TransactionStatus.Active Then _downloadCommittableTransaction.Commit()
                    _downloadCommittableTransaction.Dispose()
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
