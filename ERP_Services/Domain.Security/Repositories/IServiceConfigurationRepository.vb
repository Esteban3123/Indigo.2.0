'***********************************************************************
' Assembly         : Domain.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
    Imports Domain.Base
#End Region

Public Interface IServiceConfigurationRepository
    Inherits IRepository(Of ServiceConfiguration)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetServiceConfigurationById(id As Byte) As ServiceConfiguration
End Interface
