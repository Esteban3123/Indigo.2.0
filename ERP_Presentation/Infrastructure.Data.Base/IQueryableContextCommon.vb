'***********************************************************************
' Assembly         : Infraestructura.Datos.Base
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity
#End Region

''' <summary>
'''Este es el contrato mínimo para todas las unidades de trabajo, una unidad de trabajo por módulo, que se extienden
'''El contrato de base IUnitOfWork con características de ADO EF. NET y STE.
'''El contrato de creación de esta base para añadir características de aislamiento de contrato específico para
'''pruebas.  Se  eliminaron las dependencias innecesarias.
''' </summary>
Public Interface IQueryableContextCommon
    Inherits IContext

    ''' <summary>
    ''' Crea los Objetos en el Contexto.	
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns>Conjunto de objetos del tipo {} TEntity </returns>
    ''' <remarks></remarks>
    Function CreateEntityObjectSet(Of TEntity As {Class, IObjectWithChangeTracker, New})() As IDbSet(Of TEntity)
    ''' <summary>
    ''' Devuelve el objectSet de la entidad que le enviemos como parametro
    ''' </summary>
    ''' <typeparam name="TEntity">Entidad</typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObjectSet(Of TEntity As {Class, IObjectWithChangeTracker, New})() As DbSet(Of TEntity)

End Interface
