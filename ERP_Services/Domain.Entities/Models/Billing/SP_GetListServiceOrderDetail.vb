Imports System.Runtime.Serialization

<DataContract>
Public Class SP_GetListServiceOrderDetail

	<DataMember>
	Public Property Id As Integer

	<DataMember>
	Public Property CodeNameIpsService As String

	<DataMember>
	Public Property CodeNameCareGroup As String

	<DataMember>
	Public Property RecordType As Integer

	<DataMember>
	Public Property Packaging As Boolean

	<DataMember>
	Public Property InvoicedQuantity As Decimal

	<DataMember>
	Public Property AdmissionNumber As String

	<DataMember>
	Public Property Status As Integer

End Class