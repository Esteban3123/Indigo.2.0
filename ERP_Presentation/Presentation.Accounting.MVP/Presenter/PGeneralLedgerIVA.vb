'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Jeisson Herrera Peña
' Created          : 13-04-2015
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

Public Class PGeneralLedgerIVA

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista(Interfaz) del Funcional
    ''' </summary>
    Private _view As IGenerealLedgerIVA

    ''' <summary>
    ''' Instancia de los valores de Sesión
    ''' </summary>
    Private _sessionValues As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Instancia de la vista del Funcional</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef view As IGenerealLedgerIVA)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._sessionValues = SessionValues.Instance
            Me._view = view
        End If

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numérica asignada al Funcional
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequence()
        Using Model As New MGeneralLedgerIVA(Me._view.MyTag)
            Me._view.Sequence = Await Model.GetSequence()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas para IVA Compra/servicio
    ''' </summary>
    Public Sub Initialize()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me._view.AccountsXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas IVA ventas
    ''' </summary>
    Public Sub InitializeSale()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me._view.AccountSaleXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas control fiscal debito
    ''' </summary>
    Public Sub InitializeDebit()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me._view.AccountDebitControlFiscalXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas control fiscal debito
    ''' </summary>
    Public Sub InitializeCredit()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me._view.AccountCreditControlFiscalXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
#End Region

End Class
