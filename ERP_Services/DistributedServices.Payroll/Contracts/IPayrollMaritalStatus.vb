'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IPayrollMaritalStatus
    ''' <summary>
    ''' Lista todos los tipos de estado civil
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllMaritalStatus(session As SessionValues) As List(Of MaritalStatus)

    ''' <summary>
    ''' Elimina un tipo de estado civil
    ''' </summary>
    ''' <param name="MaritalStatus">el tipo de estado civil</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage, session As SessionValues) As ActionResult

    ''' <summary>
    ''' graba un tipo de estado civil
    ''' </summary>
    ''' <param name="MaritalStatus">el tipo de estado civil</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta un tipo de estado civil por codigo
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMaritalStatusByCode(ByVal code As String, ByVal audit As AuditMessage, session As SessionValues) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta un tipo de estado civil por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMaritalStatusById(ByVal id As Integer, session As SessionValues) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta de la cultura para el estado civil
    ''' </summary>
    ''' <param name="CultureStatus">Obtiene el estado civil según la cultura</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCultureMaritalStatus(ByVal CultureStatus As String, session As SessionValues)
End Interface
