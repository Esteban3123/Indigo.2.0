Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.PortfolioReclassification")> _
Public Class PortfolioReclassificationXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    <PersistentAlias("Iif(DocumentType = 1, 'Radicación', Iif(DocumentType = 2, 'Devoluciones',Iif(DocumentType = 3, 'Recepción de Glosas Subsanable', Iif(DocumentType = 4, 'Conciliaciones',Iif(DocumentType = 5, 'Reiteración', '')))))")>
    Public ReadOnly Property DocumentTypeName As String
        Get
            'Select Case fDocumentType
            '    Case 1
            '        Return "Radicación"
            '    Case 2
            '        Return "Devoluciones"
            '    Case 3
            '        Return "Recepción de Glosas Subsanable"
            '    Case 4
            '        Return "Conciliaciones"
            '    Case 5
            '        Return "Reiteración"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
        End Get
    End Property

    Dim fAccountReceivableId As PortfolioAccountReceivableXpo
    <Association("PortfolioReclassificationXpoReferencesPortfolioAccountReceivableXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableXpo)
            SetPropertyValue(Of PortfolioAccountReceivableXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fSourceAccountId As GeneralLedger_MainAccounts
    <Association("PortfolioReclassificationXpoReferencesGeneralLedger_MainAccounts")> _
    Public Property SourceAccountId() As GeneralLedger_MainAccounts
        Get
            Return fSourceAccountId
        End Get
        Set(ByVal value As GeneralLedger_MainAccounts)
            SetPropertyValue(Of GeneralLedger_MainAccounts)("SourceAccountId", fSourceAccountId, value)
        End Set
    End Property
    Dim fTargetAccountId As GeneralLedger_MainAccounts
    <Association("PortfolioReclassificationXpoReferencesGeneralLedger_MainAccounts2")> _
    Public Property TargetAccountId() As GeneralLedger_MainAccounts
        Get
            Return fTargetAccountId
        End Get
        Set(ByVal value As GeneralLedger_MainAccounts)
            SetPropertyValue(Of GeneralLedger_MainAccounts)("TargetAccountId", fTargetAccountId, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fStatus As String
    Public Property Status() As String
        Get
            Select Case fStatus
                Case 1
                    fStatus = ResourceManager.GetString("StateUnconfirmed")
                Case 2
                    fStatus = ResourceManager.GetString("StateConfirmed")
                Case 3
                    fStatus = ResourceManager.GetString("StatusCanceled")
            End Select
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
