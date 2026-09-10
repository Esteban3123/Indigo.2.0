Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

''' <summary>
''' De acuerdo con el Anexo Tecnico 003 - Mecanismos Sistema Tecnico de Control
''' El CUFE, permite identificar unívocamente una factura electrónica en el territorio nacional, lo cual se
''' logra por medio de la generación de un código único usando una función matemática del tipo one way hash,
''' o cryptographic hash function, o resumen criptográfico.
''' </summary>
Partial Public Class Invoice

#Region "For ElectronicBilling"

    Public InvoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result

    Public InvoiceDetails As List(Of SP_GetInvoiceDetailsByInvoiceId_Result)

    Public InvoicePrepaidPayment As List(Of SP_GetPrepaidPaymentHealth)

#End Region

#Region "Properties ElectronicBilling"

    ''' <summary>
    ''' Versión de la facturación electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property DianVersion As Decimal

    ''' <summary>
    ''' Obtiene el Número de factura
    ''' </summary>
    Public ReadOnly Property NumFac As String
        Get
            Return Me.InvoiceNumber
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha de factura en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property FecFac As String
        Get
            If Me.DianVersion = 2.1 Then
                Return Me.InvoiceDate.ToString("yyyy-MM-dd")
            End If
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha de factura en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property HorFac As String
        Get
            If Me.DianVersion = 2.1 Then
                Return Me.InvoiceDate.ToString("HH:mm:ss-05:00")
            End If
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el Valor Factura sin IVA, sin Descuentos, con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public ReadOnly Property ValFac As Decimal
        Get
            Return Me.InvoiceValue
        End Get
    End Property

    ''' <summary>
    ''' CODIGO IVA
    ''' </summary>
    Public ReadOnly Property CodImp1 As String = "01"

    ''' <summary>
    ''' VALOR TOTAL DE IVA
    ''' </summary>
    Public ReadOnly Property ValImp1 As Decimal
        Get
            Return Me.ValueTax
        End Get
    End Property

    ''' <summary>
    ''' CODIGO IMPUESTO AL CONSUMO
    ''' </summary>
    Public ReadOnly Property CodImp2 As String
        Get
            If DianVersion = 2.1 Then
                Return "04"
            End If
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' VALOR TOTAL DE IMPUESTO AL CONSUMO
    ''' </summary>
    Public Property ValImp2 As Decimal

    ''' <summary>
    ''' CODIGO ICA
    ''' </summary>
    Public ReadOnly Property CodImp3 As String = "03"

    ''' <summary>
    ''' VALOR TOTAL DE ICA
    ''' </summary>
    Public Property ValImp3 As Decimal

    ''' <summary>
    ''' Obtiene el Valor Total a pagar, con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public ReadOnly Property ValPag As Decimal
        Get
            Return TotalValue
        End Get
    End Property

    ''' <summary>
    ''' NIT del Facturador Electrónico sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property NitFE As String

    ''' <summary>
    ''' Tipo de adquirente, de acuerdo la tabla Tipos de Persona del «Anexo 001 Formato estándar XML de la Factura, notas débito y notas crédito electrónicos»
    ''' 8.1.1. Tipos de documentos de identidad
    ''' R-00-PN - No obligado a registrarse en el RUT PN
    ''' 11 - Registro civil
    ''' 12 - Tarjeta de identidad
    ''' 13 - Cédula de ciudadanía
    ''' 21 - Tarjeta de extranjería
    ''' 22 - Cédula de extranjería
    ''' 31 - NIT
    ''' 41 - Pasaporte
    ''' 42 - Documento de identificación extranjero
    ''' 91 - NUIP
    ''' </summary>
    Public Property TipAdq As String

    ''' <summary>
    ''' Número de identificación del adquirente sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property NumAdq As String

    ''' <summary>
    ''' Clave técnica del rango de facturación; se asigna a las Facturas Electrónicas de Venta, i.e. “de venta”, “de exportación”.
    ''' </summary>
    Public Property ClTec As String

    ''' <summary>
    ''' Pin del software registrado en el catalogo del participante
    ''' </summary>
    Public Property SoftwarePin As String

    ''' <summary>
    ''' Tipo de ambiente
    ''' 0 - Pruebas
    ''' 1 - Producción
    ''' </summary>
    ''' <returns></returns>
    Public Property Environment As Boolean

    ''' <summary>
    ''' Obtiene el Total de Descuentos: Suma de todos los descuentos presentes
    ''' </summary>
    Public ReadOnly Property TotalDiscount As Decimal
        Get
            Return Me.ThirdPartyDiscountValue
        End Get
    End Property

    ''' <summary>
    ''' tipo de contribuyente tercero
    ''' </summary>    
    Public ThirdPartyContributionType As Byte

    ''' <summary>
    ''' propiedad para establecer la relacion entre factura (invoice - basicBilling) CON (Invoice-fact Salud)
    ''' </summary>
    <DataMember()>
    Public InvoiceCopayCustom As InvoiceCopay
#End Region

#Region "Properties"

    ''' <summary>
    ''' RefeFuente
    ''' </summary>
    ''' <returns></returns>
    Public Property RTFValue As Decimal

    ''' <summary>
    ''' ReteIva
    ''' </summary>
    ''' <returns></returns>
    Public Property WithholdingTax As Decimal

    ''' <summary>
    ''' ReteIca
    ''' </summary>
    ''' <returns></returns>
    Public Property WithholdingICA As Decimal

#End Region

