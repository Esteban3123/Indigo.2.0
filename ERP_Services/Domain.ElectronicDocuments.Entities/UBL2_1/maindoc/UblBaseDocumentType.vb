Imports System.Xml.Serialization

Namespace UBL2_1.maindoc

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="dian:gov:co:facturaelectronica:Structures-2-1"),
        System.Xml.Serialization.XmlRootAttribute("DianExtensions", [Namespace]:="dian:gov:co:facturaelectronica:Structures-2-1", IsNullable:=False)>
    Partial Public MustInherit Class UblBaseDocumentType

        Public Shared ReadOnly Property Namespaces() As Dictionary(Of String, String)
            Get
                Dim ns As New Dictionary(Of String, String)

                ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#")
                ns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#")
                'ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
                ns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
                ns.Add("xades141", "http://uri.etsi.org/01903/v1.4.1#")
                ns.Add("sts", "dian:gov:co:facturaelectronica:Structures-2-1")
                ns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
                ns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")

                Return ns
            End Get
        End Property

        <XmlNamespaceDeclarations()>
        Public Property Xmlns As XmlSerializerNamespaces
            Get
                Dim xsNs As New XmlSerializerNamespaces()

                For Each ns In Namespaces
                    xsNs.Add(ns.Key, ns.Value)
                Next

                Return xsNs
            End Get
            Set(value As XmlSerializerNamespaces)

            End Set
        End Property

    End Class

End Namespace