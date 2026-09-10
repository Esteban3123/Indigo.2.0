'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Transactions
Imports Domain.Payroll
Imports System.Text

Public Class CostDistributionManpowerAdminService
    Implements ICostDistributionManpowerAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribucion de mano de obra
    ''' </summary>
    Private _distributionManpowerRepository As ICostDistributionManpowerRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository
    ''' <summary>
    ''' Repositorio de grupos
    ''' </summary>
    Private _groupRepository As IGroupRepository
    ''' <summary>
    ''' Servicios de costos
    ''' </summary>
    Private _costServices As ICostServices
    ''' <summary>
    ''' Repositorio de CompanySettings
    ''' </summary>
    Private _companySettingsRepository As ICompanySettingsRepository
    ''' <summary>
    ''' Repositorio de PayrollSettings
    ''' </summary>
    Private _payrollSettingsRepository As IPayrollSettingsRepository
    ''' <summary>
    ''' Repositorio de CxP
    ''' </summary>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' Repositorio de Currency
    ''' </summary>
    Private _currencyRepository As ICurrencyRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal distributionManpowerRepository As ICostDistributionManpowerRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository,
                   groupRepository As IGroupRepository, costServices As ICostServices, companySettingsRepository As ICompanySettingsRepository, payrollSettingsRepository As IPayrollSettingsRepository,
                   accountPayableRepository As IAccountPayableRepository, currencyReposiroty As ICurrencyRepository)
        If distributionManpowerRepository Is Nothing Then
            Throw New ArgumentNullException("distributionManpowerRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionManpowerRepository = distributionManpowerRepository
        _sequenceDRepository = sequenceDRepository
        _groupRepository = groupRepository
        _costServices = costServices
        _companySettingsRepository = companySettingsRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _accountPayableRepository = accountPayableRepository
        _currencyRepository = currencyReposiroty
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Function GetDistributionManpowerById(id As Integer) As CostDistributionManpower Implements ICostDistributionManpowerAdminService.GetDistributionManpowerById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionManpowerRepository.GetDistributionManpowerById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Function GetDistributionManpower(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionManpower) Implements ICostDistributionManpowerAdminService.GetDistributionManpower
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionManpower As CostDistributionManpower = Me._distributionManpowerRepository.GetDistributionManpower(code.Trim())
            If distributionManpower IsNot Nothing AndAlso distributionManpower.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionManpower)(distributionManpower, audit, status)
                auditProcess.Execute()
            End If
            If distributionManpower.EntityCurrencyId Is Nothing Then
                Dim res = RevalueDistributionManpower(distributionManpower)
                If res.StateResult Then
                    Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = res.ObjectEmbbeded}
                Else
                    Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = res.Message}
                End If
            Else
                Dim TRM = GetTRMDistributionManpower(distributionManpower)
                If TRM.StatusCode = eStatusResult.SUCCESS Then
                    If TRM.StateResult Then ' En caso de tener TRM se asigna el valor
                        For Each detail As CostDistributionManpowerDetail In distributionManpower.CostDistributionManpowerDetail
                            detail.TRMValue = CStr(TRM.ObjectEmbbeded)
                        Next
                    End If
                End If
            End If
            Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por periodo y de ser necesario un tercero
    ''' </summary>
    Public Function GetDistributionManpowerByYearMonth(Year As Integer, Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of List(Of CostDistributionManpower)) Implements ICostDistributionManpowerAdminService.GetDistributionManpowerByYearMonth
        Try
            Dim listDistributionManpower As New List(Of CostDistributionManpower)

            Dim listDistributions As List(Of SP_GetDistributionManpower_Result) = Me._distributionManpowerRepository.SP_GetDistributionManpower(Year, Month, ManpowerType, EntityId)
            For Each distribution In listDistributions
                Dim costDistributionManpower = listDistributionManpower.FirstOrDefault(Function(d) d.ManpowerType = distribution.ManpowerType AndAlso
                                                                                           ((d.GroupId Is Nothing AndAlso distribution.GroupId Is Nothing) OrElse (d.GroupId = distribution.GroupId)) AndAlso
                                                                                           ((d.EmployeeId Is Nothing AndAlso distribution.EmployeeId Is Nothing) OrElse (d.EmployeeId = distribution.EmployeeId)) AndAlso
                                                                                           d.ThirdPartyId = distribution.ThirdPartyId AndAlso
                                                                                           ((d.PositionId Is Nothing AndAlso distribution.PositionId Is Nothing) OrElse (d.PositionId = distribution.PositionId)))
                If costDistributionManpower Is Nothing Then
                    costDistributionManpower = New CostDistributionManpower() With {
                        .Year = Year,
                        .Month = Month,
                        .ManpowerType = distribution.ManpowerType,
                        .EntityId = distribution.EntityId,
                        .GroupId = distribution.GroupId,
                        .GroupCodeName = distribution.GroupCodeName,
                        .EmployeeId = distribution.EmployeeId,
                        .ThirdPartyId = distribution.ThirdPartyId,
                        .ThirdPartyNitName = distribution.ThirdPartyNitName,
                        .PositionId = distribution.PositionId,
                        .PositionCodeName = distribution.PositionCodeName,
                        .Description = distribution.ThirdPartyNitName,
                        .Status = 1
                    }
                    listDistributionManpower.Add(costDistributionManpower)
                End If

                costDistributionManpower.HoursWorked += distribution.HoursQuantity
                costDistributionManpower.TotalAccrued += distribution.TotalAccrued
                costDistributionManpower.TotalProvision += distribution.TotalProvision
                costDistributionManpower.TotalEmployerContribution += distribution.TotalEmployerContribution
                costDistributionManpower.TotalParafiscal += distribution.TotalParafiscal

                costDistributionManpower.CostDistributionManpowerDetail.Add(New CostDistributionManpowerDetail With {
                        .ProductionCenterId = If(distribution.ProductionCenterId Is Nothing, 0, distribution.ProductionCenterId),
                        .ProductionCenterCodeName = distribution.ProductionCenterCodeName,
                        .HoursQuantity = distribution.HoursQuantity,
                        .TotalAccrued = distribution.TotalAccrued,
                        .TotalProvision = distribution.TotalProvision,
                        .TotalEmployerContribution = distribution.TotalEmployerContribution,
                        .TotalParafiscal = distribution.TotalParafiscal
                    }
                )
            Next
            ' Evaluamos cada uno de los objetos de la lista para ver si es necesario revaluarlos
            For i As Integer = 0 To listDistributionManpower.Count - 1
                If listDistributionManpower(i).EntityCurrencyId Is Nothing Then
                    Dim res = RevalueDistributionManpower(listDistributionManpower(i))
                    If res.StateResult Then
                        listDistributionManpower(i) = res.ObjectEmbbeded
                    Else
                        Return New ActionResult(Of List(Of CostDistributionManpower)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = res.Message}
                    End If
                Else
                    Dim TRM = GetTRMDistributionManpower(listDistributionManpower(i))
                    If TRM.StatusCode = eStatusResult.SUCCESS Then
                        If TRM.StateResult Then ' En caso de tener TRM se asigna el valor
                            For Each detail As CostDistributionManpowerDetail In listDistributionManpower(i).CostDistributionManpowerDetail
                                detail.TRMValue = CStr(TRM.ObjectEmbbeded)
                            Next
                        End If
                    End If
                End If
            Next

            Return New ActionResult(Of List(Of CostDistributionManpower)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDistributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CostDistributionManpower)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por registro origen
    ''' </summary>
    Public Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of CostDistributionManpower) Implements ICostDistributionManpowerAdminService.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId
        Try
			Dim distributionManpower As CostDistributionManpower = Me._distributionManpowerRepository.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(Year, Month, ManpowerType, EntityId)
			If distributionManpower.Id > 0 Then
				If distributionManpower.EntityCurrencyId Is Nothing Then
					Dim res = RevalueDistributionManpower(distributionManpower)
					If res.StateResult Then
						Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = res.ObjectEmbbeded}
					Else
						Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = res.Message}
					End If
				Else
					Dim TRM = GetTRMDistributionManpower(distributionManpower)
					If TRM.StatusCode = eStatusResult.SUCCESS Then
						If TRM.StateResult Then ' En caso de tener TRM se asigna el valor
							For Each detail As CostDistributionManpowerDetail In distributionManpower.CostDistributionManpowerDetail
								detail.TRMValue = CStr(TRM.ObjectEmbbeded)
							Next
						End If
					End If
				End If
			End If
			Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la moneda origen de la distribución de mano de obra
    ''' </summary>
    ''' <param name="manpowerType"></param>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function GetOrigenCurrencyDistributionManpower(ByVal manpowerType As Integer, ByVal entityId As Integer) As Integer
        Dim originCurrency As Integer

        ' Verificamos el tipo de Distribución para saber de dónde consultar la información
        If manpowerType = 1 Then ' Si es 1, obtenemos la información de nómina
            Dim payrollSettings = _payrollSettingsRepository.GetSettingPayroll()
            If payrollSettings IsNot Nothing Then
                originCurrency = payrollSettings.CurrencyId
            End If
        Else 'Solo hay dos tipos de distribución de mano de obra. En caso de ser 2, obtenemos la moneda de CxP
            Dim accountPayable = _accountPayableRepository.GetAccountPayableById(entityId)
            If accountPayable IsNot Nothing Then
                originCurrency = accountPayable.CurrencyId
            End If
        End If
        Return originCurrency
    End Function

    ''' <summary>
    ''' Obtiene la fecha para la converción de la TRM acorde al documento origen
    ''' Último dia del mes vigente en Costos cuando viene de Nómina
    ''' Fecha del documento cuando viene por CxP
    ''' </summary>
    ''' <param name="costDistributionManpower"></param>
    ''' <returns></returns>
    Public Function GetDateTRMDistributionManpower(ByVal costDistributionManpower As CostDistributionManpower) As Date
        Dim dateTRM As Date

        ' Verificamos el tipo de Distribución para saber de dónde consultar la información
        If costDistributionManpower.ManpowerType = 1 Then ' Si es 1, obtenemos la información de nómina
            dateTRM = New Date(costDistributionManpower.Year, costDistributionManpower.Month, Date.DaysInMonth(costDistributionManpower.Year, costDistributionManpower.Month))
        Else 'Solo hay dos tipos de distribución de mano de obra. En caso de ser 2, obtenemos la moneda de CxP
            Dim accountPayable = _accountPayableRepository.GetAccountPayableById(costDistributionManpower.EntityId)
            If accountPayable IsNot Nothing Then
                dateTRM = accountPayable.DocumentDate
            End If
        End If
        Return dateTRM
    End Function

    ''' <summary>
    ''' Obtiene la TRM de la distribución de mano de obra
    ''' </summary>
    ''' <param name="costDistributionManpower"></param>
    Public Function GetTRMDistributionManpower(ByVal costDistributionManpower As CostDistributionManpower) As ActionResult(Of Decimal)
        Dim officialCurrency As Integer
        Dim originCurrency As Integer
        Dim dateTRM As Date
        Try
            ' Obtenemos la moneda oficial
            Dim companySettings = _companySettingsRepository.GetCompanySettings()
            If companySettings IsNot Nothing Then
                officialCurrency = companySettings.OfficialCurrencyId
            End If

            ' Asignamos el Id de la moneda del documento origen
            If costDistributionManpower.EntityCurrencyId Is Nothing Then
                originCurrency = GetOrigenCurrencyDistributionManpower(costDistributionManpower.ManpowerType, costDistributionManpower.EntityId)
            Else
                originCurrency = costDistributionManpower.EntityCurrencyId
            End If

            'Obtenemos la fecha para la TRM de la distribución de mano de obra
            dateTRM = GetDateTRMDistributionManpower(costDistributionManpower)

            ' Validamos si las monedas no coinciden para aplicar la revalorización
            If originCurrency <> officialCurrency Then
                Dim TRM = _currencyRepository.GetJustTRMToMakeDivision(originCurrency, officialCurrency, Nothing, dateTRM)
                Return New ActionResult(Of Decimal) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Math.Round(TRM.TRMValue, 4)}
            Else
                Return New ActionResult(Of Decimal) With {.StateResult = False, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Nothing}
            End If
        Catch ex As Exception
            Return New ActionResult(Of Decimal) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Realiza la revalorización de los detalles a la moneda del módulo
    ''' </summary>
    ''' <returns></returns>
    Public Function RevalueDistributionManpower(ByVal costDistributionManpower As CostDistributionManpower) As ActionResult(Of CostDistributionManpower)
        Try
            ' Obtenemos la moneda origen de la Distribución de mano de obra
            Dim originCurrency = GetOrigenCurrencyDistributionManpower(costDistributionManpower.ManpowerType, costDistributionManpower.EntityId)
            ' Validamos si las monedas no coinciden para aplicar la revalorización
            Dim res = GetTRMDistributionManpower(costDistributionManpower)
            If res.StatusCode = eStatusResult.SUCCESS Then
                costDistributionManpower.EntityCurrencyId = originCurrency ' Asignamos la moneda origen a la cabecera
                If res.StateResult Then
                    Dim TRM = res.ObjectEmbbeded
                    If TRM = 0 Then
                        Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = "No existe TRM de la moneda origen a la oficial"}
                    End If
                    costDistributionManpower.CostDistributionManpowerDetail.ToList().ForEach(Sub(x)
                                                                                                 x.TRMValue = CStr(TRM)
                                                                                                 x.EntityCurrencyId = originCurrency
                                                                                                 x.TotalAccruedRevalued = x.TotalAccrued / TRM
                                                                                                 x.TotalProvisionRevalued = x.TotalProvision / TRM
                                                                                                 x.TotalEmployerContributionRevalued = x.TotalEmployerContribution / TRM
                                                                                                 x.TotalParafiscalRevalued = x.TotalParafiscal / TRM
                                                                                             End Sub)
                Else
                    costDistributionManpower.CostDistributionManpowerDetail.ToList().ForEach(Sub(x)
                                                                                                 x.TotalAccruedRevalued = x.TotalAccrued
                                                                                                 x.TotalProvisionRevalued = x.TotalProvision
                                                                                                 x.TotalEmployerContributionRevalued = x.TotalEmployerContribution
                                                                                                 x.TotalParafiscalRevalued = x.TotalParafiscal
                                                                                             End Sub)
                End If
                Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = costDistributionManpower}
            Else
                Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = res.Message}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionManpowerAdminService.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionManpowerRepository.ListPeriodWithDataByMaximumPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionManpower) Implements ICostDistributionManpowerAdminService.ListDistributionManpowerByYearMonth
        Try
            Dim listCostDistributionManpower = Me._distributionManpowerRepository.ListDistributionManpowerByYearMonth(year, month)

            If listCostDistributionManpower IsNot Nothing AndAlso listCostDistributionManpower.Count > 0 Then
                'Dictionaries
                Dim dictionaryGroups As New Dictionary(Of Integer, Domain.Payroll.Entities.Group)
                'Individual Items
                Dim group As Domain.Payroll.Entities.Group = Nothing

                For Each item As CostDistributionManpower In listCostDistributionManpower
                    If item.GroupId IsNot Nothing Then
                        If Not dictionaryGroups.ContainsKey(item.GroupId) Then
                            group = _groupRepository.GetGroupSimpleById(item.GroupId)
                            dictionaryGroups.Add(item.GroupId, group)
                        Else
                            group = dictionaryGroups(item.GroupId)
                        End If
                        item.GroupCodeName = String.Format("{0} - {1}", group.Code, group.Name)
                    End If
                Next
            End If

            Return listCostDistributionManpower
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result)) Implements ICostDistributionManpowerAdminService.SP_ExportExcelCostDistributionManPower
        Try
            Dim resultStore = _distributionManpowerRepository.SP_ExportExcelCostDistributionManPower(Year, Month)
            Return New ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = resultStore}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements ICostDistributionManpowerAdminService.ImportCostDistributionManpower
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim ImportXml As String = ConvertToXmlImportIds(ImportIds)
                Dim resultStore = _distributionManpowerRepository.SP_ImportCostDistributionManpower(Year, Month, OperatingUnitId, ImportXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    Public Function SaveDistributionManpower(distributionManpower As CostDistributionManpower, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionManpower) Implements ICostDistributionManpowerAdminService.SaveDistributionManpower
        If distributionManpower Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If

        Dim unitOfWork As IUnitWork = Me._distributionManpowerRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlCostDistributionManpower(distributionManpower)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(distributionManpower.Id, distributionManpower.Status)

                Dim resultStore = Me._distributionManpowerRepository.SP_SaveCostDistributionManpower(EntityXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CostDistributionManpower) With {.StatusCode = eStatusResult.WARNING, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                distributionManpower.Id = resultStore.Id
                distributionManpower.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of CostDistributionManpower)(distributionManpower, audit, auditStatus, distributionManpower.OriginalValue)
                auditProcess.Execute()

                distributionManpower.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CostDistributionManpower) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionManpower, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostDistributionManpower) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda masivamente los datos del form
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmMasive(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult Implements ICostDistributionManpowerAdminService.ConfirmMasive
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim resultStore = _distributionManpowerRepository.SP_ConfirmMasiveCostDistributionManpower(Year, Month, OperatingUnitId, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Function DeleteDistributionManpower(distributionManpower As CostDistributionManpower, audit As AuditMessage) As ActionResult Implements ICostDistributionManpowerAdminService.DeleteDistributionManpower
        If distributionManpower Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionManpowerRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionManpower.CostDistributionManpowerDetail.Count > 0
                    distributionManpower.CostDistributionManpowerDetail.Item(distributionManpower.CostDistributionManpowerDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionManpower.MarkAsDeleted()
                distributionManpower.ModificationUser = audit.CodeUser
                distributionManpower.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionManpower)(distributionManpower, audit, status)
                Me._distributionManpowerRepository.SaveEntity(distributionManpower)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlImportIds(ImportIds As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        For Each detail In ImportIds
            builder.Append("<Data>")
            builder.Append("<Id>" & detail & "</Id>")
            builder.Append("</Data>")
        Next

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlCostDistributionManpower(costDistributionManpower As CostDistributionManpower) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CostDistributionManpower>")

        builder.Append("<Id>" & costDistributionManpower.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & costDistributionManpower.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & costDistributionManpower.Code & "</Code>")
        builder.Append("<Year>" & costDistributionManpower.Year & "</Year>")
        builder.Append("<Month>" & costDistributionManpower.Month & "</Month>")
        builder.Append("<ManpowerType>" & costDistributionManpower.ManpowerType & "</ManpowerType>")
        builder.Append("<EntityId>" & costDistributionManpower.EntityId & "</EntityId>")
        If costDistributionManpower.EmployeeId IsNot Nothing Then
            builder.Append("<EmployeeId>" & costDistributionManpower.EmployeeId & "</EmployeeId>")
        End If
        builder.Append("<ThirdPartyId>" & costDistributionManpower.ThirdPartyId & "</ThirdPartyId>")
        If costDistributionManpower.PositionId IsNot Nothing Then
            builder.Append("<PositionId>" & costDistributionManpower.PositionId & "</PositionId>")
        End If
        If costDistributionManpower.GroupId IsNot Nothing Then
            builder.Append("<GroupId>" & costDistributionManpower.GroupId & "</GroupId>")
        End If
        builder.Append("<Description>" & costDistributionManpower.Description & "</Description>")
        builder.Append("<HoursWorked>" & costDistributionManpower.HoursWorked & "</HoursWorked>")
        builder.Append("<TotalAccrued>" & costDistributionManpower.TotalAccrued.ToString().Replace(",", ".") & "</TotalAccrued>")
        builder.Append("<TotalProvision>" & costDistributionManpower.TotalProvision.ToString().Replace(",", ".") & "</TotalProvision>")
        builder.Append("<TotalEmployerContribution>" & costDistributionManpower.TotalEmployerContribution.ToString().Replace(",", ".") & "</TotalEmployerContribution>")
        builder.Append("<TotalParafiscal>" & costDistributionManpower.TotalParafiscal.ToString().Replace(",", ".") & "</TotalParafiscal>")
        builder.Append("<Status>" & costDistributionManpower.Status & "</Status>")

        For Each detail In costDistributionManpower.CostDistributionManpowerDetail
            builder.Append("<CostDistributionManpowerDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<DistributionManpowerId>" & detail.DistributionManpowerId & "</DistributionManpowerId>")
            builder.Append("<ProductionCenterId>" & detail.ProductionCenterId & "</ProductionCenterId>")
            builder.Append("<HoursQuantity>" & detail.HoursQuantity & "</HoursQuantity>")
            builder.Append("<TotalAccrued>" & detail.TotalAccrued.ToString().Replace(",", ".") & "</TotalAccrued>")
            builder.Append("<TotalProvision>" & detail.TotalProvision.ToString().Replace(",", ".") & "</TotalProvision>")
            builder.Append("<TotalEmployerContribution>" & detail.TotalEmployerContribution.ToString().Replace(",", ".") & "</TotalEmployerContribution>")
            builder.Append("<TotalParafiscal>" & detail.TotalParafiscal.ToString().Replace(",", ".") & "</TotalParafiscal>")

            builder.Append("</CostDistributionManpowerDetail>")
        Next

        builder.Append("</CostDistributionManpower>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _costServices.Dispose()
            End If
            _distributionManpowerRepository = Nothing
            _sequenceDRepository = Nothing
            _groupRepository = Nothing
            _costServices = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
