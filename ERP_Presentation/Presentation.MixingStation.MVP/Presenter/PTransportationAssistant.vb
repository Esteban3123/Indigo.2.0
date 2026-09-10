'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PTransportationAssistant

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As ITransportationAssistant

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As ITransportationAssistant)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the cargos.
    ''' </summary>
    Public Function InitializePosition() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Return Model.ConsultarEntidades(eDataSource.ListPositionByStatus, True)
        End Using
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <param name="positionId"></param>
    ''' <returns></returns>
    Public Function InitializeEmployees(positionId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PayrollService.ListEmployeeByPosition(positionId)
    End Function

    ''' <summary>
    ''' Obtiene el empleado por id
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    Public Function GetEmployeeById(employeeId As Integer) As PayrollEmployee
        Dim filter As String = "Id = " & employeeId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollEmployee)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el empleado por id
    ''' </summary>
    ''' <param name="personId"></param>
    ''' <returns></returns>
    Public Function GetPersonById(personId As Integer) As CommonPersonXpo
        Dim filter As String = "Id = " & personId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PayrollService.GetCollection(Of CommonPersonXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
