Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Domain.Base.Entities
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Root
Imports Infrastructure.CrossCutting.Signature
Imports Utils = Infrastructure.CrossCutting.Base.Utils

Namespace DIAN.UBL2_1

    Public Class UBL2_1

#Region "Properties"

        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _documentType As TypeElectronicDocument
        ReadOnly _electronicSignature As ElectronicSignature
        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _supplierThirdParty As ThirdParty

        ''' <summary>
        ''' Objetos necesarios para armar la información del archivo de Facturación Electrónica
        ''' </summary>
        Public BillingAuthorization As BillingAuthorization

        ''' <summary>
        ''' variable que almacena la nota
        ''' </summary>
        Public BillingNote As BillingNote

        ''' <summary>
        ''' variable que almacena el documento electronico
        ''' </summary>
        Public ElectronicDocument As ElectronicDocument

        ''' <summary>
        ''' variable que almacena la factura
        ''' </summary>
        Public Invoice As Invoice

        ''' <summary>
        ''' variable que almacena los metodos de pago
        ''' </summary>
        Public PaymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result)

        ''' <summary>
        ''' variable que almacena los metodos de pago para documento soporte
        ''' </summary>
        Public PaymentMethodsSupportDocument As List(Of SP_GetPaymentMethodByDocumentSupportId_Result)

        ''' <summary>
        ''' Objetos necesarios para armar la información del archivo de Nomina Electrónica
        ''' </summary>
        Public ElectronicPayroll As Domain.Payroll.Entities.ElectronicPayroll

        ''' <summary>
        ''' variable que almacena el documento de soporte electronico
        ''' </summary>
        Public ElectronicSupportDocument As ElectronicSupportDocument

        ''' <summary>
        ''' variable que almacena la nota del soporte electronico
        ''' </summary>
        Public SupportDocumentAdjustmentNote As ElectronicSupportDocumentAdjustmentNote

        ''' <summary>
        ''' Segmento de sector salud extraído del XML de la factura asociada (para notas crédito/débito)
        ''' </summary>
        Public HealthSegmentFromInvoice As CustomTagGeneralType

        Public InvoicePeriodFromInvoice As List(Of PeriodType)

        ''' <summary>
        ''' Fecha final del período de la factura de capitación anterior.
        ''' Se informa únicamente para facturas de capitación período.
        ''' </summary>
        Public PreviousCapitationPeriodEndDate As Nullable(Of Date)

        ''' <summary>
        ''' interfaz para usar el blobStorage
        ''' </summary>
        Private ReadOnly _storateService As IStorage

        ''' <summary>
        ''' interfaz servicio de almacenamiento
        ''' </summary>
        Private ReadOnly _storageType As EStorageType

#End Region

#Region "Buldier"

        Public Sub New(ByVal customerThirdParty As ThirdParty,
                            documentType As TypeElectronicDocument,
                       ByVal settingsAccount As GeneralLedgerSettings,
                       ByVal supplierThirdParty As ThirdParty,
                       Optional storateService As IStorage = Nothing)
            Me._customerThirdParty = customerThirdParty
            Me._documentType = documentType
            Me._settingsAccount = settingsAccount
            Me._supplierThirdParty = supplierThirdParty
            Me._storateService = storateService
            Me._storageType = If(Me._storateService?.StorageType, EStorageType.LocalStore)
            Me._electronicSignature = New ElectronicSignature(settingsAccount.DianVersion, SignatoryRole.ElectronicBiller, Me._settingsAccount.DigitalCertificate, Me._settingsAccount.DigitalCertificateKey, Nothing, UblBaseDocumentType.Namespaces)
        End Sub

#End Region

