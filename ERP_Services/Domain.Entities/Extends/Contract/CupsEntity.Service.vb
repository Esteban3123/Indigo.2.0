Imports System.Runtime.Serialization

Partial Public Class CUPSEntity

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del subgrupo cups
    ''' </summary>
    <DataMember()>
    Public Property CupsSubGroupDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del grupo de servicio ips
    ''' </summary>
    <DataMember()>
    Public Property IPSServiceGroupDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del grupo de facturacion
    ''' </summary>
    <DataMember()>
    Public Property BillingGroupDescription As String

    <DataMember()>
    Public Property ListHomologation As List(Of HomologationISSSOAT)

    <DataMember()>
    Public Property ListCupsEntityRIAS As New List(Of CupsEntityRIAS)

    <DataMember()>
    Public Property RIASBillingConceptCodeName As String

    <DataMember()>
    Public Property RIASBillingGroupCodeName As String

#End Region

End Class

Public Class HomologationISSSOAT
    Public Property HomologationSIPS As String

    Public Property HomologationManual As String
End Class

Public Class CupsEntityRIAS

    Public Property Id As Integer

    Public Property CupsCode As String

    Public Property RiasId As Integer

    Public Property RiasDescription As String

    Public Property ConceptRIPS As String

    Public Property IsDelete As Boolean

    Public Property ListCupsEntityRIASDetail As New List(Of CupsEntityRIASDetail)

End Class

Public Class CupsEntityRIASDetail

    Public Property Id As Integer

    Public Property CupsEntityRIASId As Integer

    Public Property Rule As Integer

    Public Property MinimunAge As Integer

    Public Property MaximunAge As Integer

    Public Property Unit As Integer

    Public Property Frequency As Integer

    Public Property FrequencyUnit As Integer

    Public Property RequireMedicalOrder As Boolean

    Public Property IsDelete As Boolean

    Public Property PeriodQuantity As Integer

    Public Property MinimunAgeDays As Integer

    Public Property MaximunAgeDays As Integer

    Public Property Sex As Integer

    Public Property Status As Boolean

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CupsEntityRIASDetail
        Dim entity As CupsEntityRIASDetail = DirectCast(MemberwiseClone(), CupsEntityRIASDetail)
        Return entity
    End Function

End Class
