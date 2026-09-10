'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IBillingReversalReasonRepository
    Inherits IRepository(Of Domain.Entities.BillingReversalReason)

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetReversalReason(code As String, tracking As Boolean) As Domain.Entities.BillingReversalReason

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetReversalReasonById(id As Integer, tracking As Boolean) As Domain.Entities.BillingReversalReason

End Interface
