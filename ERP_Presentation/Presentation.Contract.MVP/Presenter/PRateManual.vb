'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
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
Imports Presentation.Controls.MVP

#End Region

Public Class PRateManual

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IRateManual

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filterMaterials() As Object = {True, 6, 1}

    Dim filterRoom() As Object = {True, 5}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IRateManual)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeContractMinimumWage()
        Using model As New MBusqueda
            View.ContractMinimumWageXpo = model.ConsultarEntidades(eDataSource.ListContractMinimumWageByStatus, True)
        End Using
    End Sub

    Public Sub InitializeIPSServices(ByVal typeManual As Integer)
        View.IPSServiceXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer) _
            .ContractService _
            .ListIPSServicesByPresentationAndServiceClass(status:=True, serviceClass:=1, presentation:=2, options:=1, typeManual:=typeManual)
    End Sub

    Public Sub InitializeIPSServicesSurgical(ByVal typeManual As Integer)
        View.IPSServiceXpoSurgical = XpoServiceEx.Instance(Indigo.TransactionalContainer) _
            .ContractService _
            .ListIPSServicesByPresentationAndServiceClass(status:=True, serviceClass:=1, presentation:=1, options:=2, typeManual:=typeManual)
    End Sub

    Public Sub Initialize(ByVal typeManual As Integer)
        InitializeIPSServices(typeManual)
        InitializeIPSServicesSurgical(typeManual)
        InitializeMaterialNoBloodyIPSService(typeManual)
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de grupos quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSurgicalGroup()
        Using model As New MBusqueda
            View.SurgicalGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSurgicalGroupByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeUVRRange()
        Using model As New MBusqueda
            View.UVRRangeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListUVRRangeByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMaterialNoBloodyIPSService(ByVal typeManual As Integer)
        Dim filter() As Object = {True, 1, typeManual, 1}
        Using model As New MBusqueda
            View.MaterialNoBloodyIPSServiceIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServiceByServiceClassAndServiceManualAndPresentation, filter)
        End Using
    End Sub

#End Region

End Class
