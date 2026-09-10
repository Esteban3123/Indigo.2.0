Imports DevExpress.Xpo

<Persistent("Payroll.ElectronicPayrollDetail")>
Public Class ElectronicPayrollDetailXpo
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

    Dim fElectronicPayrollId As ElectronicPayrollXpo
    <Association("ElectronicPayrollDetail_References_ElectronicPayroll")>
    Public Property ElectronicPayrollId() As ElectronicPayrollXpo
        Get
            Return fElectronicPayrollId
        End Get
        Set(ByVal value As ElectronicPayrollXpo)
            SetPropertyValue(Of ElectronicPayrollXpo)("ElectronicPayrollId", fElectronicPayrollId, value)
        End Set
    End Property

    Dim fDestination As Byte
    Public Property Destination() As Byte
        Get
            Return fDestination
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Destination", fDestination, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fResponse As String
    Public Property Response() As String
        Get
            Return fResponse
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Response", fResponse, value)
        End Set
    End Property

    Dim fComments As String
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comments", fComments, value)
        End Set
    End Property

    Dim fResponseData As String
    Public Property ResponseData() As String
        Get
            Return fResponseData
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponseData", fResponseData, value)
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

#End Region

#Region "Attributes Extends"

    <PersistentAlias("Iif(Destination = 0, 'Validacion previa a la generacion del XML', Destination = 1, 'Envio Documento Electronico a la DIAN', Destination = 2, 'Validacion del Documento Enviado', '')")>
    Public ReadOnly Property DestinationName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DestinationName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 0, 'Fallido', Status = 1, 'Exitoso', '')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class