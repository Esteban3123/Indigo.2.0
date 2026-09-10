'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollJobBondingType

    ''' <summary>
    ''' Lista Todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllJobBondingType(session As SessionValues) As List(Of JobBondingType)

    ''' <summary>
    ''' Elimina un Tipo de Vinculación de Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteJobBondingType(ByVal jobBondingType As JobBondingType, session As SessionValues) As ActionMessageResult(Of JobBondingType)

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Vinculación
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveJobBondingType(ByVal jobBondingType As JobBondingType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código del Tipo de Vinculación Laboral</param>
    ''' <returns>Tipo de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetJobBondingType(ByVal code As String, session As SessionValues) As JobBondingType

End Interface
