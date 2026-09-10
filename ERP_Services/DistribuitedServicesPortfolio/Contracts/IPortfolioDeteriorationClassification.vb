'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-10
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IPortfolioDeteriorationClassification

#Region "Methods"

    ''' <summary>
    ''' Obtiene registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioDeteriorationClassificationByCode(code As String, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification)

    ''' <summary>
    ''' Guarda clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, idSequence As Integer, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification)

    ''' <summary>
    '''  Elimina clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia de estado de una clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification)

#End Region

End Interface
