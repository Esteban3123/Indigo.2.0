'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-07-23
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IFixedAssetDeteriorationIndicationService

    ''' <summary>
    ''' Función que obtiene un indicio de deterioro por Código
    ''' </summary>
    ''' <param name="Code">Código del indicio de deterioro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDeteriorationIndicationByCode(Code As String) As Domain.Entities.DeteriorationIndications

    ''' <summary>
    ''' Función que obtiene todos los indicios de deterioro
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllDeteriorationIndications(audit As AuditMessage) As List(Of Domain.Entities.DeteriorationIndications)

    ''' <summary>
    ''' Función para Almacenar un indicio de deterioro
    ''' </summary>
    ''' <param name="DeteriorationIndication"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetDeteriorationIndication(DeteriorationIndication As Domain.Entities.DeteriorationIndications, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.DeteriorationIndications)

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeteriorationIndicationChangeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.DeteriorationIndications)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="DeteriorationIndication"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteFixedAssetDeteriorationIndication(DeteriorationIndication As Domain.Entities.DeteriorationIndications, audit As AuditMessage) As ActionResult

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
    <OperationContract()>
    Function GetListReportDeteriorationIndicationsByPhysicalAsset(Year As Integer, Month As Integer, ReportType As Integer, InitialPlate As String, FinalPlate As String, LegalBookId As Integer, InitialMainAccount As String, FinalMainAccount As String, session As SessionValues) As DataSet

End Interface
