'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
	Implements IMixingStationServiceUnitDoseType

	''' <summary>
	''' Actualiza un tipo de dosis unitaria
	''' </summary>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function ListAllUnitDoseType(audit As AuditMessage) As List(Of UnitDoseType) Implements IMixingStationServiceUnitDoseType.ListAllUnitDoseType
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.ListAllUnitDoseType(audit)
		End Using
	End Function

	''' <summary>
	''' Guarda un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function SaveUnitDoseType(unitDoseType As UnitDoseType, idSequence As Int64, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IMixingStationServiceUnitDoseType.SaveUnitDoseType
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.SaveUnitDoseType(unitDoseType, audit, idSequence)
		End Using
	End Function

	''' <summary>
	''' Elimina un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function DeleteUnitDoseType(unitDoseType As UnitDoseType, audit As AuditMessage) As ActionResult Implements IMixingStationServiceUnitDoseType.DeleteUnitDoseType
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.DeleteUnitDoseType(unitDoseType, audit)
		End Using
	End Function

	''' <summary>
	''' Actualiza el estado de un tipo de dosis unitaria
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function UpdateStateUnitDoseType(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IMixingStationServiceUnitDoseType.UpdateStateUnitDoseType
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.UpdateStateUnitDoseType(code, state, audit)
		End Using
	End Function

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por codigo
	''' </summary>
	''' <param name="code">The code.</param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function GetUnitDoseType(code As String, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IMixingStationServiceUnitDoseType.GetUnitDoseType
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.GetUnitDoseType(code, audit)
		End Using
	End Function

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function GetUnitDoseTypeById(id As Integer, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IMixingStationServiceUnitDoseType.GetUnitDoseTypeById
		Using service As IUnitDoseTypeAdminService = Container.Current.Resolve(Of IUnitDoseTypeAdminService)()
			Return service.GetUnitDoseTypeById(id, audit)
		End Using
	End Function
End Class
