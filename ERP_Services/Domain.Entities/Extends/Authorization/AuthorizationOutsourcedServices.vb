Imports System.Runtime.Serialization
Partial Public Class AuthorizationOutsourcedServices

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

    ''' <summary>
    ''' Listado de ids de tramites
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ListTraceabilityPaperworkIds As List(Of Integer)

End Class
