Imports System.Runtime.Serialization

''' <summary>
''' Clase utilizada en el FrmLogin para Listar las empresas
''' </summary>
''' <remarks></remarks>
<DataContract(IsReference:=True)>
Public Class Company

    <DataMember()>
    Property Id As Integer
    <DataMember()>
    Property Code As String
    <DataMember()>
    Property Name As String
    <DataMember()>
    Property FoundationalContainer As String
    <DataMember()>
    Property TransactionalContainer As String
    <DataMember()>
    Property DocumentalContainer As String
    <DataMember()>
    Property VituelContainer As String
    <DataMember()>
    Property HISContainer As String
    <DataMember()>
    Property HISIntegration As Byte
    <DataMember()>
    Property GlossesIntegration As Byte
    <DataMember()>
    Property PayrollIntegration As Byte
    <DataMember()>
    Property HumanTalentIntegration As Byte
    <DataMember()>
    Property DispensingIntegration As Byte
    <DataMember()>
    Property CompanyType As Byte
    <DataMember()>
    Property CompanyNit As String
    <DataMember()>
    Property Address As String
    <DataMember()>
    Property Telephone As String
    <DataMember()>
    Property ProductionCompany As Boolean
    <DataMember()>
    Property State As Boolean
    <DataMember()>
    Property IndigoConnectionString As String
    <DataMember()>
    Property SecurityContainer As String
    <DataMember()>
    Property InteropCostContainer As String
    <DataMember()>
    Public Property IdOperatingUnitDefault As Integer
    <DataMember()>
    Public Property Version As String
    <DataMember()>
    Public Property VerificationDigitNit As String
    <DataMember()>
    Public Property TenantId As Short
    <DataMember()>
    Public Property ServiceConfigurationId As Byte
    <DataMember()>
    Public Property Administrator As Boolean
    <DataMember()>
    Public Property RollId As Integer
    <DataMember()>
    Public Property RollCode As String
    <DataMember()>
    Public Property RollName As String
    <DataMember()>
    Public Property GroupId As Integer
    <DataMember()>
    Public Property GroupCode As String
    <DataMember()>
    Public Property GroupName As String
    <DataMember()>
    Public Property TenantName As String
    <DataMember()>
    Public Property Environment As String
    <DataMember()>
    Public Property Flagcode As String
    <DataMember()>
    Public Property CountryName As String
    <DataMember()>
    Public Property ArchitectureType As Byte
    <DataMember()>
    Public Property DecimalSeparator As String
	<DataMember>
	Public Property ServiceConfiguration As ServiceConfiguration

	Private _clientIdGuid As Guid

	''' <summary>
	''' Propiedad Guid para ClientId
	''' </summary>
	<DataMember>
	Public Property ClientIdGuid As Guid
		Get
			Return _clientIdGuid
		End Get
		Set(value As Guid)
			_clientIdGuid = value
			ClientId = _clientIdGuid.ToString()
		End Set
	End Property

	''' <summary>
	''' Propiedad que devuelve el ClientId como cadena.
	''' </summary>
	<DataMember>
	Public Property ClientId As String

End Class
