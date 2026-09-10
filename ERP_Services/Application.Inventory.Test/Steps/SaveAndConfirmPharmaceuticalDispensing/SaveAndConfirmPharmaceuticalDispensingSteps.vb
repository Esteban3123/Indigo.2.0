Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports TechTalk.SpecFlow

Namespace Application.Inventory.Test

    Public Class ConsecutiveCode
        Public Property ListCodes As List(Of String)

        Friend Function AssignCode() As String
            If ListCodes IsNot Nothing AndAlso ListCodes.Any() Then
                Dim newcode = ListCodes(0)
                ListCodes.RemoveAt(0)
                Return newcode
            End If
            Return ""
        End Function
    End Class


    <Binding()>
    Public Class SaveAndConfirmPharmaceuticalDispensingSteps
#Region "Fields"
        Private ReadOnly _InventoryContext As InventoryContext
        Private _pharmaceuticalDispensing As New Domain.Entities.PharmaceuticalDispensing
        Private _pharmaceuticalDispensingDetail As New Domain.Entities.PharmaceuticalDispensingDetail
        Private _listPharmaceuticalDispensingDetail As New Domain.Entities.TrackableCollection(Of PharmaceuticalDispensingDetail)
        ' Private _listPharmaceuticalDispensingDetailBatchSerial As New Domain.Entities.TrackableCollection(Of Domain.Entities.PharmaceuticalDispensingDetailBatchSerial)
        Private _pharmaceuticalDispensingDetailBatchSerial As New Domain.Entities.PharmaceuticalDispensingDetailBatchSerial
        Private _actionResult As New ActionResult(Of Domain.Entities.PharmaceuticalDispensing)
        Private _state As Boolean
        Private _message As String
        Private Shared _consecutives As ConsecutiveCode

        Private Shared ReadOnly Property Instance As ConsecutiveCode
            Get
                If _consecutives Is Nothing Then
                    _consecutives = New ConsecutiveCode()
                    _consecutives.ListCodes = New List(Of String)()
                    _consecutives.ListCodes.Add("0494")
                    _consecutives.ListCodes.Add("0495")
                    _consecutives.ListCodes.Add("0496")
                    _consecutives.ListCodes.Add("0497")
                    _consecutives.ListCodes.Add("0498")
                    _consecutives.ListCodes.Add("0499")
                    _consecutives.ListCodes.Add("0500")
                    _consecutives.ListCodes.Add("0501")
                    _consecutives.ListCodes.Add("0502")
                    _consecutives.ListCodes.Add("0503")
                    _consecutives.ListCodes.Add("0504")
                    _consecutives.ListCodes.Add("0505")
                    _consecutives.ListCodes.Add("0506")
                    _consecutives.ListCodes.Add("0507")
                    _consecutives.ListCodes.Add("0508")
                    _consecutives.ListCodes.Add("0509")
                    _consecutives.ListCodes.Add("0510")
                    _consecutives.ListCodes.Add("0511")
                    _consecutives.ListCodes.Add("0512")
                    _consecutives.ListCodes.Add("0513")
                    _consecutives.ListCodes.Add("0514")
                    _consecutives.ListCodes.Add("0515")
                    _consecutives.ListCodes.Add("0516")
                    _consecutives.ListCodes.Add("0517")
                    _consecutives.ListCodes.Add("0518")
                    _consecutives.ListCodes.Add("0519")
                    _consecutives.ListCodes.Add("0520")
                    _consecutives.ListCodes.Add("0521")
                    _consecutives.ListCodes.Add("0522")
                    _consecutives.ListCodes.Add("0523")
                    _consecutives.ListCodes.Add("0524")
                    _consecutives.ListCodes.Add("0525")
                    _consecutives.ListCodes.Add("0526")
                    _consecutives.ListCodes.Add("0527")
                    _consecutives.ListCodes.Add("0528")
                    _consecutives.ListCodes.Add("0529")
                    _consecutives.ListCodes.Add("0530")
                    _consecutives.ListCodes.Add("0531")
                    _consecutives.ListCodes.Add("0532")
                    _consecutives.ListCodes.Add("0533")
                    _consecutives.ListCodes.Add("0534")
                    _consecutives.ListCodes.Add("0535")
                    _consecutives.ListCodes.Add("0536")
                End If
                Return _consecutives
            End Get
        End Property
#End Region


