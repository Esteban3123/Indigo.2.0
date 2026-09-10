'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceCCPET

    ''' <summary>
    ''' Obtiene un CCPET por código 
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCCPETById(Id As Integer, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Obtiene un CCPET por código 
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCCPETByCode(Code As String, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="CCPET">la entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult(Of CCPET)

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="CCPET">The CCPET.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatusCCPET(CCPET As CCPET, status As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CCPET)

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CCPET">La entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult

End Interface

