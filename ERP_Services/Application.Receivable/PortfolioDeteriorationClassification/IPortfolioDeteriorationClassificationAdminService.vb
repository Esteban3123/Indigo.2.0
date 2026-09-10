'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Oscar Astudillo reyes 
' Created          : 2024-11-10
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


Public Interface IPortfolioDeteriorationClassificationAdminService
    Inherits IDisposable

#Region "Methods"
    ''' <summary>
    ''' Obtiene registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetPortfolioDeteriorationClassificationByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification)


    ''' <summary>
    ''' Guarda clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function SavePortfolioDeteriorationClassification(ByVal portfolioDeteriorationClassification As PortfolioDeteriorationClassification, ByVal audit As AuditMessage, Optional ByVal idSequence As Integer = 0) As ActionResult(Of PortfolioDeteriorationClassification)

    ''' <summary>
    ''' Elimina clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeletePortfolioDeteriorationClassification(ByVal portfolioDeteriorationClassification As PortfolioDeteriorationClassification, audit As AuditMessage) As ActionResult

    ''' <summary>
    '''  Cambia de estado de una clasificacion deterioro de Cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStatePortfolioDeteriorationClassification(ByVal portfolioDeteriorationClassification As PortfolioDeteriorationClassification, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification)

#End Region

End Interface
