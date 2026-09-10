Imports Infrastructure.CrossCutting.Root

Partial Public Class ElectronicDocument

#Region "Properties"
    ''' <summary>
    ''' Modo de impresión - para enviar en el reporte al adquieriente
    ''' </summary>
    Public printMode As Byte
#End Region

#Region "Methods"

    ''' <summary>
    ''' Tipos de Documentos
    ''' 1. Factura
    ''' 2. Nota Debito
    ''' 3. Nota Credito
    ''' </summary>
    Public Function getDocumentType() As TypeElectronicDocument
        If {91, 93, 99}.Contains(DocumentType) Then
            Return TypeElectronicDocument.CreditNote
        ElseIf {92, 94, 98}.Contains(DocumentType) Then
            Return TypeElectronicDocument.DebitNote
        Else
            Return TypeElectronicDocument.Invoice
        End If
    End Function

    ''' <summary>
    ''' Tipos de Documentos
    ''' 01. Factura
    ''' 92. Nota Debito
    ''' 91. Nota Credito
    ''' </summary>
    Public Function getDocumentTypeCode() As String
        Dim DocumentTypeCode = String.Empty
        If {91, 93, 99}.Contains(DocumentType) Then
            DocumentTypeCode = "91"
        ElseIf {92, 94, 98}.Contains(DocumentType) Then
            DocumentTypeCode = "92"
        Else
            DocumentTypeCode = "01"
        End If
        Return DocumentTypeCode
    End Function

    ''' <summary>
    ''' Tipos de Documentos
    ''' 1. Factura EAPB con Contrato
    ''' 2. Factura EAPB Sin Contrato
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
                DocumentTypeName = "Factura EAPB con contrato"
            Case 2
                DocumentTypeName = "Factura EAPB sin contrato"
            Case 3
                DocumentTypeName = "Factura particular"
            Case 4
                DocumentTypeName = "Factura capitada"
            Case 5 'Este tipo no es factura, importante tener en cuenta
                DocumentTypeName = "Control de capitación"
            Case 6
                DocumentTypeName = "Factura básica"
            Case 7
                DocumentTypeName = "Factura de Venta de Productos"
            Case 91
                DocumentTypeName = "Nota crédito"
            Case 92
                DocumentTypeName = "Nota débito"
            Case 93
                DocumentTypeName = "Nota crédito: Referencia a facturas electrónicas diferentes a validación previa"
            Case 94
                DocumentTypeName = "Nota débito: Referencia a facturas electrónicas diferentes a validación previa"
            Case 98
                DocumentTypeName = "Nota débito"
            Case 99
                DocumentTypeName = "Nota crédito"
        End Select
        Return DocumentTypeName
    End Function

    ''' <summary>
    ''' Obtiene el nombre del estado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDocumentStatusName() As String
        Select Case Status
            Case 0
                Return "Erróneo"
            Case 1
                Return "Registrado"
            Case 2
                Return "Enviado"
            Case 3
                Return "Válido"
            Case 4
                Return "Inválido"
            Case Else
                Return "Procesando"
        End Select
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
    ''' Obtiene el número del documento concatenado el prefijo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDocumentNumber() As String
        Return String.Concat(Me.Prefix, Me.DocumentNumber)
    End Function

    ''' <summary>
    ''' Obtiene el nombre del documento dependiendo del tipo de documento
    ''' </summary>
    ''' <param name="supplierThirdParty">Facturador Electrónico</param>
    ''' <param name="documentType">Tipo de documento
    ''' 1 - Factura
    ''' 2 - Nota Débito
    ''' 3 - Nota Crédito
    ''' 4 - ApplicationResponse
    ''' 5 - AttachmentDocument</param>
    ''' <returns></returns>
    Public Function GetFileName(supplierThirdParty As ThirdParty, documentType As TypeElectronicDocument) As String
        Dim format = String.Empty
        Dim documentTypeName = String.Empty
        Dim nit = supplierThirdParty.Person.IdentificationNumber.PadLeft(10, "0")
        Dim ptcode = "000" 'Codigo asignado al proveedor tecnológico (Software propio es 000)
        Dim year = Me.Year.ToString().Substring(2, 2)
        Dim consecutive = Me.Consecutive.ToString.PadLeft(8, "0")

        If Me.DianVersion = 2.1 Then
            format = "{0}{1}{2}{3}{4}.xml"

            If documentType = TypeElectronicDocument.Invoice Then
                documentTypeName = "fv"
            ElseIf documentType = TypeElectronicDocument.DebitNote Then
                documentTypeName = "nd"
            ElseIf documentType = TypeElectronicDocument.CreditNote Then
                documentTypeName = "nc"
            ElseIf documentType = TypeElectronicDocument.ApplicationResponse Then
                documentTypeName = "ar"
            ElseIf documentType = TypeElectronicDocument.AttachedDocument Then
                documentTypeName = "ad"
            End If

            Return String.Format(format, documentTypeName, nit, ptcode, year, consecutive)
        End If

        Return String.Empty
    End Function

    ''' <summary>
    ''' Validar si el estado es válido para reintentar el envío del documento electrónico
    ''' </summary>
    ''' <returns></returns>
    Public Function IsValidStatusToResend() As Boolean
        Return Not {2, 3, 66, 77, 88, 99}.Contains(Me.Status)
    End Function

#End Region

End Class