'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.InteropCostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Data.PLinq
Imports System.Threading

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class InteropCostServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function ListGeneralExpenseCategoryByStatusTreeList(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of GeneralExpenseCategoryXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseCategoryXpo))
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
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

    Public Function ListGeneralExpenseCategory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralExpenseCategoryXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseCategoryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id;Status;StatusName", Nothing)
        Return serverMode
    End Function

    Public Function ListGeneralExpenseCategoryByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralExpenseCategoryXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseCategoryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;ParentId", criteria)
        Return serverMode
    End Function

    Function ListGeneralExpenseCategoryData() As DevExpress.Xpo.XPCollection
        Dim session As New IndigoXPOSession(Of GeneralExpenseCategoryXpo)()

        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim collect As XPCollection = New XPCollection(session, GetType(GeneralExpenseCategoryXpo))
        Return collect
        'End Using
    End Function

    Public Function GetInventoryMeasurementUnitById(id As Integer) As InventoryMeasurementUnitXpo
        Dim session As New IndigoXPOSession(Of InventoryMeasurementUnitXpo)()

        Return session.GetObjectByKey(Of InventoryMeasurementUnitXpo)(id)
    End Function

    Public Function GetDistributionDirectCostById(ByVal id As Integer) As DistributionDirectCostXpo
        Dim session As New IndigoXPOSession(Of DistributionDirectCostXpo)()

        Return session.GetObjectByKey(Of DistributionDirectCostXpo)(id)
    End Function

    Public Function GetLogisticsProductionCenterRecordById(ByVal id As Integer) As InteropCostLogisticsProductionCenterRecordReportXpo
        Dim session As New IndigoXPOSession(Of InteropCostLogisticsProductionCenterRecordReportXpo)()

        Return session.GetObjectByKey(Of InteropCostLogisticsProductionCenterRecordReportXpo)(id)
    End Function

    Public Function GetProductionCenterById(ByVal id As Integer) As ProductionCenterXpo
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Return session.GetObjectByKey(Of ProductionCenterXpo)(id)
    End Function

    Public Function GetMeasurementUnitById(ByVal id As Integer) As InventoryMeasurementUnitXpo
        Dim session As New IndigoXPOSession(Of InventoryMeasurementUnitXpo)()

        Return session.GetObjectByKey(Of InventoryMeasurementUnitXpo)(id)
    End Function

    Public Function GetAllDirectDistributionSecondary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DirectDistributionSecondaryXpo)()

        Dim classEntity = session.GetClassInfo(GetType(DirectDistributionSecondaryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewMeasurementUnitBySecondaryDistribution)()

        Dim criteria = CriteriaOperator.Parse("DistributionSecondaryId=?", id)
        Dim classEntity = session.GetClassInfo(GetType(ViewMeasurementUnitBySecondaryDistribution))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function GetCostEstimationXpo(ParamArray parameters As Object()) As CostEstimationXpo
        Dim session As New IndigoXPOSession(Of DistributionSecondaryXpo)()
        Dim DistributionSecondary = session.GetObjectByKey(Of DistributionSecondaryXpo)(parameters(0))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProductionCenterId=? and Year=? and Month=?", DistributionSecondary.ProductionCenterId.Id, parameters(1), parameters(2))
        Return session.FindObject(Of CostEstimationXpo)(criteria)
    End Function

    Public Function ListProductionCenterCostCenterByProductionCenterId(id As Integer) As XPCollection(Of ProductionCenterCostCenterXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProductionCenterId=?", id)
        Dim session As New IndigoXPOSession(Of ProductionCenterCostCenterXpo)()

        Dim collect As XPCollection(Of ProductionCenterCostCenterXpo) = New XPCollection(Of ProductionCenterCostCenterXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListCostCenterDinamicByListId(listId As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCENCOSXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CCACTIVO=true and [OID] in (" & String.Join(",", listId.ToArray) & ")")
        Dim classEntity = session.GetClassInfo(GetType(CTNCENCOSXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CCCODIGO;CCNOMBRE;CodeName;CCACTIVO", criteria)
        Return serverMode
    End Function

    Public Function GetCTNCUENTAById(id As Integer) As CTNCUENTAXpo
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Return session.GetObjectByKey(Of CTNCUENTAXpo)(id)
    End Function

    Function ListDocumentTypes(status As Boolean) As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([AFNDEPRECI].[ACAFECCHCI]) = ? And GetMonth([AFNDEPRECI].[ACAFECCHCI]) = ? And [AFNDEPRECI].[ACAESTADO] = 1", Year, Month)
        Dim session As New IndigoXPOSession(Of CTNTIPCOMXpo)()

        Dim classEntity = session.GetClassInfo(GetType(CTNTIPCOMXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListSecondaryMeasureUnitByProductionCenter(productionCenterId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewSecondaryMeasureUnitByProductionCenter)()

        Dim criteria = CriteriaOperator.Parse("Status = 1 and ProductionCenterId = " & productionCenterId)
        Dim classEntity = session.GetClassInfo(GetType(ViewSecondaryMeasureUnitByProductionCenter))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Abbreviation;UnitType;AllowEditCostValue;CostValue;Status;ProductionCenterId", criteria)
        Return serverMode
    End Function

    Public Function ListDepreciationByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([AFNDEPRECI].[ACAFECCHCI]) = ? And GetMonth([AFNDEPRECI].[ACAFECCHCI]) = ? And [AFNDEPRECI].[ACAESTADO] = 1 and ACADEPMEN > 0", year, month)
        Dim session As New IndigoXPOSession(Of AFNCALDEPXpo)()

        Dim classEntity = session.GetClassInfo(GetType(AFNCALDEPXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListServiceAreaByCostCenter(oidCostCenter As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCENCOS1] in (" & String.Join(",", oidCostCenter.ToArray) & ")")
        Dim session As New IndigoXPOSession(Of GENARESERXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GENARESERXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListProductionCenterByIds(ListProductionCenterIds As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id in (" & String.Join(",", ListProductionCenterIds.ToArray) & ")")
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las areas de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceAreaByListId(ByVal listId As List(Of Integer)) As XPCollection(Of GENARESERXpo)
        Dim session As New IndigoXPOSession(Of GENARESERXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[OID] in (" & String.Join(",", listId.ToArray) & ")")
        Dim collect As XPCollection(Of GENARESERXpo) = New XPCollection(Of GENARESERXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    Function GetCollectionDepreciacionByYearMont(year As Integer, month As Integer) As XPCollection(Of AFNCALDEPXpo)
        Dim session As New IndigoXPOSession(Of AFNCALDEPXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([AFNDEPRECI].[ACAFECCHCI]) = ? And GetMonth([AFNDEPRECI].[ACAFECCHCI]) = ? And [AFNDEPRECI].[ACAESTADO] = 1 And ACADEPMEN > 0", year, month)
        Dim collect As XPCollection(Of AFNCALDEPXpo) = New XPCollection(Of AFNCALDEPXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    Function GetRegistroDepreciacionByYearMont(year As Integer, month As Integer) As XPCollection(Of AFNDEPRECIXpo)
        Dim session As New IndigoXPOSession(Of AFNDEPRECIXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([ACAFECCHCI]) = ? And GetMonth([ACAFECCHCI]) = ? And [ACAESTADO] = 1", year, month)
        Dim collect As XPCollection(Of AFNDEPRECIXpo) = New XPCollection(Of AFNDEPRECIXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' List Center Production InteropCost XPCollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <param name="CenterTypes"></param>
    ''' <returns></returns>
    Function ListReportCenterProductionXpCollection(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPCollection(Of InteropCostProductionCenterReportXpo)
        Dim session As New IndigoXPOSession(Of InteropCostProductionCenterReportXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType in (" & String.Join(",", CenterTypes) & ")")
        Dim collect As New XPCollection(Of InteropCostProductionCenterReportXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' List Center Production InteropCost XPCollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Function ListReportCenterProductionStatusXpCollection(ByVal status As Boolean) As XPCollection(Of InteropCostProductionCenterReportXpo)
        Dim session As New IndigoXPOSession(Of InteropCostProductionCenterReportXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim collect As XPCollection(Of InteropCostProductionCenterReportXpo) = New XPCollection(Of InteropCostProductionCenterReportXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lists the responsible by cost center identifier.
    ''' </summary>
    ''' <param name="CostCenterId">The cost center identifier.</param>
    ''' <returns></returns>
    Public Function ListResponsibleByCostCenterId(ByVal CostCenterId As List(Of Integer)) As XPCollection(Of AFNRESPONXpo)
        Dim session As New IndigoXPOSession(Of AFNRESPONXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCENCOS] in (" & String.Join(",", CostCenterId.ToArray) & ")")
        Dim collect As XPCollection(Of AFNRESPONXpo) = New XPCollection(Of AFNRESPONXpo)(session, criteria)
        Return collect
        'End Using
    End Function


    ''' <summary>
    ''' Lista los activos fijos con el erp que se hace interfaz
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetInterfaceErp() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AFNACTIVOXpo)()

        Dim classEntity = session.GetClassInfo(GetType(AFNACTIVOXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the main account erp.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountErpByNivel(nivel As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CTNNIVEL.CTNVCODIGO in (" & String.Join(",", nivel.ToArray) & ")")
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas contables con el erp con que se hace interfaz
    ''' </summary>
    Public Function ListMainAccountByNumberAccountList(ByVal listNumberAccount As List(Of String)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CUECODIGO] in ('" & String.Join("','", listNumberAccount.ToArray) & "')")
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CUECODIGO] in (" & operatorIn & ")")
        'Dim collect As XPCollection(Of CTNCUENTAXpo) = New XPCollection(Of CTNCUENTAXpo)(New Session(XpoDefault.DataLayer), criteria)
        ''collect.Filter = New InOperator("CUECODIGO", listNumberAccount.ToArray())
        'Return collect
    End Function

    ''' <summary>
    ''' Lista las cuentas contables con el erp con que se hace interfaz por clase
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountErpByClass(ByVal MAclass As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CTNCLASE.CLACODIGO=" & MAclass)
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function
    Function ListMainAccountErpByClassAndNivel(clases As List(Of Integer), niveles As List(Of Integer)) As Object
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCLASE].[CLATIPO] = 2 AND [CTNNIVEL].[CTNVAUXILI] = 1")
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    Function ListMainAccountConsumo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("INNCONAJUs1[ICAACTIVO = True]")
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the main account deprecation.
    ''' </summary>
    Public Function ListMainAccountDeprecation(ByVal costCenterId As List(Of Integer)) As XPInstantFeedbackSource
        Dim _responsible As XPCollection(Of Infrastructure.Data.Xpo.InteropCostRepository.AFNRESPONXpo) = ListResponsibleByCostCenterId(costCenterId)
        Dim _listMainAccountsId As List(Of Integer) = (From r In _responsible Select r).Select(Function(x) x.CTNCUENTA).Distinct().ToList()
        Return ListMainAccountByOIDAccountList(_listMainAccountsId)
    End Function

    ''' <summary>
    ''' Lista las cuentas contables con el erp con que se hace interfaz por listado de clases
    ''' </summary>
    ''' <param name="classes">The classes.</param>
    ''' <returns></returns>
    Public Function ListMainAccountErpByClasses(ByVal classes As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCLASE].[CLACODIGO] in ('" & String.Join("','", classes.ToArray) & "')")
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las cuentas contables del erp q se interfaza por el listado de oid
    ''' </summary>
    ''' <param name="listOIDAccount">The list oid account.</param>
    ''' <returns></returns>
    Public Function ListMainAccountByOIDAccountList(ByVal listOIDAccount As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCUENTAXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[OID] in (" & String.Join(",", listOIDAccount) & ")")
        Dim classEntity = session.GetClassInfo(GetType(CTNCUENTAXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL.CTNVCODIGO;CTNNIVEL.CTNVNOMBRE;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los centros de costo con el erp con que se hace interfaz
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostCenterDinamic() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CTNCENCOSXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CCACTIVO=true")
        Dim classEntity = session.GetClassInfo(GetType(CTNCENCOSXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "OID;CCCODIGO;CCNOMBRE;CodeName;CCACTIVO", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the general expenses by distribution.
    ''' </summary>
    Public Function ListGeneralExpensesWithDirectDistribution() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DistributionBaseXpo[DistributionType=1]")
        Dim session As New IndigoXPOSession(Of GeneralExpenseXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListGeneralExpensesStatusActive() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1")
        Dim session As New IndigoXPOSession(Of GeneralExpenseXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListGeneralExpensesByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & Status)
        Dim session As New IndigoXPOSession(Of GeneralExpenseXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ExpenditureType;Status;GeneralExpenseCategoryId.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the general expenses with direct distribution by year month.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralExpensesWithDirectDistributionByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DistributionBaseXpo[DistributionType=1] And Year = ? And Month = ?", year, month)
        Dim session As New IndigoXPOSession(Of GeneralExpenseXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionSecondaryXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = ? And Month = ?", year, month)
        Dim classEntity = session.GetClassInfo(GetType(DistributionSecondaryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionSecondaryXpo)

        Dim classEntity = session.GetClassInfo(GetType(DistributionSecondaryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los registros de distribucion secundaria por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionSecondaryXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DistributionSecondaryBaseXpo[DistributionType=1] and Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(DistributionSecondaryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de gastos directos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionDirectCost() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionDirectCostXpo)

        Dim classEntity = session.GetClassInfo(GetType(DistributionDirectCostXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionFixedAsset() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionFixedAssetXpo)

        Dim classEntity = session.GetClassInfo(GetType(DistributionFixedAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionFixedAssetXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        Dim classEntity = session.GetClassInfo(GetType(DistributionFixedAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionIntermediateXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        Dim classEntity = session.GetClassInfo(GetType(DistributionIntermediateXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista la distribución por mano de obra
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionManpower() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DistributionManpowerXpo)()

        Dim classEntity = session.GetClassInfo(GetType(DistributionManpowerXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the service area.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralExpenses() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralExpenseXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    Public Function ListLogisticsProductionCenterRecord() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LogisticsProductionCenterRecordXpo)()

        Dim classEntity = session.GetClassInfo(GetType(LogisticsProductionCenterRecordXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the service area.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceArea() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GENARESERXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GENARESERXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity) ', "OID;GASCODIGO;GASNOMBRE;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the organizational structure data.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructureData() As XPCollection
        Dim session As New IndigoXPOSession(Of OrganizationalStructureOfCostsXpo)()
        Dim collect As XPCollection = New XPCollection(session, GetType(OrganizationalStructureOfCostsXpo))
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeOrganizationalStructureWithOut(ByVal code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of OrganizationalStructureOfCostsXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code <> '" & code & "'")
        Dim classEntity = session.GetClassInfo(GetType(OrganizationalStructureOfCostsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of OrganizationalStructureOfCostsXpo)()

        Dim classEntity = session.GetClassInfo(GetType(OrganizationalStructureOfCostsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListProductionCenterByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id", criteria)
        Return serverMode
    End Function

    Public Function GetDistributionSecondaryProductionCenterBySecundaryId(id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewProductionCenterBySecondaryDistribution)()

        Dim criteria = CriteriaOperator.Parse("DistributionSecondaryId=?", id)
        Dim classEntity = session.GetClassInfo(GetType(ViewProductionCenterBySecondaryDistribution))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListProductionCenterByStatusAndCenterType(ByVal status As Boolean, CenterType As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType = " & CenterType)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;CenterType", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    Public Function ListProductionCenterByStatusAndCenterTypeXpCollection(ByVal status As Boolean, CenterType As Byte) As XPCollection(Of ProductionCenterXpo)
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType = " & CenterType)
        Dim collect As XPCollection(Of ProductionCenterXpo) = New XPCollection(Of ProductionCenterXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lists the production center Logistic by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListProductionCenterLogisticByStatusAndCenterType(ByVal status As Boolean, CenterType As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType = " & CenterType)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the MeasurementUnit.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInventoryMeasurementUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InventoryMeasurementUnitXpo)()

        Dim classEntity = session.GetClassInfo(GetType(InventoryMeasurementUnitXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the MeasurementUnitCCLogistic.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInventoryMeasurementUnitCCLogistic() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InteropCostViewInventoryMeasurementUnitCCLogistic)()

        Dim classEntity = session.GetClassInfo(GetType(InteropCostViewInventoryMeasurementUnitCCLogistic))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function


    Public Function ListProductionCenterByStatusAndCenterTypes(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType in (" & String.Join(",", CenterTypes) & ")")
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;CenterType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;OrganizationalStructureOfCostId.Id;OrganizationalStructureOfCostId.Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los centros de produccion por estado y tipo de centro para los reportes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenterByStatusCenterTypeReport(ByVal status As Boolean, ParamArray CenterTypes() As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And CenterType=" & CenterTypes(0))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CenterType;CodeName;Status;OrganizationalStructureOfCostId.Id;OrganizationalStructureOfCostId.Name", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    Public Function GetAllGeneralExpenseCategory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralExpenseCategoryXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralExpenseCategoryXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1")
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista la estructura Organizacional teniendo en cuenta el Centro de Producción.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructureOfCostsWithPCenterReport() As XPCollection(Of InteropCostViewOrganizationalStructureOfCostsWithPCenterReportXpo)
        Dim session As New IndigoXPOSession(Of InteropCostViewOrganizationalStructureOfCostsWithPCenterReportXpo)()

        Dim classEntity = session.GetClassInfo(GetType(InteropCostViewOrganizationalStructureOfCostsWithPCenterReportXpo))
        Dim serverMode = New XPCollection(Of InteropCostViewOrganizationalStructureOfCostsWithPCenterReportXpo)(session)
        Return serverMode
        'End Using
    End Function
#End Region

#Region "LinqFeedBackSource"

    '#Region "SchedulePayment"
    '    Private WithEvents vlinqSchedulePayment As New LinqInstantFeedbackSource
    '    ''' <summary>
    '    ''' Obtiene todos los empleados
    '    ''' </summary>
    '    Public Function GetSchedulePayment() As LinqInstantFeedbackSource
    '        vlinqSchedulePayment.KeyExpression = "Name"
    '        Return vlinqSchedulePayment
    '    End Function

    '    Private Sub OnGetQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqSchedulePayment.GetQueryable
    '        Try
    '            Dim session = New Session(XpoDefault.DataLayer)
    '            Dim _supplier As XPQuery(Of Supplier) = New XPQuery(Of Supplier)(session)
    '            Dim _supplierDistributionLines As XPQuery(Of SuppliersDistributionLinesXpo) = New XPQuery(Of SuppliersDistributionLinesXpo)(session)
    '            Dim _distributionLines As XPQuery(Of DistributionLinesXpo) = New XPQuery(Of DistributionLinesXpo)(session)
    '            Dim _accountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)

    '            Dim zero As Integer = 0
    '            Dim Dos As Integer = 2

    '            Dim TmpQueryableSource = From S In _supplier
    '                                     Join SDL In _supplierDistributionLines On S.Id Equals SDL.IdSupplier
    '                                     Join DL In _distributionLines On SDL.IdDistributionLine Equals DL.Id
    '                                     Join AP In _accountPayable On AP.IdSupplier Equals S.Id
    '                                     Where AP.Balance > zero And AP.Status = Dos
    '                                     Select Supplier = S.Name, DistributionLine = DL.Description, Invoice = AP.BillNumber, ExpirationDate = AP.ExpirationDate, Balance = AP.Balance
    '            'Select New With {Key .Nombre = S.Name, Key .Linea = DL.Description, Key .Factura = AP.BillNumber, Key .Fecha = AP.ExpirationDate, Key .Edad = CalculateAgePayments(AP.ExpirationDate), Key .Saldo = AP.Balance}
    '            'Dim prueba = TmpQueryableSource.ToList
    '            e.QueryableSource = TmpQueryableSource
    '            e.Tag = _accountPayable
    '        Catch ex As Exception
    '        End Try
    '    End Sub

    '    Private Sub DismissQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqSchedulePayment.DismissQueryable
    '        Try
    '            'Dispose of the DataContext 
    '            CType(e.Tag, Object).Dispose()
    '        Catch ex As Exception
    '            ex.Message.ToString()
    '        End Try
    '    End Sub

    '    Public Function CalculateAgePayments(ByVal ExpirationDate As Date, session As Session) As String
    '        Try
    '            Dim _agesPayments As XPQuery(Of AgesPaymentsXpo) = New XPQuery(Of AgesPaymentsXpo)(session)
    '            Dim dias = Date.Now.Day - ExpirationDate.Day + 1
    '            Dim edad = From AP In _agesPayments
    '                                     Where AP.InitialRange <= dias And AP.EndRange >= dias
    '                                     Select AP.Name
    '            Return edad.FirstOrDefault()
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '#End Region

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