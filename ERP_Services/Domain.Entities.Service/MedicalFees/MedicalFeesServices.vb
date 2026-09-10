'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/12/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Globalization
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports System.Text
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class MedicalFeesServices

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

    ''' <summary>
    ''' Repositorio de contratos profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesContractRepository As IMedicalFeesContractRepository

    ''' <summary>
    ''' Repositorio de contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepository As IContractRepository

    ''' <summary>
    ''' Repositorio de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualRepository As IRateManualRepository

    ''' <summary>
    ''' Repositorio de la entidad INPROFSAL de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalRepository As IHealthProfessionalRepository

    ''' <summary>
    ''' Repositorio de la entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As Domain.Maintenance.ISupplierRepository

    ''' <summary>
    ''' Repositorio para cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Repositorio para proveedor lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLineRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Repositorio para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLineRepository As IDistributionLinesRepository

    ''' <summary>
    ''' Repositorio para parametros de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsMedicalFeesRepository As IMedicalFeesSettingsRepository

    ''' <summary>
    ''' Repositorio para liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesLiquidationRepository As IMedicalFeesLiquidationRepository

    ''' <summary>
    ''' Repositorio para entidades cups
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsEntityRepository As ICupsEntityRepository

    ''' <summary>
    ''' Repositorio para ipsService
    ''' </summary>
    ''' <remarks></remarks>
    Private _ipsServiceRepository As IIPSServicesRepository

    ''' <summary>
    ''' Repositorio para el detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository

    ''' <summary>
    ''' Repositorio para el detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository

    ''' <summary>
    ''' Repositorio para billing
    ''' </summary>
    ''' <remarks></remarks>
    Private _billingService As IBillingServices

    ''' <summary>
    ''' Repositorio para contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractService As IContractServices

    ''' <summary>
    ''' Repositorio de homologaciones cups
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsHomologationRepository As ICupsHomologationRepository

    ''' <summary>
    ''' Repositorio de detalles quirurgico de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository

    ''' <summary>
    ''' Repositorio para detalles de procedimientos quirugicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository

    ''' <summary>
    ''' Repositorio para los detalles de contratos del médico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalContractRepository As IHealthProfessionalContractRepository

    ''' <summary>
    ''' Repositorio para los detalles de la liquidacion de honorarios
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesLiquidationDetailRepository As IMedicalFeesLiquidationDetailRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor utilizado para causacion
    ''' </summary>
    Public Sub New(medicalFeesContractRepository As IMedicalFeesContractRepository, contractRepository As IContractRepository, rateManualRepository As IRateManualRepository,
                   healthProfessionalRepository As IHealthProfessionalRepository, cupsEntityRepository As ICupsEntityRepository, ipsServiceRepository As IIPSServicesRepository,
                   serviceOrderDetailRepository As IServiceOrderDetailRepository, serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository, billingService As IBillingServices,
                   contractService As IContractServices, cupsHomologationRepository As ICupsHomologationRepository, rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository,
                   surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository, healthProfessionalContractRepository As IHealthProfessionalContractRepository,
                   medicalFeesLiquidationDetailRepository As IMedicalFeesLiquidationDetailRepository)
        _medicalFeesContractRepository = medicalFeesContractRepository
        _contractRepository = contractRepository
        _rateManualRepository = rateManualRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _cupsEntityRepository = cupsEntityRepository
        _ipsServiceRepository = ipsServiceRepository
        _serviceOrderDetailRepository = serviceOrderDetailRepository
        _serviceOrderDetailSurgicalRepository = serviceOrderDetailSurgicalRepository
        _billingService = billingService
        _contractService = contractService
        _cupsHomologationRepository = cupsHomologationRepository
        _rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        _surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        _healthProfessionalContractRepository = healthProfessionalContractRepository
        _medicalFeesLiquidationDetailRepository = medicalFeesLiquidationDetailRepository
    End Sub

    ''' <summary>
    ''' Constructor utilizado para liquidacion
    ''' </summary>
    Public Sub New(supplierRepository As Domain.Maintenance.ISupplierRepository, medicalFeesContractRepository As IMedicalFeesContractRepository, healthProfessionalRepository As IHealthProfessionalRepository,
                   accountPayableRepository As IAccountPayableRepository, supplierDistributionLineRepository As ISuppliersDistributionLinesRepository, distributionLineRepository As IDistributionLinesRepository,
                   settingsMedicalFeesRepository As IMedicalFeesSettingsRepository, medicalFeesLiquidationRepository As IMedicalFeesLiquidationRepository)
        _supplierRepository = supplierRepository
        _medicalFeesContractRepository = medicalFeesContractRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _accountPayableRepository = accountPayableRepository
        _supplierDistributionLineRepository = supplierDistributionLineRepository
        _distributionLineRepository = distributionLineRepository
        _settingsMedicalFeesRepository = settingsMedicalFeesRepository
        _medicalFeesLiquidationRepository = medicalFeesLiquidationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity)))
        'Variable para el retorno del valor causado por item
        Dim resultCausedValue As ActionResult(Of List(Of CupsHomologation)) = Nothing
        ''Listado de errores si se presentan
        'Dim ListErrors As New StringBuilder

        If ListNoSurgical IsNot Nothing AndAlso ListNoSurgical.Count > 0 Then 'Si viene de la rejilla de NoQx
            'Se recorre el listado NoQx
            For Each item In ListNoSurgical

                'Se realiza el llamado al metodo que causa en la cual me devuelve el valor causado o el listado de homologaciones para que el usuario escoja
                resultCausedValue = CausedValue(item.MedicalFeesCausationId, item.RateManualType, item.CupsEntityId, item.CareGroupId, item.RateManualId, item.TotalSalesPrice, item.Presentation, item.IPSServiceId, item.IPSServiceDescription, item.PerformsHealthProfessionalCode, item.ThirdPartyDescription, Nothing, item.ServiceOrderDetailId, 0, item.MedicalFeesContractId, item.ListCupsHomologation)
                'Si hubo un error se registra en el listado de errores y se continua con el siguiente item
                If resultCausedValue.StateResult = False Then
                    'ListErrors.AppendLine(resultCausedValue.Message)
                    item.StatusField = 0
                    item.MessageField = resultCausedValue.Message
                    Continue For
                End If

                'Se agregan al listado las homologaciones que vienen del metodo CausedValue
                item.StatusField = 1
                If resultCausedValue.ObjectEmbbeded IsNot Nothing AndAlso resultCausedValue.ObjectEmbbeded.Count > 0 Then
                    item.IsHomologations = True
                    item.ListCupsHomologation = resultCausedValue.ObjectEmbbeded
                Else 'Se agrean los valores que devuelve el metodo CausedValue
                    item.IsHomologations = False
                    item.AmountPayable = resultCausedValue.MessageResult(0)
                    item.MedicalFeesContractId = resultCausedValue.Message
                    'TotalAmountPayable = se calcula en la presentación
                    item.MedicalFeesContractCodeName = resultCausedValue.MessageResult(1)
                End If

            Next
        ElseIf ListSurgical IsNot Nothing AndAlso ListSurgical.Count > 0 Then 'Si viene de la rejilla de Qx
            'Se recorre el listado Qx
            For Each item In ListSurgical

                'Se realiza el llamado al metodo que causa en la cual me devuelve el valor causado o el listado de homologaciones para que el usuario escoja
                resultCausedValue = CausedValue(item.MedicalFeesCausationId, item.RateManualType, item.CupsEntityId, item.CareGroupId, item.RateManualId, item.TotalSalesPrice, item.Presentation, item.IPSServiceId, item.IPSServiceDescription, item.PerformsHealthProfessionalCode, item.ThirdPartyDescription, item.IPSServiceSODId, item.ServiceOrderDetailId, item.ServiceOrderDetailSurgicalId, item.MedicalFeesContractId, item.ListCupsHomologation)
                'Si hubo un error se registra en el listado de errores y se continua con el siguiente item
                If resultCausedValue.StateResult = False Then
                    'ListErrors.AppendLine(resultCausedValue.Message)
                    item.StatusField = 0
                    item.MessageField = resultCausedValue.Message
                    Continue For
                End If

                'Se agregan al listado las homologaciones que vienen del metodo CausedValue
                item.StatusField = 1
                If resultCausedValue.ObjectEmbbeded IsNot Nothing AndAlso resultCausedValue.ObjectEmbbeded.Count > 0 Then
                    item.IsHomologations = True
                    item.ListCupsHomologation = resultCausedValue.ObjectEmbbeded
                Else 'Se agrean los valores que devuelve el metodo CausedValue
                    item.IsHomologations = False
                    item.AmountPayable = resultCausedValue.MessageResult(0)
                    item.MedicalFeesContractId = resultCausedValue.Message
                    'TotalAmountPayable = se calcula en la presentación
                    item.MedicalFeesContractCodeName = resultCausedValue.MessageResult(1)
                End If

            Next
        End If
        ''Se devuelve los errores en el caso de que hayan existido
        'If ListErrors.Length > 0 Then
        '    Return New ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity))) With {.StateResult = False, .Message = ListErrors.ToString}
        'End If

        'Se devuelven el listado ya con la info tramitada
        Dim TupleReturn As New Tuple(Of List(Of NoQxEntity), List(Of QxEntity))(ListNoSurgical, ListSurgical)
        Return New ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity))) With {.StateResult = True, .ObjectEmbbeded = TupleReturn}
    End Function

    ''' <summary>
    ''' Crea el valor causado
    ''' </summary>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="RateManualId"></param>
    ''' <param name="ValueTotal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer,
                                IPSServiceCodeName As String, healthProfessionalCode As String, thirdPartyDescription As String, IPSServiceIdParent As Integer?,
                                ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer, MedicalFeesContractId As Integer,
                                Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))

        'Si la causacion esta registrada valido que no exista en una liquidacion de honorarios con estado registrado o confirmado
        If MedicalFeesCausationId <> Nothing AndAlso MedicalFeesCausationId > 0 Then
            Dim ListTuple As New List(Of Tuple(Of Integer, Integer))
            ListTuple.Add(New Tuple(Of Integer, Integer)(MedicalFeesCausationId, 0))
            Dim ListValidate = _medicalFeesLiquidationDetailRepository.ValidateMedicalFeesCausationIdContainInMFLDConfirmedOrRegister(ListTuple)
            If ListValidate IsNot Nothing AndAlso ListValidate.Count > 0 Then
                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = "El item " + IPSServiceCodeName + " ya se encuentra en una liquidación de honorarios."}
            End If
        End If

        Dim resultValue As ActionResult(Of List(Of CupsHomologation))

        Dim _medicalFeesContractId As Integer
        'Pregunto si viene el id del contrato vacio
        If MedicalFeesContractId = 0 Then
            'Se consulta si el médico tiene asociado contratos por el codigo del medico
            Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract) = _healthProfessionalContractRepository.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode, False)
            If ListHealthProfessionalContract Is Nothing Then
                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontListContract", NAME_MODULE), thirdPartyDescription)}
            End If
            If Not ListHealthProfessionalContract.Any(Function(l) l.LiquidateDefault) Then
                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format("El médico {0} no tiene un contrato por defecto.", thirdPartyDescription)}
            End If
            'Se captura a una entidad healthProfessionalContract el contrato que viene por defecto
            Dim healthProfessionalContract As HealthProfessionalContract = (From l In ListHealthProfessionalContract Where l.LiquidateDefault = True Select l).FirstOrDefault
            _medicalFeesContractId = healthProfessionalContract.MedicalFeesContractId
        Else
            _medicalFeesContractId = MedicalFeesContractId
        End If

        'Se consulta el contrato que tiene la entidad anterior(healthProfessionalContract)
        Dim medicalFeesContract As MedicalFeesContract = _medicalFeesContractRepository.GetMedicalFeesContractById(_medicalFeesContractId)

        'Primero pregunto si hay datos en la tabla MedicalFeesContractGeneralException, si hay pregunto por cada exceptionType(1.IPSService, 2.CUPS, 3.SubGroup, 4.Group, 5.CareGroup, 6.ContractEntity, 7.RateManualType)
        If medicalFeesContract.MedicalFeesContractException IsNot Nothing AndAlso medicalFeesContract.MedicalFeesContractException.Count > 0 Then
            Dim medicalFeesContractException As MedicalFeesContractException

            'Pregunto si hay registros para IPSService
            medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.IPSServiceId = IPSServiceId AndAlso item.IPSServiceId IsNot Nothing).FirstOrDefault
            If medicalFeesContractException Is Nothing Then
                'Pregunto si hay registros para CupsEntity
                medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.CUPSEntityId = CupsEntityId AndAlso item.CUPSEntityId IsNot Nothing).FirstOrDefault
                If medicalFeesContractException Is Nothing Then
                    'Consulto CupsEntity por id, para poder saber el id del CupsSubGroup
                    Dim cupsEntity = _cupsEntityRepository.GetCupsEntityByIdSimple(CupsEntityId)
                    'Pregunto si hay registros para CupsSubGroup
                    medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.CUPSSubgroupId = cupsEntity.CUPSSubGroupId AndAlso item.CUPSSubgroupId IsNot Nothing).FirstOrDefault
                    If medicalFeesContractException Is Nothing Then
                        'Pregunto si hay registros para CupsGroup
                        medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.CUPSGroupId = cupsEntity.CupsSubgroup.CupsGroupId AndAlso item.CUPSGroupId IsNot Nothing).FirstOrDefault
                        If medicalFeesContractException Is Nothing Then
                            'Pregunto si hay registros para CareGroup
                            medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.CareGroupId = CareGroupId AndAlso item.CareGroupId IsNot Nothing).FirstOrDefault
                            If medicalFeesContractException Is Nothing Then
                                'Consulto el contrato con el id del careGroup para poder saber el id del ContractEntityId
                                Dim contract = _contractRepository.GetContractByCareGroupIdForMedicalFees(CareGroupId)
                                If contract IsNot Nothing Then
                                    'Pregunto si hay registros para ContractEntity
                                    medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.ContractEntityId = contract.ContractEntityId AndAlso item.ContractEntityId IsNot Nothing).FirstOrDefault
                                    If medicalFeesContractException Is Nothing Then
                                        'Si no encontro excepciones de tipo contractEntity busco excepciones de tipo rateManualType
                                        medicalFeesContractException = RateManualTypeMedicalFeesContractGeneralException(medicalFeesContract, RateManualId, RateManualType)
                                        'Si no encontro registros de rateManualType, busco General
                                        If medicalFeesContractException Is Nothing Then
                                            medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.ExceptionType = 8).FirstOrDefault
                                        End If
                                    End If
                                Else
                                    'Si no encontro un contrato en careGroup para poder sacar el contractEntityId entonces busco de tipo rateManualType
                                    medicalFeesContractException = RateManualTypeMedicalFeesContractGeneralException(medicalFeesContract, RateManualId, RateManualType)
                                    'Si no encontro registros de rateManualType, busco General
                                    If medicalFeesContractException Is Nothing Then
                                        medicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.ExceptionType = 8).FirstOrDefault
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

            End If

            'Pregunto si existe las excepciones
            If medicalFeesContractException IsNot Nothing Then
                resultValue = ValueReturn(medicalFeesContractException.RateType, medicalFeesContractException.PercentageRate, medicalFeesContractException.RateManualId, medicalFeesContractException.RateVariation,
                                         medicalFeesContractException.AmountPayable, ValueTotal, presentation, IPSServiceId, IPSServiceCodeName, IPSServiceIdParent, ServiceOrderDetailId, ServiceOrderDetailSurgicalId, ListCupsHomologation)
                If resultValue.StateResult = False Then
                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = resultValue.Message}
                End If

                'ObjectEmbbeded = Envio el listado de homologaciones cups
                'Message = Envio el id del contrato profesional de la salud
                'MessageResult(0) = Envio el valor causado
                'MessageResult(1) = Envio el codigo y nombre del contrato
                Dim ListReturn As New List(Of String)
                ListReturn.Add(resultValue.Message)
                ListReturn.Add(medicalFeesContract.Code + " - " + medicalFeesContract.ContractName)
                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = resultValue.ObjectEmbbeded, .Message = medicalFeesContract.Id, .MessageResult = ListReturn}
            Else
                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontExistExceptions", NAME_MODULE), medicalFeesContract.Code + " - " + LTrim(RTrim(medicalFeesContract.ContractName)), IPSServiceCodeName)}
            End If

        Else
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontExistExceptions", NAME_MODULE), medicalFeesContract.Code + " - " + medicalFeesContract.ContractName, IPSServiceCodeName)}
        End If
    End Function

    ''' <summary>
    ''' Calcula el valor a causar
    ''' </summary>
    ''' <param name="rateType"></param>
    ''' <param name="percentageRate"></param>
    ''' <param name="rateManualId"></param>
    ''' <param name="rateVariation"></param>
    ''' <param name="amountPayable"></param>
    ''' <param name="valueTotal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValueReturn(rateType As Integer?, percentageRate As Decimal?, rateManualId As Integer?, rateVariation As Decimal?, amountPayable As Decimal?, valueTotal As Decimal?, presentation As Integer?,
                                 ipseServiceId As Integer?, IPSServiceCodeName As String, IPSServiceIdParent As Integer?, ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer,
                                  ListCupsHomologation As List(Of CupsHomologation)) As ActionResult(Of List(Of CupsHomologation))
        Dim valuePercentage As Decimal
        Select Case rateType
            Case 1 'Por % del valor cobrado
                valuePercentage = percentageRate * valueTotal / 100
                valueTotal = valuePercentage
            Case 2 'Por Manual Tarifario

                'Consulto el manual tarifario por id con agregados
                Dim rateManual = _rateManualRepository.GetRateManualByIdWithAggregates(rateManualId)
                If ServiceOrderDetailSurgicalId > 0 Then 'Se consulta el serviceOrderDetailSurgical por id, si este no viene en 0, esto quiere decir que es un detalle de un Qx

                    'Se consulta el serviceClass del ipsService hijo y se asigna a la variable
                    Dim ipsServiceSon = _ipsServiceRepository.GetIPSServiceById(ipseServiceId, False)
                    Dim serviceClassIPSServiceSon = ipsServiceSon.ServiceClass

                    'Se consulta el detalle quirurgico de la orden de servicio por id
                    Dim serviceOrderDetailSurgical = _serviceOrderDetailSurgicalRepository.GetServiceOrderDetailSurgicalById(ServiceOrderDetailSurgicalId, False)

                    'Se consulta el servicio ips que trae la orden de servicio
                    Dim ipsServiceTemp = _ipsServiceRepository.GetIPSServiceById(serviceOrderDetailSurgical.IPSServiceId)

                    If ipsServiceTemp.ServiceManual = rateManual.Type Then 'si el manual tarifario es del mismo tipo que con el que se liquido
                        If rateManual.Type = 3 Then 'Si se liquida a SOAT
                            If rateManual.RateManualDetailSurgical IsNot Nothing AndAlso rateManual.RateManualDetailSurgical.Count > 0 Then
                                Dim rateManualDetailSurgical = rateManual.RateManualDetailSurgical.ToList.FindAll(Function(item) item.IPSServiceId = ipseServiceId).FirstOrDefault
                                If rateManualDetailSurgical IsNot Nothing Then
                                    valuePercentage = rateVariation * rateManualDetailSurgical.SalesValue / 100
                                    valueTotal = rateManualDetailSurgical.SalesValue + valuePercentage
                                Else
                                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontRateManual", NAME_MODULE), IPSServiceCodeName, rateManual.Code + " - " + rateManual.Name)}
                                End If
                            Else
                                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontRateManual", NAME_MODULE), IPSServiceCodeName, rateManual.Code + " - " + rateManual.Name)}
                            End If
                        Else 'Si se liquida a ISS
                            'Se consulta el ipsService hijo
                            'Dim ipsServiceSon = _ipsServiceRepository.GetIPSServiceById(ipseServiceId, False)
                            'Se consulta el ipsService padre
                            Dim ipsServiceParent = _ipsServiceRepository.GetIPSServiceById(IPSServiceIdParent, False)
                            Dim score As Decimal = ipsServiceSon.Score

                            If ipsServiceSon.ApplyChangeScore = True And ipsServiceParent.UVRNumber > 450 Then 'Si aplica para cambio de valor y el uvr es mayor a 450
                                score = ipsServiceSon.NewScore
                            End If
                            Dim totalUVR As Decimal = score * ipsServiceParent.UVRNumber
                            valuePercentage = rateVariation * totalUVR / 100
                            valueTotal = totalUVR + valuePercentage
                        End If
                    Else 'Si el manual que con el que se le paga al medico es diferente al manual con el que se liquido en facturacion

                        'Se consulta el listado de homologaciones dependiendo
                        Dim listHomologation
                        If ListCupsHomologation IsNot Nothing Then 'Si el listado viene lleno es porque en el form de MedicalFeesCausation aparecio el showPopup de homologaciones y escogieron una
                            listHomologation = ListCupsHomologation
                        Else 'Sino se consulta para ver cuantas homologaciones tiene
                            listHomologation = _cupsHomologationRepository.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManualMedicalFeesCausation(serviceOrderDetailSurgical.ServiceOrderDetail.CUPSEntityId, rateManual.Type, True)
                        End If

                        If listHomologation.Count > 0 Then 'Si tiene homologacion al nuevo manual tarifario
                            If listHomologation.Count = 1 Then 'Si la homologaciones de 1 a 1
                                'Se consulta el listado de detalles de procedimientos quirurgicos
                                'por el ipsServiceParent(listHomologation(0).IPSServiceId) y la clase del ipsServiceSon
                                Dim listSurgicalProcedureService = _surgicalProcedureServiceRepository.GetListSurgicalProcedureService(listHomologation(0).IPSServiceId, serviceClassIPSServiceSon)
                                Dim surgicalProcedureSon As SurgicalProcedureService = Nothing
                                If listSurgicalProcedureService IsNot Nothing AndAlso listSurgicalProcedureService.Count > 0 Then
                                    surgicalProcedureSon = (From l In listSurgicalProcedureService Where l.DefaultService = True Select l).SingleOrDefault()
                                Else 'Mostramos la valdacion de que el item XXXX no tiene detalles Qx
                                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ""}
                                End If

                                'Se valida que haya servicio quirurgico por defecto con la clase seleccionada
                                If surgicalProcedureSon Is Nothing Then
                                    Dim ipsValidation = _ipsServiceRepository.GetIPSServiceById(listHomologation(0).IPSServiceId, False)
                                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = "El servicio ips " + ipsValidation.Code + " - " + ipsValidation.Name + " no tiene servicios quirúrgicos con valor por defecto para la clase " + ResourceManager.GetString("ServiceClass" + serviceClassIPSServiceSon.ToString, "Contract")}
                                End If

                                If rateManual.Type = 3 Then 'Si se liquida a soat
                                    Dim rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManual.Id, surgicalProcedureSon.IPSServiceId, listHomologation(0).IPSService.SurgicalGroupId,
                                                                                                                                 listHomologation(0).IPSService.UVRNumber, listHomologation(0).IPSService.ServiceManual)

                                    valuePercentage = rateVariation * rateManualDetailSurgical.SalesValue / 100
                                    valueTotal = rateManualDetailSurgical.SalesValue + valuePercentage
                                Else 'Si se liquida a ISS
                                    Dim ipsServiceParent = _ipsServiceRepository.GetIPSServiceById(listHomologation(0).IPSServiceId, False)
                                    ipsServiceSon = _ipsServiceRepository.GetIPSServiceById(surgicalProcedureSon.IPSServiceId, False)
                                    Dim score As Decimal = ipsServiceSon.Score

                                    If ipsServiceSon.ApplyChangeScore = True And ipsServiceParent.UVRNumber > 450 Then 'Si aplica para cambio de valor y el uvr es mayor a 450
                                        score = ipsServiceSon.NewScore
                                    End If
                                    Dim totalUVR As Decimal = score * ipsServiceParent.UVRNumber
                                    valuePercentage = rateVariation * totalUVR / 100
                                    valueTotal = totalUVR + valuePercentage
                                End If

                            Else 'Si la homologacion es de uno a muchos tengo que devolverle las opciones para que el usuario escoja
                                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = listHomologation}
                            End If
                        Else 'Si no tiene homologacion al nuevo manual tarifario entonces le devuelvo el mensaje
                            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontHomologationItem", NAME_MODULE), IPSServiceCodeName)}
                        End If
                    End If

                Else 'Sino se consulta el serviceOrderDetail por id, esto quiere decir que es un No Quirurgico

                    'Se consulta el detalle de la orden de servicio por id
                    Dim serviceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(ServiceOrderDetailId, False)

                    'Se consulta el servicio ips que trae la orden de servicio
                    Dim ipsServiceTemp = _ipsServiceRepository.GetIPSServiceById(serviceOrderDetail.IPSServiceId)

                    'si el manual tarifario es del mismo tipo que con el que se liquido y el detalle de la orden de servicio tiene amarrado el manual tarifario y además también es igual al tipo de servicio, sino se va por el else y se realiza el proceso con el rateManual consultado anteriormente
                    'If serviceOrderDetail.RateManual IsNot Nothing AndAlso serviceOrderDetail.RateManual.Type = rateManual.Type AndAlso
                    '    ((serviceOrderDetail.ServiceType = 1 AndAlso serviceOrderDetail.RateManual.Type = 3) OrElse
                    '    (serviceOrderDetail.ServiceType = 2 AndAlso (serviceOrderDetail.RateManual.Type = 1 OrElse serviceOrderDetail.RateManual.Type = 2))) Then

                    'Se el tipo de manual del servicio de la orden es igual al tipo de manual de las reglas de liquidacion de contratos honorarios
                    If ipsServiceTemp.ServiceManual = rateManual.Type Then
                        If rateManual.RateManualDetail IsNot Nothing AndAlso rateManual.RateManualDetail.Count > 0 Then
                            Dim rateManualDetail = rateManual.RateManualDetail.ToList.FindAll(Function(item) item.IPSServiceId = ipseServiceId).FirstOrDefault
                            If rateManualDetail IsNot Nothing Then
                                valuePercentage = rateVariation * rateManualDetail.SalesValue / 100
                                valueTotal = rateManualDetail.SalesValue + valuePercentage
                                'Se redondea el valor dependiendo de como este parametrizado el manual tarifario
                                valueTotal = Utils.RoundValue(CDec(valueTotal), rateManual.RoundService)

                                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = Nothing, .Message = valueTotal.ToString}
                            End If
                        Else
                            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontRateManual", NAME_MODULE), IPSServiceCodeName, rateManual.Code + " - " + rateManual.Name)}
                        End If
                    End If

                    'Se consulta el listado de homologaciones 
                    Dim listHomologation
                    If ListCupsHomologation IsNot Nothing Then 'Si el listado viene lleno es porque en el form de MedicalFeesCausation aparecio el showPopup de homologaciones y escogieron una
                        listHomologation = ListCupsHomologation
                    Else 'Sino se consulta para ver cuantas homologaciones tiene
                        listHomologation = _cupsHomologationRepository.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManualMedicalFeesCausation(serviceOrderDetail.CUPSEntityId, rateManual.Type, False)
                    End If

                    If listHomologation.Count > 0 Then 'Si tiene homologacion al nuevo manual tarifario

                        If listHomologation.Count = 1 Then 'Si la homologaciones de 1 a 1
                            If rateManual.RateManualDetail IsNot Nothing AndAlso rateManual.RateManualDetail.Count > 0 Then
                                Dim rateManualDetail = rateManual.RateManualDetail.ToList.FindAll(Function(item) item.IPSServiceId = listHomologation(0).IPSServiceId).FirstOrDefault
                                If rateManualDetail IsNot Nothing Then
                                    valuePercentage = rateVariation * rateManualDetail.SalesValue / 100
                                    valueTotal = rateManualDetail.SalesValue + valuePercentage
                                Else
                                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontRateManual", NAME_MODULE), IPSServiceCodeName, rateManual.Code + " - " + rateManual.Name)}
                                End If
                            Else
                                Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontRateManual", NAME_MODULE), IPSServiceCodeName, rateManual.Code + " - " + rateManual.Name)}
                            End If
                        Else 'Si la homologacion es de uno a muchos tengo que devolverle las opciones para que el usuario escoja
                            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = listHomologation}
                        End If

                    Else 'Si no tiene homologacion al nuevo manual tarifario entonces le devuelvo el mensaje
                        Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("DontHomologationItem", NAME_MODULE), IPSServiceCodeName)}
                    End If

                End If

                'Se redondea el valor dependiendo de como este parametrizado el manual tarifario
                valueTotal = Utils.RoundValue(CDec(valueTotal), rateManual.RoundService)

            Case 3 'Por Valor Fijo
                valueTotal = amountPayable
        End Select
        Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = Nothing, .Message = valueTotal.ToString}
    End Function

    ''' <summary>
    ''' Consulta el rateManualType
    ''' </summary>
    ''' <param name="medicalFeesContract"></param>
    ''' <param name="RateManualId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RateManualTypeMedicalFeesContractGeneralException(medicalFeesContract As MedicalFeesContract, RateManualId As Integer, RateManualType As Integer) As MedicalFeesContractException
        Dim MedicalFeesContractException As MedicalFeesContractException
        'Consulto el RateManual para saber el RateManualType y poder hacer el where para este tipo de casos
        'Dim rateManual = _rateManualRepository.GetRateManualById(RateManualId)
        'Pregunto si hay registros para RateManualType
        MedicalFeesContractException = medicalFeesContract.MedicalFeesContractException.ToList.FindAll(Function(item) item.RateManualType = RateManualType AndAlso item.RateManualType IsNot Nothing).FirstOrDefault
        Return MedicalFeesContractException
    End Function

    ''' <summary>
    ''' Genera la cuenta por pagar para la liquidacion de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAccountPayable(medicalFeesLiquidation As MedicalFeesLiquidation) As ActionResult(Of AccountPayable)
        Dim accountPayable As New AccountPayable
        Dim supplier As Domain.Maintenance.Entities.Supplier = Nothing

        'Se crea la cabecera de la cuenta por pagar
        With accountPayable
            .Code = String.Empty
            .EntityId = medicalFeesLiquidation.Id
            .EntityCode = medicalFeesLiquidation.Code
            .EntityName = GetType(MedicalFeesLiquidation).Name

            Dim supplierDistributionLine As SuppliersDistributionLines = Nothing
            If medicalFeesLiquidation.MedicalFeesContractId Is Nothing Then 'Estandar
                'Se consulta el medico con el codigo ingresado en el form
                Dim healthProfessional = _healthProfessionalRepository.GetHealthProfessionalByCode(medicalFeesLiquidation.HealthProfessionalCode.Trim)
                If healthProfessional IsNot Nothing AndAlso healthProfessional.CODPROSAL <> String.Empty Then
                    'Se consulta el proveedor con el id que tiene el medico
                    supplier = _supplierRepository.GetSupplierById(healthProfessional.GENPROVEE)

                    'se consulta la linea de distribucion proveedor con el campo GENLINDIST
                    supplierDistributionLine = _supplierDistributionLineRepository.GetSuppliersDistributionLinesById(healthProfessional.GENLINDIST)
                Else
                    Dim errorDescription = "El código " + medicalFeesLiquidation.HealthProfessionalCode + " no existe como médico."
                    Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {errorDescription}.ToList}
                End If
            Else 'Agremiaciones
                Dim medicalFeesContract As MedicalFeesContract = _medicalFeesContractRepository.GetMedicalFeesContractById(medicalFeesLiquidation.MedicalFeesContractId)
                'Se consulta el proveedor con el id que tiene el contrato
                supplier = _supplierRepository.GetSupplierById(medicalFeesContract.SupplierId)

                'se consulta la linea de distribucion proveedor con el campo de medicalFeesContract
                supplierDistributionLine = _supplierDistributionLineRepository.GetSuppliersDistributionLinesById(medicalFeesContract.SupplierDistributionLineId)
            End If

            'Valido que el proveedor no tenga asociado el numero de factura si el numero de factura no viene vacio
            If medicalFeesLiquidation.BillNumber <> String.Empty Then
                Dim apCompare = _accountPayableRepository.GetAccountPayableByBillNumber(medicalFeesLiquidation.BillNumber, supplier.Id)
                If apCompare.Id > 0 Then
                    Dim errorDescription As String = "El proveedor " + supplier.Code + " - " + supplier.Name + " ya tiene asociado el número de factura " + medicalFeesLiquidation.BillNumber
                    Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {errorDescription}.ToList}
                End If
            End If

            .DescriptionSupplier = supplier.Code + " - " + supplier.Name
            .IdSupplier = supplier.Id
            .IdThirdParty = supplier.IdThirdParty
            .IdAccount = supplierDistributionLine.DistributionLines.MainAccounts.Id
            .IdCostCenter = medicalFeesLiquidation.CostCenterId
            .BillNumber = medicalFeesLiquidation.BillNumber
            .BillDate = medicalFeesLiquidation.DocumentDate
            .DocumentDate = medicalFeesLiquidation.DocumentDate
            .ServicePeriodDate = medicalFeesLiquidation.DocumentDate
            .FilingUnitId = medicalFeesLiquidation.FilingUnitId
            .SupplierTypeId = medicalFeesLiquidation.SupplierTypeId
            .Term = supplier.TimeLimitDays
            .ExpirationDate = PaymentServices.AddDaysDate(.Term, .DocumentDate)
            .Coments = "Cuenta por pagar generada desde liquidación de honorarios médicos"
            .Status = 1
            .InitialBalance = False
            .IdInitialBalance = Nothing
            .PreviousBudget = False
            .Shares = 1

            'Se calcula el valor de la cxp y valida que el valor de la cuenta por pagar no sea menor a cero
            Dim valuePayments As Decimal = (From detail As MedicalFeesLiquidationDetail In medicalFeesLiquidation.MedicalFeesLiquidationDetail
                                            Where detail.LiquidationType = 1 AndAlso detail.ChangeTracker.State <> ObjectState.Deleted
                                            Select detail.TotalAmountPayable).Sum 'Pagos
            Dim valueDeductions As Decimal = (From detail As MedicalFeesLiquidationDetail In medicalFeesLiquidation.MedicalFeesLiquidationDetail
                                              Where (detail.LiquidationType = 2 OrElse detail.LiquidationType = 3) AndAlso detail.ChangeTracker.State <> ObjectState.Deleted
                                              Select detail.TotalAmountPayable).Sum 'Descuentos y Glosas
            Dim valueTotalLiquidation As Decimal = valuePayments - valueDeductions

            If valueTotalLiquidation < 0 Then
                Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"El valor de la cuenta por pagar no puede ser menor a cero."}.ToList}
            End If

            .InvoiceValue = valueTotalLiquidation
            .Value = .InvoiceValue
            .Balance = .InvoiceValue

            .IdOperatingUnit = medicalFeesLiquidation.OperatingUnitId
            .IdSuppliersDistributionLines = supplierDistributionLine.Id
        End With

        'Se crea una cuota por defecto para la cxp
        Dim accountPayableShare As New AccountPayableShares
        With accountPayableShare
            .Share = 1
            .DateExpires = accountPayable.ExpirationDate
            .InitialValue = accountPayable.Value
            .Balance = accountPayable.Value
        End With
        accountPayable.AccountPayableShares.Add(accountPayableShare)

        'Se crean los detalles de la cuenta por pagar

        'Se valida que exista parametros de honorarios medicos para poder sacar los conceptos
        Dim settingMedicalFees = _settingsMedicalFeesRepository.GetMedicalFeesSetting()
        If settingMedicalFees Is Nothing Then
            Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"No existe parámetros de honorarios médicos."}.ToList}
        End If

        'Se valida que hayan agregado al menos un detalle de liquidacion
        If medicalFeesLiquidation.MedicalFeesLiquidationDetail Is Nothing OrElse medicalFeesLiquidation.MedicalFeesLiquidationDetail.Count = 0 Then
            Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"No hay detalles de liquidación."}.ToList}
        End If

        'Se recorre los detalles de la liquidacion para poder crear los conceptos de la cxp
        For Each itemDetail As MedicalFeesLiquidationDetail In medicalFeesLiquidation.MedicalFeesLiquidationDetail
            If itemDetail.ChangeTracker.State <> ObjectState.Deleted Then
                Dim accountPayableDetailConcept As New AccountPayableDetailConcept
                With accountPayableDetailConcept
                    .IdConceptAccountPayable = settingMedicalFees.AccountPayableConceptId
                    .IdAccount = _medicalFeesLiquidationRepository.GetMainAccountIdByMedicalFeesLiquidationDeatil(itemDetail)
                    .IdThirdParty = supplier.IdThirdParty
                    .IdCostCenter = _medicalFeesLiquidationRepository.GetCostCenterIdByMedicalFeesCausation(itemDetail.MedicalFeesCausationId, .IdAccount)

                    If itemDetail.LiquidationType = 1 Then
                        .Nature = 1
                    Else
                        .Nature = 2
                    End If

                    .BaseValue = itemDetail.TotalAmountPayable
                    .BillingValue = .BaseValue
                    .Value = .BaseValue
                    .IdRetentionConcept = Nothing
                    .Percentage = Nothing
                    .Detail = "Detalle de cuenta por pagar generada desde liquidación de honorarios médicos"
                    .DeferredCausation = False
                End With
                accountPayable.AccountPayableDetailConcept.Add(accountPayableDetailConcept)
            End If
        Next

        Return New ActionResult(Of AccountPayable) With {.StateResult = True, .ObjectEmbbeded = accountPayable}
    End Function

#End Region

End Class
