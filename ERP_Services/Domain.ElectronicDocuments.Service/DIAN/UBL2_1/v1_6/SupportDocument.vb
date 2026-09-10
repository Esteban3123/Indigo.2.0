Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root

Namespace DIAN.UBL2_1.v1_6
    Public Class SupportDocument

#Region "Variables"

        ReadOnly _ublVersionId As New UBLVersionIDType With {.Value = "UBL 2.1"}
        ReadOnly _customizationID As New CustomizationIDType
        ReadOnly _profileId As New ProfileIDType With {.Value = "DIAN 2.1: documento soporte en adquisiciones efectuadas a no obligados a facturar."}
        ReadOnly _dianInformation = New With {.DocumentType = "31", .Nit = "800197268", .DigitVerification = "4"}

#End Region

#Region "Properties"

        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _billingAuthorization As BillingAuthorization
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _supportDocument As ElectronicSupportDocument
        ReadOnly _paymentsMethods As List(Of SP_GetPaymentMethodByDocumentSupportId_Result)

#End Region

#Region "Builder"
        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings, ByVal supplierThirdParty As ThirdParty, ByVal customerThirdParty As ThirdParty, ByVal supportDocument As ElectronicSupportDocument,
                   ByVal billingAuthorization As BillingAuthorization, ByVal paymentsMethods As List(Of SP_GetPaymentMethodByDocumentSupportId_Result))
            Me._settingsAccount = settingsAccount
            Me._billingAuthorization = billingAuthorization
            Me._supplierThirdParty = supplierThirdParty
            Me._customerThirdParty = customerThirdParty
            Me._supportDocument = supportDocument
            Me._paymentsMethods = paymentsMethods

            If supplierThirdParty.Class Is Nothing Or supplierThirdParty.Class = 1 Then
                Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.Standard)
            Else
                Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.MandatesGoods)
            End If

            '-- Opcional(pendiente), por ubicación del tercero
            'Dim address = supplierThirdParty.Person.Address.FirstOrDefault(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            'If Address.CountryStandardCode = "CO" Then
            '    Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.Standard)
            'Else
            '    Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.MandatesGoods)
            'End I
        End Sub
#End Region

#Region "Methods"
        Public Function Populate() As InvoiceType
            Dim documentType As New InvoiceType
            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.UBLVersionID = Me._ublVersionId
            documentType.CustomizationID = Me._customizationID.Value
            documentType.ProfileID = Me._profileId
            documentType.ProfileExecutionID = New ProfileExecutionIDType With {.Value = Me._supportDocument.Environment}
            documentType.ID = New IDType With {.Value = Me._supportDocument.DocumentNumber}
            documentType.UUID = New UUIDType With
        {
            .schemeID = Me._supportDocument.Environment,
            .schemeName = "CUDS-SHA384",
            .Value = Me._supportDocument.CUDS
        }
            documentType.IssueDate = New IssueDateType With {.Value = Me._supportDocument.FecDS}
            documentType.IssueTime = New IssueTimeType With {.Value = Me._supportDocument.HorDS}
            documentType.DueDate = New DueDateType With {.Value = Me._supportDocument.dueDatePayment}
            documentType.InvoiceTypeCode = New InvoiceTypeCodeType With {.Value = Utils.GetXmlEnumToString(Of DocumentType)(Me._supportDocument.getInvoiceTypeCode())}
            documentType.Note = New List(Of NoteType) From {New NoteType With {.Value = Me._supportDocument.Description}}
            documentType.DocumentCurrencyCode = New DocumentCurrencyCodeType With {.Value = Utils.GetXmlEnumToString(Of CurrencyCode)(CurrencyCode.COP)}
            documentType.LineCountNumeric = New LineCountNumericType With {.Value = Me._supportDocument.documentSupportDetails.Count}
            documentType.OrderReference = Nothing ' Pendiente
            documentType.BillingReference = Nothing ' Pendiente
            documentType.AccountingSupplierParty = GetAccountingSupplierPartyInformation()
            documentType.AccountingCustomerParty = GetAccountingCustomerPartyInformation()
            documentType.PaymentMeans = GetPaymentsMeansInformation()
            documentType.AllowanceCharge = GetAllowanceChargeInformation()
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
            listUBLExtensionType.Add(GenerateDianExtensions())
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

