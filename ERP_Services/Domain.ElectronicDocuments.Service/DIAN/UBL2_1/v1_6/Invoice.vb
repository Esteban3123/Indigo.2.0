Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root

Namespace DIAN.UBL2_1.v1_6

    Public Class Invoice

#Region "Variables"

        ReadOnly _ublVersionId As New UBLVersionIDType With {.Value = "UBL 2.1"}
        ReadOnly _customizationID As New CustomizationIDType
        ReadOnly _profileId As New ProfileIDType With {.Value = "DIAN 2.1: Factura Electrónica de Venta"}
        ReadOnly _dianInformation = New With {.DocumentType = "31", .Nit = "800197268", .DigitVerification = "4"}

#End Region

#Region "Properties"

        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _billingAuthorization As BillingAuthorization
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _invoice As Domain.Entities.Invoice
        ReadOnly _paymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result)
        ReadOnly _previousCapitationPeriodEndDate As Nullable(Of Date)

#End Region

#Region "Buldier"

        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings, ByVal supplierThirdParty As ThirdParty, ByVal customerThirdParty As ThirdParty, ByVal invoice As Domain.Entities.Invoice, ByVal billingAuthorization As BillingAuthorization, ByVal paymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result), Optional ByVal previousCapitationPeriodEndDate As Nullable(Of Date) = Nothing)
            Me._settingsAccount = settingsAccount
            Me._billingAuthorization = billingAuthorization
            Me._supplierThirdParty = supplierThirdParty
            Me._customerThirdParty = customerThirdParty
            Me._invoice = invoice
            Me._paymentMethods = paymentMethods
            Me._previousCapitationPeriodEndDate = previousCapitationPeriodEndDate

            AmountType.TlsDefaultCurrencyID = CurrencyCode.COP
            If Me._invoice.InvoiceMoreInformation IsNot Nothing Then
                _customizationID.Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation?.OperationMode), "SS-CUFE", Me._invoice.InvoiceMoreInformation?.OperationMode)
                _customizationID.schemeID = "SS-CUFE"
                _customizationID.schemeName = "Sector Salud"
                _customizationID.schemeAgencyName = "www.minsalud.gov.co"
                _customizationID.schemeDataURI = "TipoOperacionF-2.1_SSalud.gc"
            ElseIf Me._invoice?.InvoiceCopayCustom IsNot Nothing Then
                _customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.MandatesGoods)
            Else
                _customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.Standard)
            End If
        End Sub

#End Region

#Region "Methods"

        Private Function GetLineExtensionAmountValue(detail As SP_GetInvoiceDetailsByInvoiceId_Result) As Decimal
            If Me._invoice.DocumentType = 4 Then
                Return detail.LineExtensionAmountValueCapitation
            End If
            Return detail.LineExtensionAmountValue
        End Function

        Private Function GetWithholdingIVATaxableAmount(detail As SP_GetInvoiceDetailsByInvoiceId_Result) As Decimal
            If Me._invoice.DocumentType = 6 Then
                Return If(detail.IVAValue.HasValue, detail.IVAValue.Value, 0D)
            End If
            Return GetLineExtensionAmountValue(detail)
        End Function

        Public Function Populate() As InvoiceType
            Dim documentType As New InvoiceType
            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.UBLVersionID = Me._ublVersionId
            documentType.CustomizationID = Me._customizationID.Value ' se modifico para Documento Soporte, aqui seria solo la entidad 
            documentType.ProfileID = Me._profileId
            documentType.ProfileExecutionID = New ProfileExecutionIDType With {.Value = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests))}
            documentType.ID = New IDType With {.Value = Me._invoice.InvoiceNumber}
            documentType.UUID = New UUIDType With
            {
                .schemeID = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests)),
                .schemeName = "CUFE-SHA384",
                .Value = Me._invoice.CUFE
            }
            documentType.IssueDate = New IssueDateType With {.Value = CType(Me._invoice.InvoiceDate, DateTime).ToString("yyyy-MM-dd")}
            documentType.IssueTime = New IssueTimeType With {.Value = CType(Me._invoice.InvoiceDate, DateTime).ToString("HH:mm:ss-05:00")}
            documentType.InvoiceTypeCode = New InvoiceTypeCodeType With {.Value = Utils.GetXmlEnumToString(Of DocumentType)(Me._invoice.getInvoiceTypeCode())}
            documentType.Note = New List(Of NoteType) From {New NoteType With {.Value = Me._invoice.getObservation()}}
            documentType.DocumentCurrencyCode = New DocumentCurrencyCodeType With {.Value = Utils.GetXmlEnumToString(Of CurrencyCode)(AmountType.TlsDefaultCurrencyID)}
            documentType.LineCountNumeric = New LineCountNumericType With {.Value = Me._invoice.InvoiceDetails.Count}
            documentType.InvoicePeriod = GetInvoicePeriodInformation()
            documentType.BillingReference = Nothing 'Si la factura es generada desde una nota (Debito o crédito)
            documentType.AdditionalDocumentReference = Nothing ' Si es una factura de contingencia Facturador se debe informar
            documentType.AccountingSupplierParty = GetAccountingSupplierPartyInformation()
            documentType.AccountingCustomerParty = GetAccountingCustomerPartyInformation()
            documentType.PaymentMeans = GetPaymentMeansInformation()
            documentType.PrepaidPayment = GetPrepaidPaymentInformation()
            documentType.TaxTotal = GetTaxTotalInformation()
            documentType.WithholdingTaxTotal = GetWithholdingTaxTotalInformation()
            documentType.LegalMonetaryTotal = GetLegalMonetaryTotalInformation()
            documentType.InvoiceLine = GetInvoiceLineInformation()
            Return documentType
        End Function

