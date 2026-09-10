'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 13-08-2013
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
Imports Domain.Payroll
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo
#End Region

''' <summary>
''' Presentador del frontal
''' </summary>
''' <remarks></remarks>
Public Class PPayrollLiquidation

#Region "Variables and Constructors"


    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IConcept
    ''' </summary>
    Private _view As IPayrollLiquidation

    Dim GroupList As List(Of Group)


    Public Sub New(ByRef iview As IPayrollLiquidation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para cargar la rejilla con los grupos de los conceptos.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function Load_Group() As Task
        'Genero una lista de todos los grupos
        Using Model As New MGroups(MGroups.TAG)
            GroupList = Await Model.ListGroupsByStatusAsync(True)
        End Using

        _view.DatasourceGroup = GroupList
        'Dim concept As Concept = _view.ConceptObject

        '_view.DatasourceConceptGroup = concept.ConceptGroup
    End Function
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtenemos la fecha de la ultima liquidación confirmada de un empleado
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    Public Function GetLastEmployeeDateLiquidated(GroupId As Integer, EmployeeId As Integer) As Date?
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetLastEmployeeDateLiquidated(GroupId, EmployeeId)
    End Function
#End Region

End Class
