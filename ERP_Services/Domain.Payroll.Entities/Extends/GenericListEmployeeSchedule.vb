Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class GenericListEmployeeSchedule
    <DataMember()>
    Property ListSP_ValidateMassiveEmployeeSchedule As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    <DataMember()>
    Property List_ImportFileRow As List(Of ImportFileRow)
End Class
