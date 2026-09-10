'***********************************************************************
' Assembly         : Domain.Base
' Created By : Diego A. Roldán
' Created On : 2021-09-21
' Description      : Se agregan métodos genéricos para consulta, FirstOrDefault, Count, Any, ...
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.SqlClient
Imports System.Linq
Imports System.Linq.Expressions

Public Interface IBaseRepository(Of TEntity As {Class, New})
    ''' <summary>
    ''' Obtene todo los tipos {T} entidad en el repositorio, mediante un filtro lambda expression.
    ''' </summary>
    ''' <param name="filter">la espression lambda. http://msdn.microsoft.com/es-es/library/bb531253.aspx </param>
    ''' <returns></returns>
    Function GetByFilter(ByVal filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of TEntity)
    ''' <summary>
    ''' Consula el primer registro que se encuentre por el filtro
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="tracking"></param>
    ''' <param name="includes"></param>
    ''' <returns></returns>
    Function FirstOrDefault(ByVal filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As TEntity
    ''' <summary>
    ''' Verifica que exista al menos un registro con el filtro indicado
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Function Any(ByVal filter As Expression(Of Func(Of TEntity, Boolean))) As Boolean
    ''' <summary>
    ''' Realiza un conteo según el filtro
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Function Count(ByVal filter As Expression(Of Func(Of TEntity, Boolean))) As Integer
    ''' <summary>
    ''' Busca un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function FindById(id As Integer) As TEntity
    ''' <summary>
    ''' Genera un IQueriable
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="tracking"></param>
    ''' <param name="includes"></param>
    ''' <returns></returns>
    Function Query(filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IQueryable(Of TEntity)

    ''' <summary>
    ''' Ejecuta un stored procedure
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sp_name"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ExecuteStoredProcedure(Of T)(sp_name As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T)

    ''' <summary>
    ''' Ejecuta consulta
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Function ExecuteNonQuery(command As String, ParamArray params As Object()) As Integer

    ''' <summary>
    ''' Execute query
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sqlQuery"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ExecuteQuery(Of T)(ByVal sqlQuery As String, ByVal ParamArray parameters As Object()) As IEnumerable(Of T)

    ''' <summary>
    ''' Execute query to database
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ExecuteQueryDt(sqlQuery As String, tablename As String, parameters As IEnumerable(Of (String, Object))) As DataTable

    Function ExecuteQueryDR(Of T)(query As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T)
End Interface

Public Interface IBaseRepository

    ''' <summary>
    ''' Ejecuta un stored procedure
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sp_name"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ExecuteStoredProcedure(Of T)(sp_name As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T)

    ''' <summary>
    ''' Ejecuta consulta
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Function ExecuteNonQuery(command As String, ParamArray params As Object()) As Integer

    Function ExecuteQueryDR(Of T)(query As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T)
End Interface