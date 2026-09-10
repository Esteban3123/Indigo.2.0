'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IUnitDoseTypeAdminService
	Inherits IDisposable

	''' <summary>
	''' Lista todos los tipos de dosis unitaria
	''' </summary>
	''' <returns>Lista de tipos de dosis unitaria</returns>
	Function ListAllUnitDoseType(ByVal audit As AuditMessage) As List(Of UnitDoseType)

	''' <summary>
	''' Guarda un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	Function SaveUnitDoseType(ByVal unitDoseType As UnitDoseType, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Elimina un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	Function DeleteUnitDoseType(ByVal unitDoseType As UnitDoseType, ByVal audit As AuditMessage) As ActionResult

	''' <summary>
	''' Actualiza un tipo de dosis unitaria
	''' </summary>
	''' <param name="code">The identifier.</param>
	''' <param name="state">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	Function UpdateStateUnitDoseType(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por codigo
	''' </summary>
	''' <param name="code">The code.</param>
	''' <param name="audit">The identifier.</param>
	''' <returns></returns>
	Function GetUnitDoseType(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	Function GetUnitDoseTypeById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of UnitDoseType)

End Interface