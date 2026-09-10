Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.Person")> _
Public Class CommonPersonXpo
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
    Dim fIdentificationNumber As String
    <Size(15)>
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationNumber", fIdentificationNumber, value)
        End Set
    End Property

    Dim fIdentificationType As Integer
    Public Property IdentificationType() As Integer
        Get
            Return fIdentificationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdentificationType", fIdentificationType, value)
        End Set
    End Property

    <PersistentAlias("Iif(
IdentificationType = 0, 'Cédula de Ciudadanía', 
IdentificationType = 1, 'Cédula de Extranjería', 
IdentificationType = 2, 'Tarjeta de Identidad', 
IdentificationType = 3, 'Registro Civil', 
IdentificationType = 4, 'Pasaporte', 
IdentificationType = 5, 'Adulto Sin Identificación', 
IdentificationType = 6, 'Menor Sin Identificación', 
IdentificationType = 7, 'Nit', 
IdentificationType = 8, 'Número único de identificación personal', 
IdentificationType = 9, 'Certificado Nacido Vivo', 
IdentificationType = 10, 'Carnet Diplomático (Aplica para extranjeros)', 
IdentificationType = 11, 'Salvoconducto (Aplica para extranjeros)', 
IdentificationType = 12, 'Permiso especial de Permanencia (Aplica para extranjeros)', 
'')")>
    Public ReadOnly Property IdentificationTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("IdentificationTypeName"))
        End Get
    End Property

    '<NonPersistent()>
    'Public ReadOnly Property IdentificationTypeName() As String
    '    Get
    '        Select Case fIdentificationType
    '            Case 0
    '                Return "Cédula Ciudadanía"
    '            Case 1
    '                Return "Cédula Extranjería"
    '            Case 2
    '                Return "Tarjeta de Identidad"
    '            Case 3
    '                Return "Registro Civil"
    '            Case 4
    '                Return "Pasaporte"
    '            Case 5
    '                Return "Adulto Sin Identificación"
    '            Case 6
    '                Return "Menor Sin Identificación"
    '            Case Else
    '                Return String.Empty
    '        End Select
    '    End Get
    'End Property
    Dim fIdentificacionCityId As Integer
    Public Property IdentificacionCityId() As Integer
        Get
            Return fIdentificacionCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdentificacionCityId", fIdentificacionCityId, value)
        End Set
    End Property
    Dim fIdentificationExpeditionDate As DateTime
    Public Property IdentificationExpeditionDate() As DateTime
        Get
            Return fIdentificationExpeditionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IdentificationExpeditionDate", fIdentificationExpeditionDate, value)
        End Set
    End Property
    Dim fMilitaryCardId As Integer
    Public Property MilitaryCardId() As Integer
        Get
            Return fMilitaryCardId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MilitaryCardId", fMilitaryCardId, value)
        End Set
    End Property
    Dim fMilitaryCardNumber As String
    <Size(10)> _
    Public Property MilitaryCardNumber() As String
        Get
            Return fMilitaryCardNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MilitaryCardNumber", fMilitaryCardNumber, value)
        End Set
    End Property
    Dim fFirstName As String
    <Size(50)> _
    Public Property FirstName() As String
        Get
            Return fFirstName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstName", fFirstName, value)
        End Set
    End Property
    Dim fSecondName As String
    <Size(50)>
    Public Property SecondName() As String
        Get
            Return fSecondName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondName", fSecondName, value)
        End Set
    End Property
    <PersistentAlias("CONCAT(FirstName,' ',ISNULL(SecondName,''))")>
    Public ReadOnly Property Name() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Name"))
        End Get
    End Property
    Dim fFirstLastName As String
    <Size(50)> _
    Public Property FirstLastName() As String
        Get
            Return fFirstLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstLastName", fFirstLastName, value)
        End Set
    End Property
    Dim fSecondLastName As String
    <Size(50)>
    Public Property SecondLastName() As String
        Get
            Return fSecondLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondLastName", fSecondLastName, value)
        End Set
    End Property
    <PersistentAlias("CONCAT(FirstLastName,' ',ISNULL(SecondLastName,''))")>
    Public ReadOnly Property LastName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("LastName"))
        End Get
    End Property
    Dim fBirthDate As DateTime
    Public Property BirthDate() As DateTime
        Get
            Return fBirthDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BirthDate", fBirthDate, value)
        End Set
    End Property
    Dim fBirthCityId As Integer
    Public Property BirthCityId() As Integer
        Get
            Return fBirthCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BirthCityId", fBirthCityId, value)
        End Set
    End Property
    Dim fDeathDate As DateTime
    Public Property DeathDate() As DateTime
        Get
            Return fDeathDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DeathDate", fDeathDate, value)
        End Set
    End Property
    Dim fGender As Byte
    Public Property Gender() As Byte
        Get
            Return fGender
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Gender", fGender, value)
        End Set
    End Property
    Dim fBloodGroup As String
    <Size(2)> _
    Public Property BloodGroup() As String
        Get
            Return fBloodGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BloodGroup", fBloodGroup, value)
        End Set
    End Property
    Dim fRH As Char
    Public Property RH() As Char
        Get
            Return fRH
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RH", fRH, value)
        End Set
    End Property
    Dim fFingerprint() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property Fingerprint() As Byte()
        Get
            Return fFingerprint
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("Fingerprint", fFingerprint, value)
        End Set
    End Property
    Dim fSonNumber As Byte
    Public Property SonNumber() As Byte
        Get
            Return fSonNumber
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SonNumber", fSonNumber, value)
        End Set
    End Property
    Dim fDependents As Byte
    Public Property Dependents() As Byte
        Get
            Return fDependents
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Dependents", fDependents, value)
        End Set
    End Property
    Dim fMaritalStatus As Byte
    Public Property MaritalStatus() As Byte
        Get
            Return fMaritalStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaritalStatus", fMaritalStatus, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("IIF(State = 1,'Activo','Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("CommonThirdPartyReferencesCommonPerson", GetType(CommonThirdPartyXpo))>
    Public ReadOnly Property CommonThirdPartys() As XPCollection(Of CommonThirdPartyXpo)
        Get
            Return GetCollection(Of CommonThirdPartyXpo)("CommonThirdPartys")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
