'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/05/2014
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
Imports Presentation.Maintenance.MVP

#End Region

Public Class PPopupDeferredCausation

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPopupDeferredCausation

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
    Public Sub New(ByRef iview As IPopupDeferredCausation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga el datasource del search de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadSearchLookUpMainAccount()
        Using model As New MBusqueda
            Dim filter() As Object = {5, True}
            View.MainAccountXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el datasource del search de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadSearchLookUpCostCenter()
        'Using model As New MBusqueda
        View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar los repositorySearchLookUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadRepositorySearchLookUp()
        Using model As New MBusqueda
            Dim filter() As Object = {5, True}
            View.MainAccountRepositoryXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            View.CostCenterRepositoryXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el datasource del search de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadSearchLookUpThirdParty()
        Using model As New MBusqueda
            View.ThirdPartyXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el tercero por medio del id del proveedor
    ''' </summary>
    ''' <param name="IdSupplier"></param>
    ''' <remarks></remarks>
    Public Sub InitializeThirdPartyByIdSupplier(ByVal IdSupplier As Integer)
        Using model As New MSupplier("")
            Me.View.ThirdParty = model.GetThirdPartyByIdSupplier(IdSupplier)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el tercero por su id
    ''' </summary>
    ''' <param name="idThirdParty"></param>
    ''' <remarks></remarks>
    Public Sub InitializeThirdPartyById(ByVal idThirdParty As Integer)
        Using model As New MThirdParty("")
            Me.View.ThirdPartyCommon = model.GetThirdPartyByIdSimple(idThirdParty)
        End Using
    End Sub

End Class
