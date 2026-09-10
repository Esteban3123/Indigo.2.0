Imports System.Runtime.Serialization
Partial Public Class GeneralLedgerSettings

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdDeficitAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdSuperavitAccountDescription As String


    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdUtilityAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdCloseDocumentDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdDistrictTreasuryDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdDianDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdApprovalDocumentDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdMovementDocumentDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdIvaRetentionConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdIcaRetentionConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdSourceRetentionConceptDescription As String

#End Region

#Region "Methods"

    Public Function GetElectronicDocumentUrl() As String
        If Me.Environment Then
            Return "https://vpfe.dian.gov.co/WcfDianCustomerServices.svc?wsdl"
        Else
            Return "https://vpfe-hab.dian.gov.co/WcfDianCustomerServices.svc?wsdl"
        End If
    End Function

#End Region

End Class
