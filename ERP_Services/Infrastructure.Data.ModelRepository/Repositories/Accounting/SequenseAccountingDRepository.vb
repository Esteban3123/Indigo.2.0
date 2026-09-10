'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Configuration
Imports System.Data.SqlClient
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base
Imports System.Threading.Tasks
Imports System.Text

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class SequenseAccountingDRepository
    Inherits GenericRepository(Of GeneralLedgerSequenceDetail)
    Implements ISequenseAccountingDRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "ISequenseAccountingRepository"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As GeneralLedgerSequenceDetail Implements ISequenseAccountingDRepository.GetSequenseDById
        Dim result = (From s As GeneralLedgerSequenceDetail In Me._context.GeneralLedgerSequenceDetail.Include("GeneralLedgerSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New GeneralLedgerSequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica primero haciendo el update para bloquear la tabla y no hayan problemas de concurrencia
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSequenseDetailUpdatedById(id As Integer) As GeneralLedgerSequenceDetail Implements ISequenseAccountingDRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE [GeneralLedger].[GeneralLedgerSequenceDetail] SET Next = Next + 1 WHERE Id = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As GeneralLedgerSequenceDetail In Me._context.GeneralLedgerSequenceDetail.AsNoTracking().Include("GeneralLedgerSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New GeneralLedgerSequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Crea 1 secuencia para JournalVoucherTypeConsecutive por año y libro especifico
    ''' </summary>
    ''' <param name="documentType">Tipo de documento</param>
    Public Async Function CreateSequenceForDocumentTypeAsync(documentType As JournalVoucherTypeConsecutive) As Task Implements ISequenseAccountingDRepository.CreateSequenceForDocumentTypeAsync
        If documentType Is Nothing Then Exit Function

        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", ServerSessionValues.Current.CurrentContainer)
        Using conn As New SqlConnection(connectionString)
            Await conn.OpenAsync()
            Dim sequenceNameCurrent As String = $"Seq_JV_T{documentType.JournalVoucherTypeId}_L{documentType.LegalBookId}_Y{documentType.Year}"

            ' Chequea si existe la secuencia para el año actual
            Dim checkSqlCurrent As String = $"SELECT 1 FROM sys.sequences s JOIN sys.schemas sc ON sc.schema_id = s.schema_id WHERE sc.name = 'GeneralLedger' AND s.name = '{sequenceNameCurrent}'"
            Dim currentExists As Boolean = False
            Using checkCmd As New SqlCommand(checkSqlCurrent, conn)
                Dim result = Await checkCmd.ExecuteScalarAsync()
                currentExists = (result IsNot Nothing)
            End Using

            Dim createSql As String = ""
            If Not currentExists Then
                createSql &= $"EXEC('CREATE SEQUENCE GeneralLedger.{sequenceNameCurrent} AS bigint START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE');"
            End If
            If Not String.IsNullOrWhiteSpace(createSql) Then
                Using createCmd As New SqlCommand(createSql, conn)
                    Await createCmd.ExecuteNonQueryAsync()
                End Using
            End If
        End Using
    End Function

    ''' <summary>
    ''' Crea secuencias para JournalVoucherTypes para año actual de creación y siguiente
    ''' </summary>
    ''' <param name="documentType">Tipo de documento</param>
    Public Async Function CreateSequencesForNewDocumentTypeAsync(documentType As JournalVoucherTypes) As Task Implements ISequenseAccountingDRepository.CreateSequencesForNewDocumentTypeAsync
        If documentType Is Nothing Then Exit Function

        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", ServerSessionValues.Current.CurrentContainer)
        Using conn As New SqlConnection(connectionString)
            Await conn.OpenAsync()

            ' 1. Obtener todos los libros legales activos
            Dim legalBooks As New List(Of Integer)
            Dim getBooksSql As String = "SELECT Id FROM GeneralLedger.LegalBook WHERE Status = 1"
            Using getBooksCmd As New SqlCommand(getBooksSql, conn)
                Using reader = Await getBooksCmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        legalBooks.Add(reader.GetInt32(0))
                    End While
                End Using
            End Using

            Dim currentYear As Integer = DateTime.Now.Year
            Dim nextYear As Integer = currentYear + 1
            Dim sqlToCreate As New StringBuilder()

            For Each legalBookId In legalBooks
                For Each targetYear In {currentYear, nextYear}
                    Dim seqName As String = $"Seq_JV_T{documentType.Id}_L{legalBookId}_Y{targetYear}"
                    Dim checkSql As String = $"SELECT 1 FROM sys.sequences s JOIN sys.schemas sc ON sc.schema_id = s.schema_id WHERE sc.name = 'GeneralLedger' AND s.name = '{seqName}'"
                    Dim exists As Boolean = False
                    Using checkCmd As New SqlCommand(checkSql, conn)
                        Dim result = Await checkCmd.ExecuteScalarAsync()
                        exists = (result IsNot Nothing)
                    End Using
                    If Not exists Then
                        sqlToCreate.AppendLine($"EXEC('CREATE SEQUENCE GeneralLedger.{seqName} AS bigint START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE');")
                    End If
                Next
            Next

            If sqlToCreate.Length > 0 Then
                Using createCmd As New SqlCommand(sqlToCreate.ToString(), conn)
                    Await createCmd.ExecuteNonQueryAsync()
                End Using
            End If
        End Using
    End Function

    ''' <summary>
    ''' Consulta el valor actual de una secuencia en SQL Server sin modificarla de forma asincrónica.
    ''' </summary>
    ''' <param name="sequenceName">Nombre de la secuencia</param>
    ''' <returns>Valor actual de la secuencia</returns>
    Public Async Function GetCurrentSequenceValueAsync(sequenceName As String) As Task(Of Long) Implements ISequenseAccountingDRepository.GetCurrentSequenceValueAsync
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim sql As String = $"SELECT current_value FROM sys.sequences WHERE name = @sequenceName"
        Using conn As New SqlConnection(connectionString)
            Await conn.OpenAsync()
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@sequenceName", sequenceName)
                Dim result = Await cmd.ExecuteScalarAsync()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return Convert.ToInt64(result)
                Else
                    Return 0
                End If
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Elimina la secuencia para JournalVoucherTypeConsecutive por libro y año especifico
    ''' </summary>
    ''' <param name="documentType">Tipo de documento</param>
    Public Async Function DeleteSequenceForDocumentTypeAsync(documentType As JournalVoucherTypeConsecutive) As Task Implements ISequenseAccountingDRepository.DeleteSequenceForDocumentTypeAsync
        If documentType Is Nothing Then Exit Function
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Using conn As New SqlConnection(connectionString)
            Await conn.OpenAsync()
            Dim sequenceName As String = $"Seq_JV_T{documentType.JournalVoucherTypeId }_L{documentType.LegalBookId}_Y{documentType.Year}"
            Dim sql As String = $"IF EXISTS (SELECT 1 FROM sys.sequences s
               JOIN sys.schemas sc ON sc.schema_id = s.schema_id
               WHERE sc.name = 'GeneralLedger' AND s.name = '{sequenceName}')
                BEGIN
                    DROP SEQUENCE GeneralLedger.{sequenceName};
                END"
            Using cmd As New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Elimina todas las secuencuas para JournalVoucherTypes
    ''' </summary>
    ''' <param name="documentType">Tipo de documento</param>
    Public Async Function DeleteAllSequencesForDocumentTypeAsync(documentType As JournalVoucherTypes) As Task Implements ISequenseAccountingDRepository.DeleteAllSequencesForDocumentTypeAsync
        If documentType Is Nothing Then Exit Function

        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Using conn As New SqlConnection(connectionString)
            Await conn.OpenAsync()

            ' Buscar todos los nombres de secuencia para ese tipo de documento
            Dim searchSql As String = "
            SELECT sc.name AS SchemaName, s.name AS SequenceName
            FROM sys.sequences s
            JOIN sys.schemas sc ON sc.schema_id = s.schema_id
            WHERE sc.name = 'GeneralLedger' AND s.name LIKE @Pattern
        "
            Dim pattern As String = $"Seq_JV_T{documentType.Id}_L%_Y%"
            Dim names As New List(Of String)
            Using searchCmd As New SqlCommand(searchSql, conn)
                searchCmd.Parameters.AddWithValue("@Pattern", pattern)
                Using reader = Await searchCmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        names.Add($"[{reader.GetString(0)}].[{reader.GetString(1)}]")
                    End While
                End Using
            End Using

            If names.Any() Then
                ' Construir el SQL para eliminar todas las secuencias encontradas
                Dim dropSql As String = String.Join(";", names.Select(Function(n) $"DROP SEQUENCE {n}"))
                Using dropCmd As New SqlCommand(dropSql, conn)
                    Await dropCmd.ExecuteNonQueryAsync()
                End Using
            End If
        End Using
    End Function
#End Region

End Class
