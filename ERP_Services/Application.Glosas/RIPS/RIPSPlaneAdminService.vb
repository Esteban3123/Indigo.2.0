'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Daniel Eduardo Arévalo
' Created          : 27-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"


Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Transactions
Imports System.Xml.Linq
Imports Application.Events.Models
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Billing.POCO.E_RIPS
Imports Domain.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.Data.CosmosModelRepository.Repositories.Billing
Imports Infrastructure.Data.CosmosModelRepository.Repositories.Generic
Imports Newtonsoft.Json

#End Region
Public Class RIPSPlaneAdminService
    Implements IRIPSPlaneAdminService

    Private _radicateInvoiceCRepository As IRadicateInvoiceCRepository
    Private _ripsPlaneService As IRIPSPlane
    Private _invoiceRepository As IInvoiceRepository
    Private _factoryQueue As IFactoryQueue
    Private _billingNoteRepository As IBillingNoteRepository
    Private _documentsAssociatedRIPSRepository As IDocumentsAssociatedRIPSRepository
    Private _electronicsRIPSRepository As IElectronicsRIPSRepository
    Private _rIPSCosmosDbModelRepository As IRIPSCosmosDbModelRepository
    Private _initialBalanceInvoiceRepository As IInitialBalanceInvoiceRepository
    Private _portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository
    Private _electronicDocumentRepository As IElectronicDocumentRepository
    Private _storage As IStorage
    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="RIPSPlaneAdminService" />.
    ''' </summary>
    ''' <param name="RIPSPlaneService">el repositorio para el manejo de Planos de RIPS</param>
    Public Sub New(radicateInvoiceCRepository As IRadicateInvoiceCRepository,
                   ByVal RIPSPlaneService As IRIPSPlane,
                   ByVal invoiceRepository As IInvoiceRepository,
                   ByVal factoryQueue As IFactoryQueue,
                   ByVal billingNoteRepository As IBillingNoteRepository,
                   ByVal electronicsRIPSRepository As IElectronicsRIPSRepository,
                   Optional ByVal rIPSCosmosDbModelRepository As IRIPSCosmosDbModelRepository = Nothing,
                   Optional ByVal documentsAssociatedRIPSRepository As IDocumentsAssociatedRIPSRepository = Nothing,
                   Optional ByVal initialBalanceInvoiceRepository As IInitialBalanceInvoiceRepository = Nothing,
                   Optional ByVal portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository = Nothing,
                   Optional ByVal electronicDocumentRepository As IElectronicDocumentRepository = Nothing,
                   Optional ByVal factoryStorage As IFactoryStorage = Nothing)

        If RIPSPlaneService Is Nothing Then
            Throw New ArgumentNullException("Repositorio de RIPSPlaneService Vacio")
        End If

        Me._radicateInvoiceCRepository = radicateInvoiceCRepository
        Me._ripsPlaneService = RIPSPlaneService
        Me._invoiceRepository = invoiceRepository
        Me._factoryQueue = factoryQueue
        Me._billingNoteRepository = billingNoteRepository
        Me._rIPSCosmosDbModelRepository = rIPSCosmosDbModelRepository
        Me._electronicsRIPSRepository = electronicsRIPSRepository
        Me._documentsAssociatedRIPSRepository = documentsAssociatedRIPSRepository
        Me._initialBalanceInvoiceRepository = initialBalanceInvoiceRepository
        Me._portfolioInitialBalanceRepository = portfolioInitialBalanceRepository
        Me._electronicDocumentRepository = electronicDocumentRepository
        Me._storage = factoryStorage?.CreateStorageControl()
    End Sub

    ''' <summary>
    ''' Genera los archivos planos RIPS (AF, US, AC, AP, AT, AN, AU, AH, AM, AD, CT) de forma paralela
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Identificador del radicado de factura</param>
    ''' <param name="ConsecutiveRadicateInvoice">Consecutivo del radicado de factura</param>
    ''' <param name="CodificationType">Tipo de codificación a utilizar</param>
    ''' <param name="ServiceCode">Código del servicio</param>
    ''' <param name="Session">Variable de sesión con información del usuario y contexto</param>
    ''' <param name="DetailPackage">Indica si se genera el detalle del paquete</param>
    ''' <param name="GenerateADPlane">Indica si se debe generar el archivo plano AD</param>
    ''' <param name="InvoicesList">Lista opcional de identificadores de facturas a procesar</param>
    ''' <returns>Lista de resultados con los archivos planos generados</returns>
    Public Function GenerateRIPSPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, DetailPackage As Boolean, GenerateADPlane As Boolean, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IRIPSPlaneAdminService.GenerateRIPSPlane
        Try
            Dim XmlInvoices = Me.ConvertToXmlInvoices(InvoicesList)
            Dim ListPlaneRIPS As New List(Of ActionMessageResult(Of StringBuilder))
            ConsecutiveRadicateInvoice = Utils.StringPad(ConsecutiveRadicateInvoice, 6, 0, Utils.PadType.STR_PAD_LEFT)


            Dim tAF = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateAFFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, Session))
                                            End Sub)

            Dim tUS = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateUSFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, Session))
                                            End Sub)

            Dim tAC = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateACFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, DetailPackage, Session, ServiceCode))
                                            End Sub)

            Dim tAp = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateAPFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, ServiceCode, DetailPackage, Session))
                                            End Sub)

            Dim tAt = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateATFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, ServiceCode, DetailPackage, Session))
                                            End Sub)

            Dim tAN = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateANFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, Session))
                                            End Sub)

            Dim tAU = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateAUFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, Session))
                                            End Sub)

            Dim tAH = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateAHFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, Session))
                                            End Sub)

            Dim tAM = Task.Factory.StartNew(Sub()
                                                Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateAMFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, CodificationType, DetailPackage, Session))
                                            End Sub)

            'Se valida si se genera o no el archido plano AD
            If GenerateADPlane Then
                Dim tAD = Task.Factory.StartNew(Sub()
                                                    Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateADFile(IdRadicateInvoice, XmlInvoices, ConsecutiveRadicateInvoice, DetailPackage, Session, ServiceCode))
                                                End Sub)

                Task.WaitAll(tAF, tUS, tAC, tAp, tAt, tAN, tAU, tAH, tAM, tAD)
            Else
                Task.WaitAll(tAF, tUS, tAC, tAp, tAt, tAN, tAU, tAH, tAM)
            End If

            Me.AddToListPlaneRIPS(ListPlaneRIPS, _ripsPlaneService.CTFile(ConsecutiveRadicateInvoice, ListPlaneRIPS))
            Return ListPlaneRIPS
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera los archivos planos FURIPS1 y FURIPS2 para accidentes de tránsito
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Identificador del radicado de factura</param>
    ''' <param name="Session">Variable de sesión con información del usuario y contexto</param>
    ''' <param name="InvoicesList">Lista opcional de facturas RIPS a procesar</param>
    ''' <returns>Lista de resultados con los archivos planos FURIPS generados</returns>
    Public Function GenerateFURIPSPlane(IdRadicateInvoice As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IRIPSPlaneAdminService.GenerateFURIPSPlane
        Try
            Dim fechaCorte As DateTime = DateTime.Now
            Dim InvoiceIds As New List(Of Integer)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                InvoiceIds = InvoicesList.Select(Function(o) o.InvoiceId).ToList()
            End If

            Dim XmlParameters = Me.ConvertToXmlParameters(Session.IndigoCompanyNit, IdRadicateInvoice, 0, 0)
            Dim XmlInvoices = Me.ConvertToXmlInvoices(InvoiceIds)
            Dim ListPlaneRIPS As New List(Of ActionMessageResult(Of StringBuilder))


            Dim tFURIPS1 = New Task(Sub()
                                        Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateFURIPS1File(IdRadicateInvoice, XmlParameters, XmlInvoices, Session, fechaCorte))
                                    End Sub)

            Dim tFURIPS2 = New Task(Sub()
                                        Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateFURIPS2File(IdRadicateInvoice, XmlParameters, XmlInvoices, Session, fechaCorte))
                                    End Sub)
            tFURIPS1.Start()
            tFURIPS2.Start()
            Task.WaitAll(tFURIPS1, tFURIPS2)
            Return ListPlaneRIPS
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano FURTRAN para servicios de transporte de accidentados
    ''' </summary>
    ''' <param name="InvoicesList">Lista de facturas RIPS a procesar</param>
    ''' <param name="Session">Variable de sesión con información del usuario y contexto</param>
    ''' <returns>Lista de resultados con el archivo plano FURTRAN generado</returns>
    Public Function GenerateFURTRANPlane(InvoicesList As List(Of RIPSBilling), Session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder)) Implements IRIPSPlaneAdminService.GenerateFURTRANPlane
        Try
            Dim fechaCorte As DateTime = DateTime.Now
            Dim AdmissionNumber As New List(Of String)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                AdmissionNumber = InvoicesList.Select(Function(o) o.AdmissionNumber).ToList()
            End If

            Dim XmlInvoices = Me.ConvertToXmlAdmissionNUmber(AdmissionNumber)
            Dim ListPlaneRIPS As New List(Of ActionMessageResult(Of StringBuilder))


            Dim tFURTRAN = New Task(Sub()
                                        Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateFURTRANFile(XmlInvoices, Session, fechaCorte))
                                    End Sub)

            tFURTRAN.Start()
            Task.WaitAll(tFURTRAN)
            Return ListPlaneRIPS
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo FUR SERVICIOS de la Circular Externa 003 de 2026 de
    ''' ADRES a partir de las facturas seleccionadas. Aplica el tope de 100
    ''' facturas, valida longitudes/catálogos y devuelve el JSON ya serializado
    ''' junto con un DataSet plano listo para exportarse a XLSX desde la UI.
    ''' </summary>
    Public Function GenerateAdresFurServiciosPlane(IdRadicateInvoice As Integer,
                                                   Session As SessionValues,
                                                   Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of AdresClaimFile) _
                                                   Implements IRIPSPlaneAdminService.GenerateAdresFurServiciosPlane

        Const SinDatos As String = "No se encontraron datos para la generación del archivo"

        Try
            Dim InvoiceIds As New List(Of Integer)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                InvoiceIds = InvoicesList.Select(Function(o) o.InvoiceId).ToList()
            End If

            Dim warnings As New List(Of String)
            If InvoiceIds.Count > AdresFurServiciosHelper.MAX_FACTURAS_JSON Then
                Dim omitted = InvoiceIds.Count - AdresFurServiciosHelper.MAX_FACTURAS_JSON
                warnings.Add($"Se procesaron las primeras {AdresFurServiciosHelper.MAX_FACTURAS_JSON} facturas; " &
                             $"se omitieron {omitted} adicionales por la restricción de máximo 100 facturas por JSON FUR SERVICIOS.")
                InvoiceIds = InvoiceIds.Take(AdresFurServiciosHelper.MAX_FACTURAS_JSON).ToList()
            End If

            Dim XmlParameters = Me.ConvertToXmlParameters(Session.IndigoCompanyNit, IdRadicateInvoice, 0, 0)
            Dim XmlInvoices = Me.ConvertToXmlInvoices(InvoiceIds)

            Dim spResult = Me.SP_GenerateFileData("SP_GenerateAdresFurServiciosData", XmlParameters, XmlInvoices, Session)
            If spResult Is Nothing OrElse spResult.Tables.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim rows = AdresFurServiciosHelper.MapDataTableToRows(spResult.Tables(0))
            If rows.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim validRows = AdresFurServiciosHelper.FilterValidRows(rows, warnings)
            If validRows.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim nit = GetCompanyNit(Session)
            Dim fileName As String = "SER" & nit
            Dim json = AdresFurServiciosHelper.BuildJson(validRows, nit)
            Dim excelData = AdresFurServiciosHelper.BuildExcelData(validRows, nit)

            Dim claimFile As New AdresClaimFile With {
                .FileName = fileName,
                .JsonContent = json,
                .ExcelData = excelData,
                .RecordCount = validRows.Count,
                .Warnings = warnings
            }

            Return New ActionMessageResult(Of AdresClaimFile) With {
                .StateResult = True,
                .Message = fileName,
                .ObjectEmbbeded = claimFile
            }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return New ActionMessageResult(Of AdresClaimFile) With {
                .StateResult = False,
                .Message = String.Format("Error al generar FUR SERVICIOS: {0}", Utils.GetInnerExceptionMessageToString(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo FUR (Formulario Único de Reclamaciones) de la Circular Externa 003 de 2026 de ADRES a partir de las facturas seleccionadas
    ''' </summary>
    Public Function GenerateAdresFurPlane(IdRadicateInvoice As Integer,
                                          Session As SessionValues,
                                          Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of AdresClaimFile) _
                                          Implements IRIPSPlaneAdminService.GenerateAdresFurPlane

        Const SinDatos As String = "No se encontraron datos para la generación del archivo"

        Try
            Dim InvoiceIds As New List(Of Integer)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                InvoiceIds = InvoicesList.Select(Function(o) o.InvoiceId).ToList()
            End If

            Dim warnings As New List(Of String)
            If InvoiceIds.Count > AdresFurHelper.MAX_FACTURAS_JSON Then
                Dim omitted = InvoiceIds.Count - AdresFurHelper.MAX_FACTURAS_JSON
                warnings.Add($"Se procesaron las primeras {AdresFurHelper.MAX_FACTURAS_JSON} facturas; " &
                             $"se omitieron {omitted} adicionales por la restricción de máximo 100 facturas por JSON FUR.")
                InvoiceIds = InvoiceIds.Take(AdresFurHelper.MAX_FACTURAS_JSON).ToList()
            End If

            Dim XmlParameters = Me.ConvertToXmlParameters(Session.IndigoCompanyNit, IdRadicateInvoice, 0, 0)
            Dim XmlInvoices = Me.ConvertToXmlInvoices(InvoiceIds)

            Dim spResult = Me.SP_GenerateFileData("SP_GenerateAdresFurData", XmlParameters, XmlInvoices, Session)
            If spResult Is Nothing OrElse spResult.Tables.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim rows = AdresFurHelper.MapDataTableToRows(spResult.Tables(0))
            If rows.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim validRows = AdresFurHelper.FilterValidRows(rows, warnings)
            If validRows.Count = 0 Then
                Return New ActionMessageResult(Of AdresClaimFile) With {.StateResult = False, .Message = SinDatos}
            End If

            Dim nit = GetCompanyNit(Session)
            Dim fileName As String = "FUR" & nit
            Dim json = AdresFurHelper.BuildJson(validRows, nit)
            Dim excelData = AdresFurHelper.BuildExcelData(validRows, nit)

            Dim claimFile As New AdresClaimFile With {
                .FileName = fileName,
                .JsonContent = json,
                .ExcelData = excelData,
                .RecordCount = validRows.Count,
                .Warnings = warnings
            }

            Return New ActionMessageResult(Of AdresClaimFile) With {
                .StateResult = True,
                .Message = fileName,
                .ObjectEmbbeded = claimFile
            }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return New ActionMessageResult(Of AdresClaimFile) With {
                .StateResult = False,
                .Message = String.Format("Error al generar FUR: {0}", Utils.GetInnerExceptionMessageToString(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el NIT del prestador desde INEMPRESU.INDNUMIDE.
    ''' Mismo enfoque que [dbo].[SP_ADM_ExportarDatosFur].
    ''' </summary>
    Private Function GetCompanyNit(Session As SessionValues) As String
        Try
            Dim dt = Me.GetDatatable(
                "SELECT TOP 1 LTRIM(RTRIM(INDNUMIDE)) AS Nit FROM dbo.INEMPRESU",
                Session,
                "INEMPRESU_Nit")
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return String.Empty
            Return Convert.ToString(dt.Rows(0)("Nit")).Trim()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos de Mega RIPS por identificador de radicado de factura
    ''' </summary>
    ''' <param name="radicateInvoiceId">Identificador del radicado de factura</param>
    ''' <param name="companyCode">Código de la compañía</param>
    ''' <param name="InvoicesList">Lista opcional de facturas RIPS a procesar</param>
    ''' <returns>Resultado con los datos de Mega RIPS en formato string</returns>
    Public Function GetMegaRIPSByRadicateInvoiceId(radicateInvoiceId As Integer, companyCode As String, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of String) Implements IRIPSPlaneAdminService.GetMegaRIPSByRadicateInvoiceId
        Try
            Dim result As String = _ripsPlaneService.GetMegaRIPSByRadicateInvoiceId(radicateInvoiceId, companyCode, InvoicesList)
            Return New ActionMessageResult(Of String)() With {.StateResult = True, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of String)() With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano Mega RIPS consolidado con toda la información de facturación
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Identificador del radicado de factura</param>
    ''' <param name="ConsecutiveRadicateInvoice">Consecutivo del radicado de factura</param>
    ''' <param name="CodificationType">Tipo de codificación a utilizar</param>
    ''' <param name="ServiceCode">Código del servicio</param>
    ''' <param name="Session">Variable de sesión con información del usuario y contexto</param>
    ''' <param name="InvoicesList">Lista opcional de identificadores de facturas a procesar</param>
    ''' <returns>Lista de resultados con el archivo Mega RIPS generado</returns>
    Public Function GenerateRIPSMegaPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IRIPSPlaneAdminService.GenerateRIPSMegaPlane
        Try
            Dim XmlParameters = Me.ConvertToXmlParameters(Session.IndigoCompanyNit, IdRadicateInvoice, CodificationType, ServiceCode)
            Dim XmlInvoices = Me.ConvertToXmlInvoices(InvoicesList)
            Dim ListPlaneRIPS As New List(Of ActionMessageResult(Of StringBuilder))
            ConsecutiveRadicateInvoice = Utils.StringPad(ConsecutiveRadicateInvoice, 6, 0, Utils.PadType.STR_PAD_LEFT)

            Me.AddToListPlaneRIPS(ListPlaneRIPS, Me.GenerateMegaPlaneFile(XmlParameters, XmlInvoices, ConsecutiveRadicateInvoice, Session))

            Return ListPlaneRIPS
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Valida y encola los RIPS electrónicos a generar según el tipo de entidad (Invoice, BillingNote, InvoiceEntityCapitated)
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad a procesar (Invoice, BillingNote, InvoiceEntityCapitated)</param>
    ''' <param name="listDocumentNumber">Lista de números de documento a procesar</param>
    ''' <param name="audit">Información de auditoría con el usuario que ejecuta la acción</param>
    ''' <param name="entityId">Identificador opcional de la entidad</param>
    ''' <returns>Resultado de la operación indicando éxito o fallo con mensaje descriptivo</returns>
    Public Function GenerateElectronicRIPSToQueue(entityName As String, listDocumentNumber As List(Of String), audit As AuditMessage, Optional entityId As Integer? = Nothing) As ActionResult Implements IRIPSPlaneAdminService.GenerateElectronicRIPSToQueue
        Try

            If String.IsNullOrEmpty(entityName) Then
                Throw New ArgumentNullException("Nombre de la entidad vacia")
            End If

            If listDocumentNumber Is Nothing OrElse Not listDocumentNumber.Any() Then
                Throw New ArgumentNullException("Lista de facturas vacia")
            End If

            If String.IsNullOrEmpty(audit?.CodeUser) Then
                Throw New ArgumentNullException("Codigo de usuario vacio")
            End If

            Dim stringBuilder As StringBuilder = New StringBuilder
            Dim listEventRIPSObject As List(Of ElectronicRIPS) = New List(Of ElectronicRIPS)

            Select Case entityName

                Case NameOf(Invoice)
                    Dim queryinvoice As List(Of Invoice)
                    queryinvoice = _invoiceRepository.GetByFilter(Function(x) listDocumentNumber.Contains(x.InvoiceNumber) _
                                                                            AndAlso x.InvoiceDate >= New DateTime(2025, 2, 1) _
                                                                            AndAlso Not Utils.ElectronicDocumentTypeNotAllowRIPS.Contains(x.DocumentType), False)?.ToList()
                    Dim result = Me.ValidateInvoiceDocuments(queryinvoice, listDocumentNumber)

                    If Not result.StateResult Then
                        Return New ActionResult With {.StateResult = False, .Message = result.Message}
                    End If

                    If Not String.IsNullOrEmpty(result?.Message) Then
                        stringBuilder.AppendLine(result?.Message)
                    End If

                    If result.ObjectEmbbeded?.Any() Then
                        listEventRIPSObject.Add(Me.CreateObjectEventRIPS(result.ObjectEmbbeded, EEntityNameERIPS.Invoice))
                    End If

                    If result.ObjectEmbbededAux?.Any() Then
                        listEventRIPSObject.Add(Me.CreateObjectEventRIPS(result.ObjectEmbbededAux, EEntityNameERIPS.InvoiceFixedAmount))
                    End If

                Case NameOf(BillingNote)
                    Dim queryNote = _billingNoteRepository.GetByFilter(Function(x) listDocumentNumber.Contains(x.Code) AndAlso x.NoteDate > New DateTime(2025, 1, 31), False)?.ToList()

                    If queryNote Is Nothing OrElse Not queryNote.Any() Then
                        Return New ActionResult With {.StateResult = False, .Message = $"No se encontrarón las siguientes Notas {String.Join(",", listDocumentNumber)}"}
                    End If

                    Dim listNotFoundNote = listDocumentNumber.Except(queryNote.Select(Function(x) x.Code).ToList())?.ToList()
                    If listNotFoundNote?.Any() Then
                        stringBuilder.AppendLine($"Se enviaron las notas, excepto las siguiente : {String.Join(",", listNotFoundNote)}")
                    End If
                    listDocumentNumber = listDocumentNumber.FindAll(Function(x) queryNote.Select(Function(f) f.Code).Contains(x))?.ToList()
                    listEventRIPSObject.Add(Me.CreateObjectEventRIPS(listDocumentNumber, EEntityNameERIPS.BillingNote))

                Case NameOf(InvoiceEntityCapitated)
                    listEventRIPSObject.Add(Me.CreateObjectEventRIPS(listDocumentNumber, EEntityNameERIPS.InvoiceEntityCapitated))
                Case Else
                    Return New ActionResult With {.StateResult = False, .Message = "Process out of range"}
            End Select

            For Each item In listEventRIPSObject
                TriggerEvent(item, "added", audit)
            Next

            Dim message As String = "Proceso existoso"

            If stringBuilder.Length > 0 Then
                message = stringBuilder.ToString()
            End If

            Return New ActionResult With {.StateResult = True, .Message = message}

        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida y segmenta las facturas para determinar cuáles deben enviarse a generación de RIPS electrónicos según su tipo de documento
    ''' </summary>
    ''' <param name="invoices">Lista de facturas a validar</param>
    ''' <param name="listDocumentNumber">Lista de números de documento solicitados</param>
    ''' <returns>Resultado con las listas de facturas segmentadas por módulo (FevRIPS o Capitado) y mensajes de validación</returns>
    Private Function ValidateInvoiceDocuments(invoices As List(Of Invoice), listDocumentNumber As List(Of String)) As ActionResult(Of List(Of String), List(Of String))
        If invoices Is Nothing OrElse Not invoices?.Any() Then
            Return New ActionResult(Of List(Of String), List(Of String)) _
                With {.StateResult = False,
                .Message = $"No se encontrarón las siguientes facturas {String.Join(",", listDocumentNumber)} o son documentos que no generar RIPS"}
        End If

        Dim stringBuilder = New StringBuilder
        Dim result = New ActionResult(Of List(Of String), List(Of String)) With {.StateResult = True}
        Dim listNotSendInvoice As List(Of String) = listDocumentNumber.Except(invoices.Select(Function(x) x.InvoiceNumber).ToList())?.ToList()

        If listNotSendInvoice?.Any() Then
            result.Message = $"Se enviaron las facturas, excepto las siguiente : {String.Join(",", listNotSendInvoice)}"
        End If

        If invoices.Exists(Function(x) Me.InvoicesModuleCapitedRIPS(x.DocumentType)) Then
            result.ObjectEmbbededAux = invoices.FindAll(Function(o) Me.InvoicesModuleCapitedRIPS(o.DocumentType)).Select(Function(x) x.InvoiceNumber).ToList()
        End If

        If invoices.Exists(Function(x) Me.InvoicesModuleFevRIPS(x.DocumentType)) Then

            result.ObjectEmbbeded = invoices.FindAll(Function(o) Me.InvoicesModuleFevRIPS(o.DocumentType)) _
                                            .Select(Function(x) x.InvoiceNumber).ToList()
        End If

        If result.ObjectEmbbeded?.Any() OrElse result?.ObjectEmbbededAux.Any() Then
            Return result
        Else
            Return New ActionResult(Of List(Of String), List(Of String)) With {.StateResult = False, .Message = $"No se encontrarón las siguientes facturas {String.Join(",", listDocumentNumber)} o son documentos que no generar RIPS"}
        End If
    End Function

    ''' <summary>
    ''' Determina si una factura según su tipo de documento debe enviarse al módulo FevRIPS
    ''' </summary>
    ''' <param name="documentType">Tipo de documento electrónico de la factura</param>
    ''' <returns>True si el documento debe procesarse por FevRIPS, False si es factura de capitación</returns>
    Public Function InvoicesModuleFevRIPS(documentType As Byte) As Boolean
        Return (documentType <> EElectronicDocumentType.CapitatedInvoice)
    End Function

    ''' <summary>
    ''' Determina si una factura según su tipo de documento debe enviarse al módulo de capitación
    ''' </summary>
    ''' <param name="documentType">Tipo de documento electrónico de la factura</param>
    ''' <returns>True si el documento es una factura de capitación, False en caso contrario</returns>
    Public Function InvoicesModuleCapitedRIPS(documentType As Byte) As Boolean
        Return documentType = EElectronicDocumentType.CapitatedInvoice
    End Function

    ''' <summary>
    ''' Crea un objeto ElectronicRIPS para publicar en la cola de eventos
    ''' </summary>
    ''' <param name="listEntityCode">Lista de códigos de entidad (números de documento)</param>
    ''' <param name="entityName">Tipo de entidad RIPS electrónica</param>
    ''' <returns>Objeto ElectronicRIPS configurado para ser publicado</returns>
    Private Function CreateObjectEventRIPS(listEntityCode As List(Of String), entityName As EEntityNameERIPS) As ElectronicRIPS
        Dim obj As ElectronicRIPS = New ElectronicRIPS
        With obj
            .ListEntityCode = listEntityCode
            .EntityName = (entityName).ToString
        End With
        Return obj
    End Function


#Region "Event IndigoQueue"
    ''' <summary>
    ''' Publica un evento de RIPS electrónicos en la cola de mensajería para su procesamiento asíncrono
    ''' </summary>
    ''' <param name="electronicRIPS">Objeto con la información del RIPS electrónico a procesar</param>
    ''' <param name="changeTracker">Tipo de cambio realizado (added, modified, deleted)</param>
    ''' <param name="audit">Información de auditoría con el usuario que ejecuta la acción</param>
    Public Sub TriggerEvent(electronicRIPS As ElectronicRIPS, changeTracker As String, audit As Infrastructure.CrossCutting.Base.AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim eventData = wrapperEvent.GenerateWrapperEventData(electronicRIPS, audit.CodeUser, changeTracker, DittoSourceType.electronicRIPS)
        Dim queue = _factoryQueue.CreateQueue()
        queue.Publish(eventData)
    End Sub
#End Region

    ''' <summary>
    ''' Valida y encola registros de servicio para reconstruir JSON RIPS.
    ''' </summary>
    Public Function RebuildFixedAmountRIPSToQueue(listDocumentNumber As List(Of String), audit As AuditMessage) As ActionResult Implements IRIPSPlaneAdminService.RebuildFixedAmountRIPSToQueue
        Try
            If listDocumentNumber Is Nothing OrElse Not listDocumentNumber.Any() Then Throw New ArgumentNullException("Lista de registros vacia")
            If String.IsNullOrEmpty(audit?.CodeUser) Then Throw New ArgumentNullException("Codigo de usuario vacio")

            Dim requested = listDocumentNumber.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Select(Function(x) x.Trim()).Distinct().ToList()
            Dim invoices = _invoiceRepository.GetByFilter(Function(x) requested.Contains(x.InvoiceNumber) AndAlso x.DocumentType = 5 AndAlso x.Status = 1, False)?.ToList()
            If invoices Is Nothing OrElse Not invoices.Any() Then Return New ActionResult With {.StateResult = False, .Message = "No se encontraron registros de servicio validos para reconstruir"}

            Dim validNumbers = invoices.Select(Function(x) x.InvoiceNumber).Distinct().ToList()
            Dim invalidNumbers = requested.Except(validNumbers).ToList()
            Dim chunkSize = 100
            Integer.TryParse(ConfigurationManager.AppSettings("RipsRebuildChunkSize"), chunkSize)
            If chunkSize <= 0 Then chunkSize = 100

            For index = 0 To validNumbers.Count - 1 Step chunkSize
                TriggerEvent(CreateObjectEventRIPS(validNumbers.Skip(index).Take(chunkSize).ToList(), EEntityNameERIPS.Invoice), "modified", audit)
            Next

            Dim message = "Proceso exitoso"
            If invalidNumbers.Any() Then message = String.Concat("Se reconstruyeron los registros validos, excepto: ", String.Join(",", invalidNumbers))
            Return New ActionResult With {.StateResult = True, .Message = message}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ReSendElectronicRIPSToQueue(entityName As String, listDocumentNumber As List(Of String), audit As AuditMessage, Optional entityId As Integer? = Nothing) As ActionResult Implements IRIPSPlaneAdminService.ReSendElectronicRIPSToQueue
        Try

            If String.IsNullOrEmpty(entityName) Then
                Throw New ArgumentNullException("Nombre de la entidad vacia")
            End If

            If listDocumentNumber Is Nothing OrElse Not listDocumentNumber.Any() Then
                Throw New ArgumentNullException("Lista de facturas vacia")
            End If

            If String.IsNullOrEmpty(audit?.CodeUser) Then
                Throw New ArgumentNullException("Codigo de usuario vacio")
            End If

            Dim entityNameRips As EEntityNameERIPS = Utils.ERIPSEntityNameHologation(entityName)
            If entityNameRips <> EEntityNameERIPS.ResendInvoice Then

                For Each item In listDocumentNumber
                    Dim electronicRIPSObject = Me.CreateObjectEventRIPS({item}.ToList(), Utils.ERIPSEntityNameHologation(entityName))
                    TriggerEvent(electronicRIPSObject, "modified", audit)
                Next

                Dim message As String = "Proceso existoso"
                Return New ActionResult With {.StateResult = True, .Message = message}

            End If

            Dim result = GetInvoiceListEventRIPSObject(listDocumentNumber, True)


            If result Is Nothing OrElse Not result.StateResult Then
                Return New ActionResult With {.StateResult = False, .Message = If(String.IsNullOrEmpty(result?.Message), "Error en el Re envio", result.Message)}
            End If

            For Each item In result.ObjectEmbbeded
                TriggerEvent(item, "modified", audit)
            Next

            Return New ActionResult With {.StateResult = True, .Message = If(String.IsNullOrEmpty(result.Message), "Proceso Exitoso", result.Message)}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida las facturas y retorna los objetos ElectronicRIPS configurados para publicar en cola de eventos
    ''' </summary>
    ''' <param name="listDocumentNumber">Lista de números de documento a procesar</param>
    ''' <param name="isResend">Indica si es un reenvío (true) o un envío nuevo (false)</param>
    ''' <returns>Resultado con la lista de objetos ElectronicRIPS listos para publicar y mensajes de validación</returns>
    Private Function GetInvoiceListEventRIPSObject(listDocumentNumber As List(Of String), isResend As Boolean) As ActionResult(Of List(Of ElectronicRIPS))
        Dim queryinvoice As List(Of Invoice)
        Dim listEventRIPSObject As List(Of ElectronicRIPS) = New List(Of ElectronicRIPS)

        queryinvoice = _invoiceRepository.GetByFilter(Function(x) listDocumentNumber.Contains(x.InvoiceNumber) _
                                                                        AndAlso x.InvoiceDate >= New DateTime(2025, 2, 1) _
                                                                        AndAlso Not Utils.ElectronicDocumentTypeNotAllowRIPS.Contains(x.DocumentType), False)?.ToList()

        Dim result = Me.ValidateInvoiceDocuments(queryinvoice, listDocumentNumber)
        Dim stringBuilder As StringBuilder = New StringBuilder

        If Not result.StateResult Then
            Return New ActionResult(Of List(Of ElectronicRIPS)) With {.StateResult = False, .Message = result.Message}
        End If

        If Not String.IsNullOrEmpty(result?.Message) Then
            stringBuilder.AppendLine(result?.Message)
        End If

        Dim listTaskInvoice As Task(Of List(Of ElectronicRIPS))
        Dim listTaskInvoiceFixedAmount As Task(Of List(Of ElectronicRIPS))


        listTaskInvoice = Task.Run(Function() As List(Of ElectronicRIPS)

                                       Dim list As List(Of ElectronicRIPS) = New List(Of ElectronicRIPS)

                                       If result.ObjectEmbbeded?.Any() Then
                                           For Each item In result.ObjectEmbbeded
                                               list.Add(Me.CreateObjectEventRIPS({item}.ToList(), If(isResend, EEntityNameERIPS.ResendInvoice, EEntityNameERIPS.Invoice)))
                                           Next
                                       End If
                                       Return list
                                   End Function)

        listTaskInvoiceFixedAmount = Task.Run(Function() As List(Of ElectronicRIPS)
                                                  Dim list As List(Of ElectronicRIPS) = New List(Of ElectronicRIPS)
                                                  If result.ObjectEmbbededAux?.Any() Then
                                                      For Each item In result.ObjectEmbbededAux
                                                          list.Add(Me.CreateObjectEventRIPS({item}.ToList(), If(isResend, EEntityNameERIPS.ResendInvoiceFixedAmount, EEntityNameERIPS.InvoiceFixedAmount)))
                                                      Next
                                                  End If
                                                  Return list
                                              End Function)

        Task.WaitAll(listTaskInvoice, listTaskInvoiceFixedAmount)

        listEventRIPSObject.AddRange(listTaskInvoice.Result.Union(listTaskInvoiceFixedAmount.Result))

        Return New ActionResult(Of List(Of ElectronicRIPS)) With {.StateResult = True, .Message = stringBuilder.ToString(), .ObjectEmbbeded = listEventRIPSObject}

    End Function


    ''' <summary>
    ''' Ejecuta el reenvío masivo de documentos RIPS electrónicos en estado erróneo o registrado, aplicando políticas de reintento
    ''' </summary>
    ''' <param name="take">Cantidad máxima de documentos a procesar en esta ejecución</param>
    ''' <param name="audit">Información de auditoría con el usuario que ejecuta la acción</param>
    ''' <returns>Resultado de la operación con el detalle de documentos reenviados</returns>
    Public Function MassiveResendWithPolicies(take As Integer, audit As AuditMessage) As ActionResult Implements IRIPSPlaneAdminService.MassiveResendWithPolicies
        Dim listElectronicsRIPS As List(Of ElectronicsRIPS) = New List(Of ElectronicsRIPS)
        Dim documentResend As List(Of String) = New List(Of String)
        Dim stringBuilder As StringBuilder = New StringBuilder
        Dim flagResendAny As Boolean = False
        Dim unitOfWork As IUnitWork = Me._electronicsRIPSRepository.UnitWork

        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                listElectronicsRIPS = _electronicsRIPSRepository.QueryElectronicsRIPSInvalidToRetry(take:=take)
                If listElectronicsRIPS Is Nothing OrElse Not listElectronicsRIPS.Any() Then
                    Return New ActionResult With {.StateResult = True, .Message = "No se encontraron datos para re enviar"}
                End If

                For Each item In listElectronicsRIPS
                    Dim message = $"documento con numero {item.DocumentNumber} y tipo {item.EntityName}"
                    'A partir de la fecha de creacion se procede a validar cuando se puede realizar el reintento
                    Dim minuteToAdd = Utils.GetMinutesToAdd(item.Retry)
                    Dim timeForRetry = DateAdd(DateInterval.Minute, minuteToAdd, item.CreationDate)

                    If DateTime.Now < timeForRetry Then
                        stringBuilder.AppendLine($"Por politcas de Re-intento No se re-envio el {message}")
                        Continue For
                    End If

                    Dim entityName = String.Empty
                    Select Case item.EntityName
                        Case NameOf(EEntityNameERIPS.Invoice)
                            entityName = NameOf(EEntityNameERIPS.ResendInvoice)
                        Case NameOf(EEntityNameERIPS.BillingNote)
                            entityName = NameOf(EEntityNameERIPS.ResendBillingNote)
                        Case Else
                            entityName = NameOf(EEntityNameERIPS.Unknown)
                    End Select

                    If item.EntityName = NameOf(EEntityNameERIPS.Invoice) AndAlso item.DocumentType = EElectronicDocumentType.CapitatedInvoice Then
                        entityName = NameOf(EEntityNameERIPS.ResendInvoiceFixedAmount)
                    End If

                    item.Retry += 1
                    item.MarkAsModified()
                    _electronicsRIPSRepository.SaveEntity(item)
                    unitOfWork.Commit()

                    Try
                        Dim electronicRIPSObject = Me.CreateObjectEventRIPS(New List(Of String) From {item.DocumentNumber}, Utils.ERIPSEntityNameHologation(entityName))
                        TriggerEvent(electronicRIPSObject, "modified", audit)

                    Catch ex As Exception
                        stringBuilder.AppendLine($"No se logro reEnviar el {message} - {Utils.GetInnerExceptionMessageToString(ex)}")
                        Continue For
                    End Try

                    flagResendAny = True
                    stringBuilder.AppendLine($"Se logro reEnviar el {message}")
                Next
                scope.Complete()
                Return New ActionResult With {.StateResult = flagResendAny, .Message = stringBuilder.ToString()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = $"{ex.StackTrace} // {Utils.GetInnerExceptionMessageToString(ex)}"}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Consulta y retorna el JSON de RIPS por su identificador desde Cosmos DB, incluyendo documentos asociados
    ''' </summary>
    ''' <param name="idCosmosDb">Identificador único del documento en Cosmos DB</param>
    ''' <returns>Resultado con el JSON completo del RIPS serializado como string</returns>
    Public Async Function GetJsonRIPSByIdByteAsync(idCosmosDb As String) As Task(Of ActionResult(Of String)) Implements IRIPSPlaneAdminService.GetJsonRIPSById
        Try
            If _rIPSCosmosDbModelRepository Is Nothing Then
                ' si No se logra instanciar correctamente el repo, quitar el optinal del parametro para seguimiento orignal del error
                Throw New ArgumentNullException(NameOf(_rIPSCosmosDbModelRepository), "No se logro instanciar el repositorio")
            End If

            If _documentsAssociatedRIPSRepository Is Nothing Then
                ' si No se logra instanciar correctamente el repo, quitar el optinal del parametro para seguimiento orignal del error
                Throw New ArgumentNullException(NameOf(_documentsAssociatedRIPSRepository), "No se logro instanciar el repositorio")
            End If

            If String.IsNullOrEmpty(idCosmosDb) Then
                Throw New ArgumentNullException(NameOf(idCosmosDb))
            End If

            If String.IsNullOrEmpty(ServerSessionValues.Current.CurrentContainer) Then
                Throw New ArgumentNullException(NameOf(ServerSessionValues.Current.CurrentContainer), "parametro vacio")
            End If

            Dim Query = Await _rIPSCosmosDbModelRepository.GetJsonRIPSByIdAsync(idCosmosDb)

            If Query Is Nothing OrElse String.IsNullOrEmpty(Query.id) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = $"No se encontró el Json @{idCosmosDb}"}
            End If

            Await EnsureXmlFevFileForInitialBalanceAsync(Query)
            Query.DocumentsAssociatedRIPS = Await AssociatedRIPSResponse(Query.JsonRIPS.rips, idCosmosDb, Query.EntityName)

            Dim jsonRIPS = JsonConvert.SerializeObject(Query, Formatting.None)
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = jsonRIPS, .Message = "Proceso exitoso"}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = "Ocurrió un error al obtener el Json RIPS: " & ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' obtiene el json rips por numero de factura, incluyendo documentos asociados
    ''' </summary>
    ''' <param name="docNumber">Identificador único del numero de factura</param>
    ''' <returns>Resultado con el JSON completo del RIPS serializado como string</returns>
    Public Async Function GetJsonRIPSByDocNumberAsync(docNumber As String) As Task(Of ActionResult(Of String)) Implements IRIPSPlaneAdminService.GetJsonRIPSByDocNumber
        Try
            If _rIPSCosmosDbModelRepository Is Nothing Then
                ' si No se logra instanciar correctamente el repo, quitar el optinal del parametro para seguimiento orignal del error
                Throw New ArgumentNullException(NameOf(_rIPSCosmosDbModelRepository), "No se logro instanciar el repositorio")
            End If

            If _documentsAssociatedRIPSRepository Is Nothing Then
                ' si No se logra instanciar correctamente el repo, quitar el optinal del parametro para seguimiento orignal del error
                Throw New ArgumentNullException(NameOf(_documentsAssociatedRIPSRepository), "No se logro instanciar el repositorio")
            End If

            If String.IsNullOrEmpty(docNumber) Then
                Throw New ArgumentNullException(NameOf(docNumber))
            End If

            If String.IsNullOrEmpty(ServerSessionValues.Current.CurrentContainer) Then
                Throw New ArgumentNullException(NameOf(ServerSessionValues.Current.CurrentContainer), "parametro vacio")
            End If

            Dim Query = Await _rIPSCosmosDbModelRepository.GetJsonRIPSByDocNumber(docNumber)

            If Query Is Nothing OrElse String.IsNullOrEmpty(Query.id) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = $"No se encontró el Json para el documento: {docNumber}"}
            End If

            Await EnsureXmlFevFileForInitialBalanceAsync(Query)
            Query.DocumentsAssociatedRIPS = Await AssociatedRIPSResponse(Query.JsonRIPS.rips, Query.id)

            Dim jsonRIPS = JsonConvert.SerializeObject(Query, Formatting.None)
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = jsonRIPS, .Message = "Proceso exitoso"}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = "Ocurrió un error al obtener el Json RIPS: " & ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el documento asociado y construye la respuesta con el CUV (Código Único de Validación) y detalles de radicación
    ''' </summary>
    ''' <param name="jsonRIPS">Modelo de RIPS electrónico con la información del documento</param>
    ''' <param name="idCosmosDb">Identificador único del documento en Cosmos DB</param>
    ''' <returns>Objeto DocumentsAssociatedRIPS con la información de respuesta y validación</returns>
    Private Async Function AssociatedRIPSResponse(jsonRIPS As ElectronicRIPSModel, idCosmosDb As String, Optional queryEntityName As String = Nothing) As Task(Of DocumentsAssociatedRIPS)
        Dim documentNumber As String
        Dim entityName As String


        If jsonRIPS Is Nothing Then
            jsonRIPS = New ElectronicRIPSModel()

            Dim documentId As String = idCosmosDb.Split("-"c).First()

            Select Case queryEntityName
                Case "BillingNote"
                    jsonRIPS.numNota = documentId

                Case "Invoice", Nothing, ""
                    jsonRIPS.numFactura = documentId

                Case Else
                    jsonRIPS.numFactura = documentId
            End Select
        End If

        If (Not String.IsNullOrEmpty(jsonRIPS.tipoNota) And jsonRIPS.tipoNota = "NA") Then
            documentNumber = idCosmosDb
            entityName = NameOf(EEntityNameERIPS.BillingNoteAdjustment)

        ElseIf String.IsNullOrEmpty(jsonRIPS.numNota) Then
            documentNumber = jsonRIPS.numFactura
            entityName = NameOf(EEntityNameERIPS.Invoice)
        Else
            documentNumber = jsonRIPS.numNota
            entityName = NameOf(EEntityNameERIPS.BillingNote)
        End If

        Dim electronicRIPS = _electronicsRIPSRepository.GetValidElectronicRIPS(documentNumber, entityName)
        If electronicRIPS Is Nothing Then Return New DocumentsAssociatedRIPS()

        Dim associatedDocument = Await _documentsAssociatedRIPSRepository.GetDocumentsAssociatedRIPSByRIPSId(idCosmosDb)

        Dim queryCUV = New QueryCUVResultModel()

        If Not String.IsNullOrEmpty(associatedDocument?.Data?.ToString()) Then
            queryCUV = JsonConvert.DeserializeObject(Of QueryCUVResultModel)(associatedDocument.Data.ToString())
        End If

        Dim radicateDate = If(queryCUV?.FechaEmision, electronicRIPS.RadicateDate)
        Dim dateUtc As DateTimeOffset = New DateTimeOffset(radicateDate.Year, radicateDate.Month, radicateDate.Day, radicateDate.Hour, radicateDate.Minute, radicateDate.Second, TimeSpan.Zero)

        'Se define el número de la factura según el tipo de entidad
        Dim numFacturaValue As String = Nothing
        Select Case entityName
            Case NameOf(EEntityNameERIPS.BillingNoteAdjustment)
                numFacturaValue = jsonRIPS.numFactura
            Case NameOf(EEntityNameERIPS.BillingNote)
                numFacturaValue = If(electronicRIPS?.BillingNoteInvoiceNumber, queryCUV?.NumDocumentoReferenciado)
            Case NameOf(EEntityNameERIPS.Invoice)
                numFacturaValue = If(queryCUV?.NumeroDocumento, jsonRIPS.numFactura)
            Case Else
                numFacturaValue = jsonRIPS.numFactura
        End Select

        Dim defaultValidationResults = New List(Of ResultValidation) From {
                                New ResultValidation With {
                                    .Clase = "NOTIFICACION",
                                    .Codigo = "FED131",
                                    .Descripcion = "[Interoperabilidad.Group.Collection.AdditionalInformation.NUMERO_POLIZA.Value] El apartado no existe o no tiene valor en el XML del documento electrónico. Por favor verifique que la etiqueta Xml use mayúsculas y minúsculas según resolución",
                                    .Fuente = "FacturaElectronica",
                                    .Observaciones = "",
                                    .PathFuente = ""
                                },
                                New ResultValidation With {
                                    .Clase = "NOTIFICACION",
                                    .Codigo = "RVC019",
                                    .Descripcion = "El código de CUPS se puede validar con el diagnóstico principal.",
                                    .Fuente = "Rips",
                                    .Observaciones = "",
                                    .PathFuente = ""
                                },
                                New ResultValidation With {
                                    .Clase = "NOTIFICACION",
                                    .Codigo = "RVC059",
                                    .Descripcion = "El código de CUPS puede ser validado con el grupo de servicio, servicio, finalidad o causa.",
                                    .Fuente = "Rips",
                                    .Observaciones = "",
                                    .PathFuente = ""
                                }
        }

        Dim validationResults = If(queryCUV?.ResultadosValidacion IsNot Nothing AndAlso queryCUV.ResultadosValidacion.Any(), queryCUV.ResultadosValidacion, defaultValidationResults)

        ' Construir la respuesta a retornar
        Dim responseResult As New LoadRIPSResultModel With {
            .ResultState = True,
            .ProcesoId = If(queryCUV IsNot Nothing AndAlso queryCUV.ProcesoId > 0, queryCUV.ProcesoId, electronicRIPS.Id),
            .NumFactura = numFacturaValue,
            .CodigoUnicoValidacion = If(String.IsNullOrEmpty(electronicRIPS?.CUV), queryCUV?.CodigoUnicoValidacion, electronicRIPS.CUV),
            .FechaRadicacion = dateUtc.ToString("o"),
            .RutaArchivos = Nothing,
            .Ambiente = queryCUV?.Ambiente,
            .Modulo = queryCUV?.Modulo,
            .ModalidadPago = queryCUV?.ModalidadPago,
            .PeriodoAtencion = queryCUV?.PeriodoAtencion,
            .ResultadosValidacion = validationResults
        }

        associatedDocument.Data = responseResult
        Return associatedDocument
    End Function

    ''' <summary>
    ''' Obtiene el objeto ElectronicRIPSModel deserializado desde Cosmos DB por su identificador
    ''' </summary>
    ''' <param name="idCosmosDb">Identificador único del documento en Cosmos DB</param>
    ''' <returns>Resultado con el objeto ElectronicRIPSModel deserializado</returns>
    Public Async Function GetObjectJsonRIPSbyId(idCosmosDb As String) As Task(Of ActionResult(Of ElectronicRIPSModel)) Implements IRIPSPlaneAdminService.GetObjectJsonRIPSbyId
        Try
            If String.IsNullOrEmpty(idCosmosDb) Then
                Throw New ArgumentNullException(NameOf(idCosmosDb))
            End If
            ' Obtener el objeto Query que contiene el JSON
            Dim Query = Await _rIPSCosmosDbModelRepository.GetJsonRIPSByIdAsync(idCosmosDb)

            If Query Is Nothing Then
                Return New ActionResult(Of ElectronicRIPSModel) With {.StateResult = False, .Message = $"No se encontró el Json @{idCosmosDb}"}
            End If

            ' Verificar si JsonRIPS ya es un objeto RIPSModel
            If TypeOf Query?.JsonRIPS Is RIPSModel Then
                ' Acceder directamente a la propiedad rips
                Dim ripsModel As RIPSModel = CType(Query?.JsonRIPS, RIPSModel)

                ' Verificar si ripsModel.rips no es Nothing
                If ripsModel?.rips Is Nothing Then
                    Return New ActionResult(Of ElectronicRIPSModel) With {.StateResult = False, .Message = "No se encontró el objeto ElectronicRIPSModel dentro de rips"}
                End If

                ' Obtener el objeto ElectronicRIPSModel desde la propiedad rips
                Dim ripsObject As ElectronicRIPSModel = ripsModel.rips

                ' Retornar el objeto deserializado como parte de la respuesta
                Return New ActionResult(Of ElectronicRIPSModel) With {.StateResult = True, .ObjectEmbbeded = ripsObject, .Message = "Proceso exitoso"}
            Else
                ' Si JsonRIPS no es un RIPSModel, manejar el caso de otra manera (posiblemente JSON string)
                Return New ActionResult(Of ElectronicRIPSModel) With {.StateResult = False, .Message = "Error: El valor de JsonRIPS no es un RIPSModel"}
            End If
        Catch ex As Exception
            Return New ActionResult(Of ElectronicRIPSModel) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "Function Privates"

    ''' <summary>
    ''' Convierte los parámetros de configuración a formato XML para ser enviados a procedimientos almacenados
    ''' </summary>
    ''' <param name="IPSNit">NIT de la IPS prestadora de servicios</param>
    ''' <param name="RadicateInvoiceId">Identificador del radicado de factura</param>
    ''' <param name="CodificationType">Tipo de codificación a utilizar</param>
    ''' <param name="ServiceCode">Código del servicio</param>
    ''' <returns>String con los parámetros en formato XML</returns>
    Private Function ConvertToXmlParameters(IPSNit As String, RadicateInvoiceId As Integer, CodificationType As String, ServiceCode As String) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")
        builder.Append("<IPSNit>" & IPSNit & "</IPSNit>")
        builder.Append("<RadicateInvoiceId>" & RadicateInvoiceId & "</RadicateInvoiceId>")
        builder.Append("<CodificationType>" & CodificationType & "</CodificationType>")
        builder.Append("<ServiceCode>" & ServiceCode & "</ServiceCode>")
        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Convierte una lista de identificadores de facturas a formato XML para ser enviada a procedimientos almacenados
    ''' </summary>
    ''' <param name="InvoicesList">Lista de identificadores de facturas</param>
    ''' <returns>String con los identificadores de facturas en formato XML</returns>
    Private Function ConvertToXmlInvoices(InvoicesList As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()
        For Each InvoiceId In InvoicesList
            builder.Append("<Data>")
            builder.Append("<InvoiceId>" & InvoiceId & "</InvoiceId>")
            builder.Append("</Data>")
        Next
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Convierte una lista de números de admisión a formato XML para ser enviada a procedimientos almacenados
    ''' </summary>
    ''' <param name="InvoicesList">Lista de números de admisión</param>
    ''' <returns>String con los números de admisión en formato XML</returns>
    Private Function ConvertToXmlAdmissionNUmber(InvoicesList As List(Of String)) As String
        Dim builder As StringBuilder = New StringBuilder()
        For Each AdmissionNumber In InvoicesList
            builder.Append("<Data>")
            builder.Append("<AdmissionNumber>" & AdmissionNumber.ToString.Trim & "</AdmissionNumber>")
            builder.Append("</Data>")
        Next
        Return builder.ToString
    End Function

    Function GenerateAFFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateAFFileData", RadicateInvoiceId, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateAFFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateAFFileData_Result With
                                {
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .AdmissionDate = dr("AdmissionDate").ToString(),
                                   .InvoiceDate = dr("InvoiceDate").ToString(),
                                   .OutputDate = dr("OutputDate").ToString(),
                                   .HealthEntityCode = dr("HealthEntityCode").ToString(),
                                   .ThirdPartyName = dr("ThirdPartyName").ToString(),
                                   .ContractNumber = dr("ContractNumber").ToString(),
                                   .CareGroupName = dr("CareGroupName").ToString(),
                                   .PolicyNumber = dr("PolicyNumber").ToString(),
                                   .TotalPatientSalesPrice = CDec(dr("TotalPatientSalesPrice")),
                                   .CommissionValue = CDec(dr("CommissionValue")),
                                   .PatientDiscount = CDec(dr("PatientDiscount")),
                                   .ThirdPartySalesValue = CDec(dr("ThirdPartySalesValue"))
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.AFStructure(resultStore, session)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AF{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AF: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateUSFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateUSFileData", RadicateInvoiceId, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateUSFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateUSFileData_Result With
                                {
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .HealthEntityCode = dr("HealthEntityCode").ToString(),
                                   .UserType = CInt(dr("UserType")),
                                   .FirstLastName = dr("FirstLastName").ToString(),
                                   .SecondLastName = dr("SecondLastName").ToString(),
                                   .FirstName = dr("FirstName").ToString(),
                                   .SecondName = dr("SecondName").ToString(),
                                   .Age = CInt(dr("Age")),
                                   .UnitMeasureAge = CInt(dr("UnitMeasureAge")),
                                   .Gender = dr("Gender").ToString(),
                                   .Department = dr("Department").ToString(),
                                   .City = dr("City").ToString(),
                                   .ResidentialZone = dr("ResidentialZone").ToString()
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.USStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("US{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo US: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateACFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, DetailPackage As Boolean, session As SessionValues, ServiceCode As String) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateACFileData", RadicateInvoiceId, XmlInvoices, DetailPackage, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateACFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateACFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .ServiceDate = dr("ServiceDate").ToString(),
                                   .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                                   .RIPSCode = dr("RIPSCode").ToString(),
                                   .ConsultationPurpose = dr("ConsultationPurpose").ToString(),
                                   .ExternalCause = dr("ExternalCause").ToString(),
                                   .MainDiagnosticCode = dr("MainDiagnosticCode").ToString(),
                                   .DiagnosticType = CInt(dr("DiagnosticType")),
                                   .TotalSalesPrice = CDec(dr("TotalSalesPrice")),
                                   .ModeratorFee = CDec(dr("ModeratorFee")),
                                   .NetToPay = CDec(dr("NetToPay")),
                                   .DiagnosticCodeRel1 = dr("DiagnosticCodeRel1").ToString(),
                                   .DiagnosticCodeRel2 = dr("DiagnosticCodeRel2").ToString(),
                                   .DiagnosticCodeRel3 = dr("DiagnosticCodeRel3").ToString(),
                                   .ManualRateCode = dr("ManualRateCode").ToString()
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.ACStructure(resultStore, ServiceCode)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AC{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AC: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function


    ''' <summary>
    ''' Funcion para generar el archivo AP
    ''' </summary>
    ''' <param name="RadicateInvoiceId"></param>
    ''' <param name="XmlInvoices"></param>
    ''' <param name="ConsecutiveRadicateInvoice"></param>
    ''' <param name="ServiceCode"></param>
    ''' <param name="DetailPackage"></param>
    ''' <param name="session"></param>
    ''' <param name="maxBatchSize"></param>
    ''' <returns></returns>
    Function GenerateAPFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, ServiceCode As String, DetailPackage As Boolean, session As SessionValues, Optional maxBatchSize As Integer = 10000) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateAPFileData", RadicateInvoiceId, XmlInvoices, DetailPackage, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultPlane As New StringBuilder()
                Dim rowCount As Integer = data.Rows.Count
                ' Calcular el batchSize basado en la cantidad de registros (proporcional)
                Dim batchSize As Integer = Math.Min(Math.Max(rowCount \ 10, 1000), maxBatchSize) ' Usa 10% de los registros, pero mínimo 1000 y máximo maxBatchSize

                For batchStart As Integer = 0 To rowCount - 1 Step batchSize
                    Dim batchEnd As Integer = Math.Min(batchStart + batchSize - 1, rowCount - 1)
                    Dim resultStore As New List(Of SP_GenerateAPFileData_Result)
                    For i As Integer = batchStart To batchEnd
                        Dim dr As DataRow = data.Rows(i)
                        Dim item As New SP_GenerateAPFileData_Result With {
                        .InvoiceNumber = dr("InvoiceNumber").ToString(),
                        .IPSCode = dr("IPSCode").ToString(),
                        .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                        .IdentificationNumber = dr("IdentificationNumber").ToString(),
                        .ServiceDate = dr("ServiceDate").ToString(),
                        .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                        .RIPSCode = dr("RIPSCode").ToString(),
                        .CUPSCode = dr("CUPSCode").ToString(),
                        .ManualRateCode = dr("ManualRateCode").ToString(),
                        .ScopeRealization = CInt(dr("ScopeRealization")),
                        .ProcedutePurpose = CInt(dr("ProcedutePurpose")),
                        .PersonalService = dr("PersonalService").ToString(),
                        .MainDiagnosticCode = dr("MainDiagnosticCode").ToString(),
                        .Complication = dr("Complication").ToString(),
                        .FormSurgicalAct = CInt(dr("FormSurgicalAct")),
                        .TotalSalesPrice = CDec(dr("TotalSalesPrice"))
                        }
                        resultStore.Add(item)
                    Next
                    If resultStore.Count > 0 Then
                        Dim StrBuilderData = _ripsPlaneService.APStructure(resultStore, ServiceCode)
                        resultPlane.AppendLine(StrBuilderData.ToString())
                    End If
                Next
                Return New ActionMessageResult(Of StringBuilder) With {
                .StateResult = True,
                .Message = String.Format("AP{0}", ConsecutiveRadicateInvoice),
                .ObjectEmbbeded = resultPlane
            }
            End If
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {
            .StateResult = False,
            .Message = String.Format("Error al generar el Archivo AP: {0}", Utils.GetInnerExceptionMessageToString(ex))
        }
        End Try
    End Function


    'Funcion para generar el archivo AD
    Function GenerateADFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, DetailPackage As Boolean, session As SessionValues, ServiceCode As String) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateADFileData", RadicateInvoiceId, XmlInvoices, DetailPackage, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateADFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateADFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .RIPSConcept = dr("RIPSConcept").ToString(),
                                   .Quantity = CInt(dr("Quantity")),
                                   .TotalSalesPrice = CDec(dr("TotalSalesPrice")),
                                   .NetToPay = CDec(dr("NetToPay"))
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.ADStructure(resultStore, ServiceCode)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AD{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AD: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateATFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, ServiceCode As String, DetailPackage As Boolean, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateATFileData", RadicateInvoiceId, XmlInvoices, DetailPackage, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateATFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateATFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                                   .TypeService = CInt(dr("TypeService")),
                                   .RIPSCode = dr("RIPSCode").ToString(),
                                   .CUPSCode = dr("CUPSCode").ToString(),
                                   .CUPSDescription = dr("CUPSDescription").ToString(),
                                   .ManualRateCode = dr("ManualRateCode").ToString(),
                                   .ManualRateName = dr("ManualRateName").ToString(),
                                   .InvoicedQuantity = CInt(dr("InvoicedQuantity")),
                                   .TotalSalesPrice = CDec(dr("TotalSalesPrice")),
                                   .GrandTotalSalesPrice = CDec(dr("GrandTotalSalesPrice"))
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.ATStructure(resultStore, ServiceCode)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AT{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AT: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateANFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateANFileData", RadicateInvoiceId, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateANFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateANFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .BirthDate = dr("BirthDate").ToString(),
                                   .BirthTime = dr("BirthTime").ToString(),
                                   .GestationalAge = CInt(dr("GestationalAge")),
                                   .AntenatalControl = CInt(dr("AntenatalControl")),
                                   .Gender = dr("Gender").ToString(),
                                   .Weight = CDec(dr("Weight")),
                                   .DiagnosticCode = dr("DiagnosticCode").ToString(),
                                   .CauseDeath = dr("CauseDeath").ToString(),
                                   .DeathDate = dr("DeathDate").ToString(),
                                   .DeathTime = dr("DeathTime").ToString()
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.ANStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AN{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AN: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateAUFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateAUFileData", RadicateInvoiceId, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateAUFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateAUFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .AdmissionDate = dr("AdmissionDate").ToString(),
                                   .AdmissionTime = dr("AdmissionTime").ToString(),
                                   .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                                   .ExternalCause = dr("ExternalCause").ToString(),
                                   .Diagnostic = dr("Diagnostic").ToString(),
                                   .UserDestination = dr("UserDestination").ToString(),
                                   .OutputState = CInt(dr("OutputState")),
                                   .CauseDeath = dr("CauseDeath").ToString(),
                                   .OutDate = dr("OutDate").ToString(),
                                   .OutTime = dr("OutTime").ToString()
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.AUStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AU{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AU: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateAHFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateAHFileData", RadicateInvoiceId, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateAHFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateAHFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .ViaIncome = CInt(dr("ViaIncome")),
                                   .AdmissionDate = dr("AdmissionDate").ToString(),
                                   .AdmissionTime = dr("AdmissionTime").ToString(),
                                   .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                                   .ExternalCause = dr("ExternalCause").ToString(),
                                   .DiagnosticAdmission = dr("DiagnosticAdmission").ToString(),
                                   .DiagnosticEgress = dr("DiagnosticEgress").ToString(),
                                   .DiagnosticComplication = dr("DiagnosticComplication").ToString(),
                                   .OutputState = CInt(dr("OutputState")),
                                   .CauseDeath = dr("CauseDeath").ToString(),
                                   .OutDate = dr("OutDate").ToString(),
                                   .OutTime = dr("OutTime").ToString()
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.AHStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AH{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AH: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateAMFile(RadicateInvoiceId As Integer, XmlInvoices As String, ConsecutiveRadicateInvoice As String, CodificationType As String, DetailPackage As Boolean, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateAMFileData", RadicateInvoiceId, XmlInvoices, DetailPackage, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateAMFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateAMFileData_Result With
                                {
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .HealthEntityCode = dr("HealthEntityCode").ToString(),
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IdentificationTypeCode = dr("IdentificationTypeCode").ToString(),
                                   .IdentificationNumber = dr("IdentificationNumber").ToString(),
                                   .AuthorizationNumber = dr("AuthorizationNumber").ToString(),
                                   .Code = dr("Code").ToString(),
                                   .CodeCUM = dr("CodeCUM").ToString(),
                                   .CodeAlternative = dr("CodeAlternative").ToString(),
                                   .CodeAlternativeTwo = dr("CodeAlternativeTwo").ToString(),
                                   .ProductId = CInt(dr("ProductId")),
                                   .MedicationType = CInt(dr("MedicationType")),
                                   .DrugName = dr("DrugName").ToString(),
                                   .PharmaceuticalForm = dr("PharmaceuticalForm").ToString(),
                                   .Concentration = dr("Concentration").ToString(),
                                   .UnitMeasure = dr("UnitMeasure").ToString(),
                                   .InvoicedQuantity = CInt(dr("InvoicedQuantity")),
                                   .TotalSalesPrice = CDec(dr("TotalSalesPrice")),
                                   .GrandTotalSalesPrice = CDec(dr("GrandTotalSalesPrice"))
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.AMStructure(resultStore, CodificationType)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("AM{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo AM: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateFURIPS1File(RadicateInvoiceId As Integer, XmlParameters As String, XmlInvoices As String, session As SessionValues, Optional fechaCorte As DateTime? = Nothing) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateFURIPS1FileData", XmlParameters, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateFURIPS1FileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateFURIPS1FileData_Result With
                                {
                                   .NumeroRadicadoAnterior = dr("NumeroRadicadoAnterior").ToString(),
                                   .RespuestaGlosa = dr("RespuestaGlosa").ToString(),
                                   .NumeroFactura = dr("NumeroFactura").ToString(),
                                   .ConsecutivoReclamacion = dr("ConsecutivoReclamacion").ToString(),
                                   .PrestadorServicioSalud = dr("PrestadorServicioSalud").ToString(),
                                   .PrimerApellidoVictima = dr("PrimerApellidoVictima").ToString(),
                                   .SegundoApellidoVictima = dr("SegundoApellidoVictima").ToString(),
                                   .PrimerNombreVictima = dr("PrimerNombreVictima").ToString(),
                                   .SegundoNombreVictima = dr("SegundoNombreVictima").ToString(),
                                   .TipoDocumentoVictima = dr("TipoDocumentoVictima").ToString(),
                                   .NumeroDocumentoVictima = dr("NumeroDocumentoVictima").ToString(),
                                   .FechaNacimientoVictima = dr("FechaNacimientoVictima").ToString(),
                                   .FechaFallecimientoVictima = dr("FechaFallecimientoVictima").ToString(),
                                   .SexoVictima = dr("SexoVictima").ToString(),
                                   .DireccionVictima = dr("DireccionVictima").ToString(),
                                   .DepartamentoResidenciaVictima = dr("DepartamentoResidenciaVictima").ToString(),
                                   .CodigoMunicipioVictima = dr("CodigoMunicipioVictima").ToString(),
                                   .TelefonoVictima = dr("TelefonoVictima").ToString(),
                                   .CondicionVictima = dr("CondicionVictima").ToString().ToString(),
                                   .NaturalezaEvento = dr("NaturalezaEvento").ToString(),
                                   .DescripcionOtherEvento = dr("DescripcionOtherEvento").ToString(),
                                   .DireccionEvento = dr("DireccionEvento").ToString(),
                                   .FechaEvento = dr("FechaEvento").ToString(),
                                   .HoraEvento = dr("HoraEvento").ToString(),
                                   .DepartamentoEvento = dr("DepartamentoEvento").ToString(),
                                   .CiudadEvento = dr("CiudadEvento").ToString(),
                                   .ZonaEvento = dr("ZonaEvento").ToString(),
                                   .EstadoAsegurado = dr("EstadoAsegurado").ToString(),
                                   .Marca = dr("Marca").ToString(),
                                   .Placa = dr("Placa").ToString(),
                                   .TipoVehiculo = dr("TipoVehiculo").ToString(),
                                   .CodigoAseguradora = dr("CodigoAseguradora").ToString(),
                                   .NumeroSOAT = dr("NumeroSOAT").ToString(),
                                   .FechaInicioVigenciaPoliza = dr("FechaInicioVigenciaPoliza").ToString(),
                                   .FechaFinVigenciaPoliza = dr("FechaFinVigenciaPoliza").ToString(),
                                   .NumeroRadicadoSIRAS = dr("NumeroRadicadoSIRAS").ToString(),
                                   .IntervencionAutoridad = dr("IntervencionAutoridad").ToString(),
                                   .CobroExcedente = dr("CobroExcedente").ToString(),
                                   .CodigoCupsServicio = dr("CodigoCupsServicio").ToString(),
                                   .ComplejidadProcedimientoQuirurgico = dr("ComplejidadProcedimientoQuirurgico").ToString(),
                                   .CodigoCupsProcedimientoQuirurgicoPrincipal = dr("CodigoCupsProcedimientoQuirurgicoPrincipal").ToString(),
                                   .CodigoCupsProcedimientoQuirurgicoSecundario = dr("CodigoCupsProcedimientoQuirurgicoSecundario").ToString(),
                                   .SePrestoServicioUCI = dr("SePrestoServicioUCI").ToString(),
                                   .DiasDeUciReclamados = dr("DiasDeUciReclamados").ToString(),
                                   .TipoDocumentoPropietario = dr("TipoDocumentoPropietario").ToString(),
                                   .NumeroDocumentoPropietario = dr("NumeroDocumentoPropietario").ToString(),
                                   .PrimerApellidoPropietario = dr("PrimerApellidoPropietario").ToString(),
                                   .SegundoApellidoPropietario = dr("SegundoApellidoPropietario").ToString(),
                                   .PrimerNombrePropietario = dr("PrimerNombrePropietario").ToString(),
                                   .SegundoNombrePropietario = dr("SegundoNombrePropietario").ToString(),
                                   .DireccionPropietario = dr("DireccionPropietario").ToString(),
                                   .TelefonoPropietario = dr("TelefonoPropietario").ToString(),
                                   .DepartamentoPropietario = dr("DepartamentoPropietario").ToString(),
                                   .MunicipioPropietario = dr("MunicipioPropietario").ToString(),
                                   .PrimerApellidoConductor = dr("PrimerApellidoConductor").ToString(),
                                   .SegundoApellidoConductor = dr("SegundoApellidoConductor").ToString(),
                                   .PrimerNombreConductor = dr("PrimerNombreConductor").ToString(),
                                   .SegundoNombreConductor = dr("SegundoNombreConductor").ToString(),
                                   .TipoDocumentoConductor = dr("TipoDocumentoConductor").ToString(),
                                   .NumeroDocumentoConductor = dr("NumeroDocumentoConductor").ToString(),
                                   .DireccionConductor = dr("DireccionConductor").ToString(),
                                   .DepartamentoConductor = dr("DepartamentoConductor").ToString(),
                                   .MunicipioConductor = dr("MunicipioConductor").ToString(),
                                   .TelefonoConductor = dr("TelefonoConductor").ToString(),
                                   .TipoReferencia = dr("TipoReferencia").ToString(),
                                   .FechaRemison = dr("FechaRemison").ToString(),
                                   .HoraSalida = dr("HoraSalida").ToString(),
                                   .CodigoHabilitacionPrestador = dr("CodigoHabilitacionPrestador").ToString(),
                                   .ProfesionalRemite = dr("ProfesionalRemite").ToString(),
                                   .CargoProfesionalRemite = dr("CargoProfesionalRemite").ToString(),
                                   .FechaIngreso = dr("FechaIngreso").ToString(),
                                   .HoraIngreso = dr("HoraIngreso").ToString(),
                                   .CodigoHabilitacionRecibe = dr("CodigoHabilitacionRecibe").ToString(),
                                   .ProfesionalRecibe = dr("ProfesionalRecibe").ToString(),
                                   .CargoProfesionalRecibe = dr("CargoProfesionalRecibe").ToString(),
                                   .PlacaTrasladoInterinstitucional = dr("PlacaTrasladoInterinstitucional").ToString(),
                                   .PlacaAmbulancia = dr("PlacaAmbulancia").ToString(),
                                   .TransporteDesde = dr("TransporteDesde").ToString(),
                                   .TransporteHasta = dr("TransporteHasta").ToString(),
                                   .TipoServicioAmbulancia = dr("TipoServicioAmbulancia").ToString(),
                                   .ZonaRecogeAmbulancia = dr("ZonaRecogeAmbulancia").ToString(),
                                   .FechaIngresoVictima = dr("FechaIngresoVictima").ToString(),
                                   .HoraIngresoVictima = dr("HoraIngresoVictima").ToString(),
                                   .FechaEgresoVictima = dr("FechaEgresoVictima").ToString(),
                                   .HoraEgresoVictima = dr("HoraEgresoVictima").ToString(),
                                   .CodigoDiagnosticoIngresoPrincipal = dr("CodigoDiagnosticoIngresoPrincipal").ToString(),
                                   .CodigoDiagnosticoIngresoUno = dr("CodigoDiagnosticoIngresoUno").ToString(),
                                   .CodigoDiagnosticoIngresoDos = dr("CodigoDiagnosticoIngresoDos").ToString(),
                                   .CodigoDiagnosticoEgresoPrincipal = dr("CodigoDiagnosticoEgresoPrincipal").ToString(),
                                   .CodigoDiagnosticoEgresoUno = dr("CodigoDiagnosticoEgresoUno").ToString(),
                                   .CodigoDiagnosticoEgresoDos = dr("CodigoDiagnosticoEgresoDos").ToString(),
                                   .PrimerApellidoMedico = dr("PrimerApellidoMedico").ToString(),
                                   .SegundoApellidoMedico = dr("SegundoApellidoMedico").ToString(),
                                   .PrimerNombreMedico = dr("PrimerNombreMedico").ToString(),
                                   .SegundoNombreMedico = dr("SegundoNombreMedico").ToString(),
                                   .TipoDocumentoMedico = dr("TipoDocumentoMedico").ToString(),
                                   .NumeroDocumentoMedico = dr("NumeroDocumentoMedico").ToString(),
                                   .NumeroRegistroMedico = dr("NumeroRegistroMedico").ToString(),
                                   .TotalFacturadoGastosMedicos = dr("TotalFacturadoGastosMedicos").ToString(),
                                   .TotalReclamadoGastosMedicos = dr("TotalReclamadoGastosMedicos").ToString(),
                                   .TotalFacturadoMovilizacion = dr("TotalFacturadoMovilizacion").ToString(),
                                   .TotalReclamadoMovilizacion = dr("TotalReclamadoMovilizacion").ToString(),
                                   .TotalFolios = dr("TotalFolios").ToString(),
                                   .ManifestacionServiciosHabilitados = dr("ManifestacionServiciosHabilitados").ToString(),
                                   .DescripcionEvento = dr("DescripcionEvento").ToString()
                                }).ToList()
                If resultStore.Any() Then
                    Dim fileName = String.Empty
                    Dim row = resultStore.FirstOrDefault()
                    'nombre del archivo plano
                    fileName = "FURIPS1" & RTrim(row.PrestadorServicioSalud) & fechaCorte.Value.ToString("ddMMyyyy")

                    Dim StrBuilderData = _ripsPlaneService.FURIPS1Structure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = fileName, .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo FURIPS1: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateFURIPS2File(RadicateInvoiceId As Integer, XmlParameters As String, XmlInvoices As String, session As SessionValues, Optional fechaCorte As DateTime? = Nothing) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateFURIPS2FileData", XmlParameters, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateFURIPS2FileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateFURIPS2FileData_Result With
                                {
                                   .PrestadorServicioSalud = dr("PrestadorServicioSalud").ToString(),
                                   .NumeroFactura = dr("NumeroFactura").ToString(),
                                   .Ingreso = dr("Consecutivo").ToString(),
                                   .Tipo = dr("Tipo").ToString(),
                                   .CodigoCUM = dr("CodigoCUM").ToString(),
                                   .Descripcion = dr("Descripcion").ToString(),
                                   .Cantidad = CInt(dr("Cantidad")),
                                   .ValorUnitario = CDec(dr("ValorUnitario")),
                                   .SubTotal = CDec(dr("SubTotal")),
                                   .Total = CDec(dr("Total"))
                                }).ToList()

                If resultStore.Any() Then
                    Dim fileName = String.Empty
                    Dim row = resultStore.FirstOrDefault()
                    'nombre del archivo plano
                    fileName = "FURIPS2" & RTrim(row.PrestadorServicioSalud) & fechaCorte.Value.ToString("ddMMyyyy")

                    Dim StrBuilderData = _ripsPlaneService.FURIPS2Structure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = fileName, .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo FURIPS2: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateFURTRANFile(XmlInvoices As String, session As SessionValues, Optional fechaCorte As DateTime? = Nothing) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFURTRANFileData("SP_GenerateFURTRANFileData", XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateFURTRANFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateFURTRANFileData_Result With
                                {
                                   .NumeroRadicadoAnterior = dr("NumeroRadicadoAnterior").ToString(),
                                   .RespuestaObjecionGlosaObjecion = dr("RespuestaObjecionGlosaObjecion").ToString(),
                                   .NumeroFactura = dr("NumeroFactura").ToString(),
                                   .CodigoHabilitacion = dr("CodigoHabilitacion").ToString(),
                                   .PrimerApellidoConductor = dr("PrimerApellidoConductor").ToString(),
                                   .SegundoApellidoConductor = dr("SegundoApellidoConductor").ToString(),
                                   .PrimerNombreConductor = dr("PrimerNombreConductor").ToString(),
                                   .SegundoNombreConductor = dr("SegundoNombreConductor").ToString(),
                                   .TipoDocumentoReclamante_o_ConductorAmbulancia = dr("TipoDocumentoReclamante_o_ConductorAmbulancia").ToString(),
                                   .NumeroDocumentoReclamante_o_ConductorAmbulancia = dr("NumeroDocumentoReclamante_o_ConductorAmbulancia").ToString(),
                                   .TipoVehiculo = dr("TipoVehiculo").ToString(),
                                   .PlacaVehiculo = dr("PlacaVehiculo").ToString(),
                                   .DireccionReclamante = dr("DireccionReclamante").ToString(),
                                   .TelefonoReclamante = dr("TelefonoReclamante").ToString(),
                                   .CodigoDepartamento = dr("CodigoDepartamento").ToString(),
                                   .CodigoMunicipio = dr("CodigoMunicipio").ToString(),
                                   .TipoDocumentoVictima = dr("TipoDocumentoVictima").ToString(),
                                   .NumeroDocumentoVictima = dr("NumeroDocumentoVictima").ToString(),
                                   .PrimerNombreVictima = dr("PrimerNombreVictima").ToString(),
                                   .SegundoNombreVictima = dr("SegundoNombreVictima").ToString(),
                                   .PrimerApellidoVictima = dr("PrimerApellidoVictima").ToString(),
                                   .SegundoApellidoVictima = dr("SegundoApellidoVictima").ToString(),
                                   .FechaNacimientoVictima = dr("FechaNacimientoVictima").ToString(),
                                   .SexoVictima = dr("SexoVictima").ToString(),
                                   .TipoEvento = dr("TipoEvento").ToString(),
                                   .DireccionLugarVictima = dr("DireccionLugarVictima").ToString(),
                                   .DepartamentoLugarVictima = dr("DepartamentoLugarVictima").ToString(),
                                   .MunicipioLugarVictima = dr("MunicipioLugarVictima").ToString(),
                                   .ZonaRecogeVictima = dr("ZonaRecogeVictima").ToString(),
                                   .FechaTrasladoVictima = dr("FechaTrasladoVictima").ToString(),
                                   .HoraTrasladoVictima = dr("HoraTrasladoVictima").ToString(),
                                   .CodigoHabilitacionIPSVictima = dr("CodigoHabilitacionIPSVictima").ToString(),
                                   .CodigoDepartamentoTrasladoVictima = dr("CodigoDepartamentoTrasladoVictima").ToString(),
                                   .CodigoMunicipioTrasladoVictima = dr("CodigoMunicipioTrasladoVictima").ToString(),
                                   .CondicionVictima = dr("CondicionVictima").ToString(),
                                   .EstadoAseguradoraVictima = dr("EstadoAseguradoraVictima").ToString(),
                                   .TipoVehiculoVictima = dr("TipoVehiculoVictima").ToString(),
                                   .PlacaVehiculoVictima = dr("PlacaVehiculoVictima").ToString(),
                                   .CodigoAseguradoraVictima = dr("CodigoAseguradoraVictima").ToString(),
                                   .NumeroPolizaSOAT = dr("NumeroPolizaSOAT").ToString(),
                                   .FechaInicioPoliza = dr("FechaInicioPoliza").ToString(),
                                   .FechaFinalPoliza = dr("FechaFinalPoliza").ToString(),
                                   .NumeroRadicadoSIRAS = dr("NumeroRadicadoSIRAS").ToString(),
                                   .ValorFacturado = CDec(dr("ValorFacturado").ToString()),
                                   .ValorReclamado = CDec(dr("ValorReclamado").ToString()),
                                   .ManifestacionServiciosHabilitados = dr("ManifestacionServiciosHabilitados").ToString()
                                }).ToList()
                If resultStore.Any() Then
                    Dim fileName = String.Empty
                    Dim row = resultStore.FirstOrDefault()
                    'nombre del archivo plano
                    fileName = "FURTRAN" & RTrim(row.CodigoHabilitacion) & fechaCorte.Value.ToString("ddMMyyyy")

                    Dim StrBuilderData = _ripsPlaneService.FURTRANStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = fileName, .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo FURTRAN: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Function GenerateMegaPlaneFile(XmlParameters As String, XmlInvoices As String, ConsecutiveRadicateInvoice As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
        Try
            Dim ds = Me.SP_GenerateFileData("SP_GenerateMegaPlaneFileData", XmlParameters, XmlInvoices, session)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim data As DataTable = ds.Tables(0)
                Dim resultStore As List(Of SP_GenerateMegaPlaneFileData_Result)
                resultStore = (From dr In data.Rows
                               Select New SP_GenerateMegaPlaneFileData_Result With
                                {
                                   .IPSCode = dr("IPSCode").ToString(),
                                   .IPSDocumentType = dr("IPSDocumentType").ToString(),
                                   .IPSNit = dr("IPSNit").ToString(),
                                   .IPSDigitVerification = dr("IPSDigitVerification").ToString(),
                                   .IPSName = dr("IPSName").ToString(),
                                   .CustomerDocumentType = dr("CustomerDocumentType").ToString(),
                                   .CustomerNit = dr("CustomerNit").ToString(),
                                   .CustomerDigitVerification = dr("CustomerDigitVerification").ToString(),
                                   .CustomerName = dr("CustomerName").ToString(),
                                   .InvoiceId = CInt(dr("InvoiceId").ToString()),
                                   .InvoiceDate = dr("InvoiceDate").ToString(),
                                   .InvoicePrefix = dr("InvoicePrefix").ToString(),
                                   .InvoiceNumber = dr("InvoiceNumber").ToString(),
                                   .InvoiceGrossValue = CDec(dr("InvoiceGrossValue")),
                                   .InvoiceModeratingFee = CDec(dr("InvoiceModeratingFee")),
                                   .InvoiceCopay = CDec(dr("InvoiceCopay")),
                                   .InvoiceNetValue = CDec(dr("InvoiceNetValue")),
                                   .DetailId = CInt(dr("DetailId").ToString()),
                                   .DetailInvoiceId = CInt(dr("DetailInvoiceId").ToString()),
                                   .DetailInvoiceNumber = dr("DetailInvoiceNumber").ToString(),
                                   .DetailAuthorizationNumber = dr("DetailAuthorizationNumber").ToString(),
                                   .DetailDocumentType = dr("DetailDocumentType").ToString(),
                                   .DetailDocumentNumber = dr("DetailDocumentNumber").ToString(),
                                   .DetailLastName = dr("DetailLastName").ToString(),
                                   .DetailFirstName = dr("DetailFirstName").ToString(),
                                   .DetailAdmissionDate = dr("DetailAdmissionDate").ToString(),
                                   .DetailOutputDate = dr("DetailOutputDate").ToString(),
                                   .DetailAttentionType = dr("DetailAttentionType").ToString(),
                                   .DetailCode = dr("DetailCode").ToString(),
                                   .DetailDescription = dr("DetailDescription").ToString(),
                                   .DetailQuantity = CInt(dr("DetailQuantity").ToString()),
                                   .DetailUnitValue = CDec(dr("DetailUnitValue")),
                                   .DetailTotalValue = CDec(dr("DetailTotalValue")),
                                   .DetailDiagnostic = dr("DetailDiagnostic").ToString(),
                                   .DetailModeratingFee = CDec(dr("DetailModeratingFee")),
                                   .DetailCopay = CDec(dr("DetailCopay"))
                                }).ToList()

                If resultStore.Count > 0 Then
                    Dim StrBuilderData = _ripsPlaneService.MegaPlaneStructure(resultStore)
                    Return New ActionMessageResult(Of StringBuilder) With {.StateResult = True, .Message = String.Format("MegaPlano-{0}", ConsecutiveRadicateInvoice), .ObjectEmbbeded = StrBuilderData}
                End If
            End If

            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False}
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = String.Format("Error al generar el Archivo MegaPlano: {0}", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    Private ReadOnly _lockObject As New Object()

    ''' <summary>
    ''' Agrega de forma segura (thread-safe) un archivo generado a la lista de archivos RIPS, solo si tiene resultado o mensaje de error
    ''' </summary>
    ''' <param name="ListPlaneRIPS">Lista de archivos RIPS generados (pasada por referencia)</param>
    ''' <param name="VarFile">Archivo generado con su resultado y contenido</param>
    Sub AddToListPlaneRIPS(ByRef ListPlaneRIPS As List(Of ActionMessageResult(Of StringBuilder)), VarFile As ActionMessageResult(Of StringBuilder))
        If VarFile.StateResult OrElse (Not VarFile.StateResult AndAlso Not String.IsNullOrEmpty(VarFile.Message)) Then
            SyncLock _lockObject
                ListPlaneRIPS.Add(VarFile)
            End SyncLock
        End If
    End Sub

#End Region

#Region "Datatables"

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado para generar datos de archivos RIPS con parámetros de radicado y detalle de paquete
    ''' </summary>
    ''' <param name="SP_Name">Nombre del procedimiento almacenado a ejecutar</param>
    ''' <param name="RadicateInvoiceId">Identificador del radicado de factura</param>
    ''' <param name="XmlInvoices">XML con la lista de facturas a procesar</param>
    ''' <param name="DetailPackage">Indica si se incluye el detalle de paquetes</param>
    ''' <param name="session">Variable de sesión con información del usuario y contexto</param>
    ''' <returns>DataSet con los resultados de la ejecución del procedimiento almacenado</returns>
    Private Function SP_GenerateFileData(SP_Name As String, RadicateInvoiceId As Integer, XmlInvoices As String, DetailPackage As Boolean, session As SessionValues) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Glosas].[{0}] {1}, '{2}',{3}", SP_Name, RadicateInvoiceId, XmlInvoices, DetailPackage)
            Dim dt = Me.GetDatatable(query, session, SP_Name)
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado para generar datos de archivos RIPS con parámetros XML configurables
    ''' </summary>
    ''' <param name="SP_Name">Nombre del procedimiento almacenado a ejecutar</param>
    ''' <param name="XmlParameters">XML con los parámetros de configuración (NIT, radicado, codificación, servicio)</param>
    ''' <param name="XmlInvoices">XML con la lista de facturas a procesar</param>
    ''' <param name="session">Variable de sesión con información del usuario y contexto</param>
    ''' <returns>DataSet con los resultados de la ejecución del procedimiento almacenado</returns>
    Private Function SP_GenerateFileData(SP_Name As String, XmlParameters As String, XmlInvoices As String, session As SessionValues) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Glosas].[{0}] '{1}', '{2}'", SP_Name, XmlParameters, XmlInvoices)
            Dim dt = Me.GetDatatable(query, session, SP_Name)
            If dt Is Nothing Then
                Return ds
            End If
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(
                New Exception(String.Format("SP_GenerateFileData falló para [Glosas].[{0}]: {1}", SP_Name, ex.Message), ex),
                "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado específico para generar datos del archivo FURTRAN
    ''' </summary>
    ''' <param name="SP_Name">Nombre del procedimiento almacenado a ejecutar</param>
    ''' <param name="XmlInvoices">XML con los números de admisión a procesar</param>
    ''' <param name="session">Variable de sesión con información del usuario y contexto</param>
    ''' <returns>DataSet con los resultados de la ejecución del procedimiento almacenado</returns>
    Private Function SP_GenerateFURTRANFileData(SP_Name As String, XmlInvoices As String, session As SessionValues) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Glosas].[{0}] '{1}'", SP_Name, XmlInvoices)
            Dim dt = Me.GetDatatable(query, session, SP_Name)
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta una consulta SQL y retorna los resultados en un DataTable
    ''' </summary>
    ''' <param name="Comando">Comando SQL a ejecutar (query o procedimiento almacenado)</param>
    ''' <param name="session">Variable de sesión con información del usuario y contexto para obtener la cadena de conexión</param>
    ''' <param name="nameDt">Nombre que se le asignará al DataTable resultante</param>
    ''' <returns>DataTable con los resultados de la consulta ejecutada</returns>
    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlClient.SqlConnection(connectionString)
            Try
                Dim cmd As New System.Data.SqlClient.SqlCommand()
                cmd.Connection = conexion
                cmd.CommandText = Comando
                cmd.CommandTimeout = 300
                Dim da = New SqlClient.SqlDataAdapter
                da.SelectCommand = cmd
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return Nothing
            End Try
        End Using
    End Function

#End Region

#Region "Upload RIPS to CosmosDB"

    Private Const OPENING_BALANCE_ENTITY_NAME As String = "InitialBalance"
    Private Const ONE_DAY_SECONDS As Long = 86400

    ''' <summary>
    ''' Cargue pequeño (≤ threshold). Internamente delega en UploadRipsBulkAsync.
    ''' </summary>
    Public Async Function UploadRipsSmallAsync(items As List(Of RipsUploadRequest),
                                                audit As AuditMessage) As Task(Of ActionResult(Of RipsBulkResponse)) Implements IRIPSPlaneAdminService.UploadRipsSmallAsync
        Return Await UploadRipsBulkAsync(Guid.NewGuid().ToString(), items, audit, CancellationToken.None)
    End Function

    ''' <summary>
    ''' Cargue masivo de RIPS. Política duplicados:
    '''   - Existente con _ts ≤ 24h → Skipped
    '''   - Existente con _ts > 24h → Overwritten (UPSERT con id existente)
    '''   - No existente → Created (UPSERT con id = numFactura-GUID)
    ''' </summary>
    Public Async Function UploadRipsBulkAsync(batchId As String,
                                               items As List(Of RipsUploadRequest),
                                               audit As AuditMessage,
                                               Optional cancellationToken As CancellationToken = Nothing) As Task(Of ActionResult(Of RipsBulkResponse)) Implements IRIPSPlaneAdminService.UploadRipsBulkAsync
        Try
            If _rIPSCosmosDbModelRepository Is Nothing Then
                Return New ActionResult(Of RipsBulkResponse) With {
                    .StateResult = False,
                    .Message = "RIPSCosmosDbModelRepository no resuelto"
                }
            End If

            If items Is Nothing OrElse items.Count = 0 Then
                Return New ActionResult(Of RipsBulkResponse) With {
                    .StateResult = False,
                    .Message = "Lista de items vacía"
                }
            End If

            Dim response As New RipsBulkResponse With {
                .BatchId = If(String.IsNullOrEmpty(batchId), Guid.NewGuid().ToString(), batchId),
                .TotalReceived = items.Count
            }

            Dim container As String = audit?.Company

            ' 1. Validar JSON + numFactura coincide
            ' El JSON que sube el usuario tiene shape ElectronicRIPSModel directo (sin wrapper rips).
            ' Aquí lo envolvemos en RIPSModel { rips = ... } para guardar en Cosmos.
            Dim valid As New List(Of Tuple(Of RipsUploadRequest, RIPSCosmosDbModel))
            For Each it In items
                Dim ripsModel As ElectronicRIPSModel = Nothing
                Try
                    ripsModel = JsonConvert.DeserializeObject(Of ElectronicRIPSModel)(it.JsonRipsRaw)
                Catch
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = it.NumFactura,
                        .Status = RipsUploadStatus.Failed,
                        .Message = "Formato incorrecto"
                    })
                    Continue For
                End Try

                If ripsModel Is Nothing OrElse
                   String.IsNullOrEmpty(ripsModel.numFactura) OrElse
                   String.IsNullOrEmpty(ripsModel.numDocumentoIdObligado) OrElse
                   ripsModel.usuarios Is Nothing OrElse
                   ripsModel.usuarios.Count = 0 Then
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = it.NumFactura,
                        .Status = RipsUploadStatus.Failed,
                        .Message = "Formato incorrecto"
                    })
                    Continue For
                End If

                If Not String.Equals(ripsModel.numFactura, it.NumFactura, StringComparison.OrdinalIgnoreCase) Then
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = it.NumFactura,
                        .Status = RipsUploadStatus.Failed,
                        .Message = String.Format("numFactura del JSON ({0}) no coincide con archivo ({1})", ripsModel.numFactura, it.NumFactura)
                    })
                    Continue For
                End If

                Dim xmlFevFile As String = Nothing
                Try
                    xmlFevFile = GetInitialBalanceAttachedDocumentBase64(it.InitialBalanceId, it.NumFactura)
                Catch ex As Exception
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = it.NumFactura,
                        .Status = RipsUploadStatus.Failed,
                        .Message = ex.Message
                    })
                    Continue For
                End Try

                If String.IsNullOrWhiteSpace(xmlFevFile) Then
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = it.NumFactura,
                        .Status = RipsUploadStatus.Failed,
                        .Message = "No se encontró el AttachedDocument XML de la factura en blob storage."
                    })
                    Continue For
                End If

                Dim envelope As New RIPSCosmosDbModel With {
                    .Container = container,
                    .Retry = 0,
                    .EntityName = OPENING_BALANCE_ENTITY_NAME,
                    .JsonRIPS = New RIPSModel With {.rips = ripsModel, .xmlFevFile = xmlFevFile}
                }
                valid.Add(Tuple.Create(it, envelope))
            Next

            If valid.Count = 0 Then
                response.Failed = response.Results.Where(Function(r) r.Status = RipsUploadStatus.Failed).Count()
                Return New ActionResult(Of RipsBulkResponse) With {
                    .StateResult = True,
                    .ObjectEmbbeded = response,
                    .Message = "Ningún item válido para subir"
                }
            End If

            ' 2. Query existentes para política duplicados
            Dim numFacturas = valid.Select(Function(p) p.Item1.NumFactura).ToList()
            Dim existing = Await _rIPSCosmosDbModelRepository.GetExistingByNumFacturaAsync(numFacturas, container)

            Dim nowSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds()

            Dim toUpsert As New List(Of RIPSCosmosDbModel)
            Dim numFacturaToStatus As New Dictionary(Of String, RipsUploadStatus)(StringComparer.OrdinalIgnoreCase)

            For Each p In valid
                Dim numF = p.Item1.NumFactura
                Dim env = p.Item2

                Dim info As RipsExistsInfo = Nothing
                If existing.TryGetValue(numF, info) Then
                    Dim ageSec = nowSeconds - info.Ts
                    If ageSec <= ONE_DAY_SECONDS Then
                        response.Results.Add(New RipsUploadResult With {
                            .NumFactura = numF,
                            .CosmosId = info.Id,
                            .Status = RipsUploadStatus.Skipped,
                            .Message = "El RIPS ya fue cargado previamente."
                        })
                        Continue For
                    Else
                        env.id = info.Id
                        numFacturaToStatus(numF) = RipsUploadStatus.Overwritten
                    End If
                Else
                    env.id = numF & "-" & Guid.NewGuid().ToString()
                    numFacturaToStatus(numF) = RipsUploadStatus.Created
                End If

                toUpsert.Add(env)
            Next

            ' 3. Upsert masivo
            If toUpsert.Count > 0 Then
                Dim bulkResult = Await _rIPSCosmosDbModelRepository.UpsertManyAsync(toUpsert, Nothing, cancellationToken)

                For Each ok In bulkResult.Succeeded
                    Dim env = ok.Item
                    Dim numF = If(env.JsonRIPS?.rips?.numFactura, String.Empty)
                    Dim status As RipsUploadStatus = RipsUploadStatus.Created
                    If numFacturaToStatus.ContainsKey(numF) Then
                        status = numFacturaToStatus(numF)
                    End If
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = numF,
                        .CosmosId = env.id,
                        .Status = status
                    })
                Next

                For Each fail In bulkResult.Failed
                    Dim env = fail.Item
                    Dim numF = If(env.JsonRIPS?.rips?.numFactura, String.Empty)
                    response.Results.Add(New RipsUploadResult With {
                        .NumFactura = numF,
                        .CosmosId = env.id,
                        .Status = RipsUploadStatus.Failed,
                        .Message = fail.ErrorMessage
                    })
                Next
            End If

            ' 4. Aggregates
            response.Succeeded = response.Results.Where(Function(r) r.Status = RipsUploadStatus.Created OrElse r.Status = RipsUploadStatus.Overwritten).Count()
            response.Skipped = response.Results.Where(Function(r) r.Status = RipsUploadStatus.Skipped).Count()
            response.Failed = response.Results.Where(Function(r) r.Status = RipsUploadStatus.Failed).Count()

            Return New ActionResult(Of RipsBulkResponse) With {
                .StateResult = True,
                .ObjectEmbbeded = response
            }

        Catch ex As Exception
            Return New ActionResult(Of RipsBulkResponse) With {
                .StateResult = False,
                .Message = Utils.GetInnerExceptionMessageToString(ex)
            }
        End Try
    End Function

    Private Function GetInitialBalanceAttachedDocumentBase64(initialBalanceId As Integer, invoiceNumber As String) As String
        If initialBalanceId <= 0 Then
            Throw New ArgumentException("InitialBalanceId no especificado para cargar xmlFevFile.")
        End If

        If String.IsNullOrWhiteSpace(invoiceNumber) Then
            Throw New ArgumentException("Número de factura vacío para cargar xmlFevFile.")
        End If

        If _portfolioInitialBalanceRepository Is Nothing Then
            Throw New InvalidOperationException("PortfolioInitialBalanceRepository no resuelto para cargar xmlFevFile.")
        End If

        If _storage Is Nothing Then
            Throw New InvalidOperationException("Storage no resuelto para cargar xmlFevFile.")
        End If

        Dim header = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceById(initialBalanceId)
        If header Is Nothing OrElse header.Id = 0 Then
            Throw New InvalidOperationException("Saldo inicial no encontrado para cargar xmlFevFile.")
        End If

        Dim filePath = BuildInitialBalanceInvoiceXmlFilePath(header, invoiceNumber)
        Dim fileBytes = ReadAttachedDocumentBytesWithFallback(filePath, invoiceNumber)

        If fileBytes Is Nothing Then
            Throw New InvalidOperationException("El XML encontrado en blob no es un AttachedDocument válido.")
        End If

        Return Convert.ToBase64String(fileBytes)
    End Function

    Private Function ReadAttachedDocumentBytesWithFallback(filePath As String, invoiceNumber As String) As Byte()
        Dim preferredFileName = invoiceNumber & ".xml"
        Dim preferredBytes = ReadFileIfExists(filePath, preferredFileName)
        If IsAttachedDocumentXml(preferredBytes) Then Return preferredBytes

        For Each fallbackPrefix In New String() {"fv", "FV", "ad", "AD"}
            Dim fallbackFileName = _storage.FindFirstFileName(filePath, fallbackPrefix, ".xml")
            If String.IsNullOrWhiteSpace(fallbackFileName) OrElse
                String.Equals(fallbackFileName, preferredFileName, StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If

            Dim fallbackBytes = ReadFileIfExists(filePath, fallbackFileName)
            If IsAttachedDocumentXml(fallbackBytes) Then Return fallbackBytes
        Next

        Return Nothing
    End Function

    Private Function ReadFileIfExists(filePath As String, fileName As String) As Byte()
        If String.IsNullOrWhiteSpace(fileName) OrElse _storage.ValidateIfNotExists(filePath, fileName) Then Return Nothing
        Return _storage.ReadFile(filePath, fileName)
    End Function

    Private Function IsAttachedDocumentXml(fileBytes As Byte()) As Boolean
        If fileBytes Is Nothing OrElse fileBytes.Length = 0 Then Return False

        Try
            Dim xmlString = Encoding.UTF8.GetString(fileBytes)
            Dim doc = XDocument.Parse(xmlString)
            Return doc.Root IsNot Nothing AndAlso String.Equals(doc.Root.Name.LocalName, "AttachedDocument", StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    Private Function BuildInitialBalanceInvoiceXmlFilePath(header As PortfolioInitialBalance, invoiceNumber As String) As String
        Dim container As String = ServerSessionValues.Current.CurrentContainer
        Return String.Format("E:\ProgramData\Indigo Technologies\ElectronicDocuments\{0}\001\{1}\{2}\Saldos Iniciales\{3}",
                             container, header.CreationDate.Year, header.CreationDate.Month, invoiceNumber)
    End Function

    ''' <summary>
    ''' Pre-check existencia RIPS en CosmosDB. Devuelve Existing/Missing por numFactura.
    ''' Reusa GetExistingByNumFacturaAsync (chunks 100 internamente).
    ''' </summary>
    Public Async Function CheckRipsExistAsync(invoiceNumbers As List(Of String),
                                              audit As AuditMessage) As Task(Of ActionResult(Of RipsCheckExistResponse)) Implements IRIPSPlaneAdminService.CheckRipsExistAsync
        Try
            Dim resp As New RipsCheckExistResponse
            If invoiceNumbers Is Nothing OrElse invoiceNumbers.Count = 0 Then
                Return New ActionResult(Of RipsCheckExistResponse) With {.StateResult = True, .ObjectEmbbeded = resp}
            End If
            If _rIPSCosmosDbModelRepository Is Nothing Then
                Return New ActionResult(Of RipsCheckExistResponse) With {
                    .StateResult = False,
                    .Message = "RIPSCosmosDbModelRepository no resuelto"
                }
            End If

            Dim container As String = audit?.Company
            Dim distinct = invoiceNumbers.
                Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
                Select(Function(n) n.Trim()).
                Distinct(StringComparer.OrdinalIgnoreCase).
                ToList()

            Dim existing = Await _rIPSCosmosDbModelRepository.GetExistingByNumFacturaAsync(distinct, container)

            For Each numF In distinct
                If existing.ContainsKey(numF) Then
                    resp.Existing.Add(numF)
                Else
                    resp.Missing.Add(numF)
                End If
            Next

            Return New ActionResult(Of RipsCheckExistResponse) With {
                .StateResult = True,
                .ObjectEmbbeded = resp
            }
        Catch ex As Exception
            Return New ActionResult(Of RipsCheckExistResponse) With {
                .StateResult = False,
                .Message = Utils.GetInnerExceptionMessageToString(ex)
            }
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _ripsPlaneService.Dispose()
            End If
            _ripsPlaneService = Nothing
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

#Region "Populate InitialBalanceInvoiceDetail (Saldos Iniciales)"

    ''' <summary>
    ''' Hidrata Cosmos por número de factura, parsea usuarios.servicios y carga InitialBalanceInvoiceDetail
    ''' (snapshot 9 cols por ServiceType, ADR-005/D26). Idempotente: DELETE detail rows existentes antes de INSERT.
    ''' Actualiza InitialBalanceInvoice.CosmosId + ObligatedPartyDocument + Status=2 (Confirmado).
    ''' Items sin consecutivo se descartan (D21).
    ''' </summary>
    Public Async Function PopulateInitialBalanceDetail(invoiceNumbers As List(Of String), audit As AuditMessage) As Task(Of ActionResult(Of List(Of RipsUploadResult))) Implements IRIPSPlaneAdminService.PopulateInitialBalanceDetail
        Dim results As New List(Of RipsUploadResult)
        If invoiceNumbers Is Nothing OrElse invoiceNumbers.Count = 0 Then
            Return New ActionResult(Of List(Of RipsUploadResult)) With {.StateResult = True, .ObjectEmbbeded = results}
        End If
        If _rIPSCosmosDbModelRepository Is Nothing OrElse _initialBalanceInvoiceRepository Is Nothing Then
            Return New ActionResult(Of List(Of RipsUploadResult)) With {
                .StateResult = False,
                .Message = "Servicio Cosmos o repositorio InitialBalanceInvoice no disponibles."
            }
        End If

        For Each invoiceNumber In invoiceNumbers
            Dim r As New RipsUploadResult With {.NumFactura = invoiceNumber}
            Try
                If String.IsNullOrWhiteSpace(invoiceNumber) Then
                    r.Status = RipsUploadStatus.Skipped
                    r.Message = "Número de factura vacío."
                    results.Add(r)
                    Continue For
                End If

                Dim ibi As InitialBalanceInvoice = _initialBalanceInvoiceRepository.GetByInvoiceNumber(invoiceNumber)
                If ibi Is Nothing OrElse ibi.Id = 0 Then
                    r.Status = RipsUploadStatus.Skipped
                    r.Message = "InitialBalanceInvoice header no encontrado para esta factura. Confirme primero el saldo inicial."
                    results.Add(r)
                    Continue For
                End If

                Dim cosmosDoc As RIPSCosmosDbModel = Await _rIPSCosmosDbModelRepository.GetJsonRIPSByDocNumber(invoiceNumber)
                If cosmosDoc Is Nothing OrElse cosmosDoc.JsonRIPS Is Nothing OrElse cosmosDoc.JsonRIPS.rips Is Nothing Then
                    r.Status = RipsUploadStatus.Skipped
                    r.Message = "Documento Cosmos no encontrado para esta factura."
                    results.Add(r)
                    Continue For
                End If

                ' Idempotencia: borrar detail previo (reupload, ADR-007).
                _initialBalanceInvoiceRepository.DeleteDetailsByInitialBalanceInvoiceId(ibi.Id)

                Dim usuarios = cosmosDoc.JsonRIPS.rips.usuarios
                If usuarios IsNot Nothing Then
                    For Each usuario In usuarios
                        If usuario.servicios Is Nothing Then Continue For
                        SaveConsultas(ibi.Id, usuario.consecutivo, usuario.servicios.consultas, audit)
                        SaveProcedimientos(ibi.Id, usuario.consecutivo, usuario.servicios.procedimientos, audit)
                        SaveUrgencias(ibi.Id, usuario.consecutivo, usuario.servicios.urgencias, audit)
                        SaveRecienNacidos(ibi.Id, usuario.consecutivo, usuario.servicios.recienNacidos, audit)
                        SaveMedicamentos(ibi.Id, usuario.consecutivo, usuario.servicios.medicamentos, audit)
                        SaveOtrosServicios(ibi.Id, usuario.consecutivo, usuario.servicios.otrosServicios, audit)
                        SaveHospitalizacion(ibi.Id, usuario.consecutivo, usuario.servicios.hospitalizacion, audit)
                    Next
                End If

                ibi.CosmosId = cosmosDoc.id
                ibi.ObligatedPartyDocument = If(cosmosDoc.JsonRIPS.rips.numDocumentoIdObligado, String.Empty)
                ibi.Status = 2
                ibi.ModificationUser = audit.CodeUser
                ibi.ModificationDate = DateTime.Now
                ibi.MarkAsModified()
                _initialBalanceInvoiceRepository.SaveEntity(ibi)
                _initialBalanceInvoiceRepository.UnitWork.Commit()

                r.Status = RipsUploadStatus.Created
                r.CosmosId = cosmosDoc.id
                r.Message = "Detail rows poblados correctamente."
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                r.Status = RipsUploadStatus.Failed
                r.Message = ex.Message
            End Try
            results.Add(r)
        Next

        Return New ActionResult(Of List(Of RipsUploadResult)) With {.StateResult = True, .ObjectEmbbeded = results}
    End Function

    Private Async Function EnsureXmlFevFileForInitialBalanceAsync(query As RIPSCosmosDbModel) As Task
        If query Is Nothing OrElse query.JsonRIPS Is Nothing OrElse query.JsonRIPS.rips Is Nothing Then Return
        If Not String.IsNullOrWhiteSpace(query.JsonRIPS.xmlFevFile) Then Return
        If Not String.Equals(query.EntityName, OPENING_BALANCE_ENTITY_NAME, StringComparison.OrdinalIgnoreCase) Then Return

        Dim invoiceNumber = query.JsonRIPS.rips.numFactura
        If String.IsNullOrWhiteSpace(invoiceNumber) Then Return

        Dim xmlFevFile = GetInitialBalanceAttachedDocumentBase64ByInvoiceNumber(invoiceNumber)
        If String.IsNullOrWhiteSpace(xmlFevFile) Then Return

        query.JsonRIPS.xmlFevFile = xmlFevFile

        If _rIPSCosmosDbModelRepository IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(query.id) Then
            Await _rIPSCosmosDbModelRepository.UpsertManyAsync(New List(Of RIPSCosmosDbModel) From {query}, Nothing, CancellationToken.None)
        End If
    End Function

    Private Function GetInitialBalanceAttachedDocumentBase64ByInvoiceNumber(invoiceNumber As String) As String
        If String.IsNullOrWhiteSpace(invoiceNumber) Then Return Nothing
        If _initialBalanceInvoiceRepository Is Nothing OrElse _electronicDocumentRepository Is Nothing OrElse _storage Is Nothing Then Return Nothing

        Dim ibi = _initialBalanceInvoiceRepository.GetByInvoiceNumber(invoiceNumber)
        If ibi Is Nothing OrElse ibi.InvoiceId <= 0 Then Return Nothing

        Dim electronicDocument = _electronicDocumentRepository.GetElectronicDocumentByInvoiceId(ibi.InvoiceId, False)
        If electronicDocument Is Nothing OrElse String.IsNullOrWhiteSpace(electronicDocument.FilePath) Then Return Nothing

        Dim fileBytes = ReadAttachedDocumentBytesWithFallback(electronicDocument.FilePath, invoiceNumber)
        If fileBytes Is Nothing Then Return Nothing

        Return Convert.ToBase64String(fileBytes)
    End Function

    Private Sub SaveConsultas(ibiId As Integer, userConsecutive As Integer, items As List(Of ConsultaModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each c In items
            Dim vr As Decimal = If(c.vrServicio, 0)
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 1, c.consecutivo, c.codConsulta, 1, vr, vr, ParseDate(c.fechaInicioAtencion), audit))
        Next
    End Sub

    Private Sub SaveProcedimientos(ibiId As Integer, userConsecutive As Integer, items As List(Of ProcedimientoModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each p In items
            Dim vr As Decimal = If(p.vrServicio, 0)
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 2, p.consecutivo, p.codProcedimiento, 1, vr, vr, ParseDate(p.fechaInicioAtencion), audit))
        Next
    End Sub

    Private Sub SaveUrgencias(ibiId As Integer, userConsecutive As Integer, items As List(Of UrgenciaModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each u In items
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 3, u.consecutivo, Nothing, 1, 0D, 0D, ParseDate(u.fechaInicioAtencion), audit))
        Next
    End Sub

    Private Sub SaveRecienNacidos(ibiId As Integer, userConsecutive As Integer, items As List(Of RecienNacidosModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each rn In items
            If Not rn.consecutivo.HasValue Then Continue For
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 4, rn.consecutivo.Value, Nothing, 1, 0D, 0D, ParseDate(rn.fechaNacimiento), audit))
        Next
    End Sub

    Private Sub SaveMedicamentos(ibiId As Integer, userConsecutive As Integer, items As List(Of MedicamentosModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each m In items
            If Not m.consecutivo.HasValue Then Continue For
            Dim qty As Integer = If(m.cantidadMedicamento, 1)
            Dim uv As Decimal = If(m.vrUnitMedicamento, 0)
            Dim sv As Decimal = If(m.vrServicio, 0)
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 5, m.consecutivo.Value, m.codTecnologiaSalud, qty, uv, sv, ParseDate(m.fechaDispensAdmon), audit))
        Next
    End Sub

    Private Sub SaveOtrosServicios(ibiId As Integer, userConsecutive As Integer, items As List(Of OtrosServiciosModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each o In items
            If Not o.consecutivo.HasValue Then Continue For
            Dim qty As Integer = If(o.cantidadOS, 1)
            Dim uv As Decimal = If(o.vrUnitOS, 0)
            Dim sv As Decimal = If(o.vrServicio, 0)
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 6, o.consecutivo.Value, o.codTecnologiaSalud, qty, uv, sv, ParseDate(o.fechaSuministroTecnologia), audit))
        Next
    End Sub

    Private Sub SaveHospitalizacion(ibiId As Integer, userConsecutive As Integer, items As List(Of HospitalizacionModel), audit As AuditMessage)
        If items Is Nothing Then Return
        For Each h In items
            If Not h.consecutivo.HasValue Then Continue For
            _initialBalanceInvoiceRepository.SaveDetail(BuildDetailRow(ibiId, userConsecutive, 7, h.consecutivo.Value, Nothing, 1, 0D, 0D, ParseDate(h.fechaInicioAtencion), audit))
        Next
    End Sub

    Private Function BuildDetailRow(ibiId As Integer, userConsecutive As Integer, serviceType As Byte, consecutive As Integer, serviceCode As String,
                                     quantity As Integer, unitValue As Decimal, serviceValue As Decimal,
                                     attentionStart As Date?, audit As AuditMessage) As InitialBalanceInvoiceDetail
        Return New InitialBalanceInvoiceDetail With {
            .InitialBalanceInvoiceId = ibiId,
            .UserConsecutive = userConsecutive,
            .ServiceType = serviceType,
            .Consecutive = consecutive,
            .ServiceCode = serviceCode,
            .Quantity = quantity,
            .UnitValue = unitValue,
            .ServiceValue = serviceValue,
            .Balance = serviceValue,
            .AttentionStartDate = attentionStart,
            .CreationUser = audit.CodeUser,
            .CreationDate = DateTime.Now
        }
    End Function

    Private Function ParseDate(value As String) As Date?
        Dim parsed As Date
        If String.IsNullOrWhiteSpace(value) Then Return Nothing
        If Date.TryParse(value, parsed) Then Return parsed
        Return Nothing
    End Function

#End Region

End Class
