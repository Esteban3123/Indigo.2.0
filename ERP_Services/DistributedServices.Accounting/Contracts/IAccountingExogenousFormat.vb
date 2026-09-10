#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IAccountingExogenousFormat

    ''' <summary>
    ''' Obtiene un formato de exógena por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetExogenousFormatById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ExogenousFormat)

    ''' <summary>
    ''' Obtiene un formato de exógena por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetExogenousFormatByCode(code As String, ByVal audit As AuditMessage) As ActionResult(Of ExogenousFormat)

    ''' <summary>
    ''' Guarda o Actualiza un formato de exógena
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveExogenousFormat(ByVal ExogenousFormat As ExogenousFormat, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ExogenousFormat)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateExogenousFormat(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ExogenousFormat)

End Interface
