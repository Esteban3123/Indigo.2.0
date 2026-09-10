'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.CrystalRepository

Public Class DefinitionRateDetailRepository
    Inherits GenericRepository(Of DefinitionRateDetail)
    Implements IDefinitionRateDetailRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un detalle de definicion de tarifa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRateDetailById(id As Integer, Optional tracking As Boolean = True) As DefinitionRateDetail Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If tracking Then
            Dim res = (From d In Me._context.DefinitionRateDetail Where d.Id = id Select d).FirstOrDefault()
            If res IsNot Nothing Then
                res.OriginalValue = (From d As DefinitionRateDetail In Me._context.DefinitionRateDetail.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
                Return res
            End If
            Return New DefinitionRateDetail
        Else
            Dim res = (From d In Me._context.DefinitionRateDetail.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            If res IsNot Nothing Then
                Return res
            End If
            Return New DefinitionRateDetail
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de condiciones del detalle de la definicion de tarifas
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition) Implements IDefinitionRateDetailRepository.GetListDefinitionRateDetailConditionByDefinitionRateDetailId
        If definitionRateDetailId = 0 Then
            Throw New ArgumentNullException("definitionRateDetailId")
        End If
        Dim res = (From d In _context.DefinitionRateDetailCondition.AsNoTracking
                   Where d.DefinitionRateDetailId = definitionRateDetailId
                   Select d).ToList
        If res.Count > 0 Then
            Dim dictionaryRateManualValidity = New Dictionary(Of Integer, RateManualValidity)()
            Dim dictionaryRateManual As Dictionary(Of Integer, RateManual) = New Dictionary(Of Integer, RateManual)()
            Dim dictionaryFunctionalUnit As Dictionary(Of Integer, FunctionalUnit) = New Dictionary(Of Integer, FunctionalUnit)
            Dim dictionaryDescriptions As Dictionary(Of Integer, ContractDescriptions) = New Dictionary(Of Integer, ContractDescriptions)

            'Se coloca el operador logico de la cabecera
            Dim logicOperator = (From l In _context.DefinitionRateDetail.AsNoTracking Where l.Id = definitionRateDetailId Select l.LogicalOperator).FirstOrDefault

            For Each item As DefinitionRateDetailCondition In res
                'Se coloca el operador logico de la cabecera para poder concatenar toda la condicion en aplicacion
                item.LogicOperator = logicOperator

                'Se coloca el operador de la primera condicion
                item.FirstCondition = ResourceManager.GetString("Operator" & item.Operator.ToString(), "Contract")
                'Horario
                If item.StartTime IsNot Nothing Then
                    item.FirstCondition = item.FirstCondition + " (" + item.StartTime.ToString + " - " + item.EndTime.ToString + ")"
                End If

                'Especialidad 1
                'No se hace aqui, se realiza en aplicacion

                'Unidad Funcional 1
                Dim functionalUnitOne As FunctionalUnit = Nothing
                If item.FunctionalUnitId IsNot Nothing Then
                    If dictionaryFunctionalUnit.ContainsKey(item.FunctionalUnitId.Value) Then
                        functionalUnitOne = dictionaryFunctionalUnit(item.FunctionalUnitId.Value)
                    Else
                        functionalUnitOne = (From f In _context.FunctionalUnit.AsNoTracking Where f.Id = item.FunctionalUnitId Select f).FirstOrDefault()
                        dictionaryFunctionalUnit.Add(item.FunctionalUnitId.Value, functionalUnitOne)
                    End If
                End If
                If functionalUnitOne IsNot Nothing Then
                    item.FirstCondition = item.FirstCondition + " (" + functionalUnitOne.Code + " - " + functionalUnitOne.Name + ")"
                    item.FunctionalUnitDescriptionFirst = functionalUnitOne.Code + " - " + functionalUnitOne.Name
                End If

                'Tipo Unidad 1
                If item.UnitTypeId IsNot Nothing Then
                    item.FirstCondition = item.FirstCondition + " (" + ResourceManager.GetString("UnitType" & item.UnitTypeId.ToString(), "Contract") + ")"
                End If

                'RIAS 1
                'No se hace aqui, se realiza en aplicacion

                'Descripción 1
                Dim descriptionOne As ContractDescriptions = Nothing
                If item.ContractDescriptionId IsNot Nothing Then
                    If dictionaryDescriptions.ContainsKey(item.ContractDescriptionId.Value) Then
                        descriptionOne = dictionaryDescriptions(item.ContractDescriptionId.Value)
                    Else
                        descriptionOne = (From f In _context.ContractDescriptions.AsNoTracking Where f.Id = item.ContractDescriptionId Select f).FirstOrDefault()
                        dictionaryDescriptions.Add(item.ContractDescriptionId.Value, descriptionOne)
                    End If
                End If
                If descriptionOne IsNot Nothing Then
                    item.FirstCondition = item.FirstCondition + " (" + descriptionOne.Code + " - " + descriptionOne.Name + ")"
                    item.DescriptionCodeNameFirst = descriptionOne.Code + " - " + descriptionOne.Name
                End If

                'Se coloca el operador de la segunda condicion
                item.SecondCondition = ResourceManager.GetString("Operator" & item.Operator2.ToString(), "Contract")
                'Horario
                If item.StartTime2 IsNot Nothing Then
                    item.SecondCondition = item.SecondCondition + " (" + item.StartTime2.ToString + " - " + item.EndTime2.ToString + ")"
                End If

                'Especialidad 2
                'No se hace aqui, se realiza en aplicacion

                'Unidad Funcional 2
                Dim functionalUnitTwo As FunctionalUnit = Nothing
                If item.FunctionalUnitId2 IsNot Nothing Then
                    If dictionaryFunctionalUnit.ContainsKey(item.FunctionalUnitId2.Value) Then
                        functionalUnitTwo = dictionaryFunctionalUnit(item.FunctionalUnitId2.Value)
                    Else
                        functionalUnitTwo = (From f In _context.FunctionalUnit.AsNoTracking Where f.Id = item.FunctionalUnitId2 Select f).FirstOrDefault()
                        dictionaryFunctionalUnit.Add(item.FunctionalUnitId2.Value, functionalUnitTwo)
                    End If
                End If
                If functionalUnitTwo IsNot Nothing Then
                    item.SecondCondition = item.SecondCondition + " (" + functionalUnitTwo.Code + " - " + functionalUnitTwo.Name + ")"
                    item.FunctionalUnitDescriptionSecond = functionalUnitTwo.Code + " - " + functionalUnitTwo.Name
                End If

                'Tipo Unidad 2
                If item.UnitTypeId2 IsNot Nothing Then
                    item.SecondCondition = item.SecondCondition + " (" + ResourceManager.GetString("UnitType" & item.UnitTypeId2.ToString(), "Contract") + ")"
                End If

                'RIAS 2
                'No se hace aqui, se realiza en aplicacion

                'Se une todo el nombre de la condicion en aplicacion por la especialidad o rias
                'item .ConditionName = 

                'Descripción 2
                Dim descriptionTwo As ContractDescriptions = Nothing
                If item.ContractDescriptionId2 IsNot Nothing Then
                    If dictionaryDescriptions.ContainsKey(item.ContractDescriptionId2.Value) Then
                        descriptionTwo = dictionaryDescriptions(item.ContractDescriptionId2.Value)
                    Else
                        descriptionTwo = (From f In _context.ContractDescriptions.AsNoTracking Where f.Id = item.ContractDescriptionId2 Select f).FirstOrDefault()
                        dictionaryDescriptions.Add(item.ContractDescriptionId2.Value, descriptionTwo)
                    End If
                End If
                If descriptionTwo IsNot Nothing Then
                    item.SecondCondition = item.SecondCondition + " (" + descriptionTwo.Code + " - " + descriptionTwo.Name + ")"
                    item.DescriptionCodeNameSecond = descriptionTwo.Code + " - " + descriptionTwo.Name
                End If

                item.RateName = ResourceManager.GetString("LiquidationType" & item.LiquidationType.ToString(), "Contract")
                If item.SalesValue IsNot Nothing Then
                    item.RateName = item.RateName + " - " + ResourceManager.GetString("ManualType" & item.ManualType.ToString(), "Contract") + " - " + item.SalesValue.ToString()
                End If

                If item.RateManualValidityId IsNot Nothing Then
                    Dim rateManualValidity As RateManualValidity = Nothing
                    If dictionaryRateManualValidity.ContainsKey(item.RateManualValidityId.Value) Then
                        rateManualValidity = dictionaryRateManualValidity(item.RateManualValidityId.Value)
                    Else
                        rateManualValidity = (From f In _context.RateManualValidity.AsNoTracking Where f.Id = item.RateManualValidityId Select f).FirstOrDefault()
                        dictionaryRateManualValidity.Add(item.RateManualValidityId.Value, rateManualValidity)
                    End If
                    item.RateName = item.RateName + " - " + rateManualValidity.Code + " - " + rateManualValidity.Name + " - " + item.RateVariation.ToString + "%"
                    item.RateManualValidityDescription = rateManualValidity.Code + " - " + rateManualValidity.Name
                End If

                If item.RateManualId IsNot Nothing Then
                    Dim rateManual As RateManual = Nothing
                    If dictionaryRateManual.ContainsKey(item.RateManualId.Value) Then
                        rateManual = dictionaryRateManual(item.RateManualId.Value)
                    Else
                        rateManual = (From f In _context.RateManual.AsNoTracking Where f.Id = item.RateManualId Select f).FirstOrDefault()
                        dictionaryRateManual.Add(item.RateManualId.Value, rateManual)
                    End If
                    If rateManual IsNot Nothing Then
                        item.RateName = item.RateName + " - " + rateManual.Code + " - " + rateManual.Name + " - " + item.RateVariation.ToString + "%"
                        item.RateManualDescription = rateManual.Code + " - " + rateManual.Name
                    End If
                End If
            Next

            Return res
        Else
            Return New List(Of DefinitionRateDetailCondition)
        End If
    End Function

    ''' <summary>
    ''' Retorna el listado de definición de tarifas con el mismo tipo de manual tarifario
    ''' </summary>
    ''' <param name="DefinitionRateId"></param>
    ''' <param name="cupsHomologation"></param>
    ''' <returns></returns>
    Public Function GetRateManualDetailByCupsHomologation(DefinitionRateId As Integer, cups As CUPSEntity, cupsHomologation As CupsHomologation) As List(Of DefinitionRateDetailByManual) Implements IDefinitionRateDetailRepository.GetRateManualDetailByCupsHomologation
        'Consultamos que tarifas coinciden con el tipo de manual tarifario del servicio IPS
        Dim definitionRateDetail = (From drd In Me._context.DefinitionRateDetail.AsNoTracking Let type = If(drd.RateManual Is Nothing, cupsHomologation.IPSService.ServiceManual, drd.RateManual.Type)
                                    Where drd.DefinitionRateId = DefinitionRateId _
                                            AndAlso cupsHomologation.IPSService.ServiceManual = If(drd.RateManual Is Nothing, cupsHomologation.IPSService.ServiceManual, drd.RateManual.Type) _
                                            AndAlso (drd.RuleType = 5 OrElse (
                                                (drd.RuleType = 1 AndAlso drd.IPSService IsNot Nothing AndAlso drd.IPSServiceId = cupsHomologation.IPSServiceId AndAlso drd.CUPSEntityId = cupsHomologation.CupsEntityId) OrElse
                                                (drd.RuleType = 2 AndAlso drd.CUPSEntityId IsNot Nothing AndAlso drd.CUPSEntityId = cupsHomologation.CupsEntityId) OrElse
                                                (drd.RuleType = 3 AndAlso drd.CupsSubgroup IsNot Nothing AndAlso drd.CUPSSubgroupId = cups.CUPSSubGroupId) OrElse
                                                (drd.RuleType = 4 AndAlso drd.CUPSGroupId IsNot Nothing AndAlso drd.CUPSGroupId = cups.CupsSubgroup.CupsGroupId))
                                                )
                                    Order By drd.RuleType, drd.Weight Descending
                                    Select New DefinitionRateDetailByManual With {
                                            .RateManualId = drd.RateManualId,
                                            .LiquidationType = drd.LiquidationType,
                                            .RateManualValidityId = drd.RateManualValidityId,
                                            .ConditionType = drd.ConditionType,
                                            .Type = type,
                                            .RuleType = drd.RuleType,
                                            .DefinitionRateDetailId = drd.Id
                                            }).ToList()

        Return definitionRateDetail
    End Function

    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del servicio ips
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="ipsServiceId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailByIPSServiceId(definitionRateId As Integer, ipsServiceId As Integer) As List(Of DefinitionRateDetail) Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailByIPSServiceId
        Return (From drd In _context.DefinitionRateDetail Where drd.DefinitionRateId = definitionRateId And drd.IPSServiceId = ipsServiceId Order By drd.Weight Descending Select drd).ToList()
    End Function

    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del cups
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="cupsId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailByCUPSEntityId(definitionRateId As Integer, cupsId As Integer) As List(Of DefinitionRateDetail) Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailByCUPSEntityId
        Return (From drd In _context.DefinitionRateDetail Where drd.DefinitionRateId = definitionRateId And drd.CUPSEntityId = cupsId And drd.IPSServiceId Is Nothing Order By drd.Weight Descending Select drd).ToList()
    End Function


    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del subgrupo
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailByCUPSGroupId(definitionRateId As Integer, groupId As Integer) As List(Of DefinitionRateDetail) Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailByCUPSGroupId
        Return (From drd In _context.DefinitionRateDetail Where drd.DefinitionRateId = definitionRateId And drd.CUPSGroupId = groupId Order By drd.Weight Descending Select drd).ToList()
    End Function

    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del grupo
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="subgroupId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailByCUPSSubGroupId(definitionRateId As Integer, subgroupId As Integer) As List(Of DefinitionRateDetail) Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailByCUPSSubGroupId
        Return (From drd In _context.DefinitionRateDetail Where drd.DefinitionRateId = definitionRateId And drd.CUPSSubgroupId = subgroupId Order By drd.Weight Descending Select drd).ToList()
    End Function

    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el tipo de regla
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="ruleType"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailByRuleType(definitionRateId As Integer, ruleType As Integer) As List(Of DefinitionRateDetail) Implements IDefinitionRateDetailRepository.GetDefinitionRateDetailByRuleType
        Return (From drd In _context.DefinitionRateDetail Where drd.DefinitionRateId = definitionRateId And drd.RuleType = ruleType Order By drd.Weight Descending Select drd).ToList()
    End Function

    ''' <summary>
    ''' Obtiene las reglas de tipo servicio ips que estan en la definición de tarifas asociado al grupo de atención
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <param name="requestDate"></param>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    Public Function GetIPSRules(careGroupId As Integer, requestDate As Date, cupsEntityId As Integer) As List(Of InfoEntityTemp) Implements IDefinitionRateDetailRepository.GetIPSRules
        Return (From cgdr In _context.CareGroupDefinitionRate.AsNoTracking()
                Join dr In _context.DefinitionRate.AsNoTracking() On dr.Id Equals cgdr.DefinitionRateId
                Join drd In _context.DefinitionRateDetail.AsNoTracking() On drd.DefinitionRateId Equals dr.Id
                Join ce In _context.CUPSEntity.AsNoTracking() On ce.Id Equals drd.CUPSEntityId
                Join ips In _context.IPSService.AsNoTracking() On ips.Id Equals drd.IPSServiceId
                Where cgdr.CareGroupId = careGroupId AndAlso requestDate >= cgdr.InitialDate AndAlso requestDate <= cgdr.EndDate AndAlso drd.RuleType = 1 AndAlso ce.Id = cupsEntityId
                Select New InfoEntityTemp With {
                    .CUPSEntityId = ce.Id,
                    .IPSServiceId = ips.Id,
                    .CUPSEntityCodeName = ce.Code + " - " + ce.Description,
                    .IPSServiceCodeName = ips.Code + " - " + ips.Name,
                    .Presentation = ips.Presentation
                }).ToList()
    End Function

    ''' <summary>
    ''' Obtiene las reglas de tipo servicio ips que estan en la definición de tarifas asociado al centro de atencion externo en Central de mezclas
    ''' </summary>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="requestDate"></param>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    Public Function GetIPSRulesMs(ContractExternalClientId As Integer, requestDate As Date, cupsEntityId As Integer) As List(Of InfoEntityTemp) Implements IDefinitionRateDetailRepository.GetIPSRulesMs
        Return (From cgdr In _context.ContractExternalClientsDefinitionRate.AsNoTracking()
                Join dr In _context.DefinitionRate.AsNoTracking() On dr.Id Equals cgdr.DefinitionRateId
                Join drd In _context.DefinitionRateDetail.AsNoTracking() On drd.DefinitionRateId Equals dr.Id
                Join ce In _context.CUPSEntity.AsNoTracking() On ce.Id Equals drd.CUPSEntityId
                Join ips In _context.IPSService.AsNoTracking() On ips.Id Equals drd.IPSServiceId
                Where cgdr.ContractExternalClientsId = ContractExternalClientId AndAlso requestDate >= cgdr.InitialDate AndAlso requestDate <= cgdr.EndDate AndAlso drd.RuleType = 1 AndAlso ce.Id = cupsEntityId
                Select New InfoEntityTemp With {
                    .CUPSEntityId = ce.Id,
                    .IPSServiceId = ips.Id,
                    .CUPSEntityCodeName = ce.Code + " - " + ce.Description,
                    .IPSServiceCodeName = ips.Code + " - " + ips.Name,
                    .Presentation = ips.Presentation
                }).ToList()
    End Function

    ''' <summary>
    ''' Consulta los codigos de las diferentes reglas del objeto DefinitionDetailQuery
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function GetQueryToImportData(obj As DefinitionDetailQuery) As DefinitionDetailQuery Implements IDefinitionRateDetailRepository.GetQueryToImportData
        If obj Is Nothing Then
            Throw New ArgumentNullException(NameOf(obj))
        End If

        Dim currentContainer = ServerSessionValues.Current.CurrentContainer
        Dim codesIPS As List(Of String) = New List(Of String)
        Dim codesIPSQx As List(Of String) = New List(Of String)


        If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.IPSService.ToString() AndAlso x.Value.Any()) Then
            codesIPS.AddRange(obj.ListOfCodes(EObjectTypeDefinition.IPSService.ToString()))
        End If
        If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.IPSServiceQx.ToString() AndAlso x.Value.Any()) Then
            codesIPSQx.AddRange(obj.ListOfCodes(EObjectTypeDefinition.IPSServiceQx.ToString()))
        End If

        Dim listIPSService = Task.Factory.StartNew(Function() As List(Of IPSService)
                                                       Dim codes = New List(Of String)
                                                       codes.AddRange(codesIPS)
                                                       codes.AddRange(codesIPSQx)
                                                       If codes.Any() Then
                                                           Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                               Return (From x In newContext.IPSService.AsNoTracking().Include("SurgicalProcedureService")
                                                                       Where codes.Contains(x.Code) AndAlso x.Status = True
                                                                       Select x).ToList()
                                                           End Using
                                                       Else
                                                           Return New List(Of IPSService)
                                                       End If
                                                   End Function)

        Dim listCUPS = Task.Factory.StartNew(Function() As List(Of CUPSEntity)
                                                 If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.CUPS.ToString() AndAlso x.Value.Any()) Then
                                                     Dim cupsCodes = obj.ListOfCodes(EObjectTypeDefinition.CUPS.ToString())
                                                     Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                         Return (From x In newContext.CUPSEntity.AsNoTracking()
                                                                 Where cupsCodes.Contains(x.Code) AndAlso x.Status = True
                                                                 Select x).ToList()
                                                     End Using
                                                 Else
                                                     Return New List(Of CUPSEntity)
                                                 End If
                                             End Function)

        Dim listSubGroupsCUPS = Task.Factory.StartNew(Function() As List(Of CupsSubgroup)
                                                          If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.SubGroupsCUPS.ToString() AndAlso x.Value.Any()) Then
                                                              Dim subGroupsCodes = obj.ListOfCodes(EObjectTypeDefinition.SubGroupsCUPS.ToString())
                                                              Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                                  Return (From x In newContext.CupsSubgroup.AsNoTracking()
                                                                          Where subGroupsCodes.Contains(x.Code) AndAlso x.Status = True
                                                                          Select x).ToList()
                                                              End Using
                                                          Else
                                                              Return New List(Of CupsSubgroup)
                                                          End If
                                                      End Function)

        Dim listGroupCUPS = Task.Factory.StartNew(Function() As List(Of CupsGroup)
                                                      If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.GroupCUPS.ToString() AndAlso x.Value.Any()) Then
                                                          Dim groupCUPSCodes = obj.ListOfCodes(EObjectTypeDefinition.GroupCUPS.ToString())
                                                          Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                              Return (From x In newContext.CupsGroup.AsNoTracking()
                                                                      Where groupCUPSCodes.Contains(x.Code) AndAlso x.Status = True
                                                                      Select x).ToList()
                                                          End Using
                                                      Else
                                                          Return New List(Of CupsGroup)
                                                      End If
                                                  End Function)

        Dim listRateManual = Task.Factory.StartNew(Function() As List(Of RateManual)
                                                       If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.RateManual.ToString() AndAlso x.Value.Any()) Then
                                                           Dim rateManualCodes = obj.ListOfCodes(EObjectTypeDefinition.RateManual.ToString())
                                                           Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                               Return (From x In newContext.RateManual.AsNoTracking()
                                                                       Where rateManualCodes.Contains(x.Code) AndAlso x.Status = True
                                                                       Select x).ToList()
                                                           End Using
                                                       Else
                                                           Return New List(Of RateManual)
                                                       End If
                                                   End Function)

        Dim listRateManualValidity = Task.Factory.StartNew(Function() As List(Of RateManualValidity)
                                                               If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinition.RateManualValidity.ToString() AndAlso x.Value.Any()) Then
                                                                   Dim rateManualValidityCodes = obj.ListOfCodes(EObjectTypeDefinition.RateManualValidity.ToString())
                                                                   Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                                       Return (From x In newContext.RateManualValidity.AsNoTracking()
                                                                               Where rateManualValidityCodes.Contains(x.Code) AndAlso x.Status = True
                                                                               Select x).ToList()
                                                                   End Using
                                                               Else
                                                                   Return New List(Of RateManualValidity)
                                                               End If
                                                           End Function)
        Task.WaitAll(listIPSService,
                    listCUPS,
                    listSubGroupsCUPS,
                    listGroupCUPS,
                    listRateManual,
                    listRateManualValidity)

        With obj
            .ListOfObjects = New Dictionary(Of String, Object) From {{EObjectTypeDefinition.IPSService.ToString(), listIPSService.Result.FindAll(Function(x) codesIPS.Contains(x.Code))},
                                                                    {EObjectTypeDefinition.IPSServiceQx.ToString, listIPSService.Result.FindAll(Function(x) codesIPSQx.Contains(x.Code))},
                                                                    {EObjectTypeDefinition.CUPS.ToString, listCUPS.Result},
                                                                    {EObjectTypeDefinition.SubGroupsCUPS.ToString, listSubGroupsCUPS.Result},
                                                                    {EObjectTypeDefinition.GroupCUPS.ToString, listGroupCUPS.Result},
                                                                    {EObjectTypeDefinition.RateManual.ToString, listRateManual.Result},
                                                                    {EObjectTypeDefinition.RateManualValidity.ToString, listRateManualValidity.Result}}
        End With
        Return obj
    End Function


    ''' <summary>
    ''' Consulta los codigos de las diferentes reglas del objeto DefinitionDetailQuery
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function GetQueryToImportCoditionData(obj As DefinitionDetailQuery) As DefinitionDetailQuery Implements IDefinitionRateDetailRepository.GetQueryToImportCoditionData
        If obj Is Nothing Then
            Throw New ArgumentNullException(NameOf(obj))
        End If

        Dim currentContainer = ServerSessionValues.Current.CurrentContainer

        Dim listINESPECIA = Task.Factory.StartNew(Function() As List(Of INESPECIA)
                                                      If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.Specialty.ToString() AndAlso x.Value.Any()) Then
                                                          Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.Specialty.ToString())
                                                          Using newContext As New CrystalModelUnitOfWork(currentContainer)

                                                              Return (From x In newContext.INESPECIA.AsNoTracking()
                                                                      Where codes.Contains(x.CODESPECI) AndAlso x.ESTADO = True
                                                                      Select x).ToList()
                                                          End Using
                                                      Else
                                                          Return New List(Of INESPECIA)
                                                      End If
                                                  End Function)

        Dim listFunctionalUnit = Task.Factory.StartNew(Function() As List(Of FunctionalUnit)
                                                           If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.FunctionalUnit.ToString() AndAlso x.Value.Any()) Then
                                                               Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.FunctionalUnit.ToString())
                                                               Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                                   Return (From x In newContext.FunctionalUnit.AsNoTracking()
                                                                           Where codes.Contains(x.Code) AndAlso x.State = True
                                                                           Select x).ToList()
                                                               End Using
                                                           Else
                                                               Return New List(Of FunctionalUnit)
                                                           End If
                                                       End Function)

        Dim listRIAS = Task.Factory.StartNew(Function() As List(Of RIAS)
                                                 If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.RIAS.ToString() AndAlso x.Value.Any()) Then
                                                     Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.RIAS.ToString())
                                                     Using newContext As New CrystalModelUnitOfWork(currentContainer)
                                                         Return (From x In newContext.RIAS.AsNoTracking()
                                                                 Where codes.Contains(x.CODPRO) AndAlso x.ESTADO = 1
                                                                 Select x).ToList()
                                                     End Using
                                                 Else
                                                     Return New List(Of RIAS)
                                                 End If
                                             End Function)

        Dim listContractDescriptions = Task.Factory.StartNew(Function() As List(Of ContractDescriptions)
                                                                 If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.Description.ToString() AndAlso x.Value.Any()) Then
                                                                     Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.Description.ToString())
                                                                     Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                                         Return (From x In newContext.ContractDescriptions.AsNoTracking()
                                                                                 Where codes.Contains(x.Code) AndAlso x.Status = True
                                                                                 Select x).ToList()
                                                                     End Using
                                                                 Else
                                                                     Return New List(Of ContractDescriptions)
                                                                 End If
                                                             End Function)

        Dim listRateManual = Task.Factory.StartNew(Function() As List(Of RateManual)
                                                       If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.RateManual.ToString() AndAlso x.Value.Any()) Then
                                                           Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.RateManual.ToString())
                                                           Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                               Return (From x In newContext.RateManual.AsNoTracking()
                                                                       Where codes.Contains(x.Code) AndAlso x.Status = True
                                                                       Select x).ToList()
                                                           End Using
                                                       Else
                                                           Return New List(Of RateManual)
                                                       End If
                                                   End Function)

        Dim listRateManualValidity = Task.Factory.StartNew(Function() As List(Of RateManualValidity)
                                                               If obj.ListOfCodes.Any(Function(x) x.Key = EObjectTypeDefinitionCondition.RateManualValidity.ToString() AndAlso x.Value.Any()) Then
                                                                   Dim codes = obj.ListOfCodes(EObjectTypeDefinitionCondition.RateManualValidity.ToString())
                                                                   Using newContext As New GlobalModelUnitOfWork(currentContainer)
                                                                       Return (From x In newContext.RateManualValidity.AsNoTracking()
                                                                               Where codes.Contains(x.Code) AndAlso x.Status = True
                                                                               Select x).ToList()
                                                                   End Using
                                                               Else
                                                                   Return New List(Of RateManualValidity)
                                                               End If
                                                           End Function)
        Task.WaitAll(listINESPECIA,
                    listFunctionalUnit,
                    listRIAS,
                    listContractDescriptions,
                    listRateManual,
                    listRateManualValidity)

        With obj
            .ListOfObjects = New Dictionary(Of String, Object) From {{EObjectTypeDefinitionCondition.Specialty.ToString(), listINESPECIA.Result},
                                                                    {EObjectTypeDefinitionCondition.FunctionalUnit.ToString, listFunctionalUnit.Result},
                                                                    {EObjectTypeDefinitionCondition.RIAS.ToString, listRIAS.Result},
                                                                    {EObjectTypeDefinitionCondition.Description.ToString, listContractDescriptions.Result},
                                                                    {EObjectTypeDefinitionCondition.RateManual.ToString, listRateManual.Result},
                                                                    {EObjectTypeDefinitionCondition.RateManualValidity.ToString, listRateManualValidity.Result}}
        End With
        Return obj
    End Function

End Class
