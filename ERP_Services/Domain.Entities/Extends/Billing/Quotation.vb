Imports System.Runtime.Serialization
Partial Public Class Quotation

    ''' <summary>
    ''' Listado de detalles de ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ListServiceOrderDetail As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Listado de detalles de dispensación farmaceutica
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ListPharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail)

    ''' <summary>
    ''' Obtiene el nit y el nombre del tercero
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ThirdPartyDescription As String

End Class
