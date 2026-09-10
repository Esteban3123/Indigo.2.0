'***********************************************************************
' Assembly         : Application.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities

#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IGeneralConfigurationAdminService
    Inherits IDisposable

    Function GetGeneralConfiguration() As GeneralConfiguration
End Interface
