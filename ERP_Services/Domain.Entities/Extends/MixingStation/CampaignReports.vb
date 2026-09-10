Imports System.Runtime.Serialization
Imports System.Text

Partial Public Class CampaignReports

    ''' <summary>
    ''' Obtiene una lista de InventoryRequest
    ''' </summary>
    <DataMember()>
    Public Property InventoryRequest As InventoryRequest

    ''' <summary>
    ''' Obtiene o establece la descripcion del paquete asociado
    ''' </summary>
    <DataMember()>
    Public Property TransferOrder As TransferOrder
End Class
