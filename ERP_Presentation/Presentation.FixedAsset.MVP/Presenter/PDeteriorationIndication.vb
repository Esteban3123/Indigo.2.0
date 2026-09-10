'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-07-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
#End Region

''' <summary>
''' Presentador del frontal de indicios de deterioro
''' </summary>
''' <remarks></remarks>
Public Class PDeteriorationIndication

#Region "Variables"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDeteriorationIndication

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDeteriorationIndication)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
