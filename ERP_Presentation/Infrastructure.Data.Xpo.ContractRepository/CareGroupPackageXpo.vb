'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Diego A. Roldán
' Created          : 2021-08-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.CareGroupPackage")>
Public Class CareGroupPackage
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    <Persistent("CareGroupId")>
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fContractPackageId As ContractPackageXpo
    <Association("ContractPackageReferencesCareGroupPackage")>
    Public Property ContractPackageId() As ContractPackageXpo
        Get
            Return fContractPackageId
        End Get
        Set(ByVal value As ContractPackageXpo)
            SetPropertyValue("ContractPackageId", fContractPackageId, value)
        End Set
    End Property

    'Dim fContractPackageId As Integer
    '<Persistent("ContractPackageId")>
    'Public Property ContractPackageId() As Integer
    '    Get
    '        Return fContractPackageId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("ContractPackageId", fContractPackageId, value)
    '    End Set
    'End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
