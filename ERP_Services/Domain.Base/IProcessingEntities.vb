'***********************************************************************
' Assembly         : Domain.Base
' Author           : WalterSierra
' Created          : 21-04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' interface con el metodo para recuperra los valores originales de una entidad {T}
''' </summary>
Public Interface IProcessingEntities

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntity">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <returns></returns>
    Function GetSourceValues(Of TEntity)(ByVal entity As TEntity) As TEntity

End Interface
