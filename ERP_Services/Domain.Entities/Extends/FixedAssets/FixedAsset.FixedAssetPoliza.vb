#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetPolicy

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la aseguradora
    ''' </summary>
    <DataMember()>
    Public Property CodeNameInsurance As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del tipo de poliza
    ''' </summary>
    <DataMember()>
    Public Property CodeNamePolizaType As String

End Class
