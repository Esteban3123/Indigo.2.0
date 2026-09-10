Imports Infrastructure.CrossCutting.Base

Public Interface IDocumentSupportReport

    ''' <summary>
    ''' Texto del título del reporte
    ''' </summary>
    WriteOnly Property TitleText As String

    ''' <summary>
    ''' Texto para la etiqueta de identificación
    ''' </summary>
    WriteOnly Property IdentificationLabel As String

    ''' <summary>
    ''' Texto para la etiqueta de receptor
    ''' </summary>
    WriteOnly Property ReceiverLabel As String

    ''' <summary>
    ''' Indica si se debe mostrar la retención de ICA
    ''' </summary>
    WriteOnly Property ShowIcaRetention As Boolean

    ''' <summary>
    ''' Indica si se debe mostrar la información de autorización
    ''' </summary>
    WriteOnly Property ShowAuthorization As Boolean

End Interface

Public Class SupportDocumentCustomReportByLocation

#Region "Properties"
    Dim View As IDocumentSupportReport

    Public Property TitleText As String

    Public Property IdentificationLabel As String

    Public Property ReceiverLabel As String

    Public Property ShowIcaRetention As Boolean

    Public Property ShowAuthorization As Boolean

#End Region

    Public Sub New(ByRef iView As IDocumentSupportReport)
        View = iView

        Select Case SessionValues.Instance.Culture.Name
            Case "es-CR"
                Me.TitleText = "FACTURA ELECTRÓNICA DE COMPRA"
                Me.IdentificationLabel = "Identificación: "
                Me.ReceiverLabel = "Emisor: "
                Me.ShowIcaRetention = False
                Me.ShowAuthorization = False
            Case Else
                Me.TitleText = "DOCUMENTO SOPORTE EN ADQUISICIONES EFECTUADAS A SUJETOS NO OBLIDAGOS A FACTURAR"
                Me.IdentificationLabel = "Nit: "
                Me.ReceiverLabel = "Tercero: "
                Me.ShowIcaRetention = True
                Me.ShowAuthorization = True
        End Select
    End Sub

    Public Sub ApplyLocalization()
        View.TitleText = Me.TitleText
        View.IdentificationLabel = Me.IdentificationLabel
        View.ReceiverLabel = Me.ReceiverLabel
        View.ShowIcaRetention = Me.ShowIcaRetention
        View.ShowAuthorization = Me.ShowAuthorization
    End Sub

End Class
