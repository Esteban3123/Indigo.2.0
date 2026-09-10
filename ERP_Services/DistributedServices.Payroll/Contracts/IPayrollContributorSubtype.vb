'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

#End Region

<ServiceContract()>
Public Interface IPayrollContributorSubtype


    ''' <summary>
    ''' Lista todos los subtipos de cotizantes.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllContributorSubtype(ByVal session As SessionValues) As List(Of ContributorSubtype)

    ''' <summary>
    ''' Elimina un subtipo de cotizante
    ''' </summary>
    ''' <param name="ContributorSubtype">el subtipo de cotizante</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, session As SessionValues) As ActionResult

    ''' <summary>
    ''' graba un subtipo de cotizante
    ''' </summary>
    ''' <param name="ContributorSubtype">el subtipo de cotizante</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of ContributorSubtype)

    ''' <summary>
    ''' Consulta un subtipo de cotizante por codigo
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContributorSubtypeByCode(ByVal code As String, ByVal audit As AuditMessage, session As SessionValues) As ActionResult(Of ContributorSubtype)

    ''' <summary>
    ''' Consulta un subtipo de cotizante por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContributorSubtypeById(ByVal id As Integer, session As SessionValues) As ActionResult(Of ContributorSubtype)
End Interface
