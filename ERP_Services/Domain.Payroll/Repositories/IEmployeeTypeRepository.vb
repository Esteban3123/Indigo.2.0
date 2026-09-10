'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IEmployeeTypeRepository
    Inherits IRepository(Of EmployeeType)

    ''' <summary>
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <returns>Tipo de empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeType(ByVal code As String, Optional ByVal tracking As Boolean = True) As EmployeeType

End Interface
