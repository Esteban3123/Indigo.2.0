'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCPARPACSRepository
    Inherits IRepository(Of HCPARPACS)

    Function GetHCPARPACSByCentAten(codCentAten As String) As HCPARPACS

End Interface
