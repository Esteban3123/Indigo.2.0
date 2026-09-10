'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("MedicalFees.MedicalFeesContract")> _
Public Class MedicalFeesContractXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
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
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fContractName As String
    <Size(100)> _
    <Persistent("ContractName")> _
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property

    Dim fContractNumber As String
    <Size(15)> _
    <Persistent("ContractNumber")> _
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fSupplierId As Maintenance_Supplier
    <Association("MaintenanceSupplierReferencesMedicalFeesContract")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fSupplierDistributionLineId As Integer
    <Persistent("SupplierDistributionLineId")> _
    Public Property SupplierDistributionLineId() As Integer
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property

    Dim fContractType As Integer
    <Persistent("ContractType")> _
    Public Property ContractType() As Integer
        Get
            Return fContractType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractType", fContractType, value)
        End Set
    End Property

    <PersistentAlias("Iif(ContractType = 1, 'Estandar', Iif(ContractType = 2, 'Agremiaciones', ''))")>
    Public ReadOnly Property ContractTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContractTypeName"))
        End Get
    End Property

    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', Iif(Status = 2, 'Suspendido',Iif(Status = 3, 'Terminado', '')))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fLastLiquidationDate As DateTime
    Public Property LastLiquidationDate() As DateTime
        Get
            Return fLastLiquidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastLiquidationDate", fLastLiquidationDate, value)
        End Set
    End Property

    <Association("MedicalFeesCausationReferencesMedicalFeesContract", GetType(MedicalFeesCausationXpo))> _
    Public ReadOnly Property MedicalFeesCausationXpo() As XPCollection(Of MedicalFeesCausationXpo)
        Get
            Return GetCollection(Of MedicalFeesCausationXpo)("MedicalFeesCausationXpo")
        End Get
    End Property

    <Association("MedicalFeesLiquidationReferencesMedicalFeesContract", GetType(MedicalFeesLiquidationXpo))> _
    Public ReadOnly Property MedicalFeesLiquidationXpo() As XPCollection(Of MedicalFeesLiquidationXpo)
        Get
            Return GetCollection(Of MedicalFeesLiquidationXpo)("MedicalFeesLiquidationXpo")
        End Get
    End Property

    <Association("HealthProfessionalContractReferencesMedicalFeesContract", GetType(HealthProfessionalContractXpo))> _
    Public ReadOnly Property HealthProfessionalContractXpo() As XPCollection(Of HealthProfessionalContractXpo)
        Get
            Return GetCollection(Of HealthProfessionalContractXpo)("HealthProfessionalContractXpo")
        End Get
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),ContractName)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

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
