Imports System.Text
Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root
Imports System.IO
Imports System.IO.Compression

Namespace DIAN.UBL2_1.v1_6

    Public Class AttachedDocument

#Region "Variables"

        ReadOnly _ublVersionId As New UBLVersionIDType With {.Value = "UBL 2.1"}
        ReadOnly _customizationID As New CustomizationIDType With {.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.AttachedDocument)}
        ReadOnly _profileId As New ProfileIDType With {.Value = "Factura Electrónica de Venta"}
        ReadOnly _dianInformation = New With {.DocumentType = "31", .Nit = "800197268", .DigitVerification = "4"}

#End Region

#Region "Properties"

        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _billingAuthorization As BillingAuthorization
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _electronicDocument As ElectronicDocument
        ''' <summary>
        ''' servicio de almacenamiento
        ''' </summary>
        Private ReadOnly _storage As Infrastructure.CrossCutting.AzureBlobStorage.IStorage

#End Region

#Region "Buldier"

        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings,
                       ByVal supplierThirdParty As ThirdParty,
                       ByVal customerThirdParty As ThirdParty,
                       ByVal electronicDocument As ElectronicDocument,
                       ByVal storage As Infrastructure.CrossCutting.AzureBlobStorage.IStorage)
            Me._settingsAccount = settingsAccount
            Me._supplierThirdParty = supplierThirdParty
            Me._customerThirdParty = customerThirdParty
            Me._electronicDocument = electronicDocument
            Me._storage = storage
            AmountType.TlsDefaultCurrencyID = CurrencyCode.COP
        End Sub

#End Region

#Region "Methods"

        Public Function Populate() As AttachedDocumentType
            Dim currentDate = DateTime.Now
            Dim documentType As New AttachedDocumentType
            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.UBLVersionID = Me._ublVersionId
            documentType.CustomizationID = Me._customizationID
            documentType.ProfileID = Me._profileId
            documentType.ProfileExecutionID = New ProfileExecutionIDType With {.Value = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests))}
            documentType.ID = New IDType With {.Value = Me._electronicDocument.Consecutive}
            documentType.IssueDate = New IssueDateType With {.Value = CType(currentDate, DateTime).ToString("yyyy-MM-dd")}
            documentType.IssueTime = New IssueTimeType With {.Value = CType(currentDate, DateTime).ToString("HH:mm:ss-05:00")}
            documentType.DocumentType = New DocumentTypeType With {.Value = Utils.GetXmlEnumToString(Of DocumentType)(Domain.Base.Entities.Enums.ElectronicDocuments.v1_6.DocumentType.AttachedDocument)}
            documentType.ParentDocumentID = New ParentDocumentIDType With {.Value = Me._electronicDocument.GetDocumentNumber()}
            documentType.SenderParty = GetPartyTypeInformation(Me._supplierThirdParty)
            documentType.ReceiverParty = GetPartyTypeInformation(Me._customerThirdParty)
            documentType.Attachment = GetAttachmentInformation()
            documentType.ParentDocumentLineReference = GetDocumentReferenceInformation()
            Return documentType
        End Function

#End Region

#Region "Private Methods"

        Private Function GenerateUBLExtensions() As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

        Private Function GetPartyTypeInformation(ByVal thirdParty As Domain.Entities.ThirdParty) As PartyType
            Dim party As New PartyType

            party.PartyTaxScheme = New List(Of PartyTaxSchemeType) From
            {
                New PartyTaxSchemeType() With
                {
                    .RegistrationName = New RegistrationNameType With {.Value = thirdParty.Name},
                    .CompanyID = New CompanyIDType With
                    {
                        .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
                        .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
                        .schemeID = thirdParty.DigitVerification,
                        .schemeName = thirdParty.Person.getAcquirerType(),
                        .Value = thirdParty.Nit
                    },
                    .TaxLevelCode = New TaxLevelCodeType With {.Value = thirdParty.getTaxLevelCode(), .listName = thirdParty.getTaxLevelName()},
                    .TaxScheme = New TaxSchemeType With
                    {
                        .ID = New IDType With {.Value = thirdParty.getTaxSchemeID()},
                        .Name = New NameType1 With {.Value = thirdParty.getTaxSchemeName()}
                    }
                }
            }

            Return party
        End Function

        Private Function GetAttachmentInformation() As AttachmentType
            Dim filePath = Me._electronicDocument.FilePath
            Dim fileName = Me._electronicDocument.GetFileName(Me._supplierThirdParty, Me._electronicDocument.getDocumentType())
            Dim xmlfilestring As String = String.Empty

            ' Verificar si existe el archivo XML directamente
            Dim xmlExists As Boolean = Not Me.ValidateIfNotExists(filePath, fileName)

            If Not xmlExists Then
                ' Si no existe el XML, intentar buscar el ZIP que contiene el archivo validado por la DIAN
                Dim zipName = fileName
                If zipName.StartsWith("fv", StringComparison.OrdinalIgnoreCase) Then
                    zipName = "z" & zipName.Substring(2)
                End If
                zipName = Path.ChangeExtension(zipName, ".zip")
                Dim zipExists As Boolean = Not Me.ValidateIfNotExists(filePath, zipName)

                If zipExists Then
                    Dim zipBytes As Byte() = Nothing
                    If _storage IsNot Nothing Then
                        zipBytes = _storage.ReadFile(filePath, zipName)
                    End If

                    ' Descomprimir el ZIP desde memoria
                    Using zipStream As New MemoryStream(zipBytes)
                        Using zip As ZipArchive = New ZipArchive(zipStream, ZipArchiveMode.Read)
                            Dim xmlEntry As ZipArchiveEntry = Nothing

                            ' Buscar el archivo XML dentro del ZIP
                            For Each entry As ZipArchiveEntry In zip.Entries
                                If entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) Then
                                    xmlEntry = entry
                                    Exit For
                                End If
                            Next

                            If xmlEntry Is Nothing Then
                                Throw New Exception("El ZIP no contiene un archivo XML válido.")
                            End If

                            ' Extraer el contenido del XML directamente a string
                            Using entryStream As Stream = xmlEntry.Open()
                                Using reader As New StreamReader(entryStream, Encoding.UTF8)
                                    xmlfilestring = reader.ReadToEnd()
                                End Using
                            End Using
                        End Using
                    End Using

                    ' Guardar el XML extraído
                    If _storage IsNot Nothing AndAlso _storage.StorageType = Infrastructure.CrossCutting.Base.EStorageType.BlobStorage Then
                        Dim xmlBytes = Encoding.UTF8.GetBytes(xmlfilestring)
                        _storage.WriteFile(filePath, fileName, xmlBytes)
                    End If
                Else
                    ' Ni XML ni ZIP encontrados
                    Throw New Exception("No se encontró el XML ni el ZIP del documento electrónico en la ruta: " & Path.Combine(filePath, fileName))
                End If
            Else
                ' Si el XML existe, leerlo directamente
                xmlfilestring = Me.ReadAllText(filePath, fileName)
            End If

            ' Crear el objeto AttachmentType
            Dim attachment As New AttachmentType With {
                .ExternalReference = New ExternalReferenceType With {
                    .MimeCode = New MimeCodeType With {.Value = "text/xml"},
                    .EncodingCode = New EncodingCodeType With {.Value = "UTF-8"},
                    .Description = New List(Of DescriptionType) From {New DescriptionType(xmlfilestring)}
                }
            }

            Return attachment
        End Function

        Private Function GetIdReference(DocumentResponse As List(Of DocumentResponseType)) As IDType
            Dim reference As New DocumentReferenceType
            Dim Id As New IDType
            For Each ds In DocumentResponse
                reference = ds.DocumentReference.FirstOrDefault
                Id = reference.ID
            Next
            Return Id
        End Function

        Private Function GetUUIDReference(DocumentResponse As List(Of DocumentResponseType)) As UUIDType
            Dim reference As New DocumentReferenceType
            Dim UUID As New UUIDType
            For Each ds In DocumentResponse
                reference = ds.DocumentReference.FirstOrDefault
                UUID = reference.UUID
            Next
            Return UUID
        End Function

        Private Function GetDocumentReferenceInformation() As List(Of LineReferenceType)
            Dim filePath = Me._electronicDocument.FilePath
            Dim fileName = Me._electronicDocument.GetFileName(Me._supplierThirdParty, TypeElectronicDocument.ApplicationResponse)
            Dim file = System.IO.Path.Combine(filePath, fileName)

            If Me.ValidateIfNotExists(filePath, fileName) Then
                Throw New Exception("No se encontró el ApplicationResponse")
            End If

            Dim xmlfilestring = Me.ReadAllText(filePath, fileName)
            Dim doc As XDocument = XDocument.Parse(xmlfilestring)
            Dim applicationResponse = Utils.Deserialize(Of ApplicationResponseType)(doc)

            Dim lineReferenceType = New LineReferenceType With
            {
                .LineID = New LineIDType With {.Value = 1},
                .DocumentReference = New DocumentReferenceType With
                {
                    .ID = GetIdReference(applicationResponse.DocumentResponse), 'applicationResponse.ID,
                    .UUID = GetUUIDReference(applicationResponse.DocumentResponse), 'applicationResponse.UUID,
                    .IssueDate = applicationResponse.IssueDate,
                    .DocumentType = New DocumentTypeType With {.Value = "ApplicationResponse"},
                    .Attachment = New AttachmentType With
                    {
                        .ExternalReference = New ExternalReferenceType With
                        {
                            .MimeCode = New MimeCodeType With {.Value = "text/xml"},
                            .EncodingCode = New EncodingCodeType With {.Value = "UTF-8"},
                            .Description = New List(Of DescriptionType) From {xmlfilestring}
                        }
                    },
                    .ResultOfVerification = New ResultOfVerificationType With
                    {
                        .ValidatorID = New ValidatorIDType With {.Value = "Unidad Especial Dirección de Impuestos y Aduanas Nacionales"},
                        .ValidationResultCode = New ValidationResultCodeType With {.Value = "02"},
                        .ValidationDate = New ValidationDateType With {.Value = applicationResponse.IssueDate.Value},
                        .ValidationTime = New ValidationTimeType With {.Value = applicationResponse.IssueTime.Value}
                    }
                }
            }

            Dim listLineReferenceType As New List(Of LineReferenceType)
            listLineReferenceType.Add(lineReferenceType)
            Return listLineReferenceType
        End Function

        ''' <summary>
        ''' valida si el documento no existe en la ruta
        ''' </summary>
        ''' <param name="filePath"></param>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        Private Function ValidateIfNotExists(filePath As String, fileName As String) As Boolean
            If _storage IsNot Nothing Then
                Return _storage.ValidateIfNotExists(filePath, fileName)
            Else
                Return Utils.ValidateFileExists(filePath, fileName)
            End If
        End Function

        ''' <summary>
        ''' funcion que lee el documento y retorna una cadena de string
        ''' </summary>
        ''' <param name="filePath"></param>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        Private Function ReadAllText(filePath As String, fileName As String) As String
            If _storage IsNot Nothing Then
                Dim fileBytes = _storage.ReadFile(filePath, fileName)
                Dim xmlString As String = Encoding.UTF8.GetString(fileBytes)
                Return xmlString
            Else
                Dim file = System.IO.Path.Combine(filePath, fileName)
                Return System.IO.File.ReadAllText(file)
            End If
        End Function
#End Region

    End Class

End Namespace
