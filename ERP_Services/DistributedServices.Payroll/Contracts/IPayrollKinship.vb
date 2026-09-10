'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollKinship

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllKinship(session As SessionValues) As List(Of Kinship)

    ''' <summary>
    ''' Obtiene un parentesco en especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetKinship(ByVal code As String, session As SessionValues) As Kinship

    ''' <summary>
    ''' Graba o actualiza un parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveKinship(ByVal kinship As Kinship, session As SessionValues) As Boolean

    ''' <summary>
    ''' elimina un objeto parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteKinship(ByVal kinship As Kinship, session As SessionValues) As ActionMessageResult(Of Kinship)

End Interface
