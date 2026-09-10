'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IAgreementsCRepository
    Inherits IRepository(Of AgreementsC)

    ''' <summary>
    ''' Lista de empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEmployee() As List(Of Employee)

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="tracking">control de segumiento de cambios</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAgreementsC(ByVal consecutive As String, Optional tracking As Boolean = True) As AgreementsC


    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAgreementsC() As List(Of AgreementsC)

    ''' <summary>
    ''' Lista de Convenios por ID del Empleado, Fecha Inicio del Convenio y Concepto
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollDate">Fecha inicio Nómina</param>
    ''' <param name="ConceptId">Id del Concepto</param>
    ''' <returns>Agreements</returns>
    ''' <remarks></remarks>
    Function ListAgreementsCByEmployeeIdStarDate(EmployeeId As String, PayrollDate As Date, ConceptId As String) As List(Of AgreementsC)

    ''' <summary>
    ''' Obtiene los convenios que tenga el empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAgreementsByEmployee(ByVal employeeId As Integer) As List(Of AgreementsC)

    ''' <summary>
    ''' Obtiene los convenios por ID
    ''' </summary>
    ''' <param name="AgreementId">Id del Convenio</param>
    ''' <returns>AgreementsC</returns>
    ''' <remarks></remarks>
    Function GetAgreementsById(AgreementId As Integer) As AgreementsC

    ''' <summary>
    ''' Función creada para que busque el Id del Detalle del Convenio, para insertarlo en el detalle de la Liquidación de Nómina
    ''' </summary>
    ''' <param name="AgreementId">Id de la Cabecera del Convenio</param>
    ''' <param name="PayrollDate">Fecha de Nomina</param>
    ''' <returns>AgreementsD</returns>
    ''' <remarks></remarks>
    Function GetAgreementsDByAgreementsCIdPayrollDate(AgreementId As Integer, PayrollDate As Date) As AgreementsD

    ''' <summary>
    ''' Función que trae un listado de Convenios por Id Empleado y Estado, diseñado solo para que cargue si Afecta Vacaciones
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Function GetAgreementsByEmployeeStatusVacation(employeeId As Integer, Status As Integer, StartDate As Date) As List(Of AgreementsC)

    Function GetAgreementsByEmployeeLiquidationContract(employeeId As Integer, Status As String) As List(Of AgreementsC)

    ''' <summary>
    ''' Guarda un convenio
    ''' </summary>
    ''' <param name="EntityXml"></param>    
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveAgreement(EntityXml As String, codeUser As String) As SP_SaveAgreement_Result


End Interface
