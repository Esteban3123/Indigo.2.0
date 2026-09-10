'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/03/2015
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
Imports Presentation.Controls.MVP

#End Region

Public Class PParametersAccounting

#Region "Fields"
    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IParameterAccountig

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues

    ''' <summary>
    ''' Filtro de las cuentas contables
    ''' </summary>
    ''' <remarks></remarks>
    Dim filter() As Object = {5, True}

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IParameterAccountig)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    Public Sub InitializeIdDeficitAccount()
        Using mSearch As New MBusqueda
            Me._view.IdDeficitAccountXpo = mSearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeIdSuperavitAccount()
        Using mSearch As New MBusqueda
            Me._view.IdSuperavitAccountXpo = mSearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeIdUtilityAccount()
        Using mSearch As New MBusqueda
            Me._view.IdUtilityAccountXpo = mSearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeIdCloseDocument()
        Using mSearch As New MBusqueda
            Me._view.IdCloseDocumentXpo = mSearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListJournalVoucherByState, "True")
        End Using
    End Sub

    Public Sub InitializeIdDian()
        Using mSearch As New MBusqueda
            Me._view.IdDianXpo = mSearch.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeIdDistrictTreasury()
        Using mSearch As New MBusqueda
            Me._view.IdDistrictTreasuryXpo = mSearch.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeIdApprovalDocument()
        Using mSearch As New MBusqueda
            Me._view.IdApprovalDocumentXpo = mSearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListJournalVoucherByState, "True")
        End Using
    End Sub

    Public Sub InitializeIdMovementDocument()
        Using mSearch As New MBusqueda
            Me._view.IdMovementDocumentXpo = mSearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListJournalVoucherByState, "True")
        End Using
    End Sub

    Public Sub InitializeIdIvaRetentionConcept()
        Using mSearch As New MBusqueda
            Me._view.IdIvaRetentionConceptXpo = mSearch.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub

    Public Sub InitializeIdIcaRetentionConcept()
        Using mSearch As New MBusqueda
            Me._view.IdIcaRetentionConceptXpo = mSearch.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub

    Public Sub InitializeIdSourceRetentionConcept()
        Using mSearch As New MBusqueda
            Me._view.IdSourceRetentionConceptXpo = mSearch.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub

#End Region

End Class