#Region "Methods"

    ''' <summary>
    ''' Si es una factura de salud enviamos la descripción del contrato, si no enviamos deacuerdo al tipo de contribuyente del cliente
    ''' </summary>
    ''' <returns></returns>
    Public Function getObservation() As String
        If InvoiceMoreInformation IsNot Nothing Then
            Return InvoiceMoreInformation.PermanentObservationOfTheInvoice
        End If
        Dim concatObservation As String = String.Empty
        If String.IsNullOrEmpty(Me.Observation) Then
            If ThirdPartyContributionType = 2 Then
                Return String.Format("#${0}#$", Me.getDocumentTypeName())
            End If
            Return Me.getDocumentTypeName()
        End If
        If ThirdPartyContributionType = 2 Then 'tipo de contribuyente empresa estatal            
            Return String.Format("#${0}#$", Me.Observation)
        End If
        Return Me.Observation
    End Function

    ''' <summary>
    ''' Descripción del tipo de documento
    ''' </summary>
    Public Function getDocumentTypeName() As String
        Dim DocumentTypeName As String = String.Empty
        Select Case DocumentType
            Case 1
                DocumentTypeName = "Factura EAPB con Contrato"
            Case 2
                DocumentTypeName = "Factura EAPB sin Contrato"
            Case 3
                DocumentTypeName = "Factura Particular"
            Case 4
                DocumentTypeName = "Factura Capitada"
            Case 5 'Este tipo no es factura, simplemente es un control de servicios
                DocumentTypeName = "Control de Capitacion"
            Case 6
                DocumentTypeName = "Factura Basica"
            Case 7
                DocumentTypeName = "Factura de Venta de Productos"
        End Select
        Return DocumentTypeName
    End Function

    ''' <summary>
    ''' Dependiendo del tipo de documento es un descuento o una cuota de paciente
    ''' </summary>
    Public Function getAllowanceChargeReason(Optional isDiscount As Boolean = True) As String
        Dim AllowanceChargeReason As String = String.Empty
        If isDiscount Then
            If Me.DocumentType = 1 OrElse Me.DocumentType = 2 OrElse Me.DocumentType = 3 Then
                AllowanceChargeReason = "Cuota Responsabilidad del Paciente"
            Else
                AllowanceChargeReason = "Descuento"
            End If
        Else
            AllowanceChargeReason = "Valor Distribuido"
        End If

        Return AllowanceChargeReason
    End Function

    ''' <summary>
    ''' Obtiene el texto base para la generación del código CUFE
    ''' </summary>
    Public Function getCUFEDefinition() As String

        If Me.DianVersion = 2.1 Then
            Return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                             Me.NumFac,
                             Me.FecFac,
                             Me.HorFac,
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValFac),
                             Me.CodImp1,
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValImp1),
                             Me.CodImp2,
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValImp2),
                             Me.CodImp3,
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValImp3),
                             Utils.GetFormatNumberWithTwoDecimals(GetTotalValue()),
                             Me.NitFE,
                             Me.NumAdq,
                             Me.ClTec,
                             If(Me.Environment, 1, 2))
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el total de factura(Incluyendo impuestos) por el tipo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTotalValue() As Decimal
        If Me.DocumentType = 3 Then 'Particular
            Return Me.ValFac
        ElseIf Me.DocumentType = 6 Then 'Factura básica
            Return Me.ValFac + Me.ValueTax
        Else
            Return Me.ValPag
        End If
    End Function

    ''' <summary>
    ''' Obtiene el código CUFE encriptado
    ''' </summary>
    Public Function getCUFE() As String
        If DianVersion = 2.1 Then
            Return Utils.Sha384Encode(Me.getCUFEDefinition())
        End If
        Return Nothing
    End Function

    Public Function GetQRCode() As String
        If Me.Environment Then
            Return String.Format("https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey={0}", Me.CUFE)
        Else
            Return String.Format("https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey={0}", Me.CUFE)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el texto para la generación del código QR
    ''' </summary>
    Public Function getQRDefinition() As String
        Dim str As New Text.StringBuilder()
        If DianVersion = 2.1 Then
            str.AppendLine(String.Concat("NumFac: ", Me.InvoiceNumber))
            str.AppendLine(String.Concat("FecFac: ", Me.FecFac))
            str.AppendLine(String.Concat("HorFac: ", Me.HorFac))
            str.AppendLine(String.Concat("NitFac: ", Me.NitFE))
            str.AppendLine(String.Concat("DocAdq: ", Me.NumAdq))
            str.AppendLine(String.Concat("ValFac: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValFac)))
            str.AppendLine(String.Concat("ValIva: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValImp1)))
            str.AppendLine(String.Concat("ValOtroIm: ", Utils.GetFormatNumberWithTwoDecimals((Me.ValImp2 + Me.ValImp3))))
            str.AppendLine(String.Concat("ValTolFac: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValFac)))
            str.AppendLine(String.Concat("CUFE: ", Me.CUFE))
            str.AppendLine(String.Concat("QRCode: ", Me.GetQRCode()))
        End If
        Return str.ToString().Trim()
    End Function

    ''' <summary>
    ''' Indica si es una factura de venta o una factura cambiaria de compraventa
    ''' </summary>
    ''' <returns></returns>
    Public Function getInvoiceTypeCode() As String
        If Me.DianVersion = 2.1 Then
            Return Domain.Base.Entities.Enums.ElectronicDocuments.v1_6.DocumentType.NationalSalesInvoice
        End If
        Return Nothing
    End Function

#End Region
#Region "Properties extend to PortfolioNote"
    <DataMember()>
    Public Property PortfolioNoteAccountReceivableAdvanceId As Integer?
#End Region
End Class
