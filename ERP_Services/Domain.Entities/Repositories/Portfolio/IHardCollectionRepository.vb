'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Rafael Eduardo Patiño cabrera
' Created          : 09-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IHardCollectionRepository
    Inherits IRepository(Of HardCollection)

    Function SetInvoicesHardCollection(xml As String) As Entity.Core.Objects.ObjectResult(Of SP_SetInvoicesHardCollection_Result)

    Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of HardCollectionDetail)

    Function GetHardCollectionById(id As Integer) As HardCollection

    Function GenerateHardCollectionSP(xml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_HardCollection_Result)


    Function GetHardCollectionByCode(code As String) As HardCollection
End Interface
