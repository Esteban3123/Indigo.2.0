'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class CostEstimationRepository
    Inherits GenericRepository(Of CostEstimation)
    Implements ICostEstimationRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class