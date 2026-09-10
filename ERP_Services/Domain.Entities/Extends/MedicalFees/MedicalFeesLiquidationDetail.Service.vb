Imports System.Runtime.Serialization
Partial Public Class MedicalFeesLiquidationDetail

    ''' <summary>
    ''' Obtiene o establece el numero de la admision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AdmissionNumber As String

    ''' <summary>
    ''' Obtiene o establece el codigo del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PatientCode As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de excepcion
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo de la orden de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ServiceOrderCode As String

    ''' <summary>
    ''' Obtiene o establece el valor unitario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AmountPayable As Decimal

    ''' <summary>
    ''' Obtiene o establece la cantidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property InvoiceQuantity As Integer

    ''' <summary>
    ''' Obtiene o establece el total
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property TotalAmountPayable As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor causado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MedicalFeesContractValue As Decimal

    ''' <summary>
    ''' Obtiene o establece si la causacion fue reversada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property InvoiceReversal As Boolean

    ''' <summary>
    ''' Obtiene o establece el estado de la causacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property StatusCausation As Integer

    ''' <summary>
    ''' Obtiene o establece la fecha de la causaci+on
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CausationDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el nombre del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property IPSServiceName As String

    ''' <summary>
    ''' Obtiene o establece si se cambia el estado de la causación cuando 
    ''' se elimina un detalle de la liquidación de honorarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ChangeStatusMedicalFeesCausation As Boolean

    ''' <summary>
    ''' Obtiene la fecha del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ServiceDate As DateTime

    <DataMember>
    Public Property InvoiceId As Integer

    <DataMember>
    Public Property InvoiceDetailId As Integer

End Class
