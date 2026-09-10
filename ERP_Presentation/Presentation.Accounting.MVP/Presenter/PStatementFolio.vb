'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Ernesto Córdoba
' Created          : 15-05-2014
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
Imports Presentation.Controls.MVP
#End Region

Public Class PStatementFolio

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IStatementFolio

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IStatementFolio)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MStatementFolio(View.MyTag)
            Me.View.Sequense = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Carga la definicion del layout
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub


End Class
