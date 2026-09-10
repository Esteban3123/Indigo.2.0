'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.InteropCostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering

#End Region

Public Class CostServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function ListCostDirectDistributionSecondary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDirectDistributionSecondaryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDirectDistributionSecondaryXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
    End Function

    Public Function GetInventoryMeasurementUnitById(id As Integer) As InventoryInventoryMeasurementUnitReportXpo
        Dim session As New IndigoXPOSession(Of InventoryInventoryMeasurementUnitReportXpo)()
        Return session.GetObjectByKey(Of InventoryInventoryMeasurementUnitReportXpo)(id)
    End Function

    Public Function GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewMeasurementUnitByCostSecondaryDistributionXpo)()
        Dim criteria = CriteriaOperator.Parse("DistributionSecondaryId=?", id)
            Dim classEntity = session.GetClassInfo(GetType(ViewMeasurementUnitByCostSecondaryDistributionXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    Public Function GetDistributionSecondaryProductionCenterBySecundaryId(id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewCostProductionCenterByCostSecondaryDistributionXpo)()
        Dim criteria = CriteriaOperator.Parse("DistributionSecondaryId=?", id)
            Dim classEntity = session.GetClassInfo(GetType(ViewCostProductionCenterByCostSecondaryDistributionXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    Public Function GetCostEstimationXpo(ParamArray parameters As Object()) As CostEstimationNativeXpo
        Dim session As New IndigoXPOSession(Of CostDistributionSecondaryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProductionCenterId=? and Year=? and Month=?", parameters(0), parameters(1), parameters(2))
        Return session.FindObject(Of CostEstimationNativeXpo)(criteria)
    End Function

    ''' <summary>
    ''' Obtiene la distribucion de elemento del costo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetDistributionDirectCostById(ByVal id As Integer) As CostDistributionDirectCostXpo
        Dim session As New IndigoXPOSession(Of CostDistributionDirectCostXpo)()
        Return session.GetObjectByKey(Of CostDistributionDirectCostXpo)(id)
    End Function

    ''' <summary>
    ''' Consulta el listado de documentos de provision
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListViewProvisionDocument(SupplierId As Integer, GeneralExpenseId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListProvisionDocumentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SupplierId =" & SupplierId & " AND GeneralExpenseId = " & GeneralExpenseId)
        Dim classEntity = session.GetClassInfo(GetType(ViewListProvisionDocumentXpo))
        Return New XPCollection(session, classEntity, criteria)
    End Function

    ''' <summary>
    ''' Lista los registros de distribucion secundaria por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostDistributionSecondaryByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionSecondaryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionSecondaryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;ProductionCenterId.CodeName;Description;CodeName;Status", criteria)
    End Function

    Public Function ListCostLogisticsProductionCenterRecord() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostLogisticsProductionCenterRecordXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostLogisticsProductionCenterRecordXpo))
        Return New XPInstantFeedbackSource(classEntity)
    End Function

    Public Function GetProductionCenterById(ByVal id As Integer) As CostProductionCenterXpo
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Return session.GetObjectByKey(Of CostProductionCenterXpo)(id)
    End Function

    Public Function GetMeasurementUnitById(ByVal id As Integer) As InventoryInventoryMeasurementUnitReportXpo
        Dim session As New IndigoXPOSession(Of InventoryInventoryMeasurementUnitReportXpo)()
        Return session.GetObjectByKey(Of InventoryInventoryMeasurementUnitReportXpo)(id)
    End Function

    Public Function ListProductionCenterByStatusAndCenterTypes(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType in (" & String.Join(",", CenterTypes) & ")")
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;CenterType", criteria)
    End Function

    Public Function CostListMeasureUnitByType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostViewSecondaryMeasureUnitByProductionCenter)()
        Dim classEntity = session.GetClassInfo(GetType(CostViewSecondaryMeasureUnitByProductionCenter))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", Nothing)
    End Function

    Public Function ListProductionCenterByIds(ListProductionCenterIds As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id in (" & String.Join(",", ListProductionCenterIds.ToArray) & ")")
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    Public Function ListGeneralExpensesByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & Status)
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ExpenditureType;Status;CostGeneralExpenseCategoryId.CodeName", criteria)
    End Function

    Public Function ListProductionCenterCostCenterByProductionCenterId(id As Integer) As XPCollection(Of CostProductionCenterCostCenterXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProductionCenterId.Id=?", id)
        Dim session As New IndigoXPOSession(Of CostProductionCenterCostCenterXpo)()
        Return New XPCollection(Of CostProductionCenterCostCenterXpo)(session, criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostGeneralExpenseCategoryByStatusTreeList(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseCategoryXpo))
        Return New XPCollection(session, classEntity, criteria)
        'End Using
    End Function

    Function ListCostDistributionIntermediateByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionIntermediateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionIntermediateXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    Function ListCostDistributionSecondary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionSecondaryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionSecondaryXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lists the general expenses by distribution.
    ''' </summary>
    Public Function ListGeneralExpensesWithDirectDistribution() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostDistributionBaseXpo[DistributionType=1]")
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lists the general expenses with direct distribution by year month.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralExpensesWithDirectDistributionByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostDistributionBaseXpo[DistributionType=1] And Year = ? And Month = ?", year, month)
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionSecondaryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = ? And Month = ?", year, month)
            Dim classEntity = session.GetClassInfo(GetType(CostDistributionSecondaryXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    Function ListMainAccountsByStatusAndBookId(status As Boolean, bookId As Integer, AllowsMovement As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " AND LegalBookId = " & bookId & " AND AllowsMovement = " & AllowsMovement)
        Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement;NumberName", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionSecondaryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionSecondaryXpo))
        Return New XPInstantFeedbackSource(classEntity)
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de gastos directos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostDistributionDirectCost() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionDirectCostXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionDirectCostXpo))
        Return New XPInstantFeedbackSource(classEntity)
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionFixedAsset() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionFixedAssetXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionFixedAssetXpo))
        Return New XPInstantFeedbackSource(classEntity)
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionFixedAssetXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
            Dim classEntity = session.GetClassInfo(GetType(CostDistributionFixedAssetXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionIntermediateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
            Dim classEntity = session.GetClassInfo(GetType(CostDistributionIntermediateXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista la distribución por mano de obra
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionManpower() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostDistributionManpowerXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostDistributionManpowerXpo))
        Return New XPInstantFeedbackSource(classEntity)
    End Function

    ''' <summary>
    ''' Lista todos los empleados y contratistas por año y mes
    ''' </summary>
    Public Function ViewCostDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewCostDistributionManpower)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", Year, Month)
        Dim classEntity = session.GetClassInfo(GetType(ViewCostDistributionManpower))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los empleados y contratistas por año y mes
    ''' </summary>
    Public Function GetCollectionViewCostDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer) As XPCollection(Of ViewCostDistributionManpower)
        Dim session As New IndigoXPOSession(Of ViewCostDistributionManpower)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", Year, Month)
        Return New XPCollection(Of ViewCostDistributionManpower)(session, criteria)
    End Function

    Function ViewCostDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewCostDistributionFixedAsset)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        Dim classEntity = session.GetClassInfo(GetType(ViewCostDistributionFixedAsset))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los activos depreciados por año y mes
    ''' </summary>
    Public Function GetCollectionViewCostDistributionFixedAssetByYearMonth(ByVal Year As Integer, ByVal Month As Integer) As XPCollection(Of ViewCostDistributionFixedAsset)
        Dim session As New IndigoXPOSession(Of ViewCostDistributionFixedAsset)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", Year, Month)
        Return New XPCollection(Of ViewCostDistributionFixedAsset)(session, criteria)
    End Function

    ''' <summary>
    ''' Lists the service area.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostGeneralExpenses() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;StatusName;Status", Nothing)
    End Function

    Function ListCostOrganizationalStructureData() As DevExpress.Xpo.XPCollection
        Dim session As New IndigoXPOSession(Of CostOrganizationalStructureOfCostsXpo)()
        Return New XPCollection(session, GetType(CostOrganizationalStructureOfCostsXpo))
        'End Using
    End Function

    ''' <summary>
    ''' Lists the organizational structure data.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructureDataCost() As XPCollection
        Dim session As New IndigoXPOSession(Of CostOrganizationalStructureOfCostsXpo)()
        Dim collect As XPCollection = New XPCollection(session, GetType(CostOrganizationalStructureOfCostsXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Lista la estructura Organizacional teniendo en cuenta el Centro de Producción.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostOrganizationalStructureOfCostsWithPCenterReport() As XPCollection(Of CostViewOrganizationalStructureOfCostsWithPCenterReportXpo)
        Dim session As New IndigoXPOSession(Of CostViewOrganizationalStructureOfCostsWithPCenterReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostViewOrganizationalStructureOfCostsWithPCenterReportXpo))
        Return New XPCollection(Of CostViewOrganizationalStructureOfCostsWithPCenterReportXpo)(session)
        'End Using
    End Function

    Function ListCategoryData() As DevExpress.Xpo.XPCollection
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Return New XPCollection(session, GetType(CostGeneralExpenseCategoryXpo))
        'End Using
    End Function

    Public Function ListCostGeneralExpenseCategoryByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
            Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;ParentId", criteria)
    End Function

    Function ListCostGeneralExpenseCategoryDatasourceTreeList() As DevExpress.Xpo.XPCollection
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        Dim collect As XPCollection = New XPCollection(session, GetType(CostGeneralExpenseCategoryXpo))
        Return collect
    End Function

    Public Function ListCostGeneralExpenseCategory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeOrganizationalStructureWithOut(ByVal code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostOrganizationalStructureOfCostsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code <> '" & code & "'")
            Dim classEntity = session.GetClassInfo(GetType(CostOrganizationalStructureOfCostsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", criteria)
    End Function

    Public Function InitializeCostGeneralExpenseCategoryWithOut(ByVal code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostGeneralExpenseCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code <> '" & code & "'")
            Dim classEntity = session.GetClassInfo(GetType(CostGeneralExpenseCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", criteria)
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostOrganizationalStructure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostOrganizationalStructureOfCostsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostOrganizationalStructureOfCostsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", Nothing)
    End Function

    Public Function ListMainAccountByNumberAccountList(ByVal listNumberAccount As List(Of String), ByVal legalbookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Number] in ('" & String.Join("','", listNumberAccount.ToArray) & "') and LegalBookId = " & legalbookId.ToString())
            Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    Function ListMainAccountsByStatusAndBookIdAndClass(status As Boolean, LegalBookId As Integer, allowmovement As Boolean, clases As List(Of Integer)) As Object
        Dim likeQuery As String = ""
        For i As Integer = 0 To clases.Count - 1 Step 1
            If i <> 0 Then
                likeQuery += " OR "
            End If
            likeQuery += " [Number] LIKE '" & clases(i) & "%'"
        Next
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " AND LegalBookId = " & LegalBookId & " AND AllowsMovement = " & allowmovement & " AND (" & likeQuery & ")")
            Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement;NumberName", criteria)
    End Function

    ''' <summary>
    ''' Lists the MeasurementUnitCCLogistic.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInventoryMeasurementUnitCCLogistic() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostViewCostInventoryMeasurementUnitCCLogistic)()
        Dim classEntity = session.GetClassInfo(GetType(CostViewCostInventoryMeasurementUnitCCLogistic))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListCostProductionCenterCategoryByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterCategoryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterCategoryXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListProductionCenterByStatusAndCenterType(ByVal status As Boolean, CenterType As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType = " & CenterType)
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;CenterType", criteria)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' List Center Production InteropCost XPCollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <param name="CenterTypes"></param>
    ''' <returns></returns>
    Function ListReportCostCenterProductionXpCollection(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPCollection(Of CostProductionCenterXpo)
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType in (" & String.Join(",", CenterTypes) & ")")
        Dim collect As XPCollection(Of CostProductionCenterXpo) = New XPCollection(Of CostProductionCenterXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListCostProductionCenterByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;CostCenterId;CostCenterCode;Status;OrganizationalStructureOfCostId.Id", criteria)
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListCostProductionCenterByStatu(ByVal status As Boolean) As XPCollection(Of CostProductionCenterXpo)
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim collect As XPCollection(Of CostProductionCenterXpo) = New XPCollection(Of CostProductionCenterXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListCostProductionCenterByStatusAndCenterType(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType in (" & String.Join(",", CenterTypes) & ")")
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;CostCenterId;CostCenterCode;Status;OrganizationalStructureOfCostId.Id;CenterType", criteria)
    End Function

    Public Function ListProductionCenterByStatusAndCenterTypeXpCollection(ByVal status As Boolean, CenterType As Byte) As XPCollection(Of CostProductionCenterXpo)
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType = " & CenterType)
        Dim collect As XPCollection(Of CostProductionCenterXpo) = New XPCollection(Of CostProductionCenterXpo)(Session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostProductionCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;CostCenterId;CostCenterCode;Status;OrganizationalStructureOfCostId.Id;OrganizationalStructureOfCostId.Name", Nothing)
    End Function

    Public Function ListCollectionProductionCenters(ByVal filtro As String) As XPCollection(Of CostProductionCenterXpo)
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of CostProductionCenterXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista las actividades 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostActivities() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostActivityXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostActivityXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;CUPSEntityId.Id;CUPSEntityId.CodeDescription", Nothing)
    End Function

    ''' <summary>
    ''' Lista las actividades activas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListActiveCostActivities() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=True")
        Dim session As New IndigoXPOSession(Of CostActivityXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostActivityXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista los grupos de productos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostInventoryGroups() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostInventoryGroupXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostInventoryGroupXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName;InventoryMeasurementUnitId.Id;InventoryMeasurementUnitId.CodeName", Nothing)
    End Function

    Function ListMainAccountConsumpsion(LegalBookId As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MovementClass=" & 1 & " And Status=True")
        Dim adjustmentList As XPCollection(Of AdjustmentConceptXpo) = New XPCollection(Of AdjustmentConceptXpo)(New Session(XpoDefault.DataLayer), criteria)
        Dim cuentasId As String = ""
        If adjustmentList IsNot Nothing AndAlso adjustmentList.Count > 0 Then
            cuentasId = String.Join(",", adjustmentList.ToList().Select(Function(x) x.AdjustmentAccountId).Distinct().ToList())
        End If
        If String.IsNullOrEmpty(cuentasId) Then
            Return Nothing
        End If
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria2 As CriteriaOperator = CriteriaOperator.Parse("[Id] in (" & cuentasId & ") And AllowsMovement = True And LegalBookId = " & LegalBookId)
            Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria2)
    End Function

    Function ListMainAccountDeprecation(listFunctionalUnit As List(Of Integer), LegalBookId As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostCenterId in (" & String.Join(",", listFunctionalUnit) & ")")
        Dim functionalUnits As XPCollection(Of FunctionalUnitXpo) = New XPCollection(Of FunctionalUnitXpo)(New Session(XpoDefault.DataLayer), criteria)
        Dim cuentas As String = ""
        If functionalUnits IsNot Nothing AndAlso functionalUnits.Count > 0 Then
            Dim criteria2 As CriteriaOperator = CriteriaOperator.Parse("AccountingStructureId in (" & String.Join(",", functionalUnits.ToList().Select(Function(x) x.AccountingStructureId).Distinct().ToArray) & ")")
            Dim adjustmentList As XPCollection(Of FixedAssetEquipmentCatalogDetailXpo) = New XPCollection(Of FixedAssetEquipmentCatalogDetailXpo)(New Session(XpoDefault.DataLayer), criteria2)
            If adjustmentList IsNot Nothing AndAlso adjustmentList.Count > 0 Then
                cuentas = String.Join(",", adjustmentList.ToList.Select(Function(x) x.LoanSpendAccountId).Distinct().ToList)
            End If
        End If
        If String.IsNullOrEmpty(cuentas) Then
            Return Nothing
        End If
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria3 As CriteriaOperator = CriteriaOperator.Parse("Id in (" & cuentas & ") And AllowsMovement = True And LegalBookId = " & LegalBookId)
            Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria3)
    End Function

    ''' <summary>
    ''' Lists the production center.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenterReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;OrganizationalStructureOfCostId.Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    Public Function ListCostProductionCenterCategory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostProductionCenterCategoryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CostProductionCenterCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    Public Function ListCollectionCategories(ByVal filtro As String) As XPCollection(Of CostProductionCenterCategoryXpo)
        Dim session As New IndigoXPOSession(Of CostProductionCenterCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of CostProductionCenterCategoryXpo)(session, criteria)
    End Function

    Public Function GetMaxLevelOrganizationalStructure() As Integer
        Dim session As New IndigoXPOSession(Of CostOrganizationalStructureOfCostsXpo)()
        Return session.Evaluate(Of CostOrganizationalStructureOfCostsXpo)(CriteriaOperator.Parse("MAX(Level)"), Nothing)
    End Function

    ''' <summary>
    ''' Metodo para obtener todos las los datos de la fuente de datos y se filtra los criterios
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceMainAccountWithOutParameterization(Month As Integer, Year As Integer) As XPCollection(Of CostViewMainAccountsWithoutParameterization)
        Dim filter As String = String.Format("Month = {0} AND Year = {1}", Month, Year)
        Dim session As New IndigoXPOSession(Of CostViewMainAccountsWithoutParameterization)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Return New XPCollection(Of CostViewMainAccountsWithoutParameterization)(session, criteria)
    End Function

    ''' <summary>
    ''' Obtiene un listado de registros de la entidad
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Function GetListStandarCost(Optional filter As String = Nothing)
        Dim criteria As CriteriaOperator = Nothing
        If filter IsNot Nothing Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        Dim session As New IndigoXPOSession(Of AverageStandardCostXpo)()
        Return New XPCollection(Of AverageStandardCostXpo)(session, criteria)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
