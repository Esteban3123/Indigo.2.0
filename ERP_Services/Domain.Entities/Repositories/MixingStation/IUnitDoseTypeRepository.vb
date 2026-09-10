'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 20-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IUnitDoseTypeRepository
	Inherits IRepository(Of UnitDoseType)

	''' <summary>
	''' Obtiene todos los tipos de dosis unitarias
	''' </summary>
	''' <returns>Lista de tipos de dosis unitarias</returns>
	''' <remarks></remarks>
	Function ListAllUnitDoseType() As List(Of UnitDoseType)

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por codigo
	''' </summary>
	''' <param name="code">The code.</param>
	''' <returns></returns>
	Function GetUnitDoseType(code As String, Optional tracking As Boolean = True) As UnitDoseType

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <returns></returns>
	Function GetUnitDoseTypeById(id As String, Optional tracking As Boolean = True) As UnitDoseType
End Interface
