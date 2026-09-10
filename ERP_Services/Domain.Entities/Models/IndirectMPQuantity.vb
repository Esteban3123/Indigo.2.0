Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

''' <summary>
''' Clase para registrar las cantidades de materia prima indirecta
''' </summary>
<DataContract>
Public Class IndirectMPQuantity
    Implements IObjectWithChangeTracker

    <DataMember>
    Public Property CampaignDetailId As Integer

    <DataMember>
    Public Property MovementType As eMovementType

    <DataMember>
    Public Property BatchSerialId As Integer?

    <DataMember>
    Public Property Quantity As Decimal

    <DataMember>
    Public Property MeasurementUnitId As Integer

    <DataMember>
    Public Property Description As String

    <DataMember>
    Public Property ProductId As Integer

    Public Property ChangeTracker As ObjectChangeTracker Implements IObjectWithChangeTracker.ChangeTracker
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As ObjectChangeTracker)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
