Public Interface ICtrFolio

    ReadOnly Property Id As Integer

    ReadOnly Property FolioOrder As Integer

    ReadOnly Property IsInvoiced As Boolean

    Property Status As Byte

    ReadOnly Property TotalEntity As Decimal

    ReadOnly Property TotalPatient As Decimal

    ReadOnly Property ThirdPartySalesPrice As Decimal

    ReadOnly Property VoucherValue As Decimal

    ReadOnly Property TotalPatientWithDiscount As Decimal

    ReadOnly Property PatientDiscount As Decimal

    ReadOnly Property GrandTotalDiscount As Decimal

    ReadOnly Property FolioType As Byte

    ReadOnly Property LiquidationType As Byte

    ReadOnly Property CareGroupCostCenterId As Integer

    Property FormOwner As ILiquidation

    ReadOnly Property CareGroupId As Integer

    ReadOnly Property RevenueControlId As Integer

    WriteOnly Property IsOncologycalMode As Boolean

    Property IsUnique As Boolean

    Property AdmissionType As Int32

    Property IsMasterAccount As Byte

    Sub SetSleNullText(ByVal text As String)

    Property AdmissionNumber As String

    ''' <summary>
    ''' Propiedad que almacene el num de autorización del ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AuthorizationNumber As String

    ''' <summary>
    ''' Propiedad que indica si el folio califica para la logica de tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Property ApplyLogicThirdPartyBeneficiary As Boolean

    ''' <summary>
    ''' Propiedad que indica el tercero del folio
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' Almacena el tipo de entidad asociada al grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Property CareGroupEntityType As Byte

    WriteOnly Property EgressChange As Boolean

    Sub SetDatasourceAsync(ByVal id As Integer, ByVal status As Byte, Optional withPrintReport As Boolean = False, Optional nullCategories As Boolean = False)

End Interface
