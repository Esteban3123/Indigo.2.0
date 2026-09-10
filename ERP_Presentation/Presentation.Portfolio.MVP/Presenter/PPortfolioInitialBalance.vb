'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
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

Public Class PPortfolioInitialBalance

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IPortfolioInitialBalance

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IPortfolioInitialBalance)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await _view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(_view.MyTag)
            _view.Sequence = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO()
        'Using model As New MBusqueda
        Me._view.CostCenterXPO = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub
    ''' <summary>
    ''' inicializa los clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me._view.CustomerXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Initializes the account accounting.
    ''' </summary>
    Public Sub InitializeMainAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me._view.MainAccountXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
#End Region
End Class
