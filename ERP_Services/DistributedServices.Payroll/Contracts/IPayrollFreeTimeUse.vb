'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollFreeTimeUse

    ''' <summary>
    ''' Elimina una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse">Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteFreeTimeUse(ByVal freeTimeUse As FreeTimeUse, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Almacena una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse">Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFreeTimeUse(ByVal freeTimeUse As FreeTimeUse, session As SessionValues, ByVal idSequense As Int64) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Obtiene una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="code">Código de la Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Actividad de Tiempo Libre</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFreeTimeUse(ByVal code As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Obtiene una Actividad de Tiempo Libre por ID
    ''' </summary>
    ''' <param name="ID">ID de la Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Actividad de Tiempo Libre</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFreeTimeUseById(ByVal ID As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateFreeTimeUse(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of FreeTimeUse)

End Interface