#End Region

#Region "Private Methods"

        Private Function GenerateUBLExtensions() As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            If Me._invoice.InvoiceMoreInformation IsNot Nothing Then
                listUBLExtensionType.Add(GenerateHealthExtensions())
            End If
            listUBLExtensionType.Add(GenerateDianExtensions())
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

#Region "MinSalud"

        Private Function GenerateHealthExtensions() As UBLExtensionType
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
                .Interoperabilidad = GenerateHealthInteroperabilidadInformation()
            }

            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.CustomTagGeneral = healthExtensions}}
        End Function

        Private Function GenerateHealthInteroperabilidadInformation() As InteroperabilidadTypeHealth
            Return New InteroperabilidadTypeHealth() With
            {
                .Group = New GroupTypeHealth() With
                {
                    .schemeName = "Sector Salud",
                    .Collection = New CollectionTypeHealth With
                    {
                        .schemeName = "Usuario",
                        .AdditionalInformation = GenerateHealthAdditionalInformation()
                    }
                }
            }
        End Function

        Private Function GenerateHealthAdditionalInformation() As List(Of AdditionalInformationTypeHealth)
            Dim listAdditionalInformation As New List(Of AdditionalInformationTypeHealth)

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CODIGO_PRESTADOR",
                .Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.IPSCode), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.IPSCode}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "MODALIDAD_PAGO",
                .Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.LiquidationTypeCode),
                            Nothing,
                            New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.schemeID = Me._invoice.InvoiceMoreInformation.LiquidationTypeCode, .schemeName = "salud_modalidad_pago.gc", .Value = Me._invoice.InvoiceMoreInformation.LiquidationTypeDescription}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "COBERTURA_PLAN_BENEFICIOS",
                .Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.CoverageCode), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.schemeID = Me._invoice.InvoiceMoreInformation.CoverageCode, .schemeName = "salud_cobertura.gc", .Value = Me._invoice.InvoiceMoreInformation.CoverageDescription}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "NUMERO_CONTRATO",
                .Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.ContractNumber), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.ContractNumber}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "NUMERO_POLIZA",
                .Value = If(String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.PolicyNumber), Nothing, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.PolicyNumber}})
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "COPAGO",
                .Value = If(Me._invoice.InvoiceMoreInformation.CopayValue > 0, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.CopayValue.ToString("0.00", Globalization.CultureInfo.InvariantCulture)}}, Nothing)
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CUOTA_MODERADORA",
                .Value = If(Me._invoice.InvoiceMoreInformation.FeeModeratorValue > 0, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.FeeModeratorValue.ToString("0.00", Globalization.CultureInfo.InvariantCulture)}}, Nothing)
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "ANTICIPO",
                .Value = If(Me._invoice.InvoiceMoreInformation.PortfolioAdvanceEntityValue > 0, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.PortfolioAdvanceEntityValue.ToString("0.00", Globalization.CultureInfo.InvariantCulture)}}, Nothing)
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "CUOTA_RECUPERACION",
                .Value = If(Me._invoice.InvoiceMoreInformation.FeeRecoveryValue > 0, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.FeeRecoveryValue.ToString("0.00", Globalization.CultureInfo.InvariantCulture)}}, Nothing)
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "PAGOS_COMPARTIDOS",
                .Value = If(Me._invoice.InvoiceMoreInformation.DistributedValue > 0, New List(Of ValueTypeHealth) From {New ValueTypeHealth With {.Value = Me._invoice.InvoiceMoreInformation.DistributedValue.ToString("0.00", Globalization.CultureInfo.InvariantCulture)}}, Nothing)
            })

            listAdditionalInformation.Add(New AdditionalInformationTypeHealth With
            {
                .Name = "FACTURA_SIN_CONTRATO",
                .Value = HealthNoContractReasonHelper.BuildValue(Me._invoice.InvoiceMoreInformation.ContractNumber, Me._invoice.InvoiceMoreInformation.NoContractReason)
            })

            Return listAdditionalInformation
        End Function

        Private Function GenerateHealthAdditionalInformationAuthorization() As AdditionalInformationTypeHealth
            Dim additionalInformation = New AdditionalInformationTypeHealth With {.Name = "NUMERO_AUTORIZACIÓN"}

            If Not String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.AuthorizationNumber) Then
                additionalInformation.Value = New List(Of ValueTypeHealth)
                Dim authorizationNumbers = Me._invoice.InvoiceMoreInformation.AuthorizationNumber.Split(",")
                For Each authorizationNumber In authorizationNumbers
                    additionalInformation.Value.Add(New ValueTypeHealth With {.Value = authorizationNumber.Trim})
                Next
            End If

            Return additionalInformation
        End Function

        Private Function GenerateHealthAdditionalInformationMIPRESNumber() As AdditionalInformationTypeHealth
            Dim additionalInformation = New AdditionalInformationTypeHealth With {.Name = "NUMERO_MIPRES"}

            If Not String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.MIPRESNumber) Then
                additionalInformation.Value = New List(Of ValueTypeHealth)
                Dim mIPRESNumbers = Me._invoice.InvoiceMoreInformation.MIPRESNumber.Split(",")
                For Each mIPRESNumber In mIPRESNumbers
                    additionalInformation.Value.Add(New ValueTypeHealth With {.Value = mIPRESNumber.Trim})
                Next
            End If

            Return additionalInformation
        End Function

        Private Function GenerateHealthAdditionalInformationMIPRESId() As AdditionalInformationTypeHealth
            Dim additionalInformation = New AdditionalInformationTypeHealth With {.Name = "NUMERO_ENTREGA_MIPRES"}

            If Not String.IsNullOrEmpty(Me._invoice.InvoiceMoreInformation.MIPRESId) Then
                additionalInformation.Value = New List(Of ValueTypeHealth)
                Dim mIPRESIds = Me._invoice.InvoiceMoreInformation.MIPRESId.Split(",")
                For Each mIPRESId In mIPRESIds
                    additionalInformation.Value.Add(New ValueTypeHealth With {.Value = mIPRESId.Trim})
                Next
            End If

            Return additionalInformation
        End Function

