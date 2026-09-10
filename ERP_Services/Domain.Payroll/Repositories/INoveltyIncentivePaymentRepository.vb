'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface INoveltyIncentivePaymentRepository

    Inherits IRepository(Of NoveltyIncentivePayment)

    ''' <summary>
    ''' Función para traer una Novedad de Primas
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="Status">Estado</param>
    ''' <returns>NoveltyIncentivePayment</returns>
    ''' <remarks></remarks>
    Function GetNoveltyIncentivePaymentByEmployeeIdStatus(ByVal EmployeeId As Integer, Status As Byte) As List(Of NoveltyIncentivePayment)

End Interface
