'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPOSPathologiesRepository
    Inherits IRepository(Of POSPathologies)
End Interface
