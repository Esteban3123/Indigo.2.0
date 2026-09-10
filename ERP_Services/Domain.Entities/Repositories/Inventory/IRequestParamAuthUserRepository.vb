'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-09-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestParamAuthUserRepository
    Inherits IRepository(Of RequestParamAuthUser)
End Interface
