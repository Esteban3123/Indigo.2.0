Imports Infrastructure.CrossCutting.Base

Partial Public Class BillingNote

#Region "Properties ElectronicBilling"


    ''' <summary>
    ''' Obtiene el Número de factura
    ''' </summary>
    Public ReadOnly Property NumFac As String
        Get
            Return Me.Code
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha de factura en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property FecFac As String
        Get
            Return Me.NoteDate.ToString("yyyy-MM-dd")
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha de factura en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property HorFac As String
        Get
            Return Me.NoteDate.ToString("HH:mm:ss-05:00")
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el Valor Factura sin IVA, sin Descuentos, con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public ReadOnly Property ValFac As Decimal
        Get
            Return Me.BillingNoteDetail.Sum(Function(d) d.BillingValue)
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
            Dim ValueTax As Decimal = 0

            If Me.BillingNoteDetail.Any(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Count > 0) Then
                ValueTax = Me.BillingNoteDetail.Where(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Count > 0).SelectMany(Function(d) d.BillingNoteDetailTax).Sum(Function(d) d.TaxValue)
            End If

            Return ValueTax
        End Get
    End Property

    ''' <summary>
    ''' CODIGO IMPUESTO AL CONSUMO
    ''' </summary>
    Public ReadOnly Property CodImp2 As String = "04"

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
            Return Me.BillingNoteDetail.Sum(Function(d) d.AdjusmentValue)
        End Get
    End Property

    ''' <summary>
    ''' NIT del Facturador Electrónico sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property NitFE As String

    ''' <summary>
    ''' Número de identificación del adquirente sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property NumAdq As String

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
    ''' extended to identify if the note is type Detail
    ''' </summary>
    ''' <returns></returns>
    Public Property NoteTypeDetail As Boolean
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el texto base para la generación del código CUFE
    ''' </summary>
    Public Function getCUDEDefinition() As String
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
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValPag),
                             Me.NitFE,
                             Me.NumAdq,
                             Me.SoftwarePin,
                             If(Me.Environment, 1, 2))
    End Function

    ''' <summary>
    ''' Obtiene el código CUFE encriptado
    ''' </summary>
    Public Function getCUDE() As String
        Return Utils.Sha384Encode(Me.getCUDEDefinition())
    End Function

    Public Function GetQRCode() As String
        If Me.Environment Then
            Return String.Format("https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDE)
        Else
            Return String.Format("https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDE)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el texto para la generación del código QR
    ''' </summary>
    Public Function getQRDefinition() As String
        Dim str As New Text.StringBuilder()

        str.AppendLine(String.Concat("NumFac: ", Me.NumFac))
        str.AppendLine(String.Concat("FecFac: ", Me.FecFac))
        str.AppendLine(String.Concat("HorFac: ", Me.HorFac))
        str.AppendLine(String.Concat("NitFac: ", Me.NitFE))
        str.AppendLine(String.Concat("DocAdq: ", Me.NumAdq))
        str.AppendLine(String.Concat("ValFac: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValFac)))
        str.AppendLine(String.Concat("ValIva: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValImp1)))
        str.AppendLine(String.Concat("ValOtroIm: ", Utils.GetFormatNumberWithTwoDecimals((Me.ValImp2 + Me.ValImp3))))
        str.AppendLine(String.Concat("ValTolFac: ", Utils.GetFormatNumberWithTwoDecimals(Me.ValPag)))
        str.AppendLine(String.Concat("CUFE: ", Me.CUDE))
        str.AppendLine(String.Concat("QRCode: ", Me.GetQRCode()))

        Return str.ToString().Trim()
    End Function

    Public Function GetCustomizationID() As Byte
        Dim customizationID As String = String.Empty
        If Me.BillingNoteDetail.Any(Function(d) d.CUFE <> String.Empty) Then
            If Me.BillingNoteDetail.Any(Function(d) d.DianVersion = 0) Then
                customizationID = If(Me.Nature = 1, "32", "22")
            ElseIf Me.BillingNoteDetail.Any(Function(d) d.DianVersion = 1) Then
                customizationID = If(Me.Nature = 1, "33", "23")
            ElseIf Me.BillingNoteDetail.Any(Function(d) d.DianVersion = 2.1) Then
                customizationID = If(Me.Nature = 1, "30", "20")
            End If
        Else
            If Me.Nature = 1 Then
                customizationID = "32"
            Else
                customizationID = "22"
            End If
        End If
        Return customizationID
    End Function

    Public Function GetDocumentType() As Byte
        Dim documentType As Byte = 0

        If Me.Nature = 1 Then
            documentType = 92 ' IIf(Me.isCurrentVersion, 92, 94)
        ElseIf Me.Nature = 2 Then
            documentType = 91 ' IIf(Me.isCurrentVersion, 91, 93)
        End If

        Return documentType
    End Function

    Public Function GetResponseCodeName() As String
        Dim responseCodeName = String.Empty

        If Me.Nature = 1 Then
            responseCodeName = "Concepto de Notas débito"
        ElseIf Me.Nature = 2 Then
            responseCodeName = "Concepto de Notas crédito"
        End If

        Return responseCodeName
    End Function

#End Region

End Class
