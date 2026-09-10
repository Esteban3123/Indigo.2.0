Imports DevExpress.Xpo

''' <summary>
''' Autor: HECTOR RODRIGUEZ
''' Date : 08/07/2019
''' Use  : lista componentes sanguineos
''' </summary>
<Persistent("dbo.HCCOMSAN")> _
Partial Public Class HCCOMSANXpo
    Inherits XPLiteObject

    Dim fID As Integer
    <Key()> _
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    Dim fCODCOMSAM As String
    <Size(20)>
    Public Property CODCOMSAM() As String
        Get
            Return fCODCOMSAM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCOMSAM", fCODCOMSAM, value)
        End Set
    End Property

    Dim fDESCOMSAM As String
    <Size(200)>
    Public Property DESCOMSAM() As String
        Get
            Return fDESCOMSAM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESCOMSAM", fDESCOMSAM, value)
        End Set
    End Property

    Dim fVALIDAHEMOCLA As Integer
    Public Property VALIDAHEMOCLA() As Integer
        Get
            Return fVALIDAHEMOCLA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VALIDAHEMOCLA", fVALIDAHEMOCLA, value)
        End Set
    End Property

    <PersistentAlias("Concat(Trim(CODCOMSAM), ' - ', Trim(DESCOMSAM))")>
    Public ReadOnly Property CodigoDescripcion As String
        Get
            Return Convert.ToString(EvaluateAlias("CodigoDescripcion"))
        End Get
    End Property

#Region "Associaton"
    <Association("HCCOMSANReferencesAppointmentHemocomponentsXpo", GetType(AppointmentHemocomponentsXpo))>
    Public ReadOnly Property AppointmentHemocomponentsXpo() As XPCollection(Of AppointmentHemocomponentsXpo)
        Get
            Return GetCollection(Of AppointmentHemocomponentsXpo)("AppointmentHemocomponentsXpo")
        End Get
    End Property

    <Association("HCCOMSANReferencesHCORHEMBOLXpo", GetType(HCORHEMBOLXpo))>
    Public ReadOnly Property HCORHEMBOLXpo() As XPCollection(Of HCORHEMBOLXpo)
        Get
            Return GetCollection(Of HCORHEMBOLXpo)("HCORHEMBOLXpo")
        End Get
    End Property


    <Association("Reference_HCCOMSANDXpo", GetType(HCCOMSANDXpo))>
    Public ReadOnly Property HCCOMSANDXpo() As XPCollection(Of HCCOMSANDXpo)
        Get
            Return GetCollection(Of HCCOMSANDXpo)("HCCOMSANDXpo")
        End Get
    End Property
#End Region

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
