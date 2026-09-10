'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Andrea Pahola Coqueco Cuellar
' Created          : 27-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CostDistributionDirectCostDetailIvaRepository
    Inherits GenericRepository(Of CostDistributionDirectCostDetailIva)
    Implements ICostDistributionDirectCostDetailIvaRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
