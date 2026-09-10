'***********************************************************************
' Assembly         : Application.Receivable
' Description      : Servicio para carga masiva de XMLs de factura DIAN
'                    asociados a facturas de saldo inicial. Almacena los
'                    XMLs en blob storage usando el FilePath canónico que
'                    creará el shadow ElectronicDocument durante el confirm,
'                    con FileName = "{numFactura}.xml" (no canónico DIAN).
'                    La nota tipo 6 los recupera vía discriminador IBI en
'                    GetHealthSegmentFromInvoiceXml.
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Threading
Imports System.Xml.Linq
Imports Domain.Base.Entities
Imports Domain.Billing.POCO.E_InvoiceXml
Imports Domain.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Root

Public Class InvoiceXmlBulkAdminService
    Implements IInvoiceXmlBulkAdminService

#Region "Fields"

    Private _portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository
    Private _stagingRepository As IPortfolioInitialBalanceAccountReceivableRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private ReadOnly _factoryStorage As IFactoryStorage
    Private ReadOnly _storage As IStorage

#End Region

#Region "Constants"

    Private Const CBC_NS As String = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    Private Const CAC_NS As String = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"

#End Region

#Region "Builders"

    Public Sub New(portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository,
                   stagingRepository As IPortfolioInitialBalanceAccountReceivableRepository,
                   settingsAccountRepository As ISettingsAccountRepository,
                   thirdPartyRepository As IThirdPartyRepository,
                   factoryStorage As IFactoryStorage)

        If portfolioInitialBalanceRepository Is Nothing Then Throw New ArgumentNullException(NameOf(portfolioInitialBalanceRepository))
        If stagingRepository Is Nothing Then Throw New ArgumentNullException(NameOf(stagingRepository))
        If settingsAccountRepository Is Nothing Then Throw New ArgumentNullException(NameOf(settingsAccountRepository))
        If thirdPartyRepository Is Nothing Then Throw New ArgumentNullException(NameOf(thirdPartyRepository))
        If factoryStorage Is Nothing Then Throw New ArgumentNullException(NameOf(factoryStorage))

        Me._portfolioInitialBalanceRepository = portfolioInitialBalanceRepository
        Me._stagingRepository = stagingRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._thirdPartyRepository = thirdPartyRepository
        Me._factoryStorage = factoryStorage
        Me._storage = factoryStorage.CreateStorageControl()
    End Sub

#End Region

