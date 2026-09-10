'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 07-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract>
Public Interface IPayrollKindsAgreements

    ''' <summary>
    ''' Funcion para cargar una clase de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetKindsAgreements(ByVal code As String, ByVal session As SessionValues) As KindsAgreements

    ''' <summary>
    ''' Funcion para guardar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Objeto clase de convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SaveKindsAgreements(ByVal KindsAgreements As KindsAgreements, ByVal session As SessionValues) As ActionResult(Of KindsAgreements)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Obj. clase de convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteKindsAgreements(ByVal KindsAgreements As KindsAgreements, ByVal session As SessionValues) As ActionMessageResult(Of KindsAgreements)

    ''' <summary>
    ''' Lista las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListKindsAgreements(ByVal session As SessionValues) As List(Of KindsAgreements)

End Interface
