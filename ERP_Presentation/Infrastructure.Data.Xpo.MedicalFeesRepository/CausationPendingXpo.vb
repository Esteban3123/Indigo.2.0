'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Diego A. Roldán
' Created          : 2022-05-18
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("MedicalFees.CausationPending")>
Public Class CausationPendingXpo
    Inherits XPLiteObject

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

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(value As String)
            SetPropertyValue("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPerformsHealthProfessionalCode As String
    Public Property PerformsHealthProfessionalCode() As String
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
        End Set
    End Property

    Dim fInvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return fInvoiceDate
        End Get
        Set(value As Date)
            SetPropertyValue("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fData As String
    Public Property Data() As String
        Get
            Return fData
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Data", fData, value)
        End Set
    End Property

    Dim fIsQx As Boolean
    Public Property IsQx() As Boolean
        Get
            Return fIsQx
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsQx", fIsQx, value)
        End Set
    End Property

    Dim fError As String
    <Persistent("Error")>
    Public Property ErrorMsg() As String
        Get
            Return fError
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ErrorMsg", fError, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("CreationDate", fCreationDate, value)
        End Set
    End Property

    <PersistentAlias("concat(Trim(PatientCode), ' - ', PatientName)")>
    Public ReadOnly Property PatientCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PatientCodeName"))
        End Get
    End Property

    ''' <summary>
    ''' Propiedad calculada que deserializa el campo Data y retorna la descripción del servicio IPS
    ''' según si es quirúrgico (IsQx = True) o no quirúrgico (IsQx = False)
    ''' </summary>
    ''' <remarks>
    ''' Esta propiedad se puede usar directamente en la rejilla con XPInstantFeedbackSource
    ''' sin necesidad de CustomUnboundColumnData
    ''' </remarks>
    <NonPersistent>
    Public ReadOnly Property IPSServiceDescription() As String
        Get
            Try
                ' Validar que el campo Data no esté vacío
                If String.IsNullOrWhiteSpace(Me.Data) Then
                    Return String.Empty
                End If

                ' Deserializar según el tipo (Quirúrgico o No Quirúrgico)
                If Me.IsQx Then
                    ' Deserializar como ViewListSurgicalAndPackage (Quirúrgico)
                    Dim surgical = CrossCutting.Base.Utils.DeserializeJsonToEntity(Of Dictionary(Of String, Object))(Me.Data)
                    If surgical IsNot Nothing AndAlso surgical.ContainsKey("IPSServiceDescription") Then
                        Return surgical("IPSServiceDescription")?.ToString()
                    End If
                Else
                    ' Deserializar como ViewListNoSurgical (No Quirúrgico)
                    Dim nonSurgical = CrossCutting.Base.Utils.DeserializeJsonToEntity(Of Dictionary(Of String, Object))(Me.Data)
                    If nonSurgical IsNot Nothing AndAlso nonSurgical.ContainsKey("IPSServiceDescription") Then
                        Return nonSurgical("IPSServiceDescription")?.ToString()
                    End If
                End If

                Return String.Empty

            Catch ex As Exception
                ' En caso de error de deserialización, retornar mensaje de error
                System.Diagnostics.Debug.WriteLine($"Error deserializando IPSServiceDescription (Id: {Me.Id}): {ex.Message}")
                Return String.Empty
            End Try
        End Get
    End Property

    <NonPersistent>
    Public Property SelectOption As Boolean

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
