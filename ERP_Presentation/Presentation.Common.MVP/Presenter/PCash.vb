'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 14-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
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
''' Presentador del frontal Cajas
''' </summary>
Public Class PCash

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICash

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICash)
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
    ''' Initializes the account accounting.
    ''' </summary>
    Public Sub InitializeAccountAccounting()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountAccountingDatasource = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the user.
    ''' </summary>
    Public Sub InitializeUser()
        Using Model As New MCashRegister(Me.View.MyTag)
            Me.View.UserDatasource = Model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub


    ''' <summary>
    ''' Initializes the cost center.
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Using ModelXpo As New MBusqueda
        Me.View.CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True) ' ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia y la establece
    ''' </summary>
    Public Async Sub GetSequence()
        Using ModelCommonTreasury As New MCashRegister(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the third party.
    ''' </summary>
    Public Sub InitializeThirdParty()
        Me.View.ThirdPartyDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

End Class