#Region "UBLExtensions"

        Private Function GenerateDianExtensions() As UBLExtensionType
            Dim dianExtensions As New DianExtensionsType() With
        {
            .InvoiceControl = GenerateInvoiceControlInformation(),
            .InvoiceSource = GenerateInvoiceSourceInformation(),
            .SoftwareProvider = GenerateSoftwareProviderInformation(),
            .SoftwareSecurityCode = GenerateSoftwareSecurityCodeInformation(),
            .AuthorizationProvider = GenerateAuthorizationProviderInformation(),
            .QRCode = Me._supportDocument.GetQRCode
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
                .schemeID = Me._customerThirdParty.DigitVerification,
                .schemeName = Me._customerThirdParty.Person.getAcquirerTypeSupportDocument(),
                .Value = Me._customerThirdParty.Person.IdentificationNumber
            },
            .SoftwareID = New IdentifierType1() With
            {
                .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
                .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
                .Value = Me._settingsAccount.SupportDocumentIdentifier
            }
        }
        End Function

        Private Function GenerateSoftwareSecurityCodeInformation() As IdentifierType1
            Return New IdentifierType1() With
        {
            .schemeAgencyID = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyID)(coID2TypeSchemeAgencyID.Item195),
            .schemeAgencyName = Utils.GetXmlEnumToString(Of coID2TypeSchemeAgencyName)(coID2TypeSchemeAgencyName.CODIANDireccióndeImpuestosyAduanasNacionales),
            .Value = Utils.Sha384Encode(String.Concat(Me._settingsAccount.SupportDocumentIdentifier, Me._settingsAccount.SupportDocumentPin, Me._supportDocument.DocumentNumber))
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

