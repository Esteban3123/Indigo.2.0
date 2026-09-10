'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.BudgetRepository
' Author           : Jhossept Kevin Garay
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports Infrastructure.CrossCutting.Base
'Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class BudgetServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

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

    ''' <summary>
    ''' lista los ingresos de presupuesto de tipo ingreso
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetIncome(filtro As String) As XPCollection(Of VReportBudgetIncomeXpo)
        Dim session As New IndigoXPOSession(Of VReportBudgetIncomeXpo)()
        Dim collect As New XPCollection(Of VReportBudgetIncomeXpo)(session, CriteriaOperator.Parse(filtro))
        collect.Sorting.Add(New SortProperty("CategoryCode", SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista los ingresos de presupuesto de tipo gasto
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetExpense(filtro As String) As XPCollection(Of VReportBudgetExpenseXpo)
        Dim session As New IndigoXPOSession(Of VReportBudgetExpenseXpo)()
        Dim collect As New XPCollection(Of VReportBudgetExpenseXpo)(session, CriteriaOperator.Parse(filtro))
        collect.Sorting.Add(New SortProperty("CategoryCode", SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista las obligaciones confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationByStatusConfirmedAndValidityId(ValidityId As Integer, Status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetObligationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetObligationXpo)),
                                               "Id;Code;Document;DocumentDate;ObligationTypeName;ThirdPartyId.NitName;Status;StatusName;BudgetaryValidityId",
                                               CriteriaOperator.Parse($"Status={Status} And BudgetaryValidityId={ValidityId}"))
    End Function

    ''' <summary>
    ''' Lista las obligaciones confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPrivateBudgetItemsStructureForOpenSearch() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PrivateBudgetItemsStructureXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PrivateBudgetItemsStructureXpo)),
                                               "Id;Code;Description", Nothing)
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de ingresos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetDocumentsNotConfirmed(documentType As EBudgetDocumentType, Optional validityId As Integer = 0) As XPCollection
        Dim session As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim collect As XPCollection = Nothing
        Select Case documentType
            Case EBudgetDocumentType.BudgetModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1 And  BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1")
                End If
                collect = New XPCollection(session, GetType(BudgetModificationXpo), criteria)
            Case EBudgetDocumentType.BudgetTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1 And BudgetaryValidityId =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1")
                End If
                collect = New XPCollection(session, GetType(BudgetTransferXpo), criteria)
            Case EBudgetDocumentType.AnnualizedCashFlowModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1 And BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1")
                End If
                collect = New XPCollection(session, GetType(BudgetAnnualizedCashFlowModificationXpo), criteria)
            Case EBudgetDocumentType.AnnualizedCashFlowTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1 And BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 1")
                End If
                collect = New XPCollection(session, GetType(BudgetAnnualizedCashFlowTransferXpo), criteria)
            Case EBudgetDocumentType.Recognition
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetRecognitionXpo), criteria)
            Case EBudgetDocumentType.RecognitionModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetRecognitionModificationXpo), criteria)
            Case EBudgetDocumentType.Collection
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetCollectionXpo), criteria)
            Case EBudgetDocumentType.CollectionModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetCollectionModificationXpo), criteria)
        End Select
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de gastos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetDocumentsNotConfirmedExpenses(documentType As EBudgetDocumentTypeExpense, Optional validityId As Integer = 0) As XPCollection
        Dim session As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim collect As XPCollection = Nothing
        Select Case documentType
            Case EBudgetDocumentTypeExpense.BudgetModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2 And  BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2")
                End If
                collect = New XPCollection(session, GetType(BudgetModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.BudgetTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2 And BudgetaryValidityId =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2")
                End If
                collect = New XPCollection(session, GetType(BudgetTransferXpo), criteria)
            Case EBudgetDocumentTypeExpense.AnnualizedCashFlowModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2 And BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2")
                End If
                collect = New XPCollection(session, GetType(BudgetAnnualizedCashFlowModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.AnnualizedCashFlowTransfer
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2 And BudgetaryValidityId.Id =" & validityId)
                Else
                    criteria = CriteriaOperator.Parse("Status=1 And DocumentSource = 2")
                End If
                collect = New XPCollection(session, GetType(BudgetAnnualizedCashFlowTransferXpo), criteria)
            Case EBudgetDocumentTypeExpense.Availability
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetAvailabilityXpo), criteria)
            Case EBudgetDocumentTypeExpense.AvailabilityModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId.Id =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetAvailabilityModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.Commitment
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetCommitmentXpo), criteria)
            Case EBudgetDocumentTypeExpense.CommitmentModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetCommitmentModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.Obligation
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetObligationXpo), criteria)
            Case EBudgetDocumentTypeExpense.ObligationModification
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetObligationModificationXpo), criteria)
            Case EBudgetDocumentTypeExpense.PaymentOrder
                If validityId > 0 Then
                    criteria = CriteriaOperator.Parse("Status=1 And BudgetaryValidityId =" & validityId)
                End If
                collect = New XPCollection(session, GetType(BudgetPaymentOrderXpo), criteria)
        End Select
        Return collect
        'End Using
    End Function


    ''' <summary>
    ''' Lista las modificaciones del pac
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPACModification(Validity As Integer, type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetAnnualizedCashFlowModificationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetAnnualizedCashFlowModificationXpo)),
                                               "Id;Code;DocumentDate;Document;Status;StatusName;BudgetaryValidityId",
                                               CriteriaOperator.Parse($"BudgetaryValidityId.Id={Validity} And DocumentSource = {type}"))
    End Function

    ''' <summary>
    ''' Lista los recaudos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollection(Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCollectionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetCollectionXpo)),
                                               "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName;InitialValue;Balance",
                                               CriteriaOperator.Parse($"BudgetaryValidityId={Validity}"))
    End Function

    ''' <summary>
    ''' Lista las obligaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligation(Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetObligationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetObligationXpo)),
                                               "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName;InitialValue;Balance;Document",
                                               CriteriaOperator.Parse($"BudgetaryValidityId={Validity}"))
    End Function

    ''' <summary>
    ''' Lista los registros de la estructura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPrivateBudgetItemsStructure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PrivateBudgetItemsStructureXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PrivateBudgetItemsStructureXpo)),
                                               "Id;Code;Description;ParentId;CodeDescription;Type", CriteriaOperator.Parse($"Type={1}"))
    End Function

    ''' <summary>
    ''' Lista los registros de la estructura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPrivateBudgetItemsStructureWithNotId(Id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PrivateBudgetItemsStructureXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PrivateBudgetItemsStructureXpo)),
                                               "Id;Code;Description;ParentId;CodeDescription;Type", CriteriaOperator.Parse($"Type={1} And Id<>{Id}"))
    End Function

    ''' <summary>
    ''' Lista los recaudos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionByStatus(Validity As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCollectionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetCollectionXpo)),
                                               "Id;Code;DocumentDate;Status;StatusName;ThirdPartyId;ThirdPartyId.NitName",
                                               CriteriaOperator.Parse($"BudgetaryValidityId={Validity} And Status={CByte(status)}"))
    End Function
    ''' <summary>
    ''' Lista los recaudos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionModification(Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCollectionModificationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetCollectionModificationXpo)),
                                               "Id;Code;DocumentDate;Status;StatusName;CollectionId;CollectionId.Code;CollectionId.ThirdPartyId.NitName",
                                               CriteriaOperator.Parse($"BudgetaryValidityId={Validity}"))
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDependency(validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetBudgetDependencyXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetBudgetDependencyXpo)),
                                               "Id;BudgetaryValidityId;Code;Name;NameCode;ResponsibleId;Status",
                                               CriteriaOperator.Parse($"BudgetaryValidityId.Id={validity}"))
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFinancialSource(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetFinancialSourceReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
        Dim classEntity = session.GetClassInfo(GetType(BudgetFinancialSourceReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEarningsType(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeReportXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetRevenueTypeReportXpo)),
                                               "Id;Code;Name;Status",
                                               CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity & " And Type=1"))
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseType(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeReportXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetRevenueTypeReportXpo)),
                                               "Id;Code;Name;Status",
                                               CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity & " And Type=2"))
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetDependency(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetDependencyReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
            Dim classEntity = session.GetClassInfo(GetType(BudgetDependencyReportXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los traslados de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetTransferByType(type As Integer, budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetTransferXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentSource=" & type & " And BudgetaryValidityId=" & budgetValidityId)
        Dim classEntity = session.GetClassInfo(GetType(BudgetTransferXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;DocumentSource;Status;StatusName;BudgetaryValidityId", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los traslados de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function ListObligationModificationByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetObligationModificationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetObligationModificationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;Status;StatusName;BudgetaryValidityId;ObligationId.Code;ObligationId.ThirdPartyId.NitName;UpToName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los reconocimientos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRecognition(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRecognitionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id = " & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetRecognitionXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Document;Observations;Status;StatusName;BudgetaryValidityId.Id;ThirdPartyId.NitName;InitialValue;TotalRecognition", criteria)
    End Function

    ''' <summary>
    ''' Lista todos las disponibilidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAvailability(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId=" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetAvailabilityXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;BudgetaryValidityId;Code;AvailabilityType;AvailabilityTypeName;DocumentDate;ExpirationDays;Observations;InitialValue;TotalAvailability;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetConcept(ByVal Entity As Integer, ByVal Validity As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetConceptReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id=" & Validity)
            Dim classEntity = session.GetClassInfo(GetType(BudgetConceptReportXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", criteria)
    End Function

    ''' <summary>
    ''' Gets all Budget Entities
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllBudgetEntities(filtro As String) As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Dim session As New IndigoXPOSession(Of BudgetBudgetaryValidityReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As New XPCollection(Of BudgetBudgetaryValidityReportXpo)(session, criteria)
        collect.Sorting.Add(New SortProperty("Year", SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllBudgetRevenueType(filtro As String) As XPCollection(Of BudgetRevenueTypeReportXpo)
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As New XPCollection(Of BudgetRevenueTypeReportXpo)(session, criteria)
        collect.Sorting.Add(New SortProperty("Code", SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFinancialSource(ByVal ValidityId As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetFinancialSourceXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetFinancialSourceXpo))
            Dim criteria As CriteriaOperator
            Dim serverMode = Nothing
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
        Dim session As New IndigoXPOSession(Of Budget_BudgetaryEntity)()
        Dim classEntity = session.GetClassInfo(GetType(Budget_BudgetaryEntity))
        Dim serverModeCollection As New XPServerCollectionSource(session, classEntity, Nothing)
        serverModeCollection.DisplayableProperties = "NameCode;Budget_BudgetaryValidity[Id = Budget_BudgetaryEntity.Id].Year"
        Return serverModeCollection
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de política pública
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPublicPolicy(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetPublicPolicyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(BudgetPublicPolicyXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode;Observation", criteria)
    End Function

#Region "Evaluation Invoice LinqInstantFeedBackSource"


    ''' <summary>
    ''' Obtiene todas las entidades presupuestales
    ''' </summary>
    Public Function ListBudgetEntity() As LinqInstantFeedbackSource
        Dim vlinqInitialBudget As New LinqInstantFeedbackSource
        AddHandler vlinqInitialBudget.GetQueryable, AddressOf OnGetQueryableInitialBudget
        AddHandler vlinqInitialBudget.DismissQueryable, AddressOf DismissQueryableInitialBudget
        vlinqInitialBudget.KeyExpression = "Id"
        Return vlinqInitialBudget
    End Function
    Private Sub OnGetQueryableInitialBudget(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEntity As XPQuery(Of Budget_BudgetaryEntity) = New XPQuery(Of Budget_BudgetaryEntity)(session)
            Dim tableValidity As XPQuery(Of Budget_BudgetaryValidity) = New XPQuery(Of Budget_BudgetaryValidity)(session)
            Dim TmpQueryableSource = From objValidity In tableValidity
                                     Join objEntity In tableEntity On objEntity.Id Equals objValidity.BudgetaryEntityId.Id
                                     Where objValidity.Status = 2
                                     Select Id = objValidity.Id, Code = objEntity.Code & " - " & objEntity.Name, Year = objValidity.Year, DateIncomeRecord = objValidity.DateIncomeRecord, DateExpensesRecord = objValidity.DateExpensesRecord, Status = objValidity.Status, ResolutionNumber = objValidity.ResolutionNumber, ResolutionValue = objValidity.ResolutionValue, ValidityId = objValidity.Id, InitialStateIncome = objValidity.InitialStateIncome, InitialStateExpenses = objValidity.InitialStateExpenses
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableValidity
            'End Using
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub DismissQueryableInitialBudget(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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
        Dim session As New IndigoXPOSession(Of BudgetBudgetInstitutionsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetBudgetInstitutionsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", Nothing)
    End Function

    ''' <summary>
    ''' Consulta los rubros presupuestales por tipo
    ''' </summary>
    ''' <param name="type">tipo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByType(type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetHeaderId.Type =" & type)
        Dim classEntity = session.GetClassInfo(GetType(BudgetXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFinancialSourceByStatus(status As Boolean, validityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetFinancialSourceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And BudgetaryValidityId = " & validityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetFinancialSourceXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las fuentes de financiación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllFinancialSourceByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetFinancialSourceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetFinancialSourceXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBudgetEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetBudgetInstitutionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(BudgetBudgetInstitutionsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode", criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionBudgetEntityByStatus(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetBudgetInstitutionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(BudgetBudgetInstitutionsXpo), criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListValidityByBudgetByBudgetEntityIdAndStatus(ByVal BudgetaryEntityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetValidityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("BudgetaryEntityId.Id = {0} AND Status = {1}", BudgetaryEntityId, status))
        Dim classEntity = session.GetClassInfo(GetType(BudgetValidityXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(ByVal BudgetaryEntityId As Integer, status As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetValidityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("BudgetaryEntityId.Id = {0} AND Status = {1}", BudgetaryEntityId, status))
        Return New XPCollection(session, GetType(BudgetValidityXpo), criteria)
    End Function

    ''' <summary>
    ''' Lista las cuentas por pagar que tenga asociado la unidad de radicacion
    ''' </summary>
    ''' <param name="BudgetaryEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListValidityByBudgetEntityId(ByVal BudgetaryEntityId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetValidityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryEntityId.Id=" & BudgetaryEntityId)
        Return New XPCollection(session, GetType(BudgetValidityXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista los detalles de la obligacion
    ''' </summary>
    ''' <param name="ObligationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationDetailByObligationId(ByVal ObligationId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetObligationDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ObligationId.Id=" & ObligationId)
        Return New XPCollection(session, GetType(BudgetObligationDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetEarningsType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=1")
            Dim classEntity = session.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Source", criteria)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetExpenseType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=2")
            Dim classEntity = session.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Classification;Definition", criteria)
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
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Type =" & itemType)
        Return New XPCollection(session, GetType(BudgetRevenueTypeXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <param name="withBalance">Permite saber si se consulta con saldo mayor a cero o no</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByBudgetValidityId(validityId As Integer, status As Integer, type As Integer, withBalance As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetXpo)()
        Dim criteria As CriteriaOperator
        If withBalance Then
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type & " And Balance>0")
        Else
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type)
        End If
        Return New XPCollection(session, GetType(BudgetXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <param name="withBalance">Permite saber si se consulta con saldo mayor a cero o no</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByBudgetValidityIdAndTypeAndStatus(validityId As Integer, status As Integer, type As Integer, withBalance As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetXpo)()
        Dim criteria As CriteriaOperator
        If withBalance Then
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type & " And Balance > 0")
        Else
            criteria = CriteriaOperator.Parse("BudgetHeaderId.BudgetaryValidityId.Id =" & validityId & " And BudgetHeaderId.Status =" & status & " And BudgetHeaderId.Type =" & type)
        End If
        Dim classEntity = session.GetClassInfo(GetType(BudgetXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista los presupuestos por tipo de rubro
    ''' </summary>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByCategoryItemType(ItemType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VIEDev.Budget_Budget)()
        Dim criteria = CriteriaOperator.Parse("CategoryId.ItemType =" & ItemType)
            Dim classEntity = session.GetClassInfo(GetType(VIEDev.Budget_Budget))
        Return New XPInstantFeedbackSource(classEntity,
                                               "Id;Balance;CategoryId.FinancialSourceId.NameCode;CategoryId.NameCode;CategoryId.BudgetaryValidityId.BudgetaryEntityId.NameCode;RevenueTypeId.NameCode;CategoryId.BudgetaryValidityId.Year",
                                               criteria)
    End Function

    ''' <summary>
    ''' Consulta el detalle de disponibilidad
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityDetailByValidityIdAndStatus(validityId As Integer, status As Integer, includeZero As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityDetailXpo)()
        Dim filter As String = "AvailabilityId.BudgetaryValidityId.Id =" & validityId & " And AvailabilityId.Status =" & status
        If Not includeZero Then
            filter = filter + " And Balance > 0"
        End If
        Dim criteria = CriteriaOperator.Parse(filter)
        Return New XPCollection(session, GetType(BudgetAvailabilityDetailXpo), criteria)
    End Function

    ''' <summary>
    ''' Consulta el detalle de la obligacion
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListObligationDetailByValidityIdAndStatus(validityId As Integer, status As Integer, thirdPartyId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetObligationDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("ObligationId.BudgetaryValidityId =" & validityId & " And ObligationId.Status =" & status & " And ObligationId.ThirdPartyId.Id = " & thirdPartyId & " And Balance > 0")
        Return New XPCollection(session, GetType(BudgetObligationDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="RecognitionId">Id del reconocimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionDetailByRecognitionId(RecognitionId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetRecognitionDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("RecognitionId.Id =" & RecognitionId)
        Return New XPCollection(session, GetType(BudgetRecognitionDetailXpo), criteria)
        'End Using
    End Function
    ''' <summary>
    ''' metodo para listar los detalles del compromiso para usarlos en el popup de obligaiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentDetailObligation(validityId As Integer, thirdPartyId As Integer, documentDate As DateTime) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetCommitmentDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("CommitmentId.BudgetaryValidityId =" & validityId & " And CommitmentId.ThirdPartyId= " & thirdPartyId & "And CommitmentId.Status = 2 And Balance > 0 And ExpiredDate >=#" & Format(documentDate, "yyyy-MM-dd") & "#")
        Return New XPCollection(session, GetType(BudgetCommitmentDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta los detalles de suspencion presupuestal por id de la suspencion
    ''' </summary>
    ''' <param name="SuspensionId">Id de la suspencion presupuestal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionDetailBySuspensionId(SuspensionId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetSuspensionDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("SuspensionId.Id =" & SuspensionId)
        Return New XPCollection(session, GetType(BudgetSuspensionDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="CommitmentId">Id del compromiso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentDetailByCommitmentId(CommitmentId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetCommitmentDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("CommitmentId.Id =" & CommitmentId)
        Return New XPCollection(session, GetType(BudgetCommitmentDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta los detalles de la orden de pago por id de la orden de pago
    ''' </summary>
    ''' <param name="PaymentOrderId">Id de la orden de pago</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderDetailByPaymentOrderId(PaymentOrderId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetPaymentOrderDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("PaymentOrderId.Id =" & PaymentOrderId)
        Return New XPCollection(session, GetType(BudgetPaymentOrderDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta los detalles de la disponibilidad por id de disponibilidad
    ''' </summary>
    ''' <param name="AvailabilityId">Id del reconocimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityDetailByAvailabilityId(AvailabilityId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("AvailabilityId.Id =" & AvailabilityId)
        Return New XPCollection(session, GetType(BudgetAvailabilityDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el PAC inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAnnualizedCashFlowValidityId(validityId As Integer, status As Integer, type As Integer, month As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetAnnualizedCashFlowXpo)()
        Dim criteria = CriteriaOperator.Parse("CategoryId.BudgetaryValidityId.Id =" & validityId & " And Status = " & CByte(status) & "And DocumentSource = " & CByte(type) & " And Month >= " & month)
        Return New XPCollection(session, GetType(BudgetAnnualizedCashFlowXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el reconocimiento la vigencia y el tercero y estado
    ''' </summary>
    Public Function ListRecognitionDetailByValidatyIdAndThirdPartyId(validityId As Integer, thirdPartyId As Integer, status As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetRecognitionDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("RecognitionId.BudgetaryValidityId =" & validityId & "And RecognitionId.ThirdPartyId=" & thirdPartyId & " And RecognitionId.Status = " & CByte(status))
        Return New XPCollection(session, GetType(BudgetRecognitionDetailXpo), criteria)
        'End Using
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
        Dim session As New IndigoXPOSession(Of BudgetCollectionDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("CollectionId.BudgetaryValidityId =" & validatyId & "And CollectionId.Id=" & collectionId & "And Balance > 0 And CollectionId.Status = " & CByte(status))
        Return New XPCollection(session, GetType(BudgetCollectionDetailXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta los traslados pac con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRecognitionModificationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetRecognitionModificationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName;RecognitionId.Code;RecognitionId.ThirdPartyId.NitName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de las disponibilidades por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityModificationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetAvailabilityModificationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;AvailabilityId.Code;UpToName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de los compromisos por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentModificationByValidityId(validityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCommitmentModificationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId)
        Dim classEntity = session.GetClassInfo(GetType(BudgetCommitmentModificationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;CommitmentId.Code;CommitmentId.ThirdPartyId.NitName;UpToName", criteria)
    End Function

    ''' <summary>
    ''' Consulta los traslados pac con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAnnualizedCashFlowTransferByValidityId(validityId As Integer, type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetAnnualizedCashFlowTransferXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & validityId & "And DocumentSource = " & type)
            Dim classEntity = session.GetClassInfo(GetType(BudgetAnnualizedCashFlowTransferXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta los reconocimientos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRecognitionByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetRecognitionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetRecognitionXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCommitmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetCommitmentXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista los compromisos por id tercero y estado
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function ListCommitmentDetailByThirdPartyIdAndStatus(ThirdPartyId As Integer, Status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCommitmentDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId =" & ThirdPartyId & " And Status = " & Status)
        Dim classEntity = session.GetClassInfo(GetType(ViewListCommitmentDetailXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista los comrpomisos por tercero y estado
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function ListCommitmentDetailByThirdPartyIdAndStatusXpCollection(ThirdPartyId As Integer, Status As Integer, BudgetaryValidityId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListCommitmentDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId =" & ThirdPartyId & " And Status = " & Status & " And BudgetaryValidityId = " & BudgetaryValidityId)
        Return New XPCollection(session, GetType(ViewListCommitmentDetailXpo), criteria)
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetSuspensionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetSuspensionXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetPaymentOrderXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetPaymentOrderXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCommitmentByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCommitmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetCommitmentXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;CommitmentTypeName;DocumentDate;ThirdPartyId.NitName;DocumentSourceName;Document;InitialValue;TotalCommitment;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentOrderByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetPaymentOrderXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetPaymentOrderXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;ThirdPartyId.NitName;InitialValue;Balance;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta los levantamientos de suspenciones de presupuesto
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionCancellationByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetSuspensionCancellationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetSuspensionCancellationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName;SuspensionId.Code", criteria)
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuspensionByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetSuspensionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetSuspensionXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Document;DocumentDate;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>-+
    Public Function ListReimbursementResourceByValidityId(budgetValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetReimbursementResourceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId)
            Dim classEntity = session.GetClassInfo(GetType(BudgetReimbursementResourceXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;UpToName;DocumentDate;PaymentOrderId.Code;PaymentOrderId.ThirdPartyId.NitName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia y estado
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityByValidityIdAndStatus(budgetValidityId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Status = " & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetAvailabilityXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;ExpirationDate", criteria)
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia, estado, saldo y que la fecha de vencimiento sea menor a la actual
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewAvailabilityBalance(budgetValidityId As Integer, status As Integer, dateServer As Date) As XPInstantFeedbackSource
        Dim dateAdd = dateServer.AddDays(7)
        Dim session As New IndigoXPOSession(Of VAvailabilityBalanceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId =" & budgetValidityId & "And GetDate(ExpirationDate) <= #" & Format(dateAdd, "yyyy-MM-dd") & "# And BalanceAvaliability > 0 And Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(VAvailabilityBalanceXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;ExpirationDate", criteria)
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
        Dim session As New IndigoXPOSession(Of VListAnnualizedCashFlowXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId =" & budgetValidityId & " And ItemType =" & itemType)
            Dim classEntity = session.GetClassInfo(GetType(VListAnnualizedCashFlowXpo))
        Return New XPInstantFeedbackSource(classEntity, "Code;Name;FinancialSource;ItemType;BudgetaryValidityId;BudgetValue;PACValue;Status;StatusText", criteria)
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
        Dim session As New IndigoXPOSession(Of BudgetRevenueTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & budgetValidityId & " And Type =" & itemType & " And Status=" & status)
            Dim classEntity = session.GetClassInfo(GetType(BudgetRevenueTypeXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;NameCode;IncomeSource", criteria)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudgetConceptType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetBudgetConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetBudgetConceptXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudgetDependencyType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetBudgetDependencyXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetBudgetDependencyXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ResponsibleId", Nothing)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType & " And Status =" & Status & " And FinancialSourceId Is null")
        Dim classEntity = session.GetClassInfo(GetType(BudgetCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCCPETByStatusAndItemType(Status As Boolean, ItemType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCCPETXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ItemType = " & ItemType & " And Status = " & Status & " AND AccountType = 1")
        Dim classEntity = session.GetClassInfo(GetType(BudgetCCPETXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;ItemTypeName;Code;Name;AccountTypeName;LinkAccountName;StatusName;CCPETOwnerId;NameCode", criteria)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCPCCatalogTByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCPCCatalogXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(" Status = " & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(BudgetCPCCatalogXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;StatusName;CCPETOwnerId;NameCode", criteria)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso que solo sean hijos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCPCCatalogTSon(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetViewCPCCatalogXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetViewCPCCatalogXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;StatusName;CCPETOwnerId;NameCode", Nothing)
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategoryByFilter(filter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
            Dim classEntity = session.GetClassInfo(GetType(BudgetCategoryXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetCategoryXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", Nothing)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType & " And Status =" & Status & " And FinancialSourceId Is Not null")
            Dim classEntity = session.GetClassInfo(GetType(BudgetCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;NameCode;Code;Name;FinancialSourceId.NameCode", criteria)
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

        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType)
        Dim classEntity = session.GetClassInfo(GetType(BudgetCategoryXpo))
        Return New XPInstantFeedbackSource(classEntity, "", criteria)

    End Function

    ''' <summary>
    ''' consulta los CCPET
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCCPET() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCCPETXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetCCPETXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;ItemTypeName;Code;Name;AccountTypeName;LinkAccountName;StatusName", Nothing)

    End Function

    ''' <summary>
    ''' consulta los CATALOGO CPC
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCPCCatalog() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetCPCCatalogXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BudgetCPCCatalogXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;StatusName;NameCode;CPCCatalogOwnerId", Nothing)
    End Function


    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(BudgetaryValidityId As Integer, ItemType As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetCategoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId.Id =" & BudgetaryValidityId & " And ItemType =" & ItemType)
        Return New XPCollection(session, GetType(BudgetCategoryXpo), criteria)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCCPETTreeList() As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetCCPETXpo)()
        Return New XPCollection(session, GetType(BudgetCCPETXpo))
    End Function

    ''' <summary>
    ''' Lista todos los CATALOGO CPC 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCPCCatalogTreeList() As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetCPCCatalogXpo)()
        Return New XPCollection(session, GetType(BudgetCPCCatalogXpo))
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos xp server collection
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBudgetModifications(budgetaryValidityId As Integer, itemType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BudgetModificationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BudgetaryValidityId = " & budgetaryValidityId & " And DocumentSource = " & CInt(itemType) & "")
        Dim classEntity = session.GetClassInfo(GetType(BudgetModificationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;BudgetaryValidityId;BudgetaryValidityId.Year;BudgetaryValidityId.BudgetaryEntityId.Name;Code;DocumentDate;Document;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista las obligaciones vigentes por documento de origen
    ''' </summary>
    ''' <param name="EntityName"></param>
    ''' <param name="EntityId"></param>
    ''' <returns></returns>
    Public Function ListObligationDetailByEntity(EntityName As String, EntityId As Integer, Nature As Integer) As XPCollection
        Dim filters As String = "EntityName ='" & EntityName & "' And EntityId = " & EntityId & If(Nature = 1, " And Balance > 0", "")

        Dim session As New IndigoXPOSession(Of ViewListObligationDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Dim collect As XPCollection = New XPCollection(session, GetType(ViewListObligationDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las disponibilidades vigentes
    ''' </summary>
    ''' <param name="BudgetaryValidityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAvailabilityDetails(BudgetaryValidityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListAvailabilityDetailXpo)()
        Dim criteria = CriteriaOperator.Parse("BudgetaryValidityId =" & BudgetaryValidityId & " And Balance > 0")
        Dim classEntity = session.GetClassInfo(GetType(ViewListAvailabilityDetailXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista CPC activos sin hijos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCPCCatalogActiveWithoutChildren() As XPCollection
        Dim session As New IndigoXPOSession(Of BudgetViewCPCCatalogXpo)()
        Return New XPCollection(session, GetType(BudgetViewCPCCatalogXpo))
    End Function

    ''' <summary>
    ''' Valida si el catalogo de CPC se usa en otros registros como disponibilidad o categoria
    ''' </summary>
    ''' <param name="CPCCatalogOwnerId"></param>
    ''' <returns></returns>
    Public Function ValidateCPCRegister(ByVal CPCCatalogOwnerId As Integer) As Boolean
        Dim filters As String = String.Format("CPCCodeId = {0} And AvailabilityId is not null And AvailabilityId.Status In (1,2)", CPCCatalogOwnerId)
        Dim session As New IndigoXPOSession(Of BudgetAvailabilityDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Dim collect As XPCollection = New XPCollection(session, GetType(BudgetAvailabilityDetailXpo), criteria)
        If collect.Count > 0 Then
            Return True
        End If
        filters = String.Format("CPCCodeId is not null And CPCCodeId.Id = {0}", CPCCatalogOwnerId)
        Dim session1 As New IndigoXPOSession(Of BudgetCategoryXpo)()
        criteria = CriteriaOperator.Parse(filters)
        collect = New XPCollection(session1, GetType(BudgetCategoryXpo), criteria)
        If collect.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    Public Function GetBudgetaryValidityId(validityId As Integer) As BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo
        Dim session As New IndigoXPOSession(Of BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo)()
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse(String.Format("Id = {0}", validityId))
        Return session.FindObject(Of BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo)(criteria)
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
