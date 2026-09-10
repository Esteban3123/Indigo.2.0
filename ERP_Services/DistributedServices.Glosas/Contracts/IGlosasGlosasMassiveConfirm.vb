'***********************************************************************
' Assembly         : DistributedServices.Glosas
' Author           : Carlos Ernesto Cordoba
' Created          : 30-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IGlosasGlosasMassiveConfirm
    
    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de glosas
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="code"></param>
    ''' <param name="session"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmGlosaDocument(processId As Integer, code As String, session As SessionValues, Optional operativeUnitId As Integer = 0) As ActionResult(Of Tuple(Of String, Integer))
    
    ''' <summary>
    ''' metodo para confirmar documentos de glosas masivamente
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmGlosasDocuments(processId As Integer, listDocuments As List(Of String), session As SessionValues, Optional operativeUnitId As Integer = 0) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
