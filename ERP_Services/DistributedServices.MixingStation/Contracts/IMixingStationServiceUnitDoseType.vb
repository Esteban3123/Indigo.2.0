'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceUnitDoseType
	''' <summary>
	''' Lista todos los tipos de dosis unitaria
	''' </summary>
	''' <returns>Lista de tipos de dosis unitaria</returns>
	<OperationContract()>
	Function ListAllUnitDoseType(audit As AuditMessage) As List(Of UnitDoseType)

	''' <summary>
	''' Guarda un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	<OperationContract()>
	Function SaveUnitDoseType(ByVal unitDoseType As UnitDoseType, idSequence As Int64, audit As AuditMessage) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Elimina un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	<OperationContract()>
	Function DeleteUnitDoseType(ByVal unitDoseType As UnitDoseType, audit As AuditMessage) As ActionResult

	''' <summary>
	''' Actualiza un tipo de dosis unitaria
	''' </summary>
	''' <param name="code">The identifier.</param>
	''' <param name="state">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	<OperationContract()>
	Function UpdateStateUnitDoseType(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por codigo
	''' </summary>
	''' <param name="code">The code.</param>
	''' <param name="audit">The identifier.</param>
	''' <returns></returns>
	<OperationContract()>
	Function GetUnitDoseType(ByVal code As String, audit As AuditMessage) As ActionResult(Of UnitDoseType)

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	<OperationContract()>
	Function GetUnitDoseTypeById(id As Integer, audit As AuditMessage) As ActionResult(Of UnitDoseType)

End Interface
