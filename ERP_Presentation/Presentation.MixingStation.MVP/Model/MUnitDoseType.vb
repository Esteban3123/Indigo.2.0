'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 20/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MUnitDoseType
	Implements IDisposable

#Region "Fields"


	''' <summary>
	''' Variable que contiene la instancia de la clase singleton
	''' </summary>
	Dim _sessionValues As SessionValues

	''' <summary>
	''' Tag del formulario
	''' </summary>
	Private _tagForm As String

#End Region

#Region "Builder"

	''' <summary>
	''' Contructor
	''' </summary>
	''' <param name="tag">tag del form</param>
	''' <remarks></remarks>
	Public Sub New(tag As String)
		Me._tagForm = tag
		_sessionValues = SessionValues.Instance
		Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
	End Sub

#End Region

#Region "Methods"
	''' <summary>
	''' Lista todos los tipo de dosis unitaria
	''' </summary>
	Public Function ListAllUnitDoseType() As List(Of UnitDoseType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllUnitDoseType(Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Lista todos los tipos de dosis unitaria asincrono
	''' </summary>
	Public Async Function ListAllUnitDoseTypeAsync() As Task(Of List(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllUnitDoseTypeAsync(Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Obtiene un tipo de dosis unitaria por código
	''' </summary>
	''' <param name="code"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetUnitDoseType(ByVal code As String) As ActionResult(Of UnitDoseType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetUnitDoseType(code, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Obtiene un tipo de dosis unitaria por código asincrono
	''' </summary>
	''' <param name="code"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function GetUnitDoseTypeAsync(ByVal code As String) As Task(Of ActionResult(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetUnitDoseTypeAsync(code, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id 
	''' </summary>
	''' <param name="id"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetUnitDoseTypeById(ByVal id As Integer) As ActionResult(Of UnitDoseType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetUnitDoseTypeById(id, Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id asincrono
	''' </summary>
	''' <param name="id"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function GetUnitDoseTypeByIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetUnitDoseTypeByIdAsync(id, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Guarda o actualiza un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function SaveUnitDoseType(ByVal unitDoseType As UnitDoseType, ByVal idSequence As Int64) As ActionResult(Of UnitDoseType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveUnitDoseType(unitDoseType, idSequence, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Guarda o actualiza un tipo de dosis unitaria asincrono
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function SaveUnitDoseTypeAsync(ByVal unitDoseType As UnitDoseType, ByVal idSequence As Int64) As Task(Of ActionResult(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveUnitDoseTypeAsync(unitDoseType, idSequence, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Elimina un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function DeleteUnitDoseType(ByVal unitDoseType As UnitDoseType) As ActionResult
		Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteUnitDoseType(unitDoseType, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Elimina un tipo de dosis unitaria asincrono
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function DeleteUnitDoseTypeAsync(ByVal unitDoseType As UnitDoseType) As Task(Of ActionResult)
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteUnitDoseTypeAsync(unitDoseType, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Cambia el estado de la entidad
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStateUnitDoseTypeAsync(code, state, Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Cambia el estado de la entidad
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function ChangeStateAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of UnitDoseType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStateUnitDoseTypeAsync(code, state, Me._sessionValues.AuditMessageWcf)
	End Function

#End Region

#Region "IDisposable Support"
	Private disposedValue As Boolean ' To detect redundant calls

	' IDisposable
	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not Me.disposedValue Then
			If disposing Then
				' TODO: dispose managed state (managed objects).
			End If

			' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
			' TODO: set large fields to null.
		End If
		Me.disposedValue = True
	End Sub

	' This code added by Visual Basic to correctly implement the disposable pattern.
	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub
#End Region

End Class
