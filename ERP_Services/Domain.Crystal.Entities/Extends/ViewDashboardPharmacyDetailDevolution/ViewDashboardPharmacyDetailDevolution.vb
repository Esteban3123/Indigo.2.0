Imports Domain.Entities
Imports System.Runtime.Serialization

Partial Class ViewDashboardPharmacyDetailDevolution

    Property ListPharmaceuticalDispensingDetailBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)

    Property Action As Integer?

    <DataMember>
    Public Property QuantityReceived As Integer

    ''' <summary>
    ''' Id del Motivo general para la anulacion de una solicitud
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property HCMOANULBId As String

    ''' <summary>
    ''' La descripcion de la anulacion de una solictud
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property Description As String

    '<DataMember>
    'Property EntityId As Integer

    '<DataMember>
    'Property EntityName As String

End Class
