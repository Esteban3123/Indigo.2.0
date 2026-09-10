'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 02-07-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region
''' <summary>
''' Presentador del frontal de tipo de centros de trabajo
''' </summary>
Public Class PWorkCenter

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IWorkCenter

    ''' <summary>
    ''' Variable que se usa para tratar el grupo como un objeto
    ''' </summary>
    Dim WorkCenter As WorkCenter

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IWorkCenter)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub Initialize()

        Dim modelCityXPO As New MBusqueda
        Me.View.WorkCenterCitiesXpo = modelCityXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity)

    End Sub


End Class
