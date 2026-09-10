'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCQXEQUIPRepository
    Inherits IRepository(Of HCQXEQUIP)

    Function GetHCQXEQUIPByAdmissionNumberAndNumefolio(admissionNumber As String, numefolio As String) As List(Of HCQXEQUIP)

End Interface