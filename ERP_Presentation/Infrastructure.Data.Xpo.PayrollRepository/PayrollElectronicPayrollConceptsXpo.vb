Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.ElectronicPayrollConcepts")>
Public Class PayrollElectronicPayrollConceptsXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property



    Dim fConceptType As Integer
    <Persistent("ConceptType")>
    Public Property ConceptType() As Integer
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConceptType", fConceptType, value)
        End Set
    End Property

    Dim fCreationUser As Integer
    <Persistent("CreationUser")>
    Public Property CreationUser() As Integer
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    <Persistent("CreationDate")>
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As Integer?
    <Persistent("ModificationUser")>
    Public Property ModificationUser() As Integer?
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime?
    <Persistent("ModificationDate")>
    Public Property ModificationDate() As DateTime?
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fState As Integer
    <Persistent("State")>
    Public Property State() As Integer
        Get
            Return fState
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("State", fState, value)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de solo lectura que concatena el código y el nombre
    ''' </summary>
    ''' <returns>Concatenación de Code y Name</returns>
    <NonPersistent>
    Public ReadOnly Property CodeName() As String
        Get
            If String.IsNullOrEmpty(fCode) AndAlso String.IsNullOrEmpty(fName) Then
                Return String.Empty
            ElseIf String.IsNullOrEmpty(fCode) Then
                Return fName
            ElseIf String.IsNullOrEmpty(fName) Then
                Return fCode
            Else
                Return String.Format("{0} - {1}", fCode, fName)
            End If
        End Get
    End Property


    ''' <summary>
    ''' Nombre del tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    <PersistentAlias("Iif(ConceptType = 1, 'Devengados', Iif(ConceptType = 2, 'Deducciones', 'No Aplica'))")>
    Public ReadOnly Property ConceptTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ConceptTypeName"))
        End Get
    End Property

    ''' <summary>
    ''' Estado concepto nomina electronica
    ''' </summary>
    ''' <returns></returns>
    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
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
