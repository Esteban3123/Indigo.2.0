'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IConfigurationServicesAmbulatoryRepository
    Inherits IRepository(Of ConfigurationServicesAmbulatory)

    Function SP_SaveConfigurationServicesAmbulatory(xml As String, userCode As String) As SP_SaveConfigurationServicesAmbulatory_Result

    Function GetConfigurationServicesAmbulatoryById(id As Integer) As ConfigurationServicesAmbulatory

End Interface
