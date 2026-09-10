'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-08-2013
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
''' 
''' </summary>
Public Class PFixedAssetPolizaType

    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetPolizaType

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetPolizaType)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub Initializes()
        View.StatePolizaType = True
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub
End Class
