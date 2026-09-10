'***********************************************************************
' Assembly         : Infraestructura.Datos.Base
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : Diego A. Roldán
' Last Modified On : 2021-09-21
' Description      : Se agregan métodos genéricos para consulta, FirstOrDefault, Count, Any, ...
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Generic
Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
'''Este repositorio genérico es una implementación predeterminada.
'''sus repositorios específicos puede heredar de esta clase base para que obtenga automáticamente la aplicación por defecto.
'''IMPORTANTE: El uso de esta clase Base Infraestructura.Base  no es obligatoria. Es sólo una clase base útil:
'''También se podría decidir que no desea utilizar esta clase Base, porque a veces usted no desea un
'''repositorio específico para  conseguir todas estas características y que podría ser malo para un repositorio específico.
''' </summary>
''' <typeparam name="TEntity">El tipo de la entidad.</typeparam>
Public Class GenericRepository(Of TEntity As {Class, Domain.Base.Entities.IObjectWithChangeTracker, New})
    Inherits BaseRepository(Of TEntity)
    Implements IRepository(Of TEntity)

#Region "Fields"

    'Devuelve La unidad de trabajo en este repositorio Generico
    Private _context As IQueryableContext

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor por Defecto Para Repositorio Generico
    ''' </summary>
    ''' <param name="context">Un Contexto para el Repositorio</param>
    Public Sub New(ByVal context As IQueryableContext)
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
    ''' Este metodo sirve para eliminar los objetos que persisten en nuestro contexto.	
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
            'objectSet.Remove(item)
            Dim col As New List(Of TEntity)
            col.Add(item)
            objectSet.BulkDelete(col.AsEnumerable)
        End If
    End Sub

    ''' <summary>
    ''' Este metodo sirve para eliminar los objetos que persisten en nuestro contexto.	
    ''' </summary>
    ''' <param name="item">El item.</param>
    ''' <remarks></remarks>
    Public Sub DeleteList(ByVal item As List(Of TEntity)) Implements Domain.Base.IRepository(Of TEntity).DeleteList
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
    Public Async Function SaveEntityMassiveAsync(item As List(Of TEntity)) As Threading.Tasks.Task Implements Domain.Base.IRepository(Of TEntity).SaveEntityMassiveAsync
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
    ''' Obteners the todos	
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
#End Region

End Class

''' <summary>
''' Punto de acceso acotado a BulkSaveChanges para contextos que requieren
''' persistir un grafo previamente registrado en Entity Framework.
''' </summary>
Public NotInheritable Class BulkUnitWorkPersistence
    Private Sub New()
    End Sub

    Public Shared Sub Commit(context As DbContext)
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If
        context.BulkSaveChanges()
    End Sub
End Class
