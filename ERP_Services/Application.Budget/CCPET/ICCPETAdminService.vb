Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICCPETAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Id">Id del CCPET</param>
    ''' <returns></returns>
    Function GetCCPETById(Id As Integer, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCCPETByCode(Code As String, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Guarda o Actualiza un CCPET
    ''' </summary>
    ''' <param name="CCPET">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="CCPET">The CCPET.</param>
    ''' <returns></returns>
    Function ChangeStatusCCPET(CCPET As CCPET, status As Boolean, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Elimina  un CCPET
    ''' </summary>
    ''' <param name="CCPET">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult

End Interface
