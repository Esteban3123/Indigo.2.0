'***********************************************************************
' Assembly         : Application.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IServiceConfigurationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="serviceConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveServiceConfiguration(ByVal serviceConfiguration As ServiceConfiguration, ByVal audit As AuditMessage) As ActionResult(Of ServiceConfiguration)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetServiceConfigurationById(id As Byte) As ServiceConfiguration

End Interface
