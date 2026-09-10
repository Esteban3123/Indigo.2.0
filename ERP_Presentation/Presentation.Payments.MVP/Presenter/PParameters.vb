'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PParameters

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IParameters

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IParameters)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los comprobantes contables
    ''' </summary>
    Public Sub Initialize()
        InitializeVoucherCxp()
        InitializeVoucherTransfer()
        InitializeVoucherCreditNotes()
        InitializeVoucherDebitNotes()
        InitializeVoucherAmortization()
    End Sub

    Public Sub InitializeVoucherCxp()
        Using model As New MBusqueda
            Me.View.VoucherTypeCxpXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeVoucherTransfer()
        Using model As New MBusqueda
            Me.View.VoucherTransfersXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeVoucherCreditNotes()
        Using model As New MBusqueda
            Me.View.VoucherCreditNotesXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeVoucherDebitNotes()
        Using model As New MBusqueda
            Me.View.VoucherDebitNotesXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeVoucherAmortization()
        Using model As New MBusqueda
            Me.View.VoucherAmortizationXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

End Class
