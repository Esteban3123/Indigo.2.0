'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollLanguage

    ''' <summary>
    ''' Lista todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllLanguage(session As SessionValues) As List(Of Language)

    ''' <summary>
    ''' Obtiene un idioma especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetLanguage(ByVal code As String, session As SessionValues) As Language

    ''' <summary>
    ''' Graba o Actualiza un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveLanguage(ByVal language As Language, session As SessionValues, idSequence As Long) As ActionResult(Of Language)

    <OperationContract()>
    Function UpdateStateLanguage(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Language)

    ''' <summary>
    ''' Elimina un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteLanguage(ByVal language As Language, session As SessionValues) As ActionMessageResult(Of Language)

End Interface
