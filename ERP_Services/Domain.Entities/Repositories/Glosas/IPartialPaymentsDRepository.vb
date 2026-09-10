'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-06-28
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPartialPaymentsDRepository
    Inherits IRepository(Of PartialPaymentsD)

End Interface