#Region "Public Methods"

    Public Async Function UploadInvoiceXmlSmallAsync(items As List(Of InvoiceXmlUploadRequest),
                                                     audit As AuditMessage) As Task(Of ActionResult(Of InvoiceXmlBulkResponse)) _
            Implements IInvoiceXmlBulkAdminService.UploadInvoiceXmlSmallAsync
        Return Await UploadInvoiceXmlBulkAsync(Guid.NewGuid().ToString(), items, audit, CancellationToken.None)
    End Function

    Public Async Function UploadInvoiceXmlBulkAsync(batchId As String,
                                                     items As List(Of InvoiceXmlUploadRequest),
                                                     audit As AuditMessage,
                                                     Optional cancellationToken As CancellationToken = Nothing) As Task(Of ActionResult(Of InvoiceXmlBulkResponse)) _
            Implements IInvoiceXmlBulkAdminService.UploadInvoiceXmlBulkAsync

        Dim response As New InvoiceXmlBulkResponse With {
            .BatchId = If(String.IsNullOrEmpty(batchId), Guid.NewGuid().ToString(), batchId),
            .TotalReceived = If(items?.Count, 0)
        }

        If items Is Nothing OrElse items.Count = 0 Then
            Return New ActionResult(Of InvoiceXmlBulkResponse) With {
                .StateResult = True,
                .ObjectEmbbeded = response,
                .Message = "No hay items para procesar"
            }
        End If

        ' Caches por (InitialBalanceId, OperatingUnitId) — múltiples items suelen compartirlos
        Dim headerById As New Dictionary(Of Integer, PortfolioInitialBalance)()
        Dim supplierByOu As New Dictionary(Of Integer, ThirdParty)()

        For Each it In items
            cancellationToken.ThrowIfCancellationRequested()

            Dim result As InvoiceXmlUploadResult = ProcessSingleUpload(it, headerById, supplierByOu)
            response.Results.Add(result)
        Next

        response.Succeeded = response.Results.Where(Function(r) r.Status = InvoiceXmlUploadStatus.Created).Count
        response.Skipped = response.Results.Where(Function(r) r.Status = InvoiceXmlUploadStatus.Skipped).Count
        response.Failed = response.Results.Where(Function(r) r.Status = InvoiceXmlUploadStatus.Failed).Count

        Return New ActionResult(Of InvoiceXmlBulkResponse) With {
            .StateResult = True,
            .ObjectEmbbeded = response,
            .Message = String.Format("Procesados: {0} | Cargados: {1} | Omitidos: {2} | Errores: {3}",
                                     response.TotalReceived, response.Succeeded, response.Skipped, response.Failed)
        }
    End Function

    Public Async Function CheckInvoiceXmlExistAsync(initialBalanceId As Integer,
                                                     invoiceNumbers As List(Of String),
                                                     audit As AuditMessage) As Task(Of ActionResult(Of InvoiceXmlCheckExistResponse)) _
            Implements IInvoiceXmlBulkAdminService.CheckInvoiceXmlExistAsync

        Dim response As New InvoiceXmlCheckExistResponse()

        If invoiceNumbers Is Nothing OrElse invoiceNumbers.Count = 0 OrElse initialBalanceId <= 0 Then
            Return New ActionResult(Of InvoiceXmlCheckExistResponse) With {
                .StateResult = True,
                .ObjectEmbbeded = response
            }
        End If

        Dim header = SafeGetHeader(initialBalanceId)
        If header Is Nothing Then
            ' Sin header no podemos derivar FilePath — marcamos todo como Missing
            For Each numF In invoiceNumbers.Distinct()
                response.Missing.Add(numF)
            Next
            Return New ActionResult(Of InvoiceXmlCheckExistResponse) With {
                .StateResult = True,
                .ObjectEmbbeded = response
            }
        End If

        For Each numFactura In invoiceNumbers.Distinct()
            Dim filePath As String = BuildShadowEdFilePath(header, numFactura)
            Dim fileName As String = BuildInitialBalanceXmlFileName(numFactura)
            Dim notExists As Boolean = _storage.ValidateIfNotExists(filePath, fileName)
            If notExists OrElse Not IsAttachedDocumentXml(filePath, fileName) Then
                response.Missing.Add(numFactura)
            Else
                response.Existing.Add(numFactura)
            End If
        Next

        Return New ActionResult(Of InvoiceXmlCheckExistResponse) With {
            .StateResult = True,
            .ObjectEmbbeded = response
        }
    End Function

#End Region

