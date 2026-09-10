'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 30-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPayrollParameterRepository

    Inherits IRepository(Of PayrollParameter)

    ''' <summary>
    ''' Lista todas las Parametrizaciones de Nómina
    ''' </summary>
    ''' <returns>Lista de Parámetros de Nómina</returns>
    ''' <remarks></remarks>
    Function ListAllPayrollParameter() As List(Of PayrollParameter)

    ''' <summary>
    ''' Obtiene los Parametrizaciones de Nómina de un Grupo
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <returns>Parametrización de Nómina de un Grupo</returns>
    ''' <remarks></remarks>
    Function GetPayrollParameter(ByVal groupId As String) As PayrollParameter

End Interface
