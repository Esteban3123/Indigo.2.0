Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISalesExecutiveAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetSalesExecutiveById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetSalesExecutiveByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveSalesExecutive(ByVal ConceptsCausesStatusFolio As SalesExecutive, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateSalesExecutive(ByVal Id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive)

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteSalesExecutive(ByVal ConceptsCausesStatusFolio As SalesExecutive, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive)

End Interface
