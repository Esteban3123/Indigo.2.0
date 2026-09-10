'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 14-06-2013
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity

Public Class InvoiceDetailRepository
    Inherits GenericRepository(Of GlosaInvoiceDetail)
    Implements IInvoiceDetailRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="ObjectionsReceptionCRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Funcion para cargar los detalle de factura sin agregados
    ''' </summary>
    ''' <param name="InvoiceNumber">id del detalle, es decir de la factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Public Function ListGlosaInvoiceDetailwithoutAggregates(InvoiceNumber As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailwithoutAggregates
        Dim Busqueda = (From e In _context.GlosaInvoiceDetail.Include("GlosaInvoiceDetailQX")
                        Select e
                        Where e.InvoiceNumber = InvoiceNumber).ToList
        Return Busqueda
    End Function


    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura con quirurgicos
    ''' </summary>
    ''' <param name="InvoiceNumber">id del detalle, es decir de la factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Public Function ListGlosaInvoiceDetail(InvoiceNumber As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetail
        Dim Busqueda = (From e In _context.GlosaInvoiceDetail
                        Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                        From m In Group.DefaultIfEmpty()
                        Where e.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative)
                        Select e).Include("GlosaMovementGlosa").Include("GlosaInvoiceDetailQX").ToList()
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' Funcion para cargar los detalles de factura con movimientos y pago parciales
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGlosaInvoiceDetailWithMovementAndPartialPayments(InvoiceNumber As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailWithMovementAndPartialPayments
        Dim Busqueda = (From e In _context.GlosaInvoiceDetail
                        Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                        From m In Group.DefaultIfEmpty()
                        Where e.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative) And e.GlosaMovementGlosa.Any()
                        Select e).Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.PartialPaymentsMovement").ToList
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' Funcion para cargar un detalle de Factura
    ''' </summary>
    ''' <param name="Id">Codigo detalle de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaInvoiceDetail(Id As String) As GlosaInvoiceDetail Implements IInvoiceDetailRepository.GetGlosaInvoiceDetail
        Dim Busqueda = (From e In _context.GlosaInvoiceDetail
                        Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                        From m In Group.DefaultIfEmpty()
                        Where e.Id = Id AndAlso (m Is Nothing OrElse m.IsNormative)
                        Select e).Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.ConceptGlosas").Include("GlosaInvoiceDetailQX").Distinct()

        If Busqueda?.Any() Then
            Dim item = TryCast(Busqueda.SingleOrDefault, GlosaInvoiceDetail)
            item.OriginalValue = (From e In _context.GlosaInvoiceDetail.AsNoTracking.Include("GlosaMovementGlosa").AsNoTracking.Include("GlosaMovementGlosa.ConceptGlosas").AsNoTracking.Include("GlosaInvoiceDetailQX").AsNoTracking
                                  Where e.Id = Id
                                  Select e).SingleOrDefault
            item.MovimientoAux = New GlosaMovementGlosa()
            item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
            item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
            If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
            End If
            item.BillerCodeName = item.BillerCode + " - " + item.BillerName
            item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
            item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName
            For Each itemQx In item.GlosaInvoiceDetailQX

                itemQx.MovimientoAux = New GlosaMovementGlosa()
                itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea
                If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                    itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                End If
                itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName
                For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                    If itemmov.ConceptGlosas IsNot Nothing Then
                        itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                    End If
                    If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                        itemQx.ValueGlosadoFacade = itemmov.ValueGlosado
                        If itemmov.ValueReiterated IsNot Nothing Then
                            itemQx.ValueReiteratedFacade = itemmov.ValueReiterated
                        End If
                    End If
                Next
            Next

            If item.GlosaMovementGlosa IsNot Nothing Then
                For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                    If itemmov.ConceptGlosas IsNot Nothing Then
                        itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                    End If
                    If itemmov.MainGlosa = True Then
                        item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                        If itemmov.ValueReiterated IsNot Nothing Then
                            item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                        End If
                    End If
                Next
            End If
            Return item
        Else
            Return New GlosaInvoiceDetail()
        End If

    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ValidateInvoices(InvoiceNumber As String) As Boolean Implements IInvoiceDetailRepository.ValidateInvoices

        Dim consulta = (From d In _context.GlosaInvoiceDetail
                        Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals d.Id Into Group
                        From m In Group.DefaultIfEmpty()
                        Where d.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative) And d.GlosaMovementGlosa.Any(Function(a) ((a.State = "1") Or (a.State = "3")) And a.MainGlosa = True) = True
                        Select total = d.GlosaMovementGlosa.Count).Include("GlosaMovementGlosa").ToList

        Dim control As Integer = 0
        For Each item In consulta
            If item > 0 Then
                control = 1
            End If
        Next
        If control > 0 Then
            Return False
        Else
            Return True
        End If
    End Function


    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumber
        DirectCast(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600

        Dim _invoiceDetail As InvoiceDetail = Nothing
        Dim ListInvoiceDetail As List(Of GlosaInvoiceDetail) = Nothing

        If Modulo = "RA" Then
            ListInvoiceDetail = (From e In _context.GlosaInvoiceDetail
                                 Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                                 From m In Group.DefaultIfEmpty()
                                 Where e.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative)
                                 Select e).Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.ConceptGlosas").Include("GlosaInvoiceDetailQX").ToList()

            ListInvoiceDetail = ListInvoiceDetail.GroupBy(Function(x) New With {
                                                     Key .AccountantAccountIncome = x.AccountantAccountIncome, Key .Ammount = x.Ammount, Key .BillerCode = x.BillerCode,
                                                     Key .BillerCodeName = x.BillerCodeName, Key .BillerName = x.BillerName, Key .BillingGroup = x.BillingGroup, Key .BillingGroupCode = x.BillingGroupCode,
                                                     Key .BillingGroupCodeName = x.BillingGroupCodeName, Key .CostCenterCode = x.CostCenterCode, Key .CostCenterCodeName = x.CostCenterCodeName,
                                                     Key .CostCenterName = x.CostCenterName, Key .CustomProperties = x.CustomProperties, Key .DescriptionServiceArea = x.DescriptionServiceArea, Key .GlosaInvoiceDetailQX = x.GlosaInvoiceDetailQX,
                                                     Key .GlosaMovementGlosa = x.GlosaMovementGlosa, Key .GlosaObjectionsReceptionD = x.GlosaObjectionsReceptionD, Key .Id = x.Id, Key .InvoiceNumber = x.InvoiceNumber, Key .InvoiceDetailNativeId = x.InvoiceDetailNativeId,
                                                     Key .InvoicedValue = x.InvoicedValue, Key .ListMovimientoAux = x.ListMovimientoAux, Key .MedicalCode = x.MedicalCode, Key .MedicalCodeName = x.MedicalCodeName, Key .MedicalName = x.MedicalName, Key .MovimientoAux = x.MovimientoAux,
                                                     Key .ObjectionsReceptionDId = x.ObjectionsReceptionDId, Key .OriginalValue = x.OriginalValue, Key .ServiceAreaCode = x.ServiceAreaCode, Key .ServiceAreaCodeName = x.ServiceAreaCodeName, Key .ServiceDate = x.ServiceDate,
                                                     Key .ServiceName = x.ServiceName, Key .ServiceOrderDetailId = x.ServiceOrderDetailId, Key .StateRecord = x.StateRecord, Key .TimeStamp = x.TimeStamp, Key .TypeProcedure = x.TypeProcedure, Key .TypeServiceProduct = x.TypeServiceProduct,
                                                     Key .UnitValue = x.UnitValue, Key .ValorPaciente = x.ValorPaciente, Key .ValueAcceptedEAPBconciliationFacade = x.ValueAcceptedEAPBconciliationFacade, Key .ValueAcceptedFirstInstanceadoFacade = x.ValueAcceptedFirstInstanceadoFacade,
                                                     Key .ValueAcceptedIPSconciliationFacade = x.ValueAcceptedIPSconciliationFacade, Key .ValueAcceptedSecondInstanceFacade = x.ValueAcceptedSecondInstanceFacade, Key .ValueGlosadoFacade = x.ValueGlosadoFacade, Key .ServiceCode = x.ServiceCode,
                                                     Key .ValuePendingConciliationFacade = x.ValuePendingConciliationFacade, Key .ValuePendingConciliationTmpFacade = x.ValuePendingConciliationTmpFacade, Key .ValueReiteratedFacade = x.ValueReiteratedFacade, Key .ValueServiceManual = x.ValueServiceManual}) _
                    .Select(Function(s) New GlosaInvoiceDetail With {.AccountantAccountIncome = s.Key.AccountantAccountIncome, .Ammount = s.Key.Ammount, .BillerCode = s.Key.BillerCode,
                                                      .BillerCodeName = s.Key.BillerCodeName, .BillerName = s.Key.BillerName, .BillingGroup = s.Key.BillingGroup, .BillingGroupCode = s.Key.BillingGroupCode,
                                                      .BillingGroupCodeName = s.Key.BillingGroupCodeName, .CostCenterCode = s.Key.CostCenterCode, .CostCenterCodeName = s.Key.CostCenterCodeName,
                                                      .CostCenterName = s.Key.CostCenterName, .CustomProperties = s.Key.CustomProperties, .DescriptionServiceArea = s.Key.DescriptionServiceArea, .GlosaInvoiceDetailQX = s.Key.GlosaInvoiceDetailQX,
                                                      .GlosaMovementGlosa = s.Key.GlosaMovementGlosa, .GlosaObjectionsReceptionD = s.Key.GlosaObjectionsReceptionD, .Id = s.Key.Id, .InvoiceNumber = s.Key.InvoiceNumber, .InvoiceDetailNativeId = s.Key.InvoiceDetailNativeId,
                                                      .InvoicedValue = s.Key.InvoicedValue, .ListMovimientoAux = s.Key.ListMovimientoAux, .MedicalCode = s.Key.MedicalCode, .MedicalCodeName = s.Key.MedicalCodeName, .MedicalName = s.Key.MedicalName, .MovimientoAux = s.Key.MovimientoAux,
                                                      .ObjectionsReceptionDId = s.Key.ObjectionsReceptionDId, .OriginalValue = s.Key.OriginalValue, .ServiceAreaCode = s.Key.ServiceAreaCode, .ServiceAreaCodeName = s.Key.ServiceAreaCodeName, .ServiceDate = s.Key.ServiceDate,
                                                      .ServiceName = s.Key.ServiceName, .ServiceOrderDetailId = s.Key.ServiceOrderDetailId, .StateRecord = s.Key.StateRecord, .TimeStamp = s.Key.TimeStamp, .TypeProcedure = s.Key.TypeProcedure, .TypeServiceProduct = s.Key.TypeServiceProduct,
                                                      .UnitValue = s.Key.UnitValue, .ValorPaciente = s.Key.ValorPaciente, .ValueAcceptedEAPBconciliationFacade = s.Key.ValueAcceptedEAPBconciliationFacade, .ValueAcceptedFirstInstanceadoFacade = s.Key.ValueAcceptedFirstInstanceadoFacade,
                                                      .ValueAcceptedIPSconciliationFacade = s.Key.ValueAcceptedIPSconciliationFacade, .ValueAcceptedSecondInstanceFacade = s.Key.ValueAcceptedSecondInstanceFacade, .ValueGlosadoFacade = s.Key.ValueGlosadoFacade, .ServiceCode = s.Key.ServiceCode,
                                                      .ValuePendingConciliationFacade = s.Key.ValuePendingConciliationFacade, .ValuePendingConciliationTmpFacade = s.Key.ValuePendingConciliationTmpFacade, .ValueReiteratedFacade = s.Key.ValueReiteratedFacade, .ValueServiceManual = s.Key.ValueServiceManual}).ToList()



            Dim ListResponsibles = (From r In _context.Responsible.AsNoTracking).ToList()

            For Each item In ListInvoiceDetail
                item.MovimientoAux = New GlosaMovementGlosa()
                item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
                item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
                If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                    item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
                End If
                item.BillerCodeName = item.BillerCode + " - " + item.BillerName
                item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
                item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName

                If item.GlosaInvoiceDetailQX IsNot Nothing Then
                    For Each itemQx In item.GlosaInvoiceDetailQX
                        itemQx.MovimientoAux = New GlosaMovementGlosa()
                        itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                        itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea
                        If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                            itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                        End If
                        itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName

                        For Each itemmov As GlosaMovementGlosa In itemQx.GlosaMovementGlosa
                            If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                                If itemmov.ConceptGlosas IsNot Nothing Then
                                    itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                                End If
                                itemQx.ValueGlosadoFacade = itemQx.ValueGlosadoFacade + itemmov.ValueGlosado
                                If itemmov.ValueReiterated IsNot Nothing Then
                                    itemQx.ValueReiteratedFacade = itemQx.ValueReiteratedFacade + itemmov.ValueReiterated
                                End If
                            End If
                        Next
                    Next
                End If

                If item.GlosaMovementGlosa IsNot Nothing Then
                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        Dim responsible = ListResponsibles.Where(Function(r) r.Id = itemmov.ResponsibleId).FirstOrDefault()
                        If responsible IsNot Nothing Then
                            itemmov.ResponsibleCodeNameGlosa = responsible.Code + " - " + responsible.Name
                        End If
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        If itemmov.MainGlosa = True Then
                            item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                            End If
                        End If
                        If itemmov.ResponsibleThirdPartyId IsNot Nothing Then
                            Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = itemmov.ResponsibleThirdPartyId Select New With {tp.Nit, tp.Name}).FirstOrDefault()
                            itemmov.ResponsibleThirdPartyNitName = String.Concat(thirdParty?.Nit, " - ", thirdParty?.Name)
                        End If
                    Next
                End If
                _invoiceDetail = Nothing
                If item.InvoiceDetailNativeId IsNot Nothing Then
                    _invoiceDetail = (From id In _context.InvoiceDetail.AsNoTracking().Include("Invoice").AsNoTracking() Where id.Id = item.InvoiceDetailNativeId AndAlso id.Invoice.InvoiceNumber = item.InvoiceNumber Select id).FirstOrDefault
                End If
                If _invoiceDetail IsNot Nothing Then
                    item.ValorEntidad = _invoiceDetail.ThirdPartySalesPrice
                    item.ValorPaciente = _invoiceDetail.SubTotalPatientSalesPrice
                Else
                    item.ValorEntidad = item.InvoicedValue
                End If
            Next
        ElseIf Modulo = "CON" Then
            ListInvoiceDetail = Query(Function(m) m.InvoiceNumber = InvoiceNumber AndAlso m.GlosaMovementGlosa.Any(), includes:={"GlosaMovementGlosa", "GlosaInvoiceDetailQX"}).ToList()
            Dim ids = ListInvoiceDetail.Select(Function(m) m.Id)?.ToList()
            _context.GlosaMovementGlosa.Include("ConceptGlosas").Include("PartialPaymentsMovement").Include("GlosaMovementGlosaConciliation") _
                .Where(Function(m) ids.Contains(m.InvoiceDetailId) AndAlso m.IsNormative) _
                .ToList()

            For Each item In ListInvoiceDetail
                item.MovimientoAux = New GlosaMovementGlosa()
                item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
                item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
                If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                    item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
                End If
                item.BillerCodeName = item.BillerCode + " - " + item.BillerName
                item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
                item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName

                item.ValueAcceptedIPSconciliationFacade = item.CalculateAcceptedIPSConciliation()
                item.ValueAcceptedEAPBconciliationFacade = item.CalculateAcceptedEAPBConciliation()
                item.ValuePendingConciliationFacade = item.CalculateValuePending(1)
                item.ValuePendingConciliationTmpFacade = item.CalculateValuePending(2)

                If item.GlosaInvoiceDetailQX IsNot Nothing AndAlso item.GlosaInvoiceDetailQX.Count > 0 Then
                    For Each itemQx In item.GlosaInvoiceDetailQX
                        itemQx.ValueAcceptedIPSconciliationFacade = itemQx.CalculateAcceptedIPSConciliation()
                        itemQx.ValueAcceptedEAPBconciliationFacade = itemQx.CalculateAcceptedEAPBConciliation()
                        itemQx.ValuePendingConciliationFacade = itemQx.CalculateValuePending(False)
                        itemQx.ValuePendingConciliationTmpFacade = itemQx.CalculateValuePending(True)

                        itemQx.MovimientoAux = New GlosaMovementGlosa()
                        itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                        itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea
                        If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                            itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                        End If
                        itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName
                        For Each itemmov As GlosaMovementGlosa In itemQx.GlosaMovementGlosa
                            itemmov.ValPendingIPSconciliation = itemmov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = conciliationId).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                            itemmov.ValPendingEAPBconciliation = itemmov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = conciliationId).Sum(Function(d) d.ValueAcceptedEAPBconciliation)
                            itemmov.ValPendingConciliation = If(itemmov.ValuePendingConciliation IsNot Nothing, itemmov.ValuePendingConciliation, 0) - (itemmov.ValPendingIPSconciliation + itemmov.ValPendingEAPBconciliation)
                            If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                                If itemmov.ConceptGlosas IsNot Nothing Then
                                    itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                                End If
                                itemQx.ValueGlosadoFacade = itemQx.ValueGlosadoFacade + itemmov.ValueGlosado
                                If itemmov.ValueReiterated IsNot Nothing Then
                                    itemQx.ValueReiteratedFacade = itemQx.ValueReiteratedFacade + itemmov.ValueReiterated
                                End If
                            End If
                        Next
                    Next
                End If

                If item.GlosaMovementGlosa IsNot Nothing Then
                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        itemmov.ValPendingIPSconciliation = itemmov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = conciliationId).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                        itemmov.ValPendingEAPBconciliation = itemmov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = conciliationId).Sum(Function(d) d.ValueAcceptedEAPBconciliation)
                        itemmov.ValPendingConciliation = If(itemmov.ValuePendingConciliation IsNot Nothing, itemmov.ValuePendingConciliation, 0) - (itemmov.ValPendingIPSconciliation + itemmov.ValPendingEAPBconciliation)
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        If itemmov.MainGlosa = True Then
                            item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                            End If
                        End If
                    Next
                End If
                _invoiceDetail = Nothing
                If item.InvoiceDetailNativeId IsNot Nothing Then
                    _invoiceDetail = (From id In _context.InvoiceDetail.AsNoTracking().Include("Invoice").AsNoTracking() Where id.Id = item.InvoiceDetailNativeId AndAlso id.Invoice.InvoiceNumber = item.InvoiceNumber Select id).FirstOrDefault
                End If
                If _invoiceDetail IsNot Nothing Then
                    item.ValorEntidad = _invoiceDetail.ThirdPartySalesPrice
                    item.ValorPaciente = _invoiceDetail.SubTotalPatientSalesPrice
                Else
                    item.ValorEntidad = item.InvoicedValue
                End If
            Next
        End If

        Return ListInvoiceDetail
    End Function

    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, ByVal Modulo As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements
        Dim invoiceDetail = (From e In _context.GlosaInvoiceDetail
                             Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                             From m In Group.DefaultIfEmpty()
                             Where e.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative) And Not e.GlosaMovementGlosa.Any()
                             Select e) _
                                 .Include("GlosaMovementGlosa.ConceptGlosas") _
                                 .Include("GlosaInvoiceDetailQX")

        Dim ListInvoiceDetail = invoiceDetail.ToList()
        Dim _invoiceDetail = Nothing
        For Each item In ListInvoiceDetail
            item.MovimientoAux = New GlosaMovementGlosa()
            item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
            item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
            If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
            End If
            item.BillerCodeName = item.BillerCode + " - " + item.BillerName
            item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
            item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName

            If item.GlosaInvoiceDetailQX IsNot Nothing AndAlso item.GlosaInvoiceDetailQX.Count > 0 Then
                For Each itemQx In item.GlosaInvoiceDetailQX
                    itemQx.MovimientoAux = New GlosaMovementGlosa()
                    itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                    itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea

                    If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                        itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                    End If
                    itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName

                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa.Where(Function(m) m.IsNormative).ToList()
                        itemQx.ValueAcceptedFirstInstanceadoFacade = itemQx.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                            itemQx.ValueGlosadoFacade = itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                itemQx.ValueReiteratedFacade = itemmov.ValueReiterated
                            End If
                        End If
                    Next
                Next
            Else
                If item.GlosaMovementGlosa IsNot Nothing Then
                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        item.ValueAcceptedFirstInstanceadoFacade = item.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.MainGlosa = True Then
                            item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                            End If
                        End If
                    Next
                End If
            End If
            _invoiceDetail = Nothing
            If item.InvoiceDetailNativeId IsNot Nothing Then
                _invoiceDetail = (From id In _context.InvoiceDetail.AsNoTracking().Include("Invoice").AsNoTracking() Where id.Id = item.InvoiceDetailNativeId AndAlso id.Invoice.InvoiceNumber = item.InvoiceNumber Select id).FirstOrDefault
            End If
            If _invoiceDetail IsNot Nothing Then
                item.ValorEntidad = _invoiceDetail.ThirdPartySalesPrice
                item.ValorPaciente = _invoiceDetail.SubTotalPatientSalesPrice
            Else
                item.ValorEntidad = item.InvoicedValue
            End If
        Next

        Return ListInvoiceDetail
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, ByVal Modulo As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative
        Dim ListInvoiceDetail = (From e In _context.GlosaInvoiceDetail
                                 Join m In _context.GlosaMovementGlosa On m.InvoiceDetailId Equals e.Id
                                 Where e.InvoiceNumber = InvoiceNumber AndAlso Not m.IsNormative
                                 Select e).Include("GlosaMovementGlosa.ConceptGlosas").Include("GlosaInvoiceDetailQX") _
                                 .ToList()

        Dim _invoiceDetail = Nothing

        For Each item In ListInvoiceDetail
            item.MovimientoAux = New GlosaMovementGlosa()
            item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
            item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
            If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
            End If
            item.BillerCodeName = item.BillerCode + " - " + item.BillerName
            item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
            item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName

            If item.GlosaInvoiceDetailQX IsNot Nothing AndAlso item.GlosaInvoiceDetailQX.Count > 0 Then
                For Each itemQx In item.GlosaInvoiceDetailQX
                    itemQx.MovimientoAux = New GlosaMovementGlosa()
                    itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                    itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea

                    If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                        itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                    End If
                    itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName

                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa.Where(Function(m) Not m.IsNormative).ToList()
                        itemQx.ValueAcceptedFirstInstanceadoFacade = itemQx.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                            itemQx.ValueGlosadoFacade = itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                itemQx.ValueReiteratedFacade = itemmov.ValueReiterated
                            End If
                        End If
                    Next

                    For Each detailQxMovement In itemQx.GlosaMovementGlosa
                        If detailQxMovement.ConceptGlosas?.ConceptCodeName Is Nothing Then
                            Dim concept = _context.ConceptGlosas.FirstOrDefault(Function(m) m.Id = detailQxMovement.CodeGlosaId)
                            detailQxMovement.ConceptGlosas.ConceptCodeName = concept.Code & " - " & concept.NameSpecific
                        End If
                    Next
                Next
            Else
                If item.GlosaMovementGlosa IsNot Nothing AndAlso item.GlosaMovementGlosa.Any() Then
                    item.MovimientoAux.CodeGlosaId = item.GlosaMovementGlosa(0).CodeGlosaId
                    item.MovimientoAux.ResponsibleId = item.GlosaMovementGlosa(0).ResponsibleId
                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        item.ValueAcceptedFirstInstanceadoFacade = item.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.MainGlosa = True Then
                            item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                            End If
                        End If
                    Next
                End If
            End If

            _invoiceDetail = Nothing

            If item.InvoiceDetailNativeId IsNot Nothing Then
                _invoiceDetail = (From id In _context.InvoiceDetail.AsNoTracking().Include("Invoice").AsNoTracking() Where id.Id = item.InvoiceDetailNativeId AndAlso id.Invoice.InvoiceNumber = item.InvoiceNumber Select id).FirstOrDefault
            End If
            If _invoiceDetail IsNot Nothing Then
                item.ValorEntidad = _invoiceDetail.ThirdPartySalesPrice
                item.ValorPaciente = _invoiceDetail.SubTotalPatientSalesPrice
            Else
                item.ValorEntidad = item.InvoicedValue
            End If
        Next

        Return ListInvoiceDetail
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, ByVal Modulo As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiteration
        Dim ListInvoiceDetail = (From e In _context.GlosaInvoiceDetail.Include("GlosaMovementGlosa.ConceptGlosas").Include("GlosaInvoiceDetailQX")
                                 Where e.InvoiceNumber = InvoiceNumber AndAlso e.GlosaMovementGlosa.Any(Function(o) o.IsNormative)
                                 Select e).ToList()

        Dim _invoiceDetail = Nothing

        For Each item In ListInvoiceDetail
            item.MovimientoAux = New GlosaMovementGlosa()
            item.ServiceCodeName = item.ServiceCode + " - " + item.ServiceName
            item.ServiceAreaCodeName = item.ServiceAreaCode + " - " + item.DescriptionServiceArea
            If item.MedicalCode <> String.Empty And item.MedicalName <> String.Empty Then
                item.MedicalCodeName = item.MedicalCode + " - " + item.MedicalName
            End If
            item.BillerCodeName = item.BillerCode + " - " + item.BillerName
            item.BillingGroupCodeName = item.BillingGroupCode + " - " + item.BillingGroup
            item.CostCenterCodeName = item.CostCenterCode + " - " + item.CostCenterName

            If item.GlosaInvoiceDetailQX IsNot Nothing AndAlso item.GlosaInvoiceDetailQX.Count > 0 Then
                For Each itemQx In item.GlosaInvoiceDetailQX
                    itemQx.MovimientoAux = New GlosaMovementGlosa()
                    itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                    itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea

                    If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                        itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                    End If
                    itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName

                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa.Where(Function(m) m.IsNormative).ToList()
                        itemQx.ValueAcceptedFirstInstanceadoFacade = itemQx.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                            itemQx.ValueGlosadoFacade = itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                itemQx.ValueReiteratedFacade = itemmov.ValueReiterated
                            End If
                        End If
                    Next

                    For Each detailQxMovement In itemQx.GlosaMovementGlosa
                        If detailQxMovement.ConceptGlosas?.ConceptCodeName Is Nothing Then
                            Dim concept = _context.ConceptGlosas.FirstOrDefault(Function(m) m.Id = detailQxMovement.CodeGlosaId)
                            detailQxMovement.ConceptGlosas.ConceptCodeName = concept.Code & " - " & concept.NameSpecific
                        End If
                    Next
                Next
            Else
                If item.GlosaMovementGlosa IsNot Nothing Then
                    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemmov.ConceptGlosas IsNot Nothing Then
                            itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific
                        End If
                        item.ValueAcceptedFirstInstanceadoFacade = item.ValueAcceptedFirstInstanceadoFacade + IIf(itemmov.ValueAcceptedFirstInstance Is Nothing, 0, itemmov.ValueAcceptedFirstInstance)
                        If itemmov.MainGlosa = True Then
                            item.ValueGlosadoFacade = item.ValueGlosadoFacade + itemmov.ValueGlosado
                            If itemmov.ValueReiterated IsNot Nothing Then
                                item.ValueReiteratedFacade = item.ValueReiteratedFacade + itemmov.ValueReiterated
                            End If
                        End If
                        If itemmov.ResponsibleThirdPartyId IsNot Nothing Then
                            Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = itemmov.ResponsibleThirdPartyId Select New With {tp.Nit, tp.Name}).FirstOrDefault()
                            itemmov.ResponsibleThirdPartyNitName = String.Concat(thirdParty?.Nit, " - ", thirdParty?.Name)
                        End If
                    Next
                End If
            End If
            _invoiceDetail = Nothing
            If item.InvoiceDetailNativeId IsNot Nothing Then
                _invoiceDetail = (From id In _context.InvoiceDetail.AsNoTracking().Include("Invoice").AsNoTracking() Where id.Id = item.InvoiceDetailNativeId AndAlso id.Invoice.InvoiceNumber = item.InvoiceNumber Select id).FirstOrDefault
            End If
            If _invoiceDetail IsNot Nothing Then
                item.ValorEntidad = _invoiceDetail.ThirdPartySalesPrice
                item.ValorPaciente = _invoiceDetail.SubTotalPatientSalesPrice
            Else
                item.ValorEntidad = item.InvoicedValue
            End If
        Next

        Return ListInvoiceDetail
    End Function

    ''' <summary>
    ''' Para cargar datos a excel de las facturas a glosar
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListsStructureDetailInvocie(ByVal listInvoice As List(Of String)) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListsStructureDetailInvocie
        Dim Busqueda = (From e In _context.GlosaInvoiceDetail
                        Select e
                        Where listInvoice.Contains(e.InvoiceNumber)).ToList()
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' Funcion para validar que el id detalle de factura exista en la BD
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateIdDetail(id As String) As Boolean Implements IInvoiceDetailRepository.ValidateIdDetail
        Dim Detail = From e In _context.GlosaInvoiceDetail
                     Where e.Id = id
                     Select e
        If Detail.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    '''  Funcion para validar que el id detalle de factura QX exista en la BD
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateIdDetailQx(id As String) As Boolean Implements IInvoiceDetailRepository.ValidateIdDetailQx
        Dim Detailqx = From e In _context.GlosaInvoiceDetailQX
                       Where e.Id = id
                       Select e
        If Detailqx.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailRepository.ListGlosaInvoiceDetailForGeneralConciliation
        Dim invoiceDetail = (From e In _context.GlosaInvoiceDetail
                             Group Join p In _context.GlosaMovementGlosa On p.InvoiceDetailId Equals e.Id Into Group
                             From m In Group.DefaultIfEmpty()
                             Where e.InvoiceNumber = InvoiceNumber AndAlso (m Is Nothing OrElse m.IsNormative) And e.GlosaMovementGlosa.Count > 0
                             Select e).Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.PartialPaymentsMovement")
        Return invoiceDetail.ToList()
    End Function

End Class
