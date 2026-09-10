Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root

Namespace DIAN.UBL2_1.v1_6

    Public Class CreditNote

#Region "Variables"

        ReadOnly _ublVersionId As New UBLVersionIDType With {.Value = "UBL 2.1"}
        ReadOnly _customizationID As CustomizationIDType
        ReadOnly _profileId As New ProfileIDType With {.Value = "DIAN 2.1: Nota Crédito de Factura Electrónica de Venta"}
        ReadOnly _dianInformation = New With {.DocumentType = "31", .Nit = "800197268", .DigitVerification = "4"}

#End Region

#Region "Properties"

        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _billingNote As BillingNote
        ReadOnly _healthSegmentFromInvoice As CustomTagGeneralType
        ReadOnly _paymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result)
        ReadOnly _invoicePeriodFromInvoice As List(Of PeriodType)

#End Region

#Region "Buldier"

        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings, ByVal supplierThirdParty As ThirdParty, ByVal customerThirdParty As ThirdParty, ByVal billingNote As BillingNote, Optional healthSegmentFromInvoice As CustomTagGeneralType = Nothing, Optional paymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result) = Nothing, Optional invoicePeriodFromInvoice As List(Of PeriodType) = Nothing)
            Me._settingsAccount = settingsAccount
            Me._supplierThirdParty = supplierThirdParty
            Me._customerThirdParty = customerThirdParty
            Me._billingNote = billingNote
            Me._healthSegmentFromInvoice = healthSegmentFromInvoice
            Me._paymentMethods = paymentMethods
            Me._invoicePeriodFromInvoice = invoicePeriodFromInvoice

            AmountType.TlsDefaultCurrencyID = CurrencyCode.COP
            Me._customizationID = New CustomizationIDType With {.Value = Me._billingNote.GetCustomizationID()}
        End Sub

#End Region

#Region "Methods"

        Public Function Populate() As CreditNoteType
            Dim documentType As New CreditNoteType
            Dim invoiceMoreInformation = Me._billingNote?.BillingNoteDetail?.FirstOrDefault?.InvoiceMoreInformation
            documentType.UBLExtensions = GenerateUBLExtensions(invoiceMoreInformation)
            documentType.UBLVersionID = Me._ublVersionId
            documentType.CustomizationID = Me._customizationID
            documentType.ProfileID = Me._profileId
            documentType.ProfileExecutionID = New ProfileExecutionIDType With {.Value = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests))}
            documentType.ID = New IDType With {.Value = Me._billingNote.Code}
            documentType.UUID = New UUIDType With
            {
                .schemeID = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests)),
                .schemeName = "CUDE-SHA384",
                .Value = Me._billingNote.CUDE
            }
            documentType.IssueDate = New IssueDateType With {.Value = CType(Me._billingNote.NoteDate, DateTime).ToString("yyyy-MM-dd")}
            documentType.IssueTime = New IssueTimeType With {.Value = CType(Me._billingNote.NoteDate, DateTime).ToString("HH:mm:ss-05:00")}
            documentType.CreditNoteTypeCode = New CreditNoteTypeCodeType With {.Value = Me._billingNote.GetDocumentType()}
            documentType.Note = New List(Of NoteType) From {New NoteType With {.Value = Me._billingNote.Observations}}
            documentType.DocumentCurrencyCode = New DocumentCurrencyCodeType With {.Value = Utils.GetXmlEnumToString(Of CurrencyCode)(AmountType.TlsDefaultCurrencyID)}
            documentType.LineCountNumeric = New LineCountNumericType With {.Value = If(Me._billingNote.BillingNoteDetail _
                                                                                                .Any(Function(x) x.NoteTypeDetails IsNot Nothing AndAlso x.NoteTypeDetails.Any()),
                                                                                                Me._billingNote.BillingNoteDetail.SelectMany(Function(f) f.NoteTypeDetails).Count(),
                                                                                                Me._billingNote.BillingNoteDetail.Count())}
            documentType.InvoicePeriod = GetInvoicePeriodInformation(invoiceMoreInformation)
            documentType.DiscrepancyResponse = GetDiscrepancyResponseInformation()
            documentType.BillingReference = GetBillingReferencenformation()
            documentType.AccountingSupplierParty = GetAccountingSupplierPartyInformation()
            documentType.AccountingCustomerParty = GetAccountingCustomerPartyInformation()
            documentType.PaymentMeans = GetPaymentsMeansInformation()
            documentType.TaxTotal = GetTaxTotalInformation()
            documentType.LegalMonetaryTotal = GetLegalMonetaryTotalInformation()
            documentType.CreditNoteLine = GetCreditNoteLineInformation()
            Return documentType
        End Function

