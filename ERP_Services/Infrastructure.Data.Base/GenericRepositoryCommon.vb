'***********************************************************************
' Assembly         : Infraestructura.Datos.Base
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : OscarSierra
' Last Modified On : 03-08-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Globalization
Imports Domain.Base
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports System.Linq.Expressions
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Threading.Tasks

#End Region

''' <summary>
'''Este repositorio genérico es una implementación predeterminada.
'''sus repositorios específicos puede heredar de esta clase base para que obtenga automáticamente la aplicación por defecto.
'''IMPORTANTE: El uso de esta clase Base Infraestructura.Base  no es obligatoria. Es sólo una clase base útil:
'''También se podría decidir que no desea utilizar esta clase Base, porque a veces usted no desea un
'''repositorio específico para  conseguir todas estas características y que podría ser malo para un repositorio específico.
''' </summary>
''' <typeparam name="TEntity">El tipo de la entidad.</typeparam>
Public Class GenericRepositoryCommon(Of TEntity As {Class, Domain.Base.Entities.IObjectWithChangeTracker, New})
    Inherits BaseRepository(Of TEntity)
    Implements IRepository(Of TEntity)

#Region "Fields"

    'Devuelve La unidad de trabajo en este repositorio Generico
    Private _context As IQueryableContextCommon

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor por Defecto Para Repositorio Generico
    ''' </summary>
    ''' <param name="context">Un Contexto para el Repositorio</param>
    Public Sub New(ByVal context As IQueryableContextCommon)
        MyBase.New(context)
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If
        _context = context
    End Sub

#End Region

