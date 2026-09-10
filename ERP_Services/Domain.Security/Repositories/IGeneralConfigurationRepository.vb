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

Public Interface IGeneralConfigurationRepository
    Function GetGeneralConfiguration() As GeneralConfiguration
End Interface
