'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 16-06-2014
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

Public Class PCashReceipts
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICashReceipts

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICashReceipts)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' inicializa las cuentas contables
    ''' </summary>
    Public Sub InitializeAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigo, True}
            Me.View.AccountXPO = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa las cajas
    ''' </summary>
    Public Sub InitializeCashXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigoId, 2, True}
            Me.View.CashXPO = model.ConsultarEntidades(eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO()
        'Using model As New MBusqueda
        Me.View.CostCenterXPO = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' inicializa los terceros
    ''' </summary>
    Public Sub InitializeThirdPartyXPO()
        Using model As New MBusqueda
            Me.View.ThirdPartyXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub
    ''' <summary>
    ''' inicializa los responsables de pago (Son los mismos terceros)
    ''' </summary>
    Public Sub InitializeResponsiblePaymentXPO()
        Using model As New MBusqueda
            Me.View.ResponsiblePaymentXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MCommonTreasury(CStr(Me.View.MyTag))
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub
End Class
