'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Andres Alarcon
' Created          : 2025-01-15
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' Vista para listar servicios causados sin reconocer
''' Mapea a: MedicalFees.ViewListCausationwithoutRecognition
''' </summary>
<Persistent("MedicalFees.ViewListCausationwithoutRecognition")>
Public Class ViewListCausationwithoutRecognition
    Inherits XPLiteObject

#Region "Members"

    ''' <summary>
    ''' ID del ServiceOrderDetail (Clave primaria)
    ''' Corresponde a: sod.Id
    ''' </summary>
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    ''' <summary>
    ''' Estado de la operación de reconocimiento (para UI)
    ''' 0 = Sin procesar, 1 = Procesando, 2 = Exitoso, 3 = Error
    ''' </summary>
    <NonPersistent()>
    Public Property StateOperation As Byte = 0

    ''' <summary>
    ''' Mensaje informativo del resultado del procesamiento (para UI)
    ''' </summary>
    <NonPersistent()>
    Public Property MessageInfo As String

    ''' <summary>
    ''' Profesional de la salud - Código y Nombre
    ''' Corresponde a: CONCAT(RTRIM(LTRIM(i.CODPROSAL)),' - ',i.NOMMEDICO)
    ''' </summary>
    Dim fProfessionalCodeName As String
    Public Property ProfessionalCodeName() As String
        Get
            Return fProfessionalCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalCodeName", fProfessionalCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Contrato - Código y Nombre
    ''' Corresponde a: CONCAT(mc.Code, ' - ', mc.ContractName)
    ''' </summary>
    Dim fContractCodeName As String
    Public Property ContractCodeName() As String
        Get
            Return fContractCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCodeName", fContractCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Tipo de Contrato (Médico / Agremiación)
    ''' Corresponde a: CASE mc.ContractType WHEN 1 THEN 'Médico' WHEN 2 THEN 'Agremiación' END
    ''' </summary>
    Dim fContractTypeName As String
    Public Property ContractTypeName() As String
        Get
            Return fContractTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractTypeName", fContractTypeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Proveedor asociado (Médico o Agremiación)
    ''' Corresponde a: IIF(mc.ContractType = 1, i.NOMMEDICO, s.Name)
    ''' </summary>
    Dim fSupplierName As String
    Public Property SupplierName() As String
        Get
            Return fSupplierName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SupplierName", fSupplierName, value)
        End Set
    End Property

    ''' <summary>
    ''' Número de Admisión
    ''' Corresponde a: mfc.AdmissionNumber
    ''' </summary>
    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    ''' <summary>
    ''' Paciente - Código y Nombre
    ''' Corresponde a: CONCAT(LTRIM(RTRIM(a.IPCODPACI)), ' - ', a.IPNOMCOMP)
    ''' </summary>
    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Grupo de Atención - Código y Nombre
    ''' Corresponde a: CONCAT(c.Code, ' - ', c.Name)
    ''' </summary>
    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Entidad Administradora - Código y Nombre
    ''' Corresponde a: CONCAT(ha.Code, ' - ', ha.Name)
    ''' </summary>
    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Servicio CUPS - Código y Descripción
    ''' Corresponde a: CONCAT(ce.Code, ' - ', ce.Description)
    ''' </summary>
    Dim fCupsEntityCodeName As String
    Public Property CupsEntityCodeName() As String
        Get
            Return fCupsEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityCodeName", fCupsEntityCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Cantidad del servicio
    ''' Corresponde a: mfc.InvoiceQuantity
    ''' </summary>
    Dim fInvoiceQuantity As Decimal
    Public Property InvoiceQuantity() As Decimal
        Get
            Return fInvoiceQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceQuantity", fInvoiceQuantity, value)
        End Set
    End Property

    ''' <summary>
    ''' Fecha de prestación del servicio
    ''' Corresponde a: sod.ServiceDate
    ''' </summary>
    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    ''' <summary>
    ''' Unidad Funcional del Servicio - Código y Nombre
    ''' Corresponde a: CONCAT(fu.Code, ' - ', fu.Name)
    ''' </summary>
    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    ''' <summary>
    ''' Total Facturado del servicio
    ''' Corresponde a: sod.TotalSalesPrice
    ''' </summary>
    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la causación
    ''' Corresponde a: mfc.CausationDate
    ''' </summary>
    Dim fCausationDate As DateTime
    Public Property CausationDate() As DateTime
        Get
            Return fCausationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CausationDate", fCausationDate, value)
        End Set
    End Property

    ''' <summary>
    ''' Valor Causado
    ''' Corresponde a: mfc.MedicalFeesContractValue
    ''' </summary>
    Dim fCausationValue As Decimal
    Public Property CausationValue() As Decimal
        Get
            Return fCausationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CausationValue", fCausationValue, value)
        End Set
    End Property

    ''' <summary>
    ''' Estado del servicio (Facturado / Sin Facturar)
    ''' Corresponde a: IIF(id.Id IS NOT NULL, 'Facturado', 'Sin Facturar')
    ''' </summary>
    Dim fServiceStatus As String
    Public Property ServiceStatus() As String
        Get
            Return fServiceStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceStatus", fServiceStatus, value)
        End Set
    End Property

    ''' <summary>
    ''' Número de Factura (si el servicio está facturado)
    ''' Corresponde a: IIF(id.Id IS NOT NULL, inv.InvoiceNumber, NULL)
    ''' </summary>
    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    ''' <summary>
    ''' ID de la Unidad Operativa (usado para filtrado)
    ''' Corresponde a: mfc.OperatingUnitId (desde WHERE de la vista SQL)
    ''' </summary>
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    ''' <summary>
    ''' ID del Proveedor
    ''' </summary>
    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property

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
