Imports DevExpress.Xpo

''' <summary>
''' Clase XPO para la vista AccountManagement.ViewReportAccountManagement.
''' Esta vista contiene información consolidada de folios para el reporte de gestión de cuentas.
''' </summary>
<Persistent("AccountManagement.ViewReportAccountManagement")>
Public Class ViewReportAccountManagementXpo
    Inherits XPLiteObject

#Region "Constructors"
    Public Sub New(session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' Identificador único del detalle de control de ingresos (RevenueControlDetail).
    ''' Usado como clave primaria de la vista.
    ''' </summary>
    <Key(True)>
    <Persistent("RevenueControlDetailId")>
    Public Property RevenueControlDetailId As Integer

    ''' <summary>
    ''' Código del centro de atención donde se generó el folio.
    ''' </summary>
    <Persistent("AttentionCenterCode"), Size(10)>
    Public Property AttentionCenterCode As String

    ''' <summary>
    ''' Nombre del área de gestión asignada al folio.
    ''' Puede provenir de una asignación automática o de un traslado aceptado.
    ''' </summary>
    <Persistent("ManagementAreaName"), Size(100)>
    Public Property ManagementAreaName As String

    ''' <summary>
    ''' Estado del folio (Status del RevenueControlDetail).
    ''' Valores posibles: 1=Registrado, 2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada, 7=Folio Cerrado.
    ''' Usado para FILTRAR.
    ''' </summary>
    <Persistent("FolioStatus")>
    Public Property FolioStatus As Byte

    ''' <summary>
    ''' Descripción textual del estado del folio.
    ''' Ejemplo: "Registrado", "Facturado", "Bloqueado", etc.
    ''' Usado para MOSTRAR en el reporte.
    ''' </summary>
    <Persistent("FolioStatusDescription"), Size(30)>
    Public Property FolioStatusDescription As String

    ''' <summary>
    ''' Nombre del grupo de atención (contrato) asociado al folio.
    ''' </summary>
    <Persistent("CareGroupName"), Size(100)>
    Public Property CareGroupName As String

    ''' <summary>
    ''' Nombre de la entidad de salud (HealthAdministrator) asociada a la factura del folio.
    ''' </summary>
    <Persistent("HealthAdministrationName"), Size(300)>
    Public Property HealthAdministrationName As String

    ''' <summary>
    ''' Valor total del folio.
    ''' </summary>
    <Persistent("FolioTotalValue")>
    Public Property FolioTotalValue As Decimal

    ''' <summary>
    ''' Código/identificación del paciente (NIT, cédula, etc.).
    ''' Usado para filtrar por tercero.
    ''' </summary>
    <Persistent("PatientCode"), Size(25)>
    Public Property PatientCode As String

    ''' <summary>
    ''' Nombre completo del paciente (IPNOMCOMP).
    ''' </summary>
    <Persistent("PatientCodeName"), Size(250)>
    Public Property PatientCodeName As String

    ''' <summary>
    ''' Número de ingreso del paciente.
    ''' </summary>
    <Persistent("AdmissionNumber"), Size(10)>
    Public Property AdmissionNumber As String

    ''' <summary>
    ''' Fecha de ingreso del paciente.
    ''' </summary>
    <Persistent("AdmissionDate")>
    Public Property AdmissionDate As DateTime

    ''' <summary>
    ''' Nombre de la unidad funcional donde se atendió al paciente.
    ''' </summary>
    <Persistent("FunctionalUnitName"), Size(60)>
    Public Property FunctionalUnitName As String

    ''' <summary>
    ''' Número de factura asociada al folio.
    ''' </summary>
    <Persistent("InvoiceNumber"), Size(20)>
    Public Property InvoiceNumber As String

    ''' <summary>
    ''' Nombre completo del usuario actualmente asignado al folio.
    ''' Puede ser el usuario de asignación automática o el usuario receptor del último traslado aceptado.
    ''' </summary>
    <Persistent("CurrentOwnerFullName"), Size(100)>
    Public Property CurrentOwnerFullName As String

    ''' <summary>
    ''' Código del usuario actualmente asignado al folio.
    ''' Usado para filtrar por usuario asignado.
    ''' </summary>
    <Persistent("CurrentOwnerCode"), Size(20)>
    Public Property CurrentOwnerCode As String

    ''' <summary>
    ''' ID del administrador de salud (entidad).
    ''' Usado para filtrar por entidad.
    ''' </summary>
    <Persistent("HealthAdministrationId")>
    Public Property HealthAdministrationId As Integer?

    ''' <summary>
    ''' ID del grupo de atención.
    ''' Usado para filtrar por grupo de atención.
    ''' </summary>
    <Persistent("CareGroupId")>
    Public Property CareGroupId As Integer

    ''' <summary>
    ''' ID del área de gestión asignada al folio.
    ''' Puede ser NULL si no tiene área asignada.
    ''' </summary>
    <Persistent("ManagementAreaId")>
    Public Property ManagementAreaId As Integer?

#End Region

End Class

