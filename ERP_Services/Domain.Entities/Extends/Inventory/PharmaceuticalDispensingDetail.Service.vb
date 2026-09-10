Imports System.Runtime.Serialization

Partial Public Class PharmaceuticalDispensingDetail

#Region "Properties"

    <DataMember()>
    Public Property CodeProduct As String

    <DataMember()>
    Public Property MedicamentCode As String

    <DataMember()>
    Public Property NameProduct As String

    <DataMember()>
    Public Property CodeNameCareGroup As String

    <DataMember()>
    Public Property CodeNameWareHouse As String

    <DataMember()>
    Public Property CodeNameHealthProfessional As String

    <DataMember()>
    Public Property CodeNameHealthProfessionalSpeciality As String

    <DataMember()>
    Public Property CodeNameCups As String

    <DataMember()>
    Public Property FullNameFunctionalUnit As String

    ''estas propiedades son de las tablas de crystal
    <DataMember()>
    Public Property UnidadMedida As String

    <DataMember()>
    Public Property CantidadPendiente As Integer

    <DataMember()>
    Public Property CantidadSolicitada As Integer

    <DataMember>
    Public Property ProductType As String

    <DataMember()>
    Public Property idProductoHeon As Integer

    <DataMember()>
    Public Property recetarioOMedica As String

    <DataMember()>
    Public Property GuardaGastoQX As Boolean

    <DataMember()>
    Public Property IdProgramacionQXPrincipal As Integer

    <DataMember()>
    Public Property Extramural As Boolean

    <DataMember()>
    Public Property Custody As Boolean

    <DataMember()>
    Public Property QuotationId As Integer

    <DataMember()>
    Public Property AuthorizationOutsourcedServicesId As Integer

    <DataMember()>
    Public Property QuotationCode As String

    <DataMember()>
    Public Property TypeProduct As Integer

    <DataMember()>
    Public Property CantidadMezcla As Integer

    ''' <summary>
    ''' Nota del quimico farmaceutico en la dispensacion de dashboard farmacia
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Note As String

    <DataMember()>
    Public Property CostCenterId As Integer?

    <DataMember()>
    Public Property PackageId As Integer?

    <DataMember>
    Public Property ListDeliveriesByPharmacyProductType As List(Of DeliveriesByPharmacyProductTypeModel)

    Public ReadOnly Property ChangeTrackerState As Int32
        Get
            Return CInt(Me.ChangeTracker.State)
        End Get
    End Property

    <DataMember()>
    Public Property DiagnosticCode As String
    <DataMember()>
    Public Property TreatmentDays As Integer
    <DataMember()>
    Public Property IDMipres As String


#End Region

#Region "Cloneable"

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As PharmaceuticalDispensingDetail
        Dim entity As PharmaceuticalDispensingDetail = DirectCast(MemberwiseClone(), PharmaceuticalDispensingDetail)
        Return entity
    End Function

#End Region

End Class