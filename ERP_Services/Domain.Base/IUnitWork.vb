'***********************************************************************
' Assembly         : Domain.Base.Entities
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports System.Data
Imports System.Threading.Tasks
#End Region

''' <summary>
''' Interface con los metodos comunes para la unidad de trabajo
''' </summary>
Public Interface IUnitWork
    Inherits ISql, IDisposable

    ''' <summary>
    ''' Confirma todos los cambios realizados en el contenedor.
    ''' </summary>
    '''<remarks>
    ''' Si la entidad tiene propiedades fijas y el problema de concurrencia optimista existe 
    ''' entonces una excepción es lanzada
    '''</remarks>
    Sub Commit()

    ''' <summary>
    ''' Confirma todos los cambios realizados en el contenedor de forma asincrona.
    ''' </summary>
    '''<remarks>
    ''' Si la entidad tiene propiedades fijas y el problema de concurrencia optimista existe 
    ''' entonces una excepción es lanzada
    '''</remarks>
    Function CommitAsync() As Task(Of Integer)

    ''' <summary>
    ''' Confirma todos los cambios realizados en el contenedor.
    ''' </summary>
    '''<remarks>
    ''' Si la entidad tiene propiedades fijas y el problema de concurrencia optimista existe 
    ''' entonces los cambios en el cliente son refrescados
    '''</remarks>
    Sub CommitAndRefreshChanges()

    ''' <summary>
    ''' Desatacha del contexto la entidad deseada
    ''' </summary>
    ''' <param name="entity">The entity.</param>
    Sub Detach(entity As Object)

    ''' <summary>
    ''' Devuelve los cambios realizados hasta ese momento de todas las entidades sin importar el estado
    ''' </summary>
    Sub RollbackAllChanges()

    ''' <summary>
    ''' Devuelve los cambios realizados hasta ese momento 
    ''' desde que se inicio la unidad de trabajo
    ''' </summary>
    Sub RollbackChanges()

    ''' <summary>
    ''' Devuelve los cambios realizados hasta ese momento 
    ''' desde que se inicio la unidad de trabajo
    ''' </summary>
    Sub RollbackChangesUnitOfWork()

    ''' <summary>
    ''' Aplica los cambios realizados en el item o items relacionados en el modelo. 
    ''' Este Metodo es para especificar registerNew,registerDirty,registerDelete 
    ''' and registerClean en los trabajos de la unidad de trabajo
    ''' </summary>
    ''' <typeparam name="TEntity">Entidad</typeparam>
    ''' <param name="item">Item con Cambios</param>
    Sub RegisterChanges(Of TEntity As {Class, IObjectWithChangeTracker})(ByVal item As TEntity)

    ''' <summary>
    ''' Executes the query.	
    ''' </summary>
    ''' <param name="sqlQuery">The SQL query.</param>
    ''' <param name="nameTable">The name table.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function ExecuteQueryDataSet(sqlQuery As String, nameTable As String) As DataSet

End Interface