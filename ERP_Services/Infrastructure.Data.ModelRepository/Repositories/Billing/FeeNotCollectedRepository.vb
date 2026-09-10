'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepositiry
' Author           : Giovanny Plazas Lozano
' Created          : 09-03-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class FeeNotCollectedRepository
    Inherits GenericRepository(Of FeeNotCollected)
    Implements IFeeNotCollectedRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub



End Class
