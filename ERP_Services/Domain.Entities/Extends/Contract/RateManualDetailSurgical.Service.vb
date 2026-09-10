Imports System.Runtime.Serialization

Partial Public Class RateManualDetailSurgical

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del servicio ips
    ''' </summary>
    <DataMember()>
    Public Property IPSServiceDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del grupo quirurgico
    ''' </summary>
    <DataMember()>
    Public Property SurgicalGroupDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del rango uvr
    ''' </summary>
    <DataMember()>
    Public Property UVRRangeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del ingreso ambulatorio
    ''' </summary>
    <DataMember()>
    Public Property OutPatientRecoveryFeeTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del ingreso hospitalario
    ''' </summary>
    <DataMember()>
    Public Property InPatientRecoveryFeeTypeDescription As String

#End Region

End Class
