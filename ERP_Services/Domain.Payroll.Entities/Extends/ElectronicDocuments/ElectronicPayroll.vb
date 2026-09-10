Imports Infrastructure.CrossCutting.Root

Partial Public Class ElectronicPayroll

#Region "Properties"

    Public ElectronicPayrollPaymentSupport As ElectronicPayrollPaymentSupport

    Public NoteAdjustmentElectronicPayroll As ElectronicPayroll

#End Region

#Region "Properties CUNE"

    ''' <summary>
    ''' Obtiene el Número del Soporte de Pago de Nomina Electronica
    ''' </summary>
    Public ReadOnly Property NumNE As String
        Get
            Return Me.GetDocumentNumber()
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha del Soporte de Pago de Nomina Electronica en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property FecNE As String
        Get
            Return Me.CreationDate.ToString("yyyy-MM-dd")
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Fecha del Soporte de Pago de Nomina Electronica en formato YYYYmmddHHMMss
    ''' </summary>
    Public ReadOnly Property HorNE As String
        Get
            Return Me.CreationDate.ToString("HH:mm:ss-05:00")
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el total devengos con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public Property ValDev As Decimal

    ''' <summary>
    ''' Obtiene o establece el total deducciones con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public Property ValDed As Decimal

    ''' <summary>
    ''' Obtiene o establece el total pagado, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    Public Property ValTolNE As Decimal

    ''' <summary>
    ''' NIT del Emisor del documento sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property NitNE As String

    ''' <summary>
    ''' Número de identificación del empleado sin puntos ni guiones, sin digito de verificación
    ''' </summary>
    Public Property DocEmp As String

    ''' <summary>
    ''' Tipo de XML utilizado
    ''' </summary>
    Public ReadOnly Property TipoXML As String
        Get
            Dim _type As String
            If DocumentType = 1 Then
                _type = "102"
            ElseIf DocumentType = 2 Then
                _type = "103"
            End If
            Return _type
        End Get
    End Property

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Tipos de Documentos
    ''' 1. Factura
    ''' 2. Nota Debito
    ''' 3. Nota Credito
    ''' </summary>
    Public Function getDocumentType() As TypeElectronicDocument
        If DocumentType = 1 Then
            Return TypeElectronicDocument.NominaIndividual
        ElseIf DocumentType = 2 Then
            Return TypeElectronicDocument.NominaIndividualDeAjuste
        End If
        Return TypeElectronicDocument.NominaIndividual
    End Function

    ''' <summary>
    ''' Obtiene el número del documento concatenado el prefijo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDocumentNumber() As String
        Return String.Concat(Me.Prefix, Me.DocumentNumber)
    End Function

    ''' <summary>
    ''' Tipos de Documentos
    ''' 1. Soporte de Pago de Nomina Electronica
    ''' 2. Nota de Ajuste Soporte de Pago de Nomina Electronica
    ''' 3. Factura Particular
    ''' 4. Factura Capitada
    ''' 5. Control de Capitacion
    ''' 6. Factura Basica
    ''' 7. Factura de Venta de Productos
    ''' --------------------------------
    ''' 91. Nota Credito Validacion Previa
    ''' 92. Nota Debito Validacion Previa
    ''' 93. Nota Credito Validacion Previa - Facturas version anterior
    ''' 94. Nota Debito Validacion Previa - Facturas version anterior
    ''' 98. Nota Debito
    ''' 99. Nota Credito
    ''' </summary>
    Public Function getDocumentTypeName() As String
        Dim DocumentTypeName As String = String.Empty
        Select Case DocumentType
            Case 1
                DocumentTypeName = "Soporte de Pago de Nomina Electronica"
            Case 2
                DocumentTypeName = "Nota de Ajuste Soporte de Pago de Nomina Electronica"
        End Select
        Return DocumentTypeName
    End Function

    ''' <summary>
    ''' Obtiene el nombre del documento dependiendo del tipo de documento
    ''' </summary>
    ''' <param name="documentType">Tipo de documento
    ''' 1 - Soporte de Pago de Nomina Electronica
    ''' 2 - Nota de Ajuste de Soporte de Pago de Nomina Electronica
    ''' 4 - ApplicationResponse
    ''' 5 - AttachmentDocument</param>
    ''' <returns></returns>
    Public Function GetFileName(documentType As TypeElectronicDocument) As String
        Dim format = "{0}{1}{2}.xml"
        Dim documentTypeName = String.Empty
        Dim year = Me.Year.ToString().Substring(2, 2)
        Dim consecutive = Hex(Me.Consecutive).PadLeft(8, "0")

        If documentType = TypeElectronicDocument.NominaIndividual Then
            documentTypeName = "nie"
        ElseIf documentType = TypeElectronicDocument.NominaIndividualDeAjuste Then
            documentTypeName = "niae"
        ElseIf documentType = TypeElectronicDocument.ApplicationResponse Then
            documentTypeName = "ar"
        ElseIf documentType = TypeElectronicDocument.AttachedDocument Then
            documentTypeName = "ad"
        End If

        Return String.Format(format, documentTypeName, year, consecutive)
    End Function

    ''' <summary>
    ''' Obtiene el texto base para la generación del código CUNE
    ''' </summary>
    Public Function getCUFEDefinition() As String
        Return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                             Me.NumNE,
                             Me.FecNE,
                             Me.HorNE,
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValDev),
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValDed),
                             Utils.GetFormatNumberWithTwoDecimals(Me.ValTolNE),
                             Me.NitNE,
                             Me.DocEmp,
                             Me.TipoXML,
                             Me.SoftwarePin,
                             If(Me.Environment, 1, 2))
    End Function

    ''' <summary>
    ''' Obtiene el código CUNE encriptado
    ''' </summary>
    Public Function getCUNE() As String
        Return Utils.Sha384Encode(Me.getCUFEDefinition())
    End Function

    ''' <summary>
    ''' Obtiene el código QR
    ''' </summary>
    ''' <returns></returns>
    Public Function GetQRCode() As String
        If Me.Environment Then
            Return String.Format("https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey={0}", Me.CUNE)
        Else
            Return String.Format("https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey={0}", Me.CUNE)
        End If
    End Function

#End Region

End Class
