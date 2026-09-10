Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Interface IInterfacePublicNET
    Inherits IDisposable


    Function RadicateObjection(ByVal IndigoEmpresa As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal Tercero As String,
                               ByVal NameContainer As String, ByVal ValorFac As Decimal, ByVal FechaFactura As Integer, ByVal intOpcion As String,
                               ByVal User As String, ByVal plancodigo As String) As InterfaceResult

    Function CreateCreditNote(ByVal NameContainer As String, ByVal Tercero As String, ByVal factura As String, ByVal ValorFac As Decimal, ByVal User As String,
                              ByVal Cuenta As String, ByVal Concepto As String, ByVal CuentaConcepto As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult


    Function AceptacionEAPBTotal(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, ByVal plancodigo As String) As InterfaceResult

    Function AcceptanceIPS(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, ByVal AfectaServicio As Boolean, ByVal plancodigo As String, Optional VAlorAceptadoEAPB As Decimal = 0, Optional StatePortfolioDGH As String = "") As InterfaceResult


    ''' <summary>
    ''' Proceso de Radicacion de Factura y Actualizacion de Cartera ERP  NOTA CONTABLE
    ''' </summary>
    ''' <param name="IndigoEmpresa"></param>
    ''' <param name="listRadicated"></param>
    ''' <param name="NameContainer"></param>
    ''' <param name="intOpcion"></param>
    ''' <param name="User"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult)

End Interface
