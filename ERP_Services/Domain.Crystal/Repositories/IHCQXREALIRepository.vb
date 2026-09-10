'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCQXREALIRepository
    Inherits IRepository(Of HCQXREALI)

    Function GetHCQXREALIByAdmissionNumberAndNumeFolioAndCodserips(admissionNumber As String, numefolio As String, codserips As String, Optional CONSECUQX As Integer? = Nothing) As HCQXREALI

End Interface