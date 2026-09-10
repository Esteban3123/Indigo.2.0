'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class TreasuryAdvanceRepository
    Inherits GenericRepository(Of TreasuryAdvances)
    Implements ITreasuryAdvanceRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetTreasuryAdvanceById(Id As Integer) As TreasuryAdvances Implements ITreasuryAdvanceRepository.GetTreasuryAdvanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As TreasuryAdvances In Me._context.TreasuryAdvances Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As TreasuryAdvances In Me._context.TreasuryAdvances.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New TreasuryAdvances()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdVoucherTransactionDetail</exception>
    Public Function GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail As Integer) As TreasuryAdvances Implements ITreasuryAdvanceRepository.GetTreasuryAdvanceByIdVoucherTransactionDetail
        If IdVoucherTransactionDetail = 0 Then
            Throw New ArgumentNullException("IdVoucherTransactionDetail")
        End If
        Dim res = (From d As TreasuryAdvances In Me._context.TreasuryAdvances Where d.IdVoucherTransactionDetail = IdVoucherTransactionDetail Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As TreasuryAdvances In Me._context.TreasuryAdvances.AsNoTracking() Where d.IdVoucherTransactionDetail = IdVoucherTransactionDetail Select d).FirstOrDefault()
            Return res
        Else
            Return New TreasuryAdvances()
        End If
    End Function

End Class
