Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root

Namespace DIAN.UBL2_1.v1_6
    Public Class SupportDocumentAdjustmentNote


#Region "Variables"

        ReadOnly _ublVersionId As New UBLVersionIDType With {.Value = "UBL 2.1"}
        ReadOnly _customizationID As New CustomizationIDType
        ReadOnly _profileId As New ProfileIDType With {.Value = "DIAN 2.1: Nota de ajuste al documento soporte en adquisiciones efectuadas a sujetos no obligados a expedir factura o documento equivalente"}
        ReadOnly _dianInformation = New With {.DocumentType = "31", .Nit = "800197268", .DigitVerification = "4"}

#End Region

#Region "Properties"
        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _billingAuthorization As BillingAuthorization
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _customerThirdParty As ThirdParty
        ReadOnly _supportDocumentAdjusmenNote As ElectronicSupportDocumentAdjustmentNote
#End Region

#Region "Builder"
        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings, ByVal supplierThirdParty As ThirdParty, ByVal customerThirdParty As ThirdParty, ByVal supportDocumentAdjusmenNote As ElectronicSupportDocumentAdjustmentNote,
                   ByVal billingAuthorization As BillingAuthorization)
            Me._settingsAccount = settingsAccount
            Me._billingAuthorization = billingAuthorization
            Me._supplierThirdParty = supplierThirdParty
            Me._customerThirdParty = customerThirdParty
            Me._supportDocumentAdjusmenNote = supportDocumentAdjusmenNote

            If supplierThirdParty.Person.getAcquirerTypeSupportDocument.Contains("31") Then
                Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.Standard)
            Else
                Me._customizationID.Value = Utils.GetXmlEnumToString(Of OperationType)(OperationType.MandatesGoods)
            End If
        End Sub
#End Region

#Region "Methods"
        Public Function Populate() As CreditNoteType
            Dim documentType As New CreditNoteType
            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.UBLVersionID = Me._ublVersionId
            documentType.CustomizationID = Me._customizationID
            documentType.ProfileID = Me._profileId
            documentType.ProfileExecutionID = New ProfileExecutionIDType With {.Value = Me._supportDocumentAdjusmenNote.ElectronicSupportDocument.Environment}
            documentType.ID = New IDType With {.Value = Me._supportDocumentAdjusmenNote.Code}
            documentType.UUID = New UUIDType With
            {
                .schemeID = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.Environment, Environment.Production, Environment.Tests)),
                .schemeName = "CUDS-SHA384",
                .Value = Me._supportDocumentAdjusmenNote.CUDS
            }
            documentType.IssueDate = New IssueDateType With {.Value = Me._supportDocumentAdjusmenNote.FecDS}
            documentType.IssueTime = New IssueTimeType With {.Value = Me._supportDocumentAdjusmenNote.HorDS}
            documentType.CreditNoteTypeCode = New CreditNoteTypeCodeType With {.Value = Utils.GetXmlEnumToString(Of DocumentType)(Me._supportDocumentAdjusmenNote.getTypeCode())}
            documentType.Note = New List(Of NoteType) From {New NoteType With {.Value = Me._supportDocumentAdjusmenNote.Description}}
            documentType.DocumentCurrencyCode = New DocumentCurrencyCodeType With {.Value = Utils.GetXmlEnumToString(Of CurrencyCode)(CurrencyCode.COP)}
            documentType.LineCountNumeric = New LineCountNumericType With {.Value = 1} 'Pendiente definir afectación de facturas
            documentType.DiscrepancyResponse = GetDiscrepancyResponseInformation()
            documentType.BillingReference = GetBillingReferenceinformation()
            documentType.AccountingSupplierParty = GetAccountingSupplierPartyInformation()
            documentType.AccountingCustomerParty = GetAccountingCustomerPartyInformation()
            documentType.PaymentMeans = GetPaymentsMeansInformation()
            documentType.TaxTotal = GetTaxTotalInformation()
            documentType.LegalMonetaryTotal = GetLegalMonetaryTotalInformation()
            documentType.CreditNoteLine = GetCreditNoteLineInformation()
            Return documentType
        End Function