#Region "ThirdPartys"
        Private Function GetAccountingSupplierPartyInformation() As SupplierPartyType
            Return New SupplierPartyType With
            {
                .AdditionalAccountID = New List(Of AdditionalAccountIDType) From {New AdditionalAccountIDType With {.Value = Me._supplierThirdParty.getAdditionalAccountID()}},
                .Party = Me.GetPartyTypeInformation(Me._supplierThirdParty, 1)
            }
        End Function

        Private Function GetAccountingCustomerPartyInformation() As CustomerPartyType
            Return New CustomerPartyType With
            {
                .AdditionalAccountID = New List(Of AdditionalAccountIDType) From {New AdditionalAccountIDType With {.Value = Me._customerThirdParty.getAdditionalAccountID()}},
                .Party = Me.GetPartyTypeInformation(Me._customerThirdParty, 2)
            }
        End Function

        Private Function GetPartyTypeInformation(ByVal thirdParty As Domain.Entities.ThirdParty, person As Integer) As PartyType
            Dim party As New PartyType

            If thirdParty.getAdditionalAccountID() = "2" Then
                party.PartyIdentification = New List(Of PartyIdentificationType) From
            {
                New PartyIdentificationType With
                {
                    .ID = New IDType With
                    {
                        .schemeID = thirdParty.GetDigitVerification(),
                        .schemeName = thirdParty.Person.getAcquirerTypeSupportDocument(),
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
                .PostalZone = New PostalZoneType With {.Value = IIf(person = 1, "660003", "110911")},
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
                    .schemeID = thirdParty.GetDigitVerification(),
                    .schemeName = thirdParty.Person.getAcquirerTypeSupportDocument(),
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
                    .schemeName = thirdParty.Person.getAcquirerTypeSupportDocument(),
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
        ''' <summary>
        ''' Obtener el metodo de pago: Si el metodo de pago es crédito, se debe informar los campos PaymentDueDate
        ''' </summary>
        ''' <returns></returns>
        Private Function GetPaymentsMeansInformation() As List(Of PaymentMeansType)
            Dim listpaymentsMeans As New List(Of PaymentMeansType)
            For Each detail In _paymentsMethods
                listpaymentsMeans.Add(New PaymentMeansType With
            {
                .ID = New IDType With {.Value = detail.PaymentMethodId},
                .PaymentMeansCode = New PaymentMeansCodeType With {.Value = detail.PaymentMethodCode},
                .PaymentDueDate = New PaymentDueDateType With {.Value = detail.PaymentDueDate},
                .PaymentID = New List(Of PaymentIDType) From {New PaymentIDType With {.Value = "Texto libre para informar datos adicionales sobre el medio de pago"}}
            })
            Next
            Return listpaymentsMeans
        End Function

        Private Function GetAllowanceChargeInformation() As List(Of AllowanceChargeType)
            Dim listAllowanceCharge As New List(Of AllowanceChargeType)
            Dim i = 0
            For Each detail In Me._supportDocument.documentSupportDetails
                i = i + 1
                If detail.SurchargeValue > 0 Then
                    listAllowanceCharge.Add(New AllowanceChargeType With
                {
                    .ID = New IDType With {.Value = i},
                    .ChargeIndicator = New ChargeIndicatorType With {.Value = True},
                    .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Cargo al documento"}},
                    .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = detail.SurchargePercentage},
                    .Amount = New AmountType2 With {.Value = detail.SurchargeValue},
                    .BaseAmount = New BaseAmountType With {.Value = Me._supportDocument.TotalValue}
                })
                ElseIf detail.DiscountValue > 0 Then
                    listAllowanceCharge.Add(New AllowanceChargeType With
                {
                    .ID = New IDType With {.Value = i},
                    .ChargeIndicator = New ChargeIndicatorType With {.Value = False},
                    .AllowanceChargeReasonCode = New AllowanceChargeReasonCodeType With {.Value = "00"},
                    .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Descuento al documento"}},
                    .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = detail.DiscountPercentage},
                    .Amount = New AmountType2 With {.Value = detail.DiscountValue},
                    .BaseAmount = New BaseAmountType With {.Value = Me._supportDocument.TotalValue}
                })
                End If
            Next
            Return IIf(listAllowanceCharge.Any(), listAllowanceCharge, Nothing)
        End Function

        Private Function GetTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotal As New List(Of TaxTotalType)

            'Iva
            If Me._supportDocument.documentSupportDetails.Any(Function(d) d.IVAPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0
                For Each tax In Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage > 0).GroupBy(Function(d) d.IVAPercentage)
                    TaxableAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage = tax.Key).Sum(Function(d) d.BaseValue)
                    TaxAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage = tax.Key).Sum(Function(d) d.IVAValue)
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
                TaxAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage > 0).Sum(Function(d) d.IVAValue)
                listTaxTotal.Add(New TaxTotalType With
            {
                .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                .TaxSubtotal = listTaxSubtotal
            })
            End If
            Return IIf(listTaxTotal.Any(), listTaxTotal, Nothing)
        End Function

        Private Function GetWithholdingTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'ReteIVA
            If Me._supportDocument.documentSupportDetails.Any(Function(d) d.WithholdingIVAPercentage > 0) Then
                Dim listTaxSubtotal As New List(Of TaxSubtotalType)
                Dim TaxAmount As Decimal = 0
                Dim TaxableAmount As Decimal = 0

                For Each tax In Me._supportDocument.documentSupportDetails.Where(Function(d) d.WithholdingIVAPercentage > 0).GroupBy(Function(d) d.WithholdingIVAPercentage)
                    TaxableAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.WithholdingIVAPercentage = tax.Key).Sum(Function(d) d.BaseValue)
                    TaxAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.WithholdingIVAPercentage = tax.Key).Sum(Function(d) d.WithholdingIVAValue)
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

                TaxAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.WithholdingIVAPercentage > 0).Sum(Function(d) d.WithholdingIVAValue)
                listTaxTotalTypes.Add(New TaxTotalType With
            {
                .TaxAmount = New TaxAmountType With {.Value = TaxAmount},
                .TaxSubtotal = listTaxSubtotal
            })
            End If
            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetLegalMonetaryTotalInformation() As MonetaryTotalType
            Dim LineExtensionAmount = Me._supportDocument.documentSupportDetails.Sum(Function(d) d.DebitValue) 'Valor bruto antes de tributos
            Dim TaxExclusiveAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage > 0).Sum(Function(d) d.BaseValue)
            Dim TaxInclusiveAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.IVAPercentage > 0).Sum(Function(d) d.IVAValue) + LineExtensionAmount
            Dim AllowanceTotalAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.DiscountPercentage > 0).Sum(Function(d) d.DiscountValue)
            Dim ChargeTotalAmount = Me._supportDocument.documentSupportDetails.Where(Function(d) d.SurchargePercentage > 0).Sum(Function(d) d.SurchargeValue)
            Dim PayableAmount = (LineExtensionAmount + ChargeTotalAmount) - AllowanceTotalAmount
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

