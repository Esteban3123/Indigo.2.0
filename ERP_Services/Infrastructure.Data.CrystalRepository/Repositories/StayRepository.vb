'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Class StayRepository
    Inherits GenericRepository(Of CHREGESTA)
    Implements IStayRepository




    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia. 1-Activo, 2-Pendiente Liquidar, 3-Liquidada</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListStaysByAdmissionCodeAndStatus(admissionCode As String, status As StayStatusEnum, Optional asNoTracking As Boolean = True) As List(Of CHREGESTA) Implements IStayRepository.ListStaysByAdmissionCodeAndStatus
        If asNoTracking Then
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.AsNoTracking().Include("CHREGESTADET").AsNoTracking().Include("ADINGRESO").AsNoTracking().Include("CHTIPESTA").AsNoTracking().Include("CHCAMASHO").AsNoTracking().Include("CHCAMASHO.CHGENTARI").AsNoTracking().Include("INPACIENT").AsNoTracking() Where est.ADINGRESO.NUMINGRES = admissionCode And est.GENESTLIQ = CInt(status) Select est Order By est.FECINIEST Ascending).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        Else
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.AsNoTracking().Include("CHREGESTADET").Include("ADINGRESO").Include("CHTIPESTA").Include("CHCAMASHO").Include("CHCAMASHO.CHGENTARI").Include("INPACIENT") Where est.ADINGRESO.NUMINGRES = admissionCode And est.GENESTLIQ = CInt(status) Select est Order By est.FECINIEST Ascending).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso que se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListLiquidatedStaysByAdmissionCode(admissionCode As String, Optional asNoTracking As Boolean = True) As List(Of CHREGESTA) Implements IStayRepository.ListLiquidatedStaysByAdmissionCode
        'Dim DOS As Integer = 2 'Liquidada parcial
        Dim TRES As Integer = 3 'Liquidada total

        If asNoTracking Then
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.AsNoTracking().Include("CHREGESTADET").AsNoTracking().Include("ADINGRESO").AsNoTracking().Include("CHTIPESTA").AsNoTracking().Include("CHCAMASHO").AsNoTracking().Include("CHCAMASHO.CHGENTARI").AsNoTracking().Include("CHCAMASHO.ADCENATEN").AsNoTracking().Include("CHCAMASHO.CHGENTARI.CHTIPESTA").AsNoTracking().Include("CHCAMASHO.CHGENTARI.ADCENATEN").AsNoTracking().Include("INPACIENT").AsNoTracking() Where est.ADINGRESO.NUMINGRES = admissionCode And (est.GENESTLIQ = TRES) Select est Order By est.FECINIEST Ascending).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        Else
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.Include("CHREGESTADET").Include("ADINGRESO").Include("CHTIPESTA").Include("CHCAMASHO").Include("CHCAMASHO.CHGENTARI").Include("CHCAMASHO.ADCENATEN").Include("CHCAMASHO.CHGENTARI.CHTIPESTA").Include("CHCAMASHO.CHGENTARI.ADCENATEN").Include("INPACIENT") Where est.ADINGRESO.NUMINGRES = admissionCode And (est.GENESTLIQ = TRES) Select est Order By est.FECINIEST Ascending).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso que no se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListDontLiquidatedStaysByAdmissionCode(admissionCode As String, Optional asNoTracking As Boolean = True) As List(Of CHREGESTA) Implements IStayRepository.ListDontLiquidatedStaysByAdmissionCode
        Dim UNO As Integer = 1 'Sin liquidar
        Dim DOS As Integer = 2 'Liquidada parcial

        If asNoTracking Then
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.AsNoTracking().Include("CHREGESTADET").AsNoTracking().Include("ADINGRESO").AsNoTracking(). _
                       Include("CHTIPESTA").AsNoTracking().Include("CHCAMASHO").AsNoTracking().Include("CHCAMASHO.CHGENTARI").AsNoTracking(). _
                       Include("CHCAMASHO.CHGENTARI.INUNIFUNC").AsNoTracking().Include("CHCAMASHO.INUNIFUNC").Include("CHCAMASHO.ADCENATEN").AsNoTracking(). _
                       Include("CHCAMASHO.CHGENTARI.CHTIPESTA").AsNoTracking().Include("CHCAMASHO.CHGENTARI.ADCENATEN").AsNoTracking().Include("INPACIENT").AsNoTracking()
                       Where est.NUMINGRES = admissionCode And (est.GENESTLIQ = UNO OrElse est.GENESTLIQ = DOS OrElse est.GENESTLIQ Is Nothing) Select est Order By est.FECINIEST Ascending).ToList()

            If res IsNot Nothing AndAlso res.Count > 0 Then
                For Each i In res
                    If i.CHCAMASHO IsNot Nothing AndAlso i.CHCAMASHO.INUNIFUNC IsNot Nothing Then
                        i.FunctionalUnitCodeName = String.Concat(i.CHCAMASHO.INUNIFUNC.UFUCODIGO.Trim(), " - ", i.CHCAMASHO.INUNIFUNC.UFUDESCRI.Trim())
                    End If
                Next
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        Else
            Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.Include("CHREGESTADET").Include("ADINGRESO").Include("CHTIPESTA").Include("CHCAMASHO").Include("CHCAMASHO.INUNIFUNC").Include("CHCAMASHO.CHGENTARI").Include("CHCAMASHO.ADCENATEN").Include("CHCAMASHO.CHGENTARI.CHTIPESTA").Include("CHCAMASHO.CHGENTARI.ADCENATEN").Include("INPACIENT") Where est.ADINGRESO.NUMINGRES = admissionCode And (est.GENESTLIQ = UNO OrElse est.GENESTLIQ = DOS OrElse est.GENESTLIQ Is Nothing) Select est Order By est.FECINIEST Ascending).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                For Each i In res
                    If i.CHCAMASHO IsNot Nothing AndAlso i.CHCAMASHO.INUNIFUNC IsNot Nothing Then
                        i.FunctionalUnitCodeName = String.Concat(i.CHCAMASHO.INUNIFUNC.UFUCODIGO, " - ", i.CHCAMASHO.INUNIFUNC.UFUDESCRI)
                    End If
                Next
                Return res
            Else
                Return New List(Of CHREGESTA)()
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad total de estancias liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número del ingreso</param>
    ''' <returns>Total de estancias liquidadas</returns>
    Public Function CountLiquidatedStaysByAdmissionCode(admissionCode As String) As Integer Implements IStayRepository.CountLiquidatedStaysByAdmissionCode
        Dim DOS As Integer = 2 'Liquidada parcial
        Dim TRES As Integer = 3 'Liquidada total

        Dim res = (From est As CHREGESTA In Me._crystalContext.CHREGESTA.AsNoTracking() Where est.ADINGRESO.NUMINGRES = admissionCode And (est.REGESTADO = DOS OrElse est.REGESTADO = TRES) Select est).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res.Count
        Else
            Return 0
        End If
    End Function



    ''' <summary>
    ''' obtiene una estancia por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetStayById(id As Integer) As CHREGESTA Implements IStayRepository.GetStayById
        Return (From s In _crystalContext.CHREGESTA.Include("CHREGESTADET") Where s.ID = id Select s).FirstOrDefault()
    End Function


    ''' <summary>
    ''' obtiene las estancias en las que esta el paciente
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Public Function GetStayByPatienCodeAdmissionNumberAndStatus(patientCode As String, admissionNumber As String, status As Integer) As List(Of CHREGESTA) Implements IStayRepository.GetStayByPatienCodeAdmissionNumberAndStatus
        Return (From s In _crystalContext.CHREGESTA.Include("CHCAMASHO") Where s.INPACIENT.IPCODPACI = patientCode And s.ADINGRESO.NUMINGRES = admissionNumber And s.REGESTADO = status Select s).ToList()
    End Function


    Public Function GetStayByAdmissionNumber(admissionNumber As String) As List(Of CHREGESTA) Implements IStayRepository.GetStayByAdmissionNumber
        Dim dateStay = CDate("1900-01-01 00:00:00.000")
        Return (From c In _crystalContext.CHREGESTA.Include("CHCAMASHO").Include("CHCAMASHO.INUNIFUNC") Where c.NUMINGRES = admissionNumber And c.FECFINEST <= dateStay Select c).ToList()
    End Function

    ''' <summary>
    ''' Consulta la tabla HCFARMEPD para obtener el origen de la solicitud
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="patienCode"></param>
    ''' <param name="requestNumber"></param>
    ''' <returns></returns>
    Public Function GetHCFARMEPDByRequest(admissionNumber As String, patienCode As String, requestNumber As Integer) As HCFARMEPD Implements IStayRepository.GetHCFARMEPDByRequest
        Return (From h In _crystalContext.HCFARMEPD Where h.NUMINGRES = admissionNumber And h.IPCODPACI = patienCode And h.CODCONCEC = requestNumber).FirstOrDefault()
    End Function


    Public Function GetStayByAdmissionNumberManualLiquidation(admissionNumber As String) As List(Of CHREGESTA) Implements IStayRepository.GetStayByAdmissionNumberManualLiquidation
        Return (From c In _crystalContext.CHREGESTA Where c.NUMINGRES = admissionNumber Select c).ToList()
    End Function

    ''' <summary>
    ''' funcion que cosnulta la primera orden medica a hospitalizacion del ingreso
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetFirstHCHISPACAByAdmissionNumber(admissionNumber As String) As HCHISPACA Implements IStayRepository.GetFirstHCHISPACAByAdmissionNumber
        Dim query = (From x In _crystalContext.HCHISPACA.AsNoTracking()
                     Where x.NUMINGRES = admissionNumber And {3, 4, 5, 6, 8, 19, 20, 21}.Contains(x.INDICAPAC)
                     Order By CInt(x.NUMEFOLIO))?.FirstOrDefault
        Return query
    End Function

End Class
