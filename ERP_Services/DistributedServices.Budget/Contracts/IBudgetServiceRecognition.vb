'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IBudgetServiceRecognition

    ''' <summary>
    ''' Obtiene un reconocimiento por código
    ''' </summary>
    '''<param name="Code">Código del reconocimiento</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition)

    ''' <summary>
    ''' Obtiene un reconocimiento por id
    ''' </summary>
    '''<param name="Id">Id del reconocimiento</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRecognitionById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition)

    ''' <summary>
    ''' Guarda o Actualiza un reconocimiento
    ''' </summary>
    ''' <param name="recognition"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRecognition(recognition As Domain.Entities.Recognition, listDetailsForDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition)

    ''' <summary>
    ''' Elimina un reconocimiento
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRecognition(recognition As Domain.Entities.Recognition, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface
