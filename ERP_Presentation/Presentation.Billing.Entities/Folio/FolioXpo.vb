Imports Presentation.Billing.MVP

Public Class FolioXpo
    Implements IFolio

    Private _details As IEnumerable(Of IFolioDetail)

    Public Property RevenueControlDetailId As Integer Implements IFolio.RevenueControlDetailId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).RevenueControlDetailId
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AdmissionNumber As String Implements IFolio.AdmissionNumber
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).AdmissionNumber
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer? Implements IFolio.BillingAuthorizationId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).BillingAuthorizationId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyId As Integer Implements IFolio.ThirdPartyId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ThirdPartyId
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyNitName As String Implements IFolio.ThirdPartyNitName
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ThirdPartyNitName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CareGroupId As Integer Implements IFolio.CareGroupId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).CareGroupId
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CaregroupEntityType As Byte Implements IFolio.CaregroupEntityType
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).CaregroupEntityType
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CareGroupCodeName As String Implements IFolio.CareGroupCodeName
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).CareGroupCodeName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CareGroupCostCenterId As Integer Implements IFolio.CareGroupCostCenterId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).CareGroupCostCenterId
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property FolioType As Byte Implements IFolio.FolioType
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).FolioType
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Observation As String Implements IFolio.Observation
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).Observation
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property TotalFolio As String Implements IFolio.TotalFolio
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).TotalFolio
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ResponsibleRecoveryFee As Byte Implements IFolio.ResponsibleRecoveryFee
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ResponsibleRecoveryFee
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientDiscountPercentage As Decimal Implements IFolio.PatientDiscountPercentage
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).PatientDiscountPercentage
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientDiscount As Decimal Implements IFolio.PatientDiscount
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).PatientDiscount
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property TotalPatientSalesPrice As Decimal Implements IFolio.TotalPatientSalesPrice
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).TotalPatientSalesPrice
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property TotalPatientWithDiscount As Decimal Implements IFolio.TotalPatientWithDiscount
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).TotalPatientWithDiscount
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property VoucherValue As Decimal Implements IFolio.VoucherValue
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).VoucherValue
        End Get
        Set(value As Decimal)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Status As Byte Implements IFolio.Status
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).Status
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property StatusFolioId As Integer? Implements IFolio.StatusFolioId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).StatusFolioId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property LiquidationType As Byte Implements IFolio.LiquidationType
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).LiquidationType
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property IsMasterAccount As Byte Implements IFolio.IsMasterAccount
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).IsMasterAccount
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property FolioOrder As Byte Implements IFolio.FolioOrder
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).FolioOrder
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractId As Integer? Implements IFolio.ContractId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ContractEntityId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractEntityCodeName As String Implements IFolio.ContractEntityCodeName
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).ContractEntityCodeName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoiceCategoryId As Integer? Implements IFolio.InvoiceCategoryId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).InvoiceCategoryId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientCode As String Implements IFolio.PatientCode
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).PatientCode
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractEntityId As Integer? Implements IFolio.ContractEntityId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ContractEntityId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractCodeName As String Implements IFolio.ContractCodeName
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).ContractCodeName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoiceCategoryCodeName As String Implements IFolio.InvoiceCategoryCodeName
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).InvoiceCategoryCodeName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AdmissionNumberPatient As String Implements IFolio.AdmissionNumberPatient
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).AdmissionNumberPatient
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyPatientId As Integer Implements IFolio.ThirdPartyPatientId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ThirdPartyPatientId
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property HealthAdministratorId As Integer? Implements IFolio.HealthAdministratorId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).HealthAdministratorId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property HealthAdministratorCodeName As String Implements IFolio.HealthAdministratorCodeName
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).HealthAdministratorCodeName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyHealthAdministrator As Integer? Implements IFolio.ThirdPartyHealthAdministrator
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ThirdPartyHealthAdministrator
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoiceId As Integer? Implements IFolio.InvoiceId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).InvoiceId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoiceNumber As String Implements IFolio.InvoiceNumber
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).InvoiceNumber
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoiceDate As Date Implements IFolio.InvoiceDate
        Get
            If Me._details.Count = 0 Then
                Return Nothing
            End If

            Return DirectCast(Me._details(0), Object).InvoiceDate
        End Get
        Set(value As Date)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property InvoicedUser As String Implements IFolio.InvoicedUser
        Get
            If Me._details.Count = 0 Then
                Return ""
            End If

            Return DirectCast(Me._details(0), Object).InvoicedUser
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property StatusFolioName As String Implements IFolio.StatusFolioName
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).StatusFolioName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyResponsibleQuotaNitName As String Implements IFolio.ThirdPartyResponsibleQuotaNitName
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).ThirdPartyResponsibleQuotaNitName
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientQuotaResponsibleThirdPartyId As Integer? Implements IFolio.PatientQuotaResponsibleThirdPartyId
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).PatientQuotaResponsibleThirdPartyId
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Details As IEnumerable(Of IFolioDetail) Implements IFolio.Details
        Get
            Return IIf(_details IsNot Nothing AndAlso _details.Any() AndAlso _details(0).Id > 0, _details, New List(Of IFolioDetail)())
        End Get
        Set(value As IEnumerable(Of IFolioDetail))
            Me._details = value
        End Set
    End Property

    Public Property CurrencyAbbreviation As String Implements IFolio.CurrencyAbbreviation
        Get
            If Me._details.Count = 0 Then
                Return 0
            End If

            Return DirectCast(Me._details(0), Object).CurrencyAbbreviation
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Bandera que determina si el folio tiene guarda una justificacion de Sin recaudo de cuota
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FeeNotCollectedId As Integer? Implements IFolio.FeeNotCollectedId
        Get
            If Me._details?.Any() Then
                Return DirectCast(Me._details(0), Object).FeeNotCollectedId
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Id de la factura basica copago relacionada al ingreso
    ''' </summary>
    ''' <returns></returns>
    Public Property BasicbillingCopayId As Integer? Implements IFolio.BasicbillingCopayId
        Get
            If Me._details?.Any() Then
                Return DirectCast(Me._details(0), Object).BasicbillingCopayId
            Else
                Return Nothing
            End If
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Aplica o no para logica de tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyLogicThirdPartyBeneficiary As Boolean Implements IFolio.ApplyLogicThirdPartyBeneficiary
        Get
            If Me._details?.Any() Then
                Return DirectCast(Me._details(0), Object).ApplyLogicThirdPartyBeneficiary
            Else
                Return Nothing
            End If
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