#End Region

#Region "DIAN"

        Private Function GenerateDianExtensions() As UBLExtensionType
            Dim listUBLExtension As New List(Of UBLExtensionType)

            Dim dianExtensions As New DianExtensionsType() With
            {
                .InvoiceControl = GenerateInvoiceControlInformation(),
                .InvoiceSource = GenerateInvoiceSourceInformation(),
                .SoftwareProvider = GenerateSoftwareProviderInformation(),
                .SoftwareSecurityCode = GenerateSoftwareSecurityCodeInformation(),
                .AuthorizationProvider = GenerateAuthorizationProviderInformation(),
                .QRCode = Me._invoice.QR
            }

            Return New UBLExtensionType With {.ExtensionContent = New ExtensionContentType With {.DianExtensions = dianExtensions}}
        End Function

        Private Function GenerateInvoiceControlInformation() As InvoiceControl
            Return New InvoiceControl() With
            {
                .InvoiceAuthorization = Me._billingAuthorization.ResolutionNumber,
                .AuthorizationPeriod = New PeriodType() With
                {
                    .StartDate = New StartDateType With {.Value = CType(Me._billingAuthorization.InitialDate, DateTime).ToString("yyyy-MM-dd")},
                    .EndDate = New EndDateType With {.Value = CType(Me._billingAuthorization.FinalDate, DateTime).ToString("yyyy-MM-dd")}
                },
                .AuthorizedInvoices = New AuthrorizedInvoices() With
                {
                    .Prefix = Me._billingAuthorization.InvoicePrefix,
                    .From = Me._billingAuthorization.InitialInvoice,
                    .To = Me._billingAuthorization.FinalInvoice
                }
            }
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
                .Value = Utils.Sha384Encode(String.Concat(Me._settingsAccount.SoftwareIdentifier, Me._settingsAccount.SoftwarePin, Me._invoice.InvoiceNumber))
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

