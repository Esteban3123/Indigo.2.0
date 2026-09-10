'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 09-12-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCUNITHISRepository
    Inherits IRepository(Of HCUNITHIS)

    Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS)

    Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS

End Interface