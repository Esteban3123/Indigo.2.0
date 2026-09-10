'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo Flores
' Created          : 10-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports System.Runtime.CompilerServices
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PSupplier

#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As ISupplier
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ISupplier)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Carga todos los combos
    ''' </summary>
    Public Sub Initialize()
        Using model As New MBusqueda
            Dim filter() As Object = {"5", True}
            Me.View.CitiesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity), DevExpress.Xpo.XPInstantFeedbackSource)
            Me.View.ThirdPartyDatasource = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty), DevExpress.Xpo.XPInstantFeedbackSource)
            Me.View.DistributionLineXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDistributionLine), DevExpress.Xpo.XPInstantFeedbackSource)
            Me.View.PositionXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Position), DevExpress.Xpo.XPInstantFeedbackSource)
        End Using
    End Sub

    Public Sub InitializeSupplierType()
        Using model As New MBusqueda
            Me.View.SupplierTypeXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierTypeByStatusTreeList, True), DevExpress.Xpo.XPCollection)
        End Using
    End Sub

    ''' <summary>
    ''' Carga el datasource de los bancos
    ''' </summary>
    Public Sub InitializeBank()
        Dim modelXPO As New MBusqueda
        Me.View.BankDatasource = CType(modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllBank), XPInstantFeedbackSource)
    End Sub

    Public Async Sub GetSequence()
        Using model As New MSupplier(Me.View.MyTag.ToString())
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la linea de distribucion por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDistributionLineById(Id As Integer) As Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineXpo
        Dim filter As String = "Id = " & Id
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el datasource de las monedas
    ''' </summary>
    Public Sub InitializeCurrency()
        Dim modelXPO As New MBusqueda
        Me.View.CurrencyXpo = TryCast(modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Currency), XPInstantFeedbackSource)
    End Sub
End Class