#End Region

        Private Function GetInvoicePeriodInformation() As List(Of PeriodType)

            Dim firstDate As Date?
            If Me._invoice.InvoiceMoreInformation?.FirstServiceDate IsNot Nothing Then
                firstDate = Me._invoice.InvoiceMoreInformation.FirstServiceDate
            End If
            Dim periodType = New PeriodType()

            If firstDate IsNot Nothing Then
                periodType.StartDate = New StartDateType With {.Value = firstDate.Value.ToString("yyyy-MM-dd")}
                periodType.StartTime = New StartTimeType With {.Value = New DateTime(firstDate.Value.Year, firstDate.Value.Month, firstDate.Value.Day).ToString("HH:mm:ss-05:00")}
            End If

            If Me._invoice.DocumentType = 1 OrElse Me._invoice.DocumentType = 2 Then
                Dim endDate = Me._invoice.OutputDate

                periodType.EndDate = New EndDateType With {.Value = endDate.ToString("yyyy-MM-dd")}
                periodType.EndTime = New EndTimeType With {.Value = New DateTime(endDate.Year, endDate.Month, endDate.Day).AddDays(1).AddSeconds(-1).ToString("HH:mm:ss-05:00")}

                Return New List(Of PeriodType) From {periodType}
            ElseIf Me._invoice.DocumentType = 4 Then
                Dim endDate As DateTime
                If Me._previousCapitationPeriodEndDate.HasValue Then
                    endDate = Me._previousCapitationPeriodEndDate.Value
                Else
                    endDate = CType(Me._invoice.CapitationEndDate, DateTime)
                End If

                periodType.EndDate = New EndDateType With {.Value = endDate.ToString("yyyy-MM-dd")}
                periodType.EndTime = New EndTimeType With {.Value = endDate.AddDays(1).AddSeconds(-1).ToString("HH:mm:ss-05:00")}
                Return New List(Of PeriodType) From {periodType}
            End If

            Return Nothing
        End Function

#Region "ThirdParty"

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
                    .TaxLevelCode = New TaxLevelCodeType With {.Value = thirdParty.getTaxLevelCode()},
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
                    },
                    .CorporateRegistrationScheme = New CorporateRegistrationSchemeType With {.ID = New IDType With {.Value = Me._billingAuthorization.InvoicePrefix}}
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

#End Region

        Private Function GetPaymentMeansInformation() As List(Of PaymentMeansType)
            Dim listPaymentMeansType As New List(Of PaymentMeansType)

            Dim paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit
            If Me._paymentMethods IsNot Nothing AndAlso Me._paymentMethods.Any() Then
                Dim paymentSum = Me._paymentMethods.Where(Function(d) Not {4}.Contains(d.AccountReceivableType)).Sum(Function(d) d.Value)

                Dim netPayableValue = Me._invoice.InvoiceValue + Me._invoice.ValueTax - Me._invoice.RTFValue - Me._invoice.WithholdingTax - Me._invoice.WithholdingICA
                If paymentSum = netPayableValue OrElse paymentSum = Me._invoice.InvoiceValue Then
                    paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Counted
                End If
                ' Si hay métodos de pago, registrarlos con sus datos (ya sea Contado o Crédito)
                For Each detail In Me._paymentMethods
                    listPaymentMeansType.Add(New PaymentMeansType With
                    {
                        .ID = New IDType With {.Value = Utils.GetXmlEnumToString(Of Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods)(paymentMethod)},
                        .PaymentMeansCode = New PaymentMeansCodeType With {.Value = detail.MethodTypeCode},
                        .PaymentID = New List(Of PaymentIDType) From {New PaymentIDType With {.Value = detail.Code}},
                        .PaymentDueDate = If(paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit,
                                             New PaymentDueDateType With {.Value = CType(Me._invoice.InvoiceExpirationDate, DateTime).ToString("yyyy-MM-dd")},
                                             Nothing)
                    })
                Next
            Else
                ' Solo cuando no hay métodos de pago, usar ZZZ genérico
                listPaymentMeansType.Add(New PaymentMeansType With
                {
                    .ID = New IDType With {.Value = Utils.GetXmlEnumToString(Of Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods)(paymentMethod)},
                    .PaymentMeansCode = New PaymentMeansCodeType With {.Value = "ZZZ"},
                    .PaymentDueDate = New PaymentDueDateType With {.Value = CType(Me._invoice.InvoiceExpirationDate, DateTime).ToString("yyyy-MM-dd")}
                })
            End If

            Return listPaymentMeansType
        End Function

        ''' <summary>
        ''' Retorna el segemento de anticipos segun metodos de pago o tipo de recaudo 
        ''' </summary>
        ''' <returns></returns>
        Private Function GetPrepaidPaymentInformation() As List(Of PaymentType)
            Dim listPaymentType As New List(Of PaymentType)

            If Me._invoice.InvoicePrepaidPayment?.Any() Then

                listPaymentType = Me._invoice.InvoicePrepaidPayment.Select(Function(item) New PaymentType With
                                                                            {
                                                                                .ID = New IDType With {.schemeID = item.ConceptCollection},
                                                                                .PaidAmount = New PaidAmountType With {.Value = item.PatientValue},
                                                                                .ReceivedDate = New ReceivedDateType With {.Value = CType(item.DocumentDate, DateTime).ToString("yyyy-MM-dd")},
                                                                                .InstructionID = New InstructionIDType With {.Value = item.Description}
                                                                            }).ToList()

                Return listPaymentType
            End If

            If Me._paymentMethods Is Nothing OrElse Not Me._paymentMethods.Any() OrElse {3, 6}.Contains(Me._invoice.DocumentType) Then
                Return Nothing
            End If

            listPaymentType = Me._paymentMethods.Select(Function(detail)
                                                            Return New PaymentType With
                                                                        {
                                                                            .ID = New IDType With {.schemeID = Nothing, .Value = detail.Code},
                                                                            .PaidAmount = New PaidAmountType With {.Value = detail.Value},
                                                                            .ReceivedDate = New ReceivedDateType With {.Value = CType(detail.DocumentDate, DateTime).ToString("yyyy-MM-dd")},
                                                                            .InstructionID = Nothing
                                                                        }
                                                        End Function).ToList()

            Return listPaymentType
        End Function

        Private Function GetTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'IVA - incluye Gravado y Exento, excluye Excluido
            Dim ivaDetails = Me._invoice.InvoiceDetails.Where(Function(d) d.IVAPercentage > 0 OrElse
               (d.TaxClassificationType.HasValue AndAlso d.TaxClassificationType.Value = 3))

            If ivaDetails.Any() Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0

                For Each tax In ivaDetails.GroupBy(Function(d) d.IVAPercentage)
                    Dim TaxableAmount = ivaDetails.Where(Function(d) d.IVAPercentage = tax.Key).Sum(Function(d) GetLineExtensionAmountValue(d))
                    Dim groupTaxAmount = ivaDetails.Where(Function(d) d.IVAPercentage = tax.Key).Sum(Function(d) d.IVAValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = groupTaxAmount},
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

                TaxAmount = ivaDetails.Sum(Function(d) d.IVAValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            'ICA
            If Me._invoice.InvoiceDetails.Any(Function(d) d.ICAPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In Me._invoice.InvoiceDetails.Where(Function(d) d.ICAPercentage > 0).GroupBy(Function(d) d.ICAPercentage)
                    TaxableAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.ICAPercentage = tax.Key).Sum(Function(d) GetLineExtensionAmountValue(d))
                    TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.ICAPercentage = tax.Key).Sum(Function(d) d.ICAValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "03"},
                                .Name = New NameType1 With {.Value = "ICA"}
                            }
                        }
                    })
                Next

                TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.ICAPercentage > 0).Sum(Function(d) d.ICAValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetWithholdingTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'ReteIVA
            If Me._invoice.InvoiceDetails.Any(Function(d) d.WithholdingIVAPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingIVAPercentage > 0).GroupBy(Function(d) d.WithholdingIVAPercentage)
                    TaxableAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingIVAPercentage = tax.Key).Sum(Function(d) GetWithholdingIVATaxableAmount(d))
                    TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingIVAPercentage = tax.Key).Sum(Function(d) d.WithholdingIVAValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "05"},
                                .Name = New NameType1 With {.Value = "ReteIVA"}
                            }
                        }
                    })
                Next

                TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingIVAPercentage > 0).Sum(Function(d) d.WithholdingIVAValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            'ReteFuente
            If Me._invoice.InvoiceDetails.Any(Function(d) d.WithholdingPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingPercentage > 0).GroupBy(Function(d) d.WithholdingPercentage)
                    TaxableAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingPercentage = tax.Key).Sum(Function(d) GetLineExtensionAmountValue(d))
                    TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingPercentage = tax.Key).Sum(Function(d) d.WithholdingValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "06"},
                                .Name = New NameType1 With {.Value = "ReteFuente"}
                            }
                        }
                    })
                Next

                TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingPercentage > 0).Sum(Function(d) d.WithholdingValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            'ReteICA
            If Me._invoice.InvoiceDetails.Any(Function(d) d.WithholdingICAPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingICAPercentage > 0).GroupBy(Function(d) d.WithholdingICAPercentage)
                    TaxableAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingICAPercentage = tax.Key).Sum(Function(d) GetLineExtensionAmountValue(d))
                    TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingICAPercentage = tax.Key).Sum(Function(d) d.WithholdingICAValue)
                    listTaxSubtotal.Add(New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = TaxableAmount},
                        .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = tax.Key},
                            .TaxScheme = New TaxSchemeType() With
                            {
                                .ID = New IDType With {.Value = "07"},
                                .Name = New NameType1 With {.Value = "ReteICA"}
                            }
                        }
                    })
                Next

                TaxAmount = Me._invoice.InvoiceDetails.Where(Function(d) d.WithholdingICAPercentage > 0).Sum(Function(d) d.WithholdingICAValue)
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                    .TaxSubtotal = listTaxSubtotal
                })
            End If

            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetLegalMonetaryTotalInformation() As MonetaryTotalType
            Dim LineExtensionAmount = Me._invoice.InvoiceDetails.Sum(Function(d) GetLineExtensionAmountValue(d))
            Dim TaxExclusiveAmount = Me._invoice.InvoiceDetails.Where(Function(x) x.TaxClassificationType <> 2).Sum(Function(d) GetLineExtensionAmountValue(d))
            Dim TaxInclusiveAmount = Me._invoice.InvoiceDetails.Sum(Function(d) GetLineExtensionAmountValue(d) + d.IVAValue)
            Dim ThirdPartyPortfolioAdvanceAmount = Me._invoice.InvoicePrepaidPayment.Where(Function(y) y.ConceptCollection = "04").Sum(Function(d) d.PatientValue)
            Dim PrepaidAmount As Decimal = 0
            Dim PayableAmount As Decimal = 0
            Dim DiscountAmount As Decimal = Me._invoice.InvoiceDetails.Sum(Function(d) d.DiscountValue)

            If Me._invoice.InvoicePrepaidPayment IsNot Nothing AndAlso Me._invoice.InvoicePrepaidPayment.Any() Then
                PrepaidAmount = Me._invoice.InvoicePrepaidPayment.Sum(Function(d) d.PatientValue)
            ElseIf Me._invoice.DocumentType = 4 Then
                PrepaidAmount = Me._invoice.InvoicePrepaidPayment.Where(Function(y) {"01", "02", "03"}.Contains(y.ConceptCollection)).Sum(Function(d) d.PatientValue)
            ElseIf ThirdPartyPortfolioAdvanceAmount > 0 Then
                PrepaidAmount = ThirdPartyPortfolioAdvanceAmount
            ElseIf Me._paymentMethods IsNot Nothing AndAlso Me._paymentMethods.Any() AndAlso Not {3, 6, 7, 4}.Contains(Me._invoice.DocumentType) Then
                PrepaidAmount = Me._paymentMethods.Sum(Function(d) d.Value)
            End If

            If Me._invoice.DocumentType = 4 Then
                PayableAmount = TaxInclusiveAmount - PrepaidAmount - DiscountAmount
            Else
                PayableAmount = TaxInclusiveAmount - PrepaidAmount
            End If

            Return New MonetaryTotalType With
            {
                .LineExtensionAmount = New LineExtensionAmountType With {.Value = LineExtensionAmount},
                .TaxExclusiveAmount = New TaxExclusiveAmountType With {.Value = TaxExclusiveAmount},
                .TaxInclusiveAmount = New TaxInclusiveAmountType With {.Value = TaxInclusiveAmount},
                .PrepaidAmount = New PrepaidAmountType With {.Value = PrepaidAmount},
                .PayableAmount = New PayableAmountType With {.Value = PayableAmount}
            }
        End Function

