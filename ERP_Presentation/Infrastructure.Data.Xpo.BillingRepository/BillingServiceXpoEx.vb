'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Diego A. Roldan
' Created          : 2017-04-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
#End Region

Public Class BillingServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function ListViewRelatedInvoices(AdmissionNumber As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"AdmissionNumber = '{AdmissionNumber}'")
        Dim session As New IndigoXPOSession(Of ViewRelatedInvoicesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewRelatedInvoicesXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicSupportDocument() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicSupportDocumentXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicSupportDocumentXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista de Numeracion Autorizacion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDocumentNumberingAuthorization() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of NumberingAuthorizationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(NumberingAuthorizationXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista las devoluciones parciales de venta
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDocumentInvoiceProductSalesDevolution() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentInvoiceProductSalesDevolutionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentInvoiceProductSalesDevolutionXpo)), "Id;Code;DocumentDate;WarehouseId.Id;WarehouseId.CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista las devoluciones parciales de venta
    ''' </summary>
    ''' <returns></returns>
    Public Function ListQuotation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of QuotationXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(QuotationXpo)), "Id;Code;DocumentDate;Status;StatusName;QuotationTypeName;Description;AdmissionInformation.CareGroupCodeName;AdmissionInformation.PatientCodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los cups y medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCUPSEntityAndInventoryProduct() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCUPSEntityAndInventoryProductXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewListCUPSEntityAndInventoryProductXpo)), Nothing, Nothing)
    End Function

    Public Function ListViewAdmissionsToAccountControlAmbulatoryByCareCenterCode(CareCenterCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToAccountControlAmbulatoryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CareCenterCode = {CareCenterCode}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewAdmissionsToAccountControlAmbulatoryXpo)), Nothing, criteria)
    End Function

    Public Function ListRIASCupsByCupsCodeAndStatus(CupsCode As String, Status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewRIASCupsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CupsCode = '{CupsCode}' And RiasStatus = {Status}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewRIASCupsXpo)), Nothing, criteria)
    End Function
    Public Function ListRIASCupsByCupsCodeAndStatus(CupsCode As List(Of String), Status As Integer) As XPInstantFeedbackSource
        Dim codeFilter = String.Join(",", CupsCode.Select(Function(d) $"'{d}'"))
        Dim session As New IndigoXPOSession(Of ViewRIASCupsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CupsCode IN ({codeFilter}) And RiasStatus = {Status}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewRIASCupsXpo)), Nothing, criteria)
    End Function

    Public Function GetCourtDate(revenueControlId As Integer) As Date
        Dim session As New IndigoXPOSession(Of BillingRevenueControlCourtAccountXpo)()
        Return CDate(session.Evaluate(Of BillingRevenueControlCourtAccountXpo)(CriteriaOperator.Parse("Max(CourtDate)"),
                                                                                      New OperandProperty("RevenueControlId") = revenueControlId))
        'End Using
    End Function

    Public Function GetSettingsBillingByOperatingUnitId(OperatingUnitId As Integer) As SettingsBillingXpo
        Dim session As New IndigoXPOSession(Of SettingsBillingXpo)()
        Return session.FindObject(Of SettingsBillingXpo)(CriteriaOperator.Parse($"IdOperatingUnit={OperatingUnitId}"))
    End Function
    ''' <summary>
    ''' Lista todos los grupos de facturacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllServiceOrder() As XPInstantFeedbackSource
        'Using session As New Session(XpoDefault.DataLayer)
        Dim session As New IndigoXPOSession(Of ServiceOrderXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ServiceOrderXpo)),
                                                          "Id;Code;AdmissionNumber;PatientCode;OrderDate;Status;StatusName;EntityName", Nothing)
    End Function

    Public Function ListBillingJustificationControl() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingJustificationControlXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingJustificationControlXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los ejecutivos de ventas
    ''' </summary>
    Public Function ListSalesExecutive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SalesExecutiveXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(SalesExecutiveXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los ejecutivos de ventas por estado
    ''' </summary>
    Public Function ListSalesExecutiveByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SalesExecutiveXpo)()
        Dim criteria = CriteriaOperator.Parse("Status = " & Status)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(SalesExecutiveXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los Estado de folio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsCausesStatusFolio(Optional Status As Boolean = False) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = Nothing
        If Status Then
            criteria = CriteriaOperator.Parse("Status = 1")
        End If
        Dim session As New IndigoXPOSession(Of ConceptsCausesStatusFolioXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ConceptsCausesStatusFolioXpo)), "Id;Code;Name;StatusName;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los Estado de folio por usuarios permitido
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsCausesStatusFolioUsers(Optional UserId As Integer = 0) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = Nothing
        If UserId > 0 Then
            criteria = CriteriaOperator.Parse("ConceptsCausesStatusFolioId.Status = 1 AND UserId = " & UserId & " ")
        End If
        Dim session As New IndigoXPOSession(Of ConceptsCausesStatusFolioUsersXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ConceptsCausesStatusFolioUsersXpo)), "Id;ConceptsCausesStatusFolioId.Code;ConceptsCausesStatusFolioId.Name;ConceptsCausesStatusFolioId.StatusName;ConceptsCausesStatusFolioId.CodeName;ConceptsCausesStatusFolioId.Id;UserId", criteria)
    End Function

    Public Function GetReverseRecognition(operativeUnitId As Integer) As PLinqServerModeSource
        Dim session As New IndigoXPOSession(Of ViewListRecognitionReverse)()
        Dim tableView As New XPQuery(Of ViewListRecognitionReverse)(session)
        Dim TmpQueryableSource = From T1 In tableView Where T1.OperativeUnitId = operativeUnitId Select T1
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.GroupBy(Function(x) New With {.a = x.CareGroupCodeName, .b = x.CareGroupId, .c = x.RecognitionId}).Select(Function(x) New ViewListRecognitionReverse With {
                                                                           .RecognitionId = x.Key.c,
                                                                           .CareGroupId = x.Key.b,
                                                                           .CareGroupCodeName = x.Key.a,
                                                                           .FolioQuantity = x.Sum(Function(o) o.FolioQuantity),
                                                                           .TotalCareGroup = x.Sum(Function(o) o.TotalCareGroup)
                                                                           }).ToList()
        Return b
    End Function
    ''' <summary>
    ''' Lista todos los grupos de facturacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionEntranceData(operativeUnitId As Integer) As PLinqServerModeSource
        Dim session As New IndigoXPOSession(Of ViewListRecognitionEntrance)()
        Dim tableView As New XPQuery(Of ViewListRecognitionEntrance)(session)
        Dim TmpQueryableSource = From T1 In tableView Where T1.OperativeUnitId = operativeUnitId Select T1
        ''.CareGroupId = x.Key.a,
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.GroupBy(Function(x) New With {.a = x.CareGroupCodeName, .b = x.CareGroupId}).Select(Function(x) New ViewListRecognitionEntrance With {
                                                                           .CareGroupId = x.Key.b,
                                                                           .CareGroupCodeName = x.Key.a,
                                                                           .FolioQuantity = x.Sum(Function(o) o.FolioQuantity),
                                                                           .TotalCareGroup = x.Sum(Function(o) o.TotalCareGroup)
                                                                           }).ToList()
        Return b
        'End Using

    End Function

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceById(id As Integer) As InvoiceXpo
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return session.GetObjectByKey(Of InvoiceXpo)(id)
    End Function

    ''' <summary>
    ''' obtiene una categoria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceCategoriesById(id As Integer) As InvoiceCategoriesXpo
        Dim session As New IndigoXPOSession(Of InvoiceCategoriesXpo)()
        Return session.GetObjectByKey(Of InvoiceCategoriesXpo)(id)
    End Function
    ''' <summary>
    ''' Lists the invoice categories.
    ''' </summary>
    ''' <returns></returns>
    Function ListInvoiceCategories() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceCategoriesXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceCategoriesXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <returns></returns>
    Function ListInvoice() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoicesXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoicesXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <returns></returns>
    Function ListViewListInvoiceAndPatient() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingViewListInvoiceAndPatientXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingViewListInvoiceAndPatientXpo)), Nothing, Nothing)
    End Function

    Public Function GetCategoriesByStatusAndUser(status As Boolean, UserId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceCategoriesXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceCategoriesXpo)), Nothing,
                                               CriteriaOperator.Parse($"Status={status} And InvoiceCategoriesUserXpo[UserId={UserId}]"))
    End Function

    Public Function ListInvoiceCategoryByPermissionCategories(PermissionCategories As Integer, UserId As Integer, careGroupId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceCategoriesXpo)()
        Dim criteria As CriteriaOperator = Nothing
        Select Case PermissionCategories
            Case 1
                criteria = CriteriaOperator.Parse($"Status= 1 And InvoiceCategoriesUserXpo[UserId={UserId}]")
            Case 2
                criteria = CriteriaOperator.Parse($"Status= 1 And Contract_CareGroupInvoiceCategoriess[CareGroupId={careGroupId}]")
            Case 3
                criteria = CriteriaOperator.Parse($"Status= 1 And Contract_CareGroupInvoiceCategoriess[CareGroupId={careGroupId}] And InvoiceCategoriesUserXpo[UserId={UserId}]")
        End Select
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceCategoriesXpo)), Nothing, criteria)
    End Function

    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, listId As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewServiceOrderDetailXpo)()
        Dim InCriteria As String = String.Join(",", listId.ToArray)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewServiceOrderDetailXpo)), Nothing,
                                                      CriteriaOperator.Parse($"Not [Id] In ({InCriteria}) And RecordType = 1  And Status <> 3 And Packaging = 0 And InvoicedQuantity > 0  And AdmissionNumber = '{admissionNumber}'"))
    End Function

    Public Function ListRIPSInvoices(dateInit As Date?, dateEnd As Date?, invoiceType As Byte?, fechaCorte As Date?, entityId As String, careGroupId As String, admissionCode As String,
                                     careCenterId As String, contractId As String, categoryId As String, incomeCauseId As String, typeRiskId As String, Optional Reso676 As Boolean = False) As XPCollection(Of ViewRIPSInvoice)
        Dim criteria As CriteriaOperator = Nothing
        Dim filterStr As New List(Of String)()
        Dim filterStrOther As New List(Of InOperator)()
        If dateInit IsNot Nothing Then
            filterStr.Add("[InvoiceDate] >= #" & Format(dateInit.Value, "yyyy-MM-dd HH:mm : ss") & "#")
        End If
        If dateEnd IsNot Nothing Then
            filterStr.Add("[InvoiceDate] <= #" & Format(dateEnd.Value, "yyyy-MM-dd HH:mm : ss") & "#")
        End If
        If invoiceType IsNot Nothing Then
            If invoiceType = 99 Then
                filterStr.Add("[DocumentType] <> 4")
            Else
                filterStr.Add("[DocumentType] = " & invoiceType)
            End If
        End If
        If fechaCorte IsNot Nothing Then
            'filterStr.Add("[InvoiceDate] = " & fechaCorte)
        End If
        If Not String.IsNullOrEmpty(admissionCode) Then
            filterStr.Add("[AdmissionNumber] = '" & admissionCode & "'")
        End If
        If Not String.IsNullOrEmpty(entityId) Then
            filterStrOther.Add(New InOperator("HealthAdministratorId", entityId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(careGroupId) Then
            filterStrOther.Add(New InOperator("CareGroupId", careGroupId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(careCenterId) Then
            filterStrOther.Add(New InOperator("CODCENATE", careCenterId.Split(",").ToList()))
        End If
        If Not String.IsNullOrEmpty(contractId) Then
            filterStrOther.Add(New InOperator("ContractId", contractId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(categoryId) Then
            filterStrOther.Add(New InOperator("InvoiceCategoryId", categoryId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Reso676 Then
            filterStr.Add(" [MainDiagnosticCode] IN ('U072','U072','J00X','J22X','J440','J441','J46X','J80X','J81X','J960','J969','P220')")
        End If

        If Not String.IsNullOrEmpty(incomeCauseId) Then
            filterStrOther.Add(New InOperator("ICAUSAING", incomeCauseId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(typeRiskId) Then
            filterStrOther.Add(New InOperator("ITIPORIES", typeRiskId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If filterStr.Any() AndAlso filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr) + " AND " + String.Join(" AND ", filterStrOther))
        ElseIf filterStr.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr))
        ElseIf filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStrOther))
        End If

        Dim session As New IndigoXPOSession(Of ViewRIPSInvoice)()
        Return New XPCollection(Of ViewRIPSInvoice)(session, criteria)

        'End Using
    End Function

    Public Function ListRIPSInvoicesPopulationGroup(dateInit As Date?, dateEnd As Date?, invoiceType As Byte?, fechaCorte As Date?, entityId As String, careGroupId As String, admissionCode As String,
                                     careCenterId As String, contractId As String, categoryId As String, incomeCauseId As String, typeRiskId As String, populationGroupId As String) As XPCollection(Of ViewRIPSInvoice)
        Dim criteria As CriteriaOperator = Nothing
        Dim filterStr As New List(Of String)()
        Dim filterStrOther As New List(Of InOperator)()
        If dateInit IsNot Nothing Then
            filterStr.Add("[InvoiceDate] >= #" & Format(dateInit.Value, "yyyy-MM-dd HH:mm : ss") & "#")
        End If
        If dateEnd IsNot Nothing Then
            filterStr.Add("[InvoiceDate] <= #" & Format(dateEnd.Value, "yyyy-MM-dd HH:mm : ss") & "#")
        End If
        If invoiceType IsNot Nothing Then
            If invoiceType = 99 Then
                filterStr.Add("[DocumentType] <> 4")
            Else
                filterStr.Add("[DocumentType] = " & invoiceType)
            End If
        End If
        If fechaCorte IsNot Nothing Then
            'filterStr.Add("[InvoiceDate] = " & fechaCorte)
        End If
        If Not String.IsNullOrEmpty(admissionCode) Then
            filterStr.Add("[AdmissionNumber] = '" & admissionCode & "'")
        End If
        If Not String.IsNullOrEmpty(entityId) Then
            filterStrOther.Add(New InOperator("HealthAdministratorId", entityId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(careGroupId) Then
            filterStrOther.Add(New InOperator("CareGroupId", careGroupId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(careCenterId) Then
            filterStrOther.Add(New InOperator("CODCENATE", careCenterId.Split(",").ToList()))
        End If
        If Not String.IsNullOrEmpty(contractId) Then
            filterStrOther.Add(New InOperator("ContractId", contractId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(categoryId) Then
            filterStrOther.Add(New InOperator("InvoiceCategoryId", categoryId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(incomeCauseId) Then
            filterStrOther.Add(New InOperator("ICAUSAING", incomeCauseId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(typeRiskId) Then
            filterStrOther.Add(New InOperator("ITIPORIES", typeRiskId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If Not String.IsNullOrEmpty(populationGroupId) Then
            filterStrOther.Add(New InOperator("IDADPOBESPE", populationGroupId.Split(",").Select(Function(c) CInt(c)).ToList()))
        End If
        If filterStr.Any() AndAlso filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr) + " AND " + String.Join(" AND ", filterStrOther))
        ElseIf filterStr.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr))
        ElseIf filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStrOther))
        End If

        Dim filterInvoices As String = String.Empty
        Dim session As New IndigoXPOSession(Of ViewRIPSInvoicePopulationGroup)()
        Dim resultado As XPCollection(Of ViewRIPSInvoicePopulationGroup) = New XPCollection(Of ViewRIPSInvoicePopulationGroup)(session, criteria)
        If resultado IsNot Nothing AndAlso resultado.Count > 0 Then
            Dim invoices = String.Format("{0}", String.Join(",", resultado.Select(Function(r) r.InvoiceId)))
            filterInvoices = String.Format("InvoiceId IN ({0}) ", invoices)
        End If

        filterStrOther.RemoveAt(filterStrOther.Count() - 1)
        If filterStr.Any() AndAlso filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr) + " AND " + String.Join(" AND ", filterStrOther) + If(String.IsNullOrEmpty(filterInvoices), filterInvoices, " AND " + filterInvoices))
        ElseIf filterStr.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStr) + If(String.IsNullOrEmpty(filterInvoices), filterInvoices, " AND " + filterInvoices))
        ElseIf filterStrOther.Any() Then
            criteria = CriteriaOperator.Parse(String.Join(" AND ", filterStrOther) + If(String.IsNullOrEmpty(filterInvoices), filterInvoices, " AND " + filterInvoices))
        Else
            criteria = CriteriaOperator.Parse(filterInvoices)
        End If


        Return New XPCollection(Of ViewRIPSInvoice)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista las causas de ingreso que se encuentren en estado activo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListActiveIncomeCauses() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CausesofattentionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CausesofattentionXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=True")
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status", criteria)
    End Function


    'Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(listId As List(Of Integer)) As XPInstantFeedbackSource
    '    Dim session = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[Id] not in (?)", String.Join(",", listId))
    '    classEntity = session.GetClassInfo(GetType(ServiceOrderDetailXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    '    Return serverMode
    'End Function

    Public Function ListServiceOrderDetailSurgicalByServiceOrderDetailId(serviceOrderDetailId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ServiceOrderDetailSurgicalXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ServiceOrderDetailSurgicalXpo)),
                                                          Nothing,
                                                          CriteriaOperator.Parse($"[ServiceOrderDetailId].Id = {serviceOrderDetailId}"))
    End Function

    Public Function ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(surgeryNumber As Byte, serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Dim session As New IndigoXPOSession(Of ServiceOrderDetailXpo)()
        Dim collect As New XPCollection(Of ServiceOrderDetailXpo)(session, CriteriaOperator.Parse($"[SurgeryNumber]={surgeryNumber} And [SurgeryNumber] > 0 And IsDelete = False And Presentation = 2 And [ServiceOrderId].Id = {serviceOrderId}"))
        For Each item In collect
            item.FolioNumbers = String.Join("-", item.ServiceOrderDetailDistributions.Select(Function(o) o.FolioNumber).ToArray)
        Next
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles de la orden 
    ''' </summary>
    ''' <param name="serviceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListServiceOrderDetailByServiceOrderId(serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Dim session As New IndigoXPOSession(Of ServiceOrderDetailXpo)()
        Return New XPCollection(Of ServiceOrderDetailXpo)(session, CriteriaOperator.Parse($"[ServiceOrderId].Id = {serviceOrderId} And IsDelete = 0"))
    End Function

    ''' <summary>
    ''' Lista las facturas a entidades capitadas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceEntityCapitated() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceEntityCapitatedXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceEntityCapitatedXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista las facturas capita que se puedes relacionar para la factura previa RIPS
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <returns></returns>
    Public Function ListInvoiceEntityCapitatedToRIPSByCareGroupId(ByVal CareGroupId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceEntityCapitatedXpo)()
        Dim Criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = 2 and (InvoicePeriod IS NOT NULL AND InvoicePeriod <> 3) And CareGroupId.Id ='{CareGroupId}' And PreviousInvoices[Status IN (2,5)].Count =0 ")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceEntityCapitatedXpo)), Nothing, Criteria)
    End Function

    ''' <summary>
    ''' Lista las facturas a entidades capitadas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceEntityCapitatedDistributions() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceEntityCapitatedDistributionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceEntityCapitatedDistributionXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las autorizaciones de facturación por código de usuario
    ''' </summary>
    ''' <param name="userCode">The user code.</param>
    ''' <returns></returns>
    Public Function ListAllBillingAuthorizationByUserCode(ByVal userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingAuthorizationXpo)()
        Dim Criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = True And InvoiceType In (1,2,3) And Billing_BillingAuthorizationUsers[UserCode = '{userCode}']")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingAuthorizationXpo)),
                                                "Id;Code;Name;ResolutionNumber;ResolutionDate;InvoicePrefix;InitialInvoice;FinalInvoice;InvoiceType;Consecutive;Status;CodeName",
                                                Criteria)
    End Function

    ''' <summary>
    ''' funcion para cargar la informacion del PopUp de "importar" en facturacion basica
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewListBasicBillingForImportXpo(Optional Header As Boolean = True,
                                                        Optional SaleModality As Byte? = Nothing,
                                                        Optional CurrencyId As Integer? = Nothing,
                                                        Optional CustomerId As Integer? = Nothing) As List(Of ViewListBasicBillingForImportXpo)
        Dim session As New IndigoXPOSession(Of ViewListBasicBillingForImportXpo)()
        Dim CriteriaTmp = (String.Format(If(Header, "HeaderId IS NULL", "HeaderId IS NOT NULL ")))

        If SaleModality IsNot Nothing Then
            CriteriaTmp = CriteriaTmp & $"AND SaleModality = {SaleModality} "
        End If

        If CurrencyId IsNot Nothing Then
            CriteriaTmp = CriteriaTmp & $"AND CurrencyId = {CurrencyId} "
        End If

        If CustomerId IsNot Nothing Then
            CriteriaTmp = CriteriaTmp & $"AND CustomerId = {CustomerId}"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(CriteriaTmp)
        Dim _viewListBasicBillingForImportXpo As List(Of ViewListBasicBillingForImportXpo) = New XPCollection(Of ViewListBasicBillingForImportXpo)(session, criteria).ToList()
        Return _viewListBasicBillingForImportXpo
    End Function

    ''' <summary>
    ''' Lista las resoluciones de DS por usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListDocumentSupportAuthorizationByUser(ByVal userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingAuthorizationXpo)()
        Dim Criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = true And InvoiceType = 4 And Billing_BillingAuthorizationUsers[UserCode = '{userCode}']")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingAuthorizationXpo)), Nothing, Criteria)
    End Function

    ''' <summary>
    ''' Lists the type of the service order detail by admission number record.
    ''' </summary>
    Public Function ListServiceOrderDetailByAdmissionNumberRecordType(AdmissionNumber As String, recordType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CUPSEntityXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(CUPSEntityXpo)), "Id;Code;Description;RIPSCode;RIPSDescription;CodeDescription",
                                                          CriteriaOperator.Parse("Billing_ServiceOrderDetails[ServiceOrderId.AdmissionNumber = ?] And Billing_ServiceOrderDetails[RecordType = ?]", AdmissionNumber, recordType))
    End Function

    ''' <summary>
    ''' Lista todos los grupos de facturacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingGroupXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingGroupXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las boletas de salida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSlipOut() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SlipOutXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(SlipOutXpo)),
                                                          "Id;Code;DocumentDate;AdmissionNumber", Nothing)
    End Function

    ''' <summary>
    ''' Lista los registro de la tabla ProductServiceDetail por Orden de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductServiceDetail(ServiceOrderDetail As Integer) As XPCollection(Of ProductServiceDetailXpo)
        Dim criteria = CriteriaOperator.Parse($"ServiceOrderDetailId = {ServiceOrderDetail}")
        Dim session As New IndigoXPOSession(Of ProductServiceDetailXpo)()
        Return New XPCollection(Of ProductServiceDetailXpo)(session, criteria)
    End Function


    ''' <summary>
    ''' Lista todas las boletas de salida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSlipOutAndAdmissionNumber() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingVSlipOutAndAdmission)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingVSlipOutAndAdmission)),
                                                          "idSlipOut;codeSlipOut;dateSlipOut;AdmissionNumber;documentPacient;namePacient",
                                                          Nothing)
    End Function

    ''' <summary>
    ''' Lista todos las autorizaciones de facturación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingAuthotization() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingAuthorizationXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingAuthorizationXpo)),
                                                          "Id;Code;Name;Status;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos las condiciones de venta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConditionSales() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConditionSalesXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ConditionSalesXpo)),
                                                          "Id;Code;Name;Status;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los grupos de facturacion por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingGroupXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingGroupXpo)),
                                                          "Id;Code;Name;Status;CodeName",
                                                          CriteriaOperator.Parse($"Status={status}"))
    End Function

    ''' <summary>
    ''' Lista los grupos de facturacion por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingConceptByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingConceptXpo)),
                                                          "Id;Code;Name;Status;CodeName",
                                                          CriteriaOperator.Parse($"Status={status}"))
    End Function

    ''' <summary>
    ''' lista los conceptos de facturacion segun el tipo de servicio, en este caso "servicio primario"
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAssociatedMainServiceId(TypeService As Byte, Id As Integer, type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("TypeService = " & TypeService & "And Id <> " & Id & " And ConceptType=" & type)
        Dim classEntity = session.GetClassInfo(GetType(BillingConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los conceptos de facturacion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBillingConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListBillingConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewListBillingConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los conceptos de facturacion secundarios dependiendo del Id del principal
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAssociatedSecondaryService(IdMainService As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AssociatedMainServiceId = " & IdMainService)
        Dim classEntity = session.GetClassInfo(GetType(BillingConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las facturas por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceByAdmissionStatus(status As Integer, admissionNumber As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceXpo)),
                                                          Nothing,
                                                          CriteriaOperator.Parse($"AdmissionNumber = '{admissionNumber}' and Status = '{status}'"))
    End Function

    Public Function ListInvoiceByStatus(status As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceXpo)),
                                                          "Id;DocumentType;DocumentTypeName;InvoiceNumber;AdmissionNumber;PatientCode;InvoiceDate;TotalInvoice;NumberDocument;Status;FolioNumber",
                                                          CriteriaOperator.Parse($"Status={status}"))
    End Function

    Function ListInvoiceByStatusAndPatientCode(status As Integer, PatientCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(InvoiceXpo)),
                                                          "Id;DocumentType;DocumentTypeName;InvoiceNumber;AdmissionNumber;PatientCode;InvoiceDate;TotalInvoice;NumberDocument;Status;FolioNumber",
                                                          CriteriaOperator.Parse($"Status={status} And PatientCode = '{PatientCode}'"))
    End Function

    Function GetInvoiceByAdminssionNumber(admissionNumber As String) As InvoiceXpo
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return session.FindObject(Of InvoiceXpo)(CriteriaOperator.Parse($"AdmissionNumber='{admissionNumber}' and Status = 1 "))
    End Function

    Function ListInvoiceByAdminssionNumber(admissionNumber As String) As XPCollection(Of InvoiceXpo)
        Dim session As New IndigoXPOSession(Of InvoiceXpo)()
        Return New XPCollection(Of InvoiceXpo)(session, CriteriaOperator.Parse($"AdmissionNumber='{admissionNumber}' and Status = 1 "),
                                                   New SortProperty("OutputDate", SortingDirection.Descending))
        'End Using
    End Function

    ''' <summary>
    ''' Lista las facturas por numero de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewNoSurgicalByInvoiceId(InvoiceId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListNoSurgical)()
        Return New XPCollection(session, GetType(ViewListNoSurgical),
                                    CriteriaOperator.Parse($"InvoiceId={InvoiceId}"))
        'End Using
    End Function

    ''' <summary>
    ''' lista el detalle para el reporte Liquidar todo de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewPrintAllInvoiceDetailByInvoiceId(ListInvoiceId As List(Of Integer)) As XPCollection(Of BillingViewRadicatedInvoicReportXpo)
        Dim session As New IndigoXPOSession(Of BillingViewRadicatedInvoicReportXpo)()
        Return New XPCollection(Of BillingViewRadicatedInvoicReportXpo)(session, CriteriaOperator.Parse($"Id In ({String.Join(", ", CType(ListInvoiceId, List(Of Integer)))})"))
        'End Using
    End Function

    ''' <summary>
    ''' lista para el reporte de facturas ------------------------------------------------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceFilters(Filter As String) As XPCollection(Of InvoicesXpo)
        Dim session As New IndigoXPOSession(Of InvoicesXpo)()
        Return New XPCollection(Of InvoicesXpo)(session, CriteriaOperator.Parse(Filter))
        'End Using
    End Function

    ''' <summary>
    ''' lista el detalle para el reporte de facturas cargadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceDetailByInvoiceLoadId(InvoiceId As Integer) As XPCollection(Of BillingVReportInvoiceDetailLoad)
        Dim session As New IndigoXPOSession(Of BillingVReportInvoiceDetailLoad)()
        Dim collect As New XPCollection(Of BillingVReportInvoiceDetailLoad)(session, CriteriaOperator.Parse($"Id = {InvoiceId}"))
        collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
        ' End Using
    End Function

    ''' <summary>
    ''' lista el detalle para el reporte de facturas Anuladas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewInvoiceDetailAnulateByInvoiceId(InvoiceId As Integer) As XPCollection(Of BillingVReportInvoiceDetailAnulate)
        Dim session As New IndigoXPOSession(Of BillingVReportInvoiceDetailAnulate)()
        Dim collect As New XPCollection(Of BillingVReportInvoiceDetailAnulate)(session, CriteriaOperator.Parse($"Id = {InvoiceId}"))
        collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' Lista las facturas por numero de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewSurgicalAndPackageByInvoiceId(InvoiceId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListSurgicalAndPackageXpo)()
        Return New XPCollection(session, GetType(ViewListSurgicalAndPackageXpo), CriteriaOperator.Parse($"InvoiceId={InvoiceId}"))
        'End Using
    End Function

    Public Function ListRevenueControlCollection(id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListServiceOrderDetailXpo)()
        Return New XPCollection(session, GetType(ViewListServiceOrderDetailXpo),
                                    CriteriaOperator.Parse($"RevenueControlDetailId={id}"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que aún no se ha facturado
    ''' Lista los detalles de un folio o una facturado
    ''' </summary>
    ''' <param name="id">Id del RevenueControlDetail</param>
    Public Function ListRevenueControl(ByVal id As Integer, ByVal admissionNumber As String) As PLinqServerModeSource

        Dim session As New IndigoXPOSession(Of ViewListServiceOrderDetailXpo)()
        Dim tableView As New XPQuery(Of ViewListServiceOrderDetailXpo)(session)

        Dim b As New PLinqServerModeSource
        Dim listData = (From T1 In tableView Where T1.AdmissionNumber = admissionNumber Select T1).ToList()
        b.Source = (From d In listData
                    Where d.RevenueControlDetailId = id
                    Select d).ToList()
        Return b
    End Function

    ''' <summary>
    ''' Lists the revenue control by care group identifier.
    ''' </summary>
    ''' <param name="caregroupId">The caregroup identifier.</param>
    ''' <returns></returns>
    Public Function ListRevenueControlByCareGroupId(ByVal caregroupId As Integer, operativeUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListRecognitionDetailXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewListRecognitionDetailXpo)),
                                               Nothing,
                                               CriteriaOperator.Parse("Status IN (1,3) And CareGroupId= ? And OperativeUnitId = ?", caregroupId, operativeUnitId))
    End Function

    Public Function ListRevenueControl() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewRevenueControl)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewRevenueControl)), Nothing, Nothing)
    End Function

    Public Function ListRevenueControlActive(Optional status As String = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewRevenueControl)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewRevenueControl)), Nothing, CriteriaOperator.Parse("IESTADOIN = ''", status))
    End Function

    Public Function ListReverseRecognitionDetail(ByVal revenueRecognitionId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListReverseRecognitionDetailXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewListReverseRecognitionDetailXpo)), Nothing,
                                               CriteriaOperator.Parse("RecognitionState = ? And RevenueRecognitionId = ?", 1, revenueRecognitionId))
    End Function

    ''' <summary>
    ''' Lista los motivos de anulación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnulmentReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingReversalReasonXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingReversalReasonXpo)), Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que ya ha sido anulado previamente
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRevenueControlForAnnullateInvoice(ByVal invoiceId As Integer) As PLinqServerModeSource
        Dim session As New IndigoXPOSession(Of ViewListRevenueControlForAnnullateInvoiceXpo)
        Dim tableView As New XPQuery(Of ViewListRevenueControlForAnnullateInvoiceXpo)(session)
        Dim b As New PLinqServerModeSource
        b.Source = (From T1 In tableView Where T1.InvoiceId = invoiceId Select T1).ToList()
        Return b
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que ya ha sido anulado previamente
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBasicBillings() As XPInstantFeedbackSource
        Using session = New IndigoXPOSession(Of BasicBillingXpo)
            Dim classEntity = session.GetClassInfo(GetType(BasicBillingXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;CustomerId.NitName;InvoiceId.InvoiceNumber;Status;StatusName", Nothing)
            serverMode.DefaultSorting = "Code"
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista los registros de tarifas de productos y servicios
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductAndServicesFee() As XPInstantFeedbackSource
        Using session = New IndigoXPOSession(Of ProductAndServicesFeeXpo)
            Dim classEntity = session.GetClassInfo(GetType(ProductAndServicesFeeXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName", Nothing)
            serverMode.DefaultSorting = "Code"
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista los registros de tarifas de productos y servicios activos para los usuarios autorizados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductAndServicesFeeByStatus(Status As Boolean, userCode As String) As XPInstantFeedbackSource
        Using session = New IndigoXPOSession(Of ProductAndServicesFeeXpo)
            Dim classEntity = session.GetClassInfo(GetType(ProductAndServicesFeeXpo))
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {Status} AND ProductAndServiceFeeUserDetailXpo[UserCode = '{userCode}']")
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName", criteria)
            serverMode.DefaultSorting = "Code"
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista el detalle de tipo producto de un registro de tarifas de productos y servicios
    ''' </summary>
    ''' <returns></returns>
    Public Function GeProductsByProductAndServiceId(ProductAndServiceFeeId As Integer) As XPCollection(Of ProductFeeDetailXpo)
        Dim session As New IndigoXPOSession(Of ProductFeeDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ProductAndServiceFeeId = {ProductAndServiceFeeId}")
        Return New XPCollection(Of ProductFeeDetailXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista el detalle de tipo producto de un registro de tarifas de productos y servicios
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceFeeDetail(ProductAndServiceFeeId As Integer) As XPInstantFeedbackSource
        Using session = New IndigoXPOSession(Of ServiceFeeDetailXpo)
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ProductAndServiceFeeId = {ProductAndServiceFeeId}")
            Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ServiceFeeDetailXpo)), Nothing, criteria)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las los documentos electronicos de tipo factura
    ''' </summary>
    ''' <param name="operatingUnitId">Operating Unit.</param>
    ''' <param name="status">Status.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeInvoices(operatingUnitId As Integer, status As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicDocumentXpo)()
        Dim strCriteria = "DocumentType In (1, 2, 3, 4, 5, 6, 7) And OperatingUnitId = " & operatingUnitId
        If Not String.IsNullOrEmpty(status) Then
            strCriteria = strCriteria & " And Status IN (" & status & ")"
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicDocumentXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los documentos electronicos de tipo factura
    ''' </summary>
    ''' <param name="operatingUnitId">Operating Unit.</param>
    ''' <param name="status">Status.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeDebitNotes(operatingUnitId As Integer, status As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicDocumentXpo)()
        Dim strCriteria = "DocumentType In (92, 94, 98) And OperatingUnitId = " & operatingUnitId
        If Not String.IsNullOrEmpty(status) Then
            strCriteria = strCriteria & " And Status IN (" & status & ")"
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicDocumentXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los documentos electronicos de tipo factura
    ''' </summary>
    ''' <param name="operatingUnitId">Operating Unit.</param>
    ''' <param name="status">Status.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeCreditNotes(operatingUnitId As Integer, status As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicDocumentXpo)()
        Dim strCriteria = "DocumentType In (91, 93, 99) And OperatingUnitId = " & operatingUnitId
        If Not String.IsNullOrEmpty(status) Then
            strCriteria = strCriteria & " And Status IN (" & status & ")"
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicDocumentXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <param name="electronicDocumentId">Electronic Document Id.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentDetails(electronicDocumentId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicDocumentDetailXpo)()
        Dim strCriteria = "ElectronicDocumentId.Id = " & electronicDocumentId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicDocumentDetailXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <param name="electronicDocumentId">Electronic Document Id.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentNotifications(electronicDocumentId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicDocumentNotificationXpo)()
        Dim strCriteria = "ElectronicDocumentId.Id = " & electronicDocumentId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicDocumentNotificationXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos las los documentos electronicos de tipo documento soporte
    ''' </summary>
    ''' <param name="operatingUnitId">Operating Unit.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeSupportDocument(documentType As String, operatingUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SupportDocumentXpo)()
        Dim strCriteria = $"DocumentType = '{documentType}' And OperativeUnitId = {operatingUnitId}"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(SupportDocumentXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos las los documentos electronicos de tipo documento soporte
    ''' </summary>
    ''' <param name="operatingUnitId">Operating Unit.</param>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeNote(operatingUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SupportDocumentXpo)()
        Dim strCriteria = "DocumentType = 'ElectronicSupportDocumentAdjustmentNote' And OperativeUnitId = " & operatingUnitId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(SupportDocumentXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDocumentSupportDetails(DocumentSupportId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentSupportDetailXpo)()
        Dim strCriteria = "ElectronicSupportDocumentId.Id = " & DocumentSupportId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentSupportDetailXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupportDocumentAdjustmentNoteDetail(NoteAdjustmentId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicSupportDocumentAdjustmentNoteDetailXpo)()
        Dim strCriteria = "ElectronicSupportDocumentAdjustmentNoteId.Id = " & NoteAdjustmentId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicSupportDocumentAdjustmentNoteDetailXpo)), Nothing, criteria)
    End Function


    Public Function GetJustificationByUserCode(UserCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingJustificationControlXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"BillingJustificationControlUser[UserCode = '{UserCode}']")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BillingJustificationControlXpo)), Nothing, criteria)
    End Function

    Public Function GetElectronicSupportDocumentByDocumentOrigin(EntityId As Integer, EntityName As String) As XPCollection(Of ElectronicSupportDocumentXpo)
        Dim session As New IndigoXPOSession(Of ElectronicSupportDocumentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"EntityId = {EntityId} And EntityName = '{EntityName}'")
        Return New XPCollection(Of ElectronicSupportDocumentXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Obtiene los registros para trazabilidad de rips electronicos ya sean facturas, notas debito/credito o notas de ajuste
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    Public Function ListElectronicsRIPSTraceability(operatingUnitId As Integer, entityName As String)
        Dim session As New IndigoXPOSession(Of ElectronicRIPSTraceability)()
        Dim strCriteria = $"EntityName = '{entityName}' and OperativeUnitId = {operatingUnitId} "
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicRIPSTraceability)), Nothing, criteria)
    End Function

    Public Function ListElectronicsRIPSTraceabilityInvoiceTap(operatingUnitId As Integer)
        Dim session As New IndigoXPOSession(Of ElectronicRIPSTraceability)()
        Dim strCriteria = $"EntityName IN ('Invoice','InvoiceEntityCapitated') and OperativeUnitId = {operatingUnitId} "
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicRIPSTraceability)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Obtiene los detalles del erips seleccionado
    ''' </summary>
    ''' <param name="electronicRIPSId"></param>
    ''' <returns></returns>
    Public Function ListElectronicRIPSDetails(electronicRIPSId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicRIPSDetailTraceability)()
        Dim strCriteria = "ElectronicsRIPSId = " & electronicRIPSId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicRIPSDetailTraceability)), Nothing, criteria)
    End Function

    Public Function ListElectronicRIPSDetailsRules(electronicRIPSId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicRIPSDetailTraceability)()
        Dim strCriteria = "ElectronicsRIPSId = " & electronicRIPSId & " AND (TypeMessage IS NULL OR TypeMessage = 'RECHAZADO')"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicRIPSDetailTraceability)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllRIPSSupportRecords() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RIPSSupportRecordXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(RIPSSupportRecordXpo)), Nothing, Nothing)
    End Function

