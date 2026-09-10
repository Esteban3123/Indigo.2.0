Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class GenericListNovelty
    <DataMember()>
    Property ListSP_ValidateMassiveNovelty As List(Of SP_ValidateMassiveNovelties_Result)

    <DataMember()>
    Property List_ImportFileRow As List(Of ImportFileRow)
End Class
