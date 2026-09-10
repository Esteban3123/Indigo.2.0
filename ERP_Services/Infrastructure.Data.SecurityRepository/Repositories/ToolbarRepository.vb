#Region "Imports"
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.Data.Base
Imports System.Data.Entity

#End Region

''' <summary>
''' 	
''' </summary>
Public Class ToolbarRepository
    Implements IToolbarRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="ToolbarRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        _context = contex
    End Sub

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntity">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <returns></returns>
    Public Function DevolverValoresOriginales(Of TEntity)(ByVal entity As TEntity) As TEntity
        Return IndigoContext.GetSourceValues(entity, CType(_context, DbContext))
    End Function

End Class
