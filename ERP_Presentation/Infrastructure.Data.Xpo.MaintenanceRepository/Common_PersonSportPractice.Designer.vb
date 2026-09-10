Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.PersonSportPractice")>
Partial Public Class Common_PersonSportPractice
        Inherits XPLiteObject
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
        Dim fPersonId As Integer
        <Indexed(Name:="IX_SportPractice", Unique:=True)>
        Public Property PersonId() As Integer
            Get
                Return fPersonId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("PersonId", fPersonId, value)
            End Set
        End Property
        Dim fSportPracticeId As Payroll_SportPractice
        <Association("Common_PersonSportPracticeReferencesPayroll_SportPractice")>
        Public Property SportPracticeId() As Payroll_SportPractice
            Get
                Return fSportPracticeId
            End Get
            Set(ByVal value As Payroll_SportPractice)
                SetPropertyValue(Of Payroll_SportPractice)("SportPracticeId", fSportPracticeId, value)
            End Set
        End Property
End Class