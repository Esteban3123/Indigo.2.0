'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 24-04-2013
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
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls.MVP


#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Unidades funcionales
''' </summary>
Public Class PFunctionalUnit

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IFunctionalUnit
    ''' </summary>
    Private _view As IFunctionalUnit
    ''' <summary>
    ''' Variable que se utilizapa para tratar las unidades funcionales como un Objeto
    ''' </summary>
    Private _functionalUnit As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de unidades funcionales
    ''' </summary>
    ''' <param name="view">Vista de las unidades funcionales</param>
    Public Sub New(ByRef view As IFunctionalUnit)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub
    '''' <summary>
    '''' Inicializa el datasource de los turnos
    '''' </summary>
    'Public Sub InitializeTurn()
    '    Using Model As New MBusqueda
    '        Me._view.TurnDatasource = Model.ConsultarEntidades(eDataSource.ListTurn)
    '    End Using
    'End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Async Function Initializes() As Task
        Using Model As New MBranchOffice
            _view.DatasourceBranchOffice = Await Model.ListAllBranchOfficeAsync
        End Using
    End Function

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(_view.MyTag)
            _view.Sequence = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
