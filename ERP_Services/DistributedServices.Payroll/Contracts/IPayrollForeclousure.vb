'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 12-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract>
Public Interface IPayrollForeclousure

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetForeclousure(ByVal consecutive As String, ByVal session As SessionValues) As Foreclousure

    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListForeclosure(ByVal session As SessionValues) As List(Of Foreclousure)

    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SaveForeclousure(ByVal Foreclousure As Foreclousure, idSequense As Int64, ByVal session As SessionValues) As ActionResult(Of Foreclousure)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteForeclousure(ByVal Foreclousure As Foreclousure, ByVal session As SessionValues) As ActionResult
End Interface
