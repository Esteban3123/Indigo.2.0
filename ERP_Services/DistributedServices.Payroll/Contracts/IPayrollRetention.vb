'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollRetention

    ''' <summary>
    ''' Lista Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllRetention(session As SessionValues) As List(Of Retention)

    ''' <summary>
    ''' Elimina Retenciones
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteRetention(ByVal retention As Retention, session As SessionValues) As ActionMessageResult(Of Retention)

    ''' <summary>
    ''' Almacena o Actualiza Retenciones
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveRetention(ByVal retention As Retention, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns>Retención</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetRetention(ByVal code As String, session As SessionValues) As Retention

End Interface
