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
Public Class InteropCostServiceXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Lista las areas de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceAreaByListId(ByVal listId As List(Of Integer)) As XPCollection(Of GENARESERXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[OID] in (" & String.Join(",", listId) & ")")
        Dim collect As XPCollection(Of GENARESERXpo) = New XPCollection(Of GENARESERXpo)(New Session(XpoDefault.DataLayer), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lists the responsible by cost center identifier.
    ''' </summary>
    ''' <param name="CostCenterId">The cost center identifier.</param>
    ''' <returns></returns>
    Public Function ListResponsibleByCostCenterId(ByVal CostCenterId As Integer) As XPCollection(Of AFNRESPONXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCENCOS] = " & CostCenterId)
        Dim collect As XPCollection(Of AFNRESPONXpo) = New XPCollection(Of AFNRESPONXpo)(New Session(XpoDefault.DataLayer), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las cuentas contables del erp q se interfaza por el listado de oid
    ''' </summary>
    ''' <param name="listOIDAccount">The list oid account.</param>
    ''' <returns></returns>
    Public Function ListMainAccountByOIDAccountList(ByVal listOIDAccount As List(Of Integer)) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[OID] in (" & String.Join(",", listOIDAccount) & ")")
        classEntity = sessionNew.GetClassInfo(GetType(CTNCUENTAXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function
    Public Function ListServiceAreaByCostCenter(oidCostCenter As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCENCOS1] = " & oidCostCenter)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GENARESERXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista los activos fijos con el erp que se hace interfaz
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetInterfaceErp() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AFNACTIVOXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the main account erp.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountErp() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CTNCUENTAXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL;CUECODIGO;CUENOMBRE;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas contables con el erp con que se hace interfaz
    ''' </summary>
    Public Function ListMainAccountByNumberAccountList(ByVal listNumberAccount As List(Of String)) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CUECODIGO] in (" & String.Join(",", listNumberAccount.ToArray) & ")")
        classEntity = sessionNew.GetClassInfo(GetType(CTNCUENTAXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL;CUECODIGO;CUENOMBRE;CodeName", criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CTNCLASE=" & MAclass)
        classEntity = sessionNew.GetClassInfo(GetType(CTNCUENTAXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las cuentas contables con el erp con que se hace interfaz por listado de clases
    ''' </summary>
    ''' <param name="classes">The classes.</param>
    ''' <returns></returns>
    Public Function ListMainAccountErpByClasses(ByVal classes As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteriaIn As String = String.Join(",", classes.ToArray)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[CTNCLASE] in (" & criteriaIn & ")")
        classEntity = sessionNew.GetClassInfo(GetType(CTNCUENTAXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CTNNIVEL;CUECODIGO;CUENOMBRE;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los centros de costo con el erp con que se hace interfaz
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostCenterDinamic() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CCACTIVO=true")
        classEntity = sessionNew.GetClassInfo(GetType(CTNCENCOSXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "OID;CCCODIGO;CCNOMBRE;CodeName;CCACTIVO", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the general expenses by distribution.
    ''' </summary>
    Public Function ListGeneralExpensesWithDirectDistribution() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DistributionBaseXpo[DistributionType=1]")
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralExpenseXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the general expenses with direct distribution by year month.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralExpensesWithDirectDistributionByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DistributionBaseXpo[DistributionType=1] And Year = ? And Month = ?", year, month)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralExpenseXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = ? And Month = ?", year, month)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionSecondaryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionSecondary() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionSecondaryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de gastos directos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionDirectCost() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionDirectCostXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionFixedAsset() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionFixedAssetXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionFixedAssetXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Year] = ? AND [Month] = ?", year, month)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionIntermediateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista la distribución por mano de obra
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionManpower() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DistributionManpowerXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the service area.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralExpenses() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralExpenseXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the service area.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceArea() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GENARESERXpo))
        serverMode = New XPInstantFeedbackSource(classEntity) ', "OID;GASCODIGO;GASNOMBRE;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the organizational structure data.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructureData() As XPCollection
        Dim collect As XPCollection = New XPCollection(GetType(OrganizationalStructureOfCostsXpo))
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeOrganizationalStructureWithOut(ByVal code As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code <> '" & code & "'")
        classEntity = sessionNew.GetClassInfo(GetType(OrganizationalStructureOfCostsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListOrganizationalStructure() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(OrganizationalStructureOfCostsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ParentId;ParentId.Id", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the production center by status.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function ListProductionCenterByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ProductionCenterXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;CostCenterId;CostCenterCode;Status;OrganizationalStructureOfCostId.Id", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ProductionCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;CostCenterId;CostCenterCode;Status;OrganizationalStructureOfCostId.Id;OrganizationalStructureOfCostId.Name", Nothing)
        Return serverMode
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
    '            Dim sessionNew = New Session(XpoDefault.DataLayer)
    '            Dim _supplier As XPQuery(Of Supplier) = New XPQuery(Of Supplier)(sessionNew)
    '            Dim _supplierDistributionLines As XPQuery(Of SuppliersDistributionLinesXpo) = New XPQuery(Of SuppliersDistributionLinesXpo)(sessionNew)
    '            Dim _distributionLines As XPQuery(Of DistributionLinesXpo) = New XPQuery(Of DistributionLinesXpo)(sessionNew)
    '            Dim _accountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)

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

End Class