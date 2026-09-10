'***********************************************************************
' Assembly         : Presentation.Inventory
' Author           : Mariana Gonzalez
' Created          : 19-11-2025
'
' Description      : Argumentos del evento para agregar detalles de traslado en consignacion
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddProductConsignmentTransfer
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListConsignmentTransferDetail As Domain.Entities.TrackableCollection(Of ConsignmentTransferDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemConsignmentTransferDetail As ConsignmentTransferDetail
End Class