#Region "Private Methods"

    Private Function ProcessSingleUpload(item As InvoiceXmlUploadRequest,
                                          headerById As Dictionary(Of Integer, PortfolioInitialBalance),
                                          supplierByOu As Dictionary(Of Integer, ThirdParty)) As InvoiceXmlUploadResult
        Dim result As New InvoiceXmlUploadResult With {.NumFactura = item?.NumFactura}

        If item Is Nothing OrElse String.IsNullOrWhiteSpace(item.NumFactura) OrElse String.IsNullOrWhiteSpace(item.XmlContent) Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Item inválido (numFactura o contenido vacío)"
            Return result
        End If

        If item.InitialBalanceId <= 0 Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "InitialBalanceId no especificado en la solicitud."
            Return result
        End If

        ' 1. Parsear XML + extraer Invoice + CUFE
        Dim parsed As ParsedInvoiceXml
        Try
            parsed = ExtractInvoiceXml(item.XmlContent)
        Catch ex As Exception
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Formato XML inválido: " & ex.Message
            Return result
        End Try

        If parsed Is Nothing OrElse
           String.IsNullOrWhiteSpace(parsed.InvoiceXml) OrElse
           String.IsNullOrWhiteSpace(parsed.Cufe) OrElse
           String.IsNullOrWhiteSpace(parsed.NumFactura) Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Solo se acepta AttachedDocument DIAN con Invoice embebido y CUFE válido."
            Return result
        End If

        ' 2. NumFactura del XML debe coincidir con item.NumFactura
        If Not String.Equals(parsed.NumFactura, item.NumFactura, StringComparison.OrdinalIgnoreCase) Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = String.Format("Número de factura del XML ({0}) no coincide con archivo ({1})", parsed.NumFactura, item.NumFactura)
            Return result
        End If

        ' 3. Lookup staging row (factura saldo inicial guardada pre-confirm)
        Dim staging As PortfolioInitialBalanceAccountReceivable = Nothing
        Try
            staging = _stagingRepository.GetByInvoiceNumberAndInitialBalanceId(item.InitialBalanceId, item.NumFactura)
        Catch ex As Exception
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Error consultando staging del saldo inicial: " & ex.Message
            Return result
        End Try

        If staging Is Nothing Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Factura no encontrada en el saldo inicial. Guarde el saldo inicial antes de subir el XML."
            Return result
        End If

        If String.IsNullOrWhiteSpace(staging.CUFE) Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "La factura saldo inicial no tiene CUFE registrado."
            Return result
        End If

        ' 4. Validar CUFE coincide
        If Not String.Equals(staging.CUFE.Trim(), parsed.Cufe.Trim(), StringComparison.OrdinalIgnoreCase) Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "El CUFE del XML no coincide con el CUFE registrado para la factura."
            Return result
        End If

        ' 5. Lookup parent header (OperatingUnitId + CreationDate → derivan FilePath canónico)
        Dim header = ResolveHeader(item.InitialBalanceId, headerById)
        If header Is Nothing Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "No se encontró el saldo inicial padre (InitialBalanceId)."
            Return result
        End If

        ' 6. Validar supplier DIAN configurado antes de almacenar el XML.
        Dim supplier = ResolveSupplier(header.OperatingUnitId, supplierByOu)
        If supplier Is Nothing Then
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "No se encontró tercero proveedor DIAN para la unidad operativa."
            Return result
        End If

        ' 7. Construir el FilePath canónico del documento electrónico.
        Dim filePath As String = BuildShadowEdFilePath(header, staging.InvoiceNumber)
        Dim fileName As String = BuildInitialBalanceXmlFileName(staging.InvoiceNumber)

        result.FilePath = filePath
        result.FileName = fileName

        ' 8. Idempotencia
        Dim notExists As Boolean = _storage.ValidateIfNotExists(filePath, fileName)
        If Not notExists Then
            result.Status = InvoiceXmlUploadStatus.Skipped
            result.Message = "El XML ya fue cargado previamente."
            Return result
        End If

        ' 9. Persistir
        Try
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(parsed.InvoiceXml)
            _storage.WriteFile(filePath, fileName, bytes)
            result.Status = InvoiceXmlUploadStatus.Created
            result.Message = "XML cargado correctamente."
        Catch ex As Exception
            result.Status = InvoiceXmlUploadStatus.Failed
            result.Message = "Error escribiendo el archivo: " & ex.Message
        End Try

        Return result
    End Function

    Private Function ResolveHeader(initialBalanceId As Integer, cache As Dictionary(Of Integer, PortfolioInitialBalance)) As PortfolioInitialBalance
        Dim header As PortfolioInitialBalance = Nothing
        If cache.TryGetValue(initialBalanceId, header) Then
            Return header
        End If

        header = SafeGetHeader(initialBalanceId)
        cache(initialBalanceId) = header
        Return header
    End Function

    Private Function SafeGetHeader(initialBalanceId As Integer) As PortfolioInitialBalance
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceById(initialBalanceId)
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ResolveSupplier(operatingUnitId As Integer, cache As Dictionary(Of Integer, ThirdParty)) As ThirdParty
        Dim supplier As ThirdParty = Nothing
        If cache.TryGetValue(operatingUnitId, supplier) Then
            Return supplier
        End If

        Try
            Dim settings = _settingsAccountRepository.GetSettingAccountSimple(operatingUnitId)
            If settings IsNot Nothing AndAlso settings.IdDian > 0 Then
                supplier = _thirdPartyRepository.GetThirdPartyById(settings.IdDian, False)
            End If
        Catch
            supplier = Nothing
        End Try

        cache(operatingUnitId) = supplier
        Return supplier
    End Function

    ''' <summary>
    ''' Construye el FilePath canónico para almacenar el XML del documento electrónico.
    ''' Es determinístico: deriva de PortfolioInitialBalance.CreationDate (no DateTime.Now).
    ''' </summary>
    Private Function BuildShadowEdFilePath(header As PortfolioInitialBalance, invoiceNumber As String) As String
        Dim container As String = ServerSessionValues.Current.CurrentContainer
        Return String.Format("E:\ProgramData\Indigo Technologies\ElectronicDocuments\{0}\001\{1}\{2}\Saldos Iniciales\{3}",
                              container, header.CreationDate.Year, header.CreationDate.Month, invoiceNumber)
    End Function

    ''' <summary>
    ''' Naming no canónico DIAN (XML padre saldo inicial = un único archivo por carpeta de factura,
    ''' nunca se reemite, solo se recupera). GetHealthSegmentFromInvoiceXml ramifica por IBI lookup
    ''' y usa este mismo naming para leer el blob al emitir notas tipo 6.
    ''' </summary>
    Private Function BuildInitialBalanceXmlFileName(invoiceNumber As String) As String
        Return invoiceNumber & ".xml"
    End Function

    Private Function IsAttachedDocumentXml(filePath As String, fileName As String) As Boolean
        Try
            Dim fileBytes = _storage.ReadFile(filePath, fileName)
            If fileBytes Is Nothing OrElse fileBytes.Length = 0 Then Return False

            Dim xmlString = Encoding.UTF8.GetString(fileBytes)
            Dim doc = XDocument.Parse(xmlString)
            Return doc.Root IsNot Nothing AndAlso String.Equals(doc.Root.Name.LocalName, "AttachedDocument", StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Extrae el CUFE, numero de factura e Invoice XML del contenido recibido.
    ''' Soporta AttachedDocument DIAN (envelope post-validación): el Invoice viene como CDATA
    ''' en cac:Attachment/cac:ExternalReference/cbc:Description. El CUFE se obtiene del
    ''' cbc:UUID del Invoice embebido; el cbc:ID raíz identifica el contenedor, no el CUFE.
    ''' </summary>
    Private Function ExtractInvoiceXml(xmlContent As String) As ParsedInvoiceXml
        Dim doc As XDocument = XDocument.Parse(xmlContent)
        Dim root As XElement = doc.Root
        If root Is Nothing Then Return Nothing

        Dim cbc As XNamespace = CBC_NS
        Dim cac As XNamespace = CAC_NS

        ' AttachedDocument envelope
        If String.Equals(root.Name.LocalName, "AttachedDocument", StringComparison.OrdinalIgnoreCase) Then
            Dim parentDocId = root.Element(cbc + "ParentDocumentID")?.Value
            Dim attachedDocumentId = root.Element(cbc + "ID")?.Value
            Dim description = root.Element(cac + "Attachment")?.Element(cac + "ExternalReference")?.Element(cbc + "Description")?.Value

            If String.IsNullOrWhiteSpace(attachedDocumentId) OrElse
               String.IsNullOrWhiteSpace(parentDocId) OrElse
               String.IsNullOrWhiteSpace(description) Then
                Return Nothing
            End If

            Dim innerDoc As XDocument = XDocument.Parse(description)
            Dim innerRoot = innerDoc.Root
            If innerRoot Is Nothing OrElse Not String.Equals(innerRoot.Name.LocalName, "Invoice", StringComparison.OrdinalIgnoreCase) Then
                Return Nothing
            End If

            Dim innerCufe = innerRoot.Element(cbc + "UUID")?.Value
            Dim innerNum = innerRoot.Element(cbc + "ID")?.Value

            ' El ID del contenedor es independiente del CUFE. La relación semántica
            ' con la factura se expresa mediante ParentDocumentID.
            If String.IsNullOrWhiteSpace(innerNum) OrElse String.IsNullOrWhiteSpace(innerCufe) Then
                Return Nothing
            End If

            If Not String.Equals(parentDocId.Trim(), innerNum.Trim(), StringComparison.OrdinalIgnoreCase) Then
                Return Nothing
            End If

            Return New ParsedInvoiceXml With {
                .NumFactura = innerNum,
                .Cufe = innerCufe,
                .InvoiceXml = xmlContent
            }
        End If

        Return Nothing
    End Function

#End Region

#Region "Helper Types"

    Private Class ParsedInvoiceXml
        Public Property NumFactura As String
        Public Property Cufe As String
        Public Property InvoiceXml As String
    End Class

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _portfolioInitialBalanceRepository = Nothing
                _stagingRepository = Nothing
                _settingsAccountRepository = Nothing
                _thirdPartyRepository = Nothing
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
