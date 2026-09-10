'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 24/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MShelfType
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
	''' Lista todos los tipo de estante
	''' </summary>
	Public Function ListAllShelfType() As List(Of ShelfType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListAllShelfType(Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Lista todos los tipos de estante asincrono
	''' </summary>
	Public Async Function ListAllShelfTypeAsync() As Task(Of List(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListAllShelfTypeAsync(Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Obtiene un tipo de estante por código
	''' </summary>
	''' <param name="code"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetShelfType(ByVal code As String) As ActionResult(Of ShelfType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetShelfType(code, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Obtiene un tipo de estante por código asincrono
	''' </summary>
	''' <param name="code"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function GetShelfTypeAsync(ByVal code As String) As Task(Of ActionResult(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetShelfTypeAsync(code, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Obtiene un tipo de estante por id 
	''' </summary>
	''' <param name="id"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetShelfTypeById(ByVal id As Integer) As ActionResult(Of ShelfType)
		Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetShelfTypeById(id, Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Obtiene un tipo de estante por id asincrono
	''' </summary>
	''' <param name="id"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function GetShelfTypeByIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetShelfTypeByIdAsync(id, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Guarda o actualiza un tipo de estante
	''' </summary>
	''' <param name="shelfType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function SaveShelfType(ByVal shelfType As ShelfType, ByVal idSequence As Int64) As Task(Of ActionResult(Of ShelfType))
		Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveShelfTypeAsync(shelfType, idSequence, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Guarda o actualiza un tipo de estante asincrono
	''' </summary>
	''' <param name="shelfType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function SaveShelfTypeAsync(ByVal shelfType As ShelfType, ByVal idSequence As Int64) As Task(Of ActionResult(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveShelfTypeAsync(shelfType, idSequence, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Elimina un tipo de estante
	''' </summary>
	''' <param name="shelfType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function DeleteShelfType(ByVal shelfType As ShelfType) As ActionResult
		Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteShelfType(shelfType, Me._sessionValues.AuditMessageWcf)
	End Function
	''' <summary>
	''' Elimina un tipo de estante asincrono
	''' </summary>
	''' <param name="shelfType"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function DeleteShelfTypeAsync(ByVal shelfType As ShelfType) As Task(Of ActionResult)
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteShelfTypeAsync(shelfType, Me._sessionValues.AuditMessageWcf)
	End Function
	'***********************************************************************
	''' <summary>
	''' Cambia el estado de la entidad
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function UpdateState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateShelfTypeAsync(code, state, Me._sessionValues.AuditMessageWcf)
	End Function

	''' <summary>
	''' Cambia el estado de la entidad asincrono
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Async Function ChangeStateAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ShelfType))
		Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateShelfTypeAsync(code, state, Me._sessionValues.AuditMessageWcf)
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
