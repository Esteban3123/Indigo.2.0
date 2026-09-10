'************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/05/2018
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IConsumeServiceRepository
    Inherits IRepository(Of PharmaceuticalDispensing)

    ''' <summary>
    ''' Guarda en las tablas de control para la integración con HEON
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveIntegration(XmlObject As String, UserCode As String) As SP_SaveIntegration_Result

End Interface
