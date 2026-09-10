Imports System.Runtime.Serialization

Public Class DefectClassificationHeaderModel

    <DataMember>
    Public Property Observation As String

    <DataMember>
    Public Property ValidateWeigthNPT As Boolean?

    <DataMember>
    Public Property ActualWeight As Decimal?

    <DataMember>
    Public Property IndicationSize As Integer?
End Class

Public Class DefectClassificationModel
    <DataMember>
    Public Property Id As Integer
    <DataMember>
    Public Property DefectClassificationGroupId As Integer
    <DataMember>
    Public Property DefectClassificationGroupWeight As Short
    <DataMember>
    Public Property DefectClassificationGroupDescription As String
    <DataMember>
    Public Property DefectClassificationItemId As Integer
    <DataMember>
    Public Property DefectClassificationItemWeight As Short
    <DataMember>
    Public Property DefectClassificationItemDescription As String
    <DataMember>
    Public Property RequestPackageDetailStatusDefectClassificationId As Integer?
    <DataMember>
    Public Property RequestPackageDetailStatusId As Integer?
    <DataMember>
    Public Property Critical As Boolean
    <DataMember>
    Public Property Less As Boolean
    <DataMember>
    Public Property Production As Boolean?
    <DataMember>
    Public Property Quality As Boolean?
    <DataMember>
    Public Property TypeName As String
    <DataMember>
    Public Property Observation As String
    <DataMember>
    Public Property CreatedAt As Date?
    Public Property UnitDoseClassAllowed As String
    <DataMember>
    Public Property Quantity As Integer
End Class
