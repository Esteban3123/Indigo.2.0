Imports System.Runtime.Serialization
Partial Public Class NoQxEntity

    ''' <summary>
    ''' Obtiene o establece el id de la entidad cups
    ''' </summary>
    <DataMember()>
    Public Property CupsEntityId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del grupo de atencion
    ''' </summary>
    <DataMember()>
    Public Property CareGroupId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    <DataMember()>
    Public Property RateManualId As Integer

    ''' <summary>
    ''' Obtiene o establece el valor total
    ''' </summary>
    <DataMember()>
    Public Property TotalSalesPrice As Decimal

    ''' <summary>
    ''' Obtiene o establece la presentación
    ''' </summary>
    <DataMember()>
    Public Property Presentation As Integer

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    <DataMember()>
    Public Property IPSServiceId As Integer

    ''' <summary>
    ''' Obtiene o establece el código y el nombre del servicio ips
    ''' </summary>
    <DataMember()>
    Public Property IPSServiceDescription As String

    ''' <summary>
    ''' Obtiene o establece el código del médico
    ''' </summary>
    <DataMember()>
    Public Property PerformsHealthProfessionalCode As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del tercero
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyDescription As String

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips padre
    ''' </summary>
    <DataMember()>
    Public Property IPSServiceSODId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del detalle de la orden de servicio
    ''' </summary>
    <DataMember()>
    Public Property ServiceOrderDetailId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del detalle Qx de la orden de servicio
    ''' </summary>
    <DataMember()>
    Public Property ServiceOrderDetailSurgicalId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del contrato profesional de la salud
    ''' </summary>
    <DataMember()>
    Public Property MedicalFeesContractId As Integer

    'Propiedades para la causacion masiva

    ''' <summary>
    ''' Obtiene o establece el valor causado
    ''' </summary>
    <DataMember()>
    Public Property AmountPayable As Decimal

    ''' <summary>
    ''' Obtiene o establece código y el nombre del contrato profesional de la salud
    ''' </summary>
    <DataMember()>
    Public Property MedicalFeesContractCodeName As String

    ''' <summary>
    ''' Obtiene o establece el listado de homologaciones que tiene el item
    ''' </summary>
    <DataMember()>
    Public Property ListCupsHomologation As List(Of CupsHomologation)

    ''' <summary>
    ''' Obtiene o establece si el item que se recorre tiene homologaciones(True) o ya fue diligenciado con los valores(False)
    ''' </summary>
    <DataMember()>
    Public Property IsHomologations As Boolean

    ''' <summary>
    ''' Obtiene o establece el tipo de manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RateManualType As Integer

    ''' <summary>
    ''' Obtiene o establece el estado del item 0=AlgunError, 1=SinError
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property StatusField As Integer

    ''' <summary>
    ''' Obtiene o establece el mensaje si ha ocurrido algun error con el item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MessageField As String

    ''' <summary>
    ''' Obtiene el id de la causacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MedicalFeesCausationId As Integer

End Class
