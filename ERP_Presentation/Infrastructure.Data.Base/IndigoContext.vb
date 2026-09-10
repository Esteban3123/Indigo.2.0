'***********************************************************************
' Assembly         : Infrastructure.Data.Base
' Author           : WalterSierra
' Created          : 21-04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
#End Region

''' <summary>
''' clase para resolver los valores originales de la entidad
''' </summary>
Public NotInheritable Class IndigoContext

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntity">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <param name="context">El contexto del Modulo</param>
    ''' <returns></returns>
    Public Shared Function GetSourceValues(Of TEntity)(ByVal entity As TEntity, ByVal context As DbContext) As TEntity
        For Each propiedades In entity.GetType.GetProperties
            If propiedades.Name <> "ChangeTracker" Then
                Try
                    entity.GetType.GetProperty(propiedades.Name).SetValue(entity, CType(context, IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity).OriginalValues(propiedades.Name), Nothing)
                Catch ex As Exception
                    Throw
                End Try
            End If
        Next
        Return entity
    End Function

End Class
