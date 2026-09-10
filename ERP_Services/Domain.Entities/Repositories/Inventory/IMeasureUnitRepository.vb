'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IMeasureUnitRepository
    Inherits IRepository(Of InventoryMeasurementUnit)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMeasureUnit(code As String) As InventoryMeasurementUnit

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMeasureUnitById(id As Integer) As InventoryMeasurementUnit

    ''' <summary>
    ''' Función que se utiliza para almacenar la Unidad de Medida
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Abbreviation"></param>
    ''' <param name="UnitType"></param>
    ''' <param name="Status"></param>
    ''' <param name="AllowEditCostValue"></param>
    ''' <param name="CostValue"></param>
    ''' <param name="CodeUser"></param>
    ''' <param name="RequiresStandardCode"></param>
    ''' <param name="StandardCode"></param>
    ''' <returns></returns>
    Function SaveMeasurementUnit(Code As String, Description As String, Abbreviation As String, UnitType As Byte, Status As Boolean, AllowEditCostValue As Boolean, CostValue As Decimal, CodeUser As String, RequiresStandardCode As Boolean, StandardCode As String) As SP_SaveMeasureUnit_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteMeasurementUnit(Id As Integer) As SP_DeleteMeasurementUnit_Result

End Interface
