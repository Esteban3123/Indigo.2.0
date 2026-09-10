'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PConsignmentTransfer
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IConsignmentTransfer

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IConsignmentTransfer)
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
    ''' Obtiene una secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lee la fecha del servidor
    ''' </summary>
    Public Async Sub LoadDateServer(dateServer As Date)
        Using Model As New MDocumentAccount(Me.View.MyTag)
            If Await Model.ValidatePeriod(dateServer.Month, dateServer.Year) Then
                View.DocumentDate = dateServer
            Else
                View.DocumentDate = Nothing
                View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MonthSelectedClose", "Accounting")
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Lista todas cuentas bancarias de entidades y lo asigna al datasource de cuentas bancarias
    ''' </summary>
    Public Sub InitializeEntityBankAccount()
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigo, True}
            Me.View.EntityBankAccountDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Lista todas las cajas
    ''' </summary>
    Public Sub InitializeCash(ByVal type As Short)
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigoId, type, True}
            Me.View.CashDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Using Model As New MBusqueda
        Me.View.CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

End Class
