Imports System.Runtime.Serialization

Partial Public Class MaintenanceContract

#Region "Properties"

    ''' <summary>
    ''' Descripcion del proveedor
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Descripcion del tipo de contrato
    ''' </summary>
    <DataMember()>
    Public Property DescriptionContractType As String

#End Region

End Class
