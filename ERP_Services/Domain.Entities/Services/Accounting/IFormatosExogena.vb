'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Rafael Patiño
' Created          : 25-04-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IFormatosExogena

    ''' <summary>
    ''' Genera el Formato Exogena 1001
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1001(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1003
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1003(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1004
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1004(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1005
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1005(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1006
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1006(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1007
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1007(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1008
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1008(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1009
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1009(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1010
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1010(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1011
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1011(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1012
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1012(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 1056
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1056(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exógena 1647
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat1647(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 2275
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat2275(criterias As Dictionary(Of String, String)) As String

    ''' <summary>
    ''' Genera Formato Exogena 2276
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GenerarExogenaXMLFormat2276(criterias As Dictionary(Of String, String)) As String

End Interface
