'***********************************************************************
' Assembly         : Domain.Security
' Author           : Jorge Leonardo Vernaza
' Created          : 01-08-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region
Public Interface IPermissionCompanyRepository
    Inherits IRepository(Of PermissionCompany)

    ''' <summary>
    ''' Lists the permission company all.
    ''' </summary>
    ''' <returns></returns>
    Function ListPermissionCompanyAll(ByVal UserCode As String) As List(Of PermissionCompany)

    ''' <summary>
    ''' Metodo para saber si el usuario tiene permiso para la empresa seleccionada
    ''' </summary>
    ''' <returns></returns>
    Function GetPermissionUserCompany(ByVal UserCode As String, ByVal containerCode As String) As Boolean

    Function LoginUserCompany(ByVal userCode As String, companyCode As String) As UserLogin

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <param name="idCompany"></param>
    ''' <returns></returns>
    Function LoginUserCompany(ByVal idUser As Integer, ByVal idCompany As Integer) As UserLogin
End Interface
