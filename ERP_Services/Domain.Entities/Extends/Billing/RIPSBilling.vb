Imports System.Runtime.Serialization

<DataContract()>
Public Class RIPSBilling

    ''' <summary>
    ''' Tipo de documento 
    ''' </summary>
    ''' <returns>
    ''' 1= Factura EAPB con Contrato
    ''' 2= Factura EAPB Sin Contrato 
    ''' 3= Factura Particular
    ''' 4= Factura Capitada 
    ''' 5= Control de Capitacion
    ''' 6= Factura Basica
    ''' 7= Factura de Venta de Productos  
    ''' </returns>
    <DataMember()>
    Public Property DocumentType As Byte
    ''' <summary>
    ''' Id de la factura
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InvoiceId As Integer
    <DataMember()>
    Public Property InvoiceRadicateId As Integer?
    <DataMember()>
    Public Property CapitationInitialDate As DateTime?
    <DataMember()>
    Public Property CapitationEndDate As DateTime?
    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ThirdPartyId As Integer?
    ''' <summary>
    ''' Id Grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CareGroupId As Integer?
    <DataMember()>
    Public Property InvoiceCategoryId As Integer
    <DataMember()>
    Public Property FechaCorte As DateTime
    ''' <summary>
    ''' Fecha de la factura
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InvoiceDate As DateTime

    ''' <summary>
    ''' Numero consecutivo de la factura
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InvoiceNumber As String

    ''' <summary>
    ''' Numero consecutivo de la factura
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property AdmissionNumber As String

End Class
