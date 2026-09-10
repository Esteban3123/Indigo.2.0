Imports System.Runtime.Serialization

Partial Public Class Employee

    <DataMember()>
    Property Nit As String

    <DataMember()>
    Property EmployeeName As String

    Private _pensionaryTypeName As String

    <DataMember()> _
    Public Property PensionaryTypeName As String
        Get
            Return Me._pensionaryTypeName
        End Get
        Set(value As String)
            Me._pensionaryTypeName = value
        End Set
    End Property

    Public ReadOnly Property BasicSalary As Decimal
        Get
            Dim salary As Decimal = 0D
            If Me.Contract IsNot Nothing AndAlso Me.Contract.Count > 0 Then
                Dim contract As Contract = Me.Contract.Where(Function(c) c.Valid).FirstOrDefault()
                If contract IsNot Nothing Then
                    salary = contract.BasicSalary
                End If
            End If
            Return salary
        End Get
    End Property

    Private _apply As Boolean = False

    <DataMember()> _
	Public Property Apply() As Boolean
    Get
        Return _apply
    End Get
    Set(ByVal value As Boolean)
        If Not Equals(_apply, value) Then
            _apply = value
            OnPropertyChanged("Apply")
        End If
    End Set
    End Property

    Private _CodeNameConcatenated As String

    <DataMember()>
    Public Property CodeNameConcatenated As String
        Get
            Return Me._CodeNameConcatenated
        End Get
        Set(value As String)
            Me._CodeNameConcatenated = value
        End Set
    End Property

End Class
