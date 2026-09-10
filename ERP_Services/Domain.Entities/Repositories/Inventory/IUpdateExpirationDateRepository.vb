'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 215-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IUpdateExpirationDateRepository
    Inherits IRepository(Of UpdateExpirationDate)

End Interface
