'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Juan Diego Díaz
' Created          : 31-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollSportPractice

    ''' <summary>
    ''' Elimina una Practica Deportiva
    ''' </summary>
    ''' <param name="sportPractice">Practica Deportiva</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteSportPractice(ByVal sportPractice As SportPractice, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Almacena una Practica Deportiva
    ''' </summary>
    ''' <param name="sportPractice">Practica Deportiva</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveSportPractice(ByVal sportPractice As SportPractice, session As SessionValues, ByVal idSequense As Int64) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Obtiene una Practica Deportiva
    ''' </summary>
    ''' <param name="code">Código de la Practica Deportiva</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSportPractice(ByVal code As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Obtiene una Practica Deportiva por ID
    ''' </summary>
    ''' <param name="ID">ID de la Practica Deportiva</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSportPracticeById(ByVal ID As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateSportPractice(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of SportPractice)

End Interface
