Imports Domain.ElectronicDocuments.Entities.UBL2_1.signature

Namespace UBL2_1.maindoc

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"),
        System.Xml.Serialization.XmlRootAttribute("ExtensionContent", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2", IsNullable:=False)>
    Partial Public Class ExtensionContentType

        Private dianExtensionsType As DianExtensionsType

        Private customTagGeneralType As CustomTagGeneralType

        Private signatureType As SignatureType

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="dian:gov:co:facturaelectronica:Structures-2-1")>
        Public Property DianExtensions() As DianExtensionsType
            Get
                Return Me.dianExtensionsType
            End Get
            Set
                Me.dianExtensionsType = Value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")>
        Public Property CustomTagGeneral() As CustomTagGeneralType
            Get
                Return Me.customTagGeneralType
            End Get
            Set
                Me.customTagGeneralType = Value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="dian:gov:co:facturaelectronica:Structures-2-1")>
        Public Property Signature() As SignatureType
            Get
                Return Me.signatureType
            End Get
            Set
                Me.signatureType = Value
            End Set
        End Property
    End Class

End Namespace
