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
Imports Domain.Entities
Imports Infrastructure.Data.Xpo
#End Region

''' <summary>
''' Presentador del frontal de ciudades
''' </summary>
''' <remarks></remarks>
Public Class PCity

    ''' <summary>
    ''' Interfaz que representa la vista del frontal de ciudades
    ''' </summary>
    Dim View As ICity

    ''' <summary>
    ''' Instancia la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Construye el presentador del frontal de ciudades
    ''' </summary>
    ''' <param name="iview">the view</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal iview As ICity)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los Paises
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function Initializes() As Task
        Using Model As New MCountry(MCountry.TAG)
            View.DataSourceOfAllCountries = Await Model.ListAllCountriesAsync
        End Using
    End Function

    ''' <summary>
    ''' Asigna el datasource del concepto de retención
    ''' </summary>
    Public Sub InitializeICARetention()
        View.ICARetentionConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListRetentionConcept()
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

End Class
