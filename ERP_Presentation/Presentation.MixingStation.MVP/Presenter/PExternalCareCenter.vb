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
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Payroll.MVP
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PExternalCareCenter

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IExternalCareCenter

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
    Public Sub New(ByVal iview As IExternalCareCenter)
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
    ''' Carga los usuarios
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeUsers() As DevExpress.Data.Linq.LinqInstantFeedbackSource
        Using model As New MFunctionalUnit("")
            Return model.ListAllUser(_sessionValues.SecurityContainer)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the cargos.
    ''' </summary>
    Public Function InitializeCustomer() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Function

    ''' <summary>
    ''' Initializes the cargos.
    ''' </summary>
    Public Function InitializeContractExternalClients(Optional CustomerId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListContractExternalClientsByStatus(True, CustomerId)
    End Function

#End Region

End Class
