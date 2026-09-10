Imports DevExpress.Xpo

''' <summary>
''' Autor: HECTOR RODRIGUEZ
''' Date : 09/07/2019
''' Use  : lista componentes sanguineos
''' </summary>
<Persistent("dbo.HCCOMSAND")> _
Partial Public Class HCCOMSANDXpo
    Inherits XPLiteObject

    Dim fID As Integer
    <Key()>
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    Dim fCOMSAMID As HCCOMSANXpo
    <Association("Reference_HCCOMSANDXpo")>
    Public Property COMSAMID() As HCCOMSANXpo
        Get
            Return fCOMSAMID
        End Get
        Set(ByVal value As HCCOMSANXpo)
            SetPropertyValue("COMSAMID", fCOMSAMID, value)
        End Set
    End Property

    Dim fCODSERIPS As String
    <Size(20)>
    Public Property CODSERIPS() As String
        Get
            Return fCODSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODSERIPS", fCODSERIPS, value)
        End Set
    End Property

    Dim fTIPSERIPS As Short
    Public Property TIPSERIPS() As Short
        Get
            Return fTIPSERIPS
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("TIPSERIPS", fTIPSERIPS, value)
        End Set
    End Property

    Dim fTIPOCARGUE As Short
    Public Property TIPOCARGUE() As Short
        Get
            Return fTIPOCARGUE
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("TIPOCARGUE", fTIPOCARGUE, value)
        End Set
    End Property

    <PersistentAlias("CUPSEntityContractDescriptions.Id")>
    Public ReadOnly Property IDDESCRIPCIONRELACIONADA As Integer?
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("IDDESCRIPCIONRELACIONADA"))
        End Get
    End Property

    Dim fCUPSEntityContractDescriptions As XpoCUPSEntityContractDescriptions
    <Persistent("IDDESCRIPCIONRELACIONADA")>
    <Association("HCCOMSANDXpo_Reference")>
    Public Property CUPSEntityContractDescriptions() As XpoCUPSEntityContractDescriptions
        Get
            Return fCUPSEntityContractDescriptions
        End Get
        Set(ByVal value As XpoCUPSEntityContractDescriptions)
            SetPropertyValue("CUPSEntityContractDescriptions", fCUPSEntityContractDescriptions, value)
        End Set
    End Property

    <PersistentAlias("CUPSEntityContractDescriptions.ContractDescriptionId")>
    Public ReadOnly Property ContractDescriptionId As Integer?
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ContractDescriptionId"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub


End Class
