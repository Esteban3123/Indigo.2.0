Imports DevExpress.Xpo

<Persistent("AccountManagement.ViewFolioTransferInformation")>
Public Class ViewFolioTransferInformationXpo
    Inherits XPLiteObject

#Region "Constructors"
    Public Sub New(session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

    <Key(True)>
    Public Property Id As Integer

    <Persistent("AdmissionNumber"), Size(10)>
    Public Property AdmissionNumber As String

    <Persistent("PatientFullName"), Size(250)>
    Public Property PatientFullName As String

    <Persistent("AdmissionDate")>
    Public Property AdmissionDate As DateTime

    <Persistent("PatientCode"), Size(25)>
    Public Property PatientCode As String

    <Persistent("HasFolioAlert")>
    Public Property HasFolioAlert As Boolean

    <Persistent("BedNumber"), Size(10)>
    Public Property BedNumber As String

    <Persistent("Diagnosis"), Size(350)>
    Public Property Diagnosis As String

    <Persistent("FolioNumber")>
    Public Property FolioNumber As Integer?

    <Persistent("FolioTotalValue")>
    Public Property FolioTotalValue As Decimal

    <Persistent("RevenueControlDetailId")>
    Public Property RevenueControlDetailId As Integer

    <Persistent("FunctionalUnitName"), Size(200)>
    Public Property FunctionalUnitName As String

    <Persistent("CareGroupName"), Size(200)>
    Public Property CareGroupName As String

    <Persistent("CareGroupId")>
    Public Property CareGroupId As Integer

    <Persistent("AttentionCenterCode"), Size(20)>
    Public Property AttentionCenterCode As String

    <Persistent("FolioType"), Size(50)>
    Public Property FolioType As String

    <Persistent("FolioStatusDescription"), Size(50)>
    Public Property FolioStatusDescription As String

    <Persistent("AssignmentDate")>
    Public Property AssignmentDate As DateTime?

    <Persistent("AssignedUserFullname"), Size(100)>
    Public Property AssignedUserFullname As String

    <Persistent("AssignedUserCode"), Size(50)>
    Public Property AssignedUserCode As String

    <Persistent("ManagementAreaName"), Size(150)>
    Public Property ManagementAreaName As String

    <Persistent("AdmissionCreationUser"), Size(50)>
    Public Property AdmissionCreationUser As String

    <Persistent("AdmissionModificationUser"), Size(50)>
    Public Property AdmissionModificationUser As String

    <Persistent("InvoiceUser"), Size(50)>
    Public Property InvoiceUser As String

    <Persistent("AssociatedInvoice"), Size(50)>
    Public Property AssociatedInvoice As String

    <Persistent("IsOnTime")>
    Public Property IsOnTime As Integer

    ' En la vista esta columna es texto (“Sin traslado”, “Aceptada”, etc.)
    <Persistent("TransferStatus"), Size(50)>
    Public Property TransferStatus As String

    ' -------- NUEVAS columnas del SELECT para ownership/traslados --------
    <Persistent("CurrentOwnerCode"), Size(50)>
    Public Property CurrentOwnerCode As String

    <Persistent("TransferPreviousUser"), Size(50)>
    Public Property TransferPreviousUser As String

    <Persistent("TransferReceivingUser"), Size(50)>
    Public Property TransferReceivingUser As String

    <Persistent("IsPendingToAccept")>
    Public Property IsPendingToAccept As Boolean

    <Persistent("WasRejected")>
    Public Property WasRejected As Boolean

    <Persistent("IsPendingFromMe")>
    Public Property IsPendingFromMe As Boolean
End Class
