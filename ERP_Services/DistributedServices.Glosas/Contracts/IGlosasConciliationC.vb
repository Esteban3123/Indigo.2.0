Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasConciliationC

#Region "ConciliationC"

    ''' <summary>
    ''' Obtiene una cabecera conciliación por código.
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    <OperationContract>
    Function GetConciliationC(ByVal code As String, ByVal session As SessionValues) As ConciliationC

    ''' <summary>
    ''' Obtiene una cabecera conciliación especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    <OperationContract>
    Function GetConciliationCByConsecutive(ByVal Consecutive As String, ByVal session As SessionValues) As ConciliationC

    ''' <summary>
    ''' Guarda una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveConciliationC(ByVal ConciliacionC As ConciliationC, ByVal session As SessionValues) As ActionResult(Of ConciliationC)

    ''' <summary>
    ''' Elimina una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteConciliationC(ByVal ConciliacionC As ConciliationC, ByVal session As SessionValues) As ActionResult

#End Region

End Interface
