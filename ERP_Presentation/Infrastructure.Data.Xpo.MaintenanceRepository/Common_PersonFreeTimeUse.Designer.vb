Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.PersonFreeTimeUse")>
Partial Public Class Common_PersonFreeTimeUse
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
        <Indexed(Name:="IX_FreeTimeUse", Unique:=True)>
        Public Property PersonId() As Integer
            Get
                Return fPersonId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("PersonId", fPersonId, value)
            End Set
        End Property
        Dim fFreeTimeUseId As Payroll_FreeTimeUse
        <Association("Common_PersonFreeTimeUseReferencesPayroll_FreeTimeUse")>
        Public Property FreeTimeUseId() As Payroll_FreeTimeUse
            Get
                Return fFreeTimeUseId
            End Get
            Set(ByVal value As Payroll_FreeTimeUse)
                SetPropertyValue(Of Payroll_FreeTimeUse)("FreeTimeUseId", fFreeTimeUseId, value)
            End Set
        End Property
End Class