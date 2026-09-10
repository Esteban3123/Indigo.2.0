#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceJustificationControl


    ''' <summary>
    ''' Obtiene todas las justificacion control de cuentas hospitalarias
    ''' </summary>
    ''' <returns>Lista de justificacion control</returns>
    <OperationContract()>
    Function ListAllJustificationControl() As List(Of BillingJustificationControl)

    ''' <summary>
    ''' Obtiene una justificacion control especifico
    ''' </summary>
    ''' <param name="code">Codigo de la justificacion control</param>
    ''' <returns>Justificacion control</returns>
    <OperationContract()>
    Function GetBillingJustification(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of BillingJustificationControl)

    ''' <summary>
    ''' Devuelve una justificacion control por ID
    ''' </summary>
    ''' <param name="id">Id de la justificacion</param>
    ''' <returns>Justificacion control</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBillingJustificationById(ByVal id As Integer) As ActionResult(Of BillingJustificationControl)

    '''''' <summary>
    ''' graba una justificacion
    ''' </summary>
    ''' <param name="justificationControl">justificacion control</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveJustificationControl(ByVal justificationControl As BillingJustificationControl, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BillingJustificationControl)

    ''' <summary>
    ''' Elimina un Pais
    ''' </summary>
    ''' <param name="justificationControl">Justificacion control que se desa eliminar</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    <OperationContract()>
    Function DeleteJustificationControl(ByVal justificationControl As BillingJustificationControl, ByVal audit As AuditMessage) As ActionResult

End Interface
