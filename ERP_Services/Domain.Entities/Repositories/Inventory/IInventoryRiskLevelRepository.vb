'************************************************************
' Assembly         : Domain.Inventory.IInventoryRiskLevel
' Author           : John Ortiz
' Created          : 30/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IInventoryRiskLevelRepository
    Inherits IRepository(Of InventoryRiskLevel)

    ''' <summary>
    ''' Metodo para obtener un nivel de riesgo por codigo
    ''' </summary>
    ''' <param name="code">Codigo del nivel de riesgo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRiskLevelByCode(code As String, Optional ByVal tracking As Boolean = True) As InventoryRiskLevel

    ''' <summary>
    ''' Función para almacenar el Nivel del Riesgo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Status"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SaveRiskLevel(Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SaveInventoryRiskLevel_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteRiskLevels(Id As Integer) As SP_DeleteRiskLevels_Result

End Interface
