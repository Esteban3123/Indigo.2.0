'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IDCIRepository
    Inherits IRepository(Of DCI)

    ''' <summary>
    ''' Obtiene un DCI por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDCI(code As String) As DCI

    ''' <summary>
    ''' Obtiene un DCI por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDCIById(id As Integer) As DCI

    ''' <summary>
    ''' Función para Almacenar los DCI's en VIE y CRYSTAL
    ''' </summary>
    ''' <param name="DCIXml"></param>
    ''' <param name="ListDeleteMedicaments"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SaveDCI(DCIXml As String, ListDeleteMedicaments As List(Of String), ListDeleteDrugActive As List(Of String), ListDeleteLethalDoseLimits As List(Of String), ListDeleteRisksDescription As List(Of String), ListDeleteDCIRiskFactors As List(Of String), OperatingUnitId As Integer, CodeUser As String) As SP_SaveDCI_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteDCI(Id As Integer) As SP_DeleteDCI_Result

End Interface
