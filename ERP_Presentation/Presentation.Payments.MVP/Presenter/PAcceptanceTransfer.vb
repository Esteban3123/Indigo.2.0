'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Rafael Eduardo Patiño
' Created          : 16/03/2015
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
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class PAcceptanceTransfer

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAcceptanceTransfer

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAcceptanceTransfer)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ' ''' <summary>
    ' ''' Inicializa el datasource de unidad de radicacion
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub InitializeFilingUnit()
    '    Using model As New MBusqueda
    '        Me.View.FilingUnitTargetXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatusCollection, True)
    '    End Using
    'End Sub

#End Region

End Class
