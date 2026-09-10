Imports System.Runtime.Serialization

<DataContract>
Public Class AnnulateUnitDoseModel
    <DataMember>
    Public Property GroupingCodeDose As Guid
    <DataMember>
    Public Property CodeSusceptibleMixingStation As Guid
    <DataMember>
    Public Property Quantity As Integer
End Class
