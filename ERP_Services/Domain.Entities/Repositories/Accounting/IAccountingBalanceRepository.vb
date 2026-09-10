'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 29-05-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports System.Data.Entity.Core.Objects

#End Region
Public Interface IAccountingBalanceRepository
    Inherits IRepository(Of GeneralLedgerBalance)

#Region "Functions"

    ''' <summary>
    ''' Funcion para obtener el balance por los parametros correspondientes
    ''' </summary>
    ''' <param name="mont">mes.</param>
    ''' <param name="idAccount">id cuenta contable.</param>
    ''' <param name="idThird">id tercero.</param>
    ''' <param name="idCostCenter">id centro de costo.</param>
    ''' <returns></returns>
    Function GetBalanceByMonthAccountThirdCostCenter(ByVal mont As Integer, ByVal year As Integer, ByVal idAccount As Integer, ByVal idThird As Int32?, idCostCenter As Int32?) As GeneralLedgerBalance

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As ObjectResult(Of SP_GeneralLedgerBalance_Result)

    ''' <summary>
    ''' Genera Formato 1001 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1001(XmlCriterias As String) As List(Of SP_ExogenaFormat1001_Result)

    ''' <summary>
    ''' Genera Formato 1003 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1003(XmlCriterias As String) As List(Of SP_ExogenaFormat1003_Result)

    ''' <summary>
    ''' Genera Formato 1004 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1004(XmlCriterias As String) As List(Of SP_ExogenaFormat1004_Result)

    ''' <summary>
    ''' Genera Formato 1005 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1005(XmlCriterias As String) As List(Of SP_ExogenaFormat1005_Result)

    ''' <summary>
    ''' Genera Formato 1006 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1006(XmlCriterias As String) As List(Of SP_ExogenaFormat1006_Result)

    ''' <summary>
    ''' Genera Formato 1007 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1007(XmlCriterias As String) As List(Of SP_ExogenaFormat1007_Result)

    ''' <summary>
    ''' Genera Formato 1008 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1008(XmlCriterias As String) As List(Of SP_ExogenaFormat1008_Result)

    ''' <summary>
    ''' Genera Formato 1009 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1009(XmlCriterias As String) As List(Of SP_ExogenaFormat1009_Result)

    ''' <summary>
    ''' Genera Formato 1010 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1010(XmlCriterias As String) As List(Of SP_ExogenaFormat1010_Result)

    ''' <summary>
    ''' Genera Formato 1011 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1011(XmlCriterias As String) As List(Of SP_ExogenaFormat1011_Result)

    ''' <summary>
    ''' Genera Formato 1012 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1012(XmlCriterias As String) As List(Of SP_ExogenaFormat1012_Result)

    ''' <summary>
    ''' Genera Formato 1056 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1056(XmlCriterias As String) As List(Of SP_ExogenaFormat1056_Result)

    ''' <summary>
    ''' Genera Formato 1647 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat1647(XmlCriterias As String) As List(Of SP_ExogenaFormat1647_Result)

    ''' <summary>
    ''' Genera Formato 2275 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat2275(XmlCriterias As String) As List(Of SP_ExogenaFormat2275_Result)

    ''' <summary>
    ''' Genera Formato 2276 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function ExogenaFormat2276(XmlCriterias As String) As List(Of SP_ExogenaFormat2276_Result)

#End Region


End Interface
