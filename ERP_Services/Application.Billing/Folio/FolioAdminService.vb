'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
' Modified         : Diego A. Roldán
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Collections.Concurrent
Imports System.Net
Imports System.Text
Imports System.Threading.Tasks
Imports System.Transactions
Imports Application.Common
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Billing.Repositories
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FolioAdminService
    Implements IFolioAdminService

    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository
    Private _caregroupRepository As ICareGroupRepository
    Private _invoiceRepository As IInvoiceRepository
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    Private _thirdpartyRepository As IThirdPartyRepository
    Private _contractRepository As Domain.Entities.IContractRepository
    Private _billingCategoryRepository As IBillingInvoiceCategories
    Private _ConceptsCausesStatusFolioRepository As IConceptsCausesStatusFolioRepository
    Private _posPathologiesRepository As IPOSPathologiesRepository
    Private _folioRepository As IFolioRepository
    Private _liquidationDataRepository As ILiquidationDataRepository
    Private _currencyAdminService As ICurrencyAdminService
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository
    Private _companySettingsRepository As ICompanySettingsRepository

    Public Sub New(revenueControlDetailRepository As IRevenueControlDetailRepository,
                   serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository,
                   caregroupRepository As ICareGroupRepository,
                   invoiceRepository As IInvoiceRepository,
                   thirdpartyRepository As IThirdPartyRepository,
                   contractRepository As Domain.Entities.IContractRepository,
                   billingCategoryRepository As IBillingInvoiceCategories,
                   conceptsCausesStatusFolioRepository As IConceptsCausesStatusFolioRepository,
                   posPathologiesRepository As IPOSPathologiesRepository,
                   healthAdministratorRepository As IHealthAdministratorRepository,
                   folioRepository As IFolioRepository,
                   LiquidationDataRepository As ILiquidationDataRepository,
                   CurrencyAdminService As ICurrencyAdminService,
                   GeneralLedgerIVARepository As IGeneralLedgerIVARepository,
                   CompanySettingsRepository As ICompanySettingsRepository)

        Me._revenueControlDetailRepository = revenueControlDetailRepository
        Me._serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
        Me._caregroupRepository = caregroupRepository
        Me._invoiceRepository = invoiceRepository
        Me._thirdpartyRepository = thirdpartyRepository
        Me._contractRepository = contractRepository
        Me._billingCategoryRepository = billingCategoryRepository
        Me._ConceptsCausesStatusFolioRepository = conceptsCausesStatusFolioRepository
        Me._posPathologiesRepository = posPathologiesRepository
        Me._healthAdministratorRepository = healthAdministratorRepository
        Me._folioRepository = folioRepository
        Me._liquidationDataRepository = LiquidationDataRepository
        Me._currencyAdminService = CurrencyAdminService
        Me._generalLedgerIVARepository = GeneralLedgerIVARepository
        Me._companySettingsRepository = CompanySettingsRepository
    End Sub

