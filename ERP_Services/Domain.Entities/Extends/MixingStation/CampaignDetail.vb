Imports System.Runtime.Serialization

Partial Public Class CampaignDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene la clase del tipo de dosis para la campaña
    ''' </summary>
    <DataMember()>
    Public Property MSClass As Integer

    ''' <summary>
    ''' Obtiene la entidad que esta creando el objeto
    ''' </summary>
    <DataMember()>
    Public Property Entity As String

#End Region

End Class
