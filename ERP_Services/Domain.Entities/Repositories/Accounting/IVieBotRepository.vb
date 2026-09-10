'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IVieBotRepository
    Inherits IRepository(Of VieBot)

    ''' <summary>
    ''' Lista la configuración de VieBot por formulario
    ''' </summary>
    ''' <param name="Form">Form</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetVieBotByForm(ByVal Form As String, Optional tracking As Boolean = True) As List(Of VieBot)

End Interface