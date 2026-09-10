'************************************************************
' Assembly         : Infraestructure.Data.DocumentalSystemRepository
' Author           : Juan Diego Diaz
' Created          : 09-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.DocumentalSystem.Entities
Imports Domain.DocumentalSystem
Imports Infrastructure.Data.Base
Imports System.Transactions
Imports System.Data.SqlTypes
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Configuration

#End Region

''' <summary>
''' Repositorio de Documentos
''' </summary>
Public Class DocumentsStoreRepository
    Inherits GenericRepository(Of DocumentsStore)
    Implements IDocumentsStoreRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IDocumentalSystemUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IDocumentalSystemUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener un documento según el codigo.
    ''' </summary>
    ''' <param name="Id">Codigo Documento</param>
    ''' <param name="Tran">Commitable Transaction</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Public Function GetDocument(Id As String, Tran As Object) As FileStreamContext Implements IDocumentsStoreRepository.GetDocument
        Try
            Dim transaction = TryCast(Tran, CommittableTransaction)
            _context.Connection.Open()
            _context.Connection.EnlistTransaction(transaction)
            Dim fileStream = _context.ExecuteQuery(Of FileStreamContext)("SELECT Content.PathName() AS InternalPath, GET_FILESTREAM_TRANSACTION_CONTEXT() as TransactionContext,Type as FileExtension FROM DocumentsStore WHERE Id={0}", Id).First
            Return fileStream

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Función para obtener un documento según el codigo.
    ''' </summary>
    ''' <param name="Id">Codigo Documento</param>
    ''' <param name="Tran">Commitable Transaction</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Public Function GetDocumentContext(Id As String, Tran As Object, Container As String) As FileStreamContext Implements IDocumentsStoreRepository.GetDocumentContext
        Try
            '_context.Connection.ConnectionString = String.Format(ConfigurationManager.ConnectionStrings("GENESISDOCUMENTALEntities").ToString, "GENESISDOCUMENTAL" & Company)
            '_context.Connection.Open()
            'Dim Transaction = TryCast(Tran, CommittableTransaction)
            '_context.Connection.EnlistTransaction(Transaction)
            'Dim fsc = _context.ExecuteQuery(Of FileStreamContext)("SELECT Content.PathName() AS InternalPath, GET_FILESTREAM_TRANSACTION_CONTEXT() AS TransactionContext FROM DocumentsStore WHERE Id='" & Id & "'").FirstOrDefault
            'Return fsc
            Dim Transaction = TryCast(Tran, CommittableTransaction)
            Dim ConStr As String = String.Format(ConfigurationManager.ConnectionStrings("ConnectionSqlFileStream").ToString, Container)
            Dim con As New SqlConnection(ConStr)
            con.Open()
            Dim sqlCommand As New SqlCommand()
            con.EnlistTransaction(Transaction)
            sqlCommand.Connection = con
            sqlCommand.CommandText = String.Format("SELECT Content.PathName() AS InternalPath, GET_FILESTREAM_TRANSACTION_CONTEXT() AS TransactionContext FROM DocumentsStore WHERE Id='" & Id & "'")
            Dim reader As SqlDataReader = sqlCommand.ExecuteReader
            Dim fsc As New FileStreamContext
            While reader.Read()
                fsc.InternalPath = reader(0).ToString()
                fsc.TransactionContext = CType(reader(1), Byte())
            End While
            con.Close()
            Return fsc
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para obtener todas los documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Public Function ListAllDocuments() As List(Of DocumentsStore) Implements IDocumentsStoreRepository.ListAllDocuments

        Dim Busqueda = From e In _context.DocumentsStore
                                      Select e
        Return Busqueda.ToList

    End Function

    ''' <summary>
    ''' Función para obtener todas los documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Public Function GetDocumentById(Id As String) As DocumentsStore Implements IDocumentsStoreRepository.GetDocumentById

        Dim guidCode = System.Guid.Parse(Id)
        Dim Busqueda = (From e In _context.DocumentsStore
                       Where e.Id = guidCode
                       Select e).SingleOrDefault

        Return Busqueda
    End Function


    Public Function GetDocumentFullText(searchWords As String, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore) Implements IDocumentsStoreRepository.GetDocumentFullText

        Dim documentInfos As List(Of DocumentsStore) = Nothing
        If searchWords.Trim <> String.Empty Then
            searchWords = Regex.Replace(searchWords, "\s+", " ").Trim
            Dim searchCondition = "'" & String.Join(" AND ", searchWords.Trim.Split(" "c)) & "'"
            documentInfos = _context.ExecuteQuery(Of DocumentsStore)("SELECT * FROM DocumentsStore WHERE CONTAINS(*, {0})", searchCondition).ToList
            If skip < 0 Then
                skip = 0
            End If
            If top < 0 Then
                top = 0
            End If
            Return documentInfos.Skip(skip).Take(top).ToList
        Else
            If inMenu Then
                documentInfos = (From d In _context.DocumentsStore Select d).OrderByDescending(Function(x) x.AttachDate).Take(5).ToList
            Else
                documentInfos = (From d In _context.DocumentsStore Select d).ToList
            End If
            Return documentInfos
        End If
    End Function

    ''' <summary>
    ''' Obtiene datos del fileStream
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns>Objeto FileStreamContext</returns>
    Public Function GetFileStream(Id As String, Container As String) As FileStreamContext Implements IDocumentsStoreRepository.GetFileStream
        '_context.Connection.ConnectionString = String.Format(ConfigurationManager.ConnectionStrings("GENESISDOCUMENTALEntities").ToString, "GENESISDOCUMENTAL" & Company)
        '_context.Connection.Open()
        'Dim fsc = _context.ExecuteQuery(Of FileStreamContext)("SELECT Content.PathName() AS InternalPath, GET_FILESTREAM_TRANSACTION_CONTEXT() AS TransactionContext FROM DocumentsStore WHERE Id='" & Id & "'").FirstOrDefault
        'Return fsc
        Dim ConStr As String = String.Format(ConfigurationManager.ConnectionStrings("ConnectionSqlFileStream").ToString, Container)
        Dim con As New SqlConnection(ConStr)
        con.Open()
        Dim sqlCommand As New SqlCommand()
        sqlCommand.Connection = con
        sqlCommand.CommandText = String.Format("SELECT Content.PathName() AS InternalPath, GET_FILESTREAM_TRANSACTION_CONTEXT() AS TransactionContext FROM DocumentsStore WHERE Id='" & Id & "'")
        Dim reader As SqlDataReader = sqlCommand.ExecuteReader
        Dim fsc As New FileStreamContext
        While reader.Read()
            fsc.InternalPath = reader(0).ToString()
            fsc.TransactionContext = CType(reader(1), Byte())
        End While
        con.Close()
        Return fsc

    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Lista DocumentsStore</returns>
    Public Function getDocumentByIdForm(IdForm As Integer, withTop As Boolean) As List(Of DocumentsStore) Implements IDocumentsStoreRepository.getDocumentByIdForm

        Dim Busqueda
        If withTop Then
            Busqueda = (From d In _context.DocumentsStore
                            Where d.IdForm = IdForm
                            Select d).OrderByDescending(Function(x) x.AttachDate).Take(5).ToList
        Else
            Busqueda = (From e In _context.DocumentsStore
                           Where e.IdForm = IdForm
                           Select e).ToList
        End If
        Return Busqueda

    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el registro
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdEntity">Id Entidad</param>
    ''' <returns>Lista DocumentsStore</returns>
    Public Function getDocumentByIdFormAndIdEntity(IdForm As Integer, IdEntity As Integer, withTop As Boolean) As List(Of DocumentsStore) Implements IDocumentsStoreRepository.getDocumentByIdFormAndIdEntity
        If withTop Then
            Dim result = (From a In _context.DocumentsStore.AsNoTracking
                          Where a.IdForm = IdForm AndAlso a.IdEntity = IdEntity
                          Order By a.AttachDate Descending
                          Take 5
                          Select New With
                          {
                              .Id = a.Id,
                              .Name = a.Name,
                              .Type = a.Type,
                              .AttachDate = a.AttachDate
                          }).ToList

            Return result.ToList.Select(Function(a) New DocumentsStore With
            {
                .Id = a.Id,
                .Name = a.Name,
                .Type = a.Type,
                .AttachDate = a.AttachDate
            }).ToList
        Else
            Dim result = (From a In _context.DocumentsStore.AsNoTracking
                          Where a.IdForm = IdForm AndAlso a.IdEntity = IdEntity
                          Order By a.AttachDate Descending
                          Select New With
                          {
                              .Id = a.Id,
                              .Name = a.Name,
                              .Type = a.Type,
                              .MetaData = a.MetaData,
                              .AttachDate = a.AttachDate
                          }).ToList

            Return result.ToList.Select(Function(a) New DocumentsStore With
            {
                .Id = a.Id,
                .Name = a.Name,
                .Type = a.Type,
                .MetaData = a.MetaData,
                .AttachDate = a.AttachDate
            }).ToList
        End If
    End Function

    ''' <summary>
    ''' Obtiene documentos según Id del formulario y el contenedor
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdFileContainer">Id Entidad</param>
    ''' <returns>Lista DocumentsStore</returns>
    Public Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean) As List(Of DocumentsStore) Implements IDocumentsStoreRepository.ListDocumentByIdFormAndIdFileContainer

        Dim documentInfos As List(Of DocumentsStore) = Nothing
        If FullText AndAlso TextSearch.Trim <> String.Empty Then
            TextSearch = Regex.Replace(TextSearch, "\s+", " ").Trim
            Dim searchCondition = "'" & String.Join(" AND ", TextSearch.Trim.Split(" "c)) & "'"
            Dim filter As String = String.Empty
            If IdFileContainer <> 0 Then
                filter = " AND IdFileContainer=" & IdFileContainer
            End If
            If IdForm <> 0 Then
                filter = filter & " AND IdForm=" & IdFileContainer
            End If
            documentInfos = _context.ExecuteQuery(Of DocumentsStore)("SELECT * FROM DocumentsStore WHERE CONTAINS(*, {0})" & filter, searchCondition).ToList
            Return documentInfos
        Else
            If TextSearch.Trim <> String.Empty Then
                documentInfos = (From d In _context.DocumentsStore
                                 Where d.MetaData = TextSearch.Trim OrElse d.Name = TextSearch.Trim
                                                    Select d).ToList
            Else
                documentInfos = (From d In _context.DocumentsStore
                                           Select d).ToList
            End If
         
            If IdFileContainer <> 0 Then
                documentInfos = documentInfos.Where(Function(x) x.IdFileContainer = IdFileContainer).ToList
            End If
            If IdForm <> 0 Then
                documentInfos = documentInfos.Where(Function(x) x.IdForm = IdForm).ToList
            End If
            Return documentInfos
        End If

    End Function


End Class
