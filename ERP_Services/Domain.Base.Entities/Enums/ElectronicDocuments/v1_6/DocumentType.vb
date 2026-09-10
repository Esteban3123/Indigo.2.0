Namespace Enums.ElectronicDocuments.v1_6

    ''' <summary>
    ''' 6.1.3. Tipo de Documento: cbc:InvoiceTypeCode y cbc:CreditnoteTypeCode
    ''' </summary>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0"),
     System.SerializableAttribute(),
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="dian:gov:co:facturaelectronica:Structures-2-1")>
    Public Enum DocumentType

        <System.Xml.Serialization.XmlEnumAttribute("01")>
        NationalSalesInvoice

        <System.Xml.Serialization.XmlEnumAttribute("02")>
        ExportInvoice

        <System.Xml.Serialization.XmlEnumAttribute("03")>
        BillingContingencyInvoice

        <System.Xml.Serialization.XmlEnumAttribute("04")>
        DIANContingencyInvoice

        <System.Xml.Serialization.XmlEnumAttribute("05")>
        SupportDocument

        <System.Xml.Serialization.XmlEnumAttribute("91")>
        CreditNote

        <System.Xml.Serialization.XmlEnumAttribute("92")>
        DebitNote

        <System.Xml.Serialization.XmlEnumAttribute("95")>
        AdjustmentNoteSupportDocument

        <System.Xml.Serialization.XmlEnumAttribute("Contenedor de Factura Electrónica")>
        AttachedDocument
    End Enum

End Namespace