#Region "IRepositorio<TEntity> Members"

    ''' <summary>
    ''' Guarda los cambios de un objeto en el contexto ya sea agregado o modificado
    ''' </summary>
    ''' <param name="item">Objeto que se desea guardar</param>
    ''' <remarks></remarks>
    Public Sub SaveEntity(item As TEntity) Implements IRepository(Of TEntity).SaveEntity
        If item IsNot Nothing Then
            _context.SaveChangesEntity(item)
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve para adicionar un objeto al contexto.	
    ''' </summary>
    ''' <param name="item">Item</param>
    ''' <remarks></remarks>
    <Obsolete("Este metodo es obsoleto, por favor use la funcion SaveEntity")>
    Public Overridable Sub AddEntity(ByVal item As TEntity) Implements Domain.Base.IRepository(Of TEntity).AddEntity
        If item IsNot Nothing Then
            'añadir el objeto a IObjectSet para este tipo
            _context.CreateEntityObjectSet(Of TEntity)().Add(item)

            If item.ChangeTracker.ChangeTrackingEnabled = True Then
                item.StopTracking()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve Atachar nuestros objetos al contexto y permitir que de esta manera viajen por WCF .	
    ''' </summary>
    ''' <param name="item">El item.</param>
    ''' <remarks></remarks>
    Public Sub AttachEntity(ByVal item As TEntity) Implements Domain.Base.IRepository(Of TEntity).AttachEntity
        If item IsNot Nothing Then
            _context.GetObjectSet(Of TEntity)().Attach(item)
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve para eliminar los obejtos que persisten en nuestro contexto.	
    ''' </summary>
    ''' <param name="item">El item.</param>
    ''' <remarks></remarks>
    Public Sub DeleteEntity(ByVal item As TEntity) Implements Domain.Base.IRepository(Of TEntity).DeleteEntity
        If item IsNot Nothing Then
            'Dim objectSet As IObjectSet(Of TEntity) = (_context.CreateEntityObjectSet(Of TEntity)())
            ''Adjuntar objeto al contexto y eliminar este
            ''esto es válido sólo si T es un tipo de modelo
            'objectSet.Attach(item)
            ''eliminar objeto IObjectSet para este tipo
            'objectSet.DeleteObject(item)
            'item.MarkAsAdded()
            Dim objectSet As DbSet(Of TEntity) = _context.GetObjectSet(Of TEntity)()
            objectSet.Attach(item)
            objectSet.Remove(item)
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve para modificar los objetos que persisten en nuestro contexto.	
    ''' </summary>
    ''' <param name="item">The item.</param>
    ''' <remarks></remarks>
    <Obsolete("Este metodo es obsoleto, por favor use la funcion SaveEntity")>
    Public Sub UpdateEntity(ByVal item As TEntity) Implements Domain.Base.IRepository(Of TEntity).UpdateEntity
        If item IsNot Nothing Then
            'Establecer Estado modified en Habilitado
            If item.ChangeTracker IsNot Nothing Then
                item.MarkAsModified()
            End If
            'aplicar los cambios en el Objeto
            _context.SaveChangesEntity(item)
        End If
    End Sub

    ''' <summary>
    ''' Devuelve una instancia de Nuestro contexto.	
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property UnitWork As Domain.Base.IUnitWork Implements Domain.Base.IRepository(Of TEntity).UnitWork
        Get
            Return TryCast(_context, IContext)
        End Get
    End Property

    ''' <summary>
    ''' Obteners the todos.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll() As System.Collections.Generic.IEnumerable(Of TEntity) Implements Domain.Base.IRepository(Of TEntity).GetAll
        'Crear IObjectSet y realizar consultas
        Return (_context.CreateEntityObjectSet(Of TEntity)().AsEnumerable)
    End Function

    ''' <summary>
    ''' elimina un item
    ''' no se elimina fisicamente de la base de datos sino que coloca su campo EstadoEliminado=True
    ''' </summary>
    ''' <param name="item">Item a eliminar, cambia el EstadoEliminado=True</param>
    Public Sub DeleteVirtual(ByVal item As TEntity) Implements Domain.Base.IRepository(Of TEntity).DeleteVirtual
        If item IsNot Nothing Then
            'Establecer Estado modified en Habilitado
            If item.ChangeTracker IsNot Nothing Then
                item.MarkAsModified()
            End If
            'aplicar los cambios en el Objeto
            _context.SaveChangesEntity(item)
        End If
    End Sub

    ''' <summary>
    ''' Obtene todo los tipos {T} entidad en el repositorio, mediante un filtro lambda expression.
    ''' </summary>
    ''' <param name="filter">la espression lambda. http://msdn.microsoft.com/es-es/library/bb531253.aspx</param>
    ''' <returns></returns>
    Public Function GetByFilter(ByVal filter As Expression(Of Func(Of TEntity, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of TEntity) Implements Domain.Base.IRepository(Of TEntity).GetByFilter
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
    ''' Este metodo sirve para eliminar los objetos que persisten en nuestro contexto.
    ''' </summary>
    ''' <param name="item"></param>
    Public Sub DeleteList(item As List(Of TEntity)) Implements IRepository(Of TEntity).DeleteList
        If item IsNot Nothing Then
            Dim objectSet As DbSet(Of TEntity) = _context.GetObjectSet(Of TEntity)()
            objectSet.BulkDelete(item.AsEnumerable)
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve para insert, Update y Delete masivo.	
    ''' </summary>
    ''' <param name="item">El item.</param>
    ''' <remarks></remarks>
    Public Async Function SaveEntityMassiveAsync(ByVal item As List(Of TEntity)) As Task Implements Domain.Base.IRepository(Of TEntity).SaveEntityMassiveAsync
        If item IsNot Nothing Then
            Dim objectSet As DbSet(Of TEntity) = _context.GetObjectSet(Of TEntity)()

            If item.Any(Function(d) d.ChangeTracker.State = ObjectState.Added) Then
                Dim toAdd = item.FindAll(Function(d) d.ChangeTracker.State = ObjectState.Added)
                Await objectSet.BulkInsertAsync(toAdd.AsEnumerable)
            End If

            If item.Any(Function(d) d.ChangeTracker.State = ObjectState.Modified) Then
                Dim toUpdate = item.FindAll(Function(d) d.ChangeTracker.State = ObjectState.Modified)
                Await objectSet.BulkUpdateAsync(toUpdate.AsEnumerable)
            End If

            If item.Any(Function(d) d.ChangeTracker.State = ObjectState.Deleted) Then
                Dim toDelete = item.FindAll(Function(d) d.ChangeTracker.State = ObjectState.Deleted)
                Await objectSet.BulkDeleteAsync(toDelete.AsEnumerable)
            End If
        End If
    End Function

#End Region

End Class
