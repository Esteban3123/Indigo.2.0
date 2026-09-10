'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Carlos Ernesto Cordoba
' Created          : 30-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IGlosasMassiveConfirmAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de glosas
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <param name="indigoSessionValues"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmGlosaDocument(processId As Integer, code As String, audit As AuditMessage, indigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de cuentas por cobrar masivamente
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmGlosasDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage, IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
