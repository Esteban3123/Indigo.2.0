'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IStayRepository
    Inherits IRepository(Of CHREGESTA)

    Function GetStayByAdmissionNumberManualLiquidation(admissionNumber As String) As List(Of CHREGESTA)

    Function GetStayByAdmissionNumber(admissionNumber As String) As List(Of CHREGESTA)

    ''' <summary>
    ''' Obtiene la tabla HCFARMEPD relacionada con la solitud de Dashboard farmacia
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="patienCode"></param>
    ''' <param name="requestNumber"></param>
    ''' <returns></returns>
    Function GetHCFARMEPDByRequest(admissionNumber As String, patienCode As String, requestNumber As Integer) As HCFARMEPD

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia.</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Function ListStaysByAdmissionCodeAndStatus(ByVal admissionCode As String, ByVal status As StayStatusEnum, Optional ByVal asNoTracking As Boolean = True) As List(Of CHREGESTA)

    ''' <summary>
    ''' Lista las estancias por número de ingreso que se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Function ListLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, Optional ByVal asNoTracking As Boolean = True) As List(Of CHREGESTA)

    ''' <summary>
    ''' Lista las estancias por número de ingreso que no se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Function ListDontLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, Optional ByVal asNoTracking As Boolean = True) As List(Of CHREGESTA)

    ''' <summary>
    ''' Obtiene la cantidad total de estancias liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número del ingreso</param>
    ''' <returns>Total de estancias liquidadas</returns>
    Function CountLiquidatedStaysByAdmissionCode(ByVal admissionCode As String) As Integer

    ''' <summary>
    ''' obtiene una estancia por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStayById(id As Integer) As CHREGESTA
    ''' <summary>
    ''' obtiene las estancias en las que esta el paciente
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStayByPatienCodeAdmissionNumberAndStatus(patientCode As String, admissionNumber As String, status As Integer) As List(Of CHREGESTA)

    ''' <summary>
    ''' metodo que retorna la primera orden medica de hospitalizacion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Function GetFirstHCHISPACAByAdmissionNumber(admissionNumber As String) As HCHISPACA

End Interface
