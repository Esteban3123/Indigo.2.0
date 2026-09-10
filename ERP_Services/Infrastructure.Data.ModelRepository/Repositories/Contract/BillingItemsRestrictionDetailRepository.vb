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
Imports System.Configuration

Public Class BillingItemsRestrictionDetailRepository
    Inherits GenericRepository(Of BillingItemsRestrictionDetail)
    Implements IBillingItemsRestrictionDetailRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE = "Contract"

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
    ''' consulta los detalles de los servicios no facturables y sus condiciones
    ''' </summary>
    ''' <param name="idHeader"></param>
    ''' <returns></returns>
    Public Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As List(Of BillingItemsRestrictionDetail) Implements IBillingItemsRestrictionDetailRepository.GetItemsRestrictionDetailByIdHeader

        If idHeader = 0 Then
            Throw New ArgumentNullException("BillingItemsRestrictionId")
        End If
        Dim query = (From x In _context.BillingItemsRestrictionDetail.Include("BillingItemsRestrictionDetailCondition")
                     Where x.BillingItemsRestrictionId = idHeader
                     Select x).ToList()

        If query Is Nothing OrElse Not query?.Any() Then
            Return New List(Of BillingItemsRestrictionDetail)
        End If

        Dim listLogicalOperators = Utils.ListLogicOperator
        Dim UnitTypes = Utils.UnitTypes
        Dim RateManualType = Utils.RateManualType
        Dim EqualsOperator = Utils.EqualsOperator
        Dim currentContainer = ServerSessionValues.Current.CurrentContainer

        Dim listCUPS = Task.Factory.StartNew(Function() As List(Of CUPSEntity)

                                                 Dim ids = New List(Of Integer)
                                                 ids = query.GroupBy(Function(r) r.IncludeToCUPSEntityId).Select(Function(d) d.Key).ToList()

                                                 If query.Any(Function(r) r.CUPSEntityId IsNot Nothing) Then
                                                     Dim idsRuleCups As List(Of Integer) = query.Where(Function(r) r.CUPSEntityId IsNot Nothing).GroupBy(Function(e) e.CUPSEntityId).Select(Function(s) s.Key.Value).ToList()
                                                     ids.AddRange(idsRuleCups)
                                                 End If

                                                 Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                     Return (From x In context1.CUPSEntity.AsNoTracking()
                                                             Where ids.Contains(x.Id)
                                                             Select x).ToList()
                                                 End Using

                                             End Function)

        Dim listGroupCUPS = Task.Factory.StartNew(Function() As List(Of CupsGroup)

                                                      If query.Any(Function(x) x.CUPSGroupId IsNot Nothing) Then
                                                          Dim ids = query.Where(Function(r) r.CUPSGroupId IsNot Nothing).Select(Function(s) s.CUPSGroupId).ToList()
                                                          Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                              Return (From x In context1.CupsGroup.AsNoTracking()
                                                                      Where ids.Contains(x.Id)
                                                                      Select x).ToList()
                                                          End Using
                                                      Else
                                                          Return New List(Of CupsGroup)
                                                      End If
                                                  End Function)

        Dim listSubGroupCUPS = Task.Factory.StartNew(Function() As List(Of CupsSubgroup)

                                                         If query.Any(Function(x) x.CUPSSubgroupId IsNot Nothing) Then
                                                             Dim ids = query.Where(Function(r) r.CUPSSubgroupId IsNot Nothing).Select(Function(s) s.CUPSSubgroupId).ToList()
                                                             Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                                 Return (From x In context1.CupsSubgroup.AsNoTracking()
                                                                         Where ids.Contains(x.Id)
                                                                         Select x).ToList()
                                                             End Using
                                                         Else
                                                             Return New List(Of CupsSubgroup)
                                                         End If
                                                     End Function)

        Dim listInventoryProduct = Task.Factory.StartNew(Function() As List(Of InventoryProduct)

                                                             If query.Any(Function(x) x.ProductId IsNot Nothing) Then
                                                                 Dim ids = query.Where(Function(r) r.ProductId IsNot Nothing).Select(Function(s) s.ProductId).ToList()
                                                                 Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                                     Return (From x In context1.InventoryProduct.AsNoTracking()
                                                                             Where ids.Contains(x.Id)
                                                                             Select x).ToList()
                                                                 End Using
                                                             Else
                                                                 Return New List(Of InventoryProduct)

                                                             End If
                                                         End Function)

        Dim listGroupProduct = Task.Factory.StartNew(Function()
                                                         If query.Any(Function(x) x.ProductGroupId IsNot Nothing) Then
                                                             Dim ids = query.Where(Function(r) r.ProductGroupId IsNot Nothing).Select(Function(s) s.ProductGroupId).ToList()
                                                             Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                                 Return (From x In context1.ProductGroup.AsNoTracking()
                                                                         Where ids.Contains(x.Id)
                                                                         Select x).ToList()
                                                             End Using
                                                         Else
                                                             Return New List(Of ProductGroup)
                                                         End If
                                                     End Function)

        Dim listSubGroupProduct = Task.Factory.StartNew(Function() As List(Of ProductSubGroup)
                                                            If query.Any(Function(x) x.ProductSubGroupId IsNot Nothing) Then
                                                                Dim ids = query.Where(Function(r) r.ProductSubGroupId IsNot Nothing).Select(Function(s) s.ProductSubGroupId).ToList()
                                                                Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                                    Return (From x In context1.ProductSubGroup.AsNoTracking()
                                                                            Where ids.Contains(x.Id)
                                                                            Select x).ToList()
                                                                End Using
                                                            Else
                                                                Return New List(Of ProductSubGroup)
                                                            End If
                                                        End Function)

        Dim listDetailCondition = New List(Of BillingItemsRestrictionDetailCondition)

        For Each item In query
            listDetailCondition.AddRange(item.BillingItemsRestrictionDetailCondition)
        Next


        Dim listFunctionalUnit = Task.Factory.StartNew(Function() As List(Of FunctionalUnit)
                                                           Dim FunctionalUnit = New List(Of FunctionalUnit)
                                                           Dim ids As List(Of Integer?)

                                                           Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                               If listDetailCondition.Any(Function(x) x.FunctionalUnitId IsNot Nothing) Then

                                                                   ids = listDetailCondition.Where(Function(r) r.FunctionalUnitId IsNot Nothing).Select(Function(s) s.FunctionalUnitId).ToList()
                                                                   FunctionalUnit = (From x In context1.FunctionalUnit.AsNoTracking()
                                                                                     Where ids.Contains(x.Id)
                                                                                     Select x).ToList()
                                                               End If

                                                               If listDetailCondition.Any(Function(x) x.FunctionalUnitId2 IsNot Nothing) Then
                                                                   ids = listDetailCondition.Where(Function(r) r.FunctionalUnitId2 IsNot Nothing).Select(Function(s) s.FunctionalUnitId2).ToList()
                                                                   Dim queryFunct2 = (From x In context1.FunctionalUnit.AsNoTracking()
                                                                                      Where ids.Contains(x.Id)
                                                                                      Select x).ToList()
                                                                   FunctionalUnit.AddRange(queryFunct2)
                                                               End If
                                                           End Using

                                                           Return FunctionalUnit
                                                       End Function)

        Dim listSurgicalGroup = Task.Factory.StartNew(Function() As List(Of SurgicalGroup)
                                                          Dim surgicalGroup = New List(Of SurgicalGroup)
                                                          Dim ids As List(Of Integer?)

                                                          Using context1 As New GlobalModelUnitOfWork(currentContainer)

                                                              If listDetailCondition.Any(Function(x) x.SurgicalGroupId IsNot Nothing) Then
                                                                  ids = listDetailCondition.Where(Function(r) r.SurgicalGroupId IsNot Nothing).Select(Function(s) s.SurgicalGroupId).ToList()
                                                                  Dim queryGroupSurgical1 = (From x In context1.SurgicalGroup.AsNoTracking()
                                                                                             Where ids.Contains(x.Id) Select x).ToList()
                                                                  surgicalGroup.AddRange(queryGroupSurgical1)
                                                              End If

                                                              If listDetailCondition.Any(Function(x) x.SurgicalGroupId2 IsNot Nothing) Then
                                                                  ids = listDetailCondition.Where(Function(r) r.SurgicalGroupId2 IsNot Nothing).Select(Function(s) s.SurgicalGroupId2).ToList()
                                                                  Dim queryGroupSurgical2 = (From x In context1.SurgicalGroup.AsNoTracking()
                                                                                             Where ids.Contains(x.Id)
                                                                                             Select x).ToList()
                                                                  surgicalGroup.AddRange(queryGroupSurgical2)
                                                              End If

                                                          End Using

                                                          Return surgicalGroup
                                                      End Function)

        Dim listUVRRange = Task.Factory.StartNew(Function() As List(Of UVRRange)
                                                     Dim uVR = New List(Of UVRRange)
                                                     Dim ids As List(Of Integer?)

                                                     Using context1 As New GlobalModelUnitOfWork(currentContainer)

                                                         If listDetailCondition.Any(Function(x) x.UVRRangeId IsNot Nothing) Then
                                                             ids = listDetailCondition.Where(Function(r) r.UVRRangeId IsNot Nothing).Select(Function(s) s.UVRRangeId).ToList()
                                                             Dim queryUVRRange1 = (From x In context1.UVRRange.AsNoTracking()
                                                                                   Where ids.Contains(x.Id)
                                                                                   Select x).ToList()
                                                             uVR.AddRange(queryUVRRange1)
                                                         End If

                                                         If listDetailCondition.Any(Function(x) x.UVRRangeId2 IsNot Nothing) Then
                                                             ids = listDetailCondition.Where(Function(r) r.UVRRangeId2 IsNot Nothing).Select(Function(s) s.UVRRangeId2).ToList()
                                                             Dim queryUVRRange2 = (From x In context1.UVRRange.AsNoTracking()
                                                                                   Where ids.Contains(x.Id)
                                                                                   Select x).ToList()
                                                             uVR.AddRange(queryUVRRange2)
                                                         End If

                                                     End Using

                                                     Return uVR
                                                 End Function)

        Dim listStayType = Task.Factory.StartNew(Function() As List(Of CHTIPESTA)
                                                     Dim CHTIPESTA = New List(Of CHTIPESTA)
                                                     If listDetailCondition.Any(Function(x) Not String.IsNullOrEmpty(x.StayType)) Then
                                                         Dim StayCode = listDetailCondition.FindAll(Function(x) Not String.IsNullOrEmpty(x.StayType)).GroupBy(Function(g) g.StayType).
                                                                        Select(Function(s) $"'{s.Key}'").ToList()

                                                         CHTIPESTA.AddRange(Me.GetListCHTIPESTA(StayCode))

                                                     End If

                                                     If listDetailCondition.Any(Function(x) Not String.IsNullOrEmpty(x.StayType2)) Then
                                                         Dim StayCode = listDetailCondition.FindAll(Function(x) Not String.IsNullOrEmpty(x.StayType2)).GroupBy(Function(g) g.StayType2).
                                                         Select(Function(s) $"'{s.Key}'").ToList()

                                                         CHTIPESTA.AddRange(Me.GetListCHTIPESTA(StayCode))

                                                     End If
                                                     Return CHTIPESTA
                                                 End Function)
        Task.WaitAll(listCUPS,
                    listGroupCUPS,
                    listSubGroupCUPS,
                    listInventoryProduct,
                    listGroupProduct,
                    listSubGroupProduct,
                    listFunctionalUnit,
                    listSurgicalGroup,
                    listUVRRange)

        Parallel.ForEach(query, Sub(item)
                                    With item
                                        .RuleTypeName = $"{ResourceManager.GetString($"ItemsRestrictionRuleType{ .RuleType}", NAME_MODULE)}"
                                        .IncludeToCUPSDescription = (From x In listCUPS.Result
                                                                     Where x.Id = item.IncludeToCUPSEntityId Select $"{x.Code} - {x.Description}").FirstOrDefault()
                                        Select Case .RuleType
                                            Case Utils.EItemsRestrictionRuleType.CUPS
                                                .RuleDescription = (From x In listCUPS.Result
                                                                    Where x.Id = item.CUPSEntityId Select $"{x.Code} - {x.Description}").FirstOrDefault()
                                            Case Utils.EItemsRestrictionRuleType.GroupCUPS
                                                .RuleDescription = (From x In listGroupCUPS.Result
                                                                    Where x.Id = item.CUPSGroupId Select $"{x.Code} - {x.Name}").FirstOrDefault()
                                            Case Utils.EItemsRestrictionRuleType.SubGroupCUPS
                                                .RuleDescription = (From x In listSubGroupCUPS.Result
                                                                    Where x.Id = item.CUPSSubgroupId Select $"{x.Code} - {x.Name}").FirstOrDefault()
                                            Case Utils.EItemsRestrictionRuleType.Product
                                                .RuleDescription = (From x In listInventoryProduct.Result
                                                                    Where x.Id = item.ProductId Select $"{x.Code} - {x.Name}").FirstOrDefault()
                                            Case Utils.EItemsRestrictionRuleType.GroupProduct
                                                .RuleDescription = (From x In listGroupProduct.Result
                                                                    Where x.Id = item.ProductGroupId Select $"{x.Code} - {x.Name}").FirstOrDefault()
                                            Case Utils.EItemsRestrictionRuleType.SubGroupProduct
                                                .RuleDescription = (From x In listSubGroupProduct.Result
                                                                    Where x.Id = item.ProductSubGroupId Select $"{x.Code} - {x.Name}").FirstOrDefault()
                                            Case Else
                                                .RuleDescription = .RuleTypeName
                                        End Select

                                        .ConditionsName = ResourceManager.GetString($"ItemsRestrictionConditionType{ .ConditionType}", NAME_MODULE)

                                        If .ConditionType <> Utils.EItemsRestrictionConditionType.Nothing AndAlso .LogicalOperator <> Utils.ELogicalOperators.Nothing Then
                                            .ConditionsName = $"{ .ConditionsName} {listLogicalOperators.Find(Function(x) x.Item1 = .LogicalOperator).Item2} {ResourceManager.GetString($"ItemsRestrictionConditionType{ .ConditionType2}", NAME_MODULE)}"
                                        End If

                                        Parallel.ForEach(.BillingItemsRestrictionDetailCondition, Sub(detail)
                                                                                                      With detail
                                                                                                          Select Case item.ConditionType
                                                                                                              Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                                                                                                                  .FunctionalUnitCodeName = listFunctionalUnit.Result?.FindAll(Function(h) h.Id = .FunctionalUnitId)?.Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                  .ConditionName = .FunctionalUnitCodeName
                                                                                                              Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                                                                                                                  .ConditionName = UnitTypes.FirstOrDefault(Function(x) x.Item1 = .UnitTypeId).Item2
                                                                                                              Case Utils.EItemsRestrictionConditionType.QxGroup
                                                                                                                  .SurgicalGroupCodeName = listSurgicalGroup.Result?.FindAll(Function(x) x.Id = .SurgicalGroupId).Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                  .ConditionName = .SurgicalGroupCodeName
                                                                                                              Case Utils.EItemsRestrictionConditionType.RateManualType
                                                                                                                  .ConditionName = RateManualType.FirstOrDefault(Function(x) x.Item1 = .ManualType).Item2
                                                                                                              Case Utils.EItemsRestrictionConditionType.StayType
                                                                                                                  .StayTypeCodeName = listStayType.Result?.FindAll(Function(x) x.CODTIPEST = .StayType)?.Select(Function(t) $"{t.CODTIPEST} - {t.DESTIPEST}")?.FirstOrDefault
                                                                                                                  .ConditionName = .StayTypeCodeName
                                                                                                              Case Utils.EItemsRestrictionConditionType.UVRRange
                                                                                                                  .UVRRangeCodeName = listUVRRange.Result?.FindAll(Function(x) x.Id = .UVRRangeId)?.Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                  .ConditionName = .UVRRangeCodeName
                                                                                                          End Select

                                                                                                          .ConditionName &= $" ({EqualsOperator.Find(Function(x) x.Item1 = .Operator).Item2}) {listLogicalOperators.Find(Function(x) x.Item1 = item.LogicalOperator).Item2} "

                                                                                                          If item.ConditionType2 <> Utils.EItemsRestrictionConditionType.Nothing Then
                                                                                                              Select Case item.ConditionType2
                                                                                                                  Case Utils.EItemsRestrictionConditionType.FunctionalUnit
                                                                                                                      .FunctionalUnitCodeName = listFunctionalUnit.Result?.FindAll(Function(h) h.Id = .FunctionalUnitId2)?.Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                      .ConditionName &= .FunctionalUnitCodeName
                                                                                                                  Case Utils.EItemsRestrictionConditionType.FunctionalUnitType
                                                                                                                      .ConditionName &= UnitTypes.FirstOrDefault(Function(x) x.Item1 = .UnitTypeId2).Item2
                                                                                                                  Case Utils.EItemsRestrictionConditionType.QxGroup
                                                                                                                      .SurgicalGroupCodeName = listSurgicalGroup.Result?.FindAll(Function(x) x.Id = .SurgicalGroupId2).Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                      .ConditionName &= .SurgicalGroupCodeName
                                                                                                                  Case Utils.EItemsRestrictionConditionType.RateManualType
                                                                                                                      .ConditionName &= RateManualType.FirstOrDefault(Function(x) x.Item1 = .ManualType2).Item2
                                                                                                                  Case Utils.EItemsRestrictionConditionType.StayType
                                                                                                                      .StayTypeCodeName = listStayType.Result?.FindAll(Function(x) x.CODTIPEST = .StayType2)?.Select(Function(t) $"{t.CODTIPEST} - {t.DESTIPEST}")?.FirstOrDefault
                                                                                                                      .ConditionName &= .StayTypeCodeName
                                                                                                                  Case Utils.EItemsRestrictionConditionType.UVRRange
                                                                                                                      .UVRRangeCodeName = listUVRRange.Result?.FindAll(Function(x) x.Id = .UVRRangeId2)?.Select(Function(t) $"{t.Code} - {t.Name}")?.FirstOrDefault
                                                                                                                      .ConditionName &= .UVRRangeCodeName
                                                                                                              End Select
                                                                                                              .ConditionName &= $" ({EqualsOperator.Find(Function(x) x.Item1 = .Operator2).Item2})"
                                                                                                          End If
                                                                                                      End With
                                                                                                  End Sub)


                                    End With
                                End Sub)

        Return query.ToList()

    End Function

    ''' <summary>
    ''' consulta los tipos de estancia por lista de codigos
    ''' </summary>
    ''' <param name="listCode"></param>
    ''' <returns></returns>
    Private Function GetListCHTIPESTA(listCode As List(Of String)) As List(Of CHTIPESTA)

        Dim stringCodes = String.Join(",", listCode)

        Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {("@Codes", stringCodes)}

        Dim queryStayType = ExecuteQueryDR(Of CHTIPESTA)($"SELECT CODTIPEST,trim(DESTIPEST) as DESTIPEST,INDAUDFOR
                                                          from CHTIPESTA
                                                            where CODTIPEST in ({stringCodes}) ", parmas)
        If queryStayType Is Nothing Then
            Return New List(Of CHTIPESTA)
        End If

        Return queryStayType.ToList()
    End Function


End Class
