'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 05-01-2015
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

Public Class AddProductContractEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListInventoryContractDetail As Domain.Entities.TrackableCollection(Of InventoryContractDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInventorycontractDetail As InventoryContractDetail

    ''' <summary>
    ''' Retorna un true si esta editando un item, false si no.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

End Class
