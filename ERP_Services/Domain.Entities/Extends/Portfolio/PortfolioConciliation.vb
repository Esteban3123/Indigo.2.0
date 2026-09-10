#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class PortfolioConciliation

    ''' <summary>
    ''' Obtiene o establece el Nombre del Tercero de la Aseguradora
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyName As String

    ''' <summary>
    ''' Obtiene o establece el Nombre del Tercero de la Aseguradora
    ''' </summary>
    <DataMember()>
    Public Property CustomerId As String

End Class