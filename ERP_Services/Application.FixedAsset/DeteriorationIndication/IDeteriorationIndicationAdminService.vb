#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IDeteriorationIndicationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Permite obtener el indicio de deterioro por el código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeteriorationIndication(ByVal Code As String) As DeteriorationIndications

    ''' <summary>
    ''' Permite listar todos los inidicios de deterioros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllDeteriorationIndications() As List(Of DeteriorationIndications)

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="DeteriorationIndicationAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveDeteriorationIndication(ByVal DeteriorationIndicationAsset As DeteriorationIndications, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DeteriorationIndications)

    ''' <summary>
    ''' Permite cambiar el estado del indicio de deterioro
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeteriorationIndicationChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of DeteriorationIndications)

    ''' <summary>
    ''' Permite eliminar un indicio de deterioro
    ''' </summary>
    ''' <param name="DeteriorationIndication"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteDeteriorationIndication(ByVal DeteriorationIndication As DeteriorationIndications, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Indicios de Deterioro de Activos Fijos.
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="ReportType"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="LegalBookId"></param>
    ''' <param name="InitialMainAccount"></param>
    ''' <param name="FinalMainAccount"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GetListReportDeteriorationIndicationsByPhysicalAsset(Year As Integer, Month As Integer, ReportType As Integer, InitialPlate As String, FinalPlate As String, LegalBookId As Integer, InitialMainAccount As String, FinalMainAccount As String, session As SessionValues) As DataSet

End Interface
