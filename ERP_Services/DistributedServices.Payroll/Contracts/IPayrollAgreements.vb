'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract>
Public Interface IPayrollAgreements

    ''' <summary>
    ''' Lista de empleados
    ''' </summary>
    ''' <param name="audit">Objeto Inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListEmployeeAgreements(ByVal session As SessionValues) As List(Of Employee)

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetAgreementsC(ByVal consecutive As String, ByVal session As SessionValues) As AgreementsC

    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListAgreementsC(ByVal session As SessionValues) As List(Of AgreementsC)

    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SaveAgreementsC(ByVal AgreementsC As AgreementsC, ByVal session As SessionValues) As ActionResult(Of AgreementsC)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteAgreementsC(ByVal AgreementsC As AgreementsC, ByVal session As SessionValues) As ActionResult
End Interface
