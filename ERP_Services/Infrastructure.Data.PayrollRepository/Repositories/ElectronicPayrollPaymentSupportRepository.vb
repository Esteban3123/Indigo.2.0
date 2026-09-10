Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class ElectronicPayrollPaymentSupportRepository
    Inherits GenericRepository(Of ElectronicPayrollPaymentSupport)
    Implements IElectronicPayrollPaymentSupportRepository

#Region "Builder"

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetElectronicPayrollPaymentSupportById(Id As Integer, Optional tracking As Boolean = True) As ElectronicPayrollPaymentSupport Implements IElectronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportById
        Dim res As ElectronicPayrollPaymentSupport
        If tracking = True Then
            res = (From a In Me._context.ElectronicPayrollPaymentSupport Where a.Id = Id Select a).FirstOrDefault()
        Else
            res = (From a In Me._context.ElectronicPayrollPaymentSupport.AsNoTracking() Where a.Id = Id Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicPayrollPaymentSupport
        End If
    End Function

    Public Function GetElectronicPayrollPaymentSupportByThirdPartyAndPeriod(thirdPartyId As Integer, year As Integer, month As Byte) As ElectronicPayrollPaymentSupport Implements IElectronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportByThirdPartyAndPeriod
        Return (From epps In _context.ElectronicPayrollPaymentSupport Where epps.EmployeePartyId = thirdPartyId AndAlso epps.Year = year AndAlso epps.Month = month).FirstOrDefault
    End Function

    Public Function GetElectronicPayrollPaymentSupport(id As Integer) As SP_GetElectronicPayrollPaymentSupport_Result Implements IElectronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupport
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetElectronicPayrollPaymentSupport(id).FirstOrDefault()
    End Function

    Public Function GetElectronicPayrollPaymentSupportDetails(id As Integer) As List(Of SP_GetElectronicPayrollPaymentSupportDetails_Result) Implements IElectronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetElectronicPayrollPaymentSupportDetails(id).ToList()
    End Function

#End Region

End Class
