'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Andres Alarcon
' Created          : 03-05-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInventoryRequestDetailOtherRepository
    Inherits IRepository(Of InventoryRequestDetailOther)

    ''' <summary>
    ''' Obtiene un detalle de solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestDetailOtherById(id As Integer) As InventoryRequestDetailOther

End Interface
