'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán
' Created          : 04-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IAccountControlAdminService
    Inherits IDisposable

#Region "Methods"

    Function GetHomologationsCups(parameter As String, careGroupId As Integer) As ActionResult(Of List(Of List(Of CupsHomologation)))

    ''' <summary>
    ''' Genera masivamente ordenes de servicio
    ''' </summary>
    ''' <param name="objParams">The object parameters.</param>
    ''' <param name="homologations">The homologations.</param>
    ''' <returns></returns>
    Function GenerateServiceOrderMassive(objParams As String, homologations As List(Of List(Of CupsHomologation)), audit As AuditMessage) As ActionResult(Of List(Of List(Of CupsHomologation)))

    Function GetServiceOrderDetailHomologation(careGroupId As Integer, listHomologations As List(Of List(Of CupsHomologation)), args As String) As ActionResult(Of ServiceOrder)

    Function GenerateServiceOrderMassiveWithListDetail(parameters As String, listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene los centros de atención
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, Container As String) As ActionResult(Of List(Of SP_ListCareCenterHis_Result))

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, Container As String) As ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result))

    ''' <summary>
    ''' Metodo que obtiene el parametro de autorizacion por tipo de unidad funcional.
    ''' </summary>
    ''' <param name="CareCenterCode"></param>
    ''' <param name="FunctionalUnit"></param>
    ''' <param name="EmpresaDGH"></param>
    ''' <returns></returns>
    Function GetAuthorizationParameterByTUF(CareCenterCode As String, FunctionalUnit As String, EmpresaDGH As String) As ActionResult
#End Region

End Interface
