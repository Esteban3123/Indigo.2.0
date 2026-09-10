Imports DevExpress.Xpo

''' <summary>
''' Autor: HECTOR RODRIGUEZ
''' Date : 12/07/2019
''' Use  : lista componentes sanguineos
''' </summary>
<Persistent("dbo.ADPARAMET")> _
Partial Public Class ADPARAMETXpo
    Inherits XPLiteObject

    Dim fCODCENATE As String
    <Key()> _
    <Size(10)> _
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fSERRASANTI As String
    <Size(10)> _
    Public Property SERRASANTI() As String
        Get
            Return fSERRASANTI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SERRASANTI", fSERRASANTI, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