#Region "ListHealthAdministrator"

    ''' <summary>
    ''' Lista de grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _LCareGroup As List(Of Integer)
    ' ''' <summary>
    ' ''' Objeto de consulta
    ' ''' </summary>
    'Private WithEvents linqListCategory As LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista las categorias de facturacion de una lista de grupos de atencion
    ''' </summary>
    Public Function ListCategory(lCareGroup As List(Of Integer)) As LinqInstantFeedbackSource
        Dim linqListCategory As New LinqInstantFeedbackSource
        AddHandler linqListCategory.GetQueryable, AddressOf linqListCategory_GetQueryable
        linqListCategory.KeyExpression = "Id"
        _LCareGroup = lCareGroup
        Return linqListCategory
    End Function
    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub linqListCategory_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCategory As New XPQuery(Of InvoiceCategoriesXpo)(session)
            Dim tableInter As New XPQuery(Of CareGroupInvoiceCategoriesXpo)(session)
            Dim TmpQueryableSource = From c In tableCategory
                                     Where tableInter.Where(Function(t) _LCareGroup.Contains(t.CareGroupId)).Any(Function(t) t.InvoiceCategoriesId.Id = c.Id)
                                     Select c
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCategory
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region "Informes"

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
    ''' Función para obtener una lista de datos de una entidad XPO y unir dos listas enviándole la session de la primera, así no abra conflicto de session diferentes al unirlas
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionUnionXpo(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing, Optional session As Session = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria, session)
        result.Sort()
        Return result
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class