#End Region

#Region "Private Methods"
        ''' <summary>
        ''' Informacion del periodo de las facturas SALUD
        ''' </summary>
        ''' <param name="invoiceMoreInformation"></param>
        ''' <returns></returns>
        Private Function GetInvoicePeriodInformation(Optional invoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result = Nothing) As List(Of PeriodType)

            If Me._invoicePeriodFromInvoice IsNot Nothing AndAlso Me._invoicePeriodFromInvoice.Any() Then
                Return Me._invoicePeriodFromInvoice
            End If

            If Me._billingNote?.BillingNoteDetail?.FirstOrDefault?.Invoice?.InvoiceDate Is Nothing Then
                Return Nothing
            End If

            'Nota Credito sin referencia a factura necesita enviar el periodo reportado en la emision de la factura
            If Me._billingNote.BillingNoteDetail.Any(Function(x) x.ConceptId = 7) Then

                Dim FirstDay As DateTime = New DateTime(Me._billingNote.BillingNoteDetail.FirstOrDefault.Invoice.InvoiceDate.Year,
                                                         Me._billingNote.BillingNoteDetail.FirstOrDefault.Invoice.InvoiceDate.Month, 1).ToString("yyyy-MM-dd")
                Dim LastDay As DateTime = FirstDay.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd")

                Return New List(Of PeriodType) From
                    {
                        New PeriodType With
                        {
                            .StartDate = New StartDateType With {.Value = FirstDay},
                            .EndDate = New EndDateType With {.Value = LastDay}
                        }
                    }
            End If

            Dim invoiceDate = Me._billingNote.BillingNoteDetail.FirstOrDefault.Invoice.InvoiceDate
            Dim startDate = If(invoiceMoreInformation?.FirstServiceDate, invoiceDate)

            Return New List(Of PeriodType) From
                {
                    New PeriodType With
                    {
                        .StartDate = New StartDateType With {.Value = startDate.ToString("yyyy-MM-dd")},
                        .EndDate = New EndDateType With {.Value = invoiceDate.ToString("yyyy-MM-dd")}
                    }
                }
        End Function

        ''' <summary>
        ''' UBL Etension
        ''' </summary>
        ''' <returns></returns>
        Private Function GenerateUBLExtensions(Optional invoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result = Nothing) As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            If invoiceMoreInformation IsNot Nothing Then
                If _healthSegmentFromInvoice IsNot Nothing Then
                    listUBLExtensionType.Add(GenerateHealthExtensionsFromInvoice())
                Else
                    listUBLExtensionType.Add(GenerateHealthExtensionsEmpty())
                End If
            End If
            listUBLExtensionType.Add(GenerateDianExtensions())
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

        ''' <summary>
        ''' cabecera UBL extension sector Salud
        ''' </summary>
        ''' <returns></returns>
        Private Function GenerateHealthExtensions(invoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result) As UBLExtensionType
            Dim listUBLExtension As New List(Of UBLExtensionType)

            Dim healthExtensions As New CustomTagGeneralType() With
            {
                .Name = New List(Of String) From
                {
                    "Responsable",
                    "Tipo, identificador:año del acto administrativo"
                },
                .Value = New List(Of String) From
                {
                    "url www.minsalud.gov.co",
                    "Resolución 1884:2024"
                },
                .Interoperabilidad = GenerateHealthInteroperabilidadInformation(invoiceMoreInformation)
            }

            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.CustomTagGeneral = healthExtensions}}
        End Function

        ''' <summary>
        ''' cabecera de sector salud
        ''' </summary>
        ''' <returns></returns>
        Private Function GenerateHealthInteroperabilidadInformation(invoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result) As InteroperabilidadTypeHealth
            Return New InteroperabilidadTypeHealth() With
            {
                .Group = New GroupTypeHealth() With
                {
                    .schemeName = "Sector Salud",
                    .Collection = New CollectionTypeHealth With
                    {
                        .schemeName = "Usuario",
                        .AdditionalInformation = GenerateHealthAdditionalInformation(invoiceMoreInformation)
                    }
                }
            }
        End Function

        ''' <summary>
        ''' cabecera sector SALUD
        ''' </summary>
        ''' <returns></returns>
        Private Function GenerateHealthAdditionalInformation(invoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result) As List(Of AdditionalInformationTypeHealth)
            Dim listAdditionalInformation As New List(Of AdditionalInformationTypeHealth)

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CODIGO_PRESTADOR",
                .Value = If(String.IsNullOrEmpty(invoiceMoreInformation.IPSCode), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = invoiceMoreInformation.IPSCode}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "MODALIDAD_PAGO",
                .Value = If(String.IsNullOrEmpty(invoiceMoreInformation.LiquidationTypeCode),
                            Nothing,
                            New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.schemeID = invoiceMoreInformation.LiquidationTypeCode, .schemeName = "salud_modalidad_pago.gc", .Value = invoiceMoreInformation.LiquidationTypeDescription}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "COBERTURA_PLAN_BENEFICIOS",
                .Value = If(String.IsNullOrEmpty(invoiceMoreInformation.CoverageCode), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.schemeID = invoiceMoreInformation.CoverageCode, .schemeName = "salud_cobertura.gc", .Value = invoiceMoreInformation.CoverageDescription}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "NUMERO_CONTRATO",
                .Value = If(String.IsNullOrEmpty(invoiceMoreInformation.ContractNumber), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = invoiceMoreInformation.ContractNumber}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "NUMERO_POLIZA",
                .Value = If(String.IsNullOrEmpty(invoiceMoreInformation.PolicyNumber), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = invoiceMoreInformation.PolicyNumber}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "COPAGO",
                .Value = Nothing
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CUOTA_MODERADORA",
                .Value = Nothing
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CUOTA_RECUPERACION",
                .Value = Nothing
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "PAGOS_COMPARTIDOS",
                .Value = Nothing
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "FACTURA_SIN_CONTRATO",
                .Value = HealthNoContractReasonHelper.BuildValue(invoiceMoreInformation.ContractNumber, invoiceMoreInformation.NoContractReason)
            })

            Return listAdditionalInformation
        End Function

        ''' <summary>
        ''' Genera la UBLExtension de sector salud replicando el segmento de la factura asociada
        ''' </summary>
        Private Function GenerateHealthExtensionsFromInvoice() As UBLExtensionType
            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.CustomTagGeneral = _healthSegmentFromInvoice}}
        End Function

        ''' <summary>
        ''' Genera la UBLExtension de sector salud con estructura completa pero sin valores en los campos
        ''' </summary>
        Private Function GenerateHealthExtensionsEmpty() As UBLExtensionType
            Dim healthExtensions As New CustomTagGeneralType() With
            {
                .Name = New List(Of String) From
                {
                    "Responsable",
                    "Tipo, identificador:año del acto administrativo"
                },
                .Value = New List(Of String) From
                {
                    "url www.minsalud.gov.co",
                    "Resolución 1884:2024"
                },
                .Interoperabilidad = New InteroperabilidadTypeHealth() With
                {
                    .Group = New GroupTypeHealth() With
                    {
                        .schemeName = "Sector Salud",
                        .Collection = New CollectionTypeHealth With
                        {
                            .schemeName = "Usuario",
                            .AdditionalInformation = New List(Of AdditionalInformationTypeHealth) From
                            {
                                New AdditionalInformationTypeHealth With {.Name = "CODIGO_PRESTADOR", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "MODALIDAD_PAGO", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "COBERTURA_PLAN_BENEFICIOS", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "NUMERO_CONTRATO", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "NUMERO_POLIZA", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "COPAGO", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "CUOTA_MODERADORA", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "CUOTA_RECUPERACION", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "PAGOS_COMPARTIDOS", .Value = Nothing},
                                New AdditionalInformationTypeHealth With {.Name = "FACTURA_SIN_CONTRATO", .Value = Nothing}
                            }
                        }
                    }
                }
            }

            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.CustomTagGeneral = healthExtensions}}
        End Function

        Private Function GenerateDianExtensions() As UBLExtensionType
            Dim listUBLExtension As New List(Of UBLExtensionType)

            Dim dianExtensions As New DianExtensionsType() With
            {
                .InvoiceSource = GenerateInvoiceSourceInformation(),
                .SoftwareProvider = GenerateSoftwareProviderInformation(),
                .SoftwareSecurityCode = GenerateSoftwareSecurityCodeInformation(),
                .AuthorizationProvider = GenerateAuthorizationProviderInformation(),
                .QRCode = Me._billingNote.QR
            }

            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.DianExtensions = dianExtensions}}
        End Function

        Private Function GenerateInvoiceSourceInformation() As CountryType
            Return New CountryType() With
            {
                .IdentificationCode = New IdentificationCodeType With
                {
                    .listAgencyID = "6",
                    .listAgencyName = "United Nations Economic Commission for Europe",
                    .listSchemeURI = "urn:oasis:names:specification:ubl:codelist:gc:CountryIdentificationCode-2.1",
                    .Value = "CO"
                }
            }
        End Function

        Private Function GenerateSoftwareProviderInformation() As SoftwareProvider
            Return New SoftwareProvider() With
            {
                .ProviderID = New coID2Type() With
                {
                    .schemeAgencyID = coID2TypeSchemeAgencyID.Item195,
                    .schemeAgencyName = coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales,
                    .schemeID = Me._supplierThirdParty.DigitVerification,
                    .schemeName = Me._supplierThirdParty.Person.getAcquirerType(),
                    .Value = Me._supplierThirdParty.Person.IdentificationNumber
                },
                .SoftwareID = New IdentifierType1() With
                {
                    .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
                    .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
                    .Value = Me._settingsAccount.SoftwareIdentifier
                }
            }
        End Function

        Private Function GenerateSoftwareSecurityCodeInformation() As IdentifierType1
            Return New IdentifierType1() With
            {
                .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
                .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
                .Value = Utils.Sha384Encode(String.Concat(Me._settingsAccount.SoftwareIdentifier, Me._settingsAccount.SoftwarePin, Me._billingNote.Code))
            }
        End Function

        Private Function GenerateAuthorizationProviderInformation() As AuthorizationProvider
            Return New AuthorizationProvider() With
            {
                .AuthorizationProviderID = New coID2Type With
                {
                    .schemeAgencyID = coID2TypeSchemeAgencyID.Item195,
                    .schemeAgencyName = coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales,
                    .schemeID = Me._dianInformation.DigitVerification,
                    .schemeName = Me._dianInformation.DocumentType,
                    .Value = Me._dianInformation.Nit
                }
            }
        End Function

        ''' <summary>
        ''' Genera el elemento DiscrepancyResponse requerido por DIAN para notas crédito
        ''' </summary>
        ''' <returns></returns>
        Private Function GetDiscrepancyResponseInformation() As List(Of ResponseType)
            If Not Me._billingNote.DiscrepancyConceptId.HasValue Then
                Return Nothing
            End If

            Dim listResponseType As New List(Of ResponseType)
            Dim firstDetail = Me._billingNote.BillingNoteDetail.FirstOrDefault()

            listResponseType.Add(New ResponseType With
            {
                .ReferenceID = New ReferenceIDType With {.Value = If(firstDetail IsNot Nothing AndAlso Me._billingNote.DiscrepancyConceptId <> 7, Replace(firstDetail.InvoiceNumber, " ", ""), "")}, 'Si el concepto es "Sin Referencia a una Factura" se deja en blanco
                .ResponseCode = New ResponseCodeType With {.Value = Me._billingNote.DiscrepancyConceptId.Value.ToString()},
                .Description = New List(Of DescriptionType) From
                {
                    New DescriptionType(If(String.IsNullOrEmpty(Me._billingNote.Observations), "Nota Crédito", Me._billingNote.Observations))
                }
            })

            Return listResponseType
        End Function

        Private Function GetBillingReferencenformation() As List(Of BillingReferenceType)
            Dim listBillingReferenceType As New List(Of BillingReferenceType)
            If Me._billingNote.BillingNoteDetail.Any(Function(f) f.CUFE <> String.Empty) Then
                For Each detail In Me._billingNote.BillingNoteDetail

                    'Notas creditos sin referencia a factura
                    If detail.ConceptId = 7 Then
                        Continue For
                    End If

                    listBillingReferenceType.Add(New BillingReferenceType With
                    {
                        .InvoiceDocumentReference = New DocumentReferenceType With
                        {
                            .ID = New IDType With {.Value = Replace(detail.InvoiceNumber, " ", "")},
                            .UUID = New UUIDType With
                            {
                                .schemeName = "CUFE-SHA384",
                                .Value = detail.CUFE
                            },
                            .IssueDate = New IssueDateType With {.Value = CType(detail.DocumentDate, DateTime).ToString("yyyy-MM-dd")}
                        }
                    })
                Next
            End If

            Return IIf(listBillingReferenceType.Any(), listBillingReferenceType, Nothing)
        End Function

        Private Function GetAccountingSupplierPartyInformation() As SupplierPartyType
            Return New SupplierPartyType With
            {
                .AdditionalAccountID = New List(Of AdditionalAccountIDType) From {New AdditionalAccountIDType With {.Value = Me._supplierThirdParty.getAdditionalAccountID()}},
                .Party = Me.GetPartyTypeInformation(Me._supplierThirdParty)
            }
        End Function

        Private Function GetAccountingCustomerPartyInformation() As CustomerPartyType
            Return New CustomerPartyType With
            {
                .AdditionalAccountID = New List(Of AdditionalAccountIDType) From {New AdditionalAccountIDType With {.Value = Me._customerThirdParty.getAdditionalAccountID()}},
                .Party = Me.GetPartyTypeInformation(Me._customerThirdParty)
            }
        End Function

        Private Function GetPartyTypeInformation(ByVal thirdParty As Domain.Entities.ThirdParty) As PartyType
            Dim party As New PartyType

            If thirdParty.getAdditionalAccountID() = "2" Then
                party.PartyIdentification = New List(Of PartyIdentificationType) From
                {
                    New PartyIdentificationType With
                    {
                        .ID = New IDType With
                        {
                            .schemeID = thirdParty.DigitVerification,
                            .schemeName = thirdParty.Person.getAcquirerType(),
                            .Value = thirdParty.Nit
                        }
                    }
                }
            End If

            party.PartyName = New List(Of PartyNameType) From
            {
                New PartyNameType With
                {
                    .Name = New NameType1 With {.Value = thirdParty.Name}
                }
            }

            Dim address = thirdParty.Person.Address.First(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            party.PhysicalLocation = New LocationType1 With
            {
                .Address = New AddressType With
                {
                    .ID = New IDType With {.Value = address.CityCode},
                    .CityName = New CityNameType With {.Value = address.CityName},
                    .CountrySubentity = New CountrySubentityType With {.Value = address.DepartmentName},
                    .CountrySubentityCode = New CountrySubentityCodeType With {.Value = address.DepartmentCode},
                    .AddressLine = New List(Of AddressLineType) From
                    {
                        New AddressLineType With {.Line = New LineType With {.Value = address.Addresss}}
                    },
                    .Country = New CountryType With
                    {
                        .IdentificationCode = New IdentificationCodeType With {.Value = address.CountryStandardCode},
                        .Name = New NameType1 With {.languageID = "es", .Value = "Colombia"}
                    }
                }
            }

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
                    .RegistrationAddress = New AddressType With
                    {
                        .ID = New IDType With {.Value = address.CityCode},
                        .CityName = New CityNameType With {.Value = address.CityName},
                        .CountrySubentity = New CountrySubentityType With {.Value = address.DepartmentName},
                        .CountrySubentityCode = New CountrySubentityCodeType With {.Value = address.DepartmentCode},
                        .AddressLine = New List(Of AddressLineType) From
                        {
                            New AddressLineType With {.Line = New LineType With {.Value = address.Addresss}}
                        },
                        .Country = New CountryType With
                        {
                            .IdentificationCode = New IdentificationCodeType With {.Value = address.CountryStandardCode},
                            .Name = New NameType1 With {.languageID = "es", .Value = "Colombia"}
                        }
                    },
                    .TaxScheme = New TaxSchemeType With
                    {
                        .ID = New IDType With {.Value = thirdParty.getTaxSchemeID()},
                        .Name = New NameType1 With {.Value = thirdParty.getTaxSchemeName()}
                    }
                }
            }

            party.PartyLegalEntity = New List(Of PartyLegalEntityType) From
            {
                New PartyLegalEntityType With
                {
                    .RegistrationName = New RegistrationNameType With {.Value = thirdParty.Name},
                    .CompanyID = New CompanyIDType With
                    {
                        .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
                        .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
                        .schemeID = thirdParty.DigitVerification,
                        .schemeName = thirdParty.Person.getAcquirerType(),
                        .Value = thirdParty.Nit
                    }
                }
            }

            If thirdParty.Person.Email IsNot Nothing AndAlso thirdParty.Person.Email.Any(Function(e) e.Type = 2) Then
                party.Contact = New ContactType With
                {
                    .ElectronicMail = New ElectronicMailType With {.Value = thirdParty.Person.Email.FirstOrDefault(Function(e) e.Type = 2).Email1}
                }
            End If

            Return party
        End Function

        Private Function GetPaymentsMeansInformation() As List(Of PaymentMeansType)
            Dim listPaymentMeansType As New List(Of PaymentMeansType)
            Dim firstInvoice = Me._billingNote?.BillingNoteDetail?.FirstOrDefault()?.Invoice
            Dim dueDate As String = CType(Me._billingNote.NoteDate, DateTime).ToString("yyyy-MM-dd")

            Dim paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit
            If Me._paymentMethods IsNot Nothing AndAlso Me._paymentMethods.Any() AndAlso firstInvoice IsNot Nothing Then
                Dim paymentSum = Me._paymentMethods.Where(Function(d) Not {4}.Contains(d.AccountReceivableType)).Sum(Function(d) d.Value)

                ' Calcular el valor neto a pagar (valor factura + IVA - retenciones)
                Dim netPayableValue = firstInvoice.InvoiceValue + firstInvoice.ValueTax - firstInvoice.RTFValue - firstInvoice.WithholdingTax - firstInvoice.WithholdingICA

                If paymentSum = netPayableValue OrElse paymentSum = firstInvoice.InvoiceValue Then
                    paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Counted
                End If

                For Each detail In Me._paymentMethods
                    listPaymentMeansType.Add(New PaymentMeansType With
                    {
                        .ID = New IDType With {.Value = Utils.GetXmlEnumToString(Of Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods)(paymentMethod)},
                        .PaymentMeansCode = New PaymentMeansCodeType With {.Value = detail.MethodTypeCode},
                        .PaymentID = New List(Of PaymentIDType) From {New PaymentIDType With {.Value = detail.Code}},
                        .PaymentDueDate = If(paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit,
                                     New PaymentDueDateType With {.Value = CType(dueDate, DateTime).ToString("yyyy-MM-dd")},
                                     Nothing)
                    })
                Next
            Else
                ' Solo cuando no hay métodos de pago, usar ZZZ genérico
                listPaymentMeansType.Add(New PaymentMeansType With
                {
                    .ID = New IDType With {.Value = Utils.GetXmlEnumToString(Of Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods)(Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit)},
                    .PaymentMeansCode = New PaymentMeansCodeType With {.Value = "ZZZ"},
                    .PaymentDueDate = New PaymentDueDateType With {.Value = dueDate}
                })
            End If
            Return listPaymentMeansType
        End Function

        Private Function GetTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'IVA
            If Me._billingNote.BillingNoteDetail.Any(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Count > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim details = Me._billingNote.BillingNoteDetail.Where(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Count > 0).SelectMany(Function(d) d.BillingNoteDetailTax)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                ' Gravados (TaxClassificationType = 1): porcentaje > 0, agrupados por porcentaje
                For Each tax In details.Where(Function(t) t.TaxPercentage > 0).GroupBy(Function(t) t.TaxPercentage)
                    TaxableAmount = tax.Sum(Function(d) d.BaseValue)
                    TaxAmount = tax.Sum(Function(d) d.TaxValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "01"},
                                .Name = New NameType1 With {.Value = "IVA"}
                            }
                        }
                    })
                Next

                ' Exentos (TaxClassificationType = 3): porcentaje = 0, valor impuesto = 0, agrupados por IVAId
                ' Solo pct=0 llega aquí porque excluidos (tipo 2) son filtrados en la capa de servicio
                For Each exent In details.Where(Function(t) t.TaxPercentage = 0 AndAlso t.IVAId.HasValue).GroupBy(Function(t) t.IVAId)
                    TaxableAmount = exent.Sum(Function(d) d.BaseValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = 0},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = 0},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "01"},
                                .Name = New NameType1 With {.Value = "IVA"}
                            }
                        }
                    })
                Next

                TaxAmount = details.Sum(Function(d) d.TaxValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetLegalMonetaryTotalInformation() As MonetaryTotalType
            Dim LineExtensionAmount As Decimal = Me._billingNote.BillingNoteDetail.Sum(Function(d) d.BillingValue)
            Dim TaxInclusiveAmount As Decimal = LineExtensionAmount
            Dim TaxExclusiveAmount As Decimal = 0
            Dim AllowanceTotalAmount As Decimal = 0
            Dim ChargeTotalAmount As Decimal = 0
            Dim PayableAmount As Decimal = 0

            If Me._billingNote.BillingNoteDetail.Any(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Any()) Then
                TaxExclusiveAmount = Me._billingNote.BillingNoteDetail _
                                        .Where(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Any()) _
                                        .SelectMany(Function(d) d.BillingNoteDetailTax).Sum(Function(d) d.BaseValue)
                TaxInclusiveAmount = TaxInclusiveAmount + Me._billingNote.BillingNoteDetail _
                                                            .Where(Function(d) d.BillingNoteDetailTax IsNot Nothing AndAlso d.BillingNoteDetailTax.Any()) _
                                                            .SelectMany(Function(d) d.BillingNoteDetailTax).Sum(Function(d) d.TaxValue)
            End If
            PayableAmount = TaxInclusiveAmount

            Return New MonetaryTotalType With
            {
                .LineExtensionAmount = New LineExtensionAmountType With {.Value = LineExtensionAmount},
                .TaxExclusiveAmount = New TaxExclusiveAmountType With {.Value = TaxExclusiveAmount},
                .TaxInclusiveAmount = New TaxInclusiveAmountType With {.Value = TaxInclusiveAmount},
                .AllowanceTotalAmount = New AllowanceTotalAmountType With {.Value = AllowanceTotalAmount},
                .ChargeTotalAmount = New ChargeTotalAmountType With {.Value = ChargeTotalAmount},
                .PayableAmount = New PayableAmountType With {.Value = PayableAmount}
            }
        End Function

        ''' <summary>
        ''' Funcion que obtiene los datos del segmeto CreditNoteLine
        ''' </summary>
        ''' <returns></returns>
        Private Function GetCreditNoteLineInformation() As List(Of CreditNoteLineType)
            Dim listCreditNoteLineType As New List(Of CreditNoteLineType)

            Dim i = 0
            For Each detail In Me._billingNote.BillingNoteDetail
                i = i + 1
                If detail?.NoteTypeDetails?.Any() Then
                    Dim result = GetCreditNoteLineNoteTypeDetail(detail, i)
                    If result Is Nothing OrElse Not result.Any() Then
                        Continue For
                    End If
                    i = Convert.ToInt32(result.Max(Function(d) d.ID.Value))
                    listCreditNoteLineType.AddRange(result)
                Else
                    listCreditNoteLineType.Add(GetCreditNoteLine(detail, i))
                End If
            Next

            Return listCreditNoteLineType
        End Function

        ''' <summary>
        ''' Funcion que obtiene los datos del detalle de la nota tipo detalle
        ''' </summary>
        ''' <param name="billingNoteDetail"></param>
        ''' <param name="i"></param>
        ''' <returns></returns>
        Private Function GetCreditNoteLineNoteTypeDetail(billingNoteDetail As BillingNoteDetail, Optional i As Integer = 1) As List(Of CreditNoteLineType)
            Dim listCreditNoteLineType As New List(Of CreditNoteLineType)
            For Each detail In billingNoteDetail.NoteTypeDetails

                Dim creditNoteLineType = New CreditNoteLineType() With
                                  {
                                      .ID = New IDType With {.Value = i},
                                      .CreditedQuantity = New CreditedQuantityType With
                                      {
                                          .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                                          .Value = 1
                                      },
                                      .LineExtensionAmount = New LineExtensionAmountType With {.Value = detail.BaseValue}
                                   }

                If detail.TaxClassificationType.HasValue AndAlso (detail.TaxClassificationType = 1 OrElse detail.TaxClassificationType = 3) _
                    AndAlso billingNoteDetail.BillingNoteDetailTax IsNot Nothing AndAlso billingNoteDetail.BillingNoteDetailTax.Any() Then
                    creditNoteLineType.TaxTotal = Me.CreditNoteLineTaxTotalInformation(New List(Of BillingNoteDetailTax) _
                                                                                 From {New BillingNoteDetailTax() With {.BaseValue = detail.BaseValue,
                                                                                                                        .TaxPercentage = detail.TaxPercentage,
                                                                                                                        .TaxValue = detail.TaxValue}})
                End If

                'Datos del producto o servicio
                creditNoteLineType.Item = New ItemType With
                {
                    .Description = New List(Of DescriptionType) From
                    {
                        New DescriptionType(detail.Name),
                        If(String.IsNullOrEmpty(detail.NameAlternative) OrElse detail.NameAlternative = detail.Name,
                            Nothing, New DescriptionType(detail.NameAlternative)),
                        If(String.IsNullOrEmpty(detail.NameAlternativeTwo) _
                            OrElse detail.NameAlternativeTwo = detail.Name _
                            OrElse detail.NameAlternativeTwo = detail.NameAlternative,
                            Nothing, New DescriptionType(detail.NameAlternativeTwo))
                    },
                    .StandardItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = detail.Code, .schemeID = "999"}},
                    .AdditionalItemIdentification = If(detail.Code = detail.CodeAlternative AndAlso detail.Code = detail.CodeAlternativeTwo, Nothing,
                                                    New List(Of ItemIdentificationType) From
                                                    {
                                                        If(String.IsNullOrEmpty(detail.CodeAlternative) OrElse detail.Code = detail.CodeAlternative,
                                                                Nothing,
                                                                New ItemIdentificationType With {.ID = New IDType With {.Value = detail.CodeAlternative}}),
                                                        If(String.IsNullOrEmpty(detail.CodeAlternativeTwo) _
                                                            OrElse detail.Code = detail.CodeAlternativeTwo _
                                                            OrElse detail.CodeAlternative = detail.CodeAlternativeTwo,
                                                                Nothing,
                                                                New ItemIdentificationType With {.ID = New IDType With {.Value = detail.CodeAlternativeTwo}})
                                                    })
                }

                creditNoteLineType.Price = New PriceType With
                {
                    .PriceAmount = New PriceAmountType With {.Value = detail.UnitValue},
                    .BaseQuantity = New BaseQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = detail.InvoicedQuantity
                    }
                }
                i += 1
                listCreditNoteLineType.Add(creditNoteLineType)
            Next
            Return listCreditNoteLineType
        End Function

        ''' <summary>
        ''' funcion que obtiene los datos de las nota de tipo factura total
        ''' </summary>
        ''' <param name="billingNoteDetail"></param>
        ''' <param name="i"></param>
        ''' <returns></returns>
        Private Function GetCreditNoteLine(billingNoteDetail As BillingNoteDetail, Optional i As Integer = 1) As CreditNoteLineType

            If billingNoteDetail Is Nothing Then
                Return New CreditNoteLineType
            End If

            Dim CreditNoteLineType = New CreditNoteLineType() With
                {
                    .ID = New IDType With {.Value = i},
                    .CreditedQuantity = New CreditedQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = 1
                    },
                    .LineExtensionAmount = New LineExtensionAmountType With {.Value = billingNoteDetail.BillingValue}
                }

            CreditNoteLineType.TaxTotal = Me.CreditNoteLineTaxTotalInformation(billingNoteDetail?.BillingNoteDetailTax?.ToList())

            'Datos del producto o servicio
            CreditNoteLineType.Item = New ItemType With
            {
                .Description = New List(Of DescriptionType) From
                {
                    New DescriptionType("Registro nota factura " + Replace(billingNoteDetail.InvoiceNumber, " ", ""))
                }
            }

            CreditNoteLineType.Price = New PriceType With
            {
                .PriceAmount = New PriceAmountType With {.Value = billingNoteDetail.BillingValue},
                .BaseQuantity = New BaseQuantityType With
                {
                    .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                    .Value = 1
                }
            }

            Return CreditNoteLineType
        End Function

        ''' <summary>
        ''' funcion que crea los detalle de los impuestos del detalle de la nota
        ''' </summary>
        ''' <param name="listBillingNoteDetailTax"></param>
        ''' <returns></returns>
        Private Function CreditNoteLineTaxTotalInformation(listBillingNoteDetailTax As List(Of BillingNoteDetailTax)) As List(Of TaxTotalType)
            If listBillingNoteDetailTax Is Nothing OrElse Not listBillingNoteDetailTax.Any() Then
                Return Nothing
            End If

            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'IVA
            Dim listTaxSubtotal As New List(Of TaxSubtotalType)
            Dim details = listBillingNoteDetailTax
            Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In details.GroupBy(Function(t) t.TaxPercentage)
                    TaxableAmount = details.Where(Function(d) d.TaxPercentage = tax.Key).Sum(Function(d) d.BaseValue)
                    TaxAmount = details.Where(Function(d) d.TaxPercentage = tax.Key).Sum(Function(d) d.TaxValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "01"},
                                .Name = New NameType1 With {.Value = "IVA"}
                            }
                        }
                    })
                Next

                TaxAmount = details.Sum(Function(d) d.TaxValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })

            Return listTaxTotalTypes
        End Function

#End Region

    End Class

End Namespace
