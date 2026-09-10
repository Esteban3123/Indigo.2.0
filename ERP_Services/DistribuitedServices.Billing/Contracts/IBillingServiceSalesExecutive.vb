
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface ISalesExecutive

#Region "Methods"

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSalesExecutiveByCode(code As String, audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSalesExecutiveById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="SalesExecutive">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage, idSequense As Int64) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateSalesExecutive(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="SalesExecutive">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage) As ActionResult(Of SalesExecutive)

#End Region

End Interface
