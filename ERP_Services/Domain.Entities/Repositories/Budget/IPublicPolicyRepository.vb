'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2022-01-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPublicPolicyRepository
    Inherits IRepository(Of PublicPolicy)

End Interface
