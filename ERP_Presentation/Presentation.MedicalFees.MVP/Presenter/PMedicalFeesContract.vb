'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
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
Imports Infrastructure.Data.Xpo

#End Region

Public Class PMedicalFeesContract

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IMedicalFeesContract

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IMedicalFeesContract)
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

    Public Sub InitializeRateManual()
        Using model As New MBusqueda
            View.RateManualXpoPopup = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRateManualByStatus, True)
        End Using
    End Sub

    Public Sub InitializeCUPSEntity()
        View.CUPSEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatusXpCollection(True)
    End Sub

    Public Sub InitializeCareGroup()
        View.CareGroupXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Sub

    Public Sub InitializeContractEntity()
        View.ContractEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim model As New MBusqueda
        Me.View.SupplierDistributionLineXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub

    Public Sub InitializeGroupCUPS()
        Using model As New MBusqueda
            Me.View.CUPSGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeSubgroupCUPS()
        Using model As New MBusqueda
            Me.View.CUPSSubgroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsSubGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeIPSService()
        Using model As New MBusqueda
            Me.View.IPSServiceXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de la rejilla de tipos de liquidacion
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeDataSourceGridControlsLiquidationType(type As Integer) As DevExpress.Xpo.XPCollection
        Select Case type
            Case 1 'IPSService
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListIPSServicesByStatusXpCollection(True)
            Case 2 'CUPS no se realiza porque el tiene un control especial aparte
                Return Nothing
            Case 3 'CupsSubGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsSubGroupByStatusXpCollection(True)
            Case 4 'CupsGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsGroupByStatusXpCollection(True)
            Case 5 'CareGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatusXpCollection(True)
            Case 6 'ContractEntity
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractEntityByStatusXpCollection(True)
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud con xpCollection
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeHealthProfessional()
        View.HealthProfessionalXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessionalAll()
    End Sub

    ''' <summary>
    ''' Obtiene los médicos asociados al contrato consultado
    ''' </summary>
    ''' <param name="medicalFeesContractId"></param>
    ''' <remarks></remarks>
    Public Function LoadHealthProfessionalByMedicalFeesContractId(medicalFeesContractId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListHealthProfessionalContractByMedicalFeesContractId(medicalFeesContractId)
    End Function

#End Region

End Class
