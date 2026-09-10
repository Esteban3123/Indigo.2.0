'***********************************************************************
' Assembly         : Domain.Base
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Interface con metodos necesarios para ejecuitar consultas o comando en el almacen de persistencia directamente
''' </summary>
Public Interface ISql
    ''' <summary>
    ''' Ejecuta una consulta en el almacen de persistencia
    ''' </summary>
    ''' <typeparam name="TEntity">Tipo de Entidad a mapear en el resutado</typeparam>
    ''' <param name="sqlQuery">
    ''' Dialecto de Consulta (SQL) 
    ''' <example>
    ''' SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
    ''' </example>
    ''' </param>
    ''' <param name="parameters">Un Vector con todos los valores de los parametros</param>
    ''' <returns>
    ''' Resultados Enumerados
    ''' </returns>
    Function ExecuteQuery(Of TEntity)(ByVal sqlQuery As String, ByVal ParamArray parameters As Object()) As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Ejecuta cualquier comando en el almacen de persistencia
    ''' </summary>
    ''' <param name="sqlCommand">
    ''' Comando a ejecutar
    ''' <example>
    ''' SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
    ''' </example>
    '''</param>
    ''' <param name="parameters">Un Vector con todos los valores de los parametros</param>
    ''' <returns>El numero de registros afectados</returns>
    Function ExecuteNonQuery(ByVal sqlCommand As String, ByVal ParamArray parameters As Object()) As Integer

End Interface
