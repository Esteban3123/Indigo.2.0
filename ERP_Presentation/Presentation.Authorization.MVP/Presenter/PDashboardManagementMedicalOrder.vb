#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base

#End Region

Public Class PDashboardManagementMedicalOrder

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDashboardManagementMedicalOrder

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IDashboardManagementMedicalOrder)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga las unidades funcionales
    ''' </summary>
    Public Sub InitializeCareCenter()
        If View.CentersXpo Is Nothing Then
            View.CentersXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
        End If
    End Sub

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequestsForManagementMedicalOrder(filter As String, topRows As Integer) As List(Of ViewListRequestsForManagementMedicalOrderXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsForManagementMedicalOrderXpo)(Nothing, filter, topRows).ToList()
    End Function

#End Region

End Class
