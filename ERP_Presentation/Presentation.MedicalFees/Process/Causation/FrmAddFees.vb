'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmAddFees
    Implements IAddFees

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="entityXpoViewSurgicalAndPackage">Entidad xpo Qx</param>
    ''' <param name="entityViewNoSurgical">Entidad xpo NoQx</param>
    ''' <param name="_session">Sesion de la entidad xpo que envio como parametro</param>
    ''' <param name="viewGridSurgical">Variable para saber si esta en la vista de la rejilla de Qx o NoQx (True=Rejilla Qx, False=Rejilla NoQx)</param>
    ''' <remarks></remarks>
    Public Sub New(entityXpoViewSurgicalAndPackage As ViewListSurgicalAndPackageXpo, entityViewNoSurgical As Domain.Entities.ViewListNoSurgical, _session As DevExpress.Xpo.Session, viewGridSurgical As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Se inicializa la variable de la sesion del xpo que se obtiene del parametro
        session = _session
        Me.ViewGridSurgical = viewGridSurgical
        If Me.ViewGridSurgical Then 'Si viene de la rejilla Qx
            _ViewListSurgicalAndPackage = entityXpoViewSurgicalAndPackage
        Else 'Si viene de la rejilla NoQx
            _ViewListNoSurgical = entityViewNoSurgical
        End If
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalId As String Implements IAddFees.HealthProfessionalId
        Get
            Return INDsleHealthProfessional.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessional.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalXpo As XPInstantFeedbackSource Implements IAddFees.HealthProfessionalXpo
        Get
            Return INDsleHealthProfessional.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAddFees

    ''' <summary>
    ''' Variable que establece la rejilla desde donde se esta obteniendo el item
    ''' (True=Rejilla Qx, False=Rejilla NoQx)
    ''' </summary>
    ''' <remarks></remarks>
    Dim ViewGridSurgical As Boolean

    ''' <summary>
    ''' Representa a la entidad Xpo Qx
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ViewListSurgicalAndPackage As ViewListSurgicalAndPackageXpo

    ''' <summary>
    ''' Representa a la entidad  NoQx
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ViewListNoSurgical As Domain.Entities.ViewListNoSurgical

    ''' <summary>
    ''' Evento que guarda el honorario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SaveAddFees(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Representa a la entidad de detalles quirurgicos de ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim ServiceOrderDetailSurgical As ServiceOrderDetailSurgical

    ''' <summary>
    ''' Representa a la entidad xpo del medico
    ''' </summary>
    ''' <remarks></remarks>
    Dim _HealthCareProfessionalXpo As Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo

    ''' <summary>
    ''' Representa la entidad de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim thirdParty As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Obtiene la sesion del xpo
    ''' </summary>
    ''' <remarks></remarks>
    Dim session As DevExpress.Xpo.Session

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Muestra el slide de los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If HealthProfessionalId = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir un médico."
            INDsleHealthProfessional.Focus()
            Exit Sub
        End If

        'Se crea el detalle quirurgico
        ServiceOrderDetailSurgical = CreateEntityServiceOrderDetailSurgical()

        Dim entityXpoSurgical As ViewListSurgicalAndPackageXpo = Nothing
        Dim _entityNoSurgical As Domain.Entities.ViewListNoSurgical = Nothing
        If ViewGridSurgical Then 'Si viene de la rejilla Qx
            entityXpoSurgical = CreateXpoSurgical()
        Else 'Si viene de la rejilla NoQx
            _entityNoSurgical = CreateNoSurgical()
        End If

        Dim args As AddFeesEventArgs = New AddFeesEventArgs
        args.ServiceOrderDetailSurgical = ServiceOrderDetailSurgical
        args.ViewListSurgicalAndPackage = entityXpoSurgical
        args.DomainViewListNoSurgical = _entityNoSurgical
        RaiseEvent SaveAddFees(Nothing, args)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Metodo que crea la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateEntityServiceOrderDetailSurgical() As ServiceOrderDetailSurgical
        'Se crea el detalle quirurgico
        ServiceOrderDetailSurgical = New ServiceOrderDetailSurgical
        With ServiceOrderDetailSurgical
            If ViewGridSurgical Then 'Si viene de la rejilla Qx
                .ServiceOrderDetailId = _ViewListSurgicalAndPackage.ServiceOrderDetailId
                .IPSServiceId = _ViewListSurgicalAndPackage.IPSServiceId
                .InvoicedQuantity = _ViewListSurgicalAndPackage.InvoicedQuantity
                .LiquidationPercentage = 0
                .RateManualSalePrice = _ViewListSurgicalAndPackage.RateManualSalePrice
                .TotalSalesPrice = _ViewListSurgicalAndPackage.TotalSalesPrice
                .PerformsHealthProfessionalCode = HealthProfessionalId
                .PerformsHealthProfessionalThirdPartyId = thirdParty.Id
                .CostValue = _ViewListSurgicalAndPackage.CostValue
                .BillingConceptId = _ViewListSurgicalAndPackage.BillingConceptId
                .CostCenterId = _ViewListSurgicalAndPackage.CostCenterId
                If _ViewListSurgicalAndPackage.RateManualDetailSurgicalId = 0 Then
                    .RateManualDetailSurgicalId = Nothing
                Else
                    .RateManualDetailSurgicalId = _ViewListSurgicalAndPackage.RateManualDetailSurgicalId
                End If
                .IncomeMainAccountId = _ViewListSurgicalAndPackage.IncomeMainAccountId
                .SurchargeApply = False
                .OnlyMedicalFees = True
            Else 'Si viene de la rejilla NoQx
                .ServiceOrderDetailId = _ViewListNoSurgical.ServiceOrderDetailId
                .IPSServiceId = _ViewListNoSurgical.IPSServiceId
                .InvoicedQuantity = _ViewListNoSurgical.InvoicedQuantity
                .LiquidationPercentage = 0
                .RateManualSalePrice = _ViewListNoSurgical.RateManualSalePrice
                .TotalSalesPrice = _ViewListNoSurgical.TotalSalesPrice
                .PerformsHealthProfessionalCode = HealthProfessionalId
                .PerformsHealthProfessionalThirdPartyId = thirdParty.Id
                .CostValue = _ViewListNoSurgical.CostValue
                .BillingConceptId = _ViewListNoSurgical.BillingConceptId
                .CostCenterId = _ViewListNoSurgical.CostCenterId
                If _ViewListNoSurgical.RateManualDetailSurgicalId = 0 Then
                    .RateManualDetailSurgicalId = Nothing
                Else
                    .RateManualDetailSurgicalId = _ViewListNoSurgical.RateManualDetailSurgicalId
                End If
                .IncomeMainAccountId = _ViewListNoSurgical.IncomeMainAccountId
                .SurchargeApply = False
                .OnlyMedicalFees = True
            End If
        End With
        Return ServiceOrderDetailSurgical
    End Function

    ''' <summary>
    ''' Metodo que crea la entidad xpo Qx
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateXpoSurgical() As ViewListSurgicalAndPackageXpo
        'Se crea el objeto xpo nuevo
        Dim entityXpoSurgical As New ViewListSurgicalAndPackageXpo(session)
        With entityXpoSurgical
            .AmountPayable = 0
            .BillingGroupDescription = _ViewListSurgicalAndPackage.BillingGroupDescription
            .BillingGroupId = _ViewListSurgicalAndPackage.BillingGroupId
            .CareGroupId = _ViewListSurgicalAndPackage.CareGroupId
            .CostCenterId = _ViewListSurgicalAndPackage.CostCenterId
            .CostValue = _ViewListSurgicalAndPackage.CostValue
            .CupsEntityDescription = _ViewListSurgicalAndPackage.CupsEntityDescription
            .CupsEntityId = _ViewListSurgicalAndPackage.CupsEntityId
            .GrandTotalSalesPrice = _ViewListSurgicalAndPackage.GrandTotalSalesPrice
            .InvoiceDetailId = _ViewListSurgicalAndPackage.InvoiceDetailId
            .InvoicedQuantity = _ViewListSurgicalAndPackage.InvoicedQuantity
            .InvoiceId = _ViewListSurgicalAndPackage.InvoiceId
            .IPSServiceDescription = _ViewListSurgicalAndPackage.IPSServiceDescription
            .IPSServiceDescriptionSOD = _ViewListSurgicalAndPackage.IPSServiceDescriptionSOD
            .BillingConceptId = _ViewListSurgicalAndPackage.BillingConceptId
            .IPSServiceId = _ViewListSurgicalAndPackage.IPSServiceId
            .MedicalFeesCausationId = Nothing
            .MedicalFeesContractId = _ViewListSurgicalAndPackage.MedicalFeesContractId

            .PerformsHealthProfessionalCode = ServiceOrderDetailSurgical.PerformsHealthProfessionalCode
            .ThirdPartyDescription = INDsleHealthProfessional.Text
            .ThirdPartyId = ServiceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId

            .Presentation = _ViewListSurgicalAndPackage.Presentation
            .RateManualDetailSurgicalId = _ViewListSurgicalAndPackage.RateManualDetailSurgicalId
            .RateManualId = _ViewListSurgicalAndPackage.RateManualId
            .RateManualSalePrice = _ViewListSurgicalAndPackage.RateManualSalePrice
            .SelectOption = False
            .ServiceOrderDetailId = _ViewListSurgicalAndPackage.ServiceOrderDetailId
            .ServiceOrderDetailSurgicalId = Nothing
            .ServiceOrderId = _ViewListSurgicalAndPackage.ServiceOrderId
            .Status = _ViewListSurgicalAndPackage.Status
            .TotalAmountPayable = 0
            .TotalSalesPrice = _ViewListSurgicalAndPackage.TotalSalesPrice
            .OnlyMedicalFees = True
            .IncomeMainAccountId = _ViewListSurgicalAndPackage.IncomeMainAccountId
        End With
        Return entityXpoSurgical
    End Function

    ''' <summary>
    ''' Metodo que crea el xpo NoQx
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateNoSurgical() As Domain.Entities.ViewListNoSurgical
        'Se crea el objeto  nuevo
        Dim entityNoSurgical As New Domain.Entities.ViewListNoSurgical
        With entityNoSurgical
            .AmountPayable = 0
            .BillingGroupDescription = _ViewListNoSurgical.BillingGroupDescription
            .BillingGroupId = _ViewListNoSurgical.BillingGroupId
            .CareGroupId = _ViewListNoSurgical.CareGroupId
            .CostCenterId = _ViewListNoSurgical.CostCenterId

            .CupsEntityDescription = _ViewListNoSurgical.CupsEntityDescription
            .CupsEntityId = _ViewListNoSurgical.CupsEntityId
            .GrandTotalSalesPrice = _ViewListNoSurgical.GrandTotalSalesPrice
            .InvoiceDetailId = _ViewListNoSurgical.InvoiceDetailId
            .InvoicedQuantity = _ViewListNoSurgical.InvoicedQuantity
            .InvoiceId = _ViewListNoSurgical.InvoiceId
            .IPSServiceDescription = _ViewListNoSurgical.IPSServiceDescription

            .BillingConceptId = _ViewListNoSurgical.BillingConceptId
            .IPSServiceId = _ViewListNoSurgical.IPSServiceId
            .MedicalFeesCausationId = Nothing
            .MedicalFeesContractId = _ViewListNoSurgical.MedicalFeesContractId

            .PerformsHealthProfessionalCode = ServiceOrderDetailSurgical.PerformsHealthProfessionalCode
            .ThirdPartyDescription = INDsleHealthProfessional.Text
            .ThirdPartyId = ServiceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId

            .Presentation = _ViewListNoSurgical.Presentation
            .RateManualDetailId = _ViewListNoSurgical.RateManualDetailId
            .RateManualId = _ViewListNoSurgical.RateManualId
            .RateManualSalePrice = _ViewListNoSurgical.RateManualSalePrice
            .RateManualType = _ViewListNoSurgical.RateManualType
            .SelectOption = False
            .ServiceOrderDetailId = _ViewListNoSurgical.ServiceOrderDetailId
            .ServiceOrderDetailSurgicalId = Nothing
            .ServiceOrderId = _ViewListNoSurgical.ServiceOrderId
            .Status = _ViewListNoSurgical.Status
            .TotalAmountPayable = 0
            .TotalSalesPrice = _ViewListNoSurgical.TotalSalesPrice
            .OnlyMedicalFees = True
            .IncomeMainAccountId = _ViewListNoSurgical.IncomeMainAccountId
        End With
        Return entityNoSurgical
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ViewGridSurgical = Nothing
        _ViewListSurgicalAndPackage = Nothing
        _ViewListNoSurgical = Nothing
        ServiceOrderDetailSurgical = Nothing
        _HealthCareProfessionalXpo = Nothing
        thirdParty = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddFees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAddFees(Me)
        INDsleHealthProfessional.Properties.Buttons(1).Visible = False
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddFees_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleHealthProfessional.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthProfessional_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthProfessional.QueryPopUp
        If HealthProfessionalXpo Is Nothing Then
            Presenter.InitializeHealthProfessional()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AssigningValues()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHealthProfessional.EditValueChanged
        If HealthProfessionalId <> Nothing Then
            _HealthCareProfessionalXpo = DirectCast(DirectCast(viewSearchHealthProfessional.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo)
            Using model As New MThirdParty("")
                thirdParty = model.GetThirdParty(_HealthCareProfessionalXpo.CODIGONIT.TrimStart("0"))
                If thirdParty.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), _HealthCareProfessionalXpo.CodeName)
                    HealthProfessionalId = Nothing
                End If
            End Using
        End If
    End Sub

#End Region

#End Region

End Class