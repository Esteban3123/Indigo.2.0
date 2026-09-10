'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IEconomicIndicatorAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    Function GetEconomicIndicatorByCode(ByVal code As String, ByVal audit As AuditMessage) As EconomicIndicator
    ''' <summary>
    ''' Metodo para listar todos los indicadores economicos
    ''' </summary>
    ''' <returns></returns>
    Function GetAllEconomicIndicator(ByVal audit As AuditMessage) As List(Of EconomicIndicator)

    ''' <summary>
    ''' Metodo para obtener un indicador economico por añoy mes
    ''' </summary>
    Function GetEconomicIndicator(ByVal year As String, ByVal month As String, ByVal audit As AuditMessage) As EconomicIndicator

    ''' <summary>
    ''' metodo para guardar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveEconomicIndicator(ByVal economicIndicator As EconomicIndicator, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of EconomicIndicator)

    ''' <summary>
    ''' metodo para eliminar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteEconomicIndicator(ByVal economicIndicator As EconomicIndicator, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of EconomicIndicator)
#End Region

End Interface
