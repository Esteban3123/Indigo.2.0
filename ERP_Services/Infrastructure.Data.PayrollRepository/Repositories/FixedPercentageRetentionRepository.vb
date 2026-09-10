' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-07-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class FixedPercentageRetentionRepository

    Inherits GenericRepository(Of FixedPercentageRetention)
    Implements IFixedPercentageRetentionRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene el FixedPercentageRetention
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="ValidStarDate">ValidStarDate</param>
    ''' <param name="ValidEndDate">ValidEndDate</param>
    ''' <returns>FixedPercentageRetention</returns>
    Public Function GetFixedPercentageRetentionByEmployeeDate(EmployeeId As Integer, ValidStarDate As Date, ValidEndDate As Date) As FixedPercentageRetention Implements IFixedPercentageRetentionRepository.GetFixedPercentageRetentionByEmployeeDate

        Dim FixedPercentageRetention = From e In _context.FixedPercentageRetention
       Where e.EmployeeId = EmployeeId And e.ValidStartDate = ValidStarDate And e.ValidEndDate = ValidEndDate

        If FixedPercentageRetention.Count > 0 Then
            Return FixedPercentageRetention.FirstOrDefault()
        Else
            Return Nothing
        End If

    End Function
End Class
