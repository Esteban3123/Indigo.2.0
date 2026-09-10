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
Imports Infrastructure.Data.Base
#End Region

Public Class AccountingBalanceRepository
    Inherits GenericRepository(Of GeneralLedgerBalance)
    Implements IAccountingBalanceRepository, Inject


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener el balance por los parametros correspondientes
    ''' </summary>
    ''' <param name="mont">mes.</param>
    ''' <param name="idAccount">id cuenta contable.</param>
    ''' <param name="idThird">id tercero.</param>
    ''' <param name="idCostCenter">id centro de costo.</param>
    ''' <returns></returns>
    Public Function GetBalanceByMonthAccountThirdCostCenter(mont As Integer, year As Integer, idAccount As Integer, idThird As Integer?, idCostCenter As Integer?) As GeneralLedgerBalance Implements IAccountingBalanceRepository.GetBalanceByMonthAccountThirdCostCenter
        Dim balance As GeneralLedgerBalance
        Dim query = From e In _context.GeneralLedgerBalance
                    Where e.Month = mont And e.Year = year And e.IdMainAccount = idAccount And e.IdThirdParty = idThird And e.IdCostCenter = idCostCenter
                    Select e
        If query.Count > 0 Then
            query.FirstOrDefault().OriginalValue = (From e In _context.GeneralLedgerBalance.AsNoTracking() Where e.Month = mont And e.IdMainAccount = idAccount And e.IdThirdParty = idThird And e.IdCostCenter = idCostCenter Select e).FirstOrDefault()
            balance = query.FirstOrDefault()
            balance.ChangeTracker.State = Entities.ObjectState.Modified
            Return balance
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="periodId"></param>
    ''' <param name="legalBookId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="validateMovement"></param>
    ''' <returns></returns>
    Public Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As Entity.Core.Objects.ObjectResult(Of SP_GeneralLedgerBalance_Result) Implements IAccountingBalanceRepository.RecalculateBalance
        Return _context.SP_GeneralLedgerBalance(periodId, legalBookId, mainAccountId, validateMovement, year)
    End Function

    ''' <summary>
    ''' Funcion para generar la informacion de exogena formato 1001 
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1001(XmlCriterias As String) As List(Of SP_ExogenaFormat1001_Result) Implements IAccountingBalanceRepository.ExogenaFormat1001
        Return _context.SP_ExogenaFormat1001(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1003 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1003(XmlCriterias As String) As List(Of SP_ExogenaFormat1003_Result) Implements IAccountingBalanceRepository.ExogenaFormat1003
        Return _context.SP_ExogenaFormat1003(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1004 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1004(XmlCriterias As String) As List(Of SP_ExogenaFormat1004_Result) Implements IAccountingBalanceRepository.ExogenaFormat1004
        Return _context.SP_ExogenaFormat1004(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1005 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1005(XmlCriterias As String) As List(Of SP_ExogenaFormat1005_Result) Implements IAccountingBalanceRepository.ExogenaFormat1005
        Return _context.SP_ExogenaFormat1005(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1006 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1006(XmlCriterias As String) As List(Of SP_ExogenaFormat1006_Result) Implements IAccountingBalanceRepository.ExogenaFormat1006
        Return _context.SP_ExogenaFormat1006(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera el Formato Exogena 1007
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1007(XmlCriterias As String) As List(Of SP_ExogenaFormat1007_Result) Implements IAccountingBalanceRepository.ExogenaFormat1007
        Return _context.SP_ExogenaFormat1007(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1008 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1008(XmlCriterias As String) As List(Of SP_ExogenaFormat1008_Result) Implements IAccountingBalanceRepository.ExogenaFormat1008
        Return _context.SP_ExogenaFormat1008(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1009 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1009(XmlCriterias As String) As List(Of SP_ExogenaFormat1009_Result) Implements IAccountingBalanceRepository.ExogenaFormat1009
        Return _context.SP_ExogenaFormat1009(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1010 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1010(XmlCriterias As String) As List(Of SP_ExogenaFormat1010_Result) Implements IAccountingBalanceRepository.ExogenaFormat1010
        Return _context.SP_ExogenaFormat1010(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1011 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1011(XmlCriterias As String) As List(Of SP_ExogenaFormat1011_Result) Implements IAccountingBalanceRepository.ExogenaFormat1011
        Return _context.SP_ExogenaFormat1011(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1012 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1012(XmlCriterias As String) As List(Of SP_ExogenaFormat1012_Result) Implements IAccountingBalanceRepository.ExogenaFormat1012
        Return _context.SP_ExogenaFormat1012(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1056 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1056(XmlCriterias As String) As List(Of SP_ExogenaFormat1056_Result) Implements IAccountingBalanceRepository.ExogenaFormat1056
        Return _context.SP_ExogenaFormat1056(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 1647 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat1647(XmlCriterias As String) As List(Of SP_ExogenaFormat1647_Result) Implements IAccountingBalanceRepository.ExogenaFormat1647
        Return _context.SP_ExogenaFormat1647(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 2275 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat2275(XmlCriterias As String) As List(Of SP_ExogenaFormat2275_Result) Implements IAccountingBalanceRepository.ExogenaFormat2275
        Return _context.SP_ExogenaFormat2275(XmlCriterias).ToList()
    End Function

    ''' <summary>
    ''' Genera Formato 2276 de Exogena
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Public Function ExogenaFormat2276(XmlCriterias As String) As List(Of SP_ExogenaFormat2276_Result) Implements IAccountingBalanceRepository.ExogenaFormat2276
        Return _context.SP_ExogenaFormat2276(XmlCriterias).ToList()
    End Function

#End Region

End Class