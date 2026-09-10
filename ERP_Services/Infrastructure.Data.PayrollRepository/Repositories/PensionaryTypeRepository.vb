'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class PensionaryTypeRepository

    Inherits GenericRepository(Of PensionaryType)
    Implements IPensionaryTypeRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetPensionaryType(code As String, Optional tracking As Boolean = True) As PensionaryType Implements IPensionaryTypeRepository.GetPensionaryType
        Dim pensionaryType = From e In _context.PensionaryType
                      Where e.Code = code
                      Select e
        If pensionaryType.Count > 0 Then
            Dim auxPensionaryType = Nothing
            If tracking = False Then
                auxPensionaryType = (From e In _context.PensionaryType.AsNoTracking
                                    Where e.Code = code
                                    Select e).SingleOrDefault
            Else
                auxPensionaryType = pensionaryType.SingleOrDefault
            End If
            Return auxPensionaryType
        Else
            Return New PensionaryType()
        End If
    End Function

    Public Function ListAllPensionaryType() As List(Of PensionaryType) Implements IPensionaryTypeRepository.ListAllPensionaryType
        Dim pensionaryType = From e In _context.PensionaryType
                     Select e
        Return pensionaryType.ToList()
    End Function
End Class
