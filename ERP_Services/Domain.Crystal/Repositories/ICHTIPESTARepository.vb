'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface ICHTIPESTARepository
    Inherits IRepository(Of CHTIPESTA)

    Function GetCHTIPESTAByCode(code As String) As CHTIPESTA

End Interface