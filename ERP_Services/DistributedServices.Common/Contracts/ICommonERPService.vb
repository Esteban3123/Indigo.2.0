'***********************************************************************
' Assembly         : DistributedService.Common
' Author           : Cristhian Salazar
' Created          : 10/07/2013
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 11-04-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()> _
Public Interface ICommonERPService
    Inherits ICommonERPCity, ICommonERPCommon, ICommonERPCountry, ICommonERPCurrency, ICommonERPDepartment, ICommonERPBlockRecord, ICommonERPBasicAudit
    Inherits ICommonERPPhoneType, ICommonERPThirdParty, ICommonERPDisability, ICommonERPTimeUnit, ICommonERPHoliday, ICommonERPConceptGlosas
    Inherits ICommonERPOperatingUnit, ICommonERPBank, ICommonERPSuppliersDistributionLines, ICommonERPDistributionLines, ICommonSequense, ICommonERPEconomicActivity, ICommonERPSuppliersDetailType
    Inherits ICommonERPBalancedScorecard, ICommonERPAttachment, ICommonERPTaxExemptions

    <OperationContract()>
    Function GetDataTable(procedureName As String, parameters As List(Of String()), session As SessionValues) As DataTable

End Interface
