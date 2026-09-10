'***********************************************************************
' Assembly         : Domain.Base
' Created By : Diego A. Roldán
' Created On : 2021-09-21
' Description      : Se agregan métodos genéricos para consulta, FirstOrDefault, Count, Any, ...
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Linq.Expressions
Imports Domain.Base

Public Class BaseRepository(Of TEntity As {Class, Domain.Base.Entities.IObjectWithChangeTracker, New})
    Implements IBaseRepository(Of TEntity)

    ''' <summary>
    ''' Context application
    ''' </summary>
    Private _context As IQueryableContext

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As IQueryableContext)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtene todo los tipos {T} entidad en el repositorio, mediante un filtro lambda expression.
    ''' </summary>
    ''' <param name="filter">la espression lambda. http://msdn.microsoft.com/es-es/library/bb531253.aspx </param>
    ''' <returns></returns>
    Public Function GetByFilter(filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of TEntity) Implements IBaseRepository(Of TEntity).GetByFilter
        'valido los argumentos
        If filter IsNot Nothing Then
            'Create IObjectSet and perform query
            Dim query = _context.GetObjectSet(Of TEntity).AsQueryable()

            If includes IsNot Nothing Then
                For Each include In includes
                    If Not tracking Then
                        query = query.Include(include).AsNoTracking()
                    Else
                        query = query.Include(include)
                    End If
                Next
            End If

            If Not tracking Then
                query = query.AsNoTracking()
            End If


            Return query.Where(filter).ToList()
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' Consula el primer registro que se encuentre por el filtro
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="tracking"></param>
    ''' <param name="includes"></param>
    ''' <returns></returns>
    Public Function FirstOrDefault(filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As TEntity Implements IBaseRepository(Of TEntity).FirstOrDefault
        Dim query = _context.GetObjectSet(Of TEntity).AsQueryable()

        If includes IsNot Nothing Then
            For Each include In includes
                If Not tracking Then
                    query = query.Include(include).AsNoTracking()
                Else
                    query = query.Include(include)
                End If
            Next
        End If

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Return query.Where(filter).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Verifica que exista al menos un registro con el filtro indicado
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Function Any(filter As Expression(Of Func(Of TEntity, Boolean))) As Boolean Implements IBaseRepository(Of TEntity).Any
        Dim query = _context.GetObjectSet(Of TEntity).AsQueryable()
        Return query.Any(filter)
    End Function
    ''' <summary>
    ''' Realiza un conteo según el filtro
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Function Count(filter As Expression(Of Func(Of TEntity, Boolean))) As Integer Implements IBaseRepository(Of TEntity).Count
        Dim query = _context.GetObjectSet(Of TEntity).AsQueryable()
        Return query.Count(filter)
    End Function
    ''' <summary>
    ''' Busca un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function FindById(id As Integer) As TEntity Implements IBaseRepository(Of TEntity).FindById
        Dim query As DbSet(Of TEntity) = _context.GetObjectSet(Of TEntity)

        'If Not tracking Then
        '    query = query.AsNoTracking()
        'End If

        'If includes IsNot Nothing Then
        '    For Each include In includes
        '        query = query.Include(include)
        '    Next
        'End If

        Return query.Find(id)
    End Function
    ''' <summary>
    ''' Genera un IQueriable
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="tracking"></param>
    ''' <param name="includes"></param>
    ''' <returns></returns>
    Public Function Query(filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IQueryable(Of TEntity) Implements IBaseRepository(Of TEntity).Query
        Dim quer = _context.GetObjectSet(Of TEntity).AsQueryable()

        If includes IsNot Nothing Then
            For Each include In includes
                quer = quer.Include(include)
            Next
        End If

        If Not tracking Then
            quer = quer.AsNoTracking()
        End If

        Return quer.Where(filter)
    End Function

    ''' <summary>
    ''' Ejecuta un query
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Public Function ExecuteNonQuery(command As String, ParamArray params As Object()) As Integer Implements IBaseRepository(Of TEntity).ExecuteNonQuery
        CType(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.ExecuteNonQuery(command, params)
    End Function

    ''' <summary>
    ''' Ejecuta un query
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    <Obsolete("This method is deprecated due to the strategy of ride off EF, please use ExecuteQuery DR or Execute Stored Procedure")>
    Public Function ExecuteQuery(Of T)(command As String, ParamArray params As Object()) As IEnumerable(Of T) Implements IBaseRepository(Of TEntity).ExecuteQuery
        CType(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.ExecuteQuery(Of T)(command, params)
    End Function

    ''' <summary>
    ''' Ejecuta un stored procedure
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sp_name"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ExecuteStoredProcedure(Of T)(sp_name As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository(Of TEntity).ExecuteStoredProcedure
        Return ExecuteAndWrapQuery(Of T)(sp_name, parameters, CommandType.StoredProcedure)
    End Function

    Public Function ExecuteQueryDR(Of T)(query As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository(Of TEntity).ExecuteQueryDR
        Return ExecuteAndWrapQuery(Of T)(query, parameters, CommandType.Text)
    End Function

    Private Function ExecuteAndWrapQuery(Of T)(query As String, parameters As IEnumerable(Of (String, Object)), commandType As CommandType) As IEnumerable(Of T)
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "" _
                                 , TryCast(_context, DbContext).Database.Connection.Database)
        Using connection As New SqlConnection(conx)
            Dim command As New SqlCommand(query)
            command.Connection = connection
            command.CommandType = commandType
            command.CommandTimeout = 3600

            If parameters IsNot Nothing Then
                For Each p In parameters
                    command.Parameters.Add(New SqlParameter(p.Item1, If(p.Item2, DBNull.Value)))
                Next
            End If

            connection.Open()
            Dim reader = command.ExecuteReader()
            Dim type = GetType(T)
            Dim result As New List(Of T)()

            While reader.Read()
                Dim model = Activator.CreateInstance(Of T)()

                For Each p In type.GetProperties()
                    Try
                        Dim tipo As Type = If(Nullable.GetUnderlyingType(p.PropertyType), p.PropertyType)
                        Dim value = reader(p.Name)

                        If value IsNot DBNull.Value Then
                            p.SetValue(model, Convert.ChangeType(value, tipo), Nothing)
                        End If
                    Catch ex As Exception
                    End Try
                Next

                result.Add(model)
            End While

            Return result
        End Using
    End Function

    ''' <summary>
    ''' Execute query to datatable
    ''' </summary>
    ''' <param name="sqlQuery"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ExecuteQueryDt(sqlQuery As String, tablename As String, parameters As IEnumerable(Of (String, Object))) As DataTable Implements IBaseRepository(Of TEntity).ExecuteQueryDt
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "" _
                                 , TryCast(_context, DbContext).Database.Connection.Database)
        Using connection As New SqlConnection(conx)
            connection.Open()
            Dim command As New SqlCommand(sqlQuery)
            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandTimeout = 3600

            If parameters IsNot Nothing Then
                For Each p In parameters
                    command.Parameters.Add(New SqlParameter(p.Item1, p.Item2))
                Next
            End If

            Dim reader = command.ExecuteReader()
            Dim tb As New DataTable(tablename)
            tb.Load(reader)

            Return tb
        End Using
    End Function
End Class

Public Class BaseRepository
    Implements IBaseRepository

    ''' <summary>
    ''' Context application
    ''' </summary>
    Private _context As IQueryableContext

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As IQueryableContext)
        _context = context
    End Sub
    Public Function ExecuteNonQuery(command As String, ParamArray params As Object()) As Integer Implements IBaseRepository.ExecuteNonQuery
        CType(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.ExecuteNonQuery(command, params)
    End Function

    Public Function ExecuteStoredProcedure(Of T)(sp_name As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository.ExecuteStoredProcedure
        Return ExecuteAndWrapQuery(Of T)(sp_name, parameters, CommandType.StoredProcedure)
    End Function

    Public Function ExecuteQueryDR(Of T)(query As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository.ExecuteQueryDR
        Return ExecuteAndWrapQuery(Of T)(query, parameters, CommandType.Text)
    End Function

    Private Function ExecuteAndWrapQuery(Of T)(query As String, parameters As IEnumerable(Of (String, Object)), commandType As CommandType) As IEnumerable(Of T)
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "" _
                                 , TryCast(_context, DbContext).Database.Connection.Database)
        Using connection As New SqlConnection(conx)
            connection.Open()
            Dim command As New SqlCommand(query)
            command.Connection = connection
            command.CommandType = commandType
            command.CommandTimeout = 3600

            For Each p In parameters
                command.Parameters.Add(New SqlParameter(p.Item1, p.Item2))
            Next

            Dim reader = command.ExecuteReader()
            Dim columns = Enumerable.Range(0, reader.FieldCount).Select(Of String)(Function(x) reader.GetName(x)).ToList()
            Dim type = GetType(T)
            Dim result As New List(Of T)()

            While reader.Read()
                Dim model = Activator.CreateInstance(Of T)()

                For Each p In type.GetProperties()
                    If Not columns.Contains(p.Name) Then
                        Continue For
                    End If


                    Dim value = reader(p.Name)
                    If value.GetType = GetType(System.DBNull) Then
                        value = Nothing
                    End If

                    If p.PropertyType = GetType(String) Then
                        p.SetValue(model, value?.ToString())
                    ElseIf p.PropertyType = GetType(Boolean) Then
                        p.SetValue(model, Convert.ToBoolean(value))
                    ElseIf p.PropertyType = GetType(Byte) Then
                        p.SetValue(model, Convert.ToByte(value))
                    Else
                        p.SetValue(model, value)
                    End If
                Next

                result.Add(model)
            End While

            Return result
        End Using
    End Function
End Class