'***********************************************************************
' Assembly         : Infrestructure.Data.GlosasRepository
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Domain.Base
Public Interface IOutBoxRepository
    Inherits IRepository(Of Outbox)

End Interface
