Imports Domain.Base.Entities

''' <summary>
''' 	
''' </summary>
Public Interface IContext
    Inherits IUnitWork

    ''' <summary>
    ''' Aplicar los cambios realizados en las entidades
    ''' </summary>
    ''' <typeparam name="TEntity">Entidad</typeparam>
    ''' <param name="item">Item con Cambios</param>
    Sub SaveChangesEntity(Of TEntity As {Class, IObjectWithChangeTracker, New})(ByVal item As TEntity)

End Interface
