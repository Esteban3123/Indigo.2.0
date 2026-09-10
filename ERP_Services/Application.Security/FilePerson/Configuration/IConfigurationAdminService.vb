'***********************************************************************
' Assembly         : Application.Security
' Author           : Juan F. Tamayo
' Created          : 2013-12-11
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-11
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities

#End Region

Public Interface IConfigurationAdminService
    Inherits IDisposable

#Region "Config"

    ''' <summary>
    ''' Obtiene la configuración por defecto configurada para todos los clientes
    ''' de la organisación
    ''' </summary>
    ''' <returns></returns>
    Function GetGenesisConfiguration() As DataSet

#End Region

End Interface
