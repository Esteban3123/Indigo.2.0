'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-10
' Description      : Presentador del frontal de parametros de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.CompilerServices
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports  Domain.Entities
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal de parametros de glosas
''' </summary>
Public Class PTimeParameters

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de autorizacion de centros de atencion
    ''' </summary>
    Private _view As ITimeParameters
    ''' <summary>
    ''' Objeto de los parametros
    ''' </summary>
    Private _authorization As Object
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance


    Dim filter() As Object = {5, True}
#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As ITimeParameters)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Realiza la carga de los datos al iniciar el frontal
    ''' </summary>
    Public Sub LoadDataAsync()
    End Sub

#End Region

#Region "Initialize JournalVoucherType"

    Public Sub InitializerJournalVoucherType()
        ' InitializeRadicationJournalVoucherType()
        InitializeReceptionObjectionJournalVoucherType()
        InitializeConciliationJournalVoucherType()
        InitializeDevolutionJournalVoucherType()
        InitializeTransferLegalJournalVoucherType()
    End Sub

    'Public Sub InitializeRadicationJournalVoucherType()
    '    Using model As New MBusqueda
    '        Me._view.RadicationJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
    '    End Using
    'End Sub

    Public Sub InitializeReceptionObjectionJournalVoucherType()
        Using model As New MBusqueda
            Me._view.ReceptionObjectionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeConciliationJournalVoucherType()
        Using model As New MBusqueda
            Me._view.ConciliationJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeDevolutionJournalVoucherType()
        Using model As New MBusqueda
            Me._view.DevolutionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeTransferLegalJournalVoucherType()
        Using model As New MBusqueda
            Me._view.TransferLegalJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeThirdParty()
        Using model As New MBusqueda
            Me._view.DataSourceThird = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeCostCenter()
        'Using model As New MBusqueda
        Me._view.DataSourceCostCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

#End Region

#Region "Initialize PortfolioNoteConcept"

    Public Sub InitializePortfolioNoteConcept()
        InitializeGeneralGlossConceptNote()
        InitializeDetailedGlossConceptNote()
        InitializePreviousLifetimesConceptNote()
    End Sub

    Public Sub InitializeGeneralGlossConceptNote()
        Dim filterConceptNote() As Object = {2, True}
        Using model As New MBusqueda
            Me._view.GeneralGlossConceptNoteIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioNoteConceptByNoteType, filterConceptNote)
        End Using
    End Sub

    Public Sub InitializeDetailedGlossConceptNote()
        Dim filterConceptNote() As Object = {1, True}
        Using model As New MBusqueda
            Me._view.DetailedGlossConceptNoteIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioNoteConceptByNoteType, filterConceptNote)
        End Using
    End Sub

    Public Sub InitializePreviousLifetimesConceptNote()
        Dim filterConceptNote() As Object = {2, True}
        Using model As New MBusqueda
            Me._view.PreviousLifetimesConceptNoteIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioNoteConceptByNoteType, filterConceptNote)
        End Using
    End Sub

#End Region

End Class
