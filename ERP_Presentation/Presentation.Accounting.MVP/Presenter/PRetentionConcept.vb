'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
#End Region

''' <summary>
''' Presentador del frontal de tipos de documentos
''' </summary>
''' <remarks></remarks>
Public Class PRetentionConcept

#Region "Variables and Constructor"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IRetentionConcept

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IRetentionConcept)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    Public Async Sub GetSequense()
        Using model As New MRetentionConcept(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCityXPO()
        View.CityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAllCities(True)
    End Sub

#End Region

End Class
