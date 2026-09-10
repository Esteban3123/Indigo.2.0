'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PManualConcept
#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IManualConcept

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IManualConcept)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los controles del formulario
    ''' </summary>
    Public Async Function ConceptsDatasource() As Task
        Using model As New MConcept(MConcept.TAG)
            Dim ConceptDatasource = Await model.GetConceptByConceptClassAsync(New List(Of String)({"003", "004", "007", "010", "011", "001", "005", "012", "013", "020", "042", "043", "044", "045", "050", "055", "017", "014", "052", "051", "065"}))
            Me.View.Concept_Datasource = ConceptDatasource.Where(Function(x) x.Code <> "001" And x.Code <> "701").ToList()
        End Using
    End Function

    ''' <summary>
    ''' Inicializa el datasource de los controles del formulario
    ''' </summary>
    Public Function RetentionConceptsDatasource() As Task
        Using modelBusqueda As New MBusqueda
            Me.View.RetentionConcept_Datasource = modelBusqueda.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Function

    ''' <summary>
    ''' Metodo que carga el datasource de los empleados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EmployeeDatasource()
        Using modelBusqueda As New MBusqueda
            'Dim EmployeeDataSource = modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
            Me.View.Employee_Datasource = modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
        End Using
    End Sub
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function
#End Region
End Class
''' <summary>
''' Enumeración que contiene los nombres estados de un concepto manual
''' </summary>
''' <remarks></remarks>
Public Enum EStatusManualConceps As Byte
    Active = 1
    Finalized = 2
    Suspended = 3
End Enum

''' <summary>
''' Enumeración que contiene los tipos de liquidacion
''' </summary>
''' <remarks></remarks>
Public Enum EGroupLiquidationType
    ''' <summary>
    ''' Liquidacion Mensual ->1 en db
    ''' </summary>
    ''' <remarks></remarks>
    Monthly = 1
    ''' <summary>
    ''' Liquidacion quincenal ->2 en db
    ''' </summary>
    ''' <remarks></remarks>
    Fortnightly = 2
End Enum