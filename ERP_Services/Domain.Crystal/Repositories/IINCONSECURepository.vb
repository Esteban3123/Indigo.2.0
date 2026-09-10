'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IINCONSECURepository
    Inherits IRepository(Of INCONSECU)

    Function GetINCONSECUByID(id As String) As INCONSECU

End Interface