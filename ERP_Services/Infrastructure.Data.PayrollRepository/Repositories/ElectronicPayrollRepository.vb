Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class ElectronicPayrollRepository
    Inherits GenericRepository(Of ElectronicPayroll)
    Implements IElectronicPayrollRepository

#Region "Builder"

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetElectronicPayrollById(Id As Integer, Optional tracking As Boolean = True) As ElectronicPayroll Implements IElectronicPayrollRepository.GetElectronicPayrollById
        Dim res As ElectronicPayroll
        If tracking = True Then
            res = (From a In Me._context.ElectronicPayroll Where a.Id = Id Select a).FirstOrDefault()
        Else
            res = (From a In Me._context.ElectronicPayroll.AsNoTracking() Where a.Id = Id Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicPayroll
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene un documento electrónico por el número de documento
    ''' </summary>
    ''' <param name="documentNumber"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetElectronicPayrollByDocumentNumber(documentNumber As Integer, Optional tracking As Boolean = True) As ElectronicPayroll Implements IElectronicPayrollRepository.GetElectronicPayrollByDocumentNumber
        Dim res As ElectronicPayroll
        If tracking = True Then
            res = (From e In Me._context.ElectronicPayroll Where e.DocumentNumber = documentNumber Select e).FirstOrDefault()
        Else
            res = (From e In Me._context.ElectronicPayroll.AsNoTracking() Where e.DocumentNumber = documentNumber Select e).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicPayroll
        End If
    End Function

#End Region

End Class
