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

#Region "Imports"
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Threading.Tasks
#End Region

Public Interface Inject

End Interface

''' <summary>
''' Interface necesaria para la creacion de las interfaces
''' de los repositorios en la capa de dominio
''' tiene todos los metodos comunes de los repositorios
''' </summary>
''' <typeparam name="TEntity">The type of the entity.</typeparam>
Public Interface IRepository(Of TEntity As {Class, New})
    Inherits IBaseRepository(Of TEntity)

    ''' <summary>
    ''' Obtiene la Unidad de trabajo del Repositorio
    ''' </summary>
    ReadOnly Property UnitWork() As IUnitWork

    ''' <summary>
    ''' Guarda los cambios de un objeto ya sea agregado o modificado
    ''' </summary>
    ''' <param name="item">Objeto que se desea guardar</param>
    ''' <remarks></remarks>
    Sub SaveEntity(ByVal item As TEntity)

    ''' <summary>
    ''' Adiciona un Item al Repositorio
    ''' </summary>
    ''' <param name="item">Item a ser adicionado</param>
    <Obsolete("Este metodo es obsoleto, por favor use la funcion SaveEntity")>
    Sub AddEntity(ByVal item As TEntity)

    ''' <summary>
    ''' elimina un item
    ''' </summary>
    ''' <param name="item">Item a eliminar</param>
    Sub DeleteEntity(ByVal item As TEntity)

    ''' <summary>
    ''' elimina un item
    ''' </summary>
    ''' <param name="item">Item a eliminar</param>
    Sub DeleteList(ByVal item As List(Of TEntity))

    ''' <summary>
    ''' elimina un item
    ''' no se elimina fisicamente de la base de datos sino que coloca su campo EstadoEliminado=True
    ''' </summary>
    ''' <param name="item">Item a eliminar, cambia el EstadoEliminado=True</param>
    Sub DeleteVirtual(ByVal item As TEntity)

    ''' <summary>
    ''' Adjunta una entidad a este repositorio..
    ''' Adjuntar es similar a adicionar, pero con un estado interno sin confirmar
    ''' este objeto no es marcado como 'Adicionado, Modificado o Eliminado', aplica cambios
    ''' en la Unidad de Trabajo no enviar nada al storage
    ''' </summary>
    ''' <param name="item">Item a ser adjuntado</param>
    Sub AttachEntity(ByVal item As TEntity)

    ''' <summary>
    ''' Establece una entidad Modificada. 
    ''' Cuando se llame al metodo Commit() en la Unidad de trabajo
    ''' estos cambios seran guardados en el storage
    ''' <remarks>
    ''' Internamente este metodo siempre llama el repositorio
    ''' </remarks>
    ''' </summary>
    ''' <param name="item">Item a se modificado</param>
    <Obsolete("Este metodo es obsoleto, por favor use la funcion SaveEntity")>
    Sub UpdateEntity(ByVal item As TEntity)

    ''' <summary>
    ''' Obtiene todos los tipos {T} entidad en el repositorio
    ''' </summary>
    ''' <returns>Lista de elementos seleccionados</returns>
    Function GetAll() As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Guarda, actuliza o Elimina Masivamente segun como marque la entidad.
    ''' </summary>
    ''' <param name="item">Objeto que se desea guardar</param>
    ''' <remarks></remarks>
    Function SaveEntityMassiveAsync(ByVal item As List(Of TEntity)) As Task

End Interface
