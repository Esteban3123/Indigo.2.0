Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Root

Partial Public Class ElectronicSupportDocument

#Region " Properties"

#Region "For Electronic Documents"
    Public documentSupportDetails As List(Of SP_GetDocumentSupportDetailsById_Result)
#End Region

    <DataMember>
    Property SupplierCodeName As String

    <DataMember>
    Property BillingAuthorizationCodeName As String

    <DataMember>
    Property ToConfirm As Boolean

    ''' <summary>
    ''' Nit de la compañia 
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property IndigoCompanyNit As String

    ''' <summary>
    ''' numero de identificacion del proveedor sin digito de verificacion
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property SupplierNit As String

    Public Property dueDatePayment As Date


#Region "For CUDS"
    ''' <summary>
    ''' Fecha de emisión
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FecDS As String
        Get
            Return Me.DocumentDate.ToString("yyyy-MM-dd")
        End Get
    End Property

    ''' <summary>
    ''' Hora del documento soporte incluyendo UTC - 05:00
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HorDS As String
        Get
            Return Me.DocumentDate.ToString("HH:mm:ss-05:00")
        End Get
    End Property

    ''' <summary>
    ''' Valor del documento soporte sin Impuestos, con punto decimal, con decimales a dos (2) 
    ''' dígitos truncados, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValDS As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.documentSupportDetails?.Sum(Function(d) d.DebitValue))
        End Get
    End Property

    ''' <summary>
    ''' Valor fijo según resolución 000167
    ''' </summary>
    Public ReadOnly Property CodImp As String = "01"

    ''' <summary>
    ''' Valor impuesto 01 - IVA, con punto decimal, con decimales a dos (2) dígitos truncados, sin 
    ''' separadores de miles, ni símbolo pesos. Si no está referenciado el impuesto 01 – IVA este valor 
    ''' se representa con 0.00
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValImp As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.TaxValue)
        End Get
    End Property

    ''' <summary>
    ''' Valor total del documento soporte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValTot As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.documentSupportDetails?.Sum(Function(d) d.DebitValue))
        End Get
    End Property

    ''' <summary>
    ''' Numero de identificacion del vendedor sin puntos ni guiones, sin digito de verificacion
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NumSNO As String
        Get
            Return SupplierNit
        End Get
    End Property

    ''' <summary>
    ''' Numero de identificacion del adquiriente sin puntos ni guiones, sin digito de verificacion
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NITABS As String
        Get
            Return IndigoCompanyNit
        End Get
    End Property

    ''' <summary>
    ''' Pin del software registrado en el catalogo del participante
    ''' </summary>
    ''' <returns></returns>
    Public Property SoftwarePin As String

    ''' <summary>
    ''' Tipo de ambiente 1-produccion, 2-Pruebas
    ''' </summary>
    ''' <returns></returns>
    Public Property Environment As Integer
#End Region

#End Region

#Region "Methods"
    Public Function GetQRCode() As String
        If Me.Environment = 1 Then
            Return String.Format("https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDS)
        Else
            Return String.Format("https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDS)
        End If
    End Function

    Public Function getInvoiceTypeCode() As String
        Return Domain.Base.Entities.Enums.ElectronicDocuments.v1_6.DocumentType.SupportDocument
    End Function

    ''' <summary>
    ''' arma el string para el CUDS
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUDSCode() As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha384Encode(Me.GetCUDSDefinition())
    End Function

    Public Function GetCUDSDefinition() As String
        Return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                             Me.DocumentNumber,
                             Me.FecDS,
                             Me.HorDS,
                             Me.ValDS,
                             Me.CodImp,
                             Me.ValImp,
                             Me.ValTot,
                             Me.NumSNO,
                             Me.NITABS,
                             Me.SoftwarePin,
                             Me.Environment
                             )
    End Function

    ''' <summary>
    ''' 1 - Documento soporte
    ''' 2 - Nota de Ajuste
    ''' 3 - ApplicationResponse
    ''' </summary>
    ''' <param name="customerThirdParty"></param>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    Public Function GetFileName(customerThirdParty As ThirdParty, documentType As TypeElectronicDocument) As String
        Dim format = String.Empty
        Dim documentTypeName = String.Empty
        Dim nit = customerThirdParty.Person.IdentificationNumber.PadLeft(10, "0")
        Dim ptcode = "095" 'Codigo asignado al proveedor tecnológico (INDIGO es 095)
        Dim year = Me.Year.ToString().Substring(2, 2)
        Dim consecutive = Me.Consecutive.ToString.PadLeft(8, "0")

        format = "{0}{1}{2}{3}{4}.xml"

        If documentType = TypeElectronicDocument.DocumentoSoporte Then
            documentTypeName = "ds"
        ElseIf documentType = TypeElectronicDocument.NotaDeAjuste Then
            documentTypeName = "nas"
        ElseIf documentType = TypeElectronicDocument.ApplicationResponse Then
            documentTypeName = "ars"
        End If

        Return String.Format(format, documentTypeName, nit, ptcode, year, consecutive)

    End Function

#End Region
End Class
