'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 09-12-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAGACTMDDDRepository
    Inherits IRepository(Of AGACTMDDD)

    Function GetAGACTMDDDByCODACTMED(codactmed As String) As List(Of AGACTMDDD)

    Function GetAGACTMDDDByCODACTMEDProduct(codactmed As String) As List(Of AGACTMDDD)

    Function GetAGACTMDDDByCODACTMEDListAndCupsCodeList(listActmedica As List(Of ACTMEDCUPS), IPFECNACI As Date) As List(Of AGACTMDDD)

End Interface