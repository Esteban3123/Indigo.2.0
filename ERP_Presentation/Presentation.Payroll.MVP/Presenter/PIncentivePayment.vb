'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-01-2014
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
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class PIncentivePayment

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IUnemployedLiquidation
    ''' </summary>
    Private _view As IIncentivePayment

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Lista de Grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupList As List(Of Group)

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance


#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de tipo de vinculación
    ''' </summary>
    ''' <param name="view">Vista de centros de estudio</param>
    Public Sub New(ByRef view As IIncentivePayment)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para inicializar controles en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function initialize() As Task
        'llenar empresas y empleados en controles
        Dim modelBusqueda As Presentation.Controls.MVP.MBusqueda = New Controls.MVP.MBusqueda()
        '       Me._view.datasourceCompany = modelBusqueda.ConsultarEntidades(eDataSource.CompanyPayroll)
        Await LoadYears()
    End Function

    ''' <summary>
    ''' Metodo para cargar la rejilla con los grupos de los conceptos.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function Load_Group() As Task
        'Genero una lista de todos los grupos
        Using Model As New MGroups(MGroups.TAG)
            GroupList = Await Model.ListAllGroupsAsync
        End Using

        _view.datasourceGroups = GroupList
        'Dim concept As Concept = _view.ConceptObject

        '_view.DatasourceConceptGroup = concept.ConceptGroup
    End Function

    ''' <summary>
    ''' Carga los años en el combo box de periodo, y establece si hay nominas liquidadas para habilitar el frontal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadYears() As Task
        'llenar lista de años para el control de periodo
        Using modelLiquidation As New MPayrollLiquidation
            Dim YearMin As Date
            Dim YearMax As Date
            YearMin = Await modelLiquidation.GetLiquidationMinDateAsync()
            YearMax = Await modelLiquidation.GetLiquidationMaxDateAsync()
            If YearMin.Year = 1 OrElse YearMax.Year = 1 Then
                YearMin = GroupList.FirstOrDefault.NextDateLiquidation
                YearMax = GroupList.FirstOrDefault.NextDateLiquidation
            End If
            If YearMin.Year <> 1 Then
                Me._view.PeriodControl.Properties.Items.Clear()
                For i As Integer = YearMax.Year To YearMin.Year Step -1
                    Me._view.PeriodControl.Properties.Items.Add(i)
                Next
                Me._view.PeriodControl.SelectedIndex = 0
            End If
        End Using
    End Function
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

#End Region

End Class
