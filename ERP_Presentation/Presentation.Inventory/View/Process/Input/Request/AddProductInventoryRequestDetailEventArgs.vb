'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 05-05-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddProductInventoryRequestDetailEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListInventoryRequestDetail As List(Of InventoryRequestDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInventoryRequestDetail As InventoryRequestDetail

    ''' <summary>
    ''' Obtiene o establece si el item esta en modo edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

End Class