#Region "Methods"

    Function GetFolioDetails(ByVal revenueControlDetailId As Integer) As RequestResponse(Of Folio) Implements IFolioAdminService.GetFolioDetails
        Dim folio = _folioRepository.GetFolio(revenueControlDetailId)

        If folio Is Nothing Then
            Return New RequestResponse(Of Folio)(False, Nothing, Nothing, String.Format("Folio {0} doesnot exist", revenueControlDetailId))
        End If

        Return New RequestResponse(Of Folio)(True, HttpStatusCode.OK, folio, Nothing)
    End Function

    Function GetAnnullateFolio(ByVal invoiceId As Integer) As RequestResponse(Of AnnullateFolio) Implements IFolioAdminService.GetAnnullateFolio
        Dim folio = _folioRepository.GetAnnullateFolio(invoiceId)

        If folio Is Nothing Then
            Return New RequestResponse(Of AnnullateFolio)(False, Nothing, Nothing, String.Format("Folio {0} doesnot exist", invoiceId))
        End If

        Return New RequestResponse(Of AnnullateFolio)(True, HttpStatusCode.OK, folio, Nothing)
    End Function

    ''' <summary>
    ''' servicio que se encarga de recalcular el valor de los items de acuerdo a los datos de liquidacion
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <param name="_revenueControlDetailId"></param>
    ''' <returns></returns>
    Function RecalculateFolio(ByVal _admissionNumber As String, ByVal _revenueControlDetailId As Integer, audit As AuditMessage) As ActionResult Implements IFolioAdminService.RecalculateFolio
        If String.IsNullOrEmpty(_admissionNumber) OrElse _revenueControlDetailId = 0 Then
            Return New ActionResult With {.StateResult = False, .Message = $"Error Parametros vacios"}
        End If

        Dim unitWork As Domain.Base.IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

            Try
                Dim LiquidationData = _liquidationDataRepository.FirstOrDefault(Function(x) x.AdmissionNumber = _admissionNumber, False, {"LiquidationDataDetail"})
                If LiquidationData Is Nothing OrElse LiquidationData?.Id = 0 Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"No existen parámetros de liquidación para el ingreso"}
                End If

                Dim RevenueControlD = _revenueControlDetailRepository.FirstOrDefault(Function(x) x.Id = _revenueControlDetailId AndAlso x.RevenueControl.AdmissionNumber = _admissionNumber _
                                                                                     , True, {"RevenueControl", "ServiceOrderDetailDistribution.ServiceOrderDetail.GeneralLedgerIVA",
                                                                                                "ServiceOrderDetailDistribution.ServiceOrderDetail.CUPSEntity.CupsSubgroup.CupsGroup"})

                If RevenueControlD Is Nothing Then
                    Return New ActionResult With {.StateResult = False, .Message = "No se encontró información del folio"}
                End If

                Dim ListServiceODD = RevenueControlD?.ServiceOrderDetailDistribution?.ToList()

                If ListServiceODD Is Nothing OrElse Not ListServiceODD?.Any() Then

                    ' si el valor de cabecera del folio tiene valor pero ya no hay detalles establezco en cero los valor de cabecera
                    If RevenueControlD.TotalFolio > 0 Then
                        RevenueControlD.TotalFolio = 0
                        RevenueControlD.ValueCopay = 0
                        RevenueControlD.PatientDiscount = 0
                        '-------------------------------------------------------------------------------------------------
                        _revenueControlDetailRepository.SaveEntity(RevenueControlD)
                        unitWork.Commit()
                        scope.Complete()
                    End If

                    Return New ActionResult With {.StateResult = True, .Message = $"El folio #{RevenueControlD.FolioOrder} ({Utils.FolioMasterAccountNames.Item(RevenueControlD.IsMasterAccount)}) se recalculó exitosamentes"}
                End If

                'si el folio no esta abierto no se manda a recalcular
                If RevenueControlD.Status <> 1 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"No se puede recalcular el folio #{RevenueControlD.FolioOrder} ({Utils.FolioMasterAccountNames.Item(RevenueControlD.IsMasterAccount)}) ya que no está abierto"}
                End If

                'si el folio paciente tiene asociado un folio madre busco los items del folio original para posteriormente identificar los extras
                Dim IdsMasterServiceOder As List(Of Integer) = Nothing
                If RevenueControlD?.IsMasterAccount = eMasterAccount.PatientAccount AndAlso RevenueControlD?.RevenueControlDetailMasterId IsNot Nothing Then
                    Dim FolioMasterAccount = _revenueControlDetailRepository.FirstOrDefault(Function(x) x.Id = RevenueControlD.RevenueControlDetailMasterId.Value, False, {"ServiceOrderDetailDistribution"})
                    If FolioMasterAccount Is Nothing Then
                        Return New ActionResult With {.StateResult = False, .Message = $"No se encontró información del folio madre"}
                    End If
                    IdsMasterServiceOder = FolioMasterAccount.ServiceOrderDetailDistribution.Select(Function(d) d.ServiceOrderDetailId).ToList()
                End If

                'se manda a recalcular desde la base los folio cuenta madre, paciente y folio estandar CO. siempre y cuando :
                ' si tiene relacionado item de folio original  solo se recalculan los items extra que no concuerde la sumatoria de la sod con la de sodd,
                ' si es un folio que no tiene asociado cuenta madre (ya sea porque es cuenta madre o folio paciente particular o folio estandar) y las cantidades por el valor no concuerde o que sea
                ' folio estandar y tenga algun descuento.
                If ListServiceODD?.Any(Function(x) {eMasterAccount.MasterAccount, eMasterAccount.PatientAccount, eMasterAccount.NotMasterAccount}.Contains(RevenueControlD.IsMasterAccount)) Then

                    Parallel.ForEach(ListServiceODD.FindAll(Function(s)
                                                                If (IdsMasterServiceOder IsNot Nothing AndAlso Not IdsMasterServiceOder?.Contains(s.ServiceOrderDetailId) AndAlso s.SubTotalSalesPrice <> (s.ServiceOrderDetail?.GrossValue * s.ServiceOrderDetail?.InvoicedQuantity)) _
                                                                    OrElse (RevenueControlD?.RevenueControlDetailMasterId Is Nothing _
                                                                            AndAlso (s.SubTotalSalesPrice <> (s.ServiceOrderDetail?.GrossValue * s.ServiceOrderDetail?.InvoicedQuantity) OrElse (RevenueControlD.IsMasterAccount = eMasterAccount.NotMasterAccount AndAlso s.GrandTotalDiscount > 0))) Then
                                                                    Return True
                                                                Else
                                                                    Return False
                                                                End If
                                                            End Function), Sub(x)

                                                                               x.SubTotalSalesPrice = (x.ServiceOrderDetail?.GrossValue * x.ServiceOrderDetail?.InvoicedQuantity)
                                                                               Dim _percentage As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing, 0, x.ServiceOrderDetail?.GeneralLedgerIVA?.Percentage)
                                                                               x.GrandTotalTaxes = CalculateValueOfPercent(x.SubTotalSalesPrice, _percentage)
                                                                               x.GrandTotalSalesPrice = x.SubTotalSalesPrice + x.GrandTotalTaxes
                                                                               x.ThirdPartySalesPrice = x.GrandTotalSalesPrice
                                                                               x.GrandTotalDiscount = 0
                                                                               x.Quantity = x.ServiceOrderDetail.InvoicedQuantity
                                                                           End Sub)

                End If

                '--------------------------------------------------------------------------------------------------
                'Se crea bandera para establecer en 0 el iva, cuando el tercero es de tipo EXENTO (5)
                Dim flagTaxFree As Boolean = False
                If RevenueControlD?.IsMasterAccount <> eMasterAccount.MasterAccount Then
                    Dim _patientThirdParty = _thirdpartyRepository.FirstOrDefault(Function(s) s.Id = RevenueControlD.ThirdPartyId)
                    If _patientThirdParty Is Nothing OrElse _patientThirdParty?.Id = 0 Then
                        Return New ActionResult With {.StateResult = False, .Message = $"El tercero no existe"}
                    End If
                    flagTaxFree = (_patientThirdParty?.ContributionType = 5)
                End If
                '--------------------------------------------------------------------------------------------------

                Dim ErrorString = New StringBuilder
                Dim _sumFolio = ListServiceODD.Sum(Function(s) s.SubTotalSalesPrice)

                'Validación: el valor de copago no puede exceder el valor total del folio
                If LiquidationData?.CopaymentValue > _sumFolio AndAlso RevenueControlD?.IsMasterAccount = eMasterAccount.MasterAccount Then
                    Return New ActionResult With {.StateResult = False, .Message = $"No se logró recalcular ya que el valor de copago ({LiquidationData.CopaymentValue:N2}) excede el valor del Folio ({_sumFolio:N2})"}
                End If

                Parallel.ForEach((ListServiceODD), Sub(x)
                                                       x.ApportionmentPercent = If(_sumFolio = 0, 0, (x.SubTotalSalesPrice / _sumFolio))
                                                       Dim discountValueorPercentage As Decimal = 0
                                                       Dim _percent As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing OrElse flagTaxFree, 0, x.ServiceOrderDetail.GeneralLedgerIVA.Percentage)

                                                       'si el descuento esta vacio o la aplicacion es de tipo ninguno entonces es 0
                                                       If LiquidationData?.DiscountValueorPercentage IsNot Nothing AndAlso LiquidationData.ApplyDiscount <> 4 Then
                                                           discountValueorPercentage = LiquidationData.DiscountValueorPercentage
                                                       End If

                                                       'si el folio es cuenta madre 1 , 4 (paciente) o 0 (estandar), calculo el descuento en base a los datos de liquidacion
                                                       If {eMasterAccount.MasterAccount, eMasterAccount.PatientAccount, eMasterAccount.NotMasterAccount}.Contains(x.RevenueControlDetail?.IsMasterAccount) Then
                                                           Select Case LiquidationData?.ApplyDiscount
                                                               Case 1, 3
                                                                   x.GrandTotalDiscount = Math.Round(x.SubTotalSalesPrice * (discountValueorPercentage / 100), 2, MidpointRounding.AwayFromZero)

                                                               Case 2
                                                                   'si el folio es  4(paciente) y tiene descuento por valor, entonces saco el porcentaje del coaseguro del paciente por el valor del dcto, sino tomo el valor completo
                                                                   'y luego se prorratea entre cada item
                                                                   discountValueorPercentage = If(x.RevenueControlDetail?.IsMasterAccount = eMasterAccount.PatientAccount AndAlso LiquidationData.PatientCoinsurance > 0,
                                                                                                    CalculateValueOfPercent(discountValueorPercentage, LiquidationData.PatientCoinsurance),
                                                                                                    discountValueorPercentage)

                                                                   x.GrandTotalDiscount = Math.Round((discountValueorPercentage * x.ApportionmentPercent), 2, MidpointRounding.AwayFromZero)
                                                               Case Else
                                                                   x.GrandTotalDiscount = 0
                                                           End Select
                                                       End If

                                                       If x.RevenueControlDetail?.IsMasterAccount = eMasterAccount.MasterAccount Then
                                                           x.DeductibleValue = 0
                                                           x.CupsEntityId = x?.ServiceOrderDetail?.CUPSEntityId
                                                           x.SubGroupCupsId = x?.ServiceOrderDetail?.CUPSEntity?.CUPSSubGroupId
                                                           x.GroupCupsId = x?.ServiceOrderDetail?.CUPSEntity?.CupsSubgroup?.CupsGroupId
                                                           x.ProductId = x?.ServiceOrderDetail?.ProductId

                                                           Dim _result As ActionResult
                                                           _result = GenerateCoPayment(x, LiquidationData)
                                                           If Not _result.StateResult Then
                                                               ErrorString.AppendLine(_result.Message)
                                                               Exit Sub
                                                           End If
                                                       End If

                                                       Dim _dictionaryValues = Utils.SetValueSalesPrice(False, x.SubTotalSalesPrice - x.GrandTotalDiscount, _percent)

                                                       If _dictionaryValues?.Any() Then
                                                           x.GrandTotalTaxes = Math.Round(_dictionaryValues.FirstOrDefault(Function(g) g.Key = Utils.eTypeTaxControl.TaxValue).Value, 2, MidpointRounding.AwayFromZero)
                                                           x.GrandTotalSalesPrice = Math.Round(_dictionaryValues.FirstOrDefault(Function(g) g.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value, 2, MidpointRounding.AwayFromZero)
                                                           x.ThirdPartySalesPrice = x.GrandTotalSalesPrice
                                                       Else
                                                           ErrorString.AppendLine($"Error item {x.ServiceOrderDetailId}.")
                                                       End If
                                                   End Sub)
                If ErrorString?.Length > 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"Se presentarón los siguientes Errores : {ErrorString.ToString} "}
                End If

                'se valida que si existe en la carta de cobertura copago - que la suma de este valor en los detalles de SODD no generen diferencia con los datos de liquidacion
                If LiquidationData?.CopaymentValue > 0 AndAlso ListServiceODD?.Any(Function(s) s.SubTotalPatientSalesPrice > 0) Then
                    Dim Diff = LiquidationData.CopaymentValue - ListServiceODD.Sum(Function(s) s.SubTotalPatientSalesPrice)
                    ListServiceODD.FirstOrDefault(Function(x) x.SubTotalPatientSalesPrice >= Math.Abs(Diff)).SubTotalPatientSalesPrice += Diff
                End If

                '*********LIMITE VALOR COBERTURA ASEGURADORA GEN y ESPF ********************
                If LiquidationData?.LiquidationDataDetail?.Any(Function(x) x.InsurerCoveredValue > 0) AndAlso RevenueControlD?.IsMasterAccount = eMasterAccount.MasterAccount Then
                    For Each Item In LiquidationData?.LiquidationDataDetail.ToList().FindAll(Function(x) x.InsurerCoveredValue > 0)
                        Dim Result = EspecificInsurerCoveredValue(Item, ListServiceODD)
                        If Result Is Nothing OrElse Not Result?.StateResult Then
                            scope.Dispose()
                            Return New ActionResult With {.StateResult = False, .Message = Result?.Message}
                        End If
                    Next
                End If

                If LiquidationData?.ApplyGeneralLimits AndAlso LiquidationData?.InsurerCoveredValue > 0 AndAlso RevenueControlD?.IsMasterAccount = eMasterAccount.MasterAccount Then
                    Dim Result = EspecificInsurerCoveredValue(New LiquidationDataDetail With {.ConditionType = EConditionType.None, .LimitType = ELimitType.TotalValue,
                                                     .TaxInclude = False, .InsurerCoveredValue = LiquidationData.InsurerCoveredValue}, ListServiceODD)

                    If Result Is Nothing OrElse Not Result?.StateResult Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .Message = Result?.Message}
                    End If
                End If
                '****************************************************************************

                RevenueControlD.TotalFolio = ListServiceODD.Sum(Function(x) x.GrandTotalSalesPrice)
                RevenueControlD.ValueCopay = ListServiceODD.Sum(Function(x) x.SubTotalPatientSalesPrice)
                RevenueControlD.PatientDiscount = ListServiceODD.Sum(Function(x) x.GrandTotalDiscount)

                'si el folio es Madre o algun derivado seteamos el valor total paciente y valor total paciente con descuento en 0
                If RevenueControlD.IsMasterAccount = eMasterAccount.NotMasterAccount Then
                    RevenueControlD.TotalPatientSalesPrice = RevenueControlD.ValueCopay
                    RevenueControlD.TotalPatientWithDiscount = RevenueControlD.ValueCopay
                Else
                    RevenueControlD.TotalPatientSalesPrice = 0
                    RevenueControlD.TotalPatientWithDiscount = 0
                End If
                '-------------------------------------------------------------------------------------------------
                _revenueControlDetailRepository.SaveEntity(RevenueControlD)
                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = $"El folio #{RevenueControlD.FolioOrder} ({Utils.FolioMasterAccountNames.Item(RevenueControlD.IsMasterAccount)}) se recalculó exitosamente"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function


    ''' <summary>
    ''' Funcion que se encarga de calcular el Copago
    ''' </summary>
    ''' <param name="ServiceODD"></param>
    ''' <param name="LiquidationData"></param>
    ''' <returns></returns>
    Function GenerateCoPayment(ByRef ServiceODD As ServiceOrderDetailDistribution, ByVal LiquidationData As LiquidationData) As ActionResult
        If ServiceODD Is Nothing OrElse LiquidationData Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = $"Error parámetro vacío función Copago."}
        End If

        With ServiceODD
            'calculo copago paciente
            .SubTotalPatientSalesPrice = Math.Round(LiquidationData.CopaymentValue * .ApportionmentPercent, 2, MidpointRounding.AwayFromZero)
            If .SubTotalSalesPrice = 0 Then
                .PatientPercentage = 0
                .ThirdPartyPercentage = 0
            Else
                .PatientPercentage = Math.Round((.SubTotalPatientSalesPrice * 100) / .SubTotalSalesPrice, 2)
                .ThirdPartyPercentage = Math.Round(100 - ((.SubTotalPatientSalesPrice * 100) / .SubTotalSalesPrice), 2, MidpointRounding.AwayFromZero)
            End If
            .InsurerCoveredValue = 0
        End With
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' aplicacion de limites de valor cubierto aseguradora
    ''' </summary>
    ''' <returns></returns>
    Function EspecificInsurerCoveredValue(ByVal liquidationDataDetail As LiquidationDataDetail, ByRef masterAccount As List(Of ServiceOrderDetailDistribution)) As ActionResult
        If liquidationDataDetail Is Nothing OrElse masterAccount Is Nothing OrElse Not masterAccount?.Any() Then
            Return New ActionResult With {.StateResult = False, .Message = $"Parámetros de entrada función aplicación de cobertura aseguradora, vacío!"}
        End If

        If liquidationDataDetail.LimitType = ELimitType.UnitValue AndAlso liquidationDataDetail.InsurerCoveredValue = 0 Then
            Return New ActionResult With {.StateResult = True}
        End If

        Dim sODDmasterAccount = masterAccount.ToList().Where(Function(d)
                                                                 Dim ListIds = liquidationDataDetail?.ConditionsItemsIds?.Split(",")?.ToList()
                                                                 Select Case liquidationDataDetail.ConditionType
                                                                     Case EConditionType.Cups
                                                                         If d.CupsEntityId Is Nothing Then
                                                                             Return False
                                                                         End If
                                                                         Return ListIds.Contains(d.CupsEntityId)
                                                                     Case EConditionType.SubGroupCups
                                                                         If d.SubGroupCupsId Is Nothing Then
                                                                             Return False
                                                                         End If
                                                                         Return ListIds.Contains(d.SubGroupCupsId)
                                                                     Case EConditionType.GroupsCups
                                                                         If d.GroupCupsId Is Nothing Then
                                                                             Return False
                                                                         End If
                                                                         Return ListIds.Contains(d.GroupCupsId)
                                                                     Case EConditionType.Product
                                                                         If d.ProductId Is Nothing Then
                                                                             Return False
                                                                         End If
                                                                         Return ListIds.Contains(d.ProductId)
                                                                     Case Else
                                                                         Return True
                                                                 End Select
                                                             End Function).ToList()


        Dim sumFolio As Decimal = 0
        sumFolio = sODDmasterAccount.Sum(Function(s) s.SubTotalSalesPrice - s.GrandTotalDiscount)

        'para cuando el limite es total ya sea (especifico o general) 
        If sumFolio < liquidationDataDetail.InsurerCoveredValue Then
            liquidationDataDetail.InsurerCoveredValue = sumFolio
        End If

        If sODDmasterAccount?.Any(Function(f) f.InsurerCoveredValue > 0) Then
            liquidationDataDetail.InsurerCoveredValue -= sODDmasterAccount.Sum(Function(s) s.InsurerCoveredValue)
        End If

        If liquidationDataDetail.InsurerCoveredValue < 0 Then
            Return New ActionResult With {.StateResult = False, .Message = "Existe una mala parametrización. El valor de cobertura especifico supera el general"}
        End If

        Parallel.ForEach(sODDmasterAccount, Sub(x)
                                                Dim ApportionmentPercent = ((x.SubTotalSalesPrice - x.GrandTotalDiscount) / sumFolio) * 100
                                                x.InsurerCoveredValue += CalculateValueOfPercent(liquidationDataDetail.InsurerCoveredValue, ApportionmentPercent)
                                            End Sub)


        'se valida que si existe en la carta de cobertura "valor cobertura aseguradora" - que la suma de este valor en los detalles de SODD no generen diferencia con los datos de liquidacion
        If liquidationDataDetail?.InsurerCoveredValue > 0 AndAlso sODDmasterAccount?.Any(Function(s) s.InsurerCoveredValue > 0) Then
            Dim Diff = liquidationDataDetail.InsurerCoveredValue - sODDmasterAccount.Sum(Function(s) s.InsurerCoveredValue)
            sODDmasterAccount.Find(Function(x) x.SubTotalSalesPrice >= Diff).InsurerCoveredValue += Diff
        End If

        Return New ActionResult With {.StateResult = True, .Message = "Proceso de valor de cobertura exitoso"}
    End Function

    ''' <summary>
    ''' calula el valor del iva
    ''' </summary>
    ''' <param name="Value"> valor neto (valor sin iva  - descuento)</param>
    ''' <param name="Percent">% del iva</param>
    ''' <returns></returns>
    Function CalculateValueOfPercent(ByVal Value As Decimal?, Percent As Decimal?) As Decimal
        If Value Is Nothing OrElse Percent Is Nothing OrElse Value = 0 OrElse Percent = 0 Then
            Return 0
        End If
        Return Math.Round(Value.Value * (Percent.Value / 100), 2, MidpointRounding.AwayFromZero)
    End Function

    ''' <summary>
    ''' funcion que se encarga de dividir la cuenta madre en dos folios (entidad - paciente)
    ''' </summary>
    ''' <returns></returns>
    Function SeparateAccount(ByVal _admissionNumber As String, ByVal _revenueControlDetailId As Integer, audit As AuditMessage) As ActionResult Implements IFolioAdminService.SeparateAccount

        If String.IsNullOrEmpty(_admissionNumber) OrElse _revenueControlDetailId = 0 Then
            Return New ActionResult With {.StateResult = False, .Message = $"Error Parametros vacios"}
        End If

        Dim unitWork As Domain.Base.IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

            Try
                Dim LiquidationData = _liquidationDataRepository.FirstOrDefault(Function(x) x.AdmissionNumber = _admissionNumber, False, {"LiquidationDataDetail"})

                If LiquidationData Is Nothing OrElse LiquidationData?.Id = 0 Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"No existen parámetros de liquidación para el ingreso"}
                End If

                If (LiquidationData.InsuranceCoinsurance + LiquidationData.PatientCoinsurance) <> 100 Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"La suma de los porcentajes de Coaseguro no suman el 100%. Paciente = {LiquidationData.PatientCoinsurance}% , Aseguradora = {LiquidationData.InsuranceCoinsurance}% "}
                End If

                Dim RevenueControlD = _revenueControlDetailRepository.GetByFilter(Function(x) x.Id = _revenueControlDetailId AndAlso x.RevenueControl.AdmissionNumber = _admissionNumber _
                                                                                   AndAlso x.IsMasterAccount = 1, True,
                                                                                {"RevenueControl", "ServiceOrderDetailDistribution.ServiceOrderDetail.GeneralLedgerIVA",
                                                                                "ServiceOrderDetailDistribution.ServiceOrderDetail.CUPSEntity.CupsSubgroup.CupsGroup"}).FirstOrDefault

                If RevenueControlD Is Nothing OrElse RevenueControlD?.Id = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"No se encontró información del folio o no es cuenta madre"}
                End If

                '**********************************************************************************
                'REVISAR SI ES PACIENTE O RESPONSABLE
                Dim _patientThirdParty = _thirdpartyRepository.FirstOrDefault(Function(s) s.Nit = RevenueControlD.RevenueControl.PatientCode)

                If _patientThirdParty Is Nothing OrElse _patientThirdParty?.Id = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"El paciente no está creado como tercero"}
                End If

                'Identifica si el paciente es excento de IVA
                Dim flagTaxFreePatient As Boolean = (_patientThirdParty?.ContributionType = 5)

                '**********************************************************************************
                'ASEGURADORA RESPONSABLE **********************************************************
                'Identifica si la aseguradora es excento de IVA
                Dim insuranceThirdParty = _thirdpartyRepository.FirstOrDefault(Function(s) s.Id = RevenueControlD.ThirdPartyId)

                If insuranceThirdParty Is Nothing OrElse insuranceThirdParty?.Id = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"La entidad no está creado como tercero"}
                End If
                Dim flagTaxFreeInsurance As Boolean = (insuranceThirdParty?.ContributionType = 5)
                '**********************************************************************************

                Dim ListServiceODD = RevenueControlD.ServiceOrderDetailDistribution.ToList()

                'si dentro del folio de la cuenta madre algun subtotal esta en cero se manda a recalcular el mismo
                '------------------------------------------------------------------------------------------------------------------------
                If ListServiceODD?.Any(Function(x) x.SubTotalSalesPrice = 0) OrElse (LiquidationData.CopaymentValue > 0 AndAlso ListServiceODD?.Any(Function(x) x.SubTotalPatientSalesPrice = 0)) _
                    OrElse (LiquidationData.CopaymentValue = 0 AndAlso ListServiceODD?.Any(Function(x) x.SubTotalPatientSalesPrice > 0)) OrElse LiquidationData.InsurerCoveredValue > 0 OrElse LiquidationData?.
                                                                                                                                                                                                LiquidationDataDetail?.Any(Function(s) s.InsurerCoveredValue > 0) Then
                    Dim result = RecalculateFolio(_admissionNumber, _revenueControlDetailId, audit)
                    If result Is Nothing OrElse Not result.StateResult Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = result.StateResult, .Message = $"Se mando a recálcular el folio pero : {result.Message}"}
                    End If

                    RevenueControlD = _revenueControlDetailRepository.GetByFilter(Function(x) x.Id = _revenueControlDetailId AndAlso x.RevenueControl.AdmissionNumber = _admissionNumber _
                                                                                   AndAlso x.IsMasterAccount = 1, True,
                                                                                {"RevenueControl", "ServiceOrderDetailDistribution.ServiceOrderDetail.GeneralLedgerIVA",
                                                                                "ServiceOrderDetailDistribution.ServiceOrderDetail.CUPSEntity.CupsSubgroup.CupsGroup"}).FirstOrDefault
                    ListServiceODD = RevenueControlD.ServiceOrderDetailDistribution.ToList()
                End If
                '--------------------------------------------------------------------------------------------------
                Dim RevenueControlDInsurance = New RevenueControlDetail
                Dim RevenueControlDPatient = New RevenueControlDetail
                Dim _sumFolio = ListServiceODD.Sum(Function(s) s.SubTotalSalesPrice)
                Dim Lock As New Object
                Dim coInsuranceValidation = New ConcurrentBag(Of String)
                Parallel.ForEach((ListServiceODD), Sub(x)

                                                       Dim result = ValidateAndGetCoinsurance(LiquidationData, x)

                                                       If result Is Nothing OrElse Not result?.StateResult Then
                                                           coInsuranceValidation.Add(result?.Message)
                                                           Exit Sub
                                                       End If

                                                       'se establece el % de coaseguro puede ser de los parametros generales o especificos
                                                       Dim InsuranceCoinsurance = result.ObjectEmbbeded.Item(ECoInsurance.InsurerCoveredValue)
                                                       Dim PatientCoinsurance = result.ObjectEmbbeded.Item(ECoInsurance.PatientCoinsurance)

                                                       x.ApportionmentPercent = (x.SubTotalSalesPrice / _sumFolio) * 100
                                                       x.DeductibleValue = CalculateValueOfPercent(LiquidationData.DeductibleValue, x.ApportionmentPercent)

                                                       Dim _sODDInsurance = New ServiceOrderDetailDistribution
                                                       Dim _sODDPatient = New ServiceOrderDetailDistribution
                                                       Dim NetoCop As Decimal = x.SubTotalSalesPrice - x.GrandTotalDiscount - x.SubTotalPatientSalesPrice - x.InsurerCoveredValue
                                                       Dim Base As Decimal = 0

                                                       _sODDInsurance.GrandTotalDiscount = CalculateValueOfPercent(x.GrandTotalDiscount, InsuranceCoinsurance)
                                                       _sODDPatient.GrandTotalDiscount = CalculateValueOfPercent(x.GrandTotalDiscount, PatientCoinsurance)

                                                       If Not LiquidationData.ApplyDeductible Then
                                                           '**********ANTES DE COASEGURO**********
                                                           x.DeductibleValue = If(x.DeductibleValue > NetoCop, NetoCop, x.DeductibleValue)
                                                           Base = NetoCop - x.DeductibleValue
                                                           '-------------ASEGURADORA--------------------
                                                           _sODDInsurance.SubTotalSalesPrice = CalculateValueOfPercent(Base, InsuranceCoinsurance) + _sODDInsurance.GrandTotalDiscount + x.InsurerCoveredValue
                                                           '***********************************************
                                                           '---------------PACIENTE---------------------
                                                           _sODDPatient.SubTotalSalesPrice = CalculateValueOfPercent(Base, PatientCoinsurance) + x.SubTotalPatientSalesPrice + x.DeductibleValue + _sODDPatient.GrandTotalDiscount
                                                           '-----------------------------------------------
                                                       Else
                                                           '**********DESPUES DE COASEGURO**********
                                                           Base = CalculateValueOfPercent(NetoCop, InsuranceCoinsurance)
                                                           x.DeductibleValue = If(x.DeductibleValue > Base, Base, x.DeductibleValue)
                                                           '---------------ASEGURADORA--------------------
                                                           _sODDInsurance.SubTotalSalesPrice = Base - x.DeductibleValue + _sODDInsurance.GrandTotalDiscount + x.InsurerCoveredValue
                                                           '*************************************************
                                                           '---------------PACIENTE---------------------
                                                           _sODDPatient.SubTotalSalesPrice = CalculateValueOfPercent(NetoCop, PatientCoinsurance) + x.SubTotalPatientSalesPrice + x.DeductibleValue + _sODDPatient.GrandTotalDiscount
                                                           ''--------------------------------------------------
                                                       End If

                                                       '---------------RECALCULO DESCUENTO----------------------
                                                       Dim discountValueorPercentage = LiquidationData.DiscountValueorPercentage
                                                       Select Case LiquidationData?.ApplyDiscount
                                                           Case 1, 3
                                                               _sODDInsurance.GrandTotalDiscount = Math.Round(_sODDInsurance.SubTotalSalesPrice * (LiquidationData.DiscountValueorPercentage.Value / 100), 2, MidpointRounding.AwayFromZero)
                                                               _sODDPatient.GrandTotalDiscount = Math.Round(_sODDPatient.SubTotalSalesPrice * (LiquidationData.DiscountValueorPercentage.Value / 100), 2, MidpointRounding.AwayFromZero)
                                                           Case 2
                                                               Dim discountValuePatient = CalculateValueOfPercent(discountValueorPercentage, PatientCoinsurance)
                                                               Dim discountValueInsurer = CalculateValueOfPercent(discountValueorPercentage, InsuranceCoinsurance)

                                                               _sODDPatient.GrandTotalDiscount = Math.Round((discountValuePatient * (x.ApportionmentPercent / 100)), 2, MidpointRounding.AwayFromZero)
                                                               _sODDInsurance.GrandTotalDiscount = Math.Round((discountValueInsurer * (x.ApportionmentPercent / 100)), 2, MidpointRounding.AwayFromZero)
                                                           Case Else
                                                               _sODDInsurance.GrandTotalDiscount = 0
                                                               _sODDPatient.GrandTotalDiscount = 0
                                                       End Select
                                                       '------------------------------------------------------------------------
                                                       '------------------ASEGURADORA--------------------------------------------
                                                       _sODDInsurance.SubTotalPatientSalesPrice = 0
                                                       _sODDInsurance.PatientPercentage = 0
                                                       _sODDInsurance.ThirdPartyPercentage = 100
                                                       _sODDInsurance.InsurerCoveredValue = x.InsurerCoveredValue
                                                       _sODDInsurance.GrandTotalTaxes = CalculateValueOfPercent((_sODDInsurance.SubTotalSalesPrice - _sODDInsurance.GrandTotalDiscount), If(flagTaxFreeInsurance, 0, x.ServiceOrderDetail?.GeneralLedgerIVA?.Percentage))
                                                       _sODDInsurance.GrandTotalSalesPrice = (_sODDInsurance.SubTotalSalesPrice - _sODDInsurance.GrandTotalDiscount) + _sODDInsurance.GrandTotalTaxes
                                                       _sODDInsurance.ThirdPartySalesPrice = _sODDInsurance.GrandTotalSalesPrice
                                                       _sODDInsurance.ServiceOrderDetailId = x.ServiceOrderDetailId
                                                       _sODDInsurance.Quantity = x.Quantity
                                                       _sODDInsurance.DistributionType = x.DistributionType
                                                       _sODDInsurance.ApplyRecoveryFee = x.ApplyRecoveryFee
                                                       _sODDInsurance.LastCaregroupId = x.LastCaregroupId
                                                       _sODDInsurance.DeductibleValue = x.DeductibleValue
                                                       _sODDInsurance.CupsEntityId = x?.ServiceOrderDetail?.CUPSEntityId
                                                       _sODDInsurance.SubGroupCupsId = x?.ServiceOrderDetail?.CUPSEntity?.CUPSSubGroupId
                                                       _sODDInsurance.GroupCupsId = x?.ServiceOrderDetail?.CUPSEntity?.CupsSubgroup?.CupsGroupId
                                                       _sODDInsurance.ProductId = x?.ServiceOrderDetail?.ProductId
                                                       _sODDInsurance.ServiceOrderDetail = x.ServiceOrderDetail
                                                       '---------------------------------------------------------------------------
                                                       '------------------PACIENTE-------------------------------------------------                                                       
                                                       _sODDPatient.SubTotalPatientSalesPrice = x.SubTotalPatientSalesPrice
                                                       _sODDPatient.PatientPercentage = If(_sODDPatient.SubTotalSalesPrice = 0, 0, Math.Round((_sODDPatient.SubTotalPatientSalesPrice * 100) / _sODDPatient.SubTotalSalesPrice, 2, MidpointRounding.AwayFromZero))
                                                       _sODDPatient.ThirdPartyPercentage = If(_sODDPatient.SubTotalSalesPrice = 0, 0, Math.Round(100 - ((_sODDPatient.SubTotalPatientSalesPrice * 100) / _sODDPatient.SubTotalSalesPrice), 2, MidpointRounding.AwayFromZero))

                                                       _sODDPatient.GrandTotalTaxes = CalculateValueOfPercent((_sODDPatient.SubTotalSalesPrice - _sODDPatient.GrandTotalDiscount),
                                                                                                              If(flagTaxFreePatient, 0, x.ServiceOrderDetail?.GeneralLedgerIVA?.Percentage))

                                                       _sODDPatient.GrandTotalSalesPrice = (_sODDPatient.SubTotalSalesPrice - _sODDPatient.GrandTotalDiscount) + _sODDPatient.GrandTotalTaxes
                                                       _sODDPatient.ThirdPartySalesPrice = _sODDPatient.GrandTotalSalesPrice
                                                       _sODDPatient.ServiceOrderDetailId = x.ServiceOrderDetailId
                                                       _sODDPatient.Quantity = x.Quantity
                                                       _sODDPatient.DistributionType = x.DistributionType
                                                       _sODDPatient.ApplyRecoveryFee = x.ApplyRecoveryFee
                                                       _sODDPatient.LastCaregroupId = x.LastCaregroupId
                                                       _sODDPatient.DeductibleValue = x.DeductibleValue
                                                       _sODDPatient.ServiceOrderDetail = x.ServiceOrderDetail
                                                       'se añaden a los folios de manera sincrona para evitar, perdida de datos o items fantasma( nothing) por concurrencia
                                                       SyncLock Lock
                                                           RevenueControlDPatient.ServiceOrderDetailDistribution.Add(_sODDPatient)
                                                           RevenueControlDInsurance.ServiceOrderDetailDistribution.Add(_sODDInsurance)
                                                       End SyncLock
                                                   End Sub)

                If coInsuranceValidation?.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = $"Validacion separación : {String.Join(",", coInsuranceValidation)}"}
                End If

                With RevenueControlD
                    RevenueControlDInsurance.BillingAuthorizationId = .BillingAuthorizationId
                    RevenueControlDPatient.BillingAuthorizationId = .BillingAuthorizationId

                    RevenueControlDInsurance.CareGroupId = .CareGroupId
                    RevenueControlDPatient.CareGroupId = .CareGroupId

                    RevenueControlDInsurance.ContractEntityId = .ContractEntityId
                    RevenueControlDPatient.ContractEntityId = .ContractEntityId

                    RevenueControlDInsurance.FolioType = .FolioType
                    RevenueControlDPatient.FolioType = 3

                    RevenueControlDInsurance.LiquidationType = .LiquidationType
                    RevenueControlDPatient.LiquidationType = .LiquidationType

                    RevenueControlDInsurance.HealthAdministratorId = .HealthAdministratorId
                    RevenueControlDPatient.HealthAdministratorId = .HealthAdministratorId

                    RevenueControlDInsurance.IsCutAccount = .IsCutAccount
                    RevenueControlDPatient.IsCutAccount = .IsCutAccount

                    RevenueControlDInsurance.IsMasterAccount = 2
                    RevenueControlDPatient.IsMasterAccount = 4

                    RevenueControlDInsurance.Status = 3
                    RevenueControlDPatient.Status = 1
                    .Status = 3
                    RevenueControlDInsurance.RevenueControlId = .RevenueControlId
                    RevenueControlDPatient.RevenueControlId = .RevenueControlId

                    RevenueControlDInsurance.ThirdPartyId = .ThirdPartyId
                    RevenueControlDInsurance.ThirdParty = insuranceThirdParty
                    RevenueControlDPatient.ThirdPartyId = _patientThirdParty?.Id
                    RevenueControlDPatient.ThirdParty = _patientThirdParty

                    RevenueControlDInsurance.CreationDate = DateTime.Now
                    RevenueControlDPatient.CreationDate = DateTime.Now

                    RevenueControlDPatient.CreationUser = audit.CodeUser
                    RevenueControlDInsurance.CreationUser = audit.CodeUser

                    RevenueControlDInsurance.FolioOrder = .RevenueControl.FolioQuantity + 1
                    RevenueControlDPatient.FolioOrder = RevenueControlDInsurance.FolioOrder + 1

                    .IsMasterAccount = 3
                    .RevenueControl.FolioQuantity += 2
                    '----TOTALIZADOS---------------------------------------------------------------------------------------------------------------------
                    RevenueControlDPatient.ValueCopay = .ServiceOrderDetailDistribution.Sum(Function(x) x.SubTotalPatientSalesPrice)
                    RevenueControlDInsurance.ValueCopay = .ServiceOrderDetailDistribution.Sum(Function(x) x.InsurerCoveredValue)

                    If RevenueControlDInsurance?.ServiceOrderDetailDistribution?.Any() Then
                        RevenueControlDInsurance.PatientDiscount = RevenueControlDInsurance?.ServiceOrderDetailDistribution?.Where(Function(s) s IsNot Nothing)?.Sum(Function(x) x.GrandTotalDiscount)
                    End If

                    If RevenueControlDPatient?.ServiceOrderDetailDistribution?.Any() Then
                        RevenueControlDPatient.PatientDiscount = RevenueControlDPatient?.ServiceOrderDetailDistribution?.Where(Function(s) s IsNot Nothing)?.Sum(Function(x) x.GrandTotalDiscount)
                    End If
                End With

                '-------------------------------------------------------------------------------------------------
                If LiquidationData?.LiquidationDataDetail?.Any() Then
                    For Each Item In LiquidationData?.LiquidationDataDetail.ToList()
                        Dim Result = ApplyLimitsInFolios(Item, RevenueControlDPatient, RevenueControlDInsurance, False, LiquidationData)
                        If Result Is Nothing OrElse Not Result?.StateResult Then
                            scope.Dispose()
                            Return New ActionResult With {.StateResult = False, .Message = Result?.Message}
                        End If
                    Next
                End If

                If LiquidationData?.ApplyGeneralLimits Then
                    Dim Result = ApplyLimitsInFolios(New LiquidationDataDetail With {.ConditionType = EConditionType.None, .LimitType = ELimitType.TotalValue,
                                                     .TaxInclude = LiquidationData.TaxInclude, .LimitValue = LiquidationData.LimitValue}, RevenueControlDPatient, RevenueControlDInsurance, False, LiquidationData)

                    If Result Is Nothing OrElse Not Result?.StateResult Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .Message = Result?.Message}
                    End If
                End If

                If LiquidationData IsNot Nothing AndAlso LiquidationData?.PatientCoinsuranceLimitValue > 0 Then

                    Dim Result = ApplyLimitsInFolios(New LiquidationDataDetail With {.ConditionType = EConditionType.None, .LimitType = ELimitType.TotalValue,
                                                     .TaxInclude = LiquidationData.TaxInclude, .LimitValue = LiquidationData.PatientCoinsuranceLimitValue}, RevenueControlDPatient, RevenueControlDInsurance,
                                                                                                                                                            True, LiquidationData)

                    If Result Is Nothing OrElse Not Result?.StateResult Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .Message = Result?.Message}
                    End If

                End If

                RevenueControlDInsurance.TotalFolio = RevenueControlDInsurance.ServiceOrderDetailDistribution.Where(Function(s) s IsNot Nothing).Sum(Function(x) x.GrandTotalSalesPrice)
                RevenueControlDPatient.TotalFolio = RevenueControlDPatient.ServiceOrderDetailDistribution.Where(Function(s) s IsNot Nothing).Sum(Function(x) x.GrandTotalSalesPrice)
                '------------------------------------------------------------------------------------------------

                _revenueControlDetailRepository.SaveEntity(RevenueControlD)
                RevenueControlDInsurance.RevenueControlDetailMasterId = RevenueControlD.Id
                RevenueControlDPatient.RevenueControlDetailMasterId = RevenueControlD.Id

                Dim List = New List(Of RevenueControlDetail)
                List = {RevenueControlDInsurance, RevenueControlDPatient}.ToList()

                List.ForEach(Sub(x)
                                 _revenueControlDetailRepository.SaveEntity(x)
                             End Sub)

                Me._revenueControlDetailRepository.ExecuteNonQuery("INSERT INTO Billing.LiquidationDataSeparated
                                                                    (   AdmissionNumber,
		                                                                ApplyDiscount,
		                                                                DiscountValueorPercentage,
		                                                                DeductibleValue,
		                                                                ApplyDeductible,
		                                                                CopaymentValue,
		                                                                InsuranceCoinsurance,
		                                                                PatientCoinsurance,
		                                                                CreationUser,
		                                                                CreationDate,
		                                                                ModificationUser,
		                                                                ModificationDate,
		                                                                ApplyGeneralLimits,
		                                                                TaxInclude,
		                                                                LimitValue,
		                                                                InsurerCoveredValue,
		                                                                PatientCoinsuranceLimitValue) 
                                                                    Values ({0},{1},{2},{3},{4},
                                                                             {5},{6},{7},{8},{9},{10},
                                                                                {11},{12},{13},{14},{15},{16})", LiquidationData.AdmissionNumber,
                                                                                                                LiquidationData.ApplyDiscount,
                                                                                                                LiquidationData.DiscountValueorPercentage,
                                                                                                                LiquidationData.DeductibleValue,
                                                                                                                LiquidationData.ApplyDeductible,
                                                                                                                LiquidationData.CopaymentValue,
                                                                                                                LiquidationData.InsuranceCoinsurance,
                                                                                                                LiquidationData.PatientCoinsurance,
                                                                                                                LiquidationData.CreationUser,
                                                                                                                LiquidationData.CreationDate,
                                                                                                                LiquidationData.ModificationUser,
                                                                                                                LiquidationData.ModificationDate,
                                                                                                                LiquidationData.ApplyGeneralLimits,
                                                                                                                LiquidationData.TaxInclude,
                                                                                                                LiquidationData.LimitValue,
                                                                                                                LiquidationData.InsurerCoveredValue,
                                                                                                                LiquidationData.PatientCoinsuranceLimitValue)

                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "La separación de cuentas fue exitosa"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = String.Join(";", Utils.GetInnerExceptionMessages(ex))}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para aplicarlos limites especificos o generales
    ''' </summary>
    ''' <param name="_liquidationDataDetail">detalle de los datos de liquidacion, para cuando es general se arma la entidad del detalle de l siguiente forma :
    ''' {.ConditionType = 0, .LimitType = eLimitType.TotalValue,.TaxInclude = LiquidationData.TaxInclude, .LimitValue = LiquidationData.LimitValue}</param>
    ''' <param name="_accountPatient">folio paciente</param>
    ''' <param name="_accountInsurer">folio aseguradora</param>
    ''' <returns></returns>
    Function ApplyLimitsInFolios(ByVal _liquidationDataDetail As LiquidationDataDetail, ByRef _accountPatient As RevenueControlDetail, ByRef _accountInsurer As RevenueControlDetail, ByVal _limitPatient As Boolean, ByVal liquidationData As LiquidationData) As ActionResult

        If _accountPatient Is Nothing OrElse _accountInsurer Is Nothing OrElse _liquidationDataDetail Is Nothing OrElse (_liquidationDataDetail.ConditionType <> 0 AndAlso String.IsNullOrEmpty(_liquidationDataDetail.ConditionsItemsIds)) Then
            Return New ActionResult With {.StateResult = False, .Message = $"Parámetros de entrada función aplicación de limites, vacío!"}
        End If

        Dim _SODDPatient = _accountPatient.ServiceOrderDetailDistribution.ToList()
        'se evalua si el tercero del folio paciente es tipo de contributor EXENTO(5)
        Dim flagTaxFreePatient As Boolean = (_accountPatient?.ThirdParty IsNot Nothing AndAlso _accountPatient?.ThirdParty?.ContributionType = 5)
        'se evalua si el tercero del folio Aseguradora es tipo de contributor EXENTO(5)
        Dim flagTaxFreeInsurer As Boolean = (_accountInsurer?.ThirdParty IsNot Nothing AndAlso _accountInsurer?.ThirdParty?.ContributionType = 5)

        Dim Lock As New Object
        Dim _SODDInsurer = _accountInsurer.ServiceOrderDetailDistribution.ToList().Where(Function(d)
                                                                                             Dim ListIds = _liquidationDataDetail?.ConditionsItemsIds?.Split(",")?.ToList()
                                                                                             Select Case _liquidationDataDetail.ConditionType
                                                                                                 Case EConditionType.Cups
                                                                                                     If d.CupsEntityId Is Nothing Then
                                                                                                         Return False
                                                                                                     End If
                                                                                                     Return ListIds.Contains(d.CupsEntityId)
                                                                                                 Case EConditionType.SubGroupCups
                                                                                                     If d.SubGroupCupsId Is Nothing Then
                                                                                                         Return False
                                                                                                     End If
                                                                                                     Return ListIds.Contains(d.SubGroupCupsId)
                                                                                                 Case EConditionType.GroupsCups
                                                                                                     If d.GroupCupsId Is Nothing Then
                                                                                                         Return False
                                                                                                     End If
                                                                                                     Return ListIds.Contains(d.GroupCupsId)
                                                                                                 Case EConditionType.Product
                                                                                                     If d.ProductId Is Nothing Then
                                                                                                         Return False
                                                                                                     End If
                                                                                                     Return ListIds.Contains(d.ProductId)
                                                                                                 Case Else
                                                                                                     Return True
                                                                                             End Select
                                                                                         End Function).ToList()

        Dim _sumFolio As Decimal = 0
        _sumFolio = If(Not _limitPatient,
                            _SODDInsurer.Sum(Function(s) If(_liquidationDataDetail.TaxInclude, s.GrandTotalSalesPrice, (s.SubTotalSalesPrice - s.GrandTotalDiscount))),
                            _SODDPatient.Sum(Function(s) If(_liquidationDataDetail.TaxInclude, s.GrandTotalSalesPrice, (s.SubTotalSalesPrice - s.GrandTotalDiscount))))

        'para cuando el limite es total ya sea (especifico o general) se valida, si el folio supera el limite para aplicarlo o si no que se salga de la funcion
        If (_sumFolio < _liquidationDataDetail.LimitValue OrElse _liquidationDataDetail.LimitValue = 0) AndAlso _liquidationDataDetail.LimitType <> ELimitType.UnitValue Then
            Return New ActionResult With {.StateResult = True}
        End If

        'si la cantidad del valor del limite especifico unitario viene en 0 solo el limite sera por valor
        If _liquidationDataDetail.LimitType = ELimitType.UnitValue AndAlso _liquidationDataDetail.Quantity = 0 Then _liquidationDataDetail.Quantity = 99999

        If Not _limitPatient Then

            Parallel.ForEach(_SODDInsurer.OrderByDescending(Function(a) a.Quantity), Sub(x)
                                                                                         Dim percentInsurer As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing OrElse flagTaxFreeInsurer, 0, x.ServiceOrderDetail.GeneralLedgerIVA.Percentage)
                                                                                         Dim percentPatient As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing OrElse flagTaxFreePatient, 0, x.ServiceOrderDetail.GeneralLedgerIVA.Percentage)
                                                                                         Dim _diffSubtotal As Decimal
                                                                                         Dim _valueProcess = If(_liquidationDataDetail.TaxInclude, x.GrandTotalSalesPrice, (x.SubTotalSalesPrice - x.GrandTotalDiscount))
                                                                                         Dim _dictionaryValues As Dictionary(Of Utils.eTypeValue, Decimal)
                                                                                         x.ApportionmentPercent = (_valueProcess / _sumFolio) * 100

                                                                                         With _SODDPatient.Find(Function(h) h.ServiceOrderDetailId = x.ServiceOrderDetailId)

                                                                                             If _liquidationDataDetail.LimitType = ELimitType.UnitValue Then
                                                                                                 Dim _valueUnit = _valueProcess / x.Quantity

                                                                                                 SyncLock Lock
                                                                                                     If _liquidationDataDetail.Quantity = 0 Then
                                                                                                         x.Quantity = 0
                                                                                                     ElseIf x.Quantity > _liquidationDataDetail.Quantity Then
                                                                                                         x.Quantity = _liquidationDataDetail.Quantity
                                                                                                         _liquidationDataDetail.Quantity = 0
                                                                                                     Else
                                                                                                         _liquidationDataDetail.Quantity -= x.Quantity
                                                                                                     End If
                                                                                                 End SyncLock

                                                                                                 _valueUnit = If((_liquidationDataDetail.LimitValue > _valueUnit) OrElse (_liquidationDataDetail.LimitValue = 0), _valueUnit, _liquidationDataDetail.LimitValue)
                                                                                                 _dictionaryValues = Utils.SetValueSalesPriceWithNet(If(_liquidationDataDetail.TaxInclude, Utils.eTypeValue.TotalValue, Utils.eTypeValue.NetValue), (x.Quantity * _valueUnit),
                                                                                                                                                     percentInsurer, DiscountPercentOrValueByLiquidationData(liquidationData, eMasterAccount.EntityAccount, x.ApportionmentPercent), liquidationData?.ApplyDiscount = 2) '_liquidationDataDetail.TaxInclude, (x.Quantity * _valueUnit), _percent)
                                                                                                 ' .Quantity += If(_quantity > 0, 0, Math.Abs(_quantity)) 'se comenta por si afuturo esta cantidad se guardará en un nuevo campo
                                                                                             Else
                                                                                                 _dictionaryValues = Utils.SetValueSalesPriceWithNet(If(_liquidationDataDetail.TaxInclude, Utils.eTypeValue.TotalValue, Utils.eTypeValue.NetValue), CalculateValueOfPercent(_liquidationDataDetail.LimitValue, x.ApportionmentPercent),
                                                                                                                                                    percentInsurer, DiscountPercentOrValueByLiquidationData(liquidationData, eMasterAccount.EntityAccount, x.ApportionmentPercent), liquidationData?.ApplyDiscount = 2) '_liquidationDataDetail.TaxInclude, CalculateValueOfPercent(_liquidationDataDetail.LimitValue, x.ApportionmentPercent), _percent)
                                                                                             End If

                                                                                             'actualiza folio aseguradora
                                                                                             _diffSubtotal = x.SubTotalSalesPrice - Math.Round(_dictionaryValues.Item(Utils.eTypeValue.GrossValue), 2, MidpointRounding.AwayFromZero)  '(Math.Round(_dictionaryValues.FirstOrDefault(Function(f) f.Key = Utils.eTypeTaxControl.GrossValue).Value, 2, MidpointRounding.AwayFromZero)) '+ x.GrandTotalDiscount)
                                                                                             x.SubTotalSalesPrice = (Math.Round(_dictionaryValues.Item(Utils.eTypeValue.GrossValue), 2, MidpointRounding.AwayFromZero)) '+ x.GrandTotalDiscount)                                                                                            
                                                                                             x.GrandTotalDiscount = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.DiscountValue), 2, MidpointRounding.AwayFromZero)
                                                                                             x.GrandTotalTaxes = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TaxValue), 2, MidpointRounding.AwayFromZero)
                                                                                             x.GrandTotalSalesPrice = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TotalValue), 2, MidpointRounding.AwayFromZero)
                                                                                             x.ThirdPartySalesPrice = x.GrandTotalSalesPrice
                                                                                             'actualiza folio paciente
                                                                                             .SubTotalSalesPrice += _diffSubtotal
                                                                                             _dictionaryValues = Utils.SetValueSalesPriceWithNet(Utils.eTypeValue.GrossValue, .SubTotalSalesPrice, percentPatient,
                                                                                                                                                 DiscountPercentOrValueByLiquidationData(liquidationData, eMasterAccount.PatientAccount, x.ApportionmentPercent), liquidationData?.ApplyDiscount = 2)

                                                                                             .GrandTotalDiscount = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.DiscountValue), 2, MidpointRounding.AwayFromZero)
                                                                                             .GrandTotalTaxes = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TaxValue), 2, MidpointRounding.AwayFromZero)
                                                                                             .GrandTotalSalesPrice = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TotalValue), 2, MidpointRounding.AwayFromZero)
                                                                                             .ThirdPartySalesPrice = .GrandTotalSalesPrice
                                                                                         End With
                                                                                     End Sub)
        Else
            'establece limite al folio del paciente campo limite coaseguro paciente
            Parallel.ForEach(_SODDPatient, Sub(x)
                                               Dim _diffSubtotal As Decimal
                                               Dim percentPatient As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing OrElse flagTaxFreePatient, 0, x.ServiceOrderDetail?.GeneralLedgerIVA?.Percentage)
                                               Dim percentInsurer As Decimal = If(x.ServiceOrderDetail?.IvaId Is Nothing OrElse flagTaxFreeInsurer, 0, x.ServiceOrderDetail?.GeneralLedgerIVA?.Percentage)
                                               Dim _valueProcess = If(_liquidationDataDetail.TaxInclude, x.GrandTotalSalesPrice, (x.SubTotalSalesPrice - x.GrandTotalDiscount))
                                               Dim _dictionaryValues As Dictionary(Of Utils.eTypeValue, Decimal)
                                               With _SODDInsurer.Find(Function(h) h.ServiceOrderDetailId = x.ServiceOrderDetailId)
                                                   x.ApportionmentPercent = (_valueProcess / _sumFolio) * 100
                                                   _dictionaryValues = Utils.SetValueSalesPriceWithNet(If(_liquidationDataDetail.TaxInclude, Utils.eTypeValue.TotalValue, Utils.eTypeValue.NetValue),
                                                                                                        CalculateValueOfPercent(_liquidationDataDetail.LimitValue, x.ApportionmentPercent), percentPatient,
                                                                                                        DiscountPercentOrValueByLiquidationData(liquidationData, eMasterAccount.PatientAccount, x.ApportionmentPercent), liquidationData?.ApplyDiscount = 2) '_liquidationDataDetail.TaxInclude, CalculateValueOfPercent(_liquidationDataDetail.LimitValue, x.ApportionmentPercent), _percent)

                                                   'actualiza folio paciente 
                                                   _diffSubtotal = x.SubTotalSalesPrice - (Math.Round(_dictionaryValues.Item(Utils.eTypeValue.GrossValue), 2, MidpointRounding.AwayFromZero)) '+ x.GrandTotalDiscount)
                                                   x.SubTotalSalesPrice = (Math.Round(_dictionaryValues.Item(Utils.eTypeValue.GrossValue), 2, MidpointRounding.AwayFromZero)) '+ x.GrandTotalDiscount)
                                                   x.GrandTotalDiscount = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.DiscountValue), 2, MidpointRounding.AwayFromZero)
                                                   x.GrandTotalTaxes = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TaxValue), 2, MidpointRounding.AwayFromZero)
                                                   x.GrandTotalSalesPrice = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TotalValue), 2, MidpointRounding.AwayFromZero)
                                                   x.ThirdPartySalesPrice = x.GrandTotalSalesPrice

                                                   'actualiza folio aseguradora
                                                   .SubTotalSalesPrice += _diffSubtotal
                                                   _dictionaryValues = Utils.SetValueSalesPriceWithNet(Utils.eTypeValue.GrossValue, .SubTotalSalesPrice, percentInsurer,
                                                                                                        DiscountPercentOrValueByLiquidationData(liquidationData, eMasterAccount.EntityAccount, x.ApportionmentPercent), liquidationData?.ApplyDiscount = 2)
                                                   .GrandTotalDiscount = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.DiscountValue), 2, MidpointRounding.AwayFromZero)
                                                   .GrandTotalTaxes = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TaxValue), 2, MidpointRounding.AwayFromZero)
                                                   .GrandTotalSalesPrice = Math.Round(_dictionaryValues.Item(Utils.eTypeValue.TotalValue), 2, MidpointRounding.AwayFromZero)
                                                   .ThirdPartySalesPrice = .GrandTotalSalesPrice
                                               End With

                                           End Sub)
        End If

        _SODDInsurer.RemoveAll(Function(x) x.Quantity = 0)
        _SODDPatient.RemoveAll(Function(x) x.Quantity = 0)
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' funcion que retorna el procentaje de descuento o el valor del descuento si este es por valor teniendo en cuenta el tipo de folio 
    ''' </summary>
    ''' <param name="liquidationData"></param>
    ''' <param name="masterAccountType"></param>
    ''' <param name="apportionmentPercent"></param>
    ''' <returns></returns>
    Function DiscountPercentOrValueByLiquidationData(ByVal liquidationData As LiquidationData, masterAccountType As eMasterAccount, Optional apportionmentPercent As Decimal = 0) As Decimal

        If liquidationData Is Nothing OrElse liquidationData?.DiscountValueorPercentage Is Nothing Then
            Return 0
        End If

        Dim discountValueorPercentage As Decimal = liquidationData?.DiscountValueorPercentage.Value

        Select Case liquidationData?.ApplyDiscount
            Case 1, 3
                Return discountValueorPercentage
            Case 2

                If {eMasterAccount.PatientAccount, eMasterAccount.EntityAccount}.Contains(masterAccountType) Then
                    discountValueorPercentage = CalculateValueOfPercent(discountValueorPercentage, If(masterAccountType = eMasterAccount.PatientAccount, liquidationData.PatientCoinsurance, liquidationData.InsuranceCoinsurance))
                End If

                Return Math.Round((discountValueorPercentage * apportionmentPercent), 2, MidpointRounding.AwayFromZero)
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' funcion que valida y obtiene el coaseguro general o especifico
    ''' </summary>
    ''' <param name="liquidationData"></param>
    ''' <param name="serviceOrderDetailDistribution"></param>
    ''' <returns></returns>
    Private Function ValidateAndGetCoinsurance(ByVal liquidationData As LiquidationData, ByVal serviceOrderDetailDistribution As ServiceOrderDetailDistribution) As ActionResult(Of Dictionary(Of ECoInsurance, Decimal))
        Dim patientCoinsurance As Decimal = 0
        Dim InsuranceCoinsurance As Decimal = 0

        If liquidationData Is Nothing OrElse serviceOrderDetailDistribution Is Nothing Then
            Return New ActionResult(Of Dictionary(Of ECoInsurance, Decimal)) With {.StateResult = False, .Message = "Parámetro vacio para obtener el coaseguro"}
        End If

        patientCoinsurance = liquidationData.PatientCoinsurance
        InsuranceCoinsurance = liquidationData.InsuranceCoinsurance

        If liquidationData?.LiquidationDataDetail Is Nothing OrElse Not liquidationData?.LiquidationDataDetail?.Any(Function(x) x.LimitType = 3) Then

            Return New ActionResult(Of Dictionary(Of ECoInsurance, Decimal)) With {.StateResult = True,
                                                                                    .ObjectEmbbeded = New Generic.Dictionary(Of ECoInsurance, Decimal) From {{ECoInsurance.InsurerCoveredValue, InsuranceCoinsurance},
                                                                                                                                                             {ECoInsurance.PatientCoinsurance, patientCoinsurance}}}

        End If

        With serviceOrderDetailDistribution
            .CupsEntityId = .ServiceOrderDetail?.CUPSEntityId
            .SubGroupCupsId = .ServiceOrderDetail?.CUPSEntity?.CUPSSubGroupId
            .GroupCupsId = .ServiceOrderDetail?.CUPSEntity?.CupsSubgroup?.CupsGroupId
            .ProductId = .ServiceOrderDetail?.ProductId

        End With

        If serviceOrderDetailDistribution.CupsEntityId IsNot Nothing AndAlso (serviceOrderDetailDistribution.SubGroupCupsId Is Nothing OrElse serviceOrderDetailDistribution.GroupCupsId Is Nothing) Then
            Return New ActionResult(Of Dictionary(Of ECoInsurance, Decimal)) With {.StateResult = False, .Message = $"el CupsId ( {serviceOrderDetailDistribution.CupsEntityId}) no tiene grupo o subgrupo"}
        End If

        Dim detailCondition = liquidationData?.LiquidationDataDetail?.Where(Function(item)

                                                                                If item.LimitType <> 3 Then
                                                                                    Return False
                                                                                End If

                                                                                If serviceOrderDetailDistribution?.ProductId IsNot Nothing AndAlso
                                                                                     {EConditionType.Cups, EConditionType.SubGroupCups, EConditionType.GroupsCups}.Contains(item.ConditionType) Then
                                                                                    Return False
                                                                                End If

                                                                                Dim ListIds = item?.ConditionsItemsIds?.Split(",")?.ToList()

                                                                                Select Case item.ConditionType

                                                                                    Case EConditionType.Cups
                                                                                        If serviceOrderDetailDistribution.CupsEntityId Is Nothing Then
                                                                                            Return False
                                                                                        End If
                                                                                        Return ListIds.Contains(serviceOrderDetailDistribution.CupsEntityId)

                                                                                    Case EConditionType.SubGroupCups
                                                                                        If serviceOrderDetailDistribution.SubGroupCupsId Is Nothing Then
                                                                                            Return False
                                                                                        End If
                                                                                        Return ListIds.Contains(serviceOrderDetailDistribution.SubGroupCupsId)

                                                                                    Case EConditionType.GroupsCups
                                                                                        If serviceOrderDetailDistribution.GroupCupsId Is Nothing Then
                                                                                            Return False
                                                                                        End If
                                                                                        Return ListIds.Contains(serviceOrderDetailDistribution.GroupCupsId)

                                                                                    Case EConditionType.Product
                                                                                        If serviceOrderDetailDistribution.ProductId Is Nothing Then
                                                                                            Return False
                                                                                        End If
                                                                                        Return ListIds.Contains(serviceOrderDetailDistribution.ProductId)

                                                                                    Case Else
                                                                                        Return False
                                                                                End Select
                                                                            End Function).OrderByDescending(Function(x) x.ConditionType).FirstOrDefault
        'en un futuro cuando se añadan los grupos y sub grupos de producto tocaria ordena ascendemente y descendente cuando sea cups

        If detailCondition IsNot Nothing Then
            patientCoinsurance = detailCondition.PatientCoinsurance
            InsuranceCoinsurance = detailCondition.InsuranceCoinsurance
        End If

        Return New ActionResult(Of Dictionary(Of ECoInsurance, Decimal)) With {.StateResult = True, .ObjectEmbbeded = New Generic.Dictionary(Of ECoInsurance, Decimal) _
                                                                                                    From {{ECoInsurance.InsurerCoveredValue, InsuranceCoinsurance},
                                                                                                            {ECoInsurance.PatientCoinsurance, patientCoinsurance}}}
    End Function


    ''' <summary>
    ''' servicio que se encarga de unificar las cuentas Paciente-Aseguradora en la cuenta madre
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function UnifyAccount(ByVal _admissionNumber As String, audit As AuditMessage) As ActionResult Implements IFolioAdminService.UnifyAccount
        If String.IsNullOrEmpty(_admissionNumber) Then
            Return New ActionResult With {.StateResult = False, .Message = $"Error Parametros vacios"}
        End If

        Dim unitWork As Domain.Base.IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

            Try
                Dim ListRevenueControlD = _revenueControlDetailRepository.GetByFilter(Function(x) x.RevenueControl.AdmissionNumber = _admissionNumber, True,
                                                                                {"RevenueControl", "ServiceOrderDetailDistribution.ServiceOrderDetail.IPSService",
                                                                                "ServiceOrderDetailDistribution.ServiceOrderDetail.InventoryProduct"}).ToList()

                If ListRevenueControlD Is Nothing OrElse Not ListRevenueControlD?.Any(Function(x) x.IsMasterAccount = 3) OrElse ListRevenueControlD.FindAll(Function(x) x.IsMasterAccount = 3).Count > 1 Then
                    Return New ActionResult With {.StateResult = False, .Message = $"No se encontró información del folio madre"}
                End If

                'se consulta la cantidad de folio para establecer el valor correcto en la cabecera
                Dim folioQuantity As Integer = ListRevenueControlD.Count
                ListRevenueControlD = ListRevenueControlD.FindAll(Function(x) {4, 3, 2}.Contains(x.IsMasterAccount))

                If ListRevenueControlD.Any(Function(x) {2, 5, 7}.Contains(x.Status)) Then
                    Dim folioNameList = ListRevenueControlD.FindAll(Function(x) {2, 5, 7}.Contains(x.Status)).Select(Function(s)
                                                                                                                         Dim name As String = String.Empty
                                                                                                                         Select Case s.Status
                                                                                                                             Case 2
                                                                                                                                 name = "Facturado"
                                                                                                                             Case 5
                                                                                                                                 name = "Reconocimiento de ingresos"
                                                                                                                             Case 7
                                                                                                                                 name = "Cerrado"
                                                                                                                         End Select
                                                                                                                         name = $"El folio #{s.FolioOrder} ({Utils.FolioMasterAccountNames.Item(s.IsMasterAccount)}) esta en estado {name}"
                                                                                                                         Return name
                                                                                                                     End Function).ToList()
                    Return New ActionResult With {.StateResult = False, .Message = $"No se puede unir la cuenta debido a que : {String.Join(",", folioNameList)}"}
                End If


                Dim _listIdSODD = New List(Of Integer)
                Dim _listIdRevenueControlD = New ConcurrentBag(Of Integer)
                Dim listErrors As New ConcurrentBag(Of String)
                Dim Lock As New Object
                Dim orginalFolioSOD = ListRevenueControlD?.FirstOrDefault(Function(x) x.IsMasterAccount = eMasterAccount.HideMasterAccount)?.ServiceOrderDetailDistribution?.Select(Function(x) x.ServiceOrderDetailId).ToList()

                Parallel.ForEach(ListRevenueControlD, Sub(x)
                                                          If {2, 4}.Contains(x.IsMasterAccount) Then

                                                              _listIdRevenueControlD.Add(x.Id)

                                                              If x.ServiceOrderDetailDistribution.Any(Function(e) Not orginalFolioSOD.Contains(e.ServiceOrderDetailId)) Then

                                                                  Dim listCodeName = From item In x.ServiceOrderDetailDistribution.AsParallel()
                                                                                     Where Not orginalFolioSOD.Contains(item.ServiceOrderDetailId)
                                                                                     Select If(item?.ServiceOrderDetail?.IPSService IsNot Nothing,
                                                                                                $"{item?.ServiceOrderDetail?.IPSService?.Code} - {item?.ServiceOrderDetail?.IPSService?.Name}",
                                                                                                $"{item?.ServiceOrderDetail?.InventoryProduct?.Code} - {item?.ServiceOrderDetail?.InventoryProduct?.Name}")

                                                                  listErrors.Add($"El Folio - {If(x.IsMasterAccount = eMasterAccount.PatientAccount, "Paciente", "Aseguradora")} contiene los siguientes items extra :{String.Join(", ", listCodeName.ToList())}")
                                                              End If

                                                              SyncLock Lock
                                                                  _listIdSODD.AddRange(x.ServiceOrderDetailDistribution.Select(Function(y) y.Id))
                                                              End SyncLock

                                                          Else
                                                              x.IsMasterAccount = 1
                                                              x.Status = 1
                                                              x.RevenueControl.FolioQuantity = folioQuantity - 2
                                                          End If
                                                      End Sub)

                If listErrors.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = $"Validación : {String.Join(" y ", listErrors)}. los items extra se deben excluir antes de unificar"}
                End If

                If _listIdSODD Is Nothing OrElse Not _listIdSODD?.Any() OrElse
                    _listIdRevenueControlD Is Nothing OrElse Not _listIdRevenueControlD?.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = $"No se encontró información del folio Paciente-Aseguradora"}
                End If

                Dim strSODD = String.Join(",", _listIdSODD)
                Me._revenueControlDetailRepository.ExecuteNonQuery(String.Format("DELETE FROM Billing.ServiceOrderDetailDistribution WHERE Id in ({0})", strSODD))
                Dim strRCD = String.Join(",", _listIdRevenueControlD)
                Me._revenueControlDetailRepository.ExecuteNonQuery(String.Format("DELETE FROM Billing.RevenueControlDetail WHERE Id in ({0})", strRCD))

                Me._revenueControlDetailRepository.ExecuteNonQuery("DELETE FROM Billing.LiquidationDataSeparated WHERE AdmissionNumber = {0}", _admissionNumber)


                _revenueControlDetailRepository.SaveEntity(ListRevenueControlD.Find(Function(x) x.IsMasterAccount = 1))

                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "La Unificación de cuentas fue exitosa"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' funcion que valida la liquidacion de folios
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <returns></returns>
    Function ValidateLiquidateFolio(ByVal Ids As String) As RequestResponse(Of String) Implements IFolioAdminService.ValidateLiquidateFolio
        If String.IsNullOrEmpty(Ids) Then
            Return New RequestResponse(Of String) With {.Status = False, .Message = "Parámetro de entrada Vacío"}
        End If
        Try
            Dim strinBuilder As New StringBuilder
            Dim listIds = Ids.Split(",").ToList()
            Dim listRevenueControlDetail = _revenueControlDetailRepository.Query(Function(x) listIds.Contains(x.Id), False, {"RevenueControl", "ThirdParty", "ServiceOrderDetailDistribution"})
            Dim listIdsQuery = listRevenueControlDetail?.Select(Function(x) x.Id)?.ToList()

            If listIdsQuery Is Nothing OrElse listIds?.Any(Function(s) Not listIdsQuery.Contains(s)) Then
                Return New RequestResponse(Of String) With {.Status = False, .Message = $"No se encontrarón datos de uno o más folios"}
            End If

            Parallel.ForEach(listRevenueControlDetail, Sub(x)
                                                           If x.IsMasterAccount = eMasterAccount.NotMasterAccount Then
                                                               Exit Sub
                                                           End If

                                                           If x.ThirdParty Is Nothing Then
                                                               strinBuilder.AppendLine($"No hay datos del Tercero para el folio #{x.FolioOrder}")
                                                           End If
                                                           If x.IsMasterAccount = eMasterAccount.PatientAccount _
                                                                AndAlso x.ThirdParty?.ContributionType = 5 _
                                                                AndAlso x.ServiceOrderDetailDistribution?.Any(Function(d) d.GrandTotalTaxes > 0) Then
                                                               strinBuilder.AppendLine($"Recalcule por favor folio #{x.FolioOrder}, existen Items con valor de IVA y el tercero es un contribuyente de tipo exento")
                                                           End If
                                                       End Sub)
            If strinBuilder.Length > 0 Then
                Return New RequestResponse(Of String) With {.Status = False, .Message = strinBuilder.ToString()}
            End If

            Return New RequestResponse(Of String) With {.Status = True, .Message = "Validación exitosa"}
        Catch ex As Exception
            Return New RequestResponse(Of String) With {.Status = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que se encarga de cerrar el folio (paciente o aseguradora) cuando este en 0 y cambia a estado facturado cuando no hubiesen mas folio pendientes
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function CloseAccount(ByVal revenueControlDetailId As Integer?, audit As AuditMessage) As ActionResult Implements IFolioAdminService.CloseAccount
        If revenueControlDetailId Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = $"Error Parametros vacios"}
        End If

        Dim unitWork As Domain.Base.IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

            Try
                ' se consulta el folio (PUEDER SER PACIENTE O ASEGURADORA)
                Dim revenueControl = _revenueControlDetailRepository.FirstOrDefault(Function(x) x.Id = revenueControlDetailId AndAlso x.Status = 1 _
                                                                                     AndAlso {eMasterAccount.PatientAccount, eMasterAccount.EntityAccount}.Contains(x.IsMasterAccount), True, {"RevenueControl", "ServiceOrderDetailDistribution"})

                If revenueControl Is Nothing Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"No se encontró información del folio o no está en estado abierto"}
                End If

                'se verifica si existen items y que la suma de estos sean mayor a 0, de ser el caso se descarta la transaccion y no se actualiza el estado del folio
                If revenueControl?.ServiceOrderDetailDistribution IsNot Nothing AndAlso
                    (revenueControl?.ServiceOrderDetailDistribution?.Any() AndAlso
                        revenueControl.ServiceOrderDetailDistribution.Sum(Function(s) s.GrandTotalSalesPrice) > 0) Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"Existen items con valor distribuidos en el folio"}
                End If

                revenueControl.Status = 7
                revenueControl.ModificationDate = DateTime.Now()
                revenueControl.ModificationUser = audit.CodeUser

                Dim listRevenueControl = _revenueControlDetailRepository.GetByFilter(Function(x) x.RevenueControlId = revenueControl.RevenueControlId _
                                                                                                AndAlso x.Id <> revenueControlDetailId, False)?.ToList()

                'verifico si viene algun elemento, busco si alguno que no este facturado
                If listRevenueControl IsNot Nothing AndAlso listRevenueControl?.Any(Function(x) {1, 3, 5}.Contains(x.Status) AndAlso x.IsMasterAccount <> eMasterAccount.HideMasterAccount) Then
                    _revenueControlDetailRepository.SaveEntity(revenueControl)
                    unitWork.Commit()
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .Message = "se cerró el folio, pero no se cambio el estado del ingreso ya que existen folios sin facturar"}
                End If

                ' se actualiza el estado del ingreso a facturado
                Me._revenueControlDetailRepository.ExecuteNonQuery("UPDATE ing 
			                                                            SET IESTADOIN = IIF(rcd.Id IS NULL, 'C', 'F')
		                                                            FROM [dbo].[ADINGRESO] ing
		                                                            LEFT JOIN Billing.RevenueControl rc ON ing.NUMINGRES = rc.AdmissionNumber
		                                                            LEFT JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
		                                                            WHERE ing.NUMINGRES = {0}", revenueControl?.RevenueControl?.AdmissionNumber)

                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "se cerró la cuenta del paciente y se actualizó el estado del ingreso"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' metodo que obtiene la cabecera y detalle de la pre factura
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <returns></returns>
    Function GetVReportInvoicePartial(ByVal revenueControlDetailId As Integer, CurrencyId As Integer?, dateTRM As Date?) As RequestResponse(Of InvoicePartialMasterAccount) Implements IFolioAdminService.GetVReportInvoicePartial
        Try
            Dim invoicePartial = _folioRepository.GetVReportInvoicePartial(revenueControlDetailId)

            If invoicePartial Is Nothing Then
                Return New RequestResponse(Of InvoicePartialMasterAccount)(False, Nothing, Nothing, String.Format("Folio {0} doesnot exist", revenueControlDetailId))
            End If

            'si la fecha viene vacia asumo la actual
            If dateTRM Is Nothing Then
                dateTRM = Date.Now()
            End If

            If CurrencyId IsNot Nothing AndAlso CurrencyId <> invoicePartial.CurrencyId Then
                Dim indigo = SessionValues.Instance
                indigo.OfficialCurrencyId = invoicePartial.CurrencyId

                'se consulta el TRM Custom de Invoice
                Dim result = _currencyAdminService.GetTRMbyCurrencyId(CurrencyId, invoicePartial.CurrencyId, indigo, dateTRM, NameOf(Invoice))

                If result Is Nothing OrElse Not result.StateResult Then
                    Return New RequestResponse(Of InvoicePartialMasterAccount)(True, HttpStatusCode.OK, invoicePartial, $"No se logró hacer la conversión porque no se encontró TRM para la fecha {dateTRM}")
                End If

                Dim tRMValue = result.ObjectEmbbeded.Value
                Dim currency = _currencyAdminService.GetCurrencyById(CurrencyId, New AuditMessage)

                '**** se convierten directamente los valores calculados (subtotal, descuento, IVA, total) a la nueva moneda ****
                Parallel.ForEach(invoicePartial.InvoicePartialDetail, Sub(x)
                                                                          'subtotal total (multiplicado por cantidades)
                                                                          x.SubTotalSalesPrice = Math.Round(x.SubTotalSalesPrice / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                          'descuento total
                                                                          x.GrandTotalDiscount = Math.Round(x.GrandTotalDiscount / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                          'valor neto total (bruto - descuento) ya existe: NetWorth
                                                                          x.NetWorth = Math.Round(x.NetWorth / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                          'IVA total: no se recalcula, solo se convierte
                                                                          x.IvaTotalValue = Math.Round(x.IvaTotalValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                          'valor total (con IVA)
                                                                          x.GrandTotalSalesPrice = Math.Round(x.GrandTotalSalesPrice / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                          x.ThirdPartySalesPrice = x.GrandTotalSalesPrice

                                                                          'recalculo de valores unitarios a partir de los totales convertidos
                                                                          If x.InvoicedQuantity > 0 Then
                                                                              x.GrossValue = Math.Round(x.SubTotalSalesPrice / x.InvoicedQuantity, 2, MidpointRounding.AwayFromZero)
                                                                              x.NetUnitValue = Math.Round(x.NetWorth / x.InvoicedQuantity, 2, MidpointRounding.AwayFromZero)
                                                                              x.TotalSalesPrice = Math.Round(x.GrandTotalSalesPrice / x.InvoicedQuantity, 2, MidpointRounding.AwayFromZero)
                                                                          End If

                                                                          'campos que en CR definitivamente No aplican
                                                                          x.ThirdPartyDiscount = 0
                                                                          x.SubTotalPatientSalesPrice = 0
                                                                      End Sub)

                '****ahora a partir de los detalles vamos a establecer los valores de la cabecera***
                With invoicePartial
                    'valor bruto
                    .SubTotalService = .InvoicePartialDetail.Sum(Function(d) d.SubTotalSalesPrice)
                    'Descuento total
                    .GrandTotalDiscount = .InvoicePartialDetail.Sum(Function(d) d.GrandTotalDiscount)
                    .PatientDiscount = .GrandTotalDiscount
                    .ThirdPartyDiscountValue = .GrandTotalDiscount
                    'valor Total
                    .GrandTotalSalesPrice = .InvoicePartialDetail.Sum(Function(d) d.GrandTotalSalesPrice)
                    .ThirdPartySalesValue = .GrandTotalSalesPrice
                    'campos de la moneda
                    .CurrencyId = CurrencyId
                    .CurrencyAbbreviation = currency?.Abbreviation
                    .CurrencyName = currency?.ISO4217?.CurrencyName
                    'campos de calculos segun datos de liquidacion (quedan pendiente)
                    .CoinsuranceInsurance = Math.Round(.CoinsuranceInsurance / tRMValue, 2, MidpointRounding.AwayFromZero)
                    .CopaymentValueInsurance = Math.Round(.CopaymentValueInsurance / tRMValue, 2, MidpointRounding.AwayFromZero)
                    .DeductibleValueInsurance = Math.Round(.DeductibleValueInsurance / tRMValue, 2, MidpointRounding.AwayFromZero)
                    .PatientCoinsurance = Math.Round(.PatientCoinsurance / tRMValue, 2, MidpointRounding.AwayFromZero)
                    .PatientCopaymentValue = Math.Round(.PatientCopaymentValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                    .PatientDeductibleValue = Math.Round(.PatientDeductibleValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                    'los siguientes campos no se usan en fact CRC 
                    .TotalPatientSalesPrice = 0
                    'Tasa representativa del mercado
                    .TRMValue = tRMValue
                End With

            End If
            '*********listado devolucion de IVA**********************
            Dim QueryDev = GetTaxDevolutionByRevenueControlDetail(revenueControlDetailId, invoicePartial:=invoicePartial)
            If QueryDev?.Data Is Nothing OrElse Not QueryDev?.Status Then
                Return New RequestResponse(Of InvoicePartialMasterAccount)(False, HttpStatusCode.OK, Nothing, QueryDev?.Message)
            End If
            invoicePartial.TaxDevolution = QueryDev?.Data
            '********************************************************
            Return New RequestResponse(Of InvoicePartialMasterAccount)(True, HttpStatusCode.OK, invoicePartial, Nothing)
        Catch ex As Exception
            Return New RequestResponse(Of InvoicePartialMasterAccount)(False, HttpStatusCode.OK, Nothing, Utils.GetInnerExceptionMessageToString(ex))
        End Try
    End Function


    ''' <summary>
    ''' Funcion que se encarga de consultar las devoluciones de IVA a la que una cuenta (folio) puede llegar a tener
    ''' esta funcion devuelve los valores segun la moneda y consulta el TRM segun la fecha, estos parametros son opcionales, ya que
    ''' si no se envia toma la moneda oficial
    ''' </summary>
    ''' <param name="revenueControlDetailId"> Id del Folio </param>
    ''' <param name="CurrencyId">Opcional: Id de la moneda</param>
    ''' <param name="dateTRM">Opcional: Fecha del TRM a consultart</param>
    ''' <param name="invoicePartial">Opcional: Entidad de pre-factura (solo se usa desde el metodo GetVReportInvoicePartial)</param>
    ''' <returns></returns>
    Public Function GetTaxDevolutionByRevenueControlDetail(ByVal revenueControlDetailId As Integer,
                                                            Optional CurrencyId As Integer? = Nothing,
                                                            Optional dateTRM As Date? = Nothing,
                                                            Optional ByVal invoicePartial As InvoicePartialMasterAccount = Nothing,
                                                            Optional ByVal paymentCurrencyId As Integer? = Nothing) As RequestResponse(Of List(Of TaxDevolution)) Implements IFolioAdminService.GetTaxDevolutionByRevenueControlDetail
        Try
            Dim paymentResult As ActionResult
            Dim listTaxDevolution As List(Of TaxDevolution) = New List(Of TaxDevolution)

            'si no viene la entidad de la prefactura entonces la consulto
            If invoicePartial Is Nothing Then
                Dim result = GetVReportInvoicePartial(revenueControlDetailId, CurrencyId, dateTRM)

                If result Is Nothing OrElse Not result?.Status OrElse result?.Data Is Nothing Then
                    Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, $"El proceso de consulta del folio falló")
                End If

                If result.Data?.TaxDevolution Is Nothing OrElse Not result.Data?.TaxDevolution?.Any() Then
                    Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, $"No se encontraron datos de devolución del IVA")
                End If

                invoicePartial = result.Data
                listTaxDevolution.AddRange(invoicePartial.TaxDevolution)

                paymentResult = PaymentCurrencyConverter(listTaxDevolution, invoicePartial.CurrencyId, paymentCurrencyId, dateTRM)

                If paymentResult Is Nothing OrElse Not paymentResult.StateResult Then
                    Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, paymentResult?.Message)
                End If

                'Se retorna porque el proceso de calcular el valor por medio de pago se hizo cuando consulto el invoice partial
                Return New RequestResponse(Of List(Of TaxDevolution))(True, HttpStatusCode.OK, listTaxDevolution.ToList(), Nothing)
            End If

            If invoicePartial?.InvoicePartialDetail Is Nothing OrElse Not invoicePartial?.InvoicePartialDetail?.Any() Then
                Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, $"No existen detalles en el folio")
            End If

            'se consultan los parametros de impuestos sobre las ventas.
            Dim listTaxesParametrization = _generalLedgerIVARepository.GetAll()?.ToList()

            If listTaxesParametrization Is Nothing OrElse Not listTaxesParametrization?.Any() Then
                Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, $"No hay parámetros de impuesto sobre las ventas")
            End If

            'se agrupan los ivas existentes
            Dim dataOfTaxes = (From item In invoicePartial?.InvoicePartialDetail.AsParallel()
                               Group By item.TaxPercentage Into Group
                               Select TaxPercentage, IvaTotalValue = Group.Sum(Function(y) y.IvaTotalValue)
                               Where TaxPercentage > 0).ToList()

            dataOfTaxes.ForEach(Sub(item)

                                    Dim taxParametrization = listTaxesParametrization.Find(Function(x) x.Percentage = item.TaxPercentage AndAlso x.ApplyTaxDevolution)
                                    If taxParametrization Is Nothing Then
                                        Exit Sub
                                    End If

                                    Dim paymentsMethods As List(Of Byte) = taxParametrization.PaymentMethodTypes.Split(",").ToEntityList(Of Byte)
                                    For Each pay In paymentsMethods

                                        Dim existingTaxDevolution = listTaxDevolution.Find(Function(x) x.PaymentMethodType = pay)

                                        If existingTaxDevolution IsNot Nothing Then
                                            existingTaxDevolution.TaxValue += item.IvaTotalValue
                                            existingTaxDevolution.ValueWithTaxDevolution -= item.IvaTotalValue
                                        Else
                                            Dim taxDevolution = New TaxDevolution()
                                            With taxDevolution
                                                .Id = CInt($"{revenueControlDetailId}{pay}")
                                                .TaxValue = item.IvaTotalValue
                                                .ValueWithTaxDevolution = invoicePartial.GrandTotalSalesPrice - item.IvaTotalValue
                                                .PaymentMethodType = CByte(pay)
                                                .CurrencyId = invoicePartial.CurrencyId
                                                .CurrencyName = invoicePartial.CurrencyName
                                                .CurrencyAbbreviation = invoicePartial.CurrencyAbbreviation
                                                .RevenueControlDetailId = revenueControlDetailId

                                                Select Case pay
                                                    Case 1
                                                        .PaymentMethodTypeName = ResourceManager.GetString("EffectivePaymentMethod", "Treasury")
                                                    Case 2
                                                        .PaymentMethodTypeName = ResourceManager.GetString("PaymentMethodCheck", "Treasury")
                                                    Case 3
                                                        .PaymentMethodTypeName = ResourceManager.GetString("CardPaymentMethod", "Treasury")
                                                    Case 4
                                                        .PaymentMethodTypeName = ResourceManager.GetString("ConsignmentPaymentMethod", "Treasury")
                                                    Case Else
                                                        .PaymentMethodTypeName = "Otro"
                                                End Select
                                            End With
                                            listTaxDevolution.Add(taxDevolution)
                                        End If
                                    Next
                                End Sub)

            listTaxDevolution.Add(New TaxDevolution With {.Id = revenueControlDetailId,
                                                            .PaymentMethodType = 0,
                                                            .PaymentMethodTypeName = "Otros",
                                                            .TaxValue = 0,
                                                            .ValueWithTaxDevolution = invoicePartial.GrandTotalSalesPrice,
                                                            .CurrencyId = invoicePartial.CurrencyId,
                                                            .CurrencyName = invoicePartial.CurrencyName,
                                                            .CurrencyAbbreviation = invoicePartial.CurrencyAbbreviation})

            'se verifica que la lista tenga almenos un elemento y que la moneda de pago No venga vacia para hacer la respectiva conversion
            paymentResult = PaymentCurrencyConverter(listTaxDevolution, invoicePartial.CurrencyId, paymentCurrencyId, dateTRM)

            If paymentResult Is Nothing OrElse Not paymentResult.StateResult Then
                Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, paymentResult?.Message)
            End If

            Return New RequestResponse(Of List(Of TaxDevolution))(True, HttpStatusCode.OK, listTaxDevolution.ToList(), Nothing)
        Catch ex As Exception
            Return New RequestResponse(Of List(Of TaxDevolution))(False, HttpStatusCode.OK, Nothing, Utils.GetInnerExceptionMessageToString(ex))
        End Try
    End Function

    ''' <summary>
    ''' Metodo encargado de re convertir a la moneda del metodo de pago
    ''' </summary>
    ''' <param name="listTaxDevolution"></param>
    ''' <param name="invoiceCurrencyId"></param>
    ''' <param name="paymentCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <returns></returns>
    Public Function PaymentCurrencyConverter(ByRef listTaxDevolution As List(Of TaxDevolution), invoiceCurrencyId As Integer?, paymentCurrencyId As Integer?, dateTRM As Date?) As ActionResult

        'si algun parametro viene vacio no ejecuto el metodo y lo saco ya que este metodo no es de tipo obligatorio
        If listTaxDevolution Is Nothing OrElse Not listTaxDevolution?.Any() OrElse invoiceCurrencyId Is Nothing OrElse paymentCurrencyId Is Nothing OrElse (invoiceCurrencyId = paymentCurrencyId) Then
            Return New ActionResult With {.StateResult = True}
        End If

        'consulto la moneda oficial
        Dim indigo = SessionValues.Instance
        indigo.OfficialCurrencyId = _companySettingsRepository.FirstOrDefault(Function(x) True, False).OfficialCurrencyId

        'se consulta el TRM Custom de Invoice
        Dim result = _currencyAdminService.GetTRMbyCurrencyId(paymentCurrencyId, invoiceCurrencyId, indigo, dateTRM, NameOf(Invoice))

        If result Is Nothing OrElse Not result.StateResult Then
            Return New ActionResult With {.StateResult = False, .Message = $"No se logró hacer la conversión porque no se encontró TRM para la fecha {dateTRM}"}
        End If

        'obtengo la moneda del metodo de pago
        Dim paymentCurrency = _currencyAdminService.GetCurrencyById(paymentCurrencyId, New AuditMessage)

        If paymentCurrency Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = $"No se encontro la moneda de recaudo"}
        End If

        Dim tRMValue = result.ObjectEmbbeded.Value

        'se convierte segun la tasa
        Parallel.ForEach(listTaxDevolution, Sub(x)
                                                x.TaxValue = Math.Round(x.TaxValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                Dim ValueConvertion As Decimal = x.ValueWithTaxDevolution / tRMValue
                                                Dim integerPart As Integer = Math.Floor(ValueConvertion)
                                                Dim decimalPart As Decimal = Math.Ceiling((ValueConvertion * 100) - (integerPart * 100)) / 100
                                                x.ValueWithTaxDevolution = Math.Round(integerPart + decimalPart, 2, MidpointRounding.AwayFromZero)
                                                x.CurrencyId = paymentCurrencyId
                                                x.CurrencyAbbreviation = paymentCurrency?.Abbreviation
                                                x.CurrencyName = paymentCurrency?.ISO4217?.CurrencyName
                                            End Sub)

        Return New ActionResult With {.StateResult = True}
    End Function

#End Region

#Region "IDisposable Support"

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        _revenueControlDetailRepository = Nothing
        _serviceOrderDetailDistributionRepository = Nothing
        _caregroupRepository = Nothing
        _invoiceRepository = Nothing
        _healthAdministratorRepository = Nothing
        _thirdpartyRepository = Nothing
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region
#Region "Enum"
    Private Enum ELimitType
        UnitValue = 1
        TotalValue = 2
    End Enum

    Private Enum EConditionType
        None = 0
        GroupsCups = 1
        SubGroupCups = 2
        Cups = 3
        Product = 4
    End Enum

    Private Enum ECoInsurance
        InsurerCoveredValue
        PatientCoinsurance
    End Enum
#End Region
End Class