#Region "Document Details"
        Private Function GetInvoiceLineInformation() As List(Of InvoiceLineType)
            Dim listInvoiceLineType As New List(Of InvoiceLineType)
            Dim i = 0
            For Each details In Me._supportDocument.documentSupportDetails
                Dim descriptionItem As String = details.NameItem
                'Recorto la cadenca siempre y cuando supere la longitud establecida
                If descriptionItem.Length > 300 Then descriptionItem = descriptionItem.Substring(0, 300)

                i = i + 1
                Dim invoiceLineType As New InvoiceLineType With
                {
                    .ID = New IDType With {.Value = i},
                    .InvoicedQuantity = New InvoicedQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = details.InvoiceQuantity
                    },
                    .LineExtensionAmount = New LineExtensionAmountType With {.Value = details.DebitValue}
                }

                invoiceLineType.InvoicePeriod = New List(Of PeriodType) From {New PeriodType With
                {
                    .StartDate = New StartDateType With {.Value = Me._supportDocument.DocumentDate},
                    .DescriptionCode = New List(Of DescriptionCodeType) From {New DescriptionCodeType With {.Value = Utils.GetXmlEnumToString(Of GenetationForm)(GenetationForm.ByOperation)}},
                    .Description = New List(Of DescriptionType) From {New DescriptionType("Por operación")}
                }}
                invoiceLineType.AllowanceCharge = Me.GetInvoiceLineAllowanceChargeInformation(details)
                invoiceLineType.TaxTotal = Me.GetInvoiceLineTaxTotalInformation(details)
                invoiceLineType.WithholdingTaxTotal = Me.GetInvoiceLineWithholdingTaxTotalInformation(details)
                'Datos del artículo o servicio 
                invoiceLineType.Item = New ItemType With
                {
                    .Description = New List(Of DescriptionType) From
                    {
                        IIf(String.IsNullOrEmpty(descriptionItem), Nothing, New DescriptionType(descriptionItem))
                    },
                    .PackSizeNumeric = New PackSizeNumericType With {.Value = details.InvoiceQuantity},
                    .SellersItemIdentification = IIf(String.IsNullOrEmpty(details.CodeItem), Nothing, New ItemIdentificationType With {.ID = New IDType With {.Value = details.NameItem}}),
                    .StandardItemIdentification = IIf(String.IsNullOrEmpty(details.CodeItem), Nothing, New ItemIdentificationType With
                    {
                        .ID = New IDType With {.Value = details.CodeItem, .schemeID = "999", .schemeName = "Estándar de adopción del contribuyente"}
                    })
                }
                invoiceLineType.Price = New PriceType With
                {
                    .PriceAmount = New PriceAmountType With {.Value = details.DebitValue},
                    .BaseQuantity = New BaseQuantityType With
                    {
                        .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                        .Value = details.InvoiceQuantity
                     }
                }
                listInvoiceLineType.Add(invoiceLineType)
            Next
            Return listInvoiceLineType
        End Function

        Private Function GetInvoiceLineAllowanceChargeInformation(detail As SP_GetDocumentSupportDetailsById_Result) As List(Of AllowanceChargeType)
            Dim listAllowanceChargeType As New List(Of AllowanceChargeType)
            If detail.SurchargeValue > 0 Then
                listAllowanceChargeType.Add(New AllowanceChargeType With
            {
                .ChargeIndicator = New ChargeIndicatorType With {.Value = True},
                .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Cargo al documento"}},
                .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = detail.SurchargePercentage},
                .Amount = New AmountType2 With {.Value = detail.SurchargeValue},
                .BaseAmount = New BaseAmountType With {.Value = Me._supportDocument.TotalValue}
            })
            ElseIf detail.DiscountValue > 0 Then
                listAllowanceChargeType.Add(New AllowanceChargeType With
            {
                .ChargeIndicator = New ChargeIndicatorType With {.Value = False},
                .AllowanceChargeReasonCode = New AllowanceChargeReasonCodeType With {.Value = "00"},
                .AllowanceChargeReason = New List(Of AllowanceChargeReasonType) From {New AllowanceChargeReasonType With {.Value = "Descuento al documento"}},
                .MultiplierFactorNumeric = New MultiplierFactorNumericType With {.Value = detail.DiscountPercentage},
                .Amount = New AmountType2 With {.Value = detail.DiscountValue},
                .BaseAmount = New BaseAmountType With {.Value = Me._supportDocument.TotalValue}
            })
            End If
            Return IIf(listAllowanceChargeType.Any(), listAllowanceChargeType, Nothing)
        End Function

        Private Function GetInvoiceLineTaxTotalInformation(detail As SP_GetDocumentSupportDetailsById_Result) As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)

            'IVA
            If detail.IVAPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
            {
                .TaxAmount = New TaxAmountType With {.Value = detail.IVAValue},
                .TaxSubtotal = New List(Of TaxSubtotalType) From
                {
                     New TaxSubtotalType With
                     {
                        .TaxableAmount = New TaxableAmountType With {.Value = detail.BaseValue},
                        .TaxAmount = New TaxAmountType With {.Value = detail.IVAValue},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = detail.IVAPercentage},
                            .TaxScheme = New TaxSchemeType With
                            {
                                .ID = New IDType With {.Value = "01"},
                                .Name = New NameType1 With {.Value = "IVA"}
                            }
                        }
                     }
                }
            })
            End If
            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetInvoiceLineWithholdingTaxTotalInformation(detail As SP_GetDocumentSupportDetailsById_Result) As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)
            'ReteIVA 
            If detail.WithholdingIVAPercentage > 0 Then
                listTaxTotalTypes.Add(New TaxTotalType With
            {
                .TaxAmount = New TaxAmountType With {.Value = detail.WithholdingIVAValue},
                .TaxSubtotal = New List(Of TaxSubtotalType) From
                {
                    New TaxSubtotalType With
                    {
                        .TaxableAmount = New TaxableAmountType With {.Value = detail.BaseValue},
                        .TaxAmount = New TaxAmountType With {.Value = detail.WithholdingIVAValue},
                        .TaxCategory = New TaxCategoryType() With
                        {
                            .Percent = New PercentType1 With {.Value = detail.WithholdingIVAPercentage},
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
            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

#End Region
#End Region
    End Class
End Namespace