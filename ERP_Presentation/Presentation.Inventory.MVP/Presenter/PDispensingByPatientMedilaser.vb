'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/09/2018
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
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PDispensingByPatientMedilaser

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDispensingPatientMedilaser

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IDispensingPatientMedilaser)
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
    ''' Carga los centros de atención
    ''' </summary>
    Public Sub LoadCareCenter()
        View.CareCenterXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Sub

    ''' <summary>
    ''' Carga los almacenes
    ''' </summary>
    Public Sub LoadWareHouse()
        View.WarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListNoTransitWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Carga los grupos de atención
    ''' </summary>
    Public Sub LoadCareGroup()
        View.CareGroupXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Sub

    ''' <summary>
    ''' Carga las autorizaciones
    ''' </summary>
    Public Sub LoadBillingAuthorization()
        View.BillingAuthorizationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAllBillingAuthorizationByUserCode(Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Obtiene los parámetros de facturación por unidad operativa
    ''' </summary>
    Public Function GetSettingBilling(operatingUnitId As Integer) As SettingsBillingXpo
        Dim filter As String = "IdOperatingUnit = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of SettingsBillingXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la formula medica
    ''' </summary>
    Public Function GetMedicalFormulaByNumber(number As String) As MedicalFormulaXpo
        Dim filter As String = "Number = '" & number & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of MedicalFormulaXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
