Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.PersonDiagnosedDisease")>
Partial Public Class Common_PersonDiagnosedDisease
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
        <Indexed(Name:="IX_DiagnosedDisease", Unique:=True)>
        Public Property PersonId() As Integer
            Get
                Return fPersonId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("PersonId", fPersonId, value)
            End Set
        End Property
        Dim fDiagnosedDiseaseId As Payroll_DiagnosedDisease
        <Association("Common_PersonDiagnosedDiseaseReferencesPayroll_DiagnosedDisease")>
        Public Property DiagnosedDiseaseId() As Payroll_DiagnosedDisease
            Get
                Return fDiagnosedDiseaseId
            End Get
            Set(ByVal value As Payroll_DiagnosedDisease)
                SetPropertyValue(Of Payroll_DiagnosedDisease)("DiagnosedDiseaseId", fDiagnosedDiseaseId, value)
            End Set
        End Property
End Class