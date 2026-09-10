'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 20-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PUnitDoseType

#Region "Variables"
	''' <summary>
	''' Variable para instanciar la interfaz
	''' </summary>
	Dim _view As IUnitDoseType

	''' <summary>
	''' Se utiliza para instanciar la clase singleton
	''' </summary>
	Dim _sessionValues As SessionValues
#End Region

#Region "Builder"
	''' <summary>
	''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
	''' </summary>
	''' <param name="iview">The iview.</param>
	''' <exception cref="System.ArgumentException"></exception>
	Public Sub New(ByVal iview As IUnitDoseType)
		If iview Is Nothing Then
			Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
		Else
			Me._view = iview
			Me._sessionValues = SessionValues.Instance
		End If
	End Sub
#End Region

#Region "Methods"

	Public Async Sub GetSequence()
		Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
			Me._view.Sequence = Await model.GetSequence()
		End Using
	End Sub

	Public Async Sub LoadDefinitionLayout()
		Await Me._view.MyLayoutControl.LoadDefinitionAsync()
	End Sub
#End Region

End Class
