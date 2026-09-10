Imports System.Runtime.Serialization

Public Class PharmaceuticalDispensingDevolutionDetail
    <DataMember>
    Property CodePharmaceuticalDispensing As String

    <DataMember>
    Property PharmaceuticalDispensingDetailId As Integer

    <DataMember>
    Property ProductId As Integer

    <DataMember>
    Property CodeNameProduct As String

    <DataMember()>
    Public Property OrderedHealthProfessionalCode As String

    <DataMember>
    Public Property CodeProduct As String

    <DataMember>
    Public Property HCDEVMEDDId As Integer?

    Public ReadOnly Property ChangeTrackerState As Int32
        Get
            Return CInt(Me.ChangeTracker.State)
        End Get
    End Property
End Class
