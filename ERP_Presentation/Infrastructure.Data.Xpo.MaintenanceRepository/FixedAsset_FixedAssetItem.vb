Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

Partial Public Class FixedAsset_FixedAssetItem

    <PersistentAlias("concat(Code, ' - ', Description)")>
    Public ReadOnly Property CodeDescription As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class