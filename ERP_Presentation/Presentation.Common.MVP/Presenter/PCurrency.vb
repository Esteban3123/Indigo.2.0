'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Common
Imports Presentation.Accounting.MVP
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PCurrency

#Region "Variables and constructor"

    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim view As ICurrency

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As ICurrency)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.view = iview
        End If
    End Sub

    Public Async Sub GetSequense()
        Using model As New MStatementFolio(Me.view.MyTag)
            Me.view.Sequence = Await model.GetSequense()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me.view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa una lista de divisas de moneda
    ''' </summary>
    Public Sub InitializerISO4217()
        Using Model As New MCurrency("")
            Me.view.CurrencyISO4217 = Model.GetISOCurrency()
        End Using
    End Sub
    ''' <summary>
    ''' Inicializa una lista de divisas de moneda
    ''' </summary>
    Public Sub InitializerISO4217(ByVal criteria As String)
        Using Model As New MCurrency("")
            Me.view.CurrencyISO4217filter = Model.GetISOCurrency(criteria)
        End Using
    End Sub

#End Region

End Class
