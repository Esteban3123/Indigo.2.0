'************************************************************
' Assembly         : Domain.Crystal.Service
' Author           : Juan F. Tamayo
' Created          : 2015-01-25
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IStayService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Lista las estancias que se encuentran sin liquidar, con el calculo de las unidades
    ''' que le corresponde a cada estancias hasta la fecha dada
    ''' </summary>
    ''' <param name="admissionCode"></param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica</param>
    ''' <param name="endDate">Fecha limite a liquidar</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las entidades con seguimiento</param>
    ''' <returns>Lista de estancias sin liquidar, liquidadas hasta la fecha dada</returns>
    Function ListDontLiquidatedStays(ByVal admissionCode As String, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, Optional ByVal endDate As DateTime? = Nothing, Optional ByVal asNoTracking As Boolean = True) As ActionResult(Of List(Of CHREGESTA))
    Function ListOfStaysWithConfigurationByAdmission(admissionCode As String,
            stayOption As eLiquidateStayOption,
            medicalOrderDate As Date?,
            Optional endDate As Date? = Nothing) As List(Of CHREGESTA)
    Function ListOfStaysWithConfigurationByAdmissionToModel(admissionCode As String,
            stayOption As eLiquidateStayOption,
            medicalOrderDate As Date?,
            audit As AuditMessage,
            Optional endDate As Date? = Nothing) As List(Of StayInfoModel)
#End Region

End Interface
