'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal de talento humano
''' </summary>
Public Class PEmployee

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IEmployee

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEmployee)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Lista de las rentas Exentas
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Function ListExemptIncome(ThirdPartyId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListExemptIncome(ThirdPartyId)
    End Function

    ''' <summary>
    ''' Lista del detalle (ASYNC VERSION)
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Async Function ListExemptIncomeDetailAsync(ByVal ThirdPartyId As Integer) As Task(Of List(Of CommonRepository.CommonExemptIncomeDetailXpo))
        Return Await Task.Run(Function()
                                  Dim filter As String = "ThirdPartyId=" & ThirdPartyId & " AND RegisterStatus = 'C' AND ExemptIncomeValue > 0"
                                  Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollectionAsList(Of CommonRepository.CommonExemptIncomeDetailXpo)(Nothing, filter)
                              End Function)
    End Function

    ''' <summary>
    ''' Lista del detalle filtrado por año (carga bajo demanda)
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Async Function ListExemptIncomeDetailByYearAsync(ByVal ThirdPartyId As Integer, ByVal Year As Integer) As Task(Of List(Of CommonRepository.CommonExemptIncomeDetailXpo))
        Return Await Task.Run(Function()
                                  Dim filter As String = "ThirdPartyId=" & ThirdPartyId & " AND YearLiquidated=" & Year & " AND RegisterStatus = 'C' AND ExemptIncomeValue > 0"
                                  Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollectionAsList(Of CommonRepository.CommonExemptIncomeDetailXpo)(Nothing, filter)
                              End Function)
    End Function

    ''' <summary>
    ''' Funcio que trae los registros de la tabla exempt income
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Function ListExemptIncome1(ByVal ThirdPartyId As Integer) As List(Of CommonRepository.CommonExemptIncomeXpo)
        Dim filter As String = "ThirdPartyId=" & ThirdPartyId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollectionAsList(Of CommonRepository.CommonExemptIncomeXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Inicializa los controles necesarios
    ''' </summary>
    Public Sub Initializes()
        Using modelo As New MBusqueda()
            'tipo de estudio
            View.StudyTypesDataSourceXPO = modelo.ConsultarEntidades(eDataSource.StudyType)
            'Centro de estudio
            View.StudyCentersDataSourceXPO = modelo.ConsultarEntidades(eDataSource.CenterStudy)
            'unidades de tiempo
            View.UnitsTimeDataSourceXPO = modelo.ConsultarEntidades(eDataSource.TimeUnit)
            'Ciudades de estudio
            View.StudyCitiesDataSourceXPO = modelo.ConsultarEntidades(eDataSource.AllCity)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que consulta los tipo de identificacion del EHR y se los asigna al datasource del control
    ''' </summary>
    Public Function InitializaIdentificationType()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListADTIPOIDENTIFICAxpoActive()
    End Function

    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

End Class
