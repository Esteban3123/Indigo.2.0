
Imports DevExpress.Xpo

<Persistent("dbo.ViewCitaCanceladaReprogramada")>
Public Class ViewCitaCanceladaReprogramada
    Inherits XPLiteObject
    Dim fCODAUTONU As Long
    <Key(True)>
    Public Property CODAUTONU() As Long
        Get
            Return fCODAUTONU
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("CODAUTONU", fCODAUTONU, value)
        End Set
    End Property
    Dim fCODIGOUSUARIO As String
    <Size(20)>
    Public Property CODIGOUSUARIO() As String
        Get
            Return fCODIGOUSUARIO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOUSUARIO", fCODIGOUSUARIO, value)
        End Set
    End Property
    Dim fFECHA As String
    <Size(25)>
    Public Property FECHA() As String
        Get
            Return fFECHA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FECHA", fFECHA, value)
        End Set
    End Property
    Dim fCODIGOMOTIVO As String
    <Size(4)>
    Public Property CODIGOMOTIVO() As String
        Get
            Return fCODIGOMOTIVO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOMOTIVO", fCODIGOMOTIVO, value)
        End Set
    End Property
    Dim fMOTIVO As String
    <Size(150)>
    Public Property MOTIVO() As String
        Get
            Return fMOTIVO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MOTIVO", fMOTIVO, value)
        End Set
    End Property
    Dim fUSUARIO As String
    <Size(60)>
    Public Property USUARIO() As String
        Get
            Return fUSUARIO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("USUARIO", fUSUARIO, value)
        End Set
    End Property
    Dim fTIPO As Byte
    <Size(60)>
    Public Property TIPO() As Byte
        Get
            Return fTIPO
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TIPO", fTIPO, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
