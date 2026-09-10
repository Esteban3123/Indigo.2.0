'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IProvisionRangesAdminService
    Inherits IDisposable
#Region "Methods"
    ''' <summary>
    ''' guardar un Rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">rango de provision</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveProvisionRanges(ByVal provisionRanges As ProvisionRanges, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ProvisionRanges)
    ''' <summary>
    ''' eliminar un Rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">rango de provision</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteProvisionRanges(ByVal provisionRanges As ProvisionRanges, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' metodo para obtener un Rango de provision
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetProvisionRangesByCode(ByVal code As String, ByVal audit As AuditMessage) As ProvisionRanges
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateProvisionRanges(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ProvisionRanges)
#End Region
End Interface