#Region "Private Methods"
#Region "UBL Extensions"
        Private Function GenerateUBLExtensions() As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            listUBLExtensionType.Add(GenerateDianExtensions())
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

        Private Function GenerateDianExtensions() As UBLExtensionType
            Dim listUBLExtension As New List(Of UBLExtensionType)

            Dim dianExtensions As New DianExtensionsType() With
            {
                .InvoiceSource = GenerateInvoiceSourceInformation(),
                .SoftwareProvider = GenerateSoftwareProviderInformation(),
                .SoftwareSecurityCode = GenerateSoftwareSecurityCodeInformation(),
                .AuthorizationProvider = GenerateAuthorizationProviderInformation(),
                .QRCode = Me._supportDocumentAdjusmenNote.QR
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
                    .schemeID = Me._customerThirdParty.DigitVerification,
                    .schemeName = Me._customerThirdParty.Person.getAcquirerType(),
                    .Value = Me._customerThirdParty.Person.IdentificationNumber
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
                .Value = Utils.Sha384Encode(String.Concat(Me._settingsAccount.SoftwareIdentifier, Me._settingsAccount.SoftwarePin, Me._supportDocumentAdjusmenNote.Code))
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

#Region "Additional information"

        Private Function GetDiscrepancyResponseInformation() As List(Of ResponseType)
            Dim listResponseType As New List(Of ResponseType)
            listResponseType.Add(New ResponseType With
            {
                .ReferenceID = New ReferenceIDType With {.Value = Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.DocumentNumber},
                .ResponseCode = New ResponseCodeType With {.Value = Me._supportDocumentAdjusmenNote.NoteType},
                .Description = New List(Of DescriptionType) From
                {
                    New DescriptionType(Me._supportDocumentAdjusmenNote.Description)
                }
            })
            Return listResponseType
        End Function

        Private Function GetBillingReferenceinformation() As List(Of BillingReferenceType)
            Dim listBillingReferenceType As New List(Of BillingReferenceType)
            listBillingReferenceType.Add(New BillingReferenceType With
            {
                .InvoiceDocumentReference = New DocumentReferenceType With
                {
                    .ID = New IDType With {.Value = Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.DocumentNumber},
                    .UUID = New UUIDType With
                    {
                        .schemeName = "CUDS-SHA384",
                        .Value = Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.CUDS
                    },
                    .IssueDate = New IssueDateType With {.Value = CType(Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.DocumentDate, DateTime).ToString("yyyy-MM-dd")}
                }
            })
            Return listBillingReferenceType
        End Function

        Private Function GetPaymentsMeansInformation() As List(Of PaymentMeansType)
            Dim valorDS = Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.TotalValue
            Dim paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Credit
            If valorDS = Me._supportDocumentAdjusmenNote.TotalValue Then
                paymentMethod = Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods.Counted
            End If

            Dim listpaymentsMeans As New List(Of PaymentMeansType)
            listpaymentsMeans.Add(New PaymentMeansType With
            {
                .ID = New IDType With {.Value = Utils.GetXmlEnumToString(Of Base.Entities.Enums.ElectronicDocuments.v1_6.PaymentMethods)(paymentMethod)},
                .PaymentMeansCode = New PaymentMeansCodeType With {.Value = "ZZZ"},
                .PaymentDueDate = New PaymentDueDateType With {.Value = Me._supportDocumentAdjusmenNote.DocumentDate}
            })
            Return listpaymentsMeans
        End Function


        ''' <summary>
        ''' Pendiente - no se tiene la información completa del IVA
        ''' </summary>
        ''' <returns></returns>
        Private Function GetTaxTotalInformation() As List(Of TaxTotalType)
            Dim listTaxTotalTypes As New List(Of TaxTotalType)
            'If Me._supportDocumentAdjusmenNote.TaxValue > 0 Then

            'End If
            Return IIf(listTaxTotalTypes.Any(), listTaxTotalTypes, Nothing)
        End Function

        Private Function GetLegalMonetaryTotalInformation() As MonetaryTotalType
            Dim LineExtensionAmount = Me._supportDocumentAdjusmenNote.SubTotalValue 'Valor bruto antes de tributos
            Dim TaxExclusiveAmount As Decimal = 0 'Valor base imponible ??
            Dim TaxInclusiveAmount As Decimal = Me._supportDocumentAdjusmenNote.TaxValue + LineExtensionAmount
            Dim AllowanceTotalAmount As Decimal = 0 'Valor descuento ??
            Dim ChargeTotalAmount As Decimal = 0 'Valor en cargos ??
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
                        .schemeID = thirdParty.DigitVerification,
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
                    .schemeID = thirdParty.DigitVerification,
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

            If thirdParty.Person.Email IsNot Nothing AndAlso thirdParty.Person.Email.Any(Function(e) e.Type = 2) Then
                party.Contact = New ContactType With
            {
                .ElectronicMail = New ElectronicMailType With {.Value = thirdParty.Person.Email.FirstOrDefault(Function(e) e.Type = 2).Email1}
            }
            End If

            Return party
        End Function

#End Region

#Region "Note Details"
        Private Function GetCreditNoteLineInformation() As List(Of CreditNoteLineType)
            Dim listCreditNoteLineType As New List(Of CreditNoteLineType)
            Dim CreditNoteLineType = New CreditNoteLineType() With
            {
                .ID = New IDType With {.Value = 1},
                .CreditedQuantity = New CreditedQuantityType With
                {
                    .unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit),
                    .Value = 1
                },
                .LineExtensionAmount = New LineExtensionAmountType With {.Value = Me._supportDocumentAdjusmenNote.TotalValue}
            }
            CreditNoteLineType.TaxTotal = GetTaxTotalInformation()

            CreditNoteLineType.Item = New ItemType With
            {
                .Description = New List(Of DescriptionType) From
                {
                    New DescriptionType(String.Format("Registro nota de ajuste al documento soporte {0}", Me._supportDocumentAdjusmenNote.ElectronicSupportDocument?.DocumentNumber))
                },
                .StandardItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.schemeID = "999", .schemeName = "Estándar de adopción del contribuyente", .Value = "12345"}}
            }
            CreditNoteLineType.Price = New PriceType With
            {
               .PriceAmount = New PriceAmountType With {.Value = Me._supportDocumentAdjusmenNote.TotalValue},
               .BaseQuantity = New BaseQuantityType With {.unitCode = Utils.GetXmlEnumToString(Of UnitCode)(UnitCode.Unit), .Value = 1}
            }
            listCreditNoteLineType.Add(CreditNoteLineType)
            Return listCreditNoteLineType
        End Function
#End Region

#End Region

#End Region
    End Class
End Namespace
