'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ISettingsContractRepository
    Inherits IRepository(Of SettingsContract)

    ''' <summary>
    ''' Obtiene los parámetros por unidad operativa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer, Optional tracker As Boolean = True) As SettingsContract

    ''' <summary>
    ''' Valida que si se cambia el parametro de descripciones a no no haya items registrados en los cups en estado activo
    ''' </summary>
    ''' <returns></returns>
    Function ValidationDescriptions() As Integer

End Interface
