'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IAuthorizationOutsourcedServicesRepository
    Inherits IRepository(Of AuthorizationOutsourcedServices)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationOutsourcedServices(Code As String) As AuthorizationOutsourcedServices

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationOutsourcedServicesById(Id As Integer) As AuthorizationOutsourcedServices

    ''' <summary>
    ''' Proceso de autorización servicios tercerizados
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveAuthorizationOutsourcedServices(xmlData As String, codeUser As String) As SP_SaveAuthorizationOutsourcedServices_Result

End Interface
