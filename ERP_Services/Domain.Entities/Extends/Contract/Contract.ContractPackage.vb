Imports System.Runtime.Serialization

Partial Public Class ContractPackage

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre y codigo del CUPS
    ''' </summary>
    <DataMember()>
    Public Property CupsCodeName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la descripcion relacionada
    ''' </summary>
    <DataMember()>
    Public Property ContractDescriptionName As String

    ''' <summary>
    ''' Obtiene o establece el nombre del Servicio IPS
    ''' </summary>
    <DataMember()>
    Public Property IPSServicesName As String


#End Region

End Class
