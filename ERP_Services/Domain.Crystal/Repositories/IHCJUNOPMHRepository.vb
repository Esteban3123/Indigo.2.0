'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCJUNOPMHRepository
    Inherits IRepository(Of HCJUNOPMH)

    Function GetHCJUNOPMHByCODPRONOPAndNUMINGRES(CODPRONOPList As List(Of String), AdmissionCode As String) As List(Of HCJUNOPMH)

    Function GetHCJUNOPMH(ListProductATC As List(Of ProductATC), AdmissionCode As String) As List(Of ProductATC)

End Interface