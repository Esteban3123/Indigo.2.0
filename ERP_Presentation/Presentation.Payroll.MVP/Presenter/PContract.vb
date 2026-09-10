'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 17-07-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent


#End Region

''' <summary>
''' Presentador del frontal de contratos
''' </summary>
Public Class PContract

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContract

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IContract)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Metodo que verifica si el contrato tiene nominas confirmadas para bloquear las acciones 
    ''' </summary>
    ''' <param name="contractId">Id del contrato a verificar</param>
    ''' <returns>Verdadero si existen nominas confirmadas, falso en caso contrario</returns>
    Public Function CheckConfirmedLiquidationByContractId(contractId As Integer) As Boolean
        Return False
    End Function

    ''' <summary>
    ''' Metodo para obtener los parametros del tipo de contrato seleccionado
    ''' </summary>
    ''' <param name="id">El id del tipo de contrato</param>
    ''' <returns>La entidad de tipo de contrato</returns>
    Public Async Function GetContractTypeByIdAsync(id As Integer) As Task(Of ContractType)
        Dim contractType As ContractType
        Using model As New MContractType(MContractType.TAG)
            contractType = Await model.GetContractTypeByIdAsync(id)
        End Using
        Return contractType
    End Function

    ''' <summary>
    ''' Metodo para obtener los parametros del grupo
    ''' </summary>
    ''' <param name="id">El id del grupo</param>
    ''' <returns>La entidad de grupo</returns>
    Public Async Function GetGroupByIdAsync(id As Integer) As Task(Of Group)
        Dim group As Group
        Using model As New MGroups(MGroups.TAG)
            group = Await model.GetGroupByIdAsync(id)
        End Using
        Return group
    End Function

    ''' <summary>
    ''' Metodo para obtener los grupos por tipo de contrato
    ''' </summary>
    ''' <param name="contractClass">Id de la clase de contrato</param>
    ''' <remarks></remarks>
    Public Sub ChangeGroupDatasourceByContractClass(contractClass As Integer)
        Dim modelo As New MBusqueda
        View.GroupDatasourceXPO = modelo.ConsultarEntidades(eDataSource.GroupByContractClass, contractClass)
    End Sub

    ''' <summary>
    ''' Metodo para obtener los tipo de contrato por clase de contrato
    ''' </summary>
    ''' <param name="contractClass">Id de la clase de contrato</param>
    ''' <remarks></remarks>
    Public Sub ChangeContractTypeDatasourceByContractClass(ByVal ParamArray contractClass() As String)
        Dim modelo As New MBusqueda
        View.ContractTypeDatasourceXPO = modelo.ConsultarEntidades(eDataSource.ContractTypeByContractClass, contractClass)
    End Sub

    ''' <summary>
    ''' Metodo para obtener las unidades funcionales por compañia
    ''' </summary>
    ''' <param name="companyId">El Id de compañia</param>
    ''' <remarks></remarks>
    Public Sub ChangeFunctionalUnitByCompany(companyId As Integer)
        Dim modelo As New MBusqueda
        View.FunctionalUnitDatasourceXPO = modelo.ConsultarEntidades(eDataSource.FunctionalUnitByCompany, companyId)
    End Sub

    ' ''' <summary>
    ' ''' Metodo para obtener los centros de costo por unidad funcional
    ' ''' </summary>
    ' ''' <param name="functionalUnitId"></param>
    ' ''' <remarks></remarks>
    'Public Sub ChangeCostCenterByFunctionalUnit(functionalUnitId As Integer)
    '    Dim modelo As New MBusqueda
    '    View.CostCentersDataSourceXPO = modelo.ConsultarEntidades(eDataSource.CostCenterByFunctionalUnit, functionalUnitId)
    'End Sub

End Class
