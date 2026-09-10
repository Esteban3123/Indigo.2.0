'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
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
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de liquidación de cesantías
''' </summary>
Public Class PUnemployedLiquidation

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IUnemployedLiquidation
    ''' </summary>
    Private _view As IUnemployedLiquidation

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance


#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de tipo de vinculación
    ''' </summary>
    ''' <param name="view">Vista de centros de estudio</param>
    Public Sub New(ByRef view As IUnemployedLiquidation)
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
        Me._view.datasourceCompany = XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetCompanySession(_indigo.IndigoCompanyNit)
        Me._view.datasourceEmployee = XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetAllEmployees()
        Await LoadYears()
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
            If YearMin.Year <> 1 Then
                Me._view.PeriodControl.Properties.Items.Clear()
                For i As Integer = YearMax.Year To YearMin.Year Step -1
                    Me._view.PeriodControl.Properties.Items.Add(i)
                Next
                Me._view.PeriodControl.SelectedIndex = 0
                Me._view.ActionsOnControls = True
            Else
                Me._view.ActionsOnControls = False
            End If
        End Using
    End Function
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function
#End Region

End Class