#Region "Methods"
        Public Function GenerateXMLLocal() As ActionResult(Of String)
            Dim file = String.Empty
            Dim filePath = Me.GetFilePath()
            Dim fileName = Me.GetFileName()
            file = System.IO.Path.Combine(filePath, fileName)
            Try
                Me.DeletePrevious(filePath, fileName)
                If Utils.ValidateFileExists(filePath, fileName) Then
                    Dim document As New Object
                    document = Me.GenerateDocument()
                    Dim setting As New XmlWriterSettings With {.Indent = True, .OmitXmlDeclaration = True}
                    Using writer = XmlWriter.Create(file, setting)
                        Dim xs As XmlSerializer = Me.GenerateXmlSerializer(document)
                        writer.WriteStartDocument(False)
                        xs.Serialize(writer, document)
                    End Using
                    Dim archivoXml = New IO.FileInfo(file)
                    Dim bytesArchivoFirmado = Me._electronicSignature.SignatureFile(archivoXml, Me._documentType, DateTime.Now.AddSeconds(-10))
                    System.IO.File.WriteAllBytes(file, bytesArchivoFirmado)
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = fileName}
            Catch ex As Exception
                If Not String.IsNullOrEmpty(file) Then
                    System.IO.File.Delete(file)
                End If
                Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Function

        ''' <summary>
        ''' funcion que genera el XML en el blobStorage
        ''' </summary>
        ''' <returns></returns>
        Public Function GenerateXMLBlobStorage() As ActionResult(Of String)
            Dim file = String.Empty
            Dim filePath = Me.GetFilePath()
            Dim fileName = Me.GetFileName()
            file = System.IO.Path.Combine(filePath, fileName)
            Try

                Me._storateService.DeleteFile(filePath, fileName)
                If Me._storateService.ValidateIfNotExists(filePath, fileName) Then
                    Dim document As New Object
                    document = Me.GenerateDocument()
                    Dim xs As XmlSerializer = Nothing
                    xs = Me.GenerateXmlSerializer(document)
                    Dim archivoXml As Object = Nothing
                    Dim memoryStream As New MemoryStream()
                    xs.Serialize(memoryStream, document)
                    memoryStream.Position = 0
                    archivoXml = memoryStream
                    Dim bytesArchivoFirmado = Me._electronicSignature.SignatureFile(archivoXml, Me._documentType, DateTime.Now.AddSeconds(-10))
                    _storateService.WriteFile(filePath, file, bytesArchivoFirmado)
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = fileName}
            Catch ex As Exception
                If Not String.IsNullOrEmpty(file) Then
                    _storateService.DeleteFile(filePath, fileName)
                End If
                Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Function

        ''' <summary>
        ''' function que determina como se generará el XML 
        ''' </summary>
        ''' <returns></returns>
        Public Function GenerateXML() As ActionResult(Of String)

            If Me._storageType = EStorageType.BlobStorage Then
                Return Me.GenerateXMLBlobStorage()
            End If
            Return Me.GenerateXMLLocal()
        End Function

        ''' <summary>
        ''' funcion que elimina el archivo previamente creado
        ''' </summary>
        ''' <param name="filePath"></param>
        ''' <param name="fileName"></param>
        Private Sub DeletePrevious(filePath As String, fileName As String)
            Try
                Dim file = System.IO.Path.Combine(filePath, fileName)

                If String.IsNullOrEmpty(file) Then
                    Exit Sub
                End If

                If Not Utils.ValidateFileExists(filePath, fileName) Then
                    System.IO.File.Delete(file)
                End If

            Catch ex As Exception
                Console.WriteLine(Utils.GetInnerExceptionMessageToString(ex))
            End Try
        End Sub

        ''' <summary>
        ''' funcion que obtiene el path
        ''' </summary>
        ''' <returns></returns>
        Private Function GetFilePath() As String
            If Me._documentType = TypeElectronicDocument.NominaIndividual OrElse Me._documentType = TypeElectronicDocument.NominaIndividualDeAjuste Then
                Return Me.ElectronicPayroll.FilePath
            ElseIf Me._documentType = TypeElectronicDocument.DocumentoSoporte Then
                Return Me.ElectronicSupportDocument.FilePath
            ElseIf Me._documentType = TypeElectronicDocument.NotaDeAjuste Then
                Return Me.SupportDocumentAdjustmentNote.FilePath
            Else
                Return Me.ElectronicDocument.FilePath
            End If
        End Function

        ''' <summary>
        ''' funcion retorna el nombre del archivo xml
        ''' </summary>
        ''' <returns></returns>
        Private Function GetFileName() As String
            If Me._documentType = TypeElectronicDocument.NominaIndividual OrElse Me._documentType = TypeElectronicDocument.NominaIndividualDeAjuste Then
                Return Me.ElectronicPayroll.GetFileName(Me._documentType)
            ElseIf Me._documentType = TypeElectronicDocument.DocumentoSoporte Then
                Return Me.ElectronicSupportDocument.GetFileName(Me._customerThirdParty, Me._documentType)
            ElseIf Me._documentType = TypeElectronicDocument.NotaDeAjuste Then
                Return Me.SupportDocumentAdjustmentNote.GetFileName(Me._customerThirdParty, Me._documentType)
            Else
                Return Me.ElectronicDocument.GetFileName(Me._supplierThirdParty, Me._documentType)
            End If
        End Function

        ''' <summary>
        ''' genera el xml serializer
        ''' </summary>
        ''' <param name="document"></param>
        ''' <returns></returns>
        Private Function GenerateXmlSerializer(document As Object) As XmlSerializer
            Dim xs As XmlSerializer = Nothing
            If Me._documentType = TypeElectronicDocument.Invoice Then
                xs = New XmlSerializer(GetType(InvoiceType))
                Me._electronicSignature._ublExtensions = If(CType(document, InvoiceType).UBLExtensions Is Nothing, 0, CType(document, InvoiceType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.DebitNote Then
                xs = New XmlSerializer(GetType(DebitNoteType))
                Me._electronicSignature._ublExtensions = If(CType(document, DebitNoteType).UBLExtensions Is Nothing, 0, CType(document, DebitNoteType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.CreditNote Then
                xs = New XmlSerializer(GetType(CreditNoteType))
                Me._electronicSignature._ublExtensions = If(CType(document, CreditNoteType).UBLExtensions Is Nothing, 0, CType(document, CreditNoteType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.NominaIndividual Then
                xs = New XmlSerializer(GetType(NominaIndividualType))
                Me._electronicSignature._ublExtensions = If(CType(document, NominaIndividualType).UBLExtensions Is Nothing, 0, CType(document, NominaIndividualType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.NominaIndividualDeAjuste Then
                xs = New XmlSerializer(GetType(NominaIndividualDeAjusteType))
                Me._electronicSignature._ublExtensions = If(CType(document, NominaIndividualDeAjusteType).UBLExtensions Is Nothing, 0, CType(document, NominaIndividualDeAjusteType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.DocumentoSoporte Then
                xs = New XmlSerializer(GetType(InvoiceType))
                Me._electronicSignature._ublExtensions = If(CType(document, InvoiceType).UBLExtensions Is Nothing, 0, CType(document, InvoiceType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.NotaDeAjuste Then
                xs = New XmlSerializer(GetType(CreditNoteType))
                Me._electronicSignature._ublExtensions = If(CType(document, CreditNoteType).UBLExtensions Is Nothing, 0, CType(document, CreditNoteType).UBLExtensions.Count)
            ElseIf Me._documentType = TypeElectronicDocument.AttachedDocument Then
                xs = New XmlSerializer(GetType(AttachedDocumentType))
                Me._electronicSignature._ublExtensions = If(CType(document, AttachedDocumentType).UBLExtensions Is Nothing, 0, CType(document, AttachedDocumentType).UBLExtensions.Count)
            End If
            Return xs
        End Function

        ''' <summary>
        ''' genera el documento a firmar
        ''' </summary>
        ''' <returns></returns>
        Private Function GenerateDocument() As Object
            Dim document As New Object

            If Me._documentType = TypeElectronicDocument.Invoice Then
                document = (New v1_6.Invoice(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.Invoice, Me.BillingAuthorization, Me.PaymentMethods, Me.PreviousCapitationPeriodEndDate)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.DebitNote Then
                document = (New v1_6.DebitNote(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.BillingNote, Me.HealthSegmentFromInvoice, Me.PaymentMethods, Me.InvoicePeriodFromInvoice)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.CreditNote Then
                document = (New v1_6.CreditNote(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.BillingNote, Me.HealthSegmentFromInvoice, Me.PaymentMethods, Me.InvoicePeriodFromInvoice)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.NominaIndividual Then
                document = (New v1_0.NominaIndividual(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.ElectronicPayroll)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.NominaIndividualDeAjuste Then
                document = (New v1_0.NominaIndividualDeAjuste(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.ElectronicPayroll)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.DocumentoSoporte Then
                document = (New v1_6.SupportDocument(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.ElectronicSupportDocument, Me.BillingAuthorization, Me.PaymentMethodsSupportDocument)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.NotaDeAjuste Then
                document = (New v1_6.SupportDocumentAdjustmentNote(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, SupportDocumentAdjustmentNote, Me.BillingAuthorization)).Populate()
            ElseIf Me._documentType = TypeElectronicDocument.AttachedDocument Then
                document = (New v1_6.AttachedDocument(Me._settingsAccount, Me._supplierThirdParty, Me._customerThirdParty, Me.ElectronicDocument, Me._storateService)).Populate()
            End If

            Return document
        End Function

#End Region

    End Class

End Namespace
