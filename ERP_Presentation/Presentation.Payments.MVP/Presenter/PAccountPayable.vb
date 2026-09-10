'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.CloudAgent
Imports Domain.Common.Entities

#End Region

Public Class PAccountPayable

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAccountsPayable

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAccountsPayable)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las provedores
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim model As New MBusqueda
        Me.View.SuppliersDistributionLinesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia numérica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Lista las causaciones diferidas que tiene cada factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsultListDeferredCausation(ByVal idAccountPayable As Integer)
        Using model As New MDeferredCausation("")
            View._listAddDeferredCausation = model.GetDeferredCausationByIdAccountPayable(idAccountPayable)
        End Using
    End Sub

    ''' <summary>
    ''' Lista las causaciones diferidas que tiene cada factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ConsultListDeferredCausationByCode(ByVal code As String) As Task
        Using model As New MDeferredCausation("")
            View._listAddDeferredCausation = Await model.GetDeferredCausationByAccountPayableCode(code)
        End Using
    End Function

    ''' <summary>
    ''' Inicializa el tipo de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierType()
        Using model As New MBusqueda
            Me.View.SupplierTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierTypeByStatusTreeList, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de unidad de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnit()
        Using model As New MBusqueda
            Me.View.FilingUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatusCollection, True)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que consulta los permisos que tiene el formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Permission(ByVal list As Dictionary(Of Integer, String)) As List(Of Integer)
        Dim listPermission As New List(Of Integer)
        Dim keyCausation = list.ToList.FindAll(Function(item) item.Key = 51).Count
        Dim keyConfirmation = list.ToList.FindAll(Function(item) item.Key = 7).Count
        listPermission.Add(keyCausation)
        listPermission.Add(keyConfirmation)
        Return listPermission
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSupplierDistributionLineById(Id As Integer) As CommonSuppliersDistibutionLineXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetXPOObject(Of CommonSuppliersDistibutionLineXpo)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene el proovedor por medio del Id
    ''' </summary>
    Public Function GetSupplierById(Id As Integer) As PaymentsRepository.Maintenance_Supplier
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetXPOObject(Of PaymentsRepository.Maintenance_Supplier)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetXPOObject(Of PaymentsSettingPaymentsXpo)(filter)
    End Function


    ''' <summary>
    ''' Lista los conceptos de las cuentas contables
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptById(Id As Integer) As PaymentsAccountPayableConceptsXpoP
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetXPOObject(Of PaymentsAccountPayableConceptsXpoP)(filter)
    End Function

#End Region

End Class
