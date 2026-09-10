Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PayrollSettings

	<DataMember()>
	Property NameThirdPartyPayrollSettings As String

	''' <summary>
	''' Nos permite saber si la propiedad de moneda puede editarse siempre y cuando no existan liquidaciones previas
	''' </summary>
	<DataMember()>
	Public Property CurrencyFieldEnabled As Boolean

End Class