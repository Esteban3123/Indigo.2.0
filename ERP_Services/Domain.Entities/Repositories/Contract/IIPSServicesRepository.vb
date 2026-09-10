'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IIPSServicesRepository
    Inherits IRepository(Of IPSService)

    ''' <summary>
    ''' Obtiene un servicio ips
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIPSService(code As String) As IPSService

    ''' <summary>
    ''' Obtiene un servicio ips
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIPSServiceById(id As Integer, Optional tracking As Boolean = True) As IPSService
    ''' <summary>
    ''' obtiene las homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of CupsHomologation)

    ''' <summary>
    ''' Valida el CopyPaste del form de servicios IPS
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteIPSService(xmlObject As String, ServiceManual As Integer) As List(Of SP_CopyAndPasteIPSService_Result)

End Interface
