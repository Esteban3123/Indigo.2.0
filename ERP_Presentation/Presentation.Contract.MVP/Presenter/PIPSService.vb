'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PIPSService

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IIPSService
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IIPSService)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region


    Public Sub InitializeCupsEntityXPO()
        Using model As New MBusqueda
            View.CupsEntityXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntityByStatus, True)
        End Using
    End Sub

    Public Sub InitializeIPSServiceXPO(ByVal serviceManual As Integer)
        Using model As New MBusqueda
            View.ServicesIPSXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesNoSurgical, serviceManual)
        End Using
    End Sub

    Public Sub InitializeSurgicalGroupXpo()
        Using model As New MBusqueda
            View.SurgicalGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSurgicalGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeAssociatedMaterialIPSService(ByVal typeManual As Integer)
        Dim filter() As Object = {True, 6, typeManual}
        Using model As New MBusqueda
            View.AssociatedMaterialIPSServiceIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesByServiceManualAndServiceClass, filter)
        End Using
    End Sub

    Public Sub InitializeIva()
        Using model As New MBusqueda
            View.ListGeneralLedgerIva = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub

End Class
