'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollPensionaryType

    ''' <summary>
    ''' Lista Todos los Tipos de Pensionados
    ''' </summary>
    ''' <returns>Lista de Tipos de Pensionados</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllPensionaryType(session As SessionValues) As List(Of PensionaryType)

    ''' <summary>
    ''' Obtiene un Tipo de Pensionado en Específico
    ''' </summary>
    ''' <param name="code">Código del Tipo de Pensionado</param>
    ''' <returns>Tipo de Pensionado</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPensionaryType(ByVal code As String, session As SessionValues) As PensionaryType

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Pensionados
    ''' </summary>
    ''' <param name="pensionaryType">Tipo de Pensionado</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SavePensionaryType(ByVal pensionaryType As PensionaryType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un Tipo de Pensionado
    ''' </summary>
    ''' <param name="pensionaryType">Tipo de Pensionado</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeletePensionaryType(ByVal pensionaryType As PensionaryType, session As SessionValues) As ActionMessageResult(Of PensionaryType)

End Interface
