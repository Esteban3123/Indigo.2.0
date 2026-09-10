Imports System.Runtime.Serialization

#Region "New Xmls"

''' <summary>
''' Es usada para marcar los tipos VieXml, los cuales
''' son leidos de los recursos en el ensamblado Presentation.Base
''' </summary>
Public Interface IVieXml
End Interface

<KnownType(GetType(VieReportForm))> _
<DataContract()>
Public Class VieReport
    Implements IVieXml
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property CodeEntity As String
    <DataMember()>
    Public Property ClassName As String
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property Description As String

    <Xml.Serialization.XmlIgnore()>
    Public Property ClassType As Type
    <Xml.Serialization.XmlIgnore()>
    Public Property [Forms] As List(Of VieReportForm)
End Class

<KnownType(GetType(VieForm))> _
<KnownType(GetType(VieReport))> _
<DataContract()>
Public Class VieReportForm
    Implements IVieXml
    <DataMember()>
    Public Property IdReport As Integer
    <DataMember()>
    Public Property IdForm As String
    <DataMember()>
    Public Property IsDefault As Boolean

    <Xml.Serialization.XmlIgnore()>
    Public Property [Form] As VieForm
    <Xml.Serialization.XmlIgnore()>
    Public Property [Report] As VieReport
End Class

<KnownType(GetType(ViePermission))> _
<KnownType(GetType(VieReportForm))> _
<KnownType(GetType(VieForm))> _
<DataContract()>
Public Class ViePermissionForm
    Implements IVieXml
    <DataMember()>
    Public Property IdForm As String
    <DataMember()>
    Public Property IdPermission As Integer

    <Xml.Serialization.XmlIgnore()>
    Public Property [Form] As VieForm
    <Xml.Serialization.XmlIgnore()>
    Public Property Permission As ViePermission
    <Xml.Serialization.XmlIgnore()>
    Public Property Reports As List(Of VieReportForm)
End Class

<KnownType(GetType(VieModule))>
<KnownType(GetType(VieForm))>
<KnownType(GetType(VieTitle))>
<DataContract()>
Public Class VieFormModule
    Implements IVieXml
    <DataMember()>
    Public Property IdForm As String
    <DataMember()>
    Public Property IdModule As Integer
    <DataMember()>
    Public Property IdTitle As String

    <Xml.Serialization.XmlIgnore()>
    Public Property [Form] As VieForm
    <Xml.Serialization.XmlIgnore()>
    Public Property [Module] As VieModule
End Class

<KnownType(GetType(VieModule))>
<KnownType(GetType(VieGroup))>
<DataContract()>
Public Class VieModuleGroup
    Implements IVieXml
    <DataMember()>
    Public Property IdModule As Integer
    <DataMember()>
    Public Property IdGroup As Integer

    <Xml.Serialization.XmlIgnore()>
    Public Property [Module] As VieModule
    <Xml.Serialization.XmlIgnore()>
    Public Property Group As VieGroup
End Class

<KnownType(GetType(VieForm))>
<DataContract()>
Public Class ViePermission
    Implements IVieXml
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property Name As String

    <Xml.Serialization.XmlIgnore()>
    Public Property Value As Boolean
    <Xml.Serialization.XmlIgnore()>
    Public Property Forms As List(Of VieForm)
End Class

<KnownType(GetType(VieModule))>
<DataContract()>
Public Class VieGroup
    Implements IVieXml
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property Visible As Boolean
    <DataMember()>
    Public Property IdSuite As String
    <DataMember()>
    Public Property NameSuite As String

    <Xml.Serialization.XmlIgnore()>
    Public Property Modules As List(Of VieModule)
End Class

<KnownType(GetType(VieModule))>
<KnownType(GetType(ViePermission))>
<DataContract(IsReference:=True)>
Public Class VieForm
    Implements IVieXml

    Sub New()
        Me.Permissions = New List(Of ViePermission)
    End Sub

    <DataMember()>
    Public Property Id As String
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property Type As Integer
    <DataMember()>
    Public Property IdModuleSource As Integer
    <DataMember()>
    Public Property PrintEvents As String
    <DataMember()>
    Public Property GroupForms As String
    <DataMember()>
    Public Property HasSequence As Boolean
    <DataMember()>
    Public Property IsNativeForm As Boolean
    <DataMember()>
    Public Property HasForm As Boolean
    <DataMember()>
    Public Property IsFoundational As Boolean
    <DataMember()>
    Public Property ClassName As String
    <DataMember()>
    Public Property AssemblyName As String
    <DataMember()>
    Public Property HandlesMassiveConfirm As Boolean
    <DataMember()>
    Public Property Order As Byte
    <DataMember()>
    Public Property TypeName As String
    <DataMember()>
    Public Property TypeOrder As Byte

    <DataMember()>
    Public Property SequenceModule As String

    <Xml.Serialization.XmlIgnore()>
    <DataMember()>
    Public Property [Module] As VieModule
    <Xml.Serialization.XmlIgnore()>
    Public Property [Modules] As List(Of VieModule)
    <Xml.Serialization.XmlIgnore()>
    <DataMember()>
    Public Property Permissions As List(Of ViePermission)
End Class

<KnownType(GetType(VieGroup))>
<KnownType(GetType(VieForm))>
<DataContract()>
Public Class VieModule
    Implements IVieXml
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property Description As String
    <DataMember()>
    Public Property ProductCatalogId As String

    <Xml.Serialization.XmlIgnore()>
    Public Property Group As VieGroup
    <Xml.Serialization.XmlIgnore()>
    Public Property Forms As List(Of VieForm)
End Class

<DataContract(IsReference:=True)>
Public Class VieMonths
    Implements IVieXml
    <DataMember()>
    Public Property Code As String
    <DataMember()>
    Public Property Name As String
End Class

<DataContract(IsReference:=True)>
Public Class VieWoeidCities
    Implements IVieXml
    <DataMember()>
    Public Property City As String
    <DataMember()>
    Public Property WOEID As String
End Class

<DataContract()>
Public Class VieTitle
    Implements IVieXml
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property Order As Byte
End Class

#End Region