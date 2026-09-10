'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ITreasuryAdvanceRepository
    Inherits IRepository(Of TreasuryAdvances)

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetTreasuryAdvanceById(ByVal Id As Integer) As TreasuryAdvances

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    Function GetTreasuryAdvanceByIdVoucherTransactionDetail(ByVal IdVoucherTransactionDetail As Integer) As TreasuryAdvances

End Interface
