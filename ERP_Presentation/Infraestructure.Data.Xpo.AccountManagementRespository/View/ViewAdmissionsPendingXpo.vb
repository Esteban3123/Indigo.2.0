Imports DevExpress.Xpo

''' <summary>
''' Clase XPO para la vista AccountManagement.ViewAdmissionsPending.
''' Esta vista contiene información de ingresos pendientes de asignación automática.
''' </summary>
<Persistent("AccountManagement.ViewAdmissionsPending")>
Public Class ViewAdmissionsPendingXpo
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
    ''' Número de ingreso del paciente.
    ''' Usado como clave primaria de la vista.
    ''' </summary>
    <Key(True)>
    <Persistent("AdmissionNumber"), Size(10)>
    Public Property AdmissionNumber As String

    ''' <summary>
    ''' Nombre completo del paciente.
    ''' </summary>
    <Persistent("PatientFullName"), Size(250)>
    Public Property PatientFullName As String

    ''' <summary>
    ''' Fecha de ingreso del paciente.
    ''' </summary>
    <Persistent("AdmissionDate")>
    Public Property AdmissionDate As DateTime

    ''' <summary>
    ''' Código del paciente (identificación).
    ''' </summary>
    <Persistent("PatientCode"), Size(25)>
    Public Property PatientCode As String

    ''' <summary>
    ''' NIT del paciente.
    ''' </summary>
    <Persistent("Nit"), Size(25)>
    Public Property Nit As String

    ''' <summary>
    ''' Nombre de la unidad funcional donde se atendió al paciente.
    ''' </summary>
    <Persistent("FunctionalUnitCodeName"), Size(60)>
    Public Property FunctionalUnitCodeName As String

    ''' <summary>
    ''' Grupo de atención (concatenación de código y nombre).
    ''' </summary>
    <Persistent("CareGroup"), Size(250)>
    Public Property CareGroup As String

    ''' <summary>
    ''' Número de cama actual del paciente.
    ''' </summary>
    <Persistent("Bed")>
    Public Property Bed As Integer

    ''' <summary>
    ''' Descripción del diagnóstico del ingreso.
    ''' </summary>
    <Persistent("Diagnosis"), Size(350)>
    Public Property Diagnosis As String

    ''' <summary>
    ''' Tipo de ingreso del paciente.
    ''' </summary>
    <Persistent("TypeIncome")>
    Public Property TypeIncome As Byte

    ''' <summary>
    ''' Código del centro de atención donde se generó el ingreso.
    ''' </summary>
    <Persistent("AttentionCenterCode"), Size(10)>
    Public Property AttentionCenterCode As String

    ''' <summary>
    ''' ID del grupo de atención.
    ''' Usado para filtrar por grupo de atención.
    ''' </summary>
    <Persistent("CareGroupId")>
    Public Property CareGroupId As Integer

    ''' <summary>
    ''' Código del grupo de atención.
    ''' </summary>
    <Persistent("CareGroupCode"), Size(20)>
    Public Property CareGroupCode As String

    ''' <summary>
    ''' Código de la unidad funcional.
    ''' </summary>
    <Persistent("FunctionalUnitCode"), Size(10)>
    Public Property FunctionalUnitCode As String

    ''' <summary>
    ''' Código del diagnóstico.
    ''' </summary>
    <Persistent("DiagnosisCode"), Size(4)>
    Public Property DiagnosisCode As String

    ''' <summary>
    ''' Estado del ingreso.
    ''' </summary>
    <Persistent("IncomeStatus")>
    Public Property IncomeStatus As String

    ''' <summary>
    ''' Usuario que creó el ingreso.
    ''' </summary>
    <Persistent("UserCreation"), Size(20)>
    Public Property UserCreation As String

    ''' <summary>
    ''' Usuario que modificó el ingreso.
    ''' </summary>
    <Persistent("UserModification"), Size(20)>
    Public Property UserModification As String

    ''' <summary>
    ''' Estado de asociación de folios.
    ''' Valores posibles: "Folios asociados", "Folios no asociados".
    ''' </summary>
    <Persistent("Folio"), Size(25)>
    Public Property Folio As String

    ''' <summary>
    ''' Descripción del tipo de ingreso
    ''' </summary>
    ''' <returns></returns>
    <Persistent("TypeIncomeName"), Size(15)>
    Public Property TypeIncomeName As String

#End Region

End Class