#Region "Builder"
        Public Sub New(InventoryContext As InventoryContext)
            _InventoryContext = InventoryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los registros maestro y detalle")>
        Public Sub DadoGeneroLosRegistrosMaestroYDetalle()

            For i As Double = 0 To 1
                Dim _listPharmaceuticalDispensingDetailBatchSerial As New Domain.Entities.TrackableCollection(Of Domain.Entities.PharmaceuticalDispensingDetailBatchSerial)
                _listPharmaceuticalDispensingDetailBatchSerial.Add(New Domain.Entities.PharmaceuticalDispensingDetailBatchSerial With
                    {
                        .PhysicalInventoryId = 62,
                        .Quantity = 1,
                        .OutstandingQuantity = 1
                    })

                _listPharmaceuticalDispensingDetail.Add(New PharmaceuticalDispensingDetail With
                                                        {
                                                            .CareGroupId = 1041,
                                                            .HealthAdministratorId = 41,
                                                            .ThirdPartyId = 239,
                                                            .ProductId = 6362,
                                                            .WarehouseId = 1318,
                                                            .Quantity = 1,
                                                            .ReturnedQuantity = 0,
                                                            .ServiceDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                            .FunctionalUnitId = 6,
                                                            .OrderedHealthProfessionalCode = "YANALIT",
                                                            .OrderedProfessionalSpecialty = "353",
                                                            .OrderedHealthProfessionalThirdPartyId = 368046,
                                                            .LiquidationType = 1,
                                                            .CupsEntityId = Nothing,
                                                            .SurchargeApply = False,
                                                            .SalePrice = 500.0,
                                                            .AverageCost = 606.0,
                                                            .DiscountPercentage = 0,
                                                            .DiscountValue = 0,
                                                            .TotalSalesPrice = 500.0,
                                                            .GrandTotalSalesPrice = 500.0,
                                                            .PharmaceuticalDispensingDetailBatchSerial = _listPharmaceuticalDispensingDetailBatchSerial})

            Next



            'With _pharmaceuticalDispensingDetailBatchSerial
            '    .PhysicalInventoryId = 62
            '    .Quantity = 1
            '    .OutstandingQuantity = 1
            'End With

            'With _pharmaceuticalDispensingDetail
            '    .CareGroupId = 1041
            '    .HealthAdministratorId = 41
            '    .ThirdPartyId = 239
            '    .ProductId = 6362
            '    .WarehouseId = 1318
            '    .Quantity = 1
            '    .ReturnedQuantity = 0
            '    .ServiceDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            '    .FunctionalUnitId = 6
            '    .OrderedHealthProfessionalCode = "YANALIT"
            '    .OrderedProfessionalSpecialty = "353"
            '    .OrderedHealthProfessionalThirdPartyId = 368046
            '    .LiquidationType = 1
            '    .CupsEntityId = Nothing
            '    .SurchargeApply = False
            '    .SalePrice = 500.0
            '    .AverageCost = 606.0
            '    .DiscountPercentage = 0
            '    .DiscountValue = 0
            '    .TotalSalesPrice = 500.0
            '    .GrandTotalSalesPrice = 500.0
            '    .PharmaceuticalDispensingDetailBatchSerial.Add(_pharmaceuticalDispensingDetailBatchSerial)
            'End With

            With _pharmaceuticalDispensing
                .Code = "" 'SaveAndConfirmPharmaceuticalDispensingSteps.Instance.AssignCode()
                .OperatingUnitId = 14
                .AdmissionNumber = "104180    "
                .DocumentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                .AffectInventory = 1
                .Status = 2
                .PharmaceuticalDispensingDetail = _listPharmaceuticalDispensingDetail
            End With

        End Sub

        <TechTalk.SpecFlow.Given("Luego guardo los registros (.*)")>
        Public Sub DadoLuegoGuardoLosRegistros(ByVal p0 As Int32)

            _actionResult = _InventoryContext._pharmaceuticalDispensingAdminService.SavePharmaceuticalDispensing(_pharmaceuticalDispensing, _InventoryContext._audit, True, 0, True)
            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un error al guardar la dispensación farmaceutica. Error :" + _actionResult.Message)
            End If

        End Sub

        <TechTalk.SpecFlow.When("Confirmo los registros")>
        Public Sub CuandoConfirmoLosRegistros()
            '_pharmaceuticalDispensing.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss")
            '_pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ElementAt(0).ServiceDate.ToString("dd/MM/yyyy HH:mm:ss")
            'Dim _actionResult As ActionResult = _InventoryContext._pharmaceuticalDispensingAdminService.ConfirmPharmaceuticalDispensing(_pharmaceuticalDispensing, _InventoryContext._audit, True)
            ' If Not _actionResult.StateResult Then
            ' _state = False
            ' _message = "Hubo un error al confirmar la dispensación farmaceutica. Error :" + _actionResult.Message
            '      Assert.Fail("Hubo un error al confirmar la dispensación farmaceutica. Error :" + _actionResult.Message)
            ' End If
        End Sub

        <TechTalk.SpecFlow.Then("Estos son almacenados y Confirmados")>
        Public Sub EntoncesEstosSonAlmacenadosYConfirmados()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
