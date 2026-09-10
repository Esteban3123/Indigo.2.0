Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasResponsible

#Region "Responsible"

    ''' <summary>
    ''' Lists the Responsible all.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListResponsibleAll(session As SessionValues) As List(Of ResponsibleAll)
    ''' <summary>
    ''' Elimina un Responsible
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteResponsible(ByVal Responsible As Responsible, session As SessionValues) As ActionResult
    ''' <summary>
    ''' graba un Responsible
    ''' </summary>
    ''' <param name="Responsible">el Responsible</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveResponsible(ByVal Responsible As Responsible, session As SessionValues) As ActionResult(Of Responsible)
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo del Responsible</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetResponsible(ByVal codeResponsible As String, session As SessionValues) As Responsible
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo ERP del Responsible</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetResponsibleByCodeERP(ByVal codeResponsible As String, session As SessionValues) As Responsible
    ''' <summary>
    ''' Función que retorna una lista de responsables
    ''' para reasignar 
    ''' </summary>
    ''' <param name="IdResponsible">Id Responsable</param>
    ''' <param name="session">variable sesión</param>
    ''' <returns>Lista de responsables por movimientos</returns>
    <OperationContract()>
    Function listAllResponsiblesTransfer(IdResponsible As String, session As SessionValues) As List(Of ResponsibleMovements)


#End Region

End Interface