#Region "Details"

        Private Function GetInvoiceLineInformation() As List(Of InvoiceLineType)
            Dim listInvoiceLineType As New List(Of InvoiceLineType)

            Dim i = 0
            For Each invoiceDetail In Me._invoice.InvoiceDetails
                i = i + 1
                Dim invoiceLineType = New InvoiceLineType() With
                {
                    .ID = New IDType With {.Value = i, .schemeID = If(_customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.MandatesGoods) AndAlso Me._invoice?.InvoiceCopayCustom?.ThirdPartyEAPB IsNot Nothing, 1, Nothing)},
                    .Note = New List(Of NoteType),
                    .InvoicedQuantity = New InvoicedQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = invoiceDetail.InvoiceQuantity
                    },
                    .LineExtensionAmount = New LineExtensionAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)}
                }

                invoiceLineType.AllowanceCharge = Me.GetInvoiceLineAllowanceChargeInformation(invoiceDetail)
                invoiceLineType.TaxTotal = Me.GetInvoiceLineTaxTotalInformation(invoiceDetail)
                invoiceLineType.WithholdingTaxTotal = Me.GetInvoiceLineWithholdingTaxTotalInformation(invoiceDetail)

                'Datos del producto o servicio
                invoiceLineType.Item = New ItemType With
                {
                    .Description = New List(Of DescriptionType) From
                    { 'Todos los nombres que puede tener este producto
                        New DescriptionType(invoiceDetail.Name),
                        IIf(String.IsNullOrEmpty(invoiceDetail.NameAlternative), Nothing, IIf(invoiceDetail.Name = invoiceDetail.NameAlternative, Nothing, New DescriptionType(invoiceDetail.NameAlternative))),
                        IIf(String.IsNullOrEmpty(invoiceDetail.NameAlternativeTwo), Nothing, IIf(invoiceDetail.Name = invoiceDetail.NameAlternativeTwo OrElse invoiceDetail.NameAlternative = invoiceDetail.NameAlternativeTwo, Nothing, New DescriptionType(invoiceDetail.NameAlternativeTwo)))
                    },
                    .SellersItemIdentification = IIf(String.IsNullOrEmpty(invoiceDetail.Code), Nothing, New ItemIdentificationType With {.ID = New IDType With {.Value = invoiceDetail.Code}}),
                    .StandardItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = invoiceDetail.Code, .schemeID = "999"}},
                    .AdditionalItemIdentification = IIf(invoiceDetail.Code = invoiceDetail.CodeAlternative AndAlso invoiceDetail.Code = invoiceDetail.CodeAlternativeTwo, Nothing, New List(Of ItemIdentificationType) From
                    { 'codigos alternativos
                        IIf(String.IsNullOrEmpty(invoiceDetail.CodeAlternative), Nothing, IIf(invoiceDetail.Code = invoiceDetail.CodeAlternative, Nothing, New ItemIdentificationType With {.ID = New IDType With {.Value = invoiceDetail.CodeAlternative}})),
                        IIf(String.IsNullOrEmpty(invoiceDetail.CodeAlternativeTwo), Nothing, IIf(invoiceDetail.Code = invoiceDetail.CodeAlternativeTwo OrElse invoiceDetail.CodeAlternative = invoiceDetail.CodeAlternativeTwo, Nothing, New ItemIdentificationType With {.ID = New IDType With {.Value = invoiceDetail.CodeAlternativeTwo}}))
                    }),
                    .InformationContentProviderParty = GetInformationContentProviderPartyToMandateServices(Me._invoice?.InvoiceCopayCustom?.ThirdPartyEAPB)
                }

                invoiceLineType.Price = New PriceType With
                {
                    .PriceAmount = New PriceAmountType With {.Value = invoiceDetail.Price},
                    .BaseQuantity = New BaseQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = invoiceDetail.InvoiceQuantity
                    }
                }

                If GetLineExtensionAmountValue(invoiceDetail) = 0 Then
                    invoiceLineType.PricingReference = New PricingReferenceType With
                    {
                        .AlternativeConditionPrice = New List(Of PriceType) From
                        {
                            New PriceType With
                            {
                                .PriceAmount = New PriceAmountType With {.Value = invoiceDetail.Price},
                                .PriceTypeCode = New PriceTypeCodeType With {.Value = "01"}
                            }
                        }
                    }
                End If

                listInvoiceLineType.Add(invoiceLineType)
            Next

            Return listInvoiceLineType
        End Function

        Private Function GetInvoiceLineAllowanceChargeInformation(invoiceDetail As SP_GetInvoiceDetailsByInvoiceId_Result) As List(Of AllowanceChargeType)
            Dim listAllowanceChargeType As New List(Of AllowanceChargeType)

            If invoiceDetail.DiscountPercentage > 0 Then
                listAllowanceChargeType.Add(New AllowanceChargeType With
                {
                    .ChargeIndicator = New ChargeIndicatorType With {.Value = "false"},
                    .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = invoiceDetail.DiscountPercentage},
                    .Amount = New AmountType2 With {.Value = invoiceDetail.DiscountValue},
                    .BaseAmount = New BaseAmountType With {.Value = invoiceDetail.Value},
                    .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Descuento Entidad"}}
                })
            ElseIf invoiceDetail.DiscountPercentage < 0 Then
                listAllowanceChargeType.Add(New AllowanceChargeType With
                {
                    .ChargeIndicator = New ChargeIndicatorType With {.Value = "true"},
                    .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = (invoiceDetail.DiscountPercentage * -1)},
                    .Amount = New AmountType2 With {.Value = (invoiceDetail.DiscountValue * -1)},
                    .BaseAmount = New BaseAmountType With {.Value = invoiceDetail.Value},
                    .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Cargo Entidad"}}
                })
            End If

            If invoiceDetail.DistributedPercentage > 0 Then
                listAllowanceChargeType.Add(New AllowanceChargeType With
                {
                    .ChargeIndicator = New ChargeIndicatorType With {.Value = "false"},
                    .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = invoiceDetail.DistributedPercentage},
                    .Amount = New AmountType2 With {.Value = invoiceDetail.DistributedValue},
                    .BaseAmount = New BaseAmountType With {.Value = invoiceDetail.BaseDistributeValue},
                    .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Valor Distribuido"}}
                })
            End If

            Return IIf(listAllowanceChargeType.Any(), listAllowanceChargeType, Nothing)
        End Function

        Private Function GetInvoiceLineTaxTotalInformation(invoiceDetail As SP_GetInvoiceDetailsByInvoiceId_Result) As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)
            'IVA
            If invoiceDetail.IVAPercentage > 0 Then
                ' Gravado: comportamiento existente sin cambios
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.IVAValue},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)},
                            .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.IVAValue},
                            .TaxCategory = New TaxCategoryType() With
                            {
                                .Percent = New PercentType1 With {.Value = invoiceDetail.IVAPercentage},
                                .TaxScheme = New TaxSchemeType() With
                                {
                                    .ID = New IDType With {.Value = "01"},
                                    .Name = New NameType1 With {.Value = "IVA"}
                                }
                            }
                        }
                    }
                })
            ElseIf invoiceDetail.TaxClassificationType.HasValue AndAlso invoiceDetail.TaxClassificationType.Value = 3 Then
                ' Exento: TaxTotal con Percent=0 y TaxAmount=0
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = 0},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)},
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
                        }
                    }
                })
            End If
            ' Excluido (TaxClassificationType=2): no genera TaxTotal — nada que agregar

            'ICA
            If invoiceDetail.ICAPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.ICAValue},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)},
                            .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.ICAValue},
                            .TaxCategory = New TaxCategoryType() With
                            {
                                .Percent = New PercentType1 With {.Value = invoiceDetail.ICAPercentage},
                                .TaxScheme = New TaxSchemeType() With
                                {
                                    .ID = New IDType With {.Value = "03"},
                                    .Name = New NameType1 With {.Value = "ICA"}
                                }
                            }
                        }
                    }
                })
            End If

            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetInvoiceLineWithholdingTaxTotalInformation(invoiceDetail As SP_GetInvoiceDetailsByInvoiceId_Result) As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'ReteIVA
            If invoiceDetail.WithholdingIVAPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingIVAValue},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetWithholdingIVATaxableAmount(invoiceDetail)},
                            .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingIVAValue},
                            .TaxCategory = New TaxCategoryType() With
                            {
                                .Percent = New PercentType1 With {.Value = invoiceDetail.WithholdingIVAPercentage},
                                .TaxScheme = New TaxSchemeType() With
                                {
                                    .ID = New IDType With {.Value = "05"},
                                    .Name = New NameType1 With {.Value = "ReteIVA"}
                                }
                            }
                        }
                    }
                })
            End If

            'ReteFuente
            If invoiceDetail.WithholdingPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingValue},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)},
                            .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingValue},
                            .TaxCategory = New TaxCategoryType() With
                            {
                                .Percent = New PercentType1 With {.Value = invoiceDetail.WithholdingPercentage},
                                .TaxScheme = New TaxSchemeType() With
                                {
                                    .ID = New IDType With {.Value = "06"},
                                    .Name = New NameType1 With {.Value = "ReteFuente"}
                                }
                            }
                        }
                    }
                })
            End If

            'ReteICA
            If invoiceDetail.WithholdingICAPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
                {
                    .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingICAValue},
                    .TaxSubtotal = New List(Of TaxSubtotalType) From
                    {
                        New TaxSubtotalType With
                        {
                            .TaxableAmount = New TaxableAmountType With {.Value = GetLineExtensionAmountValue(invoiceDetail)},
                            .TaxAmount = New TaxAmountType With {.Value = invoiceDetail.WithholdingICAValue},
                            .TaxCategory = New TaxCategoryType() With
                            {
                                .Percent = New PercentType1 With {.Value = invoiceDetail.WithholdingICAPercentage},
                                .TaxScheme = New TaxSchemeType() With
                                {
                                    .ID = New IDType With {.Value = "07"},
                                    .Name = New NameType1 With {.Value = "ReteICA"}
                                }
                            }
                        }
                    }
                })
            End If

            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        ''' <summary>
        ''' funcion que se encarga de armar la estructura para relacionar al mandante 
        ''' (para este caso la EAPB) la cual es la responsable de la intrada del dinero a factura 
        ''' en la fact COPAGO o cuota Moderadora
        ''' el thirdPary DEBE incluir informacion en la entidad PERSON
        ''' </summary>
        ''' <returns></returns>
        Private Function GetInformationContentProviderPartyToMandateServices(thirdParty As ThirdParty) As PartyType

            If thirdParty Is Nothing Then
                Return Nothing
            End If

            Dim partyType = New PartyType

            Dim partyIdentification = New PartyIdentificationType With {
                                                .ID = New IDType With
                                                    {
                                                        .schemeAgencyID = "195",
                                                        .schemeID = thirdParty.DigitVerification,
                                                        .schemeName = thirdParty.Person.getAcquirerType(),
                                                        .Value = thirdParty.Nit
                                                    }
                                            }

            Dim AgentParty = New PartyType With {.PartyIdentification = New List(Of PartyIdentificationType) From {partyIdentification}}
            Dim powerOfAttorneyType = New PowerOfAttorneyType With {.AgentParty = AgentParty}

            With partyType
                .PowerOfAttorney = New List(Of PowerOfAttorneyType) From {powerOfAttorneyType}
            End With
            Return partyType
        End Function
#End Region

#End Region

    End Class

End Namespace
