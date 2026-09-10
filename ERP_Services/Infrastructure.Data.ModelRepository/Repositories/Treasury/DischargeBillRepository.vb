'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DischargeBillRepository
    Inherits GenericRepository(Of DischargeBill)
    Implements IDischargeBillRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetDischargeBillById(Id As Integer) As DischargeBill Implements IDischargeBillRepository.GetDischargeBillById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As DischargeBill In Me._context.DischargeBill Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As DischargeBill In Me._context.DischargeBill.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New DischargeBill()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por id de la cuota de la factura
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdAccountPayableShare</exception>
    Public Function GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare As Integer) As DischargeBill Implements IDischargeBillRepository.GetDischargeBillByIdAccountPayableShare
        If IdAccountPayableShare = 0 Then
            Throw New ArgumentNullException("IdAccountPayableShare")
        End If
        Dim res = (From d As DischargeBill In Me._context.DischargeBill Where d.IdAccountPayableShare = IdAccountPayableShare Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As DischargeBill In Me._context.DischargeBill.AsNoTracking() Where d.IdAccountPayableShare = IdAccountPayableShare Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New DischargeBill()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdVoucherTransactionD</exception>
    Public Function ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD As Integer) As List(Of DischargeBill) Implements IDischargeBillRepository.ListDischargeBillByIdVoucherTransactionD
        If IdVoucherTransactionD = 0 Then
            Throw New ArgumentNullException("IdVoucherTransactionD")
        End If
        Dim res = (From d As DischargeBill In Me._context.DischargeBill Where d.IdVoucherTransactionD = IdVoucherTransactionD Select d).ToList()
        Return res
    End Function

End Class
