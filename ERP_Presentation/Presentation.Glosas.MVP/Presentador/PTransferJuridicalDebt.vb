'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal de traslado cobro jurídico
''' </summary>
Public Class PTransferJuridicalDebt

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de traslado cobro jurídico
    ''' </summary>
    Private _view As ITransferJuridicalDebt
    ''' <summary>
    ''' Objeto de la traslado cobro jurídico
    ''' </summary>
    Private _juridical As Object
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As ITransferJuridicalDebt)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de unidad de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnit()
        Using model As New MBusqueda
            Me._view.FilingUnitTargetXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatusCollection, True)
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub ListLawyerActive
        Me._view.Lawyers = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.ListLawyerActive()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub ListDemandStatusActive
        Me._view.DemandStatusDataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.ListDemandStatusActive()
    End Sub

#End Region

End Class
