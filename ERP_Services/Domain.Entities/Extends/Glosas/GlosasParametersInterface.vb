Imports System.Runtime.Serialization

Partial Public Class GlosasParametersInterface
#Region "Manual Properties"



    Private _ParametersInterfaceCodeName As String
    <DataMember()>
    Property ParametersInterfaceCodeName As String
        Get
            Return _ParametersInterfaceCodeName
        End Get
        Set(value As String)
            _ParametersInterfaceCodeName = value
        End Set
    End Property


#End Region
End Class
