'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.BudgetRepository
' Author           : Jhossept Kevin Garay
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
'Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports Infrastructure.Data.Xpo.BudgetRepository.GENESIS01

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class BudgetServicesXpo
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
    ''' Incializa una nueva instancia de la clase <see cref="BudgetServicesXpo" />.
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
        'cargo la uri de servicios
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
    ''' lista los ingresos de presupuesto de tipo ingreso
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetIncome(filtro As String) As XPCollection(Of VReportBudgetIncomeXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportBudgetIncomeXpo) = New XPCollection(Of VReportBudgetIncomeXpo)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("CategoryCode", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' lista los ingresos de presupuesto de tipo gasto
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetExpense(filtro As String) As XPCollection(Of VReportBudgetExpenseXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportBudgetExpenseXpo) = New XPCollection(Of VReportBudgetExpenseXpo)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("CategoryCode", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' Lista las obligaciones confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationByStatusConfirmedAndValidityId(ValidityId As Integer, Status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & " And BudgetaryValidityId=" & ValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetObligationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;ObligationTypeName;ThirdPartyId.NitName;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de ingresos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetDocumentsNotConfirmed(documentType As EBudgetDocumentType, Optional validityId As Integer = 0) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim collect As XPCollection
        Select Case documentType
            Case EBudgetDocumentType.BudgetModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1 and  BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetModificationXpo), criteria)
            Case EBudgetDocumentType.BudgetTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1 and BudgetaryValidityId =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetTransferXpo), criteria)
            Case EBudgetDocumentType.AnnualizedCashFlowModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1 and BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAnnualizedCashFlowModificationXpo), criteria)
            Case EBudgetDocumentType.AnnualizedCashFlowTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1 and BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 1")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAnnualizedCashFlowTransferXpo), criteria)
            Case EBudgetDocumentType.Recognition
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetRecognitionXpo), criteria)
            Case EBudgetDocumentType.RecognitionModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetRecognitionModificationXpo), criteria)
            Case EBudgetDocumentType.Collection
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetCollectionXpo), criteria)
            Case EBudgetDocumentType.CollectionModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetCollectionModificationXpo), criteria)
        End Select
        Return collect
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de gastos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetDocumentsNotConfirmedExpenses(documentType As EBudgetDocumentTypeExpense, Optional validityId As Integer = 0) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim collect As XPCollection
        Select Case documentType
            Case EBudgetDocumentTypeExpense.BudgetModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2 and  BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.BudgetTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2 and BudgetaryValidityId =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetTransferXpo), criteria)
            Case EBudgetDocumentTypeExpense.AnnualizedCashFlowModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2 and BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAnnualizedCashFlowModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.AnnualizedCashFlowTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2 and BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 and DocumentSource = 2")
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAnnualizedCashFlowTransferXpo), criteria)
            Case EBudgetDocumentTypeExpense.Availability
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAvailabilityXpo), criteria)
            Case EBudgetDocumentTypeExpense.AvailabilityModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetAvailabilityModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.Commitment
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetCommitmentXpo), criteria)
            Case EBudgetDocumentTypeExpense.CommitmentModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetCommitmentModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.Obligation
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetObligationXpo), criteria)
            Case EBudgetDocumentTypeExpense.ObligationModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetObligationModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.PaymentOrder
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 and BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(sessionNew, GetType(BudgetPaymentOrderXpo), criteria)
        End Select
        Return collect
    End Function


    ''' <summary>
    ''' Lista las modificaciones del pac
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPACModification(Validity As Integer, type As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity & "And DocumentSource = " & type)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetAnnualizedCashFlowModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los recaudos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollection(Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCollectionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las obligaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligation(Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetObligationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los recaudos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionByStatus(Validity As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & Validity & "And Status=" & CByte(status))
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCollectionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista los recaudos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionModification(Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCollectionModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;CollectionId;CollectionId.Code", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDependency(validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetBudgetDependencyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;BudgetaryValidityId;Code;Name;ResponsibleId;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFinancialSource(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetFinancialSourceReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEarningsType(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity & " AND Type=1")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRevenueTypeReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseType(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity & " AND Type=2")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRevenueTypeReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetDependency(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetDependencyReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los traslados de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetTransferByType(type As Integer, budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentSource=" & type & " And BudgetaryValidityId=" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetTransferXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;DocumentSource;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los traslados de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function ListObligationModificationByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetObligationModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los reconocimientos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRecognition(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRecognitionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las disponibilidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAvailability(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetAvailabilityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ExpirationDays;AvailabilityType;AvailabilityTypeName;Observations;Status;StatusName;BudgetaryValidityId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetConcept(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetConceptReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets all Budget Entities
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllBudgetEntities(filtro As String) As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of BudgetBudgetaryValidityReportXpo) = New XPCollection(Of BudgetBudgetaryValidityReportXpo)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("Year", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllBudgetRevenueType(filtro As String) As XPCollection(Of BudgetRevenueTypeReportXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of BudgetRevenueTypeReportXpo) = New XPCollection(Of BudgetRevenueTypeReportXpo)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("Code", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFinancialSource(ByVal ValidityId As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetFinancialSourceXpo))
        Dim criteria As CriteriaOperator
        If Not (String.IsNullOrEmpty(ValidityId)) Then
            criteria = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & ValidityId & "")
            serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode;Classification", criteria)
        Else
            serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode;Classification", Nothing)
        End If
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBudgetEntitiesXPSCS() As XPServerCollectionSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(Budget_BudgetaryEntity))
        'serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", Nothing)
        Dim serverModeCollection As XPServerCollectionSource = New XPServerCollectionSource(sessionNew, classEntity, Nothing)
        serverModeCollection.DisplayableProperties = "NameCode;Budget_BudgetaryValidity[Id = Budget_BudgetaryEntity.Id].Year"
        Return serverModeCollection
    End Function

#Region "Evaluation Invoice LinqInstantFeedBackSource"

    Private WithEvents vlinqInitialBudget As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todas las entidades presupuestales
    ''' </summary>
    Public Function ListBudgetEntity() As LinqInstantFeedbackSource
        vlinqInitialBudget.KeyExpression = "Id"
        Return vlinqInitialBudget
    End Function
    Private Sub OnGetQueryableInitialBudget(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInitialBudget.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEntity As XPQuery(Of Budget_BudgetaryEntity) = New XPQuery(Of Budget_BudgetaryEntity)(sessionNew)
            Dim tableValidity As XPQuery(Of Budget_BudgetaryValidity) = New XPQuery(Of Budget_BudgetaryValidity)(sessionNew)
            Dim TmpQueryableSource = From objValidity In tableValidity _
                                     Join objEntity In tableEntity On objEntity.Id Equals objValidity.BudgetaryEntityId.Id _
                                     Where objValidity.Status = 2
                                     Select Id = objValidity.Id, Code = objEntity.Code & " - " & objEntity.Name, Year = objValidity.Year, DateIncomeRecord = objValidity.DateIncomeRecord, DateExpensesRecord = objValidity.DateExpensesRecord, Status = objValidity.Status, ResolutionNumber = objValidity.ResolutionNumber, ResolutionValue = objValidity.ResolutionValue, ValidityId = objValidity.Id, InitialStateIncome = objValidity.InitialStateIncome, InitialStateExpenses = objValidity.InitialStateExpenses
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableValidity
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub DismissQueryableInitialBudget(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInitialBudget.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region


    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBudgetEntitiesXPIFS() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetBudgetInstitutionsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFinancialSourceByStatus(status As Boolean, validityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And BudgetaryValidityId = " & validityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetFinancialSourceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetBudgetInstitutionsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las cuentas por pagar que tenga asociado la unidad de radicacion
    ''' </summary>
    ''' <param name="BudgetaryEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListValidityByBudgetEntityId(ByVal BudgetaryEntityId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryEntityId.Id=" & BudgetaryEntityId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetValidityXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los detalles de la obligacion
    ''' </summary>
    ''' <param name="ObligationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationDetailByObligationId(ByVal ObligationId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ObligationId.Id=" & ObligationId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetObligationDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetEarningsType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=1")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Source", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetExpenseType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=2")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Classification;Definition", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de la tabla RevenueType dependiendo de
    ''' la vigencia que escojan y el tipo(Ingreso o Gasto)
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRevenueTypeByBudgetValidityIdAndType(budgetValidityId As Integer, itemType As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Type =" & itemType)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetRevenueTypeXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <param name="withBalance">Permite saber si se consulta con saldo mayor a cero o no</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByBudgetValidityId(validityId As Integer, status As Integer, type As Integer, withBalance As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If withBalance Then
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type & " And Balance>0")
        Else
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type)
        End If
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetXpo), criteria)
        Return collect
    End Function
    ''' <summary>
    ''' lista los presupuestos por tipo de rubro
    ''' </summary>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByCategoryItemType(ItemType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("CategoryId.ItemType =" & ItemType)
        classEntity = sessionNew.GetClassInfo(GetType(VIEDev.Budget_Budget))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Balance;CategoryId.FinancialSourceId.NameCode;CategoryId.NameCode;CategoryId.BudgetaryValidityId.BudgetaryEntityId.NameCode;RevenueTypeId.NameCode;CategoryId.BudgetaryValidityId.Year", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el detalle de disponibilidad
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityDetailByValidityIdAndStatus(validityId As Integer, status As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("AvailabilityId.BudgetaryValidityId.Id =" & validityId & " And AvailabilityId.Status =" & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetAvailabilityDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el detalle de la obligacion
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationDetailByValidityIdAndStatus(validityId As Integer, status As Integer, thirdPartyId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("ObligationId.BudgetaryValidityId =" & validityId & " And ObligationId.Status =" & status & " And ObligationId.ThirdPartyId.Id = " & thirdPartyId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetObligationDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="RecognitionId">Id del reconocimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionDetailByRecognitionId(RecognitionId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("RecognitionId.Id =" & RecognitionId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetRecognitionDetailXpo), criteria)
        Return collect
    End Function
    ''' <summary>
    ''' metodo para listar los detalles del compromiso para usarlos en el popup de obligaiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentDetailObligation(validityId As Integer, thirdPartyId As Integer, documentDate As DateTime) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("CommitmentId.BudgetaryValidityId =" & validityId & " And CommitmentId.ThirdPartyId= " & thirdPartyId & "And CommitmentId.Status = 2 And Balance > 0 And ExpiredDate >=" & documentDate)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetCommitmentDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los detalles de suspencion presupuestal por id de la suspencion
    ''' </summary>
    ''' <param name="SuspensionId">Id de la suspencion presupuestal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionDetailBySuspensionId(SuspensionId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("SuspensionId.Id =" & SuspensionId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetSuspensionDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="CommitmentId">Id del compromiso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentDetailByCommitmentId(CommitmentId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("CommitmentId.Id =" & CommitmentId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetCommitmentDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los detalles de la orden de pago por id de la orden de pago
    ''' </summary>
    ''' <param name="PaymentOrderId">Id de la orden de pago</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderDetailByPaymentOrderId(PaymentOrderId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("PaymentOrderId.Id =" & PaymentOrderId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetPaymentOrderDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los detalles de la disponibilidad por id de disponibilidad
    ''' </summary>
    ''' <param name="AvailabilityId">Id del reconocimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityDetailByAvailabilityId(AvailabilityId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("AvailabilityId.Id =" & AvailabilityId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetAvailabilityDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAnnualizedCashFlowValidityId(validityId As Integer, status As Integer, type As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("CategoryId.BudgetaryValidityId.Id =" & validityId & " And Status = " & CByte(status) & "And DocumentSource = " & CByte(type))
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetAnnualizedCashFlowXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el reconocimiento la vigencia y el tercero y estado
    ''' </summary>
    Public Function ListRecognitionDetailByValidatyIdAndThirdPartyId(validityId As Integer, thirdPartyId As Integer, status As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("RecognitionId.BudgetaryValidityId =" & validityId & "And RecognitionId.ThirdPartyId=" & thirdPartyId & " And RecognitionId.Status = " & CByte(status))
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetRecognitionDetailXpo), criteria)
        Return collect
    End Function
    ''' <summary>
    ''' lista los recaudos para hacer modificaciones
    ''' </summary>
    ''' <param name="collectionId"></param>
    ''' <param name="validatyId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionDetailByCollectionIdAndValidatyId(collectionId As Integer, validatyId As Integer, status As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("CollectionId.BudgetaryValidityId =" & validatyId & "And CollectionId.Id=" & collectionId & "And Balance > 0 And CollectionId.Status = " & CByte(status))
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetCollectionDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los traslados pac con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRecognitionModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName;RecognitionId.Code", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de las disponibilidades por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetAvailabilityModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;AvailabilityId.Code", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de los compromisos por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCommitmentModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;CommitmentId.Code", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los traslados pac con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAnnualizedCashFlowTransferByValidityId(validityId As Integer, type As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId & "And DocumentSource = " & type)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetAnnualizedCashFlowTransferXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los reconocimientos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRecognitionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCommitmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetSuspensionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetPaymentOrderXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCommitmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName;CommitmentTypeName;DocumentSourceName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetPaymentOrderXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los levantamientos de suspenciones de presupuesto
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionCancellationByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetSuspensionCancellationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName;SuspensionId.Code", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetSuspensionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>-+
    Public Function ListReimbursementResourceByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetReimbursementResourceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;UpToName;DocumentDate;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetAvailabilityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;ExpirationDate", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia, estado, saldo y que la fecha de vencimiento sea menor a la actual
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewAvailabilityBalance(budgetValidityId As Integer, status As Integer, dateServer As Date) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId =" & budgetValidityId & "And GetDate(ExpirationDate) <= #" & Format(dateServer, "yyyy-MM-dd") & "# And BalanceAvaliability > 0 And Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(VAvailabilityBalanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;ExpirationDate", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de la tabla budget con el valor presupuestado dependiendo de
    ''' la vigencia que escojan y el tipo(Ingreso o Gasto)
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListAnnualizedCashFlow(budgetValidityId As Integer, itemType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId =" & budgetValidityId & " And ItemType =" & itemType)
        classEntity = sessionNew.GetClassInfo(GetType(VListAnnualizedCashFlowXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name;FinancialSource;ItemType;BudgetaryValidityId;BudgetValue;PACValue;Status;StatusText", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de la tabla RevenueType dependiendo de
    ''' la vigencia que escojan y el tipo(Ingreso o Gasto)
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRevenueTypeByBudgetValidityIdAndTypeAndStatus(budgetValidityId As Integer, itemType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Type =" & itemType & " And Status=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode;IncomeSource", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudgetConceptType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetBudgetConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudgetDependencyType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetBudgetDependencyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ResponsibleId", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType & " And Status =" & Status & " And FinancialSourceId is null")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCategoryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType & " And Status =" & Status & " And FinancialSourceId is not null")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCategoryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategoryByFilter(filter As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCategoryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategoryByBudgetaryValidityIdAndItemType(filtro As String) As XPInstantFeedbackSource

        Dim arrayFilter As String() = filtro.Split(" - ")
        Dim BudgetaryValidityId As Integer = CInt(arrayFilter(0).Trim)
        Dim ItemType As Integer = CInt(arrayFilter(2).Trim)

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType)
        classEntity = sessionNew.GetClassInfo(GetType(BudgetCategoryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(BudgetaryValidityId As Integer, ItemType As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BudgetCategoryXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBudgetModifications(budgetaryValidityId As Integer, itemType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId = " & budgetaryValidityId & " And DocumentSource = " & CInt(itemType) & "")
        classEntity = sessionNew.GetClassInfo(GetType(BudgetModificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;BudgetaryValidityId;BudgetaryValidityId.Year;BudgetaryValidityId.BudgetaryEntityId.Name;Code;DocumentDate;Document;StatusName", criteria)
        Return serverMode
    End Function
#End Region

End Class
