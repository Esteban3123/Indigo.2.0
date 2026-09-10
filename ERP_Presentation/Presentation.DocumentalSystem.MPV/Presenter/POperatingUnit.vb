'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 13/04/2015
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

Public Class POperatingUnit

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Dim View As IOperatingUnit

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    ''' <remarks></remarks>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef iview As IOperatingUnit)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' inicializa la estructura
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeStruct()
        Using model As New MBusqueda
            Me.View.StructXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListOperatingUnit)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa la estructura
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCity()
        Using model As New MBusqueda
            Me.View.cityXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity)
        End Using
    End Sub

#End Region